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
    public partial class frmReport : Form
    {
        public string strSearching = "";
        public frmReport()
        {
            InitializeComponent();
        }
        public frmReport(string strSearching)
        {
            InitializeComponent();
            this.strSearching = strSearching;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {

            string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
            string strCommand = "Select * from Reviews where CustomerName like " + strSearching + " or CustomerEmail like " + strSearching + " or Comments like " + strSearching;
            SqlConnection myConnection = new SqlConnection(strconnectstring);
            myConnection.Open();
            SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds, "Table 1");
            ds.WriteXml("Review.xml");
            rptReview rptReview = new rptReview();
            rptReview.SetDataSource(ds);
            this.crvReview.ReportSource = rptReview;
            myConnection.Close();

        }
    }
}
