using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSelfHost
{
    public class StoreController : System.Web.Http.ApiController
    {
        public List<string> GetCategoryName()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT Name FROM tblCategory", null);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0]);
            return lcNames;
        }

        public List<String> GetOrders()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tblOrders", null);
            List<string> lcId = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcId.Add((string)dr[0]
                    );
            return lcId;
        }

        public clsOrder GetOrderDetails(int id)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Id", id);
            DataTable lcResult = clsDbConnection.GetDataTable("Select * FROM tblOrders WHERE Id =@Id", par);
            if (lcResult.Rows.Count > 0)
                return new clsOrder()
                {
                    tblProductID = (string)lcResult.Rows[0]["tblProductId"],
                    CustomerName = (string)lcResult.Rows[0]["CustomerName"],
                    CustomerEmail = (string)lcResult.Rows[0]["CustomerEmail"],
                    Quantity = (int)lcResult.Rows[0]["Quantity"],
                    Total = Convert.ToSingle((decimal)lcResult.Rows[0]["Total"])


                };
            else
                return null;
        }

        public clsComponent GetComponent(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult =
        clsDbConnection.GetDataTable("SELECT * FROM tblCategory WHERE Name = @Name", par);
            if (lcResult.Rows.Count > 0)
                return new clsComponent()
                {
                    Name = (string)lcResult.Rows[0]["Name"],
                    CategoryName = (string)lcResult.Rows[0]["CategoryName"],
                    ComponentList = GetComponents(Name)
                    
                };
            else
                return null;
        }

        private List<clsAllComponents>GetComponents(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tblProduct WHERE tblCategoryName = @Name", par);
            List<clsAllComponents> lcComponent= new List<clsAllComponents>();
            foreach (DataRow dr in lcResult.Rows)
                lcComponent.Add(dataRow2AllWork(dr));
            return lcComponent;

        }

        public clsAllComponents GetComponentDetails(string ID)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Id", ID);
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tblProduct WHERE Name = @Id", par);
            clsAllComponents lcComponents = new clsAllComponents();

            return new clsAllComponents()
            {
                Name = (string)lcResult.Rows[0]["Name"],
                tblCategoryName = (string)lcResult.Rows[0]["tblCategoryName"],
                Brand = (string)lcResult.Rows[0]["Brand"],
                Type = (string)lcResult.Rows[0]["Type"],
                Condition = (string)lcResult.Rows[0]["Condition"],
               Warranty = lcResult.Rows[0]["Warranty"] is DBNull ? (float?)null : Convert.ToSingle(lcResult.Rows[0]["Warranty"])

            };
        }

        private clsAllComponents dataRow2AllWork(DataRow prDataRow)
        {
            return new clsAllComponents()
            {
                Name = Convert.ToString(prDataRow["Name"]),
                tblCategoryName = Convert.ToString(prDataRow["tblCategoryName"]),
                Price = Convert.ToSingle(prDataRow["Price"]),
                Quantity = Convert.ToSingle(prDataRow["Quantity"]),
                Brand = Convert.ToString(prDataRow["Brand"]),
                Type = Convert.ToString(prDataRow["Type"]),
                Condition = Convert.ToString(prDataRow["Condition"]),
               Warranty = prDataRow["Warranty"] is DBNull ? (float?)null : Convert.ToSingle(prDataRow["Warranty"]),
               Modified = Convert.ToDateTime(prDataRow["Modified"])

            };

        }

        public string PutComponent(clsAllComponents prComponent)
        {   // insert
            try
            {
                int lcRecCount = clsDbConnection.Execute("UPDATE tblProduct SET" +
                " Type = @Type, Name = @Name, tblCategoryName = @CategoryName, Price = @Price, Quantity = @Quantity, Brand = @Brand, Condition = @Condition, Warranty = @Warranty, Modified = @Modified" +
                " WHERE Name = @Name",
                prepareComponentParameters(prComponent));
               if (lcRecCount == 1)
                    return "Component succesfully Updated";
                else
                    return "Item No longer exists ";
            }
            catch (SqlException)
            {

                    return "Item no longer exists";
   

            }
        }

        public string PostComponent(clsAllComponents prComponent)
        {   // add
            try
            {
                int lcRecCount = clsDbConnection.Execute("INSERT INTO tblProduct" +
                " (Type, Name, tblCategoryName, Price, Quantity, Brand, Condition, Warranty, Modified)" +
                "VALUES (@Type, @Name, @CategoryName, @Price, @Quantity, @Brand, @Condition, @Warranty, @Modified)",
                prepareComponentParameters(prComponent));
                if (lcRecCount == 1)
                    return "Component succesfully added";
                else
                    return "Component Could not be added ";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "There is already item with this name, select Another name for  the item and try again";
                }
                else
                    return ex.GetBaseException().Message;
            }
        }

        public string DeleteComponent(string Name)
        {   // remove
            try
            {
                int lcRecCount = clsDbConnection.Execute(
        "DELETE FROM tblProduct WHERE Name = @Name",
        new Dictionary<string, object>(2) { { "Name", Name } });
                if (lcRecCount == 1)
                    return "Component Deleted";
                else
                    return "This Item No Longer Exists";
            }
            catch (SqlException ex)
            {
                    return ex.GetBaseException().Message;
            }

        }


        private Dictionary<string, object> prepareComponentParameters(clsAllComponents prComponent)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(10);
            par.Add("Name", prComponent.Name);
            par.Add("CategoryName", prComponent.tblCategoryName);
            par.Add("Price", prComponent.Price);
            par.Add("Quantity", prComponent.Quantity);
            par.Add("Brand", prComponent.Brand);
            par.Add("Type", prComponent.Type);
            par.Add("Condition", prComponent.Condition);
            par.Add("Warranty", prComponent.Warranty);
            par.Add("Modified", prComponent.Modified);

            return par;
        }

    }
}
