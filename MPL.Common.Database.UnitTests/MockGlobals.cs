using System;

namespace MPL.Common.Database
{
    internal enum CommandEnum : int
    {
        Undefined = 0
    }

    internal enum ParameterEnum : int
    {
        ResultParam,

        TableAColumnA,

        TableAColumnB,

        TableAColumnC,

        TableBColumnA,

        TableBColumnB,

        TableBColumnC,

        TableBColumnD,

        TableBColumnE,

        Undefined = 0
    }

    internal enum TableEnum : int
    {
        TableA,

        TableB,

        Undefined = 0
    }
}