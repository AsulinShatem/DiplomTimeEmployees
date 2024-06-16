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
    /// Логика взаимодействия для Departments.xaml
    /// </summary>
    public partial class Departments : Window
    {
        public Departments()
        {
            InitializeComponent();
        }
        private void loaddata()
        {
            //создаем класс отделов
            TDepartments t = new TDepartments();
            t.Get();//заполняем таблицу данными
            dg.ItemsSource = t.dt.AsDataView();// указываем источник данных таблице
            //оформление таблицы
            dg.Columns[0].MaxWidth = 0;//скрываем столбец
            dg.Columns[1].Header = "Название";//заголовок колонки
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex < 0) return;
            //содаем форму редактирования и передаем ID выделенной записи в DataGrid
            DepartmentsEdit f = new DepartmentsEdit(int.Parse((this.dg.Columns[0].GetCellContent(this.dg.SelectedItem) as TextBlock).Text));
            //если  пользователь нажал "сохранить", то обновляем таблицу
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //созадем форму редактирования и передаем ID=0
            DepartmentsEdit f = new DepartmentsEdit(0);
            //если  пользователь нажал "сохранить", то обновляем таблицу
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
                //создаем класс отделов
                TDepartments t = new TDepartments();
                //задаем ID - значению текщей записи таблицы
                t.ID = int.Parse((this.dg.Columns[0].GetCellContent(this.dg.SelectedItem) as TextBlock).Text);
                t.Del();//удаляем запись из БД
                loaddata();//обновляем таблицу
            }
        }
    }
}
