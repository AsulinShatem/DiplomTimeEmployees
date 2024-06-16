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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeEmployees.Tables;

namespace TimeEmployees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable MDT = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            Title = "Руководитель - " + Auth.Name_user;
        }

        private void departments_Click(object sender, RoutedEventArgs e)
        {
            //создание формы отделов
            Departments f = new Departments();
            f.ShowDialog();//открытие формы отделов
        }

        private void employees_Click(object sender, RoutedEventArgs e)
        {
            //создание формы сотрудников
            Employees f = new Employees();
            f.ShowDialog();//открытие формы сотрудников
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();//выход из приложения

        }
        void loaddata()
        {
            TWork_times t = new TWork_times();
            t.GetWorkTimesForDirector();
            dg.ItemsSource = t.dt.AsDataView();//указываем источник записей для DataGrid
            MDT = t.dt;
            //оформление заголовков
            //dg.Columns[1].Header = "Отдел";
            dg.Columns[2].ClipboardContentBinding.StringFormat = "dd.MM.yyyy HH:mm";
            dg.Columns[3].ClipboardContentBinding.StringFormat = "dd.MM.yyyy HH:mm";
            dg.Columns[0].Header = "Отдел";
            dg.Columns[1].Header = "Сотрудник";
            dg.Columns[2].Header = "Время входа";
            dg.Columns[3].Header = "Время выхода";
            dg.Columns[4].Header = "Время на работе";

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dtStart.Text.Length == 0)
            {
                MessageBox.Show("Укажите начальную дату фильтра");
                return;
            }

            if (dtEnd.Text.Length == 0)
            {
                MessageBox.Show("Укажите конченую дату фильтра");
                return;
            }

            string filter = "time_in >= '" + dtStart.SelectedDate + "' and time_in <= '"+DateTime.Parse(dtEnd.SelectedDate.ToString()).AddHours(23).AddMinutes(59)+"'";
            //MessageBox.Show(filter);

            DataView dv = new DataView(MDT, filter, "time_in", DataViewRowState.CurrentRows);
            dg.ItemsSource = dv;

            dg.Columns[2].ClipboardContentBinding.StringFormat = "dd.MM.yyyy HH:mm";
            dg.Columns[3].ClipboardContentBinding.StringFormat = "dd.MM.yyyy HH:mm";
            dg.Columns[0].Header = "Отдел";
            dg.Columns[1].Header = "Сотрудник";
            dg.Columns[2].Header = "Время входа";
            dg.Columns[3].Header = "Время выхода";
            dg.Columns[4].Header = "Время на работе";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();//выход из приложения

        }
    }
}
