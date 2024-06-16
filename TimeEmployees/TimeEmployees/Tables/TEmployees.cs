using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeEmployees.Tables
{
    public class TEmployees
    {
        public int ID;
        public int id_department;
        public string full_name;
        public DateTime birthday;
        public string login;
        public string password;
        public string role;

        public DataTable dt = new DataTable("Employees");

        SqlConnection conn = new SqlConnection(DBConnect.SQLConnString);

        //получение всех сотрудников из таблицы employees
        // в запросе производим связку с другими таблицами для вывода читабельных данных
        public void Get()
        {
            string qrPostsList = @"SELECT e.id_employee, d.title as Отдел,  full_name as ФИО, birthday as 'День рождения', role as 'Права доступа'
                FROM dbo.employees e
                INNER JOIN dbo.departments d ON d.id_department = e.id_department";

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            conn.Close();
            da.Dispose();
        }

        //получение сотрудника по значению первичного ключа
        public void GetByID()
        {
            string qrPostsList = "SELECT * FROM employees WHERE id_employee = @id_employee";

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);
            cmd.Parameters.AddWithValue("@id_employee", ID);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            conn.Close();
            da.Dispose();
        }

        //функция добавления нового сотрудника в таблицу employees
        public void Add()
        {
            string qr = "INSERT INTO employees(id_department, full_name,birthday, login, password, role) VALUES(@id_department, @full_name,@birthday, @login, @password, @role)";

            SqlCommand cmd = new SqlCommand(qr, conn);
            //передаем параметры в запрос
            cmd.Parameters.AddWithValue("@id_department", id_department);
            cmd.Parameters.AddWithValue("@full_name", full_name);
            cmd.Parameters.AddWithValue("@birthday", birthday);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@role", role);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //функция обновления сотрудника в таблице employees
        public void Edit()
        {
            string qr = @"UPDATE employees SET id_department = @id_department, 
                full_name=@full_name, birthday = @birthday, login = @login,
                password=@password, role = @role 
                WHERE id_employee = @id_employee";

            SqlCommand cmd = new SqlCommand(qr, conn);
            cmd.Parameters.AddWithValue("@id_department", id_department);
            cmd.Parameters.AddWithValue("@full_name", full_name);
            cmd.Parameters.AddWithValue("@birthday", birthday);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@id_employee", ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //функция удаления сотрудника в таблице employees по его ID
        public void Del()
        {
            string qr = "DELETE FROM employees WHERE id_employee = @id_employee";

            SqlCommand cmd = new SqlCommand(qr, conn);
            cmd.Parameters.AddWithValue("@id_employee", ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
