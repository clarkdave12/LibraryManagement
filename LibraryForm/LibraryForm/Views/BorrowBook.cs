using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibraryForm.Popups;
using LibraryForm.Controllers;

namespace LibraryForm.Views
{
    public partial class BorrowBook : UserControl
    {
        // Singleton Instance
        private static BorrowBook instance;

        public static BorrowBook Instance
        {
            get
            {
                if (instance == null)
                    instance = new BorrowBook();
                return instance;
            }
        }

        public BorrowBook()
        {
            InitializeComponent();
            SearchForStudent();
            SearchForBook();
        }

        #region Form Methods
        //=================================
        //  <Summary>
        //      Method for Searching 
        //      Student Record
        //  </Summary>
        //=================================
        private void SearchForStudent()
        {
            string search = SearchStudent.Text;
            List<Student> students = StudentsController.SearchStudent(search);

            if(search != "")
            {
                if(students.Count > 0)
                {
                    foreach (Student student in students)
                    {
                        List<StudentInfo> infos = StudentsController.GetStudentInformations(student.studentNumber);

                        if (student.studentNumber != "")
                        {
                            StudentNumber.Text = student.studentNumber;
                        }
                        else
                        {
                            StudentNumber.Text = "No Record";
                        }

                        if (student.lastName != "" && student.firstName != "" && student.middleInitials != "")
                        {
                            StudentName.Text = student.lastName + ", " + student.firstName + " " + student.middleInitials;
                        }
                        else
                        {
                            StudentName.Text = "No Record";
                        }

                        if (student.lastVisit != "")
                        {
                            LastVisit.Text = student.lastVisit;
                        }
                        else
                        {
                            LastVisit.Text = "No Record";
                        }

                        if (student.numberOfVisits != "")
                        {
                            NumberOfVisit.Text = student.numberOfVisits;
                        }
                        else
                        {
                            NumberOfVisit.Text = "No Record";
                        }

                        foreach (StudentInfo info in infos)
                        {
                            if (info.phone != "")
                            {
                                PhoneNumber.Text = info.phone;
                            }
                            else
                            {
                                PhoneNumber.Text = "No Saved Phone";
                            }

                            if (info.email != "")
                            {
                                EmailAddress.Text = info.email;
                            }
                            else
                            {
                                EmailAddress.Text = "No Saved Email";
                            }

                            break;
                        }
                        break;
                    }
                }
                else
                {
                    StudentNumber.Text = "No Record";
                    StudentName.Text = "No Record";
                    LastVisit.Text = "No Record";
                    NumberOfVisit.Text = "No Record";
                    PhoneNumber.Text = "No Record";
                    EmailAddress.Text = "No Record";
                }
            }
            else
            {
                StudentNumber.Text = "";
                StudentName.Text = "";
                LastVisit.Text = "";
                NumberOfVisit.Text = "";
                PhoneNumber.Text = "";
                EmailAddress.Text = "";
            }
        }


        //=================================
        //  <Summary>
        //      Method for Searching 
        //      Book Record
        //  </Summary>
        //=================================
        private void SearchForBook()
        {
            string search = SearchBook.Text;
            List<Book> books = BooksController.SearchBook(search);
            if(search != "")
            {
                if(books.Count > 0)
                {
                    foreach(Book book in books)
                    {
                        CallNumber.Text = book.callNumber;
                        BarCode.Text = book.barCode;
                        AccessionNumber.Text = book.accessionNumber;
                        BookTitle.Text = book.title;
                        PublishYear.Text = book.publishYear;
                    }
                }
                else
                {
                    CallNumber.Text = "No Record";
                    BarCode.Text = "No Record";
                    AccessionNumber.Text = "No Record";
                    BookTitle.Text = "No Record";
                    PublishYear.Text = "No Record";
                }
            }
            else
            {
                CallNumber.Text = "";
                BarCode.Text = "";
                AccessionNumber.Text = "";
                BookTitle.Text = "";
                PublishYear.Text = "";
            }
        }


        //================================
        //  <Summary>
        //      Method for Showing
        //      Add Student Record Modal
        //  </Summary>
        //================================
        private void ShowAddStudentModal()
        {
            AddStudentRecord modal = new AddStudentRecord();
            modal.ShowDialog();
        }


        //================================
        //  <Summary>
        //      Method for showing 
        //      Add Book Record Modal
        //  </Summary>
        //================================
        private void ShowAddBookModal()
        {
            AddBook modal = new AddBook();
            modal.ShowDialog();
        }


        //================================
        //  <Summary>
        //      Method for showing
        //      Update Contact Modal
        //  </Summary>
        //================================
        private void ShowUpdateContactModal()
        {
            
            string studentNumber = StudentNumber.Text;
            if (studentNumber == "No Record")
                studentNumber = "";

            if(studentNumber != "")
            {
                UpdateContact modal = new UpdateContact(studentNumber);
                modal.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Search For student before updating the contact");
            }
        }

        #endregion


        #region Form Events
        private void SearchStudent_KeyDown(object sender, KeyEventArgs e)
        {
            SearchForStudent();
        }

        private void SearchStudent_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchForStudent();
        }

        private void SearchStudent_KeyUp(object sender, KeyEventArgs e)
        {
            SearchForStudent();
        }

        private void SearchBook_KeyDown(object sender, KeyEventArgs e)
        {
            SearchForBook();
        }

        private void SearchBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchForBook();
        }

        private void SearchBook_KeyUp(object sender, KeyEventArgs e)
        {
            SearchForBook();
        }

        private void SearchStudent_KeyDown_1(object sender, KeyEventArgs e)
        {
            SearchForStudent();
        }

        private void SearchStudent_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            SearchForStudent();
        }

        private void SearchStudent_KeyUp_1(object sender, KeyEventArgs e)
        {
            SearchForStudent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchForBook();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            SearchForBook();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SearchForBook();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAddStudentModal();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowAddBookModal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowUpdateContactModal();
        }

    }
}
