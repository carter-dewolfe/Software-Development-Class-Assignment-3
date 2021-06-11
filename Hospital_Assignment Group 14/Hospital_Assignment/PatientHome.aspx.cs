using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hospital_Assignment
{
    public partial class PatientHome : System.Web.UI.Page
    {
        HospitalDBEntities dbcon = new HospitalDBEntities();
        HospitalDBEntities dbcon1 = new HospitalDBEntities();
        HospitalDBEntities dbcon2 = new HospitalDBEntities();
        string name;
        string doctor;
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUserName = Session["New"];

            dbcon1.PatientsTables.Load();
            dbcon2.DoctorsTables.Load();
            var result = from item in dbcon1.PatientsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.DoctorID;
            var result1 = from item in dbcon1.PatientsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.FirstName;
            var result2 = from item in dbcon1.PatientsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.LastName;
            var result3 = from item in dbcon2.DoctorsTables.Local
                          where item.DoctorID == result.FirstOrDefault()
                          select item.FirstName;
            var result4 = from item in dbcon2.DoctorsTables.Local
                          where item.DoctorID == result.FirstOrDefault()
                          select item.LastName;

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();
            doctor = result3.FirstOrDefault().Trim() + " " + result4.FirstOrDefault().Trim();

            Label1.Text = name;
            Label2.Text = doctor;
        }
    }
}