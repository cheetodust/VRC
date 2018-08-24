using System;
using System.Collections.Generic;
using System.Collections;
using AppKit;
using Foundation;
using System.IO;
using System.Threading;
using System.Diagnostics;
namespace Corruptor
{
    public partial class ViewController : NSViewController
    {
        static private Process Emulator = null;
        string directory; //ROM directory
        //string currentSavePath; //Path where ROM should be saved

        static private RomId SelectedROM = null;

        RomDirDataSource romDir = new RomDirDataSource();
        NSError romDirError = new NSError();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            RomDirTable.DataSource = romDir;
            RomDirTable.Delegate = new RomDirDelegate(romDir);
            // Do any additional setup after loading the view.
        }

        nint OpenPanel(bool chooseFiles, bool chooseDirectories, out string dir)
        {
            nint res = 1;
            var openPanel = new NSOpenPanel
            {
                CanChooseFiles = chooseFiles,
                CanChooseDirectories = chooseDirectories,
            };
            NSWindow window = new NSWindow { };
            openPanel.BeginSheet(window, result =>
            {
                window.EndSheet(window);
                res = result;
            });
            if (res == 1)
            {
                dir = openPanel.DirectoryUrl.Path;
            }
            else dir = "";
            return res;
        }
        nint SavePanel(out string target)
        {
            nint res = 1;
            var savePanel = new NSSavePanel { };
            NSWindow window = new NSWindow { };
            savePanel.BeginSheet(window, (result) =>
            {
                window.EndSheet(window);
                res = result;
            });
            if (res == 1)
            {
                target = savePanel.Url.Path;
            }
            else target = "";
            return res;
        }
        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        private byte[] Corrupt(byte[] ROM)
        {
            // Read in all of the text boxes.
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            uint EveryNthByte;
            try
            {
                EveryNthByte = Convert.ToUInt32(EveryNthByteField.StringValue);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid byte corruption interval.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid byte corruption interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (EveryNthByte == 0)
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid byte corruption interval.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid byte corruption interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int AddXtoByte;
            try
            {
                AddXtoByte = Convert.ToInt32(AddXtoByteField.StringValue);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid byte addition value.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid byte addition value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int ShiftRightXBytes;
            try
            {
                ShiftRightXBytes = Convert.ToInt32(ShiftRightXBytesField.StringValue);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid right shift value", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid right shift value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            byte ReplaceByteXwithYByteX;
            try
            {
                ReplaceByteXwithYByteX = Convert.ToByte(ReplaceByteXwithYByteXField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid byte to match", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid byte to match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            byte ReplaceByteXwithYByteY;
            try
            {
                ReplaceByteXwithYByteY = Convert.ToByte(ReplaceByteXwithYByteYField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid byte replacement.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid byte replacement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Limit the end byte.
            if (EndByte > (ROM.LongLength - 1))
            {
                EndByte = ROM.LongLength - 1;
            }

            // Set byte corruption option.
            Corruption.ByteCorruptionOptions ByteCorruptionOption = Corruption.ByteCorruptionOptions.AddXToByte;
            if (ShiftRightRadioButton.State == NSCellStateValue.On) ByteCorruptionOption = Corruption.ByteCorruptionOptions.ShiftRightXBytes;
            else if (ReplaceRadioButton.State == NSCellStateValue.On) ByteCorruptionOption = Corruption.ByteCorruptionOptions.ReplaceByteXwithY;

            // Corrupt.
            ROM = Corruption.Run(ROM, ToggleByteCorruptionButton.State == NSCellStateValue.On, StartByte, EndByte, ByteCorruptionOption,
                                 EveryNthByte, AddXtoByte, ShiftRightXBytes, ReplaceByteXwithYByteX, ReplaceByteXwithYByteY, /* cpu jam */false,
                /* text replacement */false, /* text use byte corruption range */false, /* text to replace */"", /* replace with */"", /* anchor words */"",
                /* color replacement */false, /* color use byte corruption range */ false, /* colors to replace */"", /* replace colors */"");

            return ROM;
        }

        partial void RunButton(NSObject sender)
        {
            // Check that we can write to the file.
            if (File.Exists(RomSaveField.StringValue) && ToggleOverwriteFile.State == NSCellStateValue.Off)
            {
                if (ToggleOverwriteFile.State == NSCellStateValue.Off)
                {
                    new NSAlert { MessageText = "Error", InformativeText = "File to save to exists and overwrite is not enabled.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                    //MessageBox.Show("File to save to exists and overwrite is not enabled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Check that a ROM is selected.
            if (SelectedROM == null)
            {
                new NSAlert { MessageText = "Error", InformativeText = "No ROM is selected.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("No ROM is selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read the ROM in.
            byte[] ROM = SelectedROM.Load(); //File.ReadAllBytes($"{directory}/{currentEntry}");
            if (ROM == null)
            {
                new NSAlert { MessageText = "Error", InformativeText = "Error reading ROM.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Error reading ROM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ROM = Corrupt(ROM);
            if (ROM == null)
            {
                return;
            }

            // Write the file.
            try
            {
                File.WriteAllBytes(RomSaveField.StringValue, ROM);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Error reading ROM.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Error saving corrupted ROM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ToggleEmulatorButton.State == NSCellStateValue.On)
            {
                if (EmulatorField.StringValue == "")
                {
                    new NSAlert { MessageText = "Error", InformativeText = "No emulator path given.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                    return;
                }
                try
                {
                    Emulator = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(EmulatorField.StringValue))[0];
                    Emulator.Kill();
                    Emulator.WaitForExit();
                    try
                    {
                        Emulator = Process.Start("\"" + EmulatorField.StringValue + "\"", "\"" + RomSaveField.StringValue + "\"");
                    }
                    catch
                    {
                        new NSAlert { MessageText = "Error", InformativeText = "Error starting emulator.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                        return;
                    }
                    Emulator.WaitForInputIdle();
                    Thread.Sleep(500);
                }
                catch
                {
                    try
                    {
                        Emulator = Process.Start("\"" + EmulatorField.StringValue + "\"", "\"" + RomSaveField.StringValue + "\"");
                    }
                    catch
                    {
                        new NSAlert { MessageText = "Error", InformativeText = "Error starting emulator.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                        return;
                    }
                }
            }
        }

        #region RomAndEmulator

        partial void RomDirButton(NSObject sender)
        {
            var openPanel = new NSOpenPanel
            {
                CanChooseFiles = false,
                CanChooseDirectories = true,
                Title = "Select ROM Directory"
            };
            if (openPanel.RunModal() == 1)
            {
                directory = openPanel.DirectoryUrl.Path;
                RomDirField.StringValue = directory;
                List<string> dirEntries = new List<string>(NSFileManager.DefaultManager.GetDirectoryContent(directory, out romDirError));
                romDir.DirList.Clear();
                romDir.romDict.Clear();
                romDir.DirList.AddRange(dirEntries);
                foreach (string entry in dirEntries)
                {
                    bool isDir = false;
                    if (NSFileManager.DefaultManager.FileExists($"{directory}/{entry}", ref isDir) && isDir)
                    {
                        romDir.DirList.Remove(entry);
                    }
                    else
                    {
                        romDir.romDict.Add(entry, new RomId($"{directory}/{entry}"));
                    }
                }
                RomDirTable.ReloadData();
            }
            //NSWindow window = new NSWindow { };
            //openPanel.BeginSheet(window, result =>
            //{
            //    if (result == 1)
            //    {

            //    }
            //    window.EndSheet(window);
            //});
        }

        partial void RomSaveButton(NSObject sender)
        {
            var savePanel = new NSSavePanel
            {
                Title = "Select Save Location for Corrupted ROM",
                NameFieldStringValue = "CorruptedROM.rom"
            };
            if (savePanel.RunModal() == 1)
            {
                RomSaveField.StringValue = savePanel.Url.Path;
            }


            //NSWindow window = new NSWindow { };
            //savePanel.BeginSheet(window, result =>
            //{
            //    if (result == 1)
            //    {
            //        RomSaveField.StringValue = savePanel.Url.Path;
            //    }
            //    savePanel.EndSheet()
            //});
        }

        partial void RomDirTableAction(NSObject sender)
        {
            SelectedROM = romDir.romDict[romDir.DirList[(int)RomDirTable.SelectedRow]];
            EnforceAutoEnd();
            //currentEntry = romDir.DirList[(int)RomDirTable.SelectedRow];
        }

        partial void EmulatorSelectButton(NSObject sender)
        {
            var openPanel = new NSOpenPanel
            {
                Title = "Select Emulator to Run",
                AllowedFileTypes = new string[] { "app" }
            };
            if (openPanel.RunModal() == 1)
            {
                EmulatorField.StringValue = openPanel.Url.Path;
            }
        }

        partial void ToggleEmulator(NSObject sender)
        {
            EmulatorLabel.Enabled = ToggleEmulatorButton.State == NSCellStateValue.On;
            EmulatorField.Enabled = ToggleEmulatorButton.State == NSCellStateValue.On;
            EmulatorSelectButtonOutlet.Enabled = ToggleEmulatorButton.State == NSCellStateValue.On;
        }
        #endregion
        #region Corruption

        partial void CorruptionOption(NSObject sender)
        {

        }

        partial void ToggleByteCorruption(NSButton sender)
        {
            if (sender.State == NSCellStateValue.On)
            {
                foreach (NSView sub in ByteCorruptionView.Subviews)
                {
                    if (sub == sender)
                    {
                        continue;
                    }
                    if (sub is NSControl)
                    {
                        ((NSControl)sub).Enabled = true;
                    }
                    if (sub is NSTextField)
                    {
                        ((NSTextField)sub).TextColor = NSColor.LabelColor;
                    }
                }

            }
            else
            {
                AutoEndCheckbox.State = NSCellStateValue.Off;
                AutoEnd(AutoEndCheckbox);
                foreach (NSView sub in ByteCorruptionView.Subviews)
                {
                    if (sub == sender || (sub == EndByteField && AutoEndCheckbox.State == NSCellStateValue.On))
                    {
                        continue;
                    }
                    if (sub is NSControl)
                    {
                        ((NSControl)sub).Enabled = false;
                    }
                    if (sub is NSTextField)
                    {
                        ((NSTextField)sub).TextColor = NSColor.DisabledControlText;
                    }
                }

            }

        }

        partial void AutoEnd(NSButton sender)
        {

            if (ToggleByteCorruptionButton.State == NSCellStateValue.On)
            {
                EndByteLabel.Enabled = AutoEndCheckbox.State == NSCellStateValue.Off;
                EndByteField.Enabled = AutoEndCheckbox.State == NSCellStateValue.Off;
                EndByteUpButton.Enabled = AutoEndCheckbox.State == NSCellStateValue.Off;
                EndByteDownButton.Enabled = AutoEndCheckbox.State == NSCellStateValue.Off;
                RangeUpButton.Enabled = AutoEndCheckbox.State == NSCellStateValue.Off;
                RangeDownButton.Enabled = AutoEndCheckbox.State == NSCellStateValue.Off;
            }

            EnforceAutoEnd();
        }

        void EnforceAutoEnd()
        {
            if (SelectedROM != null && AutoEndCheckbox.State == NSCellStateValue.On)
            {
                long StartByte;
                try
                {
                    StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
                }
                catch
                {
                    new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                    //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AutoEndCheckbox.State = NSCellStateValue.Off;
                    return;
                }

                long MaxByte = SelectedROM.FileLength - 1;
                if (StartByte > MaxByte)
                {
                    StartByte = MaxByte;
                    StartByteField.StringValue = MaxByte.ToString("X");
                }
                EndByteField.StringValue = MaxByte.ToString("X");
            }
            //EndByteField.StringValue = romDir.romDict[$"{directory}/{currentEntry}"].FileLength.ToString("X");
        }

        partial void StartByteDecrease(NSObject sender)
        {
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Increment;
            try
            {
                Increment = Convert.ToInt64(IncrementField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid increment.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid increment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StartByte = StartByte - Increment;
            if (StartByte < 0)
            {
                StartByte = 0;
            }
            StartByteField.StringValue = StartByte.ToString("X");
        }

        partial void StartByteIncrease(NSObject sender)
        {
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Increment;
            try
            {
                Increment = Convert.ToInt64(IncrementField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid increment.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid increment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StartByte = StartByte + Increment;
            if (StartByte > EndByte)
            {
                StartByte = EndByte;
            }
            StartByteField.StringValue = StartByte.ToString("X");
        }

        partial void EndByteDecrease(NSObject sender)
        {
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Increment;
            try
            {
                Increment = Convert.ToInt64(IncrementField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid increment.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid increment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EndByte = EndByte - Increment;
            if (EndByte < StartByte)
            {
                EndByte = StartByte;
            }
            EndByteField.StringValue = EndByte.ToString("X");
        }

        partial void EndByteIncrease(NSObject sender)
        {
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Increment;
            try
            {
                Increment = Convert.ToInt64(IncrementField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid increment.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid increment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EndByte = EndByte + Increment;
            EndByteField.StringValue = EndByte.ToString("X");
        }

        partial void ByteRangeDown(NSObject sender)
        {
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Increment;
            try
            {
                Increment = Convert.ToInt64(IncrementField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid increment.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid increment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Difference = EndByte - StartByte;
            StartByte = StartByte - Increment;
            if (StartByte < 0)
            {
                StartByte = 0;
                EndByte = Difference;
            }
            else
            {
                EndByte = EndByte - Increment;
            }
            StartByteField.StringValue = StartByte.ToString("X");
            EndByteField.StringValue = EndByte.ToString("X");
        }

        partial void ByteRangeUp(NSObject sender)
        {
            long StartByte;
            try
            {
                StartByte = Convert.ToInt64(StartByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid start byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid start byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long EndByte;
            try
            {
                EndByte = Convert.ToInt64(EndByteField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid end byte.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid end byte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long Increment;
            try
            {
                Increment = Convert.ToInt64(IncrementField.StringValue, 16);
            }
            catch
            {
                new NSAlert { MessageText = "Error", InformativeText = "Invalid increment.", AlertStyle = NSAlertStyle.Informational }.RunModal();
                //MessageBox.Show("Invalid increment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StartByte = StartByte + Increment;
            EndByte = EndByte + Increment;
            StartByteField.StringValue = StartByte.ToString("X");
            EndByteField.StringValue = EndByte.ToString("X");
        }

        #endregion

        #region About

        partial void GithubRikerz(NSObject sender)
        {
            NSUrl url = new NSUrl("https://github.com/Rikerz/VRC");
            NSWorkspace.SharedWorkspace.OpenUrl(url);
        }

        partial void GithubCheetodust(NSObject sender)
        {
            NSUrl url = new NSUrl("https://github.com/cheetodust/VRC-Mac");
            NSWorkspace.SharedWorkspace.OpenUrl(url);
        }

        #endregion
    }

}
