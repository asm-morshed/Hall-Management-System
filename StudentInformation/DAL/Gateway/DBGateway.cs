using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentInformation.DAL.Gateway
{
    public class DBGateway
    {
        private SqlConnection connectionObj;
        private SqlCommand commandObj;

        public DBGateway()
        {
            connectionObj = new SqlConnection(@"server=.\sqlexpress;Initial Catalog=StudentInfoDB;Integrated Security=true");
            commandObj = new SqlCommand();
        }

        public SqlConnection SqlConnectionObj
        {
            get { return connectionObj; }
        }
        public SqlCommand SqlCommandObj
        {
            get { commandObj.Connection = connectionObj; return commandObj; }
        }
    }
}
