using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hospital_Assignment.mywork
{
    public partial class PatientMedicationList : System.Web.UI.Page
    {
        HospitalDBEntities dbcon1 = new HospitalDBEntities();
        HospitalDBEntities dbcon = new HospitalDBEntities();
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
            dbcon.MedicationListTables.Where(t => t.PatientID == result.FirstOrDefault()).Load();
            GridView1.DataSource = dbcon.MedicationListTables.Local;
            GridView1.DataBind();

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();

            Label1.Text = name;
        }
    }
}