using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EmployeesEdit.xaml
    /// </summary>
    public partial class EmployeesEdit : Window
    {
        int ID;
        public EmployeesEdit(int id)
        {
            InitializeComponent();
            ID = id;
            //если ID не 0, то заполняем поля из таблицы
            if (ID != 0)
            {
                TEmployees t = new TEmployees();
                t.ID = ID;
                t.GetByID();
                txtFIO.Text = t.dt.Rows[0]["full_name"].ToString();
                cbDepartment.SelectedValue = int.Parse(t.dt.Rows[0]["id_department"].ToString());
                cbRole.Text= t.dt.Rows[0]["role"].ToString();
                txtLogin.Text = t.dt.Rows[0]["login"].ToString();
                txtPassword.Text = t.dt.Rows[0]["password"].ToString();
                dtBirthday.SelectedDate = DateTime.Parse(t.dt.Rows[0]["birthday"].ToString());

            }

            FillCB();
        }
        private void FillCB()
        {
            TDepartments d = new TDepartments();
            d.Get();//получаем все отделы
            cbDepartment.ItemsSource = d.dt.DefaultView;//назначаем источник записи для выпадающего списка
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            //создаем экземпляр класса сотрудники
            TEmployees t = new TEmployees();
            //задаем значения
            t.full_name = txtFIO.Text;
            t.ID = ID;
            t.id_department = int.Parse(cbDepartment.SelectedValue.ToString());
            t.role = cbRole.Text.ToString();
            t.login = txtLogin.Text;
            t.password = txtPassword.Text;
            t.birthday = DateTime.Parse(dtBirthday.SelectedDate.ToString());


            //если id =0, то добавление данных, если нет - обновление
            if (ID == 0)
            {
                t.Add();
            }
            else
            {
                t.Edit();
            }

            DialogResult = true;
        }
    }
}
