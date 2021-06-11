using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hospital_Assignment.mywork
{
    public partial class PatientSearch : System.Web.UI.Page
    {
        HospitalDBEntities dbcon = new HospitalDBEntities();
        HospitalDBEntities dbcon1 = new HospitalDBEntities();
        HospitalDBEntities dbcon2 = new HospitalDBEntities();
        HospitalDBEntities dbcon3 = new HospitalDBEntities();
        string name;
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUserName = Session["New"];

            dbcon1.DoctorsTables.Load();
            var result = from item in dbcon1.DoctorsTables.Local
                         where item.UserLoginName == currentUserName.ToString()
                         select item.DoctorID;
            var result1 = from item in dbcon1.DoctorsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.FirstName;
            var result2 = from item in dbcon1.DoctorsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.LastName;

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();

            Label3.Text = name;
            dbcon.PatientsTables.Where(t => t.DoctorID == result.FirstOrDefault()).Load();
            GridView4.DataSource = dbcon.PatientsTables.Local;
            GridView4.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dbcon2.MedicationListTables.Load();
            dbcon3.TestsTables.Load();

            Label1.Text = TextBox2.Text + " " + TextBox3.Text;

            var result = from item in dbcon.PatientsTables.Local
                         where item.PatientID == Convert.ToInt32(TextBox1.Text)
                         select item.Email;
            Label2.Text = String.Join("", result);

            Label4.Text = TextBox1.Text;

            var result2 = from item in dbcon2.MedicationListTables.Local
                          where item.PatientID == Convert.ToInt32(TextBox1.Text)
                          select item;
            GridView6.DataSource = result2;
            GridView6.DataBind();

            var result3 = from item in dbcon3.TestsTables.Local
                          where item.PatientID == Convert.ToInt32(TextBox1.Text)
                          select item;
            GridView5.DataSource = result3;
            GridView5.DataBind();
        }
    }
}
