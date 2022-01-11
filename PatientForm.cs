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
    public partial class PatientForm : Form
    {
        public PatientForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\FAST\OneDrive\Documents\HMSLocaldb.mdf;Integrated Security=True;Connect Timeout=30");

      

       
        void populate()
        {
            Con.Open();
            string query = "Select * from Patienttbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PatientGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            if (Patid.Text == "" || PatName.Text == "" || PatAddress.Text == "" || PatPhone.Text == "" || PatAge.Text == "" || MajorD.Text == "")
            {
                MessageBox.Show("No Empty Fill Accepted");
            }

            
            else
            {
                Con.Open();
                string query = "insert into Patienttbl values (" + Patid.Text + ", '" + PatName.Text + "' ,' " + PatAddress.Text+ "', '" + PatPhone.Text + "', '"+PatAge.Text+"','"+Gender.SelectedItem.ToString()+"','"+BloodGp.SelectedItem.ToString()+"','"+MajorD.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Patient data successfully added");
                Con.Close();
                populate();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
private void PatientForm_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(Patid as int)),0)+1 from Patienttbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Patid.Text = dt.Rows[0][0].ToString();
            this.ActiveControl = Patid;
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Patid.Text == "")
                MessageBox.Show("Enter the Patient Id");
            else
            {
                Con.Open();
                string query = "Delete from Patienttbl where Patid=" + Patid.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Patient data Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void PatientGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Patid.Text = PatientGV.SelectedRows[0].Cells[0].Value.ToString();
            PatName.Text = PatientGV.SelectedRows[0].Cells[1].Value.ToString();
            PatAddress.Text = PatientGV.SelectedRows[0].Cells[2].Value.ToString();
            PatPhone.Text = PatientGV.SelectedRows[0].Cells[3].Value.ToString();
            PatAge.Text = PatientGV.SelectedRows[0].Cells[4].Value.ToString();
            MajorD.Text = PatientGV.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "Update Patienttbl set PatName='" +PatName.Text+ "',PatAddress='" +PatAddress.Text+ "',PatPhone='"+PatPhone.Text+"',PatAge='"+PatAge.Text+"',PatGender='"+Gender.SelectedItem.ToString()+"',PatBlood='"+BloodGp.SelectedItem.ToString()+"' where Patid=" + Patid.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Patient data Successfully Updated");
            Con.Close();
            populate();
        }
    }
}
