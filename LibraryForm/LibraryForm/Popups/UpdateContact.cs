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
    public partial class UpdateContact : Form
    {
        public UpdateContact()
        {
            InitializeComponent();
        }

        public UpdateContact(string studentNumber)
        {
            InitializeComponent();
            FillStudentInfo(studentNumber);
        }

        #region Form Methods

        //=========================================
        //  <Summary>
        //      Method for filling the
        //      text boxes with student
        //      current saved information
        //      in the database
        //  </Summary>
        //=========================================
        private void FillStudentInfo(string studentNumber)
        {
            StudentNumber.Text = studentNumber;
            List<StudentInfo> infos = StudentsController.GetStudentInformations(studentNumber);
            
            foreach(StudentInfo info in infos)
            {
                StudentPhone.Text = info.phone;
                StudentEmail.Text = info.email;
            }
        }


        //===========================================
        //  <Summary>
        //      Method for Updating student contact
        //      Informations
        //  </Summary>
        //===========================================
        private void SaveStudentContact()
        {
            string studentNumber = StudentNumber.Text;
            string studentPhone = StudentPhone.Text;
            string studentEmail = StudentEmail.Text;

            StudentsController.UpdateStudentContact(studentNumber, studentPhone, studentEmail);
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveStudentContact();
        }
    }
}
