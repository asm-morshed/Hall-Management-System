using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using StudentInformation.DAL.Gateway;
using StudentInformation.DAL.DAO;
using StudentInformation.BLL;

namespace StudentInformation.UI
{
    public partial class SearchStudent : Form
    {
        
        private StudentManager studentManager = new StudentManager();
        
        public SearchStudent()
        {
            InitializeComponent();
        }
        private string searchingRoom = "";
        
        private void SearchButton_Click(object sender, EventArgs e)
        {
            searchingRoom = txtSearchRoom.Text;
            loadStudentsList(searchingRoom);
            groupBox2.Visible = true;
  
        }

        public void loadStudentsList(string searchingRoom)
        {
            List<Student> students;
            studentListView.Items.Clear();
            try
            {
                int serialNo=1;
                students = studentManager.GetAllStudents(searchingRoom);
                foreach (Student student in students)
                {
                    ListViewItem anItem = new ListViewItem(serialNo.ToString());
                    anItem.Tag = (Student) student;
                    anItem.SubItems.Add(student.StudentId);
                    anItem.SubItems.Add(student.Name);
                    anItem.SubItems.Add(student.Department);
                    anItem.SubItems.Add(student.Session);
                    anItem.SubItems.Add(student.HallName);
                    anItem.SubItems.Add(student.Email);
                    anItem.SubItems.Add(student.RoomPosition);
                    //anItem.SubItems.Add(student.RoomNo);
                    anItem.SubItems.Add(student.Fathername);
                    anItem.SubItems.Add(student.Address);
                    anItem.SubItems.Add(student.School);
                    anItem.SubItems.Add(student.College);
                    anItem.SubItems.Add(student.District);
                    studentListView.Items.Add(anItem);
                    serialNo++;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student selectedStudent = GetSelectedStudent();
            if (selectedStudent != null)
            {
                //StudentInfo studentInfoUI = new StudentInfo(selectedStudent);
                StudentInfo studentInfoUI = new StudentInfo(selectedStudent);
                studentInfoUI.ShowDialog();
                //loadStudentsList(searchingRoom);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "";
            Student selectedStudent = GetSelectedStudent();
            int selectedIndex = studentListView.SelectedIndices[0];
            DialogResult result = MessageBox.Show("You are going to delete "+selectedStudent.Name+" \nAre you sure?",
                "Delete Student",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    message = studentManager.DelteStudent(selectedStudent);
                    studentListView.Items.RemoveAt(selectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Error!!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show(message);
            }
        }
        private Student GetSelectedStudent()
        {
            ListViewItem item = studentListView.SelectedItems[0];
            return (Student)item.Tag;
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        
       
    }
}
