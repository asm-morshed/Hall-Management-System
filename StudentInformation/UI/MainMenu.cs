using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentInformation.UI;

namespace StudentInformation
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            StudentInfo studentinfo = new StudentInfo();
            studentinfo.Show();
        }

        private void FindEditButton_Click(object sender, EventArgs e)
        {
            SearchStudent searchStudent = new SearchStudent();
            searchStudent.Show();

        }

        
       
    }
}
