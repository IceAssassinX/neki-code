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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _4EIT_A16_IzlozbaPasa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection("Data Source=DESKTOP-5J6JSDT\\SQLEXPRESS01;Initial Catalog=4EIT_A16_IzlozbaPasa;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            PuniComboPas();
            PuniComboIzlozba();
            PuniComboKategorija();
        }
        private void PuniComboPas()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboPas", Kon);
            cmd.CommandType = CommandType.StoredProcedure; 

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboPas.DataSource = dt;
            comboPas.DisplayMember = "Pas";

            Kon.Close();
        }
        private void PuniComboIzlozba()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboIzlozba", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboIzlozba.DataSource = dt;
            comboIzlozba.DisplayMember = "Izlozba";

            Kon.Close();
        }
        private void PuniComboKategorija()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboKategorija", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboKategorija.DataSource = dt;
            comboKategorija.DisplayMember = "Kategorija";

            Kon.Close();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            Provera();
        }
        private void Provera()
        {
            Kon.Open();


            Kon.Close();
        }
            
        private void PrijaviPsa()
        {
            Kon.Open();

            Kon.Close();
        }

        private void PuniComboIzlozba2()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboIzlozba", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboIzlozba2.DataSource = dt;
            comboIzlozba2.DisplayMember = "Izlozba";

            Kon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PuniGridIChart() 
        {
            chart1.Titles.Clear();
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniGridIChart", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = comboPas.Text.ToString();

            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            chart1.DataSource = dt;
            dataGridView1.DataSource = dt;

            chart1.Series["Series1"].XValueMember = "Naziv";
            chart1.Series["Series1"].YValueMembers = "BrojPasa";
            chart1.Titles.Add("Broj pasa koji se takmicio");

            Kon.Close();
        }
        private void UkupanPrijavljen()
        {
            Kon.Open();


            Kon.Close();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            PuniComboIzlozba2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PuniGridIChart();
            UkupanPrijavljen();
            UkupanTakmicen();
        }
        private void UkupanTakmicen()
        {
            Kon.Open();
            Kon.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PuniGridIChart();
            UkupanPrijavljen();
            UkupanTakmicen();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
