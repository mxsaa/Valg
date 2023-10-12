using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ValgSystem
{
    public partial class Vote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind both the DropDownList controls on the initial page load.
                BindDropDownListParti();
                BindDropDownListKommune();
            }
        }

        private void BindDropDownListParti()
        {
            // Bind parti DropDownList.
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Parti", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            DropDownListParti.DataSource = dt;
            DropDownListParti.DataTextField = "PartiName"; // parti Navn
            DropDownListParti.DataValueField = "Pid"; // partiID
            DropDownListParti.DataBind();
        }

        private void BindDropDownListKommune()
        {
            // Kommune DropDownList.
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Kommune", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            DropDownListKommune.DataSource = dt;
            DropDownListKommune.DataTextField = "Navn"; // vis kommune navn
            DropDownListKommune.DataValueField = "kommuneID"; // kommuneID
            DropDownListKommune.DataBind();
        }

        protected void ButtonVote_Click(object sender, EventArgs e)
        {
            string userIp = GetClientIpAddress();

            if (!HasUserVoted(userIp))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserVote (Pid, Kid, UserIp, VoteDateTime) VALUES (@pid, @kid, @userIp, GETDATE())", conn);
                    cmd.CommandType = CommandType.Text;

                    SqlParameter param = new SqlParameter("@pid", SqlDbType.Int);
                    param.Value = int.Parse(DropDownKommune.SelectedValue);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@kid", SqlDbType.Int);
                    param.Value = int.Parse(DropDownListParti.SelectedValue);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@userIp", SqlDbType.NVarChar, 255);
                    param.Value = userIp;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else
            {
                // User has already voted, show an error message or redirect as needed.
            }
        }

        private string GetClientIpAddress()
        {
            // Get the user's IP address
            string userIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIp))
                userIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return userIp;
        }

        private bool HasUserVoted(string userIp)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UserVote WHERE UserIp = @userIp", conn);
                cmd.CommandType = CommandType.Text;

                SqlParameter param = new SqlParameter("@userIp", SqlDbType.NVarChar, 255);
                param.Value = userIp;
                cmd.Parameters.Add(param);

                int voteCount = (int)cmd.ExecuteScalar();
                conn.Close();

                return voteCount > 0;
            }
        }
    }
}
