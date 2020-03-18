using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

using LibraryForm.Validations;

namespace LibraryForm.Controllers
{
    class StudentsController
    {
        //=====================================================
        // <Summary>
        //      Method for adding student's record
        //  then save it in the database
        //=====================================================
        #region AddStudentRecord(string studentNumber, string lastName, string firstName, string middleInitials, int gender, string courseCode, string phone, string email)
        public static void AddStudentRecord(string studentNumber, string lastName, string firstName, string middleInitials, int gender, string courseCode, string phone, string email)
        {
            Connection.OpenConnection();
            
            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Add_Students_StudentsInfo";

                cmd.Parameters.AddWithValue("@studentId", SqlDbType.VarChar).Value = studentNumber;
                cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = lastName;
                cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = firstName;
                cmd.Parameters.AddWithValue("@middleInitials", SqlDbType.VarChar).Value = middleInitials;
                cmd.Parameters.AddWithValue("@gender", SqlDbType.Bit).Value = gender;
                cmd.Parameters.AddWithValue("@courseCode", SqlDbType.VarChar).Value = courseCode;
                cmd.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = phone;
                cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    if(AddStudentValidations.Validate(studentNumber, firstName, lastName, middleInitials, gender, courseCode))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student's record added successfully");
                    }
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
        //  <Summary>
        //      Returns all courses saved in database
        //  returns List<Course> data type
        //=======================================================
        #region GetCourses()
        public static List<Course> GetCourses()
        {
            List<Course> course = new List<Course>();

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandText = "GetCourses";
                Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        course.Add(new Course()
                        {
                            courseCode = reader[0].ToString(),
                            courseName = reader[1].ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }

            return course;
        }
        #endregion

        //=====================================================
        // <Summary>
        //      Method for getting all the student
        //  records that is in the database
        //      returns List<Student> data type
        //=====================================================
        #region GetStudents()
        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetStudentRecords";


                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        students.Add(new Student() { 
                            studentNumber = reader["student_id"].ToString(),
                            lastName = reader["last_name"].ToString(),
                            firstName = reader["first_name"].ToString(),
                            middleInitials = reader["middle_initials"].ToString(),
                            course = reader["course_code"].ToString(),
                            gender = reader["gender"].ToString()
                        });
                    }
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


        //=====================================================
        // <Summary>
        //      Method for searching student records
        //  using student number or student's name
        //      returns List<Student> data type
        //=====================================================
        #region SearchStudent(string q)
        public static List<Student> SearchStudent(string q)
        {
            List<Student> students = new List<Student>();

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SearchStudent";

                cmd.Parameters.AddWithValue("@q", SqlDbType.VarChar).Value = q;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        students.Add(new Student() { 
                            studentNumber = reader["student_id"].ToString(),
                            lastName = reader["last_name"].ToString(),
                            firstName = reader["first_name"].ToString(),
                            middleInitials = reader["middle_initials"].ToString(),
                            gender = reader["gender"].ToString(),
                            course = reader["course_code"].ToString(),
                            offenses = reader["offenses"].ToString()
                        });
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            Connection.conn.Close();
            return students;
        }
        #endregion


        //=====================================================
        // <Summary>
        //      Method for Updating an existing student's
        //  record in the database
        //=====================================================
        #region UpdateStudent(string studentNumber, string lastName, string firstName, string mi, string course, string gender)
        public static void UpdateStudent(string studentNumber, string lastName, string firstName, string mi, string course, string gender)
        {
            // converting gender int bit
            int g = 0;
            if (gender.ToLower() == "male")
                g = 1;
            else
                g = 0;

            // updating formula
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateStudent";

                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;
                cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = lastName;
                cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = firstName;
                cmd.Parameters.AddWithValue("@middleInitials", SqlDbType.VarChar).Value = mi;
                cmd.Parameters.AddWithValue("@courseCode", SqlDbType.VarChar).Value = course;
                cmd.Parameters.AddWithValue("@gender", SqlDbType.Bit).Value = g;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Record Updated Successfully");

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Connection.conn.Close();
            }

        }
        #endregion


        //=====================================================
        // <Summary>
        //      Method for Updating the student contacts
        //  and save in the database
        //=====================================================
        #region UpdateStudentContact(string studentNumber, string phone, string email)
        public static void UpdateStudentContact(string studentNumber, string phone, string email)
        {
            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateStudentContact";

                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;
                cmd.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = phone;
                cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Contact Updated Successfully");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion


        //=====================================================
        // <Summary>
        //      Method for fetching student informations
        //  returns List<StudentInfo> data type
        //=====================================================
        #region GetStudentInformations(string studentNumber)
        public static List<StudentInfo> GetStudentInformations(string studentNumber)
        {
            List<StudentInfo> studentInfo = new List<StudentInfo>();

            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetStudentInformations";
                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                string gen = "";

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                   
                    if(reader.Read())
                    {
                        if (Convert.ToInt32(reader[3]) == 0)
                            gen = "Female";
                        else
                            gen = "Male";
                        
                        studentInfo.Add(new StudentInfo() { 
                            course = reader[0].ToString(),
                            phone = reader[1].ToString(),
                            email = reader[2].ToString(),
                            gender = gen,
                            numberOfVisit = reader[4].ToString(),
                            lastVisit = reader[5].ToString()
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

            return studentInfo;
        }
        #endregion


        //=====================================================
        //  <Summary>
        //      Method for deleting an existing student
        //  record in the database
        //=====================================================
        #region DeleteStudent(string studentNumber)
        public static void DeleteStudent(string studentNumber)
        {
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteStudent";

                cmd.Parameters.AddWithValue("@studentNumber", SqlDbType.VarChar).Value = studentNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Record Deleted Successfully");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion

    }
}
