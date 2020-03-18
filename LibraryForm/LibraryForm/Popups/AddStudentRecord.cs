using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibraryForm.Validations;
using LibraryForm.Controllers;

namespace LibraryForm.Popups
{
    public partial class AddStudentRecord : Form
    {
        public AddStudentRecord()
        {
            InitializeComponent();
            PopulateCourseBox();
        }

        #region Form Methods Used
        //=====================================
        //
        // <Sumarry>
        //      Method to Add Student records
        //
        //=====================================
        private void AddStudent()
        {
            string studentNumber = StudentNumber.Text;
            string lastName = LastName.Text;
            string firstName = FirstName.Text;
            string middleName = MiddleName.Text;
            int gender = GetGender();
            string course = "";
            if(Courses.SelectedItem != null)
            {
                course = Courses.SelectedItem.ToString();
            }
            
            // For the contacts field
            string phone = PhoneNumber.Text;
            string email = EmailAddress.Text;
            if(AddStudentValidations.Validate(studentNumber, firstName, lastName, middleName, gender, course))
            {
                StudentsController.AddStudentRecord(studentNumber, lastName, firstName, middleName, gender, course, phone, email);
                this.DialogResult = DialogResult.OK;
            }

        }


        //=====================================
        //
        // <Sumarry>
        //      Method for converting 
        //  item from gender combobox into
        //  an integer value, for the gender 
        //  column in database is in bit 
        //  format
        //
        //=====================================
        private int GetGender()
        {
            string genderString = "";

            if (Gender.SelectedItem != null)
            {
                genderString = Gender.SelectedItem.ToString();
            }

            int g = -1;

            int.TryParse(genderString, out g);

            return g;
        }


        //=====================================
        //
        // <Sumarry>
        //      Method to populate course
        //  Combobox
        //
        //=====================================
        private void PopulateCourseBox()
        {
            List<Course> courses = StudentsController.GetCourses();

            Courses.Items.Clear();

            foreach(Course course in courses)
            {
                Courses.Items.Add(course.courseCode);
            }
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            AddStudent();
        }

    }
}
