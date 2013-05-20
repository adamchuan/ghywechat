using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.OleDb;
namespace Common
{
    public class AccessHelper
    {
        private string strConnection = "";
        public AccessHelper(string _constr,int type)
        {
            if (type == 1)
                strConnection = "Provider=Microsoft.Jet.OleDb.4.0;" + _constr;
            else if (type == 2)
                strConnection = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + _constr + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
        }
        public DataTable Reader(string sql)
        {
            OleDbConnection objConnection = new OleDbConnection(strConnection);  //建立连接  
            objConnection.Open();  //打开连接  
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, objConnection);

            DataTable ThisDataTable = new DataTable();
            adapter.Fill(ThisDataTable);
            objConnection.Close();
            return ThisDataTable;
            
            
        }
        public int ExecuteNonQuery(string sql)
        {
            OleDbConnection objConnection = new OleDbConnection(strConnection);  //建立连接  
            objConnection.Open();  //打开连接  
            OleDbCommand sqlcmd = new OleDbCommand(sql, objConnection);  //sql语句 
            int result= sqlcmd.ExecuteNonQuery();
            objConnection.Close();
            return result;

        }
        public object ExecuteScalar(string sql)
        {
            OleDbConnection objConnection = new OleDbConnection(strConnection);  //建立连接  
            objConnection.Open();  //打开连接  
            OleDbCommand sqlcmd = new OleDbCommand(sql, objConnection);  //sql语句 
            object result=sqlcmd.ExecuteScalar();
            objConnection.Close();
            return result;
        }
    }
}
