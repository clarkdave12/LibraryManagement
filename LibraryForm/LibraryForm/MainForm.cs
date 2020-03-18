using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibraryForm.Views;

namespace LibraryForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        #region UI Methods
        // All Methods UI controls in this form
        
        // Method For Showing Students View
        private void ShowStudentsView()
        {
            if (!MainPanel.Controls.Contains(StudentsView.Instance))
            {
                MainPanel.Controls.Add(StudentsView.Instance);
                StudentsView.Instance.Dock = DockStyle.Fill;
                StudentsView.Instance.BringToFront();
            }
            else
            {
                StudentsView.Instance.BringToFront();
            }
        }

        // Method for showing Books View
        private void ShowBooksView()
        {
            if(!MainPanel.Controls.Contains(BooksView.Instance))
            {
                MainPanel.Controls.Add(BooksView.Instance);
                BooksView.Instance.Dock = DockStyle.Fill;
                BooksView.Instance.BringToFront();
            }
            else
            {
                BooksView.Instance.BringToFront();
            }
        }

        // Method for showing Borrow Books View
        private void ShowBorrowBooksView()
        {
            if(!MainPanel.Controls.Contains(BorrowBook.Instance))
            {
                MainPanel.Controls.Add(BorrowBook.Instance);
                BorrowBook.Instance.Dock = DockStyle.Fill;
                BorrowBook.Instance.BringToFront();
            }
            else
            {
                BorrowBook.Instance.BringToFront();
            }
        }

        // Method for showing Logs View
        private void ShowLogsView()
        {
            if(!MainPanel.Controls.Contains(LogsView.Instance))
            {
                MainPanel.Controls.Add(LogsView.Instance);
                LogsView.Instance.Dock = DockStyle.Fill;
                LogsView.Instance.BringToFront();
            }
            else
            {
                LogsView.Instance.BringToFront();
            }
        }

        // Method for showing Logs Record
        private void ShowLogsRecordView()
        {
            if(!MainPanel.Controls.Contains(LogsRecord.Instance))
            {
                MainPanel.Controls.Add(LogsRecord.Instance);
                LogsRecord.Instance.Dock = DockStyle.Fill;
                LogsRecord.Instance.BringToFront();
            }
            else
            {
                LogsRecord.Instance.BringToFront();
            }
        }

        #endregion

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowLogsView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowLogsRecordView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowBooksView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowBorrowBooksView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowStudentsView();
        }
    }
}
