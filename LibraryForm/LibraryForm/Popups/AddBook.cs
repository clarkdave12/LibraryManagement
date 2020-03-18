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
using LibraryForm.Validations;

namespace LibraryForm.Popups
{
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();

            PopulateCategories();
        }

        #region Form Methods

        //======================================
        //  <Summary>
        //      Method For populating the 
        //      Category combobox using the data
        //      coming from the database
        //  </Summary>
        //======================================
        private void PopulateCategories()
        {
            List<Category> categories = BooksController.GetCategories();
            Categories.Items.Clear();

            foreach(Category category in categories)
            {
                Categories.Items.Add(category.categoryName);    
            }
        }

        
        //======================================
        //  <Summary>
        //      Method for saving inputed book
        //      record to store in the database
        //  </Summary>
        //======================================
        private void SaveBookRecord()
        {
            string callNumber = CallNumber.Text;
            string barCode = BarCode.Text;
            string accessionNumber = AccessionNumber.Text;
            string bookTitle = BookTitle.Text;
            string publishYear = PublishYear.Text;
            string cat = null;

            if(Categories.SelectedItem != null)
            {
                cat = Categories.SelectedItem.ToString();
            }
            int category = BooksController.GetCategoryId(cat);
            string year = PublishYear.Text;

            if(AddBookValidation.Validate(callNumber, barCode, accessionNumber, bookTitle, category, year))
            {
                BooksController.AddBookRecord(callNumber, barCode, accessionNumber, bookTitle, category, year);
                this.DialogResult = DialogResult.OK;
            }

        }


        //======================================
        //  <Summary>
        //      Method for showing Add Category
        //      Modal
        //  </Summary>
        //======================================
        private void ShowAddCategoryModal()
        {
            this.WindowState = FormWindowState.Minimized;
            AddCategory modal = new AddCategory();
            
            if(modal.ShowDialog() == DialogResult.OK)
            {
                this.WindowState = FormWindowState.Normal;
                PopulateCategories();
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        #endregion


        #region Form Events
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveBookRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCategory ac = new AddCategory();
            this.WindowState = FormWindowState.Minimized;
            if (ac.ShowDialog() == DialogResult.OK)
            {
                PopulateCategories();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowAddCategoryModal();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveBookRecord();
        }
        #endregion
    }
}
