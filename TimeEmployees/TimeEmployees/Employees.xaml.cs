﻿using System;
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
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        public Employees()
        {
            InitializeComponent();
        }

        private void loaddata()
        {
            //содаем класс сотрудников
            TEmployees t = new TEmployees();
            t.Get();//получаем всех сотрудников
            dg.ItemsSource = t.dt.AsDataView();//указываем источник записей для DataGrid
            dg.Columns[0].MaxWidth = 0;//скрываем столбец ID
            //оформление заголовков
            //dg.Columns[1].Header = "Отдел";
            dg.Columns[3].ClipboardContentBinding.StringFormat = "dd.MM.yyyy";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //открываем форму редактирования сотрудника для добавления
            EmployeesEdit f = new EmployeesEdit(0);
            //если пользователь нажал "Сохранить", то обновляем таблицу
            bool? dialogResult = f.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    loaddata();
                    break;
                default:
                    // Indeterminate
                    break;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex < 0) return;
            //создаем форму редактирования и передаем значение ID из текущей записи
            EmployeesEdit f = new EmployeesEdit(int.Parse((this.dg.Columns[0].GetCellContent(this.dg.SelectedItem) as TextBlock).Text));
            //если пользователь нажал "сохранить", то обновлем данные в таблице
            bool? dialogResult = f.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    loaddata();
                    break;
                default:
                    // Indeterminate
                    break;
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить текущую запись?", "Осторожно", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //создаем класс сотрудников
                TEmployees t = new TEmployees();
                //передаем ID в экземпляр класса
                t.ID = int.Parse((this.dg.Columns[0].GetCellContent(this.dg.SelectedItem) as TextBlock).Text);
                t.Del();//удаляем запись
                loaddata();//обновляем данные в DataGrid
            }
        }

        private void dataGrid_AutoGeneratedColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void dataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {

        }

        private void dataGrid_AutoGeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.DisplayIndex == 3)
            {
                e.Column.ClipboardContentBinding.StringFormat = "d";
            }
        }
    }
}