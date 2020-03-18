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
    public partial class LogsView : UserControl
    {
        // Singleton Instance
        private static LogsView instance;

        public static LogsView Instance
        {
            get
            {
                if (instance == null)
                    instance = new LogsView();
                return instance;
            }
        }

        public LogsView()
        {
            InitializeComponent();
        }

        #region Form Methods

        //=================================
        //  <Summary>
        //      Method for Searching 
        //      Student Record
        //  </Summary>
        //=================================
        private void SearchStudent()
        {
            string q = SearchStudentBox.Text;
            List<Student> students = StudentsController.SearchStudent(q);

            if(students.Count > 0)
            {
                foreach(Student student in students)
                {
                    StudentNumber.Text = student.studentNumber;
                    StudentName.Text = string.Format("{0}, {1} {2}", student.lastName, student.firstName, student.middleInitials);
                    StudentCourse.Text = student.course;
                    LastVisit.Text = student.lastVisit;
                    NumberOfVisit.Text = student.numberOfVisits;
                    Offenses.Text = student.offenses;
                }
            }
            else
            {
                StudentNumber.Text = "No Record";
                StudentName.Text = "No Record";
                StudentCourse.Text = "No Record";
                LastVisit.Text = "No Record";
                NumberOfVisit.Text = "No Record";
                Offenses.Text = "No Record";
            }
        }


        //=================================
        //  <Summary>
        //      Method for logging in 
        //      Student with ID
        //  </Summary>
        //=================================
        private void LogStudentWithID()
        {
            string studentNumber = StudentNumber.Text;
            if (studentNumber == "No Record")
                studentNumber = "";

            if(studentNumber != "")
            {
                LogsController.AddLogs(studentNumber, 0);
            }
            else
            {
                MessageBox.Show("No Student Record");
            }
        }

        //=================================
        //  <Summary>
        //      Method for logging in 
        //      Student without ID
        //  </Summary>
        //=================================
        private void LogStudentWithoutID()
        {
            string studentNumber = StudentNumber.Text;
            if (studentNumber == "No Record")
                studentNumber = "";

            if (studentNumber != "")
            {
                LogsController.AddLogs(studentNumber, 1);
            }
            else
            {
                MessageBox.Show("No Student Record");
            }
        }


        //=======================================
        //  <Summary>
        //      Method for showing Add Student
        //      Modal
        //  </Summary>
        //=======================================
        private void ShowAddStudentModal()
        {
            AddStudentRecord modal = new AddStudentRecord();
            modal.ShowDialog();
        }


        //=======================================
        //  <Summary>
        //      Method for showing Others
        //      Log Entry Modal
        //  </Summary>
        //=======================================
        private void ShowOthersEntryModal()
        {
            OthersEntry modal = new OthersEntry();
            modal.ShowDialog();
        }

        #endregion

        private void SearchStudentBox_KeyDown(object sender, KeyEventArgs e)
        {
            SearchStudent();
        }
        
        private void SearchStudentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchStudent();
        }

        private void SearchStudentBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchStudent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogStudentWithID();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogStudentWithoutID();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowAddStudentModal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowOthersEntryModal();
        }

    }
}
