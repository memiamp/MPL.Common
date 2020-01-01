using System;
using System.Data;

namespace MPL.Common.Database
{
    internal class MockDatabaseImplementation : DatabaseBase<CommandEnum, ParameterEnum, TableEnum>
    {
        internal MockDatabaseImplementation()
            : base(new MockDatabaseHelper())
        { }

        protected override string GetName(CommandEnum command)
        {
            return command.ToString();
        }

        protected override string GetName(ParameterEnum parameter)
        {
            return parameter.ToString();
        }

        protected override string GetName(TableEnum table)
        {
            return table.ToString();
        }

        protected override void GetParameterInformation(ParameterEnum parameter, out string name, out DbType dataType, out int size)
        {
            throw new NotImplementedException();
        }

        protected override ParameterEnum GetResultParameter()
        {
            return ParameterEnum.ResultParam;
        }
    }

}
