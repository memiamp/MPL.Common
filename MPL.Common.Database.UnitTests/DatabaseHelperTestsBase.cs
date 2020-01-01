using System;
using System.Data;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;

namespace MPL.Common.Database
{
    [TestClass]
    public abstract class DatabaseHelperTestsBase<TDbTypeEnum, TCommand, TConnection, TDataAdapter, TParameter>
        where TCommand : IDbCommand
        where TConnection : IDbConnection
        where TDataAdapter : IDbDataAdapter
        where TDbTypeEnum : struct, IConvertible
        where TParameter : IDbDataParameter
    {
        #region Declarations
        #region _Members_
        private readonly DbType[] StandardDbTypes = new[] { DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.Binary, DbType.Boolean, DbType.Byte, DbType.Currency, DbType.Date, DbType.DateTime, DbType.DateTime2, DbType.DateTimeOffset, DbType.Decimal, DbType.Double, DbType.Guid, DbType.Int16, DbType.Int32, DbType.Int64, DbType.Object, DbType.SByte, DbType.Single, DbType.String, DbType.StringFixedLength, DbType.Time, DbType.UInt16, DbType.UInt32, DbType.UInt64, DbType.VarNumeric, DbType.Xml };

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private IDbCommand CreateCommand(string commandName, CommandType commandType)
        {
            return GetHelperInstance().CreateCommand(commandName, commandType);
        }

        private void ExecuteCommandTest(CommandType commandType)
        {
            IDbCommand command;
            string commandName;

            commandName = TestDataTestHelper.GetString();
            command = CreateCommand(commandName, commandType);
            TestCommand(command, commandName, commandType);
        }

        private void ExecuteParameterTest(DbType specifiedDbType, TDbTypeEnum expectedDbType, object value, ParameterDirection parameterDirection = ParameterDirection.Input, int parameterSize = 0)
        {
            IDbDataParameter parameter;
            string parameterName;

            parameterName = TestDataTestHelper.GetString();
            parameter = GetHelperInstance().CreateParameter(parameterName, specifiedDbType, parameterSize, parameterDirection, value);
            TestParameter(parameter, parameterName, value, expectedDbType, parameterDirection, parameterSize);
        }

        protected abstract IDatabaseHelper GetHelperInstance();

        protected abstract IDatabaseHelper<TDbTypeEnum, TCommand, TConnection, TDataAdapter, TParameter> GetHelperInstanceGeneric();

        private void TestCommand(IDbCommand command, string commandName, CommandType commandType)
        {
            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(TCommand));
            Assert.AreEqual(commandName, command.CommandText);
            Assert.AreEqual(commandType, command.CommandType);
        }

        private void TestConnention(IDbConnection connection, string connectionString = null)
        {
            Assert.IsNotNull(connection);
            Assert.IsInstanceOfType(connection, typeof(TConnection));
            Assert.AreEqual(connectionString, connection.ConnectionString, true);
        }

        private void TestParameter(IDbDataParameter parameter, string parameterName, object parameterValue, TDbTypeEnum parameterDataType, ParameterDirection parameterDirection = ParameterDirection.Input, int parameterSize = 0)
        {
            TParameter tParameter;

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOfType(parameter, typeof(TParameter));
            tParameter = (TParameter)parameter;
            TestParameter(tParameter, parameterName, parameterValue, parameterDataType, parameterDirection, parameterSize);
        }

        #endregion
        #region _Protected_
        protected abstract TDbTypeEnum GetDbTypeDateTime();

        protected abstract TDbTypeEnum GetDbTypeFloat();

        protected abstract TDbTypeEnum GetDbTypeInt();

        protected abstract TDbTypeEnum GetDbTypeString();

        protected abstract TDbTypeEnum[] GetDbTypeEnums();

        protected abstract void TestParameter(TParameter parameter, string parameterName, object parameterValue, TDbTypeEnum parameterDataType, ParameterDirection parameterDirection = ParameterDirection.Input, int parameterSize = 0);

        #endregion
        #region _Public_
        [TestMethod]
        public void CreateCommand_DefaultParameters_CommandCreatedCorrectly()
        {
            IDbCommand command;
            string commandName;

            commandName = TestDataTestHelper.GetString();
            command = CreateCommand(commandName, CommandType.StoredProcedure);
            TestCommand(command, commandName, CommandType.StoredProcedure);
        }

