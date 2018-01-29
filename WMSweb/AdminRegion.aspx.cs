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

public partial class AdminRegion : System.Web.UI.Page
{
    readonly SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
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

                    txtRegionName.Focus();
                    if (!IsPostBack)
                    {
                        FillGrid();
                    }
                }
                else
                {
                    Response.Redirect("Dashboard.aspx", false);
                }
            }
          
        }
        catch (Exception)
        {
            throw;
        }
    }
    void FillGrid()
    {
        try
        {
            using (var cmd = new SqlCommand("sp_Region_CRUD"))
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
        catch (Exception)
        {
            throw;
        }
    }

    void ClearControls()
    {
        try
        {
            txtRegionName.Text = "";
           // cbIsActive.Checked = false;
            hidRegionID.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var cmd = new SqlCommand("sp_Region_CRUD") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@RegionName", txtRegionName.Text);
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
                lblMessage.Text = "Saved Successfully";
                ClearControls();
            }
            else if (result == 100)
            {
                lblMessage.Text = "Region Name already exists";
                txtRegionName.Focus();
            }

        }
        catch (Exception)
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
        try
        {
            btnClear.Text = "Clear";
            ClearControls();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            var btn = sender as LinkButton;
            var grow = btn.NamingContainer as GridViewRow;
            hidRegionID.Value = (grow.FindControl("lblRegionID") as Label).Text;
            txtRegionName.Text = (grow.FindControl("lblRegionName") as Label).Text;
            cbIsActive.Checked = (grow.FindControl("lblIsActive") as CheckBox).Checked;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            btnClear.Text = "Cancel";
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            var cmd = new SqlCommand("sp_Region_CRUD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@RegionID", hidRegionID.Value);
            cmd.Parameters.AddWithValue("@RegionName", txtRegionName.Text);
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
                lblMessage.Text = "Cannot de-activate the Region Record as it is being referenced by Open/In-Progress tickets";
                ClearControls();
            }
            else if (result == 100)
            {
                lblMessage.Text = "Region Name already exists";
                txtRegionName.Focus();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (_con.State == ConnectionState.Open)
                _con.Close();
        }
    }

}