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

namespace LibraryForm.Views
{
    public partial class StudentsView : UserControl
    {
        // Singleton Instance
        private static StudentsView instance;

        public static StudentsView Instance
        {
            get
            {
                if (instance == null)
                    instance = new StudentsView();
                return instance;
            }
        }

        public StudentsView()
        {
            InitializeComponent();
            GetStudentRecords();
            GetStudentContact();
        }


        //=======================================
        //
        // <Summary>
        //      Method for Show the Add Student
        //  Popup form
        //
        //=======================================
        private void ShowAddStudentModal()
        {
            AddStudentRecord asr = new AddStudentRecord();

            if (asr.ShowDialog() == DialogResult.OK)
            {
                GetStudentRecords();
            }
        }


        //=======================================
        //
        // <Summary>
        //      Method for Showing the Update
        //  Contact Popup form
        //
        //=======================================
        private void ShowUpdateContact()
        {
            string studentNumber = "";

            if (StudentRecords.CurrentRow.Index >= 0 && StudentRecords.CurrentRow.Cells[0] != null)
            {
                studentNumber = StudentRecords.CurrentRow.Cells[0].Value.ToString();
                UpdateContact usc = new UpdateContact(studentNumber);
                if (usc.ShowDialog() == DialogResult.OK)
                {
                    GetStudentContact();
                }
            }
        }


        //=======================================
        //
        // <Summary>
        // Code for fetching All Student records
        //
        //=======================================
        private void GetStudentRecords()
        {
            List<Student> students = StudentsController.GetStudents();
            int r = 0;
            StudentRecords.Rows.Clear();
            foreach(Student student in students)
            {
                StudentRecords.Rows.Add();
                StudentRecords.Rows[r].Cells[0].Value = student.studentNumber;
                StudentRecords.Rows[r].Cells[1].Value = student.lastName;
                StudentRecords.Rows[r].Cells[2].Value = student.firstName;
                StudentRecords.Rows[r].Cells[3].Value = student.middleInitials;
                r++;
            }
        }


        //===========================================
        //
        // <Summary>
        // Method for searching Student Records
        //
        //===========================================
        private void GetStudentRecords(string search)
        {
            List<Student> students = StudentsController.SearchStudent(search);
            int r = 0;
            StudentRecords.Rows.Clear();
            foreach (Student student in students)
            {
                StudentRecords.Rows.Add();
                StudentRecords.Rows[r].Cells[0].Value = student.studentNumber;
                StudentRecords.Rows[r].Cells[1].Value = student.lastName;
                StudentRecords.Rows[r].Cells[2].Value = student.firstName;
                StudentRecords.Rows[r].Cells[3].Value = student.middleInitials;
                r++;
            }
        }


        //===========================================
        //
        // <Summary>
        //      Method for getting the selected
        //  student contact informations
        //
        //===========================================
        private void GetStudentContact()
        {

            string studentNumber = "";
            if (StudentRecords.SelectedRows.Count > 0 && StudentRecords.CurrentRow.Cells[0] != null)
            {
                studentNumber = StudentRecords.CurrentRow.Cells[0].Value.ToString();
            }

            List<StudentInfo> studentinfos = StudentsController.GetStudentInformations(studentNumber);

            foreach(StudentInfo studentinfo in studentinfos)
            {
                if(studentinfo.phone != "")
                {
                    PhoneLabel.Text = studentinfo.phone;
                }
                else
                {
                    PhoneLabel.Text = "No Saved Phone number";
                }

                if(studentinfo.email != "")
                {
                    EmailLabel.Text = studentinfo.email;
                }
                else
                {
                    EmailLabel.Text = "No Saved Email Address";
                }

                if(studentinfo.lastVisit != "")
                {
                    NumberOfVisitLabel.Text = studentinfo.lastVisit;
                }
                else
                {
                    NumberOfVisitLabel.Text = "No visit yet";
                }

                GenderLabel.Text = studentinfo.gender;
                CourseLabel.Text = studentinfo.course;
                NumberOfVisitLabel.Text = studentinfo.numberOfVisit;
            }

        }


        //===========================================
        //
        // <Summary>
        //      Method for Deleting a Student
        //  Record
        //
        //===========================================
        private void DeleteStudent()
        {
            string studentNumber = StudentRecords.CurrentRow.Cells[0].Value.ToString();
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure to delete this record ? ", "Warning", MessageBoxButtons.YesNoCancel);
           
            if(result == DialogResult.Yes)
            {
                StudentsController.DeleteStudent(studentNumber);
                GetStudentRecords();
            }
        }

        //===========================================
        //  <Summary>
        //      Method for updating student record
        //  </Summary>
        //===========================================
        private void UpdateStudentRecord()
        {
            string studentNumber = "";
            string lastName = "";
            string firstName = "";
            string middleName = "";
            string gender = GenderLabel.Text;
            string course = CourseLabel.Text;
            string phone = PhoneLabel.Text;
            string email = EmailLabel.Text;

            if (StudentRecords.SelectedRows.Count > 0)
            {
                studentNumber = StudentRecords.CurrentRow.Cells[0].Value.ToString();
                lastName = StudentRecords.CurrentRow.Cells[1].Value.ToString();
                firstName = StudentRecords.CurrentRow.Cells[2].Value.ToString();
                middleName = StudentRecords.CurrentRow.Cells[3].Value.ToString();
            }

            UpdateStudentRecord usr = new UpdateStudentRecord(studentNumber, lastName, firstName, middleName, gender, course, phone, email);
            if (usr.ShowDialog() == DialogResult.OK)
            {
                GetStudentRecords();
                GetStudentContact();
            }
        }


        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            string search = SearchBox.Text;
            if(search != "")
            {
                GetStudentRecords(search); 
            }
            else
            {
                GetStudentRecords();
            }
        }

        private void StudentRecords_MouseClick(object sender, MouseEventArgs e)
        {
            if(StudentRecords.SelectedRows.Count > 0)
            {
                GetStudentContact();
            }
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string search = SearchBox.Text;
            if (search != "")
            {
                GetStudentRecords(search);
            }
            else
            {
                GetStudentRecords();
            }
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            string search = SearchBox.Text;
            if (search != "")
            {
                GetStudentRecords(search);
            }
            else
            {
                GetStudentRecords();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowUpdateContact();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateStudentRecord();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAddStudentModal();
        }

        private void SearchBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            string q = SearchBox.Text;
            GetStudentRecords(q);
        }

        private void SearchBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            string q = SearchBox.Text;
            GetStudentRecords(q);
        }

        private void SearchBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            string q = SearchBox.Text;
            GetStudentRecords(q);
        }

        private void StudentRecords_MouseClick_1(object sender, MouseEventArgs e)
        {
            GetStudentContact();
        }
    }
}
