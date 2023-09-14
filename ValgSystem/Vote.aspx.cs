using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValgSystem
{
    public partial class Vote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDropDownListParti();
        }

        private void BindDropDownListParti()//dette blir en select *. det som returneres skal bindes til dropdown 
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Parti", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            //loope gjennom datatable for å hente ut partinavn. lage et dropdownitem og putte navnet i det
            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem(row["Parti"].ToString(), row["Pid"].ToString());//hente ut verdier
                DropDownListParti.Items.Add(item);//legge item i lista
            }

            //DropDownListParti.DataSource= dt;
            DropDownListParti.DataBind();
        }

        protected void ButtonVote_Click(object sender, EventArgs e)
        {
            SqlParameter param;
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Vote (Kid,Pid) Values(@kid,@pid)", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;

                param=new SqlParameter("@kid",SqlDbType.Int);
                param.Value = 1; //hardcode verdien til Fredrikstad. Denne må endres om det kan velges kommune fra en liste
                cmd.Parameters.Add(param);

                param = new SqlParameter("@pid", SqlDbType.Int);
                param.Value = int.Parse(DropDownListParti.SelectedValue);//her har rekkefølgen på lista no e å si
                cmd.Parameters.Add(param);

                //SqlDataReader reader = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();
                //dt.Load(reader);
                //reader.Close();
                conn.Close();
            }
            
        }
    }
}