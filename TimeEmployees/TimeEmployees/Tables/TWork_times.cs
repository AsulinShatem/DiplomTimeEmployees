using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeEmployees.Tables
{
    public class TWork_times
    {

        public DataTable dt = new DataTable("Employees");

        SqlConnection conn = new SqlConnection(DBConnect.SQLConnString);

        //получение всех сотрудников из таблицы work_times для руководителя
        // в запросе производим связку с другими таблицами для вывода читабельных данных
        public void GetWorkTimesForDirector()
        {
            string qrPostsList = @"SELECT d.title, e.full_name, wt.time_in, wt.time_out
                                ,round(cast(DATEDIFF(minute,wt.time_in,wt.time_out) as float)/60,2)  as hours_work
                                FROM employees e
                                INNER JOIN departments d ON e.id_department = d.id_department
                                INNER JOIN work_times wt ON wt.id_employee = e.id_employee";

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            conn.Close();
            da.Dispose();
        }

        //получение всех сотрудников из таблицы work_times для сотрудника
        // в запросе производим связку с другими таблицами для вывода читабельных данных
        public void GetWorkTimesForEmployee(int IDEmployee)
        {
            string qrPostsList = @"SELECT time_in, time_out
                                FROM work_times
                                WHERE id_employee = " + IDEmployee
                                + " ORDER BY time_in DESC";

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            conn.Close();
            da.Dispose();
        }

        //добавление входа
        public void AddWorkTime(int idemployee)
        {
            string qr = "INSERT INTO work_times (id_employee, time_in) VALUES(@id_employee, @time_in)";

            SqlCommand cmd = new SqlCommand(qr, conn);
            //передаем параметры в запрос
            cmd.Parameters.AddWithValue("@id_employee", idemployee);
            cmd.Parameters.AddWithValue("@time_in", DateTime.Now);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //добавление выхода
        public void AddWorkTimeOut(int idemployee)
        {
            string qr = "UPDATE work_times SET time_out = @time_out WHERE  id_employee = @id_employee and time_out is null";

            SqlCommand cmd = new SqlCommand(qr, conn);
            //передаем параметры в запрос
            cmd.Parameters.AddWithValue("@id_employee", idemployee);
            cmd.Parameters.AddWithValue("@time_out", DateTime.Now);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //процедура проверки статуса рабочего времени сотрудника
        public bool CheckType(int idemployee)
        {
            DataTable dtTemp = new DataTable("Employees");

            string qrPostsList = @"SELECT
                        CASE WHEN t.id_work_time is null then 0 else 1
                        END as type
                        FROM(
                        SELECT id_work_time
                        FROM work_times
                        WHERE time_out is null and id_employee = @id_employee) t";

            SqlCommand cmd = new SqlCommand(qrPostsList, conn);
            cmd.Parameters.AddWithValue("@id_employee", idemployee);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtTemp);

            conn.Close();
            da.Dispose();
            
            bool rez;
            if (dtTemp.Rows.Count == 0)
            {
                rez = false;
            }else
            {
                if (dtTemp.Rows[0][0].ToString() == "0")
                {
                    rez = false;
                }else
                {
                    rez = true;
                }
            }

            return rez;
        }
    }
}
