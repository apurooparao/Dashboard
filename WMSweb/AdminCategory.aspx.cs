﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminCategory : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtCategoryName.Focus();
            if (!IsPostBack)
            {
                FillGrid();
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
            using (SqlCommand cmd = new SqlCommand("sp_Category_CRUD"))
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
        catch (Exception)
        {
            throw;
        }
    }

    void ClearControls()
    {
        try
        {
            txtCategoryName.Text = "";
            cbIsActive.Checked = false;
            hidCategoryID.Value = "";
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
            SqlCommand cmd = new SqlCommand("sp_Category_CRUD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
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
                lblMessage.Text = "Category Name already exists";
                txtCategoryName.Focus();
            }

        }
        catch (Exception)
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
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidCategoryID.Value = (grow.FindControl("lblCategoryID") as Label).Text;
            txtCategoryName.Text = (grow.FindControl("lblCategoryName") as Label).Text;
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
            SqlCommand cmd = new SqlCommand("sp_Category_CRUD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@CategoryID", hidCategoryID.Value);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
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
                lblMessage.Text = "Cannot de-activate the Category Record as it is being referenced by Open/In-Progress tickets";
                ClearControls();
            }
            else if (result == 100)
            {
                lblMessage.Text = "Category Name already exists";
                txtCategoryName.Focus();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
}