using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Lab_Task_1.Models.Entity;

namespace Lab_Task_1.Models.Tables
{
    public class orders
    {
        Database db;
        public orders()
        {
            db = new Database();
        }
        public void addOrder(product p, int id)
        {
            SqlDataReader sdr = db.getTable("select quantity from products where name = '" + p.name + "'");
            int quantity = 0;
            while (sdr.Read())
            {
                quantity = sdr.GetInt32(sdr.GetOrdinal("quantity"));
            }
            if (quantity > 0)
            {
                string q = String.Format("insert into my_orders values('{0}',{1},{2},'ordered')", p.name, p.price,id);
                db.executeQuery(q);
                quantity--;
                db.executeQuery("update products set quantity = " + quantity + "where name = '"+p.name+"'");
            }
            //else
            //Error messgae (Product is unavailable)
        }
        public List<order> getAllOrder()
        {
            
            SqlDataReader sdr = db.getTable("select * from my_orders");
            List<order> lst = new List<order>();
            while (sdr.Read())
            {
                order p = new order()
                {
                    id = sdr.GetInt32(sdr.GetOrdinal("id")),
                    name = sdr.GetString(sdr.GetOrdinal("name")),
                    price = sdr.GetDouble(sdr.GetOrdinal("price")),
                    cid = sdr.GetInt32(sdr.GetOrdinal("cid")),
                    status = sdr.GetString(sdr.GetOrdinal("status"))
                };
                lst.Add(p);
            }
            return lst;
        }
        public void setProcessed(int id)
        {
            db.executeQuery("update my_orders set status='processed' where id="+id);
        }
        public void setCancel(int id)
        {
            db.executeQuery("update my_orders set status='cancelled' where id=" + id);
            db.executeQuery("update products set quantity=(quantity+1)");
        }
    }
}