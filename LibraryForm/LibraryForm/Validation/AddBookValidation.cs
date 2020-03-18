using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace LibraryForm.Validations
{
    class AddBookValidation
    {
        #region Validate(string callNumber, string barCode, string accessionNumber, string title, int category, string year)
        public static bool Validate(string callNumber, string barCode, string accessionNumber, string title, int category, string year)
        {
            if(callNumber == "")
            {
                MessageBox.Show("Call Number is Required");
                return false;
            }
            if(barCode == "")
            {
                MessageBox.Show("Barcode is Required");
                return false;
            }
            if(accessionNumber == "")
            {
                MessageBox.Show("Accession Number is Required");
                return false;
            }
            if(title == "")
            {
                MessageBox.Show("Book Title is Required");
                return false;
            }
            if(year == "")
            {
                MessageBox.Show("Published Year is Required");
                return false;
            }
            if(CheckCallNumber(callNumber))
            {
                MessageBox.Show("The Call Number is already registered");
                return false;
            }
            if(!CheckYear(year))
            {
                MessageBox.Show("Invalid Year Format");
                return false;
            }
            if(category <= 0)
            {
                MessageBox.Show("Invalid Category");
                return false;
            }

            return true;
        }
        #endregion

        private static bool CheckYear(string y)
        {
            int year;

            try
            {
                int.TryParse(y, out year);
            }
            catch(Exception)
            {
                return false;
            }

            if(year <= 999)
            {
                return false;
            }

            return true;
        }

        private static bool CheckCallNumber(string callNumber)
        {
            bool retVal = false;

            Connection.OpenConnection();

            using (SqlCommand cmd = Connection.conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCallNumber";
                cmd.Parameters.AddWithValue("@callNumber", SqlDbType.VarChar).Value = callNumber;

                if (Connection.conn.State == ConnectionState.Closed)
                    Connection.conn.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        int x;
                        int.TryParse(reader[0].ToString(), out x);
                        if(x > 0)
                        {
                            retVal = true;
                        }
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
    }
}
