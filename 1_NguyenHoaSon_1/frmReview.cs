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
    public partial class frmReview : Form
    {
        public clsReview currentReview;
        public bool isAdd = false;
        public bool isSearch = false;
        public frmReview()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strSearch = "'%" + this.txtSearch.Text + "%'";
                string strCommand = "Select * from Reviews where CustomerName like " + strSearch + " or CustomerEmail like " + strSearch + " or Comments like " + strSearch;
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
                if (!isSearch)
                {
                    MessageBox.Show("Kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds, "Table 1");
                this.dgvReviews.DataSource = ds;
                this.dgvReviews.DataMember = "Table 1";
                isSearch = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvReviews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgvReviews.Rows[e.RowIndex];
            if (dr != null)
            {
                this.currentReview = new clsReview();
                this.currentReview.reviewID = int.Parse(dr.Cells["ReviewID"].Value.ToString());
                this.currentReview.productID = int.Parse(dr.Cells["ProductID"].Value.ToString());
                this.currentReview.rating = int.Parse(dr.Cells["Rating"].Value.ToString());
                this.currentReview.customerEmail = dr.Cells["CustomerEmail"].Value.ToString();
                this.currentReview.customerName = dr.Cells["CustomerName"].Value.ToString();
                this.currentReview.comments = dr.Cells["Comments"].Value.ToString();

                this.txtReviewID.Text = this.currentReview.reviewID.ToString();
                this.txtProductID.Text = this.currentReview.productID.ToString();
                this.txtRating.Text = this.currentReview.rating.ToString();
                this.txtCustomerName.Text = this.currentReview.customerName.ToString();
                this.txtEmail.Text = this.currentReview.customerEmail.ToString();
                this.txtComments.Text = this.currentReview.comments.ToString();

                //this.lblListHeader.Text = "Review ID: " + this.currentReview.reviewID.ToString() +
                //                          "--Product ID: " + this.currentReview.productID.ToString() +
                //                          "--CustomerName: " + this.currentReview.customerName.ToString() +
                //                          "--CustomerEmail: " + this.currentReview.customerEmail.ToString() +
                //                          "--rating: " + this.currentReview.rating.ToString() +
                //                          "--Comments: " + this.currentReview.comments.ToString();



            }
        }

   
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmReviewsInfo frm = new frmReviewsInfo(true);
            frm.ShowDialog();
            frmReview_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmReviewsInfo frm = new frmReviewsInfo(false);
            frm.currentReview = this.currentReview;
            frm.ShowDialog();
            frmReview_Load(sender, e);
        }

        private void DeleteReview()
        {
            string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
            string strCommand = "Delete from Reviews where ReviewID = " + this.currentReview.reviewID;
            SqlConnection myConnection = new SqlConnection(strconnectstring);
            myConnection.Open();
            SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

    

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa review  " +this.currentReview.reviewID.ToString() + "?", "Xóa review", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DeleteReview();
                    frmReview_Load(sender, e);
                }
            } catch (Exception ex)
            {
              
                MessageBox.Show("Error occured: " + ex.Message.ToString());
            }
        }

   

        private void frmReview_Load(object sender, EventArgs e)
        {
            if (isSearch)
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strSearch = "'%" + this.txtSearch.Text + "%'";
                string strCommand = "Select * from Reviews where CustomerName like " + strSearch + " or CustomerEmail like " + strSearch + " or Comments like " + strSearch;
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds, "Table 1");
                this.dgvReviews.DataSource = ds;
                this.dgvReviews.DataMember = "Table 1";
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport("'%" + this.txtSearch.Text + "%'");
            frmReport.ShowDialog();
        }
    }
}
