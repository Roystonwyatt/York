using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YorkNet
{
    public partial class Home : System.Web.UI.Page
    {

        #region Variables
        DBStuff DB = new DBStuff();
        ArrayList alPar = new ArrayList();
        ArrayList alVal = new ArrayList();
        ArrayList alType = new ArrayList();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
               LoadNewsSection();
               LoadPoliciesSection();
        }

        //--Placeholder loading routines

        #region News
        /// <summary>
        /// Load News
        /// </summary>
        private void LoadNewsSection()
        {
            lblerror.Text = "";
            SqlDataReader drdata = null;
            try
            {
                Literal HTML = new Literal();
                plcNews.Controls.Clear();

                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_All_News");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if(drdata.HasRows)
                {
                    while(drdata.Read())
                    {
                        HTML.Text += "<tr height = \"60\"><td><img src = \"" + drdata["News_Thumbnail"].ToString() + "\" width = \"70px\"  /></td>";
                        HTML.Text += "<td align = \"left\">" + drdata["News_Teaser"].ToString() + "<br/>";
                        HTML.Text += "<a href = # onClick =  \"return OpenNewsWindow(" + drdata["News_ID"].ToString() + ");\" title = \"Click here to read more\" target = \"blank\" >Read more</a>";
                    }
                }

                plcNews.Controls.Add(HTML);
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message.ToString();
            }
            finally
            {
                if (drdata != null)
                {
                    drdata.Close();
                    drdata.Dispose();
                }
            }
        }
        #endregion

        #region Policies & Procedures
        /// <summary>
        /// Load Policies & Procedures
        /// </summary>
        private void LoadPoliciesSection()
        {
            lblerror.Text = "";
            SqlDataReader drdata = null;
            try
            {
                Literal HTML = new Literal();
                plcPolicies.Controls.Clear();

                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_All_Procedures");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        HTML.Text += "<tr height = \"60\"><td width = 40% valign = top align = left><b>" + drdata["Procedure_Name"].ToString() + "</b></td>";
                        HTML.Text += "<td align = \"left\">" + drdata["Procedure_Short_Description"].ToString() + "<br/>";
                        HTML.Text += "<a href = # onClick =  \"return OpenPolicyWindow(" + drdata["Procedure_ID"].ToString() + ");\" title = \"Click here to read more\" target = \"blank\" >Read more</a>";
                    }
                }

                plcPolicies.Controls.Add(HTML);
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message.ToString();
            }
            finally
            {
                if (drdata != null)
                {
                    drdata.Close();
                    drdata.Dispose();
                }
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Events
        /// </summary>
        private void LoadEventsSection()
        {
            lblerror.Text = "";
            SqlDataReader drdata = null;
            try
            {

            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message.ToString();
            }
            if (drdata != null)
            {
                drdata.Close();
                drdata.Dispose();
            }
        }

        /// <summary>
        /// Load all event details of the day selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EventsCalendar_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                plcEvents.Controls.Clear();
                Literal HTML = new Literal();
                HTML.Text += "<tr><td align = left colspan = 3>Event Details for: " + EventsCalendar.SelectedDate.ToShortDateString() + "</td></tr>";
                plcEvents.Controls.Add(HTML);
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message.ToString();
            }

        }
        #endregion

        //--Authentication

        #region Authentication
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                alPar.Add("@UserName");
                alPar.Add("@Password");

                alVal.Add(txtUserName.Text);
                alVal.Add(txtPassword.Text);

                alType.Add("myvarchar");
                alType.Add("myvarchar");

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Authenticate_User");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        Session["UserID"] = drdata["Usr_ID"].ToString();
                        Session["FirstName"] = drdata["Usr_FirstName"].ToString();
                        Session["Surname"] = drdata["Usr_LastName"].ToString();
                    }
                }
                else
                {
                    Session["UserID"] = null;
                    Session["FirstName"] = null;
                    Session["Surname"] = null;
                }

            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message.ToString();
            }
            finally
            {
                if (drdata != null)
                {
                    drdata.Close();
                    drdata.Dispose();
                }
            }

            if (Session["UserID"] != null) { Response.Redirect("Administration.aspx"); } else { lblerror.Text = "Username and / or password invalid"; }
        }
        #endregion
      
    }
}