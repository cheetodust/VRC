// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Corruptor
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSButton AddRadioButton { get; set; }

        [Outlet]
        AppKit.NSTextField AddXtoByteField { get; set; }

        [Outlet]
        AppKit.NSButton AutoEndCheckbox { get; set; }

        [Outlet]
        AppKit.NSView ByteCorruptionView { get; set; }

        [Outlet]
        AppKit.NSTextField EmulatorField { get; set; }

        [Outlet]
        AppKit.NSTextField EmulatorLabel { get; set; }

        [Outlet]
        AppKit.NSButton EmulatorSelectButtonOutlet { get; set; }

        [Outlet]
        AppKit.NSButton EnableCpuJamCheckbox { get; set; }

        [Outlet]
        AppKit.NSButton EndByteDownButton { get; set; }

        [Outlet]
        AppKit.NSTextField EndByteField { get; set; }

        [Outlet]
        AppKit.NSTextField EndByteLabel { get; set; }

        [Outlet]
        AppKit.NSButton EndByteUpButton { get; set; }

        [Outlet]
        AppKit.NSTextField EveryNthByteField { get; set; }

        [Outlet]
        AppKit.NSTextField IncrementField { get; set; }

        [Outlet]
        AppKit.NSButton RangeDownButton { get; set; }

        [Outlet]
        AppKit.NSButton RangeUpButton { get; set; }

        [Outlet]
        AppKit.NSTextField ReplaceByteXwithYByteXField { get; set; }

        [Outlet]
        AppKit.NSTextField ReplaceByteXwithYByteYField { get; set; }

        [Outlet]
        AppKit.NSButton ReplaceRadioButton { get; set; }

        [Outlet]
        AppKit.NSTextField RomDirField { get; set; }

        [Outlet]
        AppKit.NSTableView RomDirTable { get; set; }

        [Outlet]
        AppKit.NSTableColumn RomDirTableColumn { get; set; }

        [Outlet]
        AppKit.NSTextField RomSaveField { get; set; }

        [Outlet]
        AppKit.NSButton ShiftRightRadioButton { get; set; }

        [Outlet]
        AppKit.NSTextField ShiftRightXBytesField { get; set; }

        [Outlet]
        AppKit.NSButton StartByteDownButton { get; set; }

        [Outlet]
        AppKit.NSTextField StartByteField { get; set; }

        [Outlet]
        AppKit.NSButton StartByteUpButton { get; set; }

        [Outlet]
        AppKit.NSButton ToggleByteCorruptionButton { get; set; }

        [Outlet]
        AppKit.NSButton ToggleEmulatorButton { get; set; }

        [Outlet]
        AppKit.NSButton ToggleOverwriteFile { get; set; }

        [Action ("AutoEnd:")]
        partial void AutoEnd (AppKit.NSButton sender);

        [Action ("ByteRangeDown:")]
        partial void ByteRangeDown (Foundation.NSObject sender);

        [Action ("ByteRangeUp:")]
        partial void ByteRangeUp (Foundation.NSObject sender);

        [Action ("CorruptionOption:")]
        partial void CorruptionOption (Foundation.NSObject sender);

        [Action ("CorruptionTypeRadio:")]
        partial void CorruptionTypeRadio (AppKit.NSButton sender);

        [Action ("EmulatorSelectButton:")]
        partial void EmulatorSelectButton (Foundation.NSObject sender);

        [Action ("EnableByteCorruption:")]
        partial void EnableByteCorruption (Foundation.NSObject sender);

        [Action ("EndByteDecrease:")]
        partial void EndByteDecrease (Foundation.NSObject sender);

        [Action ("EndByteIncrease:")]
        partial void EndByteIncrease (Foundation.NSObject sender);

        [Action ("GithubCheetodust:")]
        partial void GithubCheetodust (Foundation.NSObject sender);

        [Action ("GithubRikerz:")]
        partial void GithubRikerz (Foundation.NSObject sender);

        [Action ("LowByteDecrease:")]
        partial void LowByteDecrease (Foundation.NSObject sender);

        [Action ("LowByteIncrease:")]
        partial void LowByteIncrease (Foundation.NSObject sender);

        [Action ("RomDirButton:")]
        partial void RomDirButton (Foundation.NSObject sender);

        [Action ("RomDirTableAction:")]
        partial void RomDirTableAction (Foundation.NSObject sender);

        [Action ("RomSaveButton:")]
        partial void RomSaveButton (Foundation.NSObject sender);

        [Action ("RunButton:")]
        partial void RunButton (Foundation.NSObject sender);

        [Action ("StartByteDecrease:")]
        partial void StartByteDecrease (Foundation.NSObject sender);

        [Action ("StartByteIncrease:")]
        partial void StartByteIncrease (Foundation.NSObject sender);

        [Action ("ToggleByteCorruption:")]
        partial void ToggleByteCorruption (AppKit.NSButton sender);

        [Action ("ToggleEmulator:")]
        partial void ToggleEmulator (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (AddRadioButton != null) {
                AddRadioButton.Dispose ();
                AddRadioButton = null;
            }

            if (AddXtoByteField != null) {
                AddXtoByteField.Dispose ();
                AddXtoByteField = null;
            }

            if (AutoEndCheckbox != null) {
                AutoEndCheckbox.Dispose ();
                AutoEndCheckbox = null;
            }

            if (ByteCorruptionView != null) {
                ByteCorruptionView.Dispose ();
                ByteCorruptionView = null;
            }

            if (EmulatorField != null) {
                EmulatorField.Dispose ();
                EmulatorField = null;
            }

            if (EmulatorLabel != null) {
                EmulatorLabel.Dispose ();
                EmulatorLabel = null;
            }

            if (EmulatorSelectButtonOutlet != null) {
                EmulatorSelectButtonOutlet.Dispose ();
                EmulatorSelectButtonOutlet = null;
            }

            if (EnableCpuJamCheckbox != null) {
                EnableCpuJamCheckbox.Dispose ();
                EnableCpuJamCheckbox = null;
            }

            if (EndByteDownButton != null) {
                EndByteDownButton.Dispose ();
                EndByteDownButton = null;
            }

            if (EndByteField != null) {
                EndByteField.Dispose ();
                EndByteField = null;
            }

            if (EndByteLabel != null) {
                EndByteLabel.Dispose ();
                EndByteLabel = null;
            }

            if (EndByteUpButton != null) {
                EndByteUpButton.Dispose ();
                EndByteUpButton = null;
            }

            if (EveryNthByteField != null) {
                EveryNthByteField.Dispose ();
                EveryNthByteField = null;
            }

            if (IncrementField != null) {
                IncrementField.Dispose ();
                IncrementField = null;
            }

            if (RangeDownButton != null) {
                RangeDownButton.Dispose ();
                RangeDownButton = null;
            }

            if (RangeUpButton != null) {
                RangeUpButton.Dispose ();
                RangeUpButton = null;
            }

            if (ReplaceByteXwithYByteXField != null) {
                ReplaceByteXwithYByteXField.Dispose ();
                ReplaceByteXwithYByteXField = null;
            }

            if (ReplaceByteXwithYByteYField != null) {
                ReplaceByteXwithYByteYField.Dispose ();
                ReplaceByteXwithYByteYField = null;
            }

            if (ReplaceRadioButton != null) {
                ReplaceRadioButton.Dispose ();
                ReplaceRadioButton = null;
            }

            if (RomDirField != null) {
                RomDirField.Dispose ();
                RomDirField = null;
            }

            if (RomDirTable != null) {
                RomDirTable.Dispose ();
                RomDirTable = null;
            }

            if (RomDirTableColumn != null) {
                RomDirTableColumn.Dispose ();
                RomDirTableColumn = null;
            }

            if (RomSaveField != null) {
                RomSaveField.Dispose ();
                RomSaveField = null;
            }

            if (ShiftRightRadioButton != null) {
                ShiftRightRadioButton.Dispose ();
                ShiftRightRadioButton = null;
            }

            if (ShiftRightXBytesField != null) {
                ShiftRightXBytesField.Dispose ();
                ShiftRightXBytesField = null;
            }

            if (StartByteDownButton != null) {
                StartByteDownButton.Dispose ();
                StartByteDownButton = null;
            }

            if (StartByteField != null) {
                StartByteField.Dispose ();
                StartByteField = null;
            }

            if (StartByteUpButton != null) {
                StartByteUpButton.Dispose ();
                StartByteUpButton = null;
            }

            if (ToggleByteCorruptionButton != null) {
                ToggleByteCorruptionButton.Dispose ();
                ToggleByteCorruptionButton = null;
            }

            if (ToggleEmulatorButton != null) {
                ToggleEmulatorButton.Dispose ();
                ToggleEmulatorButton = null;
            }

            if (ToggleOverwriteFile != null) {
                ToggleOverwriteFile.Dispose ();
                ToggleOverwriteFile = null;
            }
        }
    }
}
