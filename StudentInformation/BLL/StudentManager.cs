using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformation.DAL.Gateway;
using StudentInformation.DAL.DAO;

using System.Windows.Forms;

namespace StudentInformation.BLL
{
    class StudentManager
    {
        private StudentGateway studentGateway=new StudentGateway();
        private string message = "";
        public string Save(Student aStudent)
        {
            if (aStudent.StudentId == string.Empty)
            {
                message = "Student Id is missing";
            }
            else if(aStudent.Name==string.Empty)
            {
                message = "Student Name is missing";
            }
            else if (aStudent.Department == string.Empty)
            {
                message = "Department is missing";
            }
            else if (aStudent.Session == string.Empty)
            {
                message = "Session is missing";
            }
            else if (aStudent.HallName == string.Empty)
            {
                message = "HallName is missing";
            }
            else if (aStudent.Email == string.Empty)
            {
                message = "Email is missing";
            }
            else if (aStudent.Fathername == string.Empty)
            {
                message = "Father Name is missing";
            }
            else if (aStudent.Address == string.Empty)
            {
                message = "Address Name is missing";
            }
            else if (aStudent.School == string.Empty)
            {
                message = "School Name is missing";
            }
            else if (aStudent.College == string.Empty)
            {
                message = "College Name is missing";
            }
            else if (aStudent.District == string.Empty)
            {
                message = "District Name is missing";
            }
            else
            {
                message = studentGateway.Save(aStudent);;
            }

            return message;
        }
        public string Update(Student aStudent)
        {
            string message = "";
            return message = studentGateway.Update(aStudent);

        }
        
        public List<Student> GetAllStudents(string name)
        {
            return studentGateway.GetAllStudent(name);
        }
        public AutoCompleteStringCollection autoComplete()
        {
            return studentGateway.autoComplete();
        }
        public string DelteStudent(Student selectedStudent)
        {
            return studentGateway.DeleteStudent(selectedStudent);
        }
    }
}
