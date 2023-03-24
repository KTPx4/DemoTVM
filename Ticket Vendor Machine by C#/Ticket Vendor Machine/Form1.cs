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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace Ticket_Vendor_Machine
{
    public partial class TicketM : Form
    {
        SqlConnection cn;
        SqlDataAdapter data;
        SqlCommand cm;
        DataTable tb;
        String SumPr = "0";
        public TicketM()
        {
            InitializeComponent();
        }

        private void TicketM_Load(object sender, EventArgs e)
        {
            //string s = "initial catalog = Demo; data source = DESKTOP-VGVFSE4\\SQLEXPRESS; integrated security = true";
            //cn = new SqlConnection(s);
            //  cn.Open();
            cn = new SqlConnection(Program.strConn);
            cn.Open();
            cmbTran.Focus();

            Style();

           
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
        void Style()
        {
            btnCre.FlatStyle = FlatStyle.Flat;
            btnCre.FlatAppearance.BorderSize = 0;
            btnCre.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCre.Width, btnCre.Height, 12, 12));
            btnCre.BackColor = ColorTranslator.FromHtml("#3D44E0");
            btnCre.ForeColor = Color.White;
            
            btnQR.FlatStyle = FlatStyle.Flat;
            btnQR.FlatAppearance.BorderSize = 0;
            btnQR.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnQR.Width, btnQR.Height, 12, 12));
            btnQR.BackColor = ColorTranslator.FromHtml("#DE54C0");
            btnQR.ForeColor = Color.White;

            btnRef.FlatStyle = FlatStyle.Flat;
            btnRef.FlatAppearance.BorderSize = 0;
            btnRef.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRef.Width, btnRef.Height, 12, 12));
            btnRef.BackColor = ColorTranslator.FromHtml("#18CA75");
            btnRef.ForeColor = Color.White;

            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#94C8F8");
            
            treeView1.BackColor = System.Drawing.ColorTranslator.FromHtml("#94C8F8");
            lbTicket.ForeColor = ColorTranslator.FromHtml("#F51CAB");

            cmbTran.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            cmbDes.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            txtQuan.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            
            label1.Width = 2;
            label1.Height = 500;

            // Tạo một đường viền hình tròn
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, ptbAvt.Width - 1, ptbAvt.Height - 1);

            // Gán đường viền cho thuộc tính Region của PictureBox
            ptbAvt.Region = new Region(path);
        }

        private Font customIconFont = new Font("Segoe UI Symbol", 12);
        private Brush customIconBrush = new SolidBrush(Color.DarkGray);
        private int customIconSize = 10; // kích thước biểu tượng

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        int getQuan(string Quan)
        {
            return int.Parse(Quan);
            //return 0;
        }
        float getPrice(string Des)
        {
            String[] arrDes = Des.Split('-');
            return float.Parse(arrDes[2].Trim());
           // return 0;
        }
        string sumPrice(int Quan, float price)
        {
            return ((float)Quan * price).ToString("0.000");
        }
        private void btnCre_Click(object sender, EventArgs e)
        {
            SumPr = "0";
            if (cmbTran.Text == "")
            {
                MessageBox.Show("Please Select type of Transport!");
                cmbTran.Focus();
                cmbTran.DroppedDown = true;
                return;
            }
            if (cmbDes.Text == "")
            {
                MessageBox.Show("Please Select Destination in list!");
                cmbDes.Focus();
                cmbDes.DroppedDown = true;
                return;
            }
            if (txtQuan.Text == "")
            {
                MessageBox.Show("Please input Quantity!");
                txtQuan.Focus();
                return;
            }
            float price = getPrice(cmbDes.Text);
            int Quan = getQuan(txtQuan.Text);
            SumPr = sumPrice(Quan, price);
            MessageBox.Show("The amount you need to pay is: " + SumPr);

            bool result = false;
            using (Credit form2 = new Credit())
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {

                    result = form2.Result;
                }
            }
            if (result)
            {
                MessageBox.Show("Your Ticket: XXXXXXXXXXXXXXXX");
                return;
            }
            else
            {
                MessageBox.Show("Please Payment again!");
                return;
            }
        }

        private void cmbTran_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDes.Items.Clear();
            cmbDes.Text = "";
            cmbDes.Focus();
            cmbDes.DroppedDown = true;
            cmbDes.Items.Add("Destination - Distance - Price");
            String tran = cmbTran.Text;
            String s = "Select nameDes, Distance, Price from Destinations where Transport like '" + tran + "'";
            data = new SqlDataAdapter(s, cn);
            tb = new DataTable();
            data.Fill(tb);

            String nameDes = "";
            float distance;
            String price = "0";
            String item = "";
            foreach (DataRow row in tb.Rows)
            {
                 nameDes = row["nameDes"].ToString();
                 distance = float.Parse(row["Distance"].ToString());
                 price = (row["Price"]).ToString();
                 item = nameDes + " - " + distance +" - " + price;
                cmbDes.Items.Add(item);
            }
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Program.strConn);
            cmbDes.Items.Clear();
            cmbDes.Text = "";
            cmbTran.Text = "";
            txtQuan.Text = "";
            cmbTran.Focus();
        }

        private void cmbDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDes.SelectedIndex == 0)
            {
                // Đặt lại giá trị được chọn
                cmbDes.SelectedIndex = -1;
            }
            txtQuan.Focus();
        }

        private void txtQuan_TextChanged(object sender, EventArgs e)
        {
            if (txtQuan.Text == null || txtQuan.Text is null || txtQuan.Text == "")
                return;

            int n;
            bool isNumeric = int.TryParse(txtQuan.Text, out n);

            if (!isNumeric)
            {

                MessageBox.Show("Quantity only number!");
                txtQuan.Text = null;
                txtQuan.Focus();
            }
            else
            {
                if (int.Parse(txtQuan.Text) < 1)
                {
                    MessageBox.Show("Please input Quantity > 0");
                    txtQuan.Text = null;
                    txtQuan.Focus();
                }
            }
        }

        private void btnQR_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The system is developing, please try again later!");
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




        /*private void cmbTran_SelectedIndexChanged(object sender, EventArgs e)
{
   String tran = cmbTran.Text;
   String s = "Select nameDes, Distance, Price from Destinations where Transport like '" + tran +"'";
   showData(s);

}
public void showData(string s)
{
   data = new SqlDataAdapter(s, cn);
   tb = new DataTable();
   data.Fill(tb);
   dGVDes.DataSource = tb;
   dGVDes.Columns["nameDes"].HeaderText = "location";
   dGVDes.Columns["Distance"].HeaderText = "Distance";
   dGVDes.Columns["Price"].HeaderText = "Price";
}

private void txtQuan_TextChanged(object sender, EventArgs e)
{
   if (txtQuan.Text == null || txtQuan.Text is null || txtQuan.Text == "")
       return;

   int n;
   bool isNumeric = int.TryParse(txtQuan.Text, out n);

   if (!isNumeric)
   {

       MessageBox.Show("Invalid Quantity!");
       txtQuan.Text = null;
       txtQuan.Focus();
   }
   else
   {
       if(int.Parse(txtQuan.Text) < 1)
       {
           MessageBox.Show("Please input Quantity > 0");
           txtQuan.Text = null;
           txtQuan.Focus();
       }    
   }

}

private void dGVDes_CellClick(object sender, DataGridViewCellEventArgs e)
{
   int index = dGVDes.CurrentRow.Index;
   String des = dGVDes.Rows[index].Cells[0].Value.ToString();
   txtDes.Text = des;
}

private void btnRef_Click(object sender, EventArgs e)
{
   cmbTran.Text = null;
   cmbTran.Focus();
   txtDes.Text = null;
   txtQuan.Text = null;
   dGVDes.DataSource = null;
}

private void btnCredit_Click(object sender, EventArgs e)
{
   if(cmbTran.Text == "")
   {
       MessageBox.Show("Please Select type of Transport!");
       cmbTran.Focus();
       return;
   }
   if (txtDes.Text == "")
   {
       MessageBox.Show("Please Select Destination by Click in list!");
       dGVDes.Focus();
       return;
   }
   if(txtQuan.Text == "")
   {
       MessageBox.Show("Please input Quantity!");
       txtQuan.Focus();
       return;
   }
   bool result = false;
   using (Credit form2 = new Credit())
   {
       if (form2.ShowDialog() == DialogResult.OK)
       {

            result = form2.Result;
       }
   }
   if(result)
   {
       MessageBox.Show("Your Ticket: XXXXXXXXXXXXXXXX");
       return;
   }
   else
   {
       MessageBox.Show("Please Payment again!");
       return;
   }
}

private void btnQRcode_Click(object sender, EventArgs e)
{
   if (cmbTran.Text == "")
   {
       MessageBox.Show("Please Select type of Transport!");
       cmbTran.Focus();
       return;
   }
   if (txtDes.Text == "")
   {
       MessageBox.Show("Please Select Destination by Click in list!");
       dGVDes.Focus();
       return;
   }
   if (txtQuan.Text == "")
   {
       MessageBox.Show("Please input Quantity!");
       txtQuan.Focus();
       return;
   }
   MessageBox.Show("QR code is work!");

}*/
    }
}
