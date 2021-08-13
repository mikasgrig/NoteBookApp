﻿using MySql.Data.MySqlClient;
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
        public void Append(Note item, string database)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "Localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "testas";
            connectionStringBuilder.Password = "Testas2020;";
            connectionStringBuilder.Database = database;
            var connectionString = connectionStringBuilder.GetConnectionString(true);
            using var connection = new MySqlConnection(connectionString);
            var command = new MySqlCommand("INSERT INTO note (Data, Title, Text)  VALUES  (@Data, @Title, @Text)", connection);
            command.Parameters.AddWithValue("@Data", item.DateCreated);
            command.Parameters.AddWithValue("@Title", item.Title);
            command.Parameters.AddWithValue("@Text", item.Text);
            connection.Open();
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
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
            var command = new MySqlCommand("DELETE FROM note", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            command = new MySqlCommand("ALTER TABLE `notes`.`note` AUTO_INCREMENT = 1 ", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public IEnumerable<T> ReadAll<T>(string database)
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
            connection.Open();
            var reader = command.ExecuteReader();
            var notesList = new List<Note>();
            while (reader.Read())
            {
                notesList.Add(new Note
                {
                    Id = reader.GetInt32(0),
                    DateCreated = reader.GetDateTime(1),
                    Title = reader.GetString(2),
                    Text = reader.GetString(3)
                });
            }
            connection.Close();
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
