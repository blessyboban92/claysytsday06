using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;
using task2.Models;

namespace task2.DAL
{
    public class entrydal
    {
        string conString = ConfigurationManager.ConnectionStrings["pankaliConnectonString"].ToString();
        public List<Task> GetAllDetails()
        {
            List<Task> details = new List<Task>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                connection.Open();
                adapter.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    details.Add(new Task
                    {
                        regid = Convert.ToInt32(dr["regid"]),
                        firstname = dr["firstname"].ToString(),
                        lastname = dr["lastname"].ToString(),
                        age = Convert.ToInt32(dr["age"]),
                        photo = dr["photo"].ToString(),
                        biodata = dr["biodata"].ToString(),
                    });


                }

            }
            return details;

        }
        public bool InsertDetails(Task task)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_insert", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@firstname", task.firstname);
                command.Parameters.AddWithValue("@lastname", task.lastname);
                command.Parameters.AddWithValue("@age", task.age);
                command.Parameters.AddWithValue("@photo", task.photo);
                command.Parameters.AddWithValue("@biodata", task.biodata);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }

            if (id > 0)
            {
                return true;
            }

            else
            {
                return false;
            }


        }
        public List<Task> GetDetailsById(int regid)
        {
            List<Task> details = new List<Task>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetDetailByid";
                command.Parameters.AddWithValue("@regid", regid);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                connection.Open();
                adapter.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    details.Add(new Task
                    {
                        regid = Convert.ToInt32(dr["regid"]),
                        firstname = dr["firstname"].ToString(),
                        lastname = dr["lastname"].ToString(),
                        age = Convert.ToInt32(dr["age"]),
                        photo = dr["photo"].ToString(),
                        biodata = dr["biodata"].ToString(),
                    });


                }

            }
            return details;
        }

        public bool UpdateDetails(Task task)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_update", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@regid", task.regid);
                command.Parameters.AddWithValue("@firstname", task.firstname);
                command.Parameters.AddWithValue("@lastname", task.lastname);
                command.Parameters.AddWithValue("@age", task.age);
                command.Parameters.AddWithValue("@photo", task.photo);
                command.Parameters.AddWithValue("@biodata", task.biodata);
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }

            if (i > 0)
            {
                return true;
            }

            else
            {
                return false;
            }


        }

        public string DeleteDetails(int regid)
        {
            string result = "Your details deleted successfully";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_delete";
                command.Parameters.AddWithValue("@regid", regid);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
    }
}
