using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticket_Vendor_Machine
{
    public partial class Credit : Form
    {
        SqlConnection cn;
        SqlDataAdapter data;
        SqlCommand cm;
        DataTable tb;
        int timeCheck = 0;
        public bool Result { get; set; }
        public Credit()
        {
            InitializeComponent();
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
     (
         int nLeftRect,
         int nTopRect,
         int nRightRect,
         int nBottomRect,
         int nWidthEllipse,
         int nHeightEllipse
     );
        private void Credit_Load(object sender, EventArgs e)
        {
            //string s = "initial catalog = Demo; data source = DESKTOP-VGVFSE4\\SQLEXPRESS; integrated security = true";
            cn = new SqlConnection(Program.strConn);
            cn.Open();
            grbCreditCard.Visible = false;
            btnPhysical.Focus();
            Style();
        }
        void Style()
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#5084D1");

            btnPhysical.FlatStyle = FlatStyle.Flat;
            btnPhysical.FlatAppearance.BorderSize = 0;
            btnPhysical.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnPhysical.Width, btnPhysical.Height, 12, 12));
            btnPhysical.BackColor = System.Drawing.ColorTranslator.FromHtml("#2B2E4A");
            btnPhysical.ForeColor = Color.White;

            btnInput.FlatStyle = FlatStyle.Flat;
            btnInput.FlatAppearance.BorderSize = 0;
            btnInput.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnInput.Width, btnInput.Height, 12, 12));
            btnInput.BackColor = System.Drawing.ColorTranslator.FromHtml("#2B2E4A");
            btnInput.ForeColor = Color.White;

            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnOk.Width, btnOk.Height, 12, 12));
            btnOk.BackColor = System.Drawing.ColorTranslator.FromHtml("#339933");
            btnOk.ForeColor = Color.White;

            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 12, 12));
            btnCancel.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff3333");
            btnCancel.ForeColor = Color.White;

            label1.Width = 2;
            label1.Height = 500;

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, ptbAvt.Width - 1, ptbAvt.Height - 1);

            // Gán đường viền cho thuộc tính Region của PictureBox
            ptbAvt.Region = new Region(path);
        }

        private void btnPhysical_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payment successfull!");
            this.DialogResult = DialogResult.OK;
            Result = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text == "")
            {
                MessageBox.Show("Please input number card!");
                txtNumber.Focus();
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Please input your name!");
                txtName.Focus();
                return;
            }
            if (txtExp.Text == "")
            {
                MessageBox.Show("Please input expiration date!");
                txtExp.Focus();
                return;
            }
            if (txtCvv.Text == "")
            {
                MessageBox.Show("Please input number cvv!");
                txtCvv.Focus();
                return;
            }
            String number, s, name, exp, cvv;
            number = txtNumber.Text;
            name = txtName.Text;
            exp = txtExp.Text;
            cvv = txtCvv.Text;
            s = "Select * from CreditCard where IDcredit = '" + number + "' and NameUser = '" + name + "' and expDate = '" + exp + "' and cvv = '" + cvv + "'";

            data = new SqlDataAdapter(s, cn);
            tb = new DataTable();
            data.Fill(tb);
            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Your Credit Card is Invalid!");
                Result = false;
                timeCheck++;
                if (timeCheck >= 5)
                {
                    MessageBox.Show("You have entered more than 5 times, please try again later!");
                    this.Close();
                }
            }
            else
            {

                MessageBox.Show("Payment successfull!");
                this.DialogResult = DialogResult.OK;
                Result = true;
                this.Close();
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            txtNumber.Text = "";
            txtName.Text = "";
            txtExp.Text = "";
            txtCvv.Text = "";
            grbCreditCard.Visible = true;

        }
        private void ptbFb_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/px4.k3");
        }

        private void ptbWriter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/");
        }

        private void ptbPhone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My phone is: xxxxxxxxxx");
        }
        /* private void button2_Click(object sender, EventArgs e)
         {
             if(txtNumber.Text == "")
             {
                 MessageBox.Show("Please input number card!");
                 txtNumber.Focus();
                 return;
             }
             if (txtName.Text == "")
             {
                 MessageBox.Show("Please input your name!");
                 txtName.Focus();
                 return;
             }
             if (txtExp.Text == "")
             {
                 MessageBox.Show("Please input expiration date!");
                 txtExp.Focus();
                 return;
             }
             if (txtCvv.Text == "")
             {
                 MessageBox.Show("Please input number cvv!");
                 txtCvv.Focus();
                 return;
             }
             String number, s, name, exp, cvv;
             number = txtNumber.Text;
             name = txtName.Text;
             exp = txtExp.Text;
             cvv = txtCvv.Text;
             s = "Select * from CreditCard where IDcredit = '" + number + "' and NameUser = '" + name + "' and expDate = '" + exp + "' and cvv = '" + cvv +"'";

             data = new SqlDataAdapter(s, cn);
             tb = new DataTable();
             data.Fill(tb);
             if(tb.Rows.Count == 0 )
             {
                 MessageBox.Show("Your Credit Card is Invalid!");
                 Result = false;
                 timeCheck++;
                 if(timeCheck >= 5)
                 {
                     this.Close();
                 }
             }
             else
             {

                 MessageBox.Show("Payment successfull!");
                 this.DialogResult = DialogResult.OK;
                 Result = true;               
                 this.Close();
             }

         }

         private void button1_Click(object sender, EventArgs e)
         {
             MessageBox.Show("Payment successfull!");
             this.DialogResult = DialogResult.OK;
             Result = true;
             this.Close();
         }*/
    }
}
