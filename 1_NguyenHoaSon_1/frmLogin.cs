using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_NguyenHoaSon_1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public string username = "";
        public string password = "";
        public bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string strCommand = "Select * from Customers where EmailAddress = @email and Password = @password" ;
            string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();

            SqlConnection myConnection = new SqlConnection(strconnectstring);
            myConnection.Open();
            SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            sqlCmd.Parameters.AddWithValue("@email", user);
            sqlCmd.Parameters.AddWithValue("@password", pass);
            DataSet ds2 = new DataSet();
            da.Fill(ds2,"UserAccount");
            myConnection.Close();

            if (ds2.Tables["UserAccount"].Rows.Count > 0)
            {
                isValid = true;
            }

            return isValid;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(this.IsValidUser(txtUserName.Text,txtPassword.Text) == false)
            {
                MessageBox.Show("Invalid Username or Password!","Wrong Information",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.password = this.txtPassword.Text;
                this.username = this.txtUserName.Text;
                MessageBox.Show("Login Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(this.MdiParent is frmMDIMain)
                {
                    ((frmMDIMain)this.MdiParent).Text ="User: "+ this.password + " - Password: " + this.username;  
                }
                this.Close();
           
                
            }
        }

        private void chbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowPass.Checked == true)
            {
                this.txtPassword.UseSystemPasswordChar = false;

            }
            else
            {
                this.txtPassword.PasswordChar = '•';
                this.txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
