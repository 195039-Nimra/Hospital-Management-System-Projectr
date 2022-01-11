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
    public partial class DiagonsisForm : Form
    {
        public DiagonsisForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\FAST\OneDrive\Documents\HMSLocaldb.mdf;Integrated Security=True;Connect Timeout=30");
        

        void populatecombo()
        {
            string sql = "select * from PatientTbl";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("PatId", typeof(int));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                Patientidtb.ValueMember = "PatId";
                Patientidtb.DataSource = dt;
                Con.Close();
            
             }
	       catch
	      {
	         
	      }
	    }
        string patname;
        void fecthpatientname()
        {
            Con.Open();
            string mysql = "select * from Patienttbl where Patid ="+Patientidtb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(mysql, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da =new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                patname = dr["PatName"].ToString();
                PatientTb.Text = patname;
            }
            Con.Close();
        }
        void populate()
        {
            Con.Open();
            string query = "Select * from Diagonsistbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DiagnosisGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                if (DiagId.Text == "" || MedicineTb.Text =="" || Diagnosistb.Text == "" || PatientTb.Text == "" )

                    MessageBox.Show("No Empty Fill Accepted");

            else
            {
                Con.Open();
               string query = "insert into Diagonsistbl values(" + DiagId.Text + " ," + Patientidtb.SelectedValue.ToString()+ ",'" + PatientTb.Text + "','" + Symptomstb.Text + "','" + Diagnosistb.Text + "','" + MedicineTb.Text+ "')";
               SqlCommand cmd = new SqlCommand(query, Con);
               cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Successfully Added");
                Con.Close();
                populate();
            }

        }

        private void DiagonsisForm_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(DiagId as int)),0)+1 from Diagonsistbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DiagId.Text = dt.Rows[0][0].ToString();
            this.ActiveControl = DiagId;
            
            populatecombo();
            populate();
        }

        private void PatientidTb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fecthpatientname();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DiagId.Text == "")
                MessageBox.Show("Enter the Diagnosis Id");
            else
            {
                Con.Open();
                string query = "Delete from Diagonsistbl where DiagId=" +DiagId.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagonsis data Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void DiagnosisGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DiagId.Text = DiagnosisGV.SelectedRows[0].Cells[0].Value.ToString();
            Patientidtb.SelectedValue= DiagnosisGV.SelectedRows[0].Cells[1].Value.ToString();
            PatientTb.Text = DiagnosisGV.SelectedRows[0].Cells[2].Value.ToString();
            Symptomstb.Text = DiagnosisGV.SelectedRows[0].Cells[3].Value.ToString();
           Diagnosistb.Text = DiagnosisGV.SelectedRows[0].Cells[4].Value.ToString();
            MedicineTb.Text = DiagnosisGV.SelectedRows[0].Cells[5].Value.ToString();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "Update Diagonsistbl set PatId="+Patientidtb.SelectedValue.ToString()+",PatName='" + PatientTb.Text+ "',Symptoms='" +Symptomstb.Text + "',Diagnosis='" +Diagnosistb.Text + "',Medicines='" + MedicineTb.Text+ "' where DiagId=" +DiagId.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Diagnosis Successfully Updated");
            Con.Close();
            populate();
        }

        private void DiagId_TextChanged(object sender, EventArgs e)
        {

        }

        private void DiagnosisLbl_Click(object sender, EventArgs e)
        {

        }

        private void DiagnosisTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PatientNameLbl_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
    

