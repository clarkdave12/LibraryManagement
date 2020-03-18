using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibraryForm.Controllers;
using LibraryForm.Popups;
using LibraryForm;

namespace LibraryForm.Views
{
    public partial class BooksView : UserControl
    {
        // Singleton instanciation
        private static BooksView instance;

        public static BooksView Instance
        {
            get
            {
                if (instance == null)
                    instance = new BooksView();
                return instance;
            }
        }

        public BooksView()
        {
            InitializeComponent();
            GetBookRecords();
        }

        //==================================================
        //  <Summary>
        //          Method for getting all
        //      the book records and display
        //      it in the BookRecords
        //      DataGridView
        //  </Summary>
        //==================================================
        private void GetBookRecords()
        {
            List<Book> books = BooksController.GetBooks("");
            BookRecords.Rows.Clear();
            int r = 0;

            foreach(Book book in books)
            {
                BookRecords.Rows.Add();
                BookRecords.Rows[r].Cells[0].Value = book.callNumber;
                BookRecords.Rows[r].Cells[1].Value = book.title;
                BookRecords.Rows[r].Cells[2].Value = book.publishYear;
                r++;
            }
        }
        
        //=======================================
        //  <Summary>
        //      This is an Overload Method
        //  </Summary>
        //=======================================
        private void GetBookRecords(string q)
        {
            List<Book> books = BooksController.GetBooks(q);
            BookRecords.Rows.Clear();
            int r = 0;

            foreach (Book book in books)
            {
                BookRecords.Rows.Add();
                BookRecords.Rows[r].Cells[0].Value = book.callNumber;
                BookRecords.Rows[r].Cells[1].Value = book.title;
                BookRecords.Rows[r].Cells[2].Value = book.publishYear;
                r++;
            }
        }

        
        //=======================================
        //  <Summary>
        //      Method for deleting book record
        //  </Summary>
        //=======================================
        private void DeleteBookRecord()
        {
            if(BookRecords.SelectedRows.Count > 0 && BookRecords.RowCount > 0)
            {
                string callNumber = BookRecords.CurrentRow.Cells[0].Value.ToString();
                
                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure to delete this record ?", "Warning", MessageBoxButtons.YesNoCancel);
                if(result == DialogResult.Yes)
                {
                    BooksController.DeleteBookRecord(callNumber);
                    GetBookRecords();
                }
            }
        }


        //======================================
        //  <Summary>
        //      Method for Showing the Add
        //      Book Record
        //  </Summary>
        //======================================
        private void ShowAddBookRecord()
        {
            AddBook dialog = new AddBook();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                GetBookRecords();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            string search = SearchBook.Text;
            
            if(search != "")
            {
                GetBookRecords(search);
            }
            else
            {
                GetBookRecords();
            }
        }

        private void SearchBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            string search = SearchBook.Text;

            if (search != "")
            {
                GetBookRecords(search);
            }
            else
            {
                GetBookRecords();
            }
        }

        private void SearchBook_KeyUp(object sender, KeyEventArgs e)
        {
            string search = SearchBook.Text;

            if (search != "")
            {
                GetBookRecords(search);
            }
            else
            {
                GetBookRecords();
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            AddBook ab = new AddBook();
            if(ab.ShowDialog() == DialogResult.OK)
            {
                GetBookRecords();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            DeleteBookRecord();
        }

        private void SearchBook_KeyDown(object sender, KeyEventArgs e)
        {
            string q = SearchBook.Text;
            GetBookRecords(q);
        }

        private void SearchBook_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            string q = SearchBook.Text;
            GetBookRecords(q);
        }

        private void SearchBook_KeyUp_1(object sender, KeyEventArgs e)
        {
            string q = SearchBook.Text;
            GetBookRecords(q);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAddBookRecord();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteBookRecord();
        }
    }
}
