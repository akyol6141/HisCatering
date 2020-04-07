using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HisCatering
{
    public class Userdal
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=NT00387\WHITEWAY;User ID=sa;Password=2138216Akyol;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand command;
        SqlDataReader reader;
        public List<Users> GetAll()
        {
            ConnectionControl();
            command = new SqlCommand("Select * from Users", _connection);
            reader = command.ExecuteReader();
            List<Users> users = new List<Users>();

            while (reader.Read())
            {
                Users user = new Users
                {
                    UserId = Convert.ToInt32(reader["UserID"]),
                    UserName = reader["UserName"].ToString(),
                    UserPassword = reader["UserPassword"].ToString()
                };
                users.Add(user);
            }
            reader.Close();
            _connection.Close();
            return users;
        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public void Login(Users user)
        {
            ConnectionControl();
            command = new SqlCommand("Select UserName,UserPassword From Users Where UserName=@UserName And UserPassword=@UserPassword", _connection);
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                HomeScreen homeScreen = new HomeScreen();
                homeScreen.Show();
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
            _connection.Close();
        }
    }
}
