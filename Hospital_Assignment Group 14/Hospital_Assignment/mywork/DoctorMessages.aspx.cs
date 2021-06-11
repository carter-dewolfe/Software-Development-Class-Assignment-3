using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace Hospital_Assignment.mywork
{
    public partial class DoctorMessages : System.Web.UI.Page
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
            dbcon1.DoctorsTables.Load();
            var result2 = from item in dbcon1.DoctorsTables.Local
                          where item.UserLoginName == currentUserName.ToString()
                          select item.LastName;

            name = result1.FirstOrDefault().Trim() + " " + result2.FirstOrDefault().Trim();

            dbcon.MessagesTables.Where(t => t.MessageFROM == name || t.MessageTO == name).Load();
            GridView1.DataSource = dbcon.MessagesTables.Local;
            GridView1.DataBind();

            Label1.Text = name;
        }

        //Delete
        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        //Send
        protected void Button1_Click(object sender, EventArgs e)
        {
            MessagesTable myMessage = new MessagesTable();

            myMessage.Date = DateTime.Now;
            myMessage.MessageTO = DropDownList1.SelectedValue;
            myMessage.Message = TextBox1.Text;
            myMessage.MessageFROM = name;

            dbcon.MessagesTables.Add(myMessage);
            dbcon.SaveChanges();
            GridView1.DataBind();
        }
    }
}