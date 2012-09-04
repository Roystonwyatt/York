using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace YorkNet
{
    public partial class AdminTelephoneDirectory : System.Web.UI.Page
    {

        ArrayList alPar = new ArrayList();
        ArrayList alVal = new ArrayList();
        ArrayList alType = new ArrayList();
        DBStuff DB = new DBStuff();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check for Session Timeout
            if (Session["UserID"] == null) { Response.Redirect("Home.aspx"); }

            if (Page.IsPostBack == false)
            {
                imgPhoto.Visible = false;
                lblFullName.Text = "Logged in as: " + Session["FirstName"].ToString() + " " + Session["SurName"].ToString();
                LoadTreeView();
                LoadUserTreeView();
            }
        }

        //--Submit new user

        #region Add user
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean Failure = false;
            try
            {
                //Perform all validations
                if (txtFirstName.Text.Trim() == "")
                {
                    Failure = true;
                    txtFirstName.BackColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    txtFirstName.BackColor = System.Drawing.Color.Transparent;
                }

                if (txtSurname.Text.Trim() == "")
                {
                    Failure = true;
                    txtSurname.BackColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    txtSurname.BackColor = System.Drawing.Color.Transparent;
                }

                if (txtMobile.Text.Trim() == "")
                {
                    Failure = true;
                    txtMobile.BackColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    txtMobile.BackColor = System.Drawing.Color.Transparent;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    Failure = true;
                    txtEmail.BackColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    txtEmail.BackColor = System.Drawing.Color.Transparent;
                }

                if (Failure) { throw new Exception("Please fill in all fields highlighted in Red"); }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                mdlExceptions.Show();
            }
        }
        #endregion

        //--Loading Routines

        #region Load of divisions
        /// <summary>
        /// Load Divisions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddDivision_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                try
                {
                    alPar.Clear();
                    alVal.Clear();
                    alType.Clear();

                    SqlDataReader drdata = DB.QuerySystemSP(alPar,alVal,alType,"sp_Get_All_Locations");

                    ddDivision.Items.Clear();
                    ddDivision.DataSource = drdata;
                    ddDivision.DataTextField = "Loc_Name";
                    ddDivision.DataValueField = "Loc_ID";
                    ddDivision.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message.ToString();
                    mdlExceptions.Show();
                }
            }
        }
        #endregion

        #region Loading Treeview control
        /// <summary>
        /// Load treeview control
        /// </summary>
        private void LoadTreeView()
        {
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                //Clear out Treeview
                tvTabLocations.Nodes.Clear();

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_All_Tree_Locations");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        if (drdata["TD_No"].ToString() != "") 
                        { 
                            TreeNode root = new TreeNode(drdata["Loc_Name"].ToString() + " - " + drdata["TD_No"].ToString(), drdata["Loc_ID"].ToString());
                            root.SelectAction = TreeNodeSelectAction.Select;
                            CreateChildNode(root);
                            tvTabLocations.Nodes.Add(root);

                        } 
                        else 
                        { 
                            TreeNode root = new TreeNode(drdata["Loc_Name"].ToString(), drdata["Loc_ID"].ToString());
                            root.SelectAction = TreeNodeSelectAction.Select;
                            CreateChildNode(root);
                            tvTabLocations.Nodes.Add(root);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                mdlExceptions.Show();
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

        /// <summary>
        /// Routine below to populate all children nodes
        /// </summary>
        /// <param name="Node"></param>
        private void CreateChildNode(TreeNode Node)
        {
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                alPar.Add("@LocationID");
                alVal.Add(Node.Value);
                alType.Add("myint");

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_All_Tree_Locations");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        if (drdata["TD_No"].ToString() != "")
                        {
                            TreeNode tnode = new TreeNode(drdata["Loc_Name"].ToString() + " - " + drdata["TD_No"].ToString(), drdata["Loc_ID"].ToString());
                            tnode.SelectAction = TreeNodeSelectAction.Select;
                            Node.ChildNodes.Add(tnode);
                            CreateChildNode(tnode);
                        }
                        else
                        {
                            TreeNode tnode = new TreeNode(drdata["Loc_Name"].ToString(), drdata["Loc_ID"].ToString());
                            tnode.SelectAction = TreeNodeSelectAction.Select;
                            Node.ChildNodes.Add(tnode);
                            CreateChildNode(tnode);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                mdlExceptions.Show();
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

        #region Loading Treeview control popup for add new user
        /// <summary>
        /// Load treeview control
        /// </summary>
        private void LoadUserTreeView()
        {
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                //Clear out Treeview
                tvLocations.Nodes.Clear();

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_All_Tree_Locations");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        if (drdata["TD_No"].ToString() != "")
                        {
                            TreeNode root = new TreeNode(drdata["Loc_Name"].ToString() + " - " + drdata["TD_No"].ToString(), drdata["Loc_ID"].ToString());
                            root.SelectAction = TreeNodeSelectAction.Select;
                            CreateUserChildNode(root);
                            tvLocations.Nodes.Add(root);

                        }
                        else
                        {
                            TreeNode root = new TreeNode(drdata["Loc_Name"].ToString(), drdata["Loc_ID"].ToString());
                            root.SelectAction = TreeNodeSelectAction.Select;
                            CreateUserChildNode(root);
                            tvLocations.Nodes.Add(root);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                mdlExceptions.Show();
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

        /// <summary>
        /// Routine below to populate all children nodes
        /// </summary>
        /// <param name="Node"></param>
        private void CreateUserChildNode(TreeNode Node)
        {
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                alPar.Add("@LocationID");
                alVal.Add(Node.Value);
                alType.Add("myint");

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Get_All_Tree_Locations");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        if (drdata["TD_No"].ToString() != "")
                        {
                            TreeNode tnode = new TreeNode(drdata["Loc_Name"].ToString() + " - " + drdata["TD_No"].ToString(), drdata["Loc_ID"].ToString());
                            tnode.SelectAction = TreeNodeSelectAction.Select;
                            Node.ChildNodes.Add(tnode);
                            CreateChildNode(tnode);
                        }
                        else
                        {
                            TreeNode tnode = new TreeNode(drdata["Loc_Name"].ToString(), drdata["Loc_ID"].ToString());
                            tnode.SelectAction = TreeNodeSelectAction.Select;
                            Node.ChildNodes.Add(tnode);
                            CreateChildNode(tnode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                mdlExceptions.Show();
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

        //--Update routines for Add new employee

        #region Telephone numbers
        protected void btnTelephoneNumbers_Click(object sender, EventArgs e)
        {
            mdlLocations.Show();
        }
        #endregion

        #region Preview photo
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string FileName = "";
            string RegThumbnailSaveLocation = "";

            //Get location of the thumbnail
            if ((fluPhoto.PostedFile != null) && (fluPhoto.PostedFile.ContentLength > 0))
            {
                string Image1 = System.IO.Path.GetFileName(fluPhoto.PostedFile.FileName);
                FileName = Image1;
                RegThumbnailSaveLocation = Server.MapPath("Photos") + "\\" + Image1;
                fluPhoto.PostedFile.SaveAs(RegThumbnailSaveLocation);

                //Load the preview control below
                imgPhoto.ImageUrl = "~/Photos//" + Image1;
                imgPhoto.Visible = true;
            }
            else
            {
                RegThumbnailSaveLocation = "";
                imgPhoto.Visible = false;
            }
        }
        #endregion

        //--Routines for Adding new location

        #region Add location Popup
        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            //Load Selected node into Node label on Locations popup
            if (tvTabLocations.SelectedNode != null)
            {
                TreeNode SelectedNode = tvTabLocations.SelectedNode;
                lblLocation.Text = SelectedNode.Text;
            }
            else
            {
                lblLocation.Text = "Top level location";
            }

            //Clear out all controls
            txtNewLocationName.Text = "";
            txtLocationDescription.Text = "";
            txtLocationTelNumber.Text = "";
            lblLocationError.Text = "";

            //Launch insert popup
            mdlAddLocations.Show();
            fvNewLocation.Enabled = true;
        }
        #endregion

        #region Create new location
        /// <summary>
        /// Create new location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitNewLocation_Click(object sender, EventArgs e)
        {
            SqlDataReader drdata = null;
            try
            {
                alPar.Clear();
                alVal.Clear();
                alType.Clear();

                alPar.Add("@LocationName");
                alVal.Add(txtNewLocationName.Text);
                alType.Add("myvarchar");

                if (txtLocationDescription.Text.Trim() != "")
                {
                    alPar.Add("@LocationDescription");
                    alVal.Add(txtLocationDescription.Text);
                    alType.Add("myvarchar");
                }

                if (tvTabLocations.SelectedNode != null)
                {
                    alPar.Add("@LocationParentID");
                    alVal.Add(tvTabLocations.SelectedNode.Value);
                    alType.Add("myint");
                }

                if (txtLocationTelNumber.Text.Trim() != "")
                {
                    alPar.Add("@Telephonenumber");
                    alVal.Add(txtLocationTelNumber.Text);
                    alType.Add("myvarchar");
                }

                drdata = DB.QuerySystemSP(alPar, alVal, alType, "sp_Add_Location");
                if (DB.m_bQuerySystemSPErr) { throw new Exception(DB.m_sExceptionMessage); }

                if (drdata.HasRows)
                {
                    while (drdata.Read())
                    {
                        if (drdata["Result"].ToString() == "2") { throw new Exception("A location with this name already exists "); }
                    }
                }

                //Rebuild Treeview
                LoadTreeView();
            }
            catch (Exception ex)
            {
                mdlAddLocations.Show();
                lblLocationError.Text = ex.Message.ToString();
            }
            finally
            {
                fvNewLocation.Enabled = false;
                if (drdata != null)
                {
                    drdata.Close();
                    drdata.Dispose();
                }
            }
        }
        #endregion

        #region Change Node selection
        protected void tvTabLocations_SelectedNodeChanged(object sender, EventArgs e)
        {
            //Disable location name validation to reset the postback function
            fvNewLocation.Enabled = false;
        }
        
        protected void tvLocations_SelectedNodeChanged(object sender, EventArgs e)
        {
            mdlLocations.Show();
        }
        #endregion

        //--Create new user section

        #region New user
        /// <summary>
        /// New user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitExtension_Click(object sender, EventArgs e)
        {
            SqlDataReader drdata = null;
            lblUserError.Text = "";
            try
            {

            }
            catch (Exception ex)
            {
                mdlLocations.Show();
                lblUserError.Text = ex.Message.ToString();
            }
        }
        #endregion

    }
}