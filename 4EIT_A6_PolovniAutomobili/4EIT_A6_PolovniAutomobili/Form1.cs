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

namespace _4EIT_A6_PolovniAutomobili
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection("Data Source=LAPTOP-U3UPVFJG\\MSSQLSERVER01;Initial Catalog=4EIT_А6_PolovniAutomobili;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            PuniListView();
            PuniComboProizvodjac();
        }
        private void PuniListView()
        {
            listView1.Items.Clear();
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniListView", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                ListViewItem item = new ListViewItem(dr[0].ToString());
                item.SubItems.Add(dr[2].ToString());
                item.SubItems.Add(dr[1].ToString());
                listView1.Items.Add(item);
            }

            Kon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaListViewNaKontrole();
        }
        private void SaListViewNaKontrole()
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                textBox2.Text = item.SubItems[0].Text.ToString();
                comboBox1.Text = item.SubItems[1].Text.ToString();
                comboBox2.Text = item.SubItems[2].Text.ToString();
                label5.Text = item.SubItems[2].Text.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IzmeniAutomobil();
            PuniListView();
        }
        private void IzmeniAutomobil()
        {
            Kon.Open();
            string[] model = comboBox2.Text.Split('-');
            SqlCommand cmd = new SqlCommand("IzmeniUpdate", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ModelID", SqlDbType.Int).Value = Convert.ToInt32(model[0]);
            cmd.Parameters.AddWithValue("@VoziloID", SqlDbType.Int).Value = Convert.ToInt32(textBox2.Text.ToString());

            cmd.ExecuteNonQuery();

            Kon.Close();
        }
        private void PuniComboModel()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboModel", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProizvodjacID", SqlDbType.NVarChar).Value = comboBox1.Text.Substring(0, 1).ToString();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Model";

            Kon.Close();
        }
        private void PuniComboProizvodjac()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboProizvodjac", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Proizvodjac";

            Kon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PuniListView();
            int count = 0;
            foreach(ListViewItem item in listView1.Items)
            {
                if(item.SubItems[0].Text.ToString() == textBox2.Text.ToString())
                {
                    listView1.Items[count].Selected = true;
                    listView1.Items[count].EnsureVisible();
                }
                count++;
            }
            PuniComboModel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PuniGridIChart();
        }
        private void PuniGridIChart()
        {
            chart1.Titles.Clear();
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniGridChart", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GodinaProizvodnjeMin", SqlDbType.NVarChar).Value = numericUpDown1.Value;
            cmd.Parameters.AddWithValue("@GodinaProizvodnjeMax", SqlDbType.NVarChar).Value = numericUpDown2.Value;
            cmd.Parameters.AddWithValue("@Kilometraza", SqlDbType.Int).Value = Convert.ToInt32(textBox1.Text);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            dataGridView1.DataSource = dt;
            chart1.DataSource = dt;

            chart1.Series["Series1"].XValueMember = "Proizvodjac";
            chart1.Series["Series1"].YValueMembers = "BrVozila";
            chart1.Titles.Add("Automobili");

            Kon.Close();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PuniComboModel();
        }
    }
}
