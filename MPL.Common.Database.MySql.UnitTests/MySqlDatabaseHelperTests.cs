using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Database;
using MPL.Common.Database.MySql;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MPL.Common.Database.MySql
{
    [TestClass]
    public class MySqlDatabaseHelperTests : DatabaseHelperTestsBase<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>
    {
        #region Methods
        #region _Protected_
        protected override MySqlDbType GetDbTypeDateTime()
        {
            return MySqlDbType.DateTime;
        }

        protected override MySqlDbType[] GetDbTypeEnums()
        {
            return new[] { MySqlDbType.VarChar, MySqlDbType.String, MySqlDbType.VarBinary, MySqlDbType.Bit, MySqlDbType.Byte, MySqlDbType.Double, MySqlDbType.Date, MySqlDbType.DateTime, MySqlDbType.DateTime, MySqlDbType.DateTime, MySqlDbType.Decimal, MySqlDbType.Double, MySqlDbType.Guid, MySqlDbType.Int16, MySqlDbType.Int32, MySqlDbType.Int64, MySqlDbType.Blob, MySqlDbType.Byte, MySqlDbType.Float, MySqlDbType.String, MySqlDbType.String, MySqlDbType.Time, MySqlDbType.UInt16, MySqlDbType.UInt32, MySqlDbType.UInt64, MySqlDbType.Decimal, MySqlDbType.LongText };
        }

        protected override MySqlDbType GetDbTypeFloat()
        {
            return MySqlDbType.Float;
        }

        protected override MySqlDbType GetDbTypeInt()
        {
            return MySqlDbType.Int32;
        }

        protected override MySqlDbType GetDbTypeString()
        {
            return MySqlDbType.String;
        }

        protected override IDatabaseHelper GetHelperInstance()
        {
            return new MySqlDatabaseHelper();
        }

        protected override IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter> GetHelperInstanceGeneric()
        {
            return new MySqlDatabaseHelper();
        }

        protected override void TestParameter(MySqlParameter parameter, string parameterName, object parameterValue, MySqlDbType parameterDataType, ParameterDirection parameterDirection = ParameterDirection.Input, int parameterSize = 0)
        {
            Assert.AreEqual(parameterName, parameter.ParameterName);
            Assert.AreEqual(parameterValue, parameter.Value);
            Assert.AreEqual(parameterDataType, parameter.MySqlDbType);
            Assert.AreEqual(parameterDirection, parameter.Direction);
            if (parameterSize > 0)
                Assert.AreEqual(parameterSize, parameter.Size);
        }

        #endregion
        #endregion
    }
}