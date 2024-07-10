using System;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;


namespace Minions
{
    class Program
    {
        static void Main()
        {
            /*SqlConnection dbcon = new SqlConnection(
                "Server=localhost;User Id=Minion;Password=minion;database=MinionsDb;Integrated Security=True;");
                */
            bool end = true;

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "Minion",
                Password = "minion",
                Database = "MinionsDb",
            };

            MySqlConnection dbcon = new MySqlConnection(builder.ConnectionString);

            dbcon.Open();

            using (dbcon)
            {
                while (end)
                {
                    Console.WriteLine("Enter command:");
                    string command = Console.ReadLine();
                    switch (command)
                    {

                        case "VillainNames":
                            MySqlCommand sqlCommand = dbcon.CreateCommand();
                            sqlCommand.CommandText = $"SELECT v.Name, COUNT(mv.MinionId) AS minionsOwned FROM Villains AS v " +
                                "JOIN MinionVillains AS mv ON v.Id = mv.VillainId " +
                                "JOIN Minions AS m ON mv.MinionId = m.Id " +
                                "GROUP BY v.Name";

                            MySqlDataReader reader = sqlCommand.ExecuteReader();
                            using (reader)
                            {
                                while (reader.Read())
                                {
                                    string villainName = reader.GetString("Name");
                                    int minionCount = reader.GetInt32("minionsOwned");
                                    if (minionCount > 3)
                                    {
                                        Console.WriteLine($"{villainName} -> {minionCount}");
                                    }
                                }
                            };
                            break;
                        case "MinionNames":
                            int idVillain = int.Parse(Console.ReadLine());
                            int counter = 1;

                            MySqlCommand sqlCommand2 = dbcon.CreateCommand();

                            sqlCommand2.CommandText = "SELECT v.Name AS villainName, m.Name AS minionName, m.Age FROM Minions AS m " +
                            "JOIN MinionVillains AS mv ON m.Id = mv.MinionId " +
                            "JOIN Villains AS v ON mv.VillainId = v.Id " +
                            $"WHERE v.Id = {idVillain}";

                            MySqlDataReader reader2 = sqlCommand2.ExecuteReader();
                            using (reader2)
                            {
                                while (reader2.Read())
                                {
                                    string villainName = reader2.GetString("villainName");
                                    string minionName = reader2.GetString("minionName");
                                    int minionAge = reader2.GetInt32("Age");
                                    if (counter == 1)
                                    {
                                        Console.WriteLine($"Villain: {villainName}");

                                    }
                                    Console.WriteLine($"{counter}: {minionName}, {minionAge}");
                                    counter++;
                                }
                            }
                            break;
                        case "AddMinion":
                            Console.WriteLine("Minion(Name Age TownName):");
                            string[] minionData = Console.ReadLine().Split().ToArray();
                            Console.WriteLine("Villain Name: ");
                            string villainForMinion = Console.ReadLine();

                            MySqlCommand checkForVillain = dbcon.CreateCommand();
                            checkForVillain.CommandText = $"SELECT Name FROM Villains WHERE Name = '{villainForMinion}'";

                            string? villainReader = (string?)checkForVillain.ExecuteScalar();

                            if (villainReader == null)
                            {
                                Console.WriteLine("Villain not found enter evilness factor id (1 - 5)");
                                int evilnessFactor = int.Parse(Console.ReadLine());
                                while (evilnessFactor < 1 && evilnessFactor > 5)
                                {
                                    Console.WriteLine("Number not valid must be between 1 and 5");
                                    evilnessFactor = int.Parse(Console.ReadLine());
                                }
                                MySqlCommand createNewVillain = dbcon.CreateCommand();
                                createNewVillain.CommandText = "INSERT INTO Villains(Name, EvilnessFactorId) " +
                                $"VALUES ('{villainForMinion}', {evilnessFactor})";
                                createNewVillain.ExecuteNonQuery();

                            }
                            MySqlCommand checkIfTownExists = dbcon.CreateCommand();
                            checkIfTownExists.CommandText = $"SELECT Id FROM Towns WHERE Name = '{minionData[2]}'";
                            int? townReader = (int?)checkIfTownExists.ExecuteScalar();
                            if (townReader == null)
                            {
                                Console.WriteLine("Town not found, enter which country it is from: ");
                                string countryName = Console.ReadLine();
                                MySqlCommand checkIfCountryExists = dbcon.CreateCommand();
                                checkIfCountryExists.CommandText = $"SELECT Id FROM Countries WHERE Name = '{countryName}'";
                                int? countryReader = (int?)checkIfCountryExists.ExecuteScalar();
                                if (countryReader == null)
                                {
                                    MySqlCommand createNewCountry = dbcon.CreateCommand();
                                    createNewCountry.CommandText = $"INSERT INTO Countries(Name) VALUES ('{countryName}')";
                                    createNewCountry.ExecuteNonQuery();
                                }
                                int countryExistsReader = (int)checkIfCountryExists.ExecuteScalar();

                                MySqlCommand createNewTown = dbcon.CreateCommand();
                                createNewTown.CommandText = $"INSERT INTO Towns(Name, CountryCode) VALUES ('{minionData[2]}', {countryExistsReader})";
                                createNewTown.ExecuteNonQuery();
                            }
                            int townExistsReader = (int)checkIfTownExists.ExecuteScalar();

                            MySqlCommand createNewMinion = dbcon.CreateCommand();
                            createNewMinion.CommandText = "INSERT INTO Minions(Name, Age, TownId) " +
                            $"VALUES ('{minionData[0]}', {minionData[1]}, {townExistsReader})";
                            createNewMinion.ExecuteNonQuery();

                            MySqlCommand getMinionId = dbcon.CreateCommand();
                            getMinionId.CommandText = $"SELECT Id FROM Minions WHERE Name = '{minionData[0]}' AND Age = {minionData[1]} AND TownId = {townExistsReader}";
                            int minionId = (int)getMinionId.ExecuteScalar();

                            MySqlCommand getVillainId = dbcon.CreateCommand();
                            getVillainId.CommandText = $"SELECT Id FROM Villains WHERE Name = '{villainForMinion}'";
                            int villainIdNew = (int)getVillainId.ExecuteScalar();

                            MySqlCommand linkMinionWithVillain = dbcon.CreateCommand();
                            linkMinionWithVillain.CommandText = $"INSERT INTO MinionVillains(MinionId, VillainId) VALUES ({minionId}, {villainIdNew})";
                            linkMinionWithVillain.ExecuteNonQuery();
                            dbcon.ChangeDatabase("MinionsDb");
                            break;
                        case "NormalizeTowns":
                            Console.WriteLine("Write a Country Name:");
                            string countryNameIdentifier = Console.ReadLine();

                            MySqlCommand findCountryId = dbcon.CreateCommand();
                            findCountryId.CommandText = $"SELECT Id FROM Countries WHERE Name = '{countryNameIdentifier}'";
                            int? countryId = (int?)findCountryId.ExecuteScalar();
                            if (countryId == null)
                            {
                                Console.WriteLine("Country not found");
                            }
                            else
                            {
                                MySqlCommand findTownsForCountry = dbcon.CreateCommand();
                                findTownsForCountry.CommandText = $"SELECT * FROM Towns WHERE CountryCode = {countryId}";

                                List<string> normalizedTownNames = new List<string>();
                                List<string> normalTownNames = new List<string>();
                                int townsCounter = 0;

                                MySqlDataReader allTowns = findTownsForCountry.ExecuteReader();
                                using (allTowns)
                                {
                                    while (allTowns.Read())
                                    {
                                        string townNameForCountry = allTowns.GetString("Name");
                                        normalizedTownNames.Add(townNameForCountry.ToUpper());
                                        normalTownNames.Add(townNameForCountry);
                                        townsCounter++;
                                    }
                                }
                                for (int i = 0; i < townsCounter; i++)
                                {
                                    MySqlCommand normalizeTowns = dbcon.CreateCommand();
                                    normalizeTowns.CommandText = "UPDATE Towns " +
                                    $"SET Name = '{normalizedTownNames[i]}' " +
                                    $"WHERE Name = '{normalTownNames[i]}'";
                                    normalizeTowns.ExecuteNonQuery();
                                }
                                System.Console.WriteLine($"{townsCounter} towns were affected.");
                                foreach (string town in normalizedTownNames)
                                {
                                    System.Console.Write($"{town} ");
                                }
                                dbcon.ChangeDatabase("MinionsDb");
                            }
                            break;
                        case "RemoveVillain":
                            System.Console.WriteLine("Enter villain name:");
                            string villainRemoveName = Console.ReadLine();

                            MySqlCommand getRemoveVillainId = dbcon.CreateCommand();
                            getRemoveVillainId.CommandText = $"SELECT Id FROM Villains WHERE Name = '{villainRemoveName}'";

                            int? removeVillainId = (int?)getRemoveVillainId.ExecuteScalar();

                            MySqlCommand getMinionsCount = dbcon.CreateCommand();
                            getMinionsCount.CommandText = $"SELECT Count(*) FROM MinionVillains WHERE VillainId = {removeVillainId}";
                            long? minionsCount = (long?)getMinionsCount.ExecuteScalar();

                            MySqlCommand releaseMinions = dbcon.CreateCommand();
                            releaseMinions.CommandText = "DELETE FROM MinionVillains " +
                            $"WHERE VillainId = {removeVillainId}";
                            releaseMinions.ExecuteNonQuery();

                            MySqlCommand removeVillain = dbcon.CreateCommand();
                            removeVillain.CommandText = $"DELETE FROM Villains WHERE Id = {removeVillainId}";
                            removeVillain.ExecuteNonQuery();

                            Console.WriteLine($"{villainRemoveName} has beed removed");
                            Console.WriteLine($"{minionsCount} have been released");

                            break;
                        case "End":
                            end = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Command");
                            break;
                    }
                }
                dbcon.Close();
            }
        }
    }
}