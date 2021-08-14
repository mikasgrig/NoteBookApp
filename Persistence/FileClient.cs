using Dapper;
using MySql.Data.MySqlClient;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class FileClient : IFileClient
    {
        public void Append(Note item, string database, string query)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "Localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "testas";
            connectionStringBuilder.Password = "Testas2020;";
            connectionStringBuilder.Database = database;
            var connectionString = connectionStringBuilder.GetConnectionString(true);
            using var connection = new MySqlConnection(connectionString);
            connection.Execute(query, item);
        }

        public void DeleteFileContents(string database)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "Localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "testas";
            connectionStringBuilder.Password = "Testas2020;";
            connectionStringBuilder.Database = database;
            var connectionString = connectionStringBuilder.GetConnectionString(true);
            using var connection = new MySqlConnection(connectionString);
            var deleteSql = "DELETE FROM note";
            connection.Execute(deleteSql);
            var command = new MySqlCommand("ALTER TABLE `notes`.`note` AUTO_INCREMENT = 1 ", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public IEnumerable<T> ReadAll<T>(string database, string tableName)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "Localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "testas";
            connectionStringBuilder.Password = "Testas2020;";
            connectionStringBuilder.Database = database;
            var connectionString = connectionStringBuilder.GetConnectionString(true);

            using var connection = new MySqlConnection(connectionString);
            var query = "SELECT * FROM note";
            using var command = new MySqlCommand(query, connection);
            var notesList = connection.Query<Note>(query);
            var newlist = (IEnumerable<T>)notesList;
            return newlist;
        }

        public void WriteAll(string database, int id, string query)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "Localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "testas";
            connectionStringBuilder.Password = "Testas2020;";
            connectionStringBuilder.Database = database;
            var connectionString = connectionStringBuilder.GetConnectionString(true);
            using var connection = new MySqlConnection(connectionString);
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
