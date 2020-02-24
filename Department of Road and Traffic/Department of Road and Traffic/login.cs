using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Department_of_Road_and_Traffic
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void txtRetypePassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtRetypePassword.Text)
            {
                passwordMatch.Visible = true;
                passwordError.Visible = false;
            }
            else
            {
                passwordError.Visible = true;
                passwordMatch.Visible = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-2EAQR2O\\SQLEXPRESS;Initial Catalog=Friday;Integrated Security=True");

            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select count (*) from driver where username = '"+txtUsername.Text+"'",con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (txtUsername.Text == "" || txtPassword.Text == "" || txtRetypePassword.Text =="")
            {
                MessageBox.Show("Please enter your details", "Warning");
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "1" )
                {
                    MessageBox.Show("Successfully Logged in", "Information!");
                }
                else
                {
                    MessageBox.Show("Incorrect password or username", "Warning!");
                }
            }

            con.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtRetypePassword.UseSystemPasswordChar == true)
            {
                txtRetypePassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtRetypePassword.UseSystemPasswordChar = true;
            }
        }
    }
}
