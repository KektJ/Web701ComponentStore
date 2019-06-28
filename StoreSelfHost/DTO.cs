using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSelfHost
{

    public class clsComponent
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }

        public string ComponentFormList { get; set; }

        public List<clsAllComponents> ComponentList { get; set; }

    }

    public class clsAllComponents
    {
        public string Name { get; set; }

        public string tblCategoryName { get; set; }

        public float Price { get; set; }

        public float Quantity { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string Condition { get; set; }

        public float? Warranty { get; set; }

        public DateTime Modified { get; set; }

        public static clsAllComponents NewComponent(string prChoice)
        {
            return new clsAllComponents() { Type = (prChoice) };
        }


    }

    public class clsOrder
    {
        public int id { get; set; }
        public string tblProductID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public float Quantity { get; set; }

        public float Total { get; set; }

        public char Status { get; set; }

    }
}
