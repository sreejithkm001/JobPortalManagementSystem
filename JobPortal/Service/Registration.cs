using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using JobPortal.Models;
namespace MVC_FinalProject.Service
{
    public class Registration
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlCommand command;
        SqlDataAdapter sda;
        DataTable datatable;
        /// <summary>
        /// to read data from database
        /// </summary>
        /// <returns></returns>
        public List<UserMV> GetUsers()
        {
            try
            {
                // Create a SqlCommand object and associate it with a stored procedure
                using (SqlCommand command = new SqlCommand("SPR_UserTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Create a SqlDataAdapter and a DataTable to store the results
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        DataTable datatable = new DataTable();
                        // Fill the DataTable with data from the stored procedure
                        sda.Fill(datatable);
                        List<UserMV> list = new List<UserMV>();
                        // Iterate through the rows in the DataTable and create UserMV objects
                        foreach (DataRow datarow in datatable.Rows)
                        {
                            list.Add(new UserMV
                            {
                                UserID = Convert.ToInt32(datarow["UserID"]),
                                UserTypeID = Convert.ToInt32(datarow["UserTypeID"]),
                                UserName = datarow["UserName"].ToString(),
                                Password = datarow["Password"].ToString(),
                                EmailAddress = datarow["EmailAddress"].ToString(),
                                ContactNo = datarow["ContactNo"].ToString(),
                            });
                        }
                        return list;
                    }
                }
            }
            finally
            {
                // The finally block will always execute, ensuring that database resources are cleaned up
                // Close the database connection if it's open
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
        public bool InsertUser(UserMV user)
        {
            try
            {
                command = new SqlCommand("SPI_UserTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@ContactNo", user.ContactNo);
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
                // The finally block will always execute, ensuring that database resources are cleaned up
                // Close the database connection if it's open
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
        public bool UpdateUser(UserMV user)
        {
            try
            {
                command = new SqlCommand("SPU_UserTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@ContactNo", user.ContactNo);
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
                // The finally block will always execute, ensuring that database resources are cleaned up
                // Close the database connection if it's open
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
        public int DeleteUser(int UserID)
        {
            try
            {
                command = new SqlCommand("SPD_UserTable", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                connection.Open();
                return command.ExecuteNonQuery();
            }
            finally
            {
                // The finally block will always execute, ensuring that database resources are cleaned up
                // Close the database connection if it's open
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}