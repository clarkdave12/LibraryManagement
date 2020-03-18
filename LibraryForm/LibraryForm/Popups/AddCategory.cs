using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibraryForm.Controllers;

namespace LibraryForm.Popups
{
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        #region Form Methods

        //================================
        //  <Summary>
        //      Method for saving new
        //      Book Category in the 
        //      Database
        //  </Summary>
        //================================
        private void AddBookCategory()
        {
            string categoryName = CategoryName.Text;
            if(categoryName != "")
            {
                BooksController.AddBookCategory(categoryName);
                this.DialogResult = DialogResult.OK;
            }
        }

        #endregion


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddBookCategory();
        }

    }
}
