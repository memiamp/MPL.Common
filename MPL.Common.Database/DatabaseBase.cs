using MPL.Common.Logging;
using MPL.Common.Reflection;
using System;
using System.Data;
using System.Reflection;

namespace MPL.Common.Database
{
    /// <summary>
    /// A class that defines the base implementation of database access functionality.
    /// </summary>
    /// <typeparam name="TCommandEnum">An enumeration that defines command names.</typeparam>
    /// <typeparam name="TParameterEnum">An enumeration that defines column or parameter names.</typeparam>
    /// <typeparam name="TTableEnum">An enumeration that defines table names.</typeparam>
    public abstract class DatabaseBase<TCommandEnum, TParameterEnum, TTableEnum> : IDatabaseBase<TParameterEnum, TTableEnum>
        where TCommandEnum : struct, IConvertible
        where TParameterEnum : struct, IConvertible
        where TTableEnum : struct, IConvertible
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        protected DatabaseBase(IDatabaseHelper databaseHelper)
        {
            _DatabaseHelper = databaseHelper;

            _DatabaseBaseInterface = this;
            SupportsResultParam = true;
        }

        #endregion

        #region Declarations
        #region _Members_
        private string _ConnectionString;
        private readonly IDatabaseBase<TParameterEnum, TTableEnum> _DatabaseBaseInterface;
        private readonly IDatabaseHelper _DatabaseHelper;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private IDataParameter AddParameterResult(IDbCommand command)
        {
            return AddParameter(command, GetResultParameter(), ParameterDirection.Output);
        }

        private bool GetColumnValue<T>(DataRow row, TParameterEnum column, out T value)
        {
            bool ReturnValue = false;

            // Defaults
            value = default(T);

            if (_DatabaseBaseInterface.GetColumnValue(row, column, out object Value))
                ReturnValue = GetObjectAsValue(Value, out value);

            return ReturnValue;
        }

        #endregion
        #region _Protected_
        /// <summary>
        /// Creates a parameter with the specified information and adds it to the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to add the parameter to.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to create.</param>
        /// <param name="direction">A ParameterDirection that indicates the direction of the parameter.</param>
        /// <param name="value">An object containing the value of the parameter.</param>
        /// <returns>An IDataParameter that was created.</returns>
        protected IDataParameter AddParameter(IDbCommand command, TParameterEnum parameter, ParameterDirection direction = ParameterDirection.Input, object value = null)
        {
            IDataParameter returnValue;

            returnValue = CreateParameter(parameter, direction, value);
            command.Parameters.Add(returnValue);

            return returnValue;
        }
        /// <summary>
        /// Creates a parameter with the specified information and adds it to the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to add the parameter to.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to create.</param>
        /// <param name="value">An object containing the value of the parameter.</param>
        /// <returns>An IDataParameter that was created.</returns>
        protected IDataParameter AddParameter(IDbCommand command, TParameterEnum parameter, object value)
        {
            return AddParameter(command, parameter, ParameterDirection.Input, value);
        }

        /// <summary>
        /// Builds the command with the specified name.
        /// </summary>
        /// <param name="name">A TCommandEnum that is the name of the command to build.</param>
        /// <returns>An IDbCommand that was built.</returns>
        protected IDbCommand BuildCommand(TCommandEnum name)
        {
            return BuildCommand(name, SupportsResultParam);
        }
        /// <summary>
        /// Builds the command with the specified name.
        /// </summary>
        /// <param name="name">A TCommandEnum that is the name of the command to build.</param>
        /// <param name="addResultParam">A bool that indicates whether this command supports the result parameter.</param>
        /// <returns>An IDbCommand that was built.</returns>
        protected IDbCommand BuildCommand(TCommandEnum name, bool addResultParam)
        {
            IDbCommand ReturnValue;

            ReturnValue = CreateCommand(name);
            if (addResultParam)
                AddParameterResult(ReturnValue);

            return ReturnValue;
        }

        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="name">A TCommandEnum indicating the command to create.</param>
        /// <returns>An IDbCommand that was created.</returns>
        protected IDbCommand CreateCommand(TCommandEnum name)
        {
            return CreateCommand(name, CommandType.StoredProcedure);
        }
        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="name">A TCommandEnum indicating the command to create.</param>
        /// <param name="commandType">A CommandType indicating the type of the command to create.</param>
        /// <returns>An IDbCommand that was created.</returns>
        protected IDbCommand CreateCommand(TCommandEnum name, CommandType commandType)
        {
            return _DatabaseHelper.CreateCommand(GetName(name), commandType);
        }

