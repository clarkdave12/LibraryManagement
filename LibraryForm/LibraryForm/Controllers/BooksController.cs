using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryForm;

namespace LibraryForm.Controllers
{
    class BooksController
    {
        //==================================================
        // <Summary>
        //      Method for borrowing books 
        //  by getting the student's ID number and 
        //  books call number, then save it in the
        //  database
        //==================================================
        #region BorrowBook(string callNumber, string identify, string date)
        public static void BorrowBook(string callNumber, string identify)
        {
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BorrowBook";

                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;
                cmd.Parameters.AddWithValue("@identify", SqlDbType.VarChar).Value = identify;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion


        //==================================================
        // <Summary>
        //      Method for adding a book record then
        //  save it in the database
        //==================================================
        #region AddBookRecord(string callNumber, string barCode, string accessionNumber, string title, int category, string year, List<Authors> authors)
        public static void AddBookRecord(string callNumber, string barCode, string accessionNumber, string title, int category, string year, List<Authors> authors)
        {
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddBookRecord";

                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;
                cmd.Parameters.AddWithValue("@barCode", SqlDbType.VarChar).Value = barCode;
                cmd.Parameters.AddWithValue("@accessionNumber", SqlDbType.VarChar).Value = accessionNumber;
                cmd.Parameters.AddWithValue("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.AddWithValue("@category_id", SqlDbType.Int).Value = category;
                cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = year;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    
                    foreach(Authors author in authors)
                    {
                        AddAuthors(callNumber, author.lastName, author.firstName, author.middleName);
                    }
                    MessageBox.Show("Book Record Added");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion


        //==================================================
        //  <Summary>
        //      Method for adding book used in
        //  the popups menu
        //==================================================
        #region AddBookRecord(string callNumber, string barCode, string accessionNumber, string title, int category, string year)
        public static void AddBookRecord(string callNumber, string barCode, string accessionNumber, string title, int category, string year)
        {
            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddBookRecord";

                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;
                cmd.Parameters.AddWithValue("@barCode", SqlDbType.VarChar).Value = barCode;
                cmd.Parameters.AddWithValue("@accessionNumber", SqlDbType.VarChar).Value = accessionNumber;
                cmd.Parameters.AddWithValue("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.AddWithValue("@category_id", SqlDbType.Int).Value = category;
                cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = year;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Record Added Successfully");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion

        //==================================================
        //  <Summary>
        //      Method for getting the book's category
        //  ID
        //      returns an int data type
        //==================================================
        public static int GetCategoryId(string name)
        {
            int retVal = 0;

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCategoryId";
                cmd.Parameters.AddWithValue("@category_name", SqlDbType.VarChar).Value = name;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        int.TryParse(reader[0].ToString(), out retVal);
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }

            return retVal;
        }


        //==================================================
        //  <Summary>
        //      Method for searching a book
        //  returns List<Book> data type
        //==================================================
        public static List<Book> SearchBook(string search)
        {
            List<Book> books = new List<Book>();

            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SearchBook";
                cmd.Parameters.AddWithValue("@search", SqlDbType.VarChar).Value = search;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        books.Add(new Book() { 
                            callNumber = reader["call_number"].ToString(),
                            barCode = reader["barcode"].ToString(),
                            accessionNumber = reader["accession_number"].ToString(),
                            title = reader["title"].ToString(),
                            publishYear = reader["publish_year"].ToString()
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

            return books;
        }

        //==================================================
        // <Summary>
        //      Method for adding a book's category
        // then save it in the database
        //==================================================
        #region AddBookCategory(string name)
        public static void AddBookCategory(string name)
        {
            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddBookCategory";
                cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = name;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added Successfully");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion


        //==================================================
        // <Summary>
        //      Method for getting all categories 
        //  that is saved in the database
        //      returns List<Category> data type
        //==================================================
        #region GetCategories()
        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCategories";

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    int x;
                    while(reader.Read())
                    {
                        int.TryParse(reader["category_id"].ToString(), out x);

                        categories.Add(new Category() { 
                            categoryId = x,
                            categoryName = reader["name"].ToString()
                        });
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            Connection.conn.Close();

            return categories;
        }
        #endregion


        //==================================================
        // <Summary>
        //      Method for getting all the book records
        //  that is saved in the database 
        //  pass empty string in payload to return all
        //  book records in the database
        //  OR pass book call number to get specific 
        //  book record
        //      returns List<Book> data type
        //==================================================
        #region GetBooks(string payload)
        public static List<Book> GetBooks(string payload)
        {
            List<Book> books = new List<Book>();

            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetBookRecords";
                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = payload;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        books.Add(new Book() {
                            callNumber = reader["call_number"].ToString(),
                            title = reader["title"].ToString(),
                            publishYear = reader["publish_year"].ToString()
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

            return books;
        }
        #endregion


        //==================================================
        //  <Summary>
        //      Method for getting the author(s)
        //  of the book using the book's call number as
        //  identifier
        //      returns List<Authors> data type
        //==================================================
        #region GetAuthorOfBook(string callNumber)
        public static List<Authors> GetAuthorOfBook(string callNumber)
        {
            List<Authors> authors = new List<Authors>();

            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAuthorOfBook";
                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        authors.Add(new Authors() { 
                            firstName = reader["first_name"].ToString(),
                            lastName = reader["last_name"].ToString(),
                            middleName = reader["middle_name"].ToString()
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

            return authors;
        }
        #endregion


        //==================================================
        // <Summary>
        //      Method for adding the author(s) of
        //  the book
        //==================================================
        #region AddAuthors(string callNumber, string lastName, string firstName, string middleName)
        private static void AddAuthors(string callNumber, string lastName, string firstName, string middleName)
        {
            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddAuthors";

                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;
                cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = lastName;
                cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = firstName;
                cmd.Parameters.AddWithValue("@middleName", SqlDbType.VarChar).Value = middleName;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Connection.conn.Close();
            }
        }
        #endregion


        //==================================================
        // <Summary>
        //      Method for deleting a book record
        //  that is saved in the database
        //==================================================
        #region DeleteBookRecord(string callNumber)
        public static void DeleteBookRecord(string callNumber)
        {
            Connection.OpenConnection();

            using(SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteBookRecord";
                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Record Deleted");
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
