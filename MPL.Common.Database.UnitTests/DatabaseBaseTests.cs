using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;

namespace MPL.Common.Database
{
    [TestClass]
    public class DatabaseBaseTests
    {
        #region Methods
        #region _Private_
        private DataSet BuildDummyDataSet(int tableARows = 4, int tableBRows = 6)
        {
            DataSet returnValue;

            returnValue = new DataSet();

            returnValue.Tables.Add(TableEnum.TableA.ToString());
            returnValue.Tables[0].Columns.Add(ParameterEnum.TableAColumnA.ToString());
            returnValue.Tables[0].Columns.Add(ParameterEnum.TableAColumnB.ToString());
            returnValue.Tables[0].Columns.Add(ParameterEnum.TableAColumnC.ToString());
            for (int i = 0; i < tableARows; i++)
                returnValue.Tables[0].Rows.Add(TestDataTestHelper.GetStrings(arraySize: 3));

            returnValue.Tables.Add(TableEnum.TableB.ToString());
            returnValue.Tables[1].Columns.Add(ParameterEnum.TableBColumnA.ToString());
            returnValue.Tables[1].Columns.Add(ParameterEnum.TableBColumnB.ToString());
            returnValue.Tables[1].Columns.Add(ParameterEnum.TableBColumnC.ToString());
            returnValue.Tables[1].Columns.Add(ParameterEnum.TableBColumnD.ToString());
            returnValue.Tables[1].Columns.Add(ParameterEnum.TableBColumnE.ToString());
            for (int i = 0; i < tableBRows; i++)
                returnValue.Tables[1].Rows.Add(TestDataTestHelper.GetStrings(arraySize: 5));

            return returnValue;
        }

        private DataTable BuildDummyDataTable<T>(TableEnum tableName, ParameterEnum columnName, T[] columnValues)
        {
            DataTable returnValue;

            returnValue = new DataTable(tableName.ToString());
            returnValue.Columns.Add(columnName.ToString());
            returnValue.Columns[0].DataType = typeof(T);
            if (columnValues != null)
                foreach (object Item in columnValues)
                    returnValue.Rows.Add(Item);

            return returnValue;
        }

        private IDatabaseBase<ParameterEnum, TableEnum> GetDatabaseInstance()
        {
            return new MockDatabaseImplementation();
        }

        #endregion
        #region _Public_
        [TestMethod]
        public void IDatbaseInterface_GetColumnBoolValues_AreColumnValuesRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataTable sourceData;
            bool[] values;

            database = GetDatabaseInstance();
            values = new bool[5];
            for (int i = 0; i < values.Length; i++)
                values[i] = TestDataTestHelper.GetInt() % 2 == 1;
            sourceData = BuildDummyDataTable(TableEnum.TableA, ParameterEnum.TableAColumnA, values);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.IsTrue(database.GetColumnValue(sourceData.Rows[i], ParameterEnum.TableAColumnA, out bool value));
                Assert.AreEqual(value, values[i]);
            }
        }

        [TestMethod]
        public void IDatbaseInterface_GetColumnDateTimeValues_AreColumnValuesRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataTable sourceData;
            DateTime[] values;

            database = GetDatabaseInstance();
            values = new DateTime[5];
            for (int i = 0; i < values.Length; i++)
                values[i] = TestDataTestHelper.GetDateTime();
            sourceData = BuildDummyDataTable(TableEnum.TableA, ParameterEnum.TableAColumnA, values);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.IsTrue(database.GetColumnValue(sourceData.Rows[i], ParameterEnum.TableAColumnA, out DateTime value));
                Assert.AreEqual(value, values[i]);
            }
        }

        [TestMethod]
        public void IDatbaseInterface_GetColumnFloatValues_AreColumnValuesRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataTable sourceData;
            float[] values;

            database = GetDatabaseInstance();
            values = new float[5];
            for (int i = 0; i < values.Length; i++)
                values[i] = TestDataTestHelper.GetFloat();
            sourceData = BuildDummyDataTable(TableEnum.TableA, ParameterEnum.TableAColumnA, values);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.IsTrue(database.GetColumnValue(sourceData.Rows[i], ParameterEnum.TableAColumnA, out float value));
                Assert.AreEqual(value, values[i], 0.0001);
            }
        }

        [TestMethod]
        public void IDatbaseInterface_GetColumnIntValues_AreColumnValuesRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataTable sourceData;
            int[] values;

            database = GetDatabaseInstance();
            values = new int[5];
            for (int i = 0; i < values.Length; i++)
                values[i] = TestDataTestHelper.GetInt();
            sourceData = BuildDummyDataTable(TableEnum.TableA, ParameterEnum.TableAColumnA, values);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.IsTrue(database.GetColumnValue(sourceData.Rows[i], ParameterEnum.TableAColumnA, out int value));
                Assert.AreEqual(value, values[i]);
            }
        }

        [TestMethod]
        public void IDatbaseInterface_GetColumnObjects_AreColumnValuesRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataTable sourceData;
            object[] values;

            database = GetDatabaseInstance();
            values = new object[5];
            for (int i = 0; i < values.Length; i++)
                values[i] = new object();
            sourceData = BuildDummyDataTable(TableEnum.TableA, ParameterEnum.TableAColumnA, values);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.IsTrue(database.GetColumnValue(sourceData.Rows[i], ParameterEnum.TableAColumnA, out object value));
                Assert.AreEqual(value, values[i]);
            }
        }

        [TestMethod]
        public void IDatbaseInterface_GetColumnStringValues_AreColumnValuesRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataTable sourceData;
            string[] values;

            database = GetDatabaseInstance();
            values = TestDataTestHelper.GetStrings(arraySize: 3);
            sourceData = BuildDummyDataTable(TableEnum.TableA, ParameterEnum.TableAColumnA, values);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.IsTrue(database.GetColumnValue(sourceData.Rows[i], ParameterEnum.TableAColumnA, out string value));
                Assert.AreEqual(value, values[i]);
            }
        }

        [TestMethod]
        public void IDatbaseInterface_GetTable_IsTableRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            DataSet sourceData;

            database = GetDatabaseInstance();
            sourceData = BuildDummyDataSet(5, 7);
            
            Assert.IsTrue(database.GetTable(sourceData, TableEnum.TableA, out DataTable tableA));
            Assert.AreEqual(5, tableA.Rows.Count);

            Assert.IsTrue(database.GetTable(sourceData, TableEnum.TableB, out DataTable tableB));
            Assert.AreEqual(7, tableB.Rows.Count);
        }

        [TestMethod]
        public void IDatbaseInterface_SetConnectionParameters_AreStoredAndRetrieved()
        {
            IDatabaseBase<ParameterEnum, TableEnum> database;
            string connectionParameter;

            database = GetDatabaseInstance();
            connectionParameter = TestDataTestHelper.GetString();
            database.ConnectionParameters = connectionParameter;
            Assert.AreEqual(connectionParameter, database.ConnectionParameters);

            connectionParameter = TestDataTestHelper.GetString();
            database.ConnectionParameters = connectionParameter;
            Assert.AreEqual(connectionParameter, database.ConnectionParameters);
        }

        #endregion
        #endregion
    }
}