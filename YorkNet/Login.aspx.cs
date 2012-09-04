using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YorkNet
{
    public partial class Login : System.Web.UI.Page
    {

        #region Variables
        DBStuff DB = new DBStuff();
        ArrayList alPar = new ArrayList();
        ArrayList alVal = new ArrayList();
        ArrayList alType = new ArrayList();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Login 

        /// <summary>
        /// Login Routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                Response.Redirect("Administration.aspx");

                
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
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

    }
}