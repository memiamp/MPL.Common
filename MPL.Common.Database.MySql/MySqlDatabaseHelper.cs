using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MPL.Common.Database.MySql
{
    /// <summary>
    /// A class that provides helper functions for MySQL databases.
    /// </summary>
    public sealed class MySqlDatabaseHelper : DatabaseHelperBase<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>, IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>
    {
        #region Consturctors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="useParameterPrefix">A bool indicating whether to use parameter name prefixes when creating parameters.</param>
        public MySqlDatabaseHelper(bool useParameterPrefix = true)
        {
            UseParameterPrefix = useParameterPrefix;
            _DatabaseHelperInterface = this;
        }

        #endregion

        #region Declarations
        #region _Members_
        private readonly IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter> _DatabaseHelperInterface;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private string GetParameterName(string name, ParameterDirection direction)
        {
            string returnValue;

            if (UseParameterPrefix)
            {
                string prefix = null;

                switch (direction)
                {
                    case ParameterDirection.Input:
                        prefix = "i";
                        break;

                    case ParameterDirection.InputOutput:
                        prefix = "io";
                        break;

                    case ParameterDirection.Output:
                        prefix = "o";
                        break;

                    case ParameterDirection.ReturnValue:
                        prefix = "rv";
                        break;
                }

                returnValue = $"{prefix}_{name}";
            }
            else
                returnValue = name;

            return returnValue;
        }

        #endregion
        #region _Protected_
        protected override IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter> GetHelperInterface()
        {
            return this;
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets an indication of whether to use a parameter prefix when creating parameters.
        /// </summary>
        public bool UseParameterPrefix { get; set; }

        #endregion

        #region Interfaces
        #region _IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>_
        MySqlCommand IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>.GetCommand(string name, CommandType type)
        {
            return new MySqlCommand(name) { CommandType = type };
        }

        MySqlConnection IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>.GetConnection(string connectionParameters)
        {
            return new MySqlConnection(connectionParameters);
        }

        MySqlDataAdapter IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>.GetDataAdapter(MySqlCommand command, string[] tableNames)
        {
            MySqlDataAdapter ReturnValue = null;

            ReturnValue = new MySqlDataAdapter(command);
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

        MySqlParameter IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>.GetParameter(string name, DbType dataType, int size, ParameterDirection direction, object value)
        {
            return _DatabaseHelperInterface.GetParameter(name, _DatabaseHelperInterface.MapDbType(dataType), size, direction, value);
        }
        MySqlParameter IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>.GetParameter(string name, MySqlDbType dataType, int size, ParameterDirection direction, object value)
        {
            MySqlParameter ReturnValue;

            ReturnValue = new MySqlParameter(GetParameterName(name, direction), dataType)
            {
                Direction = direction,
                Value = value
            };
            ReturnValue.Size = size;

            return ReturnValue;
        }

        MySqlDbType IDatabaseHelper<MySqlDbType, MySqlCommand, MySqlConnection, MySqlDataAdapter, MySqlParameter>.MapDbType(DbType dbType)
        {
            MySqlDbType returnValue = 0;

            switch (dbType)
            {
                case DbType.AnsiString:
                    returnValue = MySqlDbType.VarChar;
                    break;

                case DbType.AnsiStringFixedLength:
                    returnValue = MySqlDbType.String;
                    break;

                case DbType.Binary:
                    returnValue = MySqlDbType.VarBinary;
                    break;

                case DbType.Boolean:
                    returnValue = MySqlDbType.Bit;
                    break;

                case DbType.Byte:
                    returnValue = MySqlDbType.Byte;
                    break;

                case DbType.Currency:
                    returnValue = MySqlDbType.Double;
                    break;

                case DbType.Date:
                    returnValue = MySqlDbType.Date;
                    break;

                case DbType.DateTime:
                    returnValue = MySqlDbType.DateTime;
                    break;

                case DbType.DateTime2:
                    returnValue = MySqlDbType.DateTime;
                    break;

                case DbType.DateTimeOffset:
                    returnValue = MySqlDbType.DateTime;
                    break;

                case DbType.Decimal:
                    returnValue = MySqlDbType.Decimal;
                    break;

                case DbType.Double:
                    returnValue = MySqlDbType.Double;
                    break;

                case DbType.Guid:
                    returnValue = MySqlDbType.Guid;
                    break;

                case DbType.Int16:
                    returnValue = MySqlDbType.Int16;
                    break;

                case DbType.Int32:
                    returnValue = MySqlDbType.Int32;
                    break;

                case DbType.Int64:
                    returnValue = MySqlDbType.Int64;
                    break;

                case DbType.Object:
                    returnValue = MySqlDbType.Blob;
                    break;

                case DbType.SByte:
                    returnValue = MySqlDbType.Byte;
                    break;

                case DbType.Single:
                    returnValue = MySqlDbType.Float;
                    break;

                case DbType.String:
                    returnValue = MySqlDbType.String;
                    break;

                case DbType.StringFixedLength:
                    returnValue = MySqlDbType.String;
                    break;

                case DbType.Time:
                    returnValue = MySqlDbType.Time;
                    break;

                case DbType.UInt16:
                    returnValue = MySqlDbType.UInt16;
                    break;

                case DbType.UInt32:
                    returnValue = MySqlDbType.UInt32;
                    break;

                case DbType.UInt64:
                    returnValue = MySqlDbType.UInt64;
                    break;

                case DbType.VarNumeric:
                    returnValue = MySqlDbType.Decimal;
                    break;

                case DbType.Xml:
                    returnValue = MySqlDbType.LongText;
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