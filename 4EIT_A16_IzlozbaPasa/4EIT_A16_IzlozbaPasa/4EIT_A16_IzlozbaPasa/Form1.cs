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

namespace _4EIT_A16_IzlozbaPasa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection("Data Source=DESKTOP-MMPSDR0\\SQLEXPRESS;Initial Catalog=4EIT_A16_IzlozbaPasa;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            PuniListView();
            PuniComboPas();
            PuniComboIzlozba();
            PuniComboKategorija();
        }
        private void PuniListView()
        {
            listView1.Items.Clear();
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniListView", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr[0].ToString());
                for (int i = 1; i < 3; i++)
                {
                    if (i == 4)
                    {
                        if (Convert.ToInt32(dr[i]) == 0)
                        {
                            item.SubItems.Add("False");
                        }
                        else
                        {
                            item.SubItems.Add("True");
                        }
                    }
                    else
                    {
                        item.SubItems.Add(dr[i].ToString());
                    }
                }
                listView1.Items.Add(item);
            }

            Kon.Close();
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

        private void UpdateIzlozbaPas()
        {
            Kon.Open();

            string Izlozba = comboIzlozba.Text.ToString();
            string[] IzlozbaID = Izlozba.Split('-');

            string Pas = comboPas.Text.ToString();
            string[] PasID = Pas.Split('-');

            string Kategorija = comboKategorija.Text.ToString();
            string[] KategorijaID = Kategorija.Split('-');

            SqlCommand cmd = new SqlCommand("IzmenaIzlozbaPsi", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = IzlozbaID[0].ToString();
            cmd.Parameters.AddWithValue("@PasID", SqlDbType.Char).Value = PasID[0].ToString();
            cmd.Parameters.AddWithValue("@KategorijaID", SqlDbType.Char).Value = KategorijaID[0].ToString();

         
            cmd.ExecuteNonQuery();

            Kon.Close();

        }
        private void Provera()
        {
            Kon.Open();

            string Izlozba = comboIzlozba.Text.ToString();
            string[] IzlozbaID = Izlozba.Split('-');

            string Pas = comboPas.Text.ToString();
            string[] PasID = Pas.Split('-');

            string Kategorija = comboKategorija.Text.ToString();
            string[] KategorijaID = Kategorija.Split('-');

            SqlCommand cmd = new SqlCommand("UnosIzlozbaPsi", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@PasID", SqlDbType.Char).Value = PasID[0].ToString();
            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = IzlozbaID[0].ToString();
            cmd.Parameters.AddWithValue("@KategorijaID", SqlDbType.Char).Value = KategorijaID[0].ToString();

            //string Pass = "";
            //string Izlozbaa = "";
            //string Kategorija1 = "";

            cmd.ExecuteNonQuery();
            //while (dr.Read())
            //{
            //    Pass = dr[2].ToString().Trim();
            //    Izlozbaa = dr[0].ToString().Trim();
            //    Kategorija1 = dr[1].ToString().Trim();
            //}

            Kon.Close();

            //if (PasID[0].Trim() == Pass && IzlozbaID[0].Trim() == Izlozbaa && KategorijaID[0].Trim() == Kategorija1)
            //{
            //    MessageBox.Show("Vec ste se prijavili", "Greska");
            //}
            //else
            //{
            //    PrijaviPsa();
            //}
        }
        private void PrijaviPsa()
        {
            Kon.Open();

            string Pas = comboPas.Text.ToString();
            string[] PasID = Pas.Split('-');
            string Izlozba = comboIzlozba.Text.ToString();
            string[] IzlozbaID = Izlozba.Split('-');
            string Kategorija = comboKategorija.Text.ToString();
            string[] KategorijaID = Kategorija.Split('-');

            SqlCommand cmd = new SqlCommand("UnosIzlozbaPsi", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = IzlozbaID.ToString();
            cmd.Parameters.AddWithValue("@KategorijaID", SqlDbType.Int).Value = Convert.ToInt32(KategorijaID[0]);
            cmd.Parameters.AddWithValue("@PasID", SqlDbType.Int).Value = Convert.ToInt32(PasID[0]);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Uspesno ste se prijavili!");
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
            string Izlozba = comboIzlozba2.Text.ToString();
            string[] IzlozbaID = Izlozba.Split('-');
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniGridIChart", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = IzlozbaID.ToString();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            dataGridView1.DataSource = dt;
            chart1.DataSource = dt;

            chart1.Series["Series1"].XValueMember = "Naziv";
            chart1.Series["Series1"].YValueMembers = "BrojPasa";
            chart1.Titles.Add("Broj pasa koji se takmicio");

            Kon.Close();
        }
        private void UkupanPrijavljen()
        {
            Kon.Open();

            string Izlozba = comboIzlozba2.Text.ToString();
            string[] IzlozbaID = Izlozba.Split('-');

            SqlCommand cmd = new SqlCommand("UkupanBrPrijavljenih", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = IzlozbaID.ToString();

            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                label7.Text = "Ukupan broj pasa koji je prijavljen : " + dr[0].ToString();
            }

            Kon.Close();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            PuniComboIzlozba2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void UkupanTakmicen()
        {
            Kon.Open();

            string Izlozba = comboIzlozba2.Text.ToString();
            string[] IzlozbaID = Izlozba.Split('-');

            SqlCommand cmd = new SqlCommand("UkupanBrKojiSeTakmicio", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IzlozbaID", SqlDbType.VarChar).Value = IzlozbaID.ToString();
            

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label8.Text = "Ukupan broj pasa koji se takmicio : " + dr[0].ToString();
            }

            Kon.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Provera();
            PuniListView();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateIzlozbaPas();
            PuniListView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            BrisiPsa();
            PuniComboPas();
            PuniComboIzlozba();
            PuniComboKategorija();
            PuniListView();

        }
        private void BrisiPsa()
        {
            Kon.Open();

            string Pas = comboPas.Text.ToString();
            string[] PasID = Pas.Split('-');
            

            SqlCommand cmd = new SqlCommand("DeletePas", Kon);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@PasID", SqlDbType.Int).Value = Convert.ToInt32(PasID[0]);
            cmd.ExecuteNonQuery();
            
            Kon.Close();
        }
    }
}
