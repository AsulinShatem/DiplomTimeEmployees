using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TimeEmployees.Tables
{
    class DBConnect
    {
        //строка подключения к БД
        public static string SQLConnString = "Data Source=WIN-DLIDHQMLG2J\\MSSQLSERVER01;Initial Catalog=TimeEmployees;Integrated Security=True";

        //функция получает текстовое описание к значениям из наборов Type_developments.cs и Type_vacation.cs
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());//получаем значение, которое необходимо заменить
            //проверяем, если значение найдено, то подставляем значение описания
            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }
    }
}
