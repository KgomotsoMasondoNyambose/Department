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
using Microsoft.VisualBasic;

namespace Department_of_Road_and_Traffic
{
    public partial class Register : Form
    {
        // SqlConnection con = new SqlConnection("Data Source=DESKTOP-2EAQR2O\\SQLEXPRESS;Initial Catalog=DataDepart;Integrated Security=True");

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2EAQR2O\\SQLEXPRESS;Initial Catalog=Friday;Integrated Security=True");
        string number;

        string gen;
        int ix;

        public Register()
        {
            InitializeComponent();
        }

        private void txtRetypePassword_TextChanged(object sender, EventArgs e)
        {
            if(txtPassword.Text == txtRetypePassword.Text)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnViewPassword_Click(object sender, EventArgs e)
        {
            if(txtRetypePassword.UseSystemPasswordChar == true)
            {
                txtRetypePassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtRetypePassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
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

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
           
                
        }

        private void txtMonthlyContributions_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtMonthlyContributions.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

            number = txtNumber.Text;
            string AdminCode;;
            string admin = "UserLogin";

            

            if(txtFullname.Text == "")
            {
                MessageBox.Show("Please Enter your full name", "Information!");
                txtFullname.Focus();
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Enter Username", "Information!");
                txtUsername.Focus();
            }
            else if (txtNumber.Text == "")
            {
                MessageBox.Show("Enter Phone Please", "Information!");
                txtNumber.Focus();
            }
            else if (txtFemale.Text == "")
            {
                MessageBox.Show("Please atleast check Male or Female", "Information!");
                clbGender.Focus();
            }
            else if (txtRichAddress.Text == "")
            {
                MessageBox.Show("Enter Address Please", "Information!");
                txtRichAddress.Focus();
            }
            else if (txtMonthlyContributions.Text == "")
            {
                MessageBox.Show("Enter Monthly Contribution Please", "Information!");
                txtMonthlyContributions.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please Create Password");
                txtPassword.Focus();
            }
            else if (txtRetypePassword.Text == "")
            {
                MessageBox.Show("Enter Please", "Information!");
                txtRetypePassword.Focus();
            }
            else if (passwordError.Visible == true)
            {
                MessageBox.Show("Please check if the passwords match", "Information!");
                txtRetypePassword.Focus();
                txtPassword.Focus();
            }
            else if (number[0] == 0)
            {
                MessageBox.Show("Please make sure that the first number is zero (0)");
            }
            else
            {

                do
                {
                    AdminCode = Microsoft.VisualBasic.Interaction.InputBox("hello");
                } while (admin != AdminCode);
                //SqlCommand com = new SqlCommand("insert into table (fname, username, dob, cnumber, gender, address, contribution, password, rpassword, register) values ('"+txtFullname.Text+"', '"+txtUsername.Text+"', '"+DateOfBirth.Value.ToString()+"', '"+txtNumber.Text+"', '"+txtFemale.Text+"', '"+ txtRichAddress.Text+"', '"+txtMonthlyContributions.Text+"', '"+txtPassword.Text+"', '"+txtRetypePassword.Text+"', '"+RegistrationDate.Value.ToString()+"',)", con);
                con.Open();
                SqlCommand com = new SqlCommand("insert into driver(fullname,username,dob,password,r_password,gender,address,cellnumber,datereg)values('" + txtFullname.Text + "','" + txtUsername.Text + "','" + DateOfBirth.Value.ToString() + "','" + txtPassword.Text + "', '" + txtRetypePassword.Text + "', '" + txtFemale.Text + "', '" + txtRichAddress.Text + "', '" + txtNumber.Text + "', '"+RegistrationDate.Value.ToString()+"')", con);
                SqlDataAdapter da = new SqlDataAdapter("select count (*) from driver where username = '" + txtUsername.Text + "'", con);

                try
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("Username Exist already", "Warning!");
                    }
                    else
                    {
                        com.ExecuteNonQueryAsync();
                        MessageBox.Show("User added in database", "Information!");

                        login lg = new login();
                        this.Hide();
                        lg.Show();
                    }

                        //try
                        //{
                        //    com.ExecuteNonQueryAsync();
                        //    MessageBox.Show("User added in database", "Information!");

                        //    //login lg = new login();
                        //    //this.Hide();
                        //    //lg.Show();
                        //}
                        //catch (Exception)
                        //{

                        //    MessageBox.Show("Username Exist already", "Warning!");
                        //}

                    }
                catch
                {
                    MessageBox.Show("DataBase disconnected", "Error!");
                }
                con.Close();
            }
        }
        private void clbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void clbGender_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            for (ix = 0; ix < clbGender.Items.Count; ++ix)
            {
                if (ix != e.Index)
                {
                    clbGender.SetItemChecked(ix, false);
                    gen = "male";
                }
                else if (clbGender.CheckedItems != clbGender.CheckedItems)
                {
                    txtFemale.Text = "";
                }
                else
                {
                    gen = "female";
                }
            }
            txtFemale.Text = gen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login log = new login();
            this.Hide();
            log.Show();
        }

        private void DateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            DateTime from = DateOfBirth.Value;
            DateTime to = DateTime.Now;
            TimeSpan TSpan = to - from;
            double days = TSpan.TotalDays;
            lblAge.Text=(days / 365).ToString("0");
            int age = Convert.ToInt32(lblAge.Text);


            if(age < 18)
            {
                if (to<DateOfBirth.Value)
                {
                    MessageBox.Show("Please enter the appropiate date");
                }

                else
                {
                    MessageBox.Show("Sorry you can't register if you are less than 18 years old", "Information!");
                    lblAge.Text = "";
                    lblAge.Visible = false;
                }
            }
            
            else
            {
                lblAge.Text = (days / 365).ToString("0" + " Years old");
                lblAge.Visible = true;
            }
        }

        private void RegistrationDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
