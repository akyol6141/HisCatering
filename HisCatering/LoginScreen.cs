using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HisCatering
{
    public partial class LoginScreen : Form
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=NT00387\WHITEWAY;Initial Catalog=HisCatering;User ID=sa;Password=2138216Akyol;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand _command = new SqlCommand();
        SqlDataReader _reader;
        public LoginScreen()
        {
            InitializeComponent();
        }
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }       
        private void btnEntry_Click(object sender, EventArgs e)
        {
           string query = "SELECT * FROM Users where UserName = '" + tbxUserName.Text.Trim() + "' AND UserPassword = '" + tbxPassword.Text.Trim() + "'";
           ConnectionControl();
            _command.Connection = _connection;
            _command.CommandText = query;
            _reader = _command.ExecuteReader();                 
            if (_reader.Read())
            {
                HomeScreen home = new HomeScreen();
                this.Hide();
                home.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı bilgilerini kontrol ediniz");
            }                                   
            _connection.Close();
        }

        
    }
}

