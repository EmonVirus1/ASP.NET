using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Lab_Task_1.Models
{
    public class Database
    {
        private string conn_string;
        private SqlConnection conn;
        private SqlCommand cmd;
        public Database()
        {
            conn_string = @"Server=DESKTOP-9MQ4HEP\SQLEXPRESS;Database=practice;Integrated Security=true";
            //conn = new SqlConnection(conn_string);
        }
        public void executeQuery(string q)
        {
            conn = new SqlConnection(conn_string);
            cmd = new SqlCommand(q, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public SqlDataReader getTable(string q)
        {
            conn = new SqlConnection(conn_string);
            cmd = new SqlCommand(q, conn);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            //conn.Close();
            return rd;
        }
    }
}