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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection("Data Source=DESKTOP-MMPSDR0\\SQLEXPRESS;Initial Catalog=EIT_A03_EvidencijaRadnika;Integrated Security=True");


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
                for (int i = 1; i < 6; i++)
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

        private void btnIzadji_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaListViewNaKontrole()
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                txtSifra.Text = item.SubItems[0].Text;
                txtNaziv.Text = item.SubItems[1].Text;
                txtDatum.Text = item.SubItems[2].Text;
                txtBudzet.Text = item.SubItems[3].Text;
                if (item.SubItems[4].Text == "True")
                {
                    checkZavrsen.Checked = true;
                }
                else
                {
                    checkZavrsen.Checked = false;
                }
                txtOpis.Text = item.SubItems[5].Text;
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            ObrisiProjekat();
            PuniListView();
        }

        private void ObrisiProjekat()
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("BrisiProjekat", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SifraProjekta", SqlDbType.NVarChar).Value = txtSifra.Text.ToString();

            cmd.ExecuteNonQuery();
            Kon.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaListViewNaKontrole();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PuniListView();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 forma = new Form2();
            forma.ShowDialog();
        }

        private void bntDodaj_Click(object sender, EventArgs e)
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("DodajProjekat", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Naziv", SqlDbType.NVarChar).Value = txtNaziv.Text.ToString();
            cmd.Parameters.AddWithValue("@DatumPocetka", SqlDbType.NVarChar).Value = DateTime.Parse(txtDatum.Text.ToString());
            cmd.Parameters.AddWithValue("@Budzet", SqlDbType.NVarChar).Value = txtBudzet.Text.ToString();
            cmd.Parameters.AddWithValue("@ProjekatZavrsen", checkZavrsen.Checked ? "true" : "false");
            cmd.Parameters.AddWithValue("@Opis", SqlDbType.NVarChar).Value = txtOpis.Text.ToString();

            cmd.ExecuteNonQuery();
            Kon.Close();
            PuniListView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Kon.Open();

            SqlCommand cmd = new SqlCommand("UpdateProjekat", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjekatID", SqlDbType.NVarChar).Value = txtSifra.Text.ToString();
            cmd.Parameters.AddWithValue("@Naziv", SqlDbType.NVarChar).Value = txtNaziv.Text.ToString();
            cmd.Parameters.AddWithValue("@DatumPocetka", SqlDbType.NVarChar).Value = DateTime.Parse(txtDatum.Text.ToString());
            cmd.Parameters.AddWithValue("@Budzet", SqlDbType.NVarChar).Value = txtBudzet.Text.ToString();
            cmd.Parameters.AddWithValue("@ProjekatZavrsen", checkZavrsen.Checked ? "true" : "false");
            cmd.Parameters.AddWithValue("@Opis", SqlDbType.NVarChar).Value = txtOpis.Text.ToString();

            cmd.ExecuteNonQuery();
            Kon.Close();
            PuniListView();
        }
    }
}
