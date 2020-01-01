using System;
using System.Data;

namespace MPL.Common.Database
{
    internal class MockDatabaseHelper : DatabaseHelperBase<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>, IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>
    {
        protected override IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter> GetHelperInterface()
        {
            return this;
        }

        IDbCommand IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>.GetCommand(string name, CommandType type)
        {
            throw new NotImplementedException();
        }

        IDbConnection IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>.GetConnection(string connectionParameters)
        {
            throw new NotImplementedException();
        }

        IDbDataAdapter IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>.GetDataAdapter(IDbCommand command, string[] tableNames)
        {
            throw new NotImplementedException();
        }

        IDbDataParameter IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>.GetParameter(string name, DbType dataType, int size, ParameterDirection direction, object value)
        {
            throw new NotImplementedException();
        }

        IDbDataParameter IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>.GetParameter(string name, SqlDbType dataType, int size, ParameterDirection direction, object value)
        {
            throw new NotImplementedException();
        }

        SqlDbType IDatabaseHelper<SqlDbType, IDbCommand, IDbConnection, IDbDataAdapter, IDbDataParameter>.MapDbType(DbType dbType)
        {
            throw new NotImplementedException();
        }
    }
}