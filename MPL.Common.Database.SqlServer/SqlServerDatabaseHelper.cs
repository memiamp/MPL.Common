using System;
using System.Data;
using System.Data.SqlClient;

namespace MPL.Common.Database.SqlServer
{
    /// <summary>
    /// A class that provides helper functions for SQL Server databases.
    /// </summary>
    public class SqlServerDatabaseHelper : DatabaseHelperBase<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>, IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>
    {
        #region Consturctors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public SqlServerDatabaseHelper()
        {
            _DatabaseHelperInterface = this;
        }

        #endregion 

        #region Declarations
        #region _Members_
        private readonly IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter> _DatabaseHelperInterface;

        #endregion
        #endregion


        #region Methods
        #region _Protected_
        protected override IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter> GetHelperInterface()
        {
            return this;
        }

        #endregion
        #endregion

        #region Interfaces
        #region _IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>_
        SqlCommand IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>.GetCommand(string name, CommandType type)
        {
            return new SqlCommand(name) { CommandType = type };
        }

        SqlConnection IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>.GetConnection(string connectionParameters)
        {
            return new SqlConnection(connectionParameters);
        }

        SqlParameter IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>.GetParameter(string name, DbType dataType, int size, ParameterDirection direction, object value)
        {
            return _DatabaseHelperInterface.GetParameter(name, _DatabaseHelperInterface.MapDbType(dataType), size, direction, value);
        }
        SqlParameter IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>.GetParameter(string name, SqlDbType dataType, int size, ParameterDirection direction, object value)
        {
            SqlParameter ReturnValue;

            ReturnValue = new SqlParameter(name, dataType, size)
            {
                Direction = direction,
                Value = value
            };

            return ReturnValue;
        }

        SqlDataAdapter IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>.GetDataAdapter(SqlCommand command, string[] tableNames)
        {
            SqlDataAdapter ReturnValue = null;

            ReturnValue = new SqlDataAdapter(command);
            if (tableNames != null)
            {
                for (int i = 0; i < tableNames.Length; i++)
                {
                    string SourceTable;
                    string TargetTable;

                    SourceTable = "Table";
                    if (i > 0)
                        SourceTable += i.ToString();
                    TargetTable = tableNames[i];
                    ReturnValue.TableMappings.Add(SourceTable, TargetTable);
                }
            }

            return ReturnValue;
        }

        SqlDbType IDatabaseHelper<SqlDbType, SqlCommand, SqlConnection, SqlDataAdapter, SqlParameter>.MapDbType(DbType dbType)
        {
            SqlDbType returnValue = 0;

            switch (dbType)
            {
                case DbType.AnsiString:
                    returnValue = SqlDbType.VarChar;
                    break;

                case DbType.AnsiStringFixedLength:
                    returnValue = SqlDbType.VarChar;
                    break;

                case DbType.Binary:
                    returnValue = SqlDbType.VarBinary;
                    break;

                case DbType.Boolean:
                    returnValue = SqlDbType.Bit;
                    break;

                case DbType.Byte:
                    returnValue = SqlDbType.TinyInt;
                    break;

                case DbType.Currency:
                    returnValue = SqlDbType.Money;
                    break;

                case DbType.Date:
                    returnValue = SqlDbType.Date;
                    break;

                case DbType.DateTime:
                    returnValue = SqlDbType.DateTime;
                    break;

                case DbType.DateTime2:
                    returnValue = SqlDbType.DateTime2;
                    break;

                case DbType.DateTimeOffset:
                    returnValue = SqlDbType.DateTimeOffset;
                    break;

                case DbType.Decimal:
                    returnValue = SqlDbType.Decimal;
                    break;

                case DbType.Double:
                    returnValue = SqlDbType.Real;
                    break;

                case DbType.Guid:
                    returnValue = SqlDbType.UniqueIdentifier;
                    break;

                case DbType.Int16:
                    returnValue = SqlDbType.SmallInt;
                    break;

                case DbType.Int32:
                    returnValue = SqlDbType.Int;
                    break;

                case DbType.Int64:
                    returnValue = SqlDbType.BigInt;
                    break;

                case DbType.Object:
                    returnValue = SqlDbType.Variant;
                    break;

                case DbType.SByte:
                    returnValue = SqlDbType.TinyInt;
                    break;

                case DbType.Single:
                    returnValue = SqlDbType.Float;
                    break;

                case DbType.String:
                    returnValue = SqlDbType.NVarChar;
                    break;

                case DbType.StringFixedLength:
                    returnValue = SqlDbType.NVarChar;
                    break;

                case DbType.Time:
                    returnValue = SqlDbType.Time;
                    break;

                case DbType.UInt16:
                    returnValue = SqlDbType.SmallInt;
                    break;

                case DbType.UInt32:
                    returnValue = SqlDbType.Int;
                    break;

                case DbType.UInt64:
                    returnValue = SqlDbType.BigInt;
                    break;

                case DbType.VarNumeric:
                    returnValue = SqlDbType.Decimal;
                    break;

                case DbType.Xml:
                    returnValue = SqlDbType.Xml;
                    break;

                default:
                    throw new ArgumentException("The specified data type was not understood", nameof(dbType));
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}