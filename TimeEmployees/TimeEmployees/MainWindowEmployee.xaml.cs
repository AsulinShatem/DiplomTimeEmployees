using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для MainWindowEmployee.xaml
    /// </summary>
    public partial class MainWindowEmployee : Window
    {
        public MainWindowEmployee()
        {
            InitializeComponent();
            this.Title = "Сотрудник - " + Auth.Name_user;
        }

        void loaddata()
        {
            TWork_times t = new TWork_times();
            t.GetWorkTimesForEmployee(Auth.ID_current_user);
            dg.ItemsSource = t.dt.AsDataView();//указываем источник записей для DataGrid
            //оформление заголовков
            //dg.Columns[1].Header = "Отдел";
            dg.Columns[0].ClipboardContentBinding.StringFormat = "dd.MM.yyyy HH:mm";
            dg.Columns[1].ClipboardContentBinding.StringFormat = "dd.MM.yyyy HH:mm";
            dg.Columns[0].Header = "Вход";
            dg.Columns[1].Header = "Выход";

            if (t.CheckType(Auth.ID_current_user))
            {
                btnEnter.IsEnabled = false;
                btnOut.IsEnabled = true;
            }else
            {
                btnEnter.IsEnabled = true;
                btnOut.IsEnabled = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TWork_times t = new TWork_times();
            t.AddWorkTime(Auth.ID_current_user);
            loaddata();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loaddata();

        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();//выход из приложения

        }

        private void btnOut_Click(object sender, RoutedEventArgs e)
        {
            TWork_times t = new TWork_times();
            t.AddWorkTimeOut(Auth.ID_current_user);
            loaddata();
        }
    }
}