        /// <summary>
        /// Creates a parameter with the specified information.
        /// </summary>
        /// <param name="parameter">A TParameterEnum indicating the parameter to create.</param>
        /// <param name="direction">A ParameterDirection that indicates the direction of the parameter.</param>
        /// <param name="value">An object containing the value of the parameter.</param>
        /// <returns>An IDataParameter that was created.</returns>
        protected IDataParameter CreateParameter(TParameterEnum parameter, ParameterDirection direction = ParameterDirection.Input, object value = null)
        {
            IDataParameter returnValue;

            GetParameterInformation(parameter, out string parameterName, out DbType parameterDataType, out int parameterSize);
            returnValue = _DatabaseHelper.CreateParameter(parameterName, parameterDataType, parameterSize, direction, value);

            return returnValue;
        }

        /// <summary>
        /// Executes the specified command in non-query mode.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to execute.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteNonQuery(IDbCommand command)
        {
            int ReturnValue = 0;

            try
            {
                using (IDbConnection connection = GetConnection(_ConnectionString))
                {
                    connection.Open();
                    command.Connection = connection;
                    ReturnValue = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogException(ex, $"Unable to execute the query '{command.CommandText}'");
                ReturnValue = -2;
            }

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command in non-query mode.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to execute.</param>
        /// <param name="result">An int that will be set to the value of the result parameter.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteNonQuery(IDbCommand command, out int result)
        {
            int ReturnValue;

            ReturnValue = ExecuteNonQuery(command);
            result = GetResultParameterValue(command);

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command with a single parameter in non-query mode.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="parameterName">A TParameterEnum indicating the name of the parameter.</param>
        /// <param name="parameterValue">An object indicating the value of the parameter.</param>
        /// <param name="result">An int that will be set to the result of the execution.</param>
        /// <returns>An int containing the value of the result parametr.</returns>
        protected int ExecuteNonQuery(TCommandEnum commandName, TParameterEnum parameterName, object parameterValue, out int executionResult)
        {
            IDbCommand TheCommand;

            TheCommand = BuildCommand(commandName);
            AddParameter(TheCommand, parameterName, parameterValue);
            executionResult = ExecuteNonQuery(TheCommand, out int ReturnValue);

            return ReturnValue;
        }

        /// <summary>
        /// Executes the specified command and provides results in a DataSet.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to execute.</param>
        /// <param name="results">A DataSet that will be populated with the results.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the result set.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteReader(IDbCommand command, out DataSet results, TTableEnum[] tableNames = null)
        {
            int ReturnValue = 0;

            // Defaults
            results = null;

            try
            {
                using (IDbConnection conn = GetConnection(_ConnectionString))
                {
                    IDataAdapter DataAdapter;

                    conn.Open();
                    command.Connection = conn;

                    DataAdapter = GetDataAdapter(command, tableNames);
                    results = new DataSet();
                    DataAdapter.Fill(results);
                    if (SupportsResultParam)
                        ReturnValue = GetResultParameterValue(command);
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogException(ex, $"Unable to read data for command '{command.CommandText}'");
                ReturnValue = -2;
            }

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command and provides results in a DataTable.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to execute.</param>
        /// <param name="results">A DataTable that will be populated with the results.</param>
        /// <param name="tableNumber">An int indicating the zero-based table number to return.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteReader(IDbCommand command, out DataTable results, int tableNumber = 0)
        {
            int ReturnValue = 0;

            // Defaults
            results = null;

            ReturnValue = ExecuteReader(command, out DataSet Results);
            if (Results != null && Results.Tables.Count >= tableNumber + 1)
                results = Results.Tables[tableNumber];

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command and provides results in a DataTable.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="results">A DataTable that will be populated with the results.</param>
        /// <param name="tableNumber">An int indicating the zero-based table number to return.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteReader(TCommandEnum commandName, out DataTable results, int tableNumber = 0)
        {
            int ReturnValue;
            IDbCommand TheCommand;

            TheCommand = BuildCommand(commandName);
            ReturnValue = ExecuteReader(TheCommand, out results, tableNumber);

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command and provides results in a DataSet.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="results">A DataSet that will be populated with the results.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the result set.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteReader(TCommandEnum commandName, out DataSet results, TTableEnum[] tableNames = null)
        {
            int ReturnValue;
            IDbCommand TheCommand;

            TheCommand = BuildCommand(commandName);
            ReturnValue = ExecuteReader(TheCommand, out results, tableNames);

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command with a single parameter and provides results in a DataSet.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="parameterName">A TParameterEnum indicating the name of the parameter.</param>
        /// <param name="parameterValue">An object indicating the value of the parameter.</param>
        /// <param name="results">A DataSet that will be populated with the results.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the result set.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteReader(TCommandEnum commandName, TParameterEnum parameterName, object parameterValue, out DataSet results, TTableEnum[] tableNames = null)
        {
            int ReturnValue;
            IDbCommand TheCommand;

            TheCommand = BuildCommand(commandName);
            AddParameter(TheCommand, parameterName, parameterValue);
            ReturnValue = ExecuteReader(TheCommand, out results, tableNames);

            return ReturnValue;
        }
        /// <summary>
        /// Executes the specified command with a single parameter and provides results in a DataTable.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="parameterName">A TParameterEnum indicating the name of the parameter.</param>
        /// <param name="parameterValue">An object indicating the value of the parameter.</param>
        /// <param name="results">A DataTable that will be populated with the results.</param>
        /// <param name="tableNumber">An int indicating the zero-based table number to return.</param>
        /// <returns>An int containing the result of the execution.</returns>
        protected int ExecuteReader(TCommandEnum commandName, TParameterEnum parameterName, object parameterValue, out DataTable results, int tableNumber = 0)
        {
            int ReturnValue;
            IDbCommand TheCommand;

            TheCommand = BuildCommand(commandName);
            AddParameter(TheCommand, parameterName, parameterValue);
            ReturnValue = ExecuteReader(TheCommand, out results, tableNumber);

            return ReturnValue;
        }

        /// <summary>
        /// Executes the specified command and provides results in a DataSet.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to execute.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the result set.</param>
        /// <returns>A DataSet that will be populated with the results.</returns>
        protected DataSet ExecuteReaderToDataSet(IDbCommand command, TTableEnum[] tableNames = null)
        {
            ExecuteReader(command, out DataSet returnValue, tableNames);
            return returnValue;
        }
        /// <summary>
        /// Executes the specified command with a single parameter and provides results in a DataSet.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="parameterName">A TParameterEnum indicating the name of the parameter.</param>
        /// <param name="parameterValue">An object indicating the value of the parameter.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the result set.</param>
        /// <returns>A DataSet that will be populated with the results.</returns>
        protected DataSet ExecuteReaderToDataSet(TCommandEnum commandName, TParameterEnum parameterName, object parameterValue, TTableEnum[] tableNames = null)
        {
            ExecuteReader(commandName, parameterName, parameterValue, out DataSet returnValue, tableNames);
            return returnValue;
        }
        /// <summary>
        /// Executes the specified command and provides results in a DataSet.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the result set.</param>
        /// <returns>A DataSet that will be populated with the results.</returns>
        protected DataSet ExecuteReaderToDataSet(TCommandEnum commandName, TTableEnum[] tableNames = null)
        {
            ExecuteReader(commandName, out DataSet returnValue, tableNames);
            return returnValue;
        }

        /// <summary>
        /// Executes the specified command and provides results in a DataTable.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to execute.</param>
        /// <param name="tableNumber">An int indicating the zero-based table number to return.</param>
        /// <returns>A DataTable that will be populated with the results.</returns>
        protected DataTable ExecuteReaderToDataTable(IDbCommand command, int tableNumber = 0)
        {
            ExecuteReader(command, out DataTable returnValue, tableNumber);
            return returnValue;
        }
        /// <summary>
        /// Executes the specified command and provides results in a DataTable.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="tableNumber">An int indicating the zero-based table number to return.</param>
        /// <returns>A DataTable that will be populated with the results.</returns>
        protected DataTable ExecuteReaderToDataTable(TCommandEnum commandName, int tableNumber = 0)
        {
            ExecuteReader(commandName, out DataTable returnValue, tableNumber);
            return returnValue;
        }
        /// <summary>
        /// Executes the specified command with a single parameter and provides results in a DataTable.
        /// </summary>
        /// <param name="commandName">A TCommandEnum that is the command to execute.</param>
        /// <param name="parameterName">A TParameterEnum indicating the name of the parameter.</param>
        /// <param name="parameterValue">An object indicating the value of the parameter.</param>
        /// <param name="tableNumber">An int indicating the zero-based table number to return.</param>
        /// <returns>A DataTable that will be populated with the results.</returns>
        protected DataTable ExecuteReaderToDataTable(TCommandEnum commandName, TParameterEnum parameterName, object parameterValue, int tableNumber = 0)
        {
            ExecuteReader(commandName, parameterName, parameterValue, out DataTable returnValue, tableNumber);
            return returnValue;
        }

        /// <summary>
        /// Gets a connection with the specified connection parameters.
        /// </summary>
        /// <param name="connectionParameters">A string containing the connection parameters.</param>
        /// <returns>An IDbConnection that is the connection.</returns>
        protected IDbConnection GetConnection(string connectionParameters)
        {
            return _DatabaseHelper.CreateConnection(connectionParameters);
        }

        /// <summary>
        /// Gets a data adapter for the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command.</param>
        /// <param name="tableNames">An array of TTableEnum indicating the table names for the data adapter.</param>
        /// <returns>An IDataAdapter for the specified command.</returns>
        protected IDataAdapter GetDataAdapter(IDbCommand command, TTableEnum[] tableNames = null)
        {
            return _DatabaseHelper.CreateDataAdapter(command, GetName(tableNames));
        }

        /// <summary>
        /// Gets the name of a command.
        /// </summary>
        /// <param name="command">A TCommandEnum that is the command to get the name of.</param>
        /// <returns>A string containing the name of the command.</returns>
        protected abstract string GetName(TCommandEnum command);
        /// <summary>
        /// Gets the name of a parameter.
        /// </summary>
        /// <param name="parameter">A TParameterEnum that is the parameter to get the name of.</param>
        /// <returns>A string containing the name of the parameter.</returns>
        protected abstract string GetName(TParameterEnum parameter);
        /// <summary>
        /// Overridden in derived clases to get the name of a table.
        /// </summary>
        /// <param name="table">A TTableEnum indicating the table to get the name of.</param>
        /// <returns>A string containing the table name.</returns>
        protected abstract string GetName(TTableEnum table);
        /// <summary>
        /// Gets the table names in the specified tables list.
        /// </summary>
        /// <param name="tables">A TTableEnum containing the table names.</param>
        /// <returns>An array of string containing the table names.</returns>
        protected string[] GetName(TTableEnum[] tables)
        {
            string[] ReturnValue = null;

            if (tables != null && tables.Length > 0)
            {
                ReturnValue = new string[tables.Length];
                for (int i = 0; i < tables.Length; i++)
                    ReturnValue[i] = GetName(tables[i]);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Tries to get the value of the specified object as the specified type.
        /// </summary>
        /// <typeparam name="T">A T that is the type that the object will be cast as.</typeparam>
        /// <param name="Value">An object that is the object to be cast.</param>
        /// <param name="value">A T that is the cast object, or the default value of T.</param>
        /// <returns>A bool indicating success.</returns>
        protected static bool GetObjectAsValue<T>(object Value, out T value)
        {
            bool ReturnValue = false;

            // Defaults
            value = default(T);

            if (Value != null)
            {
                if (Value is T)
                {
                    ReturnValue = true;
                    value = (T)Value;
                }
                else
                {
                    MethodInfo TryParseMethod;

                    TryParseMethod = typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });
                    if (TryParseMethod != null)
                    {
                        object Result;
                        object[] TryParseParams;

                        TryParseParams = new object[] { Value.ToString(), null };
                        Result = TryParseMethod.Invoke(null, TryParseParams);
                        if (Result != null && Result is bool && (bool)Result)
                        {
                            value = (T)TryParseParams[1];
                            ReturnValue = true;
                        }
                    }
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Overridden in derived classes to retrieve parameter information.
        /// </summary>
        /// <param name="parameter">A TParameterEnum that is the parameter to get information for.</param>
        /// <param name="name">A string that will be set to the name of the parameter.</param>
        /// <param name="dataType">A DbType that will be set to the data type of the parameter.</param>
        /// <param name="size">An int that will be set to the size of the parameter.</param>
        protected abstract void GetParameterInformation(TParameterEnum parameter, out string name, out DbType dataType, out int size);

        /// <summary>
        /// Gets the value of the specified parameter from the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to get the parameter from.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to get the value of.</param>
        /// <param name="value">A DateTime that will be set to the value of the parameter.</param>
        /// <returns>A bool indicating whether the parameter value was obtained successfully.</returns>
        protected bool GetParameterValue(IDbCommand command, TParameterEnum parameter, out DateTime value)
        {
            return GetParameterValue<DateTime>(command, parameter, out value);
        }
        /// <summary>
        /// Gets the value of the specified parameter from the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to get the parameter from.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to get the value of.</param>
        /// <param name="value">A float that will be set to the value of the parameter.</param>
        /// <returns>A bool indicating whether the parameter value was obtained successfully.</returns>
        protected bool GetParameterValue(IDbCommand command, TParameterEnum parameter, out float value)
        {
            return GetParameterValue<float>(command, parameter, out value);
        }
        /// <summary>
        /// Gets the value of the specified parameter from the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to get the parameter from.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to get the value of.</param>
        /// <param name="value">An int that will be set to the value of the parameter.</param>
        /// <returns>A bool indicating whether the parameter value was obtained successfully.</returns>
        protected bool GetParameterValue(IDbCommand command, TParameterEnum parameter, out int value)
        {
            return GetParameterValue<int>(command, parameter, out value);
        }
        /// <summary>
        /// Gets the value of the specified parameter from the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to get the parameter from.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to get the value of.</param>
        /// <param name="value">An object that will be set to the value of the parameter.</param>
        /// <returns>A bool indicating whether the parameter value was obtained successfully.</returns>
        protected bool GetParameterValue(IDbCommand command, TParameterEnum parameter, out object value)
        {
            string ParameterName;
            bool ReturnValue = false;

            value = null;

            ParameterName = GetName(parameter);
            if (command.Parameters.Contains(ParameterName))
            {
                IDataParameter Parameter;

                ReturnValue = true;
                Parameter = (IDataParameter)command.Parameters[ParameterName];
                value = Parameter.Value;
            }

            return ReturnValue;
        }
        /// <summary>
        /// Gets the value of the specified parameter from the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to get the parameter from.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to get the value of.</param>
        /// <param name="value">A string that will be set to the value of the parameter.</param>
        /// <returns>A bool indicating whether the parameter value was obtained successfully.</returns>
        protected bool GetParameterValue(IDbCommand command, TParameterEnum parameter, out string value)
        {
            return GetParameterValue<string>(command, parameter, out value);
        }
        /// <summary>
        /// Gets the value of the specified parameter from the specified command, cast to type T.
        /// </summary>
        /// <typeparam name="T">A T that is the type to cast the value to</typeparam>
        /// <param name="command">An IDbCommand that is the command to get the parameter from.</param>
        /// <param name="parameter">A TParameterEnum indicating the parameter to get the value of.</param>
        /// <param name="value">A T that will be set to the value of the parameter.</param>
        /// <returns>A bool indicating whether the parameter value was obtained successfully.</returns>
        protected bool GetParameterValue<T>(IDbCommand command, TParameterEnum parameter, out T value)
        {
            bool ReturnValue = false;

            // Defaults
            value = default(T);

            if (GetParameterValue(command, parameter, out object Value))
                ReturnValue = GetObjectAsValue(Value, out value);

            return ReturnValue;
        }

        /// <summary>
        /// Gets the result parameter.
        /// </summary>
        /// <returns>A TParameterEnum that is the result parameter.</returns>
        protected abstract TParameterEnum GetResultParameter();

        /// <summary>
        /// Gets the value of the result parameter for the specified command.
        /// </summary>
        /// <param name="command">An IDbCommand that is the command to get the result parameter for.</param>
        /// <returns>An int indicating the value of the result parameter.</returns>
        protected int GetResultParameterValue(IDbCommand command)
        {
            int ReturnValue = -3;

            if (!GetParameterValue(command, GetResultParameter(), out ReturnValue))
                ReturnValue = -3;

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Create an instance of the database with the specified type name.
        /// </summary>
        /// <param name="assembly">A string containing the name of the assembly in which the database type is found.</param>
        /// <param name="typeName">A string containing the type name of the database to create.</param>
        /// <param name="connectionParameters">A string containing the connection parameters for the database.</param>
        /// <returns>A T that is the created instance.</returns>
        public static T CreateInstance<T>(string assembly, string typeName, string connectionParameters)
            where T : IDatabaseBase<TParameterEnum, TTableEnum>
        {
            T ReturnValue = default(T);

            try
            {
                ReturnValue = InstanceCreator<T>.CreateInstance(assembly, typeName);
                if (ReturnValue != null)
                    ReturnValue.ConnectionParameters = connectionParameters;
                else
                    throw new InvalidOperationException("The created database instance was null");
            }
            catch (Exception ex)
            {
                LogWriter.LogException(ex, "Unable to create an instance of the requested database type");
                throw new InvalidOperationException("Unable to create an instance of the requested database type", ex);
            }

            return ReturnValue;
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets an indication of whether the database implementation supports a results parameter.
        /// </summary>
        protected bool SupportsResultParam { get; set; }

        #endregion

        #region Interfaces
        #region _IDatabaseBase_
        #region __Methods__
        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetColumnValue(DataRow row, TParameterEnum column, out bool value)
        {
            bool ReturnValue = false;

            // Defaults
            value = false;

            if (_DatabaseBaseInterface.GetColumnValue(row, column, out object Value))
            {
                ReturnValue = true;
                if (!GetObjectAsValue(Value, out value))
                {
                    if (Value != null)
                    {
                        try
                        {
                            switch (Value.ToString().ToUpper())
                            {
                                case "1":
                                case "TRUE":
                                case "Y":
                                case "YES":
                                    value = true;
                                    break;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            return ReturnValue;
        }
        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetColumnValue(DataRow row, TParameterEnum column, out DateTime value)
        {
            return GetColumnValue(row, column, out value);
        }
        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetColumnValue(DataRow row, TParameterEnum column, out float value)
        {
            return GetColumnValue(row, column, out value);
        }
        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetColumnValue(DataRow row, TParameterEnum column, out int value)
        {
            return GetColumnValue(row, column, out value);
        }
        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetColumnValue(DataRow row, TParameterEnum column, out object value)
        {
            bool ReturnValue = false;

            // Defaults
            value = null;

            if (row != null)
            {
                if (row.Table != null)
                {
                    string ColumnName;

                    ColumnName = GetName(column);
                    if (row.Table.Columns.Contains(ColumnName))
                    {
                        value = row[ColumnName];
                        ReturnValue = true;
                    }
                }
                else
                    throw new ArgumentException("The specified row is not associated with a DataTable", "row");
            }
            else
                throw new ArgumentException("The specified row is NULL", "row");

            return ReturnValue;
        }
        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetColumnValue(DataRow row, TParameterEnum column, out string value)
        {
            return GetColumnValue(row, column, out value);
        }

        bool IDatabaseBase<TParameterEnum, TTableEnum>.GetTable(DataSet dataSet, TTableEnum table, out DataTable dataTable)
        {
            bool ReturnValue = false;

            // Defaults
            dataTable = null;

            if (dataSet != null)
            {
                string TableName;

                TableName = GetName(table);
                if (dataSet.Tables.Contains(TableName))
                {
                    dataTable = dataSet.Tables[TableName];
                    ReturnValue = true;
                }
            }
            else
                throw new ArgumentException("The specified DataSet is NULL", "dataSet");

            return ReturnValue;
        }

        #endregion
        #region __Properties__
        string IDatabaseBase<TParameterEnum, TTableEnum>.ConnectionParameters
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        #endregion
        #endregion
        #endregion
    }
}