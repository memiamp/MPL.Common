using System;
using System.Data;

namespace MPL.Common.Database
{
    /// <summary>
    /// An interface that defines a database helper.
    /// </summary>
    public interface IDatabaseHelper
    {
        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="name">A string containing the name of the command.</param>
        /// <param name="type">A CommandType indicating the type of the command.</param>
        /// <returns>An IDbCommand that was created.</returns>
        IDbCommand CreateCommand(string name, CommandType type = CommandType.StoredProcedure);

        /// <summary>
        /// Creates a connection with the specified connection parameters.
        /// </summary>
        /// <param name="connectionParameters">A string containing the connection parameters.</param>
        /// <returns>An IDbConnection that is the connection.</returns>
        IDbConnection CreateConnection(string connectionParameters);

        /// <summary>
        /// Gets a SqlDataAdapter for the specified command with the specified table mappings.
        /// </summary>
        /// <param name="command">An IDbCommand to use for the data adapter.</param>
        /// <param name="tableNames">An array of string containing the table names.</param>
        /// <returns>An IDbDataAdapter for the command.</returns>
        IDbDataAdapter CreateDataAdapter(IDbCommand command, string[] tableNames = null);

        /// <summary>
        /// Creates the specified parameter.
        /// </summary>
        /// <param name="name">A string containing the name of the parameter.</param>
        /// <param name="dataType">A DbType indicating the type of the parameter.</param>
        /// <param name="size">An int indicating the size of the parameter.</param>
        /// <param name="direction">A ParameterDirection indicating the direction of the parameter.</param>
        /// <param name="value">An object containing the value of the parameter.</param>
        /// <returns>An IDbDataParameter that was created.</returns>
        IDbDataParameter CreateParameter(string name, DbType dataType, int size, ParameterDirection direction, object value);
    }
}