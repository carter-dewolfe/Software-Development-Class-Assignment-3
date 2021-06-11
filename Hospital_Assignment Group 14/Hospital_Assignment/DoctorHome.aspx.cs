using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hospital_Assignment
{
    public partial class DoctorHome : System.Web.UI.Page
    {
        HospitalDBEntities dbcon = new HospitalDBEntities();
        HospitalDBEntities dbcon1 = new HospitalDBEntities();
        string name;
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUserName = Session["New"];

            dbcon1.DoctorsTables.Load();
            var result1 = from item in dbcon1.DoctorsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.FirstName;
            var result2 = from item in dbcon1.DoctorsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.LastName;

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();

            Label1.Text = name;
        }
    }
}