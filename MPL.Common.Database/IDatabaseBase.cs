using System;
using System.Data;

namespace MPL.Common.Database
{
    /// <summary>
    /// An interface that defines shared database access functionality.
    /// </summary>
    /// <typeparam name="TParameterEnum">An enumeration that defines the column or parameter names.</typeparam>
    /// <typeparam name="TTableEnum">An enumeration that defines the table names.</typeparam>
    public interface IDatabaseBase<TParameterEnum, TTableEnum>
        where TParameterEnum : struct, IConvertible
        where TTableEnum : struct, IConvertible
    {
        #region Methods
        /// <summary>
        /// Gets the boolean value of the specified column from the specified row.
        /// </summary>
        /// <param name="row">A DataRow that is the row to get the value from.</param>
        /// <param name="column">A TParameterEnum that is the column to get the value from.</param>
        /// <param name="value">A bool that will be set to the value of the column from the row.</param>
        /// <returns>A bool indicating whether the value was obtained.</returns>
        bool GetColumnValue(DataRow row, TParameterEnum column, out bool value);
        /// <summary>
        /// Gets the DateTime value of the specified column from the specified row.
        /// </summary>
        /// <param name="row">A DataRow that is the row to get the value from.</param>
        /// <param name="column">A TParameterEnum that is the column to get the value from.</param>
        /// <param name="value">A DateTime that will be set to the value of the column from the row.</param>
        /// <returns>A bool indicating whether the value was obtained.</returns>
        bool GetColumnValue(DataRow row, TParameterEnum column, out DateTime value);
        /// <summary>
        /// Gets the float value of the specified column from the specified row.
        /// </summary>
        /// <param name="row">A DataRow that is the row to get the value from.</param>
        /// <param name="column">A TParameterEnum that is the column to get the value from.</param>
        /// <param name="value">A float that will be set to the value of the column from the row.</param>
        /// <returns>A bool indicating whether the value was obtained.</returns>
        bool GetColumnValue(DataRow row, TParameterEnum column, out float value);
        /// <summary>
        /// Gets the int value of the specified column from the specified row.
        /// </summary>
        /// <param name="row">A DataRow that is the row to get the value from.</param>
        /// <param name="column">A TParameterEnum that is the column to get the value from.</param>
        /// <param name="value">An int that will be set to the value of the column from the row.</param>
        /// <returns>A bool indicating whether the value was obtained.</returns>
        bool GetColumnValue(DataRow row, TParameterEnum column, out int value);
        /// <summary>
        /// Gets the value of the specified column from the specified row.
        /// </summary>
        /// <param name="row">A DataRow that is the row to get the value from.</param>
        /// <param name="column">A TParameterEnum that is the column to get the value from.</param>
        /// <param name="value">An object that will be set to the value of the column from the row.</param>
        /// <returns>A bool indicating whether the value was obtained.</returns>
        bool GetColumnValue(DataRow row, TParameterEnum column, out object value);
        /// <summary>
        /// Gets the string value of the specified column from the specified row.
        /// </summary>
        /// <param name="row">A DataRow that is the row to get the value from.</param>
        /// <param name="column">A TParameterEnum that is the column to get the value from.</param>
        /// <param name="value">A string that will be set to the value of the column from the row.</param>
        /// <returns>A bool indicating whether the value was obtained.</returns>
        bool GetColumnValue(DataRow row, TParameterEnum column, out string value);

        /// <summary>
        /// Gets the specified table from a dataset.
        /// </summary>
        /// <param name="dataSet">A DataSet that is the data source to get the table from.</param>
        /// <param name="table">A TTableEnum that is the table to get.</param>
        /// <param name="dataTable">A DataTable that will be set to the data table.</param>
        /// <returns>A bool indcating whether the table was obtained.</returns>
        bool GetTable(DataSet dataSet, TTableEnum table, out DataTable dataTable);

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the connection parameters for the database.
        /// </summary>
        string ConnectionParameters { get; set; }

        #endregion
    }
}