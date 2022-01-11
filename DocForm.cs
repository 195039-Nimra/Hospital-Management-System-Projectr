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

namespace HLMS
{
    public partial class DocForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\FAST\OneDrive\Documents\HMSLocaldb.mdf;Integrated Security=True;Connect Timeout=30");

        public DocForm()
        {
            InitializeComponent();
        }
        void populate()
        {
            Con.Open();
            string query = "Select * from Doctbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DocGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        

        private void DocForm_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(DocID as int)),0)+1 from Doctbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DocID.Text = dt.Rows[0][0].ToString();
            this.ActiveControl = DocID;
            populate();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DocID.Text == "" || DocName.Text == "" || Password.Text == "" || DocExp.Text == "")
                MessageBox.Show("No Empty Fill Accepted");
            else
            {
                Con.Open();
                string query = "insert into Doctbl values (" + DocID.Text + ", '" + DocName.Text + "' ," + DocExp.Text + ", '" + Password.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Doctor data successfully added");
                Con.Close();
                populate();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (DocID.Text == "")
                MessageBox.Show("Enter the Doctor Id");
            else
            {
                Con.Open();
                string query = "Delete from Doctbl where DocID=" + DocID.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Doctor Successfully Deleted");
                Con.Close();
                populate();
            }
                 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "Update Doctbl set DocName='"+DocName.Text+"',DocExp='"+DocExp.Text+"',Password='"+Password.Text+"' where DocID="+DocID.Text+"";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Doctor Successfully Updated");
            Con.Close();
            populate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Home Hom = new Home();
            Hom.Show();
            this.Hide();
        }

        private void Docid_TextChanged(object sender, EventArgs e)
        {

        }

        private void DocGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DocID.Text = DocGV.SelectedRows[0].Cells[0].Value.ToString();
            DocName.Text = DocGV.SelectedRows[0].Cells[1].Value.ToString();
            DocExp.Text = DocGV.SelectedRows[0].Cells[2].Value.ToString();
            Password.Text = DocGV.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
























