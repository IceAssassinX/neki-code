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

namespace faks
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            PuniGrid();
        }

        SqlConnection Kon = new SqlConnection(@"Data Source=DESKTOP-54H1JRH\SQLEXPRESS;Initial Catalog=4EIT_A7_FakultetskaEvidencija;Integrated Security=True"); 
        SqlCommand kom = new SqlCommand();
        SqlDataReader dr;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
        private void PuniGrid()
        {
            Kon.Open();
            SqlCommand cmd = new SqlCommand("PuniGrid", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            Kon.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int predmetID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PredmetID"].Value);
                Kon.Open();
                SqlCommand cmd = new SqlCommand("BrisiPredmet", Kon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PredmetID", predmetID);
                cmd.ExecuteNonQuery();
                Kon.Close();

                PuniGrid();

            }
            else
            {
                MessageBox.Show("Predmet nije selektovan.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
