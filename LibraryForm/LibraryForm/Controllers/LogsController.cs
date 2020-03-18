using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace LibraryForm.Controllers
{
    class LogsController
    {
        //=======================================================
        // <Summary>
        //      Method to save the student's visit log in 
        //  the database
        //=======================================================
        #region AddLogs(string studentNumber, int noId, string identify)
        public static void AddLogs(string studentNumber, int noId)
        {
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddLog";

                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;
                cmd.Parameters.AddWithValue("@noId", SqlDbType.Bit).Value = noId;
                cmd.Parameters.AddWithValue("@identify", SqlDbType.VarChar).Value = "student";

                cmd.Parameters.AddWithValue("@fullName", SqlDbType.VarChar).Value = "";
                cmd.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = "";
                cmd.Parameters.AddWithValue("@label", SqlDbType.VarChar).Value = "";

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion


        //=======================================================
        // <Summary>
        //      Method to save Faculty's or other's visit 
        //  log in the database
        //=======================================================
        #region AddLogs(string fullName, string department, string label)
        public static void AddLogs(string fullName, string department, string label, string identify)
        {
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddLog";

                cmd.Parameters.AddWithValue("@fullName", SqlDbType.VarChar).Value = fullName;
                cmd.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = department;
                cmd.Parameters.AddWithValue("@label", SqlDbType.VarChar).Value = label;
                cmd.Parameters.AddWithValue("@identify", SqlDbType.VarChar).Value = "others";

                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = "";
                cmd.Parameters.AddWithValue("@noId", SqlDbType.Bit).Value = 0;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Connection.conn.Close();
            }
        }
        #endregion


        //=======================================================
        // <Summary>
        //      Method to get the student's log record
        //  returns the basic information of the student and 
        //      his/her visit informations
        //  returns List<Student> data type
        //=======================================================
        #region GetStudentLog(string studentNumber)
        public static List<Student> GetStudentLog(string studentNumber)
        {
            List<Student> students = new List<Student>();

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FetchStudentLogRecord";

                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        students.Add(new Student() { 
                            studentNumber = reader["student_id"].ToString(),
                            lastName = reader["last_name"].ToString(),
                            firstName = reader["first_name"].ToString(),
                            middleInitials = reader["middle_initials"].ToString(),
                            course = reader["course_code"].ToString(),
                            gender = reader["gender"].ToString(),
                            numberOfVisits = reader["number_of_visits"].ToString(),
                            lastVisit = reader["last_visit"].ToString()
                        });
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }

            return students;
        }
        #endregion


        //=======================================================
        //  <Summary>
        //      Method to get the student's number of offenses.
        //  returns int data type
        //=======================================================
        #region GetOffenses(string studentNumber)
        public static int GetOffenses(string studentNumber)
        {
            int retVal = 0;
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandText = "SELECT offenses FROM cl_students WHERE student_id = @studentNumber";
                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        retVal = Convert.ToInt32(reader["offenses"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Connection.conn.Close();
            }
            return retVal;
        }
        #endregion


        //=======================================================
        // <Summary>
        //      Method to get the log data in the given date
        //  returns List<Log> data type
        //=======================================================
        #region GetData(string day, string month, string year, string identify)
        public static List<Log> GetData(string day, string month, string year, string identify)
        {
            List<Log> logs = new List<Log>();

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FetchLogsRecord";

                cmd.Parameters.AddWithValue("@day", SqlDbType.VarChar).Value = day;
                cmd.Parameters.AddWithValue("@month", SqlDbType.VarChar).Value = month;
                cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = year;
                cmd.Parameters.AddWithValue("@identify", SqlDbType.VarChar).Value = identify;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        logs.Add(new Log() { 
                            label = reader["label"].ToString(),
                            numberOfVisits = reader["numberOfVisit"].ToString()
                        });
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }

            return logs;
        }
        #endregion
    }
}
