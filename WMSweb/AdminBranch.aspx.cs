using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSobjects;

public partial class AdminBranch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserBO"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                UserBO _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                if (_userBO.RoleID == 1)
                {

                    txtBranchName.Focus();
                    if (!IsPostBack)
                    {
                        FillGrid();
                        Fillddl(ddlAddRegName);
                    }
                }
                else
                {
                    Response.Redirect("Dashboard.aspx", false);
                }
            }
        }
        catch
        {
            throw;
        }

    }

    private void Fillddl(DropDownList ddl)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_Branch_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECTDDL");
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        ddl.DataSource = dt;
                        ddl.DataTextField = "RegionName";
                        ddl.DataValueField = "RegionID";
                        ddl.DataBind();
                        ddl.Items.Insert(0, new ListItem("Select", "0"));
                    }

                }

            }
        }
        catch
        {
            throw;
        }
    }

    private void FillGrid()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_Branch_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        datagrid.DataSource = dt;
                        datagrid.DataBind();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    void ClearControls()
    {
        try
        {
            txtBranchName.Text = "";
           // cbIsActive.Checked = false;
            hidBranchID.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            ddlAddRegName.ClearSelection();
        }
        catch
        {

            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_Branch_CRUD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
            cmd.Parameters.AddWithValue("@RegionID", ddlAddRegName.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            SqlParameter OutValue = new SqlParameter("@OutValue", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(OutValue);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt16(OutValue.Value);
            FillGrid();

            if (result == 1)
            {
                lblMessage.Text = "Saved Successfully";
                ClearControls();
            }
            else if (result == 100)
            {
                lblMessage.Text = "Branch Name for this Region already exists";
                txtBranchName.Focus();
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            btnClear.Text = "Clear";
            ClearControls();
        }
        catch
        {
            throw;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();

            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidBranchID.Value = (grow.FindControl("lblBranchID") as Label).Text;
            string sqlquery = "Select BranchID,BranchName,RegionID,IsActive from tblm_Branch where BranchID=" + hidBranchID.Value;
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtBranchName.Text = dr["BranchName"].ToString();
                ddlAddRegName.SelectedIndex = ddlAddRegName.Items.IndexOf(ddlAddRegName.Items.FindByValue(dr["RegionID"].ToString()));
                cbIsActive.Checked = (grow.FindControl("lblIsActive") as CheckBox).Checked;
            }

            btnSave.Visible = false;
            btnUpdate.Visible = true;
            btnClear.Text = "Cancel";
        }

        catch
        {
            throw;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_Branch_CRUD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@BranchID", hidBranchID.Value);
            cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
            cmd.Parameters.AddWithValue("@RegionID", ddlAddRegName.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            SqlParameter OutValue = new SqlParameter("@OutValue", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(OutValue);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt16(OutValue.Value);
            FillGrid();

            if (result == 1)
            {
                lblMessage.Text = "Updated Successfully";
                ClearControls();
            }
            else if (result == 2)
            {
                lblMessage.Text = "Cannot de-activate the Branch Record as it is being referenced by Open/In-Progress tickets";
                ClearControls();
            }
            else if (result == 100)
            {
                lblMessage.Text = "Branch Name for this Region already exists";
                txtBranchName.Focus();
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void datagrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        datagrid.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}