using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeEmployees.Tables
{
    public class TDepartments
    {
        //переменные, которые используются в классе
        public int ID;
        public string Title;

        public DataTable dt = new DataTable("Departments");
        SqlConnection conn = new SqlConnection(DBConnect.SQLConnString);

        // получение всех записей из таблицы departments
        public void Get()
        {
            string qrPostsList = "SELECT * FROM departments";//запрос

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);// параметры команды
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);//заполняем таблицу данными, которые возвращены на основании запроса

            conn.Close();
            da.Dispose();
        }

        // получение конкретной записи из таблицы departments по значению первичного ключа
        // аналогично Get, только заполняет таблицу одной записью
        public void GetByID()
        {
            string qrPostsList = "SELECT * FROM departments WHERE id_department = @id_department";

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);
            cmd.Parameters.AddWithValue("@id_department", ID);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            conn.Close();
            da.Dispose();
        }

        //функция добавления данных в таблицу
        public void Add()
        {
            string qr = "INSERT INTO departments(title) VALUES(@title)";//запрос добавления

            SqlCommand cmd = new SqlCommand(qr, conn);//создаем команду
            cmd.Parameters.AddWithValue("@title", Title);// передаем параметры в команду, значение переменной из строки 15
            conn.Open();
            cmd.ExecuteNonQuery();//выполнения запроса
            conn.Close();
        }

        //функция обновления данных в таблице
        public void Edit()
        {
            string qr = "UPDATE departments SET title = @title WHERE id_department = @id_department";//запрос обновления

            SqlCommand cmd = new SqlCommand(qr, conn);//создаем команду
            cmd.Parameters.AddWithValue("@title", Title);// передаем параметры в команду
            cmd.Parameters.AddWithValue("@id_department", ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //функция удаления данных в таблице
        public void Del()
        {
            string qr = "DELETE FROM departments WHERE id_department = @id_department";

            SqlCommand cmd = new SqlCommand(qr, conn);
            cmd.Parameters.AddWithValue("@id_department", ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
