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
    public partial class frmReviewsInfo : Form
    {
        public clsReview currentReview;
        public bool isAdd = false;
        public frmReviewsInfo()
        {
            InitializeComponent();
        }
        public frmReviewsInfo(bool isAdd)
        {
            this.isAdd = isAdd;
            InitializeComponent();

        }
        private void ResetText()
        {
            this.txtReviewID.Text = "";
            this.txtProductID.Text = "";
            this.txtRating.Text = "";
            this.txtCustomerName.Text = "";
            this.txtEmail.Text = "";
            this.txtComments.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isAdd)
            {
                AddReviews();
                DialogResult dr = MessageBox.Show("Bạn có muốn thêm tiếp không?", "Tiếp tục", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {

                    ResetText();
                }
                else this.Close();
            }
            else
            {
                EditReviews();
                MessageBox.Show("Sửa thành công!");
                this.Close();
            }


        }
        private void AddReviews()
        {
            try
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strCommand = "Insert into Reviews(ProductID, CustomerName, CustomerEmail, Rating, Comments) values(@productid, @name, @email, @rating, @comments)";
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);

                sqlCmd.Parameters.AddWithValue("@productid", this.txtProductID.Text);
                sqlCmd.Parameters.AddWithValue("@name", this.txtCustomerName.Text);
                sqlCmd.Parameters.AddWithValue("@email", this.txtEmail.Text);
                sqlCmd.Parameters.AddWithValue("@rating", this.txtRating.Text);
                sqlCmd.Parameters.AddWithValue("@comments", this.txtComments.Text);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());

            }
        }

        private void EditReviews()
        {
            try
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strCommand = "Update Reviews set ProductID = @productid, CustomerName = @name, CustomerEmail = @email, Rating = @rating, Comments = @comments where ReviewID = @reviewid";
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);

                sqlCmd.Parameters.AddWithValue("@productid", this.txtProductID.Text);
                sqlCmd.Parameters.AddWithValue("@name", this.txtCustomerName.Text);
                sqlCmd.Parameters.AddWithValue("@email", this.txtEmail.Text);
                sqlCmd.Parameters.AddWithValue("@rating", this.txtRating.Text);
                sqlCmd.Parameters.AddWithValue("@comments", this.txtComments.Text);
                sqlCmd.Parameters.AddWithValue("@reviewid", this.currentReview.reviewID.ToString());

                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());

            }
        }

        private void frmReviewsInfo_Load(object sender, EventArgs e)
        {
            this.txtReviewID.Enabled = false;
            if (isAdd)
            {
                this.lblSelectedHeader.Text = "Add Review";
            }
            else
            {
                this.lblSelectedHeader.Text = "Edit Review";
                this.txtReviewID.Text = this.currentReview.reviewID.ToString();
                this.txtProductID.Text = this.currentReview.productID.ToString();
                this.txtRating.Text = this.currentReview.rating.ToString();
                this.txtCustomerName.Text = this.currentReview.customerName.ToString();
                this.txtEmail.Text = this.currentReview.customerEmail.ToString();
                this.txtComments.Text = this.currentReview.comments.ToString();
            }
        }
    }
}
