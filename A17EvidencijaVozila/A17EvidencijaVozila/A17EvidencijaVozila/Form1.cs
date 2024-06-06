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

namespace A17EvidencijaVozila
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection(@"Data Source=DESKTOP-11JQC4J\SQLEXPRESS;Initial Catalog=4EIT_A17_EvidencijaVozila;Integrated Security=True");

        SqlCommand kom = new SqlCommand();

        SqlDataReader dr;

        int id = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            PuniListView();
            button2.Visible = false;
            punicombogorivo();
            punicomboboja();
            punicombomodel();
        }

        private void PuniListView()
        {
            listView1.Items.Clear();

            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniListBoxView", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                ListViewItem red = new ListViewItem(dr[0].ToString());
                for (int i = 1; i < 8; i++) /* i IDE DO KOLIKO POLJA VRACA PROCEDURA*/
                    red.SubItems.Add(dr[i].ToString());
                listView1.Items.Add(red);
            }
            Kon.Close();

        }

        private void SaListViewNaKontrole()
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                id = Convert.ToInt32(item.SubItems[0].Text);

                textBox1.Text = id.ToString();
                textBox2.Text = item.SubItems[1].Text;
                textBox3.Text = item.SubItems[2].Text;
                textBox4.Text = item.SubItems[3].Text;
                textBox5.Text = item.SubItems[4].Text;
                
            }

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaListViewNaKontrole();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            textBox1.Enabled = false;

        }

        private void punicombogorivo()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboGorivo", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "Naziv";

            Kon.Close();

        }
        private void punicomboboja()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboBoja", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Naziv";

            Kon.Close();
        }

        private void punicombomodel()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniComboModel", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Naziv";

            Kon.Close();
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            textBox1.Enabled = true;
        }

        private void AzuriranjeVozila()
        {
            Kon.Open();
            SqlCommand cmd = new SqlCommand("AzurirajPodatkeVozilo", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Registracija", SqlDbType.VarChar).Value = textBox2.Text.ToString();
            cmd.Parameters.AddWithValue("@GodinaProizvodnje", SqlDbType.VarChar).Value = textBox3.Text.ToString();
            cmd.Parameters.AddWithValue("@Kilometraza", SqlDbType.VarChar).Value = textBox4.Text.ToString();
            cmd.Parameters.AddWithValue("@ModelNaziv", SqlDbType.VarChar).Value = comboBox1.Text.ToString();
            cmd.Parameters.AddWithValue("@BojaNaziv", SqlDbType.VarChar).Value = comboBox2.Text.ToString();
            cmd.Parameters.AddWithValue("@GorivoNaziv", SqlDbType.VarChar).Value = comboBox3.Text.ToString();
            cmd.Parameters.AddWithValue("@Cena", SqlDbType.VarChar).Value = textBox5.Text.ToString();
            cmd.Parameters.AddWithValue("@VoziloID", SqlDbType.VarChar).Value = textBox1.Text.ToString();
            cmd.ExecuteNonQuery();
            Kon.Close();

        }

        private void izmeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(button2.Visible = true)
            {
                MessageBox.Show("Da biste azurirali vozilko treba prvo da pritisnete dugme 'priprema za izmenu' ");
            }else
            {
                AzuriranjeVozila();
                MessageBox.Show("Podaci vozila su izmenjeni u bazi.\n");
                PuniListView();
                button2.Visible = false;
                textBox1.Enabled = true;
            }


            
        }
    }
}
