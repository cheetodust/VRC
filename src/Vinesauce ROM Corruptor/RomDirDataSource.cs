using System;
using AppKit;
using System.Collections.Generic;

namespace Corruptor
{
    public class RomDirDataSource : NSTableViewDataSource

    {
        public RomDirDataSource()
        { }

        public List<string> DirList = new List<string>();
        public Dictionary<string, RomId> romDict = new Dictionary<string, RomId>();


        public override nint GetRowCount(NSTableView tableView)
        {
            return DirList.Count;
        }
    }
}
