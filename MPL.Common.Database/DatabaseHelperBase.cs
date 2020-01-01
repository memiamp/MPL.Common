using System;
using System.Data;

namespace MPL.Common.Database
{
    /// <summary>
    /// A class that provides helper functions for databases.
    /// </summary>
    public abstract class DatabaseHelperBase<TDbTypeEnum, TCommand, TConnection, TDataAdapter, TParameter> : IDatabaseHelper
        where TCommand : IDbCommand
        where TConnection : IDbConnection
        where TDataAdapter : IDbDataAdapter
        where TDbTypeEnum : struct, IConvertible
        where TParameter : IDbDataParameter
    {
        #region Methods
        #region _Protected_
        /// <summary>
        /// Overridden in derived classes to return the helper interface to use.
        /// </summary>
        /// <returns>An IDatabaseHelper of types TDbTypeEnum, TCommand, TConnection, TDataAdapter, and TParameter that is the helper interface.</returns>
        protected abstract IDatabaseHelper<TDbTypeEnum, TCommand, TConnection, TDataAdapter, TParameter> GetHelperInterface();

        #endregion
        #endregion

        #region Interfaces
        #region _IDatabaseHelper_
        IDbCommand IDatabaseHelper.CreateCommand(string name, CommandType type)
        {
            return GetHelperInterface().GetCommand(name, type);
        }

        IDbConnection IDatabaseHelper.CreateConnection(string connectionParameters)
        {
            return GetHelperInterface().GetConnection(connectionParameters);
        }

        IDbDataAdapter IDatabaseHelper.CreateDataAdapter(IDbCommand command, string[] tableNames)
        {
            IDbDataAdapter returnValue;

            if (command is TCommand tCommand)
                returnValue = GetHelperInterface().GetDataAdapter(tCommand, tableNames);
            else
                throw new ArgumentException("The specified command is not valid for this helper", nameof(command));

            return returnValue;
        }

        IDbDataParameter IDatabaseHelper.CreateParameter(string name, DbType dataType, int size, ParameterDirection direction, object value)
        {
            return GetHelperInterface().GetParameter(name, dataType, size, direction, value);
        }

        #endregion
        #endregion
    }
}