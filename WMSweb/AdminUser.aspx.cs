﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSobjects;

public partial class AdminUser : System.Web.UI.Page
{
    SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserBO"] == null)
        {
            Response.Redirect("Login.aspx", false);
        }
        else
        {

            var userBo = new UserBo();
            userBo = (UserBo)Session["UserBO"];
            if (userBo.RoleId == 1)
            {              
                if (!IsPostBack)
                {
                    FillGrid();
                    Fillddl(ddlBranch);
                    FillddlRole(ddlRole);
                }
            }
            else
            {
                Response.Redirect("Dashboard.aspx", false);
            }
           

        }
    }

    private void FillddlRole(DropDownList ddl)
    {
        try
        {
            using (var cmd = new SqlCommand("sp_User_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECTROLE");
                using (var sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    sda.SelectCommand = cmd;
                    using (var dt = new DataTable())
                    {
                        sda.Fill(dt);
                        ddl.DataSource = dt;
                        ddl.DataTextField = "RoleName";
                        ddl.DataValueField = "RoleId";
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

    private void Fillddl(DropDownList ddl)
    {
        try
        {
            using (var cmd = new SqlCommand("sp_User_CRUD"))
            {
                cmd.Parameters.AddWithValue("@Action", "SELECTBR");
                using (var sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    sda.SelectCommand = cmd;
                    using (var dt = new DataTable())
                    {
                        sda.Fill(dt);
                        ddl.DataSource = dt;
                        ddl.DataTextField = "BranchName";
                        ddl.DataValueField = "BranchID";
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
            using (var cmd = new SqlCommand("sp_User_CRUD"))
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var cmd = new SqlCommand("sp_User_CRUD") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
            cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@RoleId", ddlRole.SelectedValue);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
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
                lblMessage.Text = "User Name already exists";
                txtUserName.Focus();
            }
            else if (result == 101)
            {
                lblMessage.Text = "User with same Role already exists in the branch";
                ddlRole.Focus();
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

    private void ClearControls()
    {
        try
        {
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
          //  cbIsActive.Checked = false;
            hidUserId.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            ddlRole.ClearSelection();
            ddlBranch.ClearSelection();
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
            var cmd = new SqlCommand("sp_User_CRUD") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@UserId", hidUserId.Value);
            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
            cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@RoleId", ddlRole.SelectedValue);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
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
                lblMessage.Text = "Cannot de-activate the User as it is being referenced by Open/In-Progress tickets";
                ClearControls();
            }
            else if (result == 100)
            {
                lblMessage.Text = "User Name already exists";
                txtUserName.Focus();
            }
            else if (result == 101)
            {
                lblMessage.Text = "User with same Role already exists in the branch";
                txtUserName.Focus();
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
        try
        {
            datagrid.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception)
        {

            throw;
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

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();

            var btn = sender as LinkButton;
            var grow = btn.NamingContainer as GridViewRow;
            hidUserId.Value = (grow.FindControl("lblUserId") as Label).Text;
            var sqlquery = "Select UserID,UserName,BranchID,RoleID,EmailID,PhoneNumber,IsActive from tblm_User where UserID=" + hidUserId.Value;
            _con.Open();
            var cmd = new SqlCommand(sqlquery, _con);
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtUserName.Text = dr["UserName"].ToString();
                ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(dr["BranchID"].ToString()));
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(dr["RoleID"].ToString()));
                txtEmail.Text = dr["EmailID"].ToString();
                txtPhone.Text = dr["PhoneNumber"].ToString();                
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
}