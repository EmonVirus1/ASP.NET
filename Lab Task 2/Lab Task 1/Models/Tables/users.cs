using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Lab_Task_1.Models.Entity;

namespace Lab_Task_1.Models.Tables
{
    public class users
    {
        Database db;
        public users()
        {
            db = new Database();
        }
        public Boolean isUser (string uname, string pass)
        {
            //*** For debugging ***//
            //db.executeQuery("delete from my_orders");
            //*** For bebugging ***//

            SqlDataReader sdr = db.getTable(string.Format("select * from client where uname = '{0}' and pass = '{1}'", uname, pass));
            if (sdr.HasRows)
            {
                return true;
            }
            else return false;
        }
        public Boolean isAdmin(string uname, string pass)
        {
            //*** For debugging ***//
            //db.executeQuery("delete from my_orders");
            //*** For bebugging ***//

            SqlDataReader sdr = db.getTable(string.Format("select * from admin where uname = '{0}' and pass = '{1}'", uname, pass));
            if (sdr.HasRows)
            {
                return true;
            }
            else return false;
        }
        public void addNewUser (string uname, string phone, string pass)
        {
            if (isUser(uname, pass))
            {
                return;
            }
            else db.executeQuery(String.Format("insert into client values('{0}','{1}','{2}')",uname,pass,phone));
        }
        public User user_object(string uname, string pass)
        {
            SqlDataReader sdr = db.getTable(string.Format("select * from client where uname = '{0}' and pass = '{1}'", uname, pass));
            User demo = new User();
            while (sdr.Read())
            {
                User ttt = new User()
                {
                    uid = sdr.GetInt32(sdr.GetOrdinal("id")),
                    uname = sdr.GetString(sdr.GetOrdinal("uname")),
                    pass = sdr.GetString(sdr.GetOrdinal("pass")),
                    phone = sdr.GetString(sdr.GetOrdinal("phone"))
                };
                demo = ttt;
            }
            return demo;
        }
    }
}