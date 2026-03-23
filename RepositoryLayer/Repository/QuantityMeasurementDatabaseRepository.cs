using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RepositoryLayer.Interface;
using ModelLayer.Entity;

namespace RepositoryLayer.Repository
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string _connectionString =
@"Server=localhost\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void SaveMeasurement(MeasurementRecord record)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(@"
                INSERT INTO Measurements (InputValue, FromUnit, ToUnit, ResultValue, ConversionDateTime)
                VALUES (@InputValue, @FromUnit, @ToUnit, @ResultValue, @ConversionDateTime)", connection);

            command.Parameters.AddWithValue("@InputValue", record.InputValue);
            command.Parameters.AddWithValue("@FromUnit", record.FromUnit);
            command.Parameters.AddWithValue("@ToUnit", record.ToUnit);
command.Parameters.AddWithValue("@ResultValue", record.ResultValue);
            command.Parameters.AddWithValue("@ConversionDateTime", record.ConversionDateTime);

            command.ExecuteNonQuery();
        }

        public List<MeasurementRecord> GetAllMeasurements()
        {
            var measurements = new List<MeasurementRecord>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(
                "SELECT Id, InputValue, FromUnit, ToUnit, ResultValue, ConversionDateTime FROM Measurements ORDER BY ConversionDateTime DESC",
                connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var record = new MeasurementRecord(
                    reader.GetInt32(0),
                    reader.GetDouble(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetDouble(4),
                    reader.GetDateTime(5)
                );
                measurements.Add(record);
            }
            return measurements;
        }

        public int GetTotalCount()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand("SELECT COUNT(*) FROM Measurements", connection);
            return (int)command.ExecuteScalar();
        }

        public void DeleteAllMeasurements()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand("DELETE FROM Measurements", connection);
            command.ExecuteNonQuery();
        }
    }
}
