using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace AdministracijaKorisnika.Model
{
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        // Metoda za dobijanje korisnika na osnovu korisničkog imena i lozinke
        public static User GetUser(string userName, string userPass)
        {
            // Logujemo pokušaj prijave
            Console.WriteLine($"Attempting to log in with Username: {userName} and Password: {userPass}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserName = @userName AND UserPass = @userPass", connection);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@userPass", userPass);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("User found in the database.");
                        return new User
                        {
                            ID = (int)reader["ID"],
                            UserName = (string)reader["UserName"],
                            UserPass = (string)reader["UserPass"],
                            IsAdmin = (bool)reader["IsAdmin"]
                        };
                    }
                    else
                    {
                        Console.WriteLine("User not found in the database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex.Message}");
                }
            }
            return null;
        }


        // Metoda za dobijanje svih korisnika iz baze
        public static List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        ID = (int)reader["ID"],
                        UserName = (string)reader["UserName"],
                        UserPass = (string)reader["UserPass"],
                        IsAdmin = (bool)reader["IsAdmin"]
                    });
                }
            }
            return users;
        }

        // Metoda za ažuriranje podataka o korisniku
        public static void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Users SET UserName = @userName, UserPass = @userPass, IsAdmin = @isAdmin WHERE ID = @id", connection);
                cmd.Parameters.AddWithValue("@id", user.ID);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@userPass", user.UserPass);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                cmd.ExecuteNonQuery();
            }
        }

        // Metoda za dodavanje novog korisnika
        public static void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (UserName, UserPass, IsAdmin) VALUES (@userName, @userPass, @isAdmin)", connection);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@userPass", user.UserPass);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