        [TestMethod]
        public void CreateCommand_SpecifiedStoredProcedure_CommandCreatedCorrectly()
        {
            ExecuteCommandTest(CommandType.StoredProcedure);
        }

        [TestMethod]
        public void CreateCommand_SpecifiedText_CommandCreatedCorrectly()
        {
            ExecuteCommandTest(CommandType.Text);
        }

        [TestMethod]
        public void CreateConnection_WithConnectionString_ConnectionCreatedCorrectly()
        {
            IDbConnection connection;
            string connectionString;

            connectionString = $"Server={TestDataTestHelper.GetString()}";
            connection = GetHelperInstance().CreateConnection(connectionString);
            TestConnention(connection, connectionString);
        }

        [TestMethod]
        public void CreateParameter_UsingDbTypeDateTime_ParameterCreatedCorrectly()
        {
            ExecuteParameterTest(DbType.DateTime, GetDbTypeDateTime(), TestDataTestHelper.GetDateTime());
        }

        [TestMethod]
        public void CreateParameter_UsingDbTypeFloat_ParameterCreatedCorrectly()
        {
            ExecuteParameterTest(DbType.Single, GetDbTypeFloat(), TestDataTestHelper.GetFloat());
        }

        [TestMethod]
        public void CreateParameter_UsingDbTypeInt_ParameterCreatedCorrectly()
        {
            ExecuteParameterTest(DbType.Int32, GetDbTypeInt(), TestDataTestHelper.GetInt());
        }

        [TestMethod]
        public void CreateParameter_UsingDbTypeString_ParameterCreatedCorrectly()
        {
            ExecuteParameterTest(DbType.String, GetDbTypeString(), TestDataTestHelper.GetString(), ParameterDirection.Input, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDataAdapter_InvalidIDbCommandType_ExceptionThrown()
        {
            GetHelperInstance().CreateDataAdapter(new System.Data.OleDb.OleDbCommand(TestDataTestHelper.GetString()));
        }

        [TestMethod]
        public void GetDataAdapter_WithTableMappings_TableMappingsCreated()
        {
            IDbDataAdapter adapter;
            IDbCommand command;
            string[] tableNames;

            command = CreateCommand(TestDataTestHelper.GetString(), CommandType.StoredProcedure);
            tableNames = TestDataTestHelper.GetStrings(arraySize: 3);
            adapter = GetHelperInstance().CreateDataAdapter(command, tableNames);

            Assert.IsNotNull(adapter);
            Assert.IsNotNull(adapter.TableMappings);
            Assert.AreEqual(tableNames.Length, adapter.TableMappings.Count);
            for (int i = 0; i < tableNames.Length; i++)
            {
                DataTableMapping tableMapping;

                Assert.IsNotNull(adapter.TableMappings[i]);
                Assert.IsInstanceOfType(adapter.TableMappings[i], typeof(DataTableMapping));
                tableMapping = (DataTableMapping)adapter.TableMappings[i];

                Assert.AreEqual(tableNames[i], tableMapping.DataSetTable);
                if (i > 0)
                    Assert.AreEqual($"Table{i}", tableMapping.SourceTable);
                else
                    // Table0 is named Table
                    Assert.AreEqual("Table", tableMapping.SourceTable);
            }
        }

        [TestMethod]
        public void GetDataAdapter_ValidIDbCommand_DataAdapterCreatedCorrectly()
        {
            IDbDataAdapter adapter;
            IDbCommand command;

            command = CreateCommand(TestDataTestHelper.GetString(), CommandType.StoredProcedure);
            adapter = GetHelperInstance().CreateDataAdapter(command);

            Assert.IsNotNull(adapter);
            Assert.IsNotNull(adapter.SelectCommand);
            Assert.AreEqual(command, adapter.SelectCommand);
        }

        [TestMethod]
        public void MapDbType_AllValues_AreConvertedCorrectly()
        {
            TDbTypeEnum[] dbTypeEnums = GetDbTypeEnums();

            Assert.AreEqual(dbTypeEnums.Length, StandardDbTypes.Length);
            for (int i = 0; i < StandardDbTypes.Length; i++)
                Assert.AreEqual(dbTypeEnums[i], GetHelperInstanceGeneric().MapDbType(StandardDbTypes[i]));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MapDbType_UnknownValue_ThrowsException()
        {
            GetHelperInstanceGeneric().MapDbType((DbType)(-1));
        }

        #endregion
        #endregion
    }
}