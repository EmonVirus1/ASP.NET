using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Lab_Task_1.Models.Entity;

namespace Lab_Task_1.Models.Tables
{
    public class products
    {
        Database db;
        public products()
        {
            db = new Database();
        }
        public List<product> getAllProducts()
        {
            SqlDataReader sdr = db.getTable("select * from products");
            List<product> lst = new List<product>();
            //Console.WriteLine("Tawhid is my name");
            while (sdr.Read())
            {
                product p = new product()
                {
                    name = sdr.GetString(sdr.GetOrdinal("name")),
                    price = sdr.GetDouble(sdr.GetOrdinal("price")),
                    quantity = sdr.GetInt32(sdr.GetOrdinal("quantity"))
                };
                lst.Add(p);
            }
            return lst;
        }
    }
}