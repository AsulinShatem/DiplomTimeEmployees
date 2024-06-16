using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeEmployees.Tables;

namespace TimeEmployees
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public static int ID_current_user;
        public static string Role_user;
        public static string Name_user;
        public Auth()
        {
            InitializeComponent();
        }

        private void OutSystem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();//выход из приложения

        }

        private void InSystem_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Text;

            if (login.Length == 0)
            {
                MessageBox.Show("Укажите логин!");
                txtLogin.Focus();
                return;
            }

            if (password.Length == 0)
            {
                MessageBox.Show("Укажите пароль!");
                txtPassword.Focus();
                return;
            }

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(DBConnect.SQLConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM employees WHERE login = @login and password = @password", conn);
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            adapter.SelectCommand = command;
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ID_current_user = int.Parse(dt.Rows[0]["id_employee"].ToString());
                Role_user = dt.Rows[0]["role"].ToString();
                Name_user = dt.Rows[0]["full_name"].ToString();

                //MessageBox.Show("Здравствуйте, " + Name_user + "!");

                if (Role_user == "Руководитель")
                {
                    MainWindow f = new MainWindow();
                    f.Show();
                }else
                {
                    MainWindowEmployee f = new MainWindowEmployee();
                    f.Show();
                }
                Hide();

            }
            else MessageBox.Show("Упс, не удалось войти.\n Проверьте логин или пароль!");
        }
    }
}
