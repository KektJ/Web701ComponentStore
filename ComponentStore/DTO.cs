using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentStore
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

        public float? Price { get; set; }

        public float Quantity { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string Condition { get; set; }

        public float? Warranty { get; set; }

        public DateTime Modified { get; set; }

        public override string ToString()
        {
            return Name + "\t" + Brand + "\t" + Type;
        }


        public static clsAllComponents NewComponent(string prChoice)
        {
            return new clsAllComponents() { Type = (prChoice), Brand="", Condition="" };
        }


    }

    public class clsOrder
    {
        public string id { get; set; }
        public string tblProductID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public float Quantity { get; set; }

        public float Total { get; set; }

        public char Status { get; set; }

        public clsComponent Component { get; set; }

    }
}
