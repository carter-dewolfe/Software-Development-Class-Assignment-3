using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hospital_Assignment.mywork
{
    public partial class PatientTestResults : System.Web.UI.Page
    {
        HospitalDBEntities dbcon = new HospitalDBEntities();
        HospitalDBEntities dbcon1 = new HospitalDBEntities();
        string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUserName = Session["New"];

            dbcon1.PatientsTables.Load();
            var result = from item in dbcon1.PatientsTables.Local
                         where item.UserLoginName == currentUserName.ToString()
                         select item.PatientID;
            var result1 = from item in dbcon1.PatientsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.FirstName;
            var result2 = from item in dbcon1.PatientsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.LastName;
            dbcon.TestsTables.Where(t => t.PatientID == result.FirstOrDefault()).Load();
            GridView1.DataSource = dbcon.TestsTables.Local;
            GridView1.DataBind();

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();

            Label1.Text = name;
        }
    }
}