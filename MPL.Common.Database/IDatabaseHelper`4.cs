using System;
using System.Data;

namespace MPL.Common.Database
{
    /// <summary>
    /// An interface that defines a database helper.
    /// </summary>
    /// <typeparam name="TDbTypeEnum">The type of the database types enumeration.</typeparam>
    /// <typeparam name="TCommand">The type of the command object.</typeparam>
    /// <typeparam name="TConnection">The type of the connection object.</typeparam>
    /// <typeparam name="TDataAdapter">The type of the data adapter object.</typeparam>
    /// <typeparam name="TParameter">The type of the parameter object.</typeparam>
    public interface IDatabaseHelper<TDbTypeEnum, TCommand, TConnection, TDataAdapter, TParameter> : IDatabaseHelper
        where TCommand : IDbCommand
        where TConnection : IDbConnection
        where TDataAdapter : IDbDataAdapter
        where TDbTypeEnum : struct, IConvertible
        where TParameter : IDbDataParameter
    {
        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="name">A string containing the name of the command.</param>
        /// <param name="type">A CommandType indicating the type of the command.</param>
        /// <returns>A TCommand that was created.</returns>
        TCommand GetCommand(string name, CommandType type = CommandType.StoredProcedure);

        /// <summary>
        /// Creates a connection with the specified connection parameters.
        /// </summary>
        /// <param name="connectionParameters">A string containing the connection parameters.</param>
        /// <returns>A TConnection that is the connection.</returns>
        TConnection GetConnection(string connectionParameters);

        /// <summary>
        /// Gets a SqlDataAdapter for the specified command with the specified table mappings.
        /// </summary>
        /// <param name="command">A TCommand to use for the data adapter.</param>
        /// <param name="tableNames">An array of string containing the table names.</param>
        /// <returns>A TDataAdapter for the command.</returns>
        TDataAdapter GetDataAdapter(TCommand command, string[] tableNames = null);

        /// <summary>
        /// Creates the specified parameter.
        /// </summary>
        /// <param name="name">A string containing the name of the parameter.</param>
        /// <param name="dataType">A DbType indicating the type of the parameter.</param>
        /// <param name="size">An int indicating the size of the parameter.</param>
        /// <param name="direction">A ParameterDirection indicating the direction of the parameter.</param>
        /// <param name="value">An object containing the value of the parameter.</param>
        /// <returns>A TParameter that was created.</returns>
        TParameter GetParameter(string name, DbType dataType, int size, ParameterDirection direction, object value);
        /// <summary>
        /// Creates the specified parameter.
        /// </summary>
        /// <param name="name">A string containing the name of the parameter.</param>
        /// <param name="dataType">A TDbTypeEnum indicating the type of the parameter.</param>
        /// <param name="size">An int indicating the size of the parameter.</param>
        /// <param name="direction">A ParameterDirection indicating the direction of the parameter.</param>
        /// <param name="value">An object containing the value of the parameter.</param>
        /// <returns>A TParameter that was created.</returns>
        TParameter GetParameter(string name, TDbTypeEnum dataType, int size, ParameterDirection direction, object value);

        /// <summary>
        /// Maps a DbType to a TDbTypeEnum.
        /// </summary>
        /// <param name="dbType">A DbType that is the type to map.</param>
        /// <returns>A TDbTypeEnum that was mapped from the DbType.</returns>
        TDbTypeEnum MapDbType(DbType dbType);
    }
}