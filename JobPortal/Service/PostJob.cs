using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using JobPortal.Models;
namespace JobPortal.Service
{
    /// <summary>
    /// database connection
    /// </summary>
    public class PostJob
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlCommand command;
        SqlDataAdapter sda;
        DataTable datatable;
        /// <summary>
        /// to read data from database
        /// </summary>
        /// <returns></returns>
        public List<PostJobMV> GetUsers()
        {
            try
            {
                command = new SqlCommand("SPR_PostJobTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(command);
                datatable = new DataTable();
                sda.Fill(datatable);
                List<PostJobMV> list = new List<PostJobMV>();
                foreach (DataRow datarow in datatable.Rows)
                {
                    list.Add(new PostJobMV
                    {
                        PostJobID = Convert.ToInt32(datarow["PostJobID"]),
                        JobTitle = datarow["JobTitle"].ToString(),
                        JobDescription = datarow["JobDescription"].ToString(),
                        MinSalary = Convert.ToInt32(datarow["MinSalary"]),
                        MaxSalary = Convert.ToInt32(datarow["MaxSalary"]),
                        Location = datarow["Location"].ToString(),
                        Vacancy = Convert.ToInt32(datarow["Vacancy"]),
                        PostDate = Convert.ToDateTime(datarow["PostDate"]),
                        WebUrl = datarow["WebUrl"].ToString(),
                    });
                }
                return list;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// to insert data to database
        /// </summary>
        /// <returns></returns>
        public bool InsertUser(PostJobMV user)
        {
            try
            {
                command = new SqlCommand("SPI_PostJobTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JobTitle", user.JobTitle);
                command.Parameters.AddWithValue("@JobDescription", user.JobDescription);
                command.Parameters.AddWithValue("@MinSalary", user.MinSalary);
                command.Parameters.AddWithValue("@MaxSalary", user.MaxSalary);
                command.Parameters.AddWithValue("@Location", user.Location);
                command.Parameters.AddWithValue("@Vacancy", user.Vacancy);
                command.Parameters.AddWithValue("@WebUrl", user.WebUrl);
                command.Parameters.AddWithValue("@PostDate", user.PostDate);
                connection.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <returns></returns>
        public bool UpdateUser(PostJobMV user)
        {
            try
            {
                command = new SqlCommand("SPU_PostJobTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PostJobID", user.PostJobID);
                command.Parameters.AddWithValue("@JobTitle", user.JobTitle);
                command.Parameters.AddWithValue("@JobDescription", user.JobDescription);
                command.Parameters.AddWithValue("@MinSalary", user.MinSalary);
                command.Parameters.AddWithValue("@MaxSalary", user.MaxSalary);
                command.Parameters.AddWithValue("@Location", user.Location);
                command.Parameters.AddWithValue("@Vacancy", user.Vacancy);
                command.Parameters.AddWithValue("@WebUrl", user.WebUrl);
                command.Parameters.AddWithValue("@PostDate", user.PostDate);
                connection.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)

                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// to delete data from database
        /// </summary>
        /// <returns></returns>
        public int DeleteUser(int PostJobID)
        {
            try
            {
                command = new SqlCommand("SPD_PostJobTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PostJobID", PostJobID);

                connection.Open();

                return command.ExecuteNonQuery();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}