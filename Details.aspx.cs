using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

public partial class Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label6.Text = Request.QueryString["ResId"];
        

        string location = Label6.Text.ToString();

        string ConnectString = "datasource= localhost; username=root; password=; database=isddb";
        MySqlConnection DBconnect = new MySqlConnection(ConnectString);
        DBconnect.Open();

        MySqlDataAdapter da;
        MySqlCommandBuilder builder;

        MySqlCommand command;
        string sql = "select * from restbl inner join rate on restbl.resid = rate.resid where rate.resid=  '" + location + "' ";
        //string sql = "Select resid,resname,address,location from restbl where resid = '" + location + "' ";

        da = new MySqlDataAdapter(sql, ConnectString);
        DataTable dt = new DataTable();
        //ds = new DataSet();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }

        else
        {
            Response.Redirect("~/DetailsReview.aspx?ResId=" + Label6.Text);
        }
        DBconnect.Close();

        
        DBconnect.Open();

        //MySqlDataAdapter da;
       // MySqlCommandBuilder builder;

        MySqlCommand commanding;
        string insertQuery = "Select * from review where resid = '" + location + "' ";

        MySqlDataAdapter dy = new MySqlDataAdapter(insertQuery, ConnectString);
        DataTable dx = new DataTable();
        //ds = new DataSet();
        dy.Fill(dx);

        DataList2.DataSource = dx;
        DataList2.DataBind();

        DBconnect.Close();

    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "reserve")
        {
            string v = e.CommandArgument.ToString();
            Response.Redirect("~/ReservationForm.aspx?ResId=" + v);
        }

        else if (e.CommandName == "menu")
        {
            string v = e.CommandArgument.ToString();
            Response.Redirect("~/Menushow.aspx?ResId=" + v);
        }
        else if (e.CommandName == "rate")
        {
            string v = e.CommandArgument.ToString();
            Response.Redirect("~/RatingForm.aspx?ResId=" + v);
        }
    }
}