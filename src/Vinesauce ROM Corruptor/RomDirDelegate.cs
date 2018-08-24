using System; using System.Collections.Generic; using AppKit;

namespace Corruptor
 {
    public class RomDirDelegate : NSTableViewDelegate 
    {
        private const string CellIdentifier = "RomDirCell";
        private RomDirDataSource DataSource;

        public RomDirDelegate(RomDirDataSource datasource)
        {
            this.DataSource = datasource;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            // This pattern allows you reuse existing views when they are no-longer in use.
            // If the returned view is null, you instance up a new view.
            // If a non-null view is returned, you modify it enough to reflect the new data.
            NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            // Set up view based on the column and row
            view.StringValue = DataSource.DirList[(int)row];             return view;
        }
    }
} 