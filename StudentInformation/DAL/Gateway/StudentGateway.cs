using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using StudentInformation.DAL.DAO;
using StudentInformation.DAL.Gateway;
using System.Data;


using System.Windows.Forms;

namespace StudentInformation.DAL.Gateway
{
    class StudentGateway:DBGateway
    {
        
        public string Save(Student aStudent)
        {
            
            string message = "";
            aStudent.RoomNo = aStudent.RoomPosition.Remove(aStudent.RoomPosition.Length - 2, 2);
            try
            {
                SqlConnectionObj.Open();
                string query = String.Format("insert into tbl_Student values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", aStudent.StudentId, aStudent.Name, aStudent.Department, aStudent.Session, aStudent.HallName, aStudent.Email, aStudent.RoomPosition, aStudent.RoomNo, aStudent.Fathername, aStudent.Address, aStudent.School, aStudent.College, aStudent.District);
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
                message = "Employee: " + aStudent.Name + " has been saved";
                query = String.Format("delete from tbl_room where roomNo='{0}'",aStudent.RoomPosition);
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Error!!!", exception);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
            return message;

        }
        public string Update(Student aStudent)
        {

            string message = "";

            aStudent.RoomNo = aStudent.RoomPosition.Remove(aStudent.RoomPosition.Length - 2, 2);
            try
            {
                SqlConnectionObj.Open();
                string roomPos;
                string roomQuery = String.Format("select roomPosition from tbl_Student where studentId='{0}'",aStudent.StudentId);
                SqlCommandObj.CommandText = roomQuery;
                object roomp = SqlCommandObj.ExecuteScalar();
                roomPos = roomp.ToString();
                
                if (roomPos != aStudent.RoomPosition)
                {
                    string queryRoom = "insert into tbl_room";
                    queryRoom = String.Format("insert into tbl_room values ('{0}')", roomPos);
                    SqlCommandObj.CommandText = queryRoom;
                    SqlCommandObj.ExecuteNonQuery();
                }
                SqlConnectionObj.Close();
                SqlConnectionObj.Open();
                string query = String.Format(
                    "UPDATE  tbl_Student SET name='{0}',department='{1}',session='{2}',hall='{3}',email='{4}',roomPosition='{5}',roomNo='{6}',fathername='{7}',address='{8}',school='{9}',college='{10}',district='{11}' WHERE studentId='{12}'",  
                    aStudent.Name, aStudent.Department, aStudent.Session, aStudent.HallName, aStudent.Email, aStudent.RoomPosition, aStudent.RoomNo, aStudent.Fathername, aStudent.Address, aStudent.School, aStudent.College, aStudent.District, aStudent.StudentId);
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
                message = "Employee: " + aStudent.Name + " has been Updated";
                query = String.Format("delete from tbl_room where roomNo='{0}'", aStudent.RoomPosition);
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Error!!!", exception);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
            return message;

        }
        
        public List<Student> GetAllStudent()
        {
            string nameofsearchingWord = "";
            return GetAllStudent(nameofsearchingWord);
        }
        public List<Student> GetAllStudent(string roomNo)
        {
            List<Student> students = new List<Student>();
            try
            {
                SqlConnectionObj.Open();
                string query = String.Format("select * from tbl_Student WHERE roomNo LIKE '%{0}%'", roomNo);
                //query += "order by studentName";
                //SqlConnectionObj.Open();
                //
                //SqlCommandObj.CommandText = query;
                //SqlDataReader reader = SqlCommandObj.ExecuteReader();
                //SqlCommandObj.CommandText = query;
                SqlCommandObj.CommandText = query;
                SqlDataReader reader = SqlCommandObj.ExecuteReader();
                while (reader.Read())
                {
                    Student aStudent = new Student();
                    aStudent.StudentId = reader["studentId"].ToString();
                    aStudent.Name = reader["name"].ToString();
                    aStudent.Department = reader["department"].ToString();
                    aStudent.Session = reader["session"].ToString();
                    aStudent.HallName = reader["hall"].ToString();
                    aStudent.Email = reader["email"].ToString();
                    aStudent.RoomPosition = reader["roomPosition"].ToString();
                    aStudent.RoomNo = reader["roomNo"].ToString();
                    aStudent.Fathername = reader["fatherName"].ToString();
                    aStudent.Address = reader["address"].ToString();
                    aStudent.School = reader["school"].ToString();
                    aStudent.College = reader["college"].ToString();
                    aStudent.District = reader["district"].ToString();

                    students.Add(aStudent);
                }
                                
            }
            catch(Exception ex)
            {
                throw new Exception("can't populate students",ex);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
            return students;
        }

        public AutoCompleteStringCollection autoComplete()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            try
            {
                SqlConnectionObj.Open();
                string query = "select * from tbl_room";
                SqlCommandObj.CommandText = query;
                SqlDataReader reader = SqlCommandObj.ExecuteReader();
                while (reader.Read())
                {
                    string roomNo = reader["roomNo"].ToString();
                    collection.Add(roomNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
            return collection;
        }
        public string DeleteStudent(Student selectedStudent)
        {
            string message = "";
            try
            {
                SqlConnectionObj.Open();
                string query = String.Format("DELETE FROM tbl_Student WHERE studentId='{0}'", selectedStudent.StudentId);
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
                message = "Student: " + selectedStudent.Name + " has been deleted.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during student delete operation " + selectedStudent.Name, ex);
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
            InsertRoom(selectedStudent);
            return message;
        }
        public void InsertRoom(Student selectedStudent)
        {
            try
            {
                SqlConnectionObj.Open();
                string query = String.Format("insert into tbl_room values ('{0}')", selectedStudent.RoomPosition);
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (SqlConnectionObj != null && SqlConnectionObj.State == ConnectionState.Open)
                {
                    SqlConnectionObj.Close();
                }
            }
        }
    }

}
