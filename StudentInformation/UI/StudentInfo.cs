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
using StudentInformation.DAL.DAO;
using StudentInformation.BLL;

namespace StudentInformation.UI
{
    public partial class StudentInfo : Form
    {
        private StudentManager studentManager = new StudentManager();
        public StudentInfo()
        {
            InitializeComponent();
            AutoCompleterText();
        }
        
        private void StudentInformation_Load(object sender, EventArgs e)
        {

        }

        public StudentInfo(Student aStudent) : this()
        {
            btnSave.Text = "Update";
            FillFieldWith(aStudent);
            
        }


        private void FillFieldWith(Student aStudent)
        {
            txtStudentId.Text = aStudent.StudentId;
            txtStudentName.Text = aStudent.Name;
            txtDepartment.Text = aStudent.Department;
            txtEmail.Text = aStudent.Email;
            txtRoomNo.Text = aStudent.RoomPosition;
            txtSession.Text = aStudent.Session;
            txtFatherName.Text = aStudent.Fathername;
            txtAddress.Text = aStudent.Address;
            txtSchool.Text = aStudent.School;
            txtCollege.Text = aStudent.College;
            txtDist.Text = aStudent.District;
            txtHallName.Text = aStudent.HallName;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            Student aStudent = new Student();
            aStudent.StudentId = txtStudentId.Text;
            aStudent.Name = txtStudentName.Text;
            aStudent.Department = txtDepartment.Text;
            aStudent.Session = txtSession.Text;
            aStudent.HallName = txtHallName.Text;
            aStudent.RoomPosition = txtRoomNo.Text;
            aStudent.Email = txtEmail.Text;
            aStudent.Fathername = txtFatherName.Text;
            aStudent.Address = txtAddress.Text;
            aStudent.School = txtSchool.Text;
            aStudent.College = txtCollege.Text;
            aStudent.District = txtDist.Text;
            if (btnSave.Text != "Update")
            {
                try
                {
                    message = studentManager.Save(aStudent);
                    MessageBox.Show(message, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    message = studentManager.Update(aStudent);
                    MessageBox.Show(message, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void AutoCompleterText()
        {
            txtRoomNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtRoomNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection collection = studentManager.autoComplete();
            txtRoomNo.AutoCompleteCustomSource = collection;

        }
       
    }
}
