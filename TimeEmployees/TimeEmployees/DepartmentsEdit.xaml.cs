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
    /// Логика взаимодействия для DepartmentsEdit.xaml
    /// </summary>
    public partial class DepartmentsEdit : Window
    {
        int ID;
        public DepartmentsEdit(int id)
        {
            InitializeComponent();
            ID = id;
            //если ID не равен 0, то заполняем значения полей из БД
            if (ID != 0)
            {
                TDepartments t = new TDepartments();
                t.ID = ID;
                t.GetByID();
                txtTitle.Text = t.dt.Rows[0][1].ToString();
            }
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            TDepartments t = new TDepartments();
            t.Title = txtTitle.Text;
            t.ID = ID;

            //если ID был передан, то функция добавления, если нет - редактирования
            if (ID == 0)
            {
                t.Add();
            }
            else
            {
                t.Edit();
            }

            DialogResult = true;//результат вызова окна
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
