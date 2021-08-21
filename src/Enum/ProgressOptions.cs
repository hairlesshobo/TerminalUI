using System;

namespace TerminalUI
{
    [Flags]
    public enum ProgressOptions
    {
        None = 0,
        AvgTransferRate = 1,
        InstantTransferRate = 2,
        CurrentSize = 4,
        TotalSize = 8
    }
}