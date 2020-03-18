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
    public partial class UpdateStudentRecord : Form
    {
        public UpdateStudentRecord()
        {
            InitializeComponent();
        }


        //=======================================
        //
        // <Summary>
        //      Overload the constructor to 
        //  get the data from previous form
        //
        //=======================================
        public UpdateStudentRecord(string studentNumber, string lastName, string firstName, string middleName, string gender, string course, string phone, string email)
        {
            InitializeComponent();

            PopulateCourses();

            StudentNumber.Text = studentNumber;
            LastName.Text = lastName;
            FirstName.Text = firstName;
            MiddleName.Text = middleName;
            Gender.SelectedItem = gender;
            Courses.SelectedItem = course;
            FillStudentInfo(studentNumber);
        }

        
        //=======================================
        //  <Summary>
        //      Method for getting the student
        //      contact informations
        //  </Summary>
        //=======================================
        private void FillStudentInfo(string studentNumber)
        {
            StudentNumber.Text = studentNumber;
            List<StudentInfo> infos = StudentsController.GetStudentInformations(studentNumber);

            foreach (StudentInfo info in infos)
            {
                PhoneNumber.Text = info.phone;
                EmailAddress.Text = info.email;
            }
        }

        //=======================================
        //
        // <Summary>
        //      Populate the courses combobox
        //
        //=======================================
        private void PopulateCourses()
        {
            List<Course> courses = StudentsController.GetCourses();

            Courses.Items.Clear();

            foreach (Course course in courses)
            {
                Courses.Items.Add(course.courseCode);
            }
        }


        //=======================================
        //
        // <Summary>
        //      Method for updating the student
        //  record
        //
        //=======================================
        private void UpdateStudent()
        {
            string studentNumber = StudentNumber.Text;
            string lastName = LastName.Text;
            string firstName = FirstName.Text;
            string middleName = MiddleName.Text;

            int gender = -1;
            string g = Gender.SelectedItem.ToString();

            if ("Male" == g)
                gender = 1;
            else
                gender = 0;

            string course = Courses.SelectedItem.ToString();
            string phone = PhoneNumber.Text;
            string email = EmailAddress.Text;

            if (AddStudentValidations.Validate(firstName, lastName, middleName, gender, course))
            {
                StudentsController.UpdateStudent(studentNumber, lastName, firstName, middleName, course, g);
                StudentsController.UpdateStudentContact(studentNumber, phone, email);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            UpdateStudent();
        }
    }
}
