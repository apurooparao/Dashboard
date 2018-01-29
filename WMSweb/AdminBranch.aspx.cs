using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using WMSobjects;

public partial class AdminBranch : System.Web.UI.Page
{
    private readonly SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
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
                var userBo = (UserBo)Session["UserBO"];
                if (userBo.RoleId == 1)
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
            using (var cmd = new SqlCommand("sp_Branch_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECTDDL");
                using (var sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    sda.SelectCommand = cmd;
                    using (var dt = new DataTable())
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
            using (var cmd = new SqlCommand("sp_Branch_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                using (var sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    sda.SelectCommand = cmd;
                    using (var dt = new DataTable())
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
            var cmd = new SqlCommand("sp_Branch_CRUD") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
            cmd.Parameters.AddWithValue("@RegionID", ddlAddRegName.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            var outValue = new SqlParameter("@OutValue", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(outValue);
            cmd.Connection = _con;
            _con.Open();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt16(outValue.Value);
            FillGrid();

            // ReSharper disable once ConvertIfStatementToSwitchStatement
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
            if (_con.State == ConnectionState.Open)
                _con.Close();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnClear.Text = "Clear";
        ClearControls();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();

            var btn = sender as LinkButton;
            // ReSharper disable once PossibleNullReferenceException
            var grow = btn.NamingContainer as GridViewRow;
            // ReSharper disable once PossibleNullReferenceException
            hidBranchID.Value = (grow.FindControl("lblBranchID") as Label).Text;
            var sqlquery = "Select BranchID,BranchName,RegionID,IsActive from tblm_Branch where BranchID=" + hidBranchID.Value;
            _con.Open();
            var cmd = new SqlCommand(sqlquery, _con);
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtBranchName.Text = dr["BranchName"].ToString();
                ddlAddRegName.SelectedIndex = ddlAddRegName.Items.IndexOf(ddlAddRegName.Items.FindByValue(dr["RegionID"].ToString()));
                // ReSharper disable once PossibleNullReferenceException
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
            var cmd = new SqlCommand("sp_Branch_CRUD") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@BranchID", hidBranchID.Value);
            cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
            cmd.Parameters.AddWithValue("@RegionID", ddlAddRegName.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            var outValue = new SqlParameter("@OutValue", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(outValue);
            cmd.Connection = _con;
            _con.Open();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt16(outValue.Value);
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
            if (_con.State == ConnectionState.Open)
                _con.Close();
        }
    }

    protected void datagrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        datagrid.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}