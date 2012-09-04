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
    public partial class ProcedureDetails : System.Web.UI.Page
    {

        ArrayList alPar = new ArrayList();
        ArrayList alVal = new ArrayList();
        ArrayList alType = new ArrayList();
        DBStuff DB = new DBStuff();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                SqlDataReader drdata = null;
                string Link = "";
                try
                {
                    alPar.Clear();
                    alVal.Clear();
                    alType.Clear();

                    alPar.Add("@ProcedureID");
                    alVal.Add(Request.QueryString["id"].ToString());
                    alType.Add("myint");

                    drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_Procedure_Details");
                    if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                    if (drdata.HasRows)
                    {
                        while (drdata.Read())
                        {
                            Link = drdata["Procedure_File_Name"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (drdata != null)
                    {
                        drdata.Close();
                        drdata.Dispose();
                    }
                }

                Response.Redirect("~/Policies/" + Link);

            }
        }

       
    }
}