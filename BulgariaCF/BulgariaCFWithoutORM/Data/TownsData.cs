using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWithoutORM.Data
{
    using CrudWithoutORM.Common;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using MySqlConnector;
    using System.Data;

    public class TownsData
    {
        public List<Town> GetAll()
        {
            var townList = new List<Town>();
            using (var connection = DataBase.GetConnection())
            {
                var command = new MySqlCommand("SELECT * FROM Town", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var town = new Town(
                            reader.GetInt32(0), reader.GetString(1));
                        townList.Add(town);
                    }
                }
                connection.Close();
            }
            return townList;
        }

        public Town Get(int id)
        {
            Town town = null;
            using (var connection = DataBase.GetConnection())
            {
                var command = new MySqlCommand("SELECT * FROM Town WHERE Id=@id", connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        town = new Town(
                           reader.GetInt32(0), reader.GetString(1));
                    }
                }
                connection.Close();
            }
            return town;
        }
        public void Add(Town town)
        {
            using (var connection = DataBase.GetConnection())
            {
                var command = new MySqlCommand("INSERT INTO Town (Name) VALUES(@name)", connection);
                command.Parameters.AddWithValue("name", town.Name);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(Town town)
        {
            using (var connection = DataBase.GetConnection())
            {
                var command = new MySqlCommand("UPDATE Town SET Name=@name WHERE Id= @id", connection);
                command.Parameters.AddWithValue("id", town.Id);
                command.Parameters.AddWithValue("name", town.Name);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            using (var connection = DataBase.GetConnection())
            {
                connection.Open();

                var command = new MySqlCommand("DELETE FROM Town WHERE Id=@id", connection);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}
