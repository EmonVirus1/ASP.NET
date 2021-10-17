using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_Task_1.Models.Entity
{
    public class order
    {
        public int id { set; get; }
        public string name { set; get; }
        public double price { set; get; }
        public int cid {set; get;}
        public string status { set; get; }
    }
}