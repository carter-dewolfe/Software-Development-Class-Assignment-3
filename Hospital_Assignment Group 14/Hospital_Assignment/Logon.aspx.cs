using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.Entity;

namespace Hospital_Assignment.mywork
{
    public partial class Logon : System.Web.UI.Page
    {
        HospitalDBEntities dbcon = new HospitalDBEntities();
        string accountType;
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            dbcon.UsersTables.Load();
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var result = from item in dbcon.UsersTables.Local
                         where item.UserLoginName == Login1.UserName
                         select item.UserLoginType;

            var result1 = from item in dbcon.UsersTables.Local
                         where item.UserLoginName == Login1.UserName
                         select item.UserLoginName;

            Session["New"] = Login1.UserName;
            accountType = String.Join("", result);

            if (accountType == "1")
            {
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
                Response.Redirect("DoctorHome.aspx");
            }

            if (accountType == "0")
            {
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
                Response.Redirect("PatientHome.aspx");
            }
        }
    }
}