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

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection("Data Source=DESKTOP-MMPSDR0\\SQLEXPRESS;Initial Catalog=EIT_A03_EvidencijaRadnika;Integrated Security=True");


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            chart1.Titles.Clear();
            PuniGridIChart();
        }
        private void PuniGridIChart()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniGridChart", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Period", SqlDbType.NVarChar).Value = numericUpDown1.Value;

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            dataGridView1.DataSource = dt;
            chart1.DataSource = dt;

            chart1.Series["Series1"].XValueMember = "Godina";
            chart1.Series["Series1"].YValueMembers = "BrRadnika";
            chart1.Titles.Add("Broj radnika po godini");

            Kon.Close();
        }
    }
}
