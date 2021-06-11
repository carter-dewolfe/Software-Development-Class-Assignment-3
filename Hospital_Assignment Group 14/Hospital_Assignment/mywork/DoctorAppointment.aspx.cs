using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Globalization;

namespace Hospital_Assignment.mywork
{
    public partial class DoctorAppointment : System.Web.UI.Page
    {
        HospitalDBEntities dbcon = new HospitalDBEntities();
        HospitalDBEntities dbcon1 = new HospitalDBEntities();
        HospitalDBEntities dbcon2 = new HospitalDBEntities();
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
            dbcon.AppointmentsTables.Where(t => t.DoctorID == result.FirstOrDefault() && t.Date > DateTime.Now).Load();
            GridView1.DataSource = dbcon.AppointmentsTables.Local;
            GridView1.DataBind();

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();

            Label1.Text = name;
        }

        //Delete
        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        //Add
        protected void Button1_Click(object sender, EventArgs e)
        {
            var currentUserName = Session["New"];
            var result = from item in dbcon1.PatientsTables.Local
                         where item.UserLoginName == currentUserName.ToString()
                         select item.PatientID;
            AppointmentsTable myApp = new AppointmentsTable();
            myApp.Date = Calendar1.SelectedDate;
            myApp.VisitSummary = TextBox1.Text;
            myApp.Purpose = TextBox2.Text;
            myApp.PatientID = Convert.ToInt32(DropDownList3.SelectedValue);
            myApp.DoctorID = result.FirstOrDefault();

            int hour = Convert.ToInt32(DropDownList1.SelectedValue);
            if (RadioButtonList1.SelectedIndex == 1)
                hour = hour;

            TimeSpan time = TimeSpan.Parse(hour.ToString() + ":" + DropDownList2.SelectedValue);
            myApp.Time = time;

            dbcon2.AppointmentsTables.Load();
            foreach (var x in dbcon2.AppointmentsTables.Local)
            {
                if (time == x.Time && myApp.Date == x.Date && myApp.PatientID == x.PatientID)
                {
                    eLabel.Visible = true;
                    eLabel.Text = "Conflicting times please choose a new time";
                }
                else if(myApp.Date < DateTime.Now)
                {
                    eLabel.Visible = true;
                    eLabel.Text = "Date has already passed";
                }
                else
                {
                    eLabel.Visible = false;
                    dbcon1.AppointmentsTables.Add(myApp);
                    dbcon1.SaveChanges();
                    GridView1.DataBind();
                }
            }
        }
    }
}