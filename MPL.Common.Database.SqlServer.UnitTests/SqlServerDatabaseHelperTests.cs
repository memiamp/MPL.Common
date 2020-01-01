using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MPL.Common.Database.SqlServer
{
    [TestClass]
    public class SqlServerDatabaseHelperTests : DatabaseHelperTestsBase<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>
    {
        #region Methods
        #region _Protected_
        protected override SqlDbType GetDbTypeDateTime()
        {
            return SqlDbType.DateTime;
        }

        protected override SqlDbType[] GetDbTypeEnums()
        {
            return new[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.Bit, SqlDbType.TinyInt, SqlDbType.Money, SqlDbType.Date, SqlDbType.DateTime, SqlDbType.DateTime2, SqlDbType.DateTimeOffset, SqlDbType.Decimal, SqlDbType.Real, SqlDbType.UniqueIdentifier, SqlDbType.SmallInt, SqlDbType.Int, SqlDbType.BigInt, SqlDbType.Variant, SqlDbType.TinyInt, SqlDbType.Float, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Time, SqlDbType.SmallInt, SqlDbType.Int, SqlDbType.BigInt, SqlDbType.Decimal, SqlDbType.Xml };
        }

        protected override SqlDbType GetDbTypeFloat()
        {
            return SqlDbType.Float;
        }

        protected override SqlDbType GetDbTypeInt()
        {
            return SqlDbType.Int;
        }

        protected override SqlDbType GetDbTypeString()
        {
            return SqlDbType.NVarChar;
        }

        protected override IDatabaseHelper GetHelperInstance()
        {
            return new SqlServerDatabaseHelper();
        }

        protected override IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter> GetHelperInstanceGeneric()
        {
            return new SqlServerDatabaseHelper();
        }

        protected override void TestParameter(SqlParameter parameter, string parameterName, object parameterValue, SqlDbType parameterDataType, ParameterDirection parameterDirection = ParameterDirection.Input, int parameterSize = 0)
        {
            Assert.AreEqual(parameterName, parameter.ParameterName);
            Assert.AreEqual(parameterValue, parameter.Value);
            Assert.AreEqual(parameterDataType, parameter.SqlDbType);
            Assert.AreEqual(parameterDirection, parameter.Direction);
            if (parameterSize > 0)
                Assert.AreEqual(parameterSize, parameter.Size);
        }
        
        #endregion
        #endregion
    }
}