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
using System.Data.SqlClient;

namespace bibliotekaAndjela
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection(@"Data Source=DESKTOP-EAC7ALT\SQLEXPRESS;Initial Catalog=EIT_А02_Biblioteka;Integrated Security=True"); /* MM 2 sp*/

        SqlCommand kom = new SqlCommand();

        SqlDataReader dr;

        int id = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            PuniLV();
        }
        private void PuniLV()
        {
            listView1.Items.Clear();

            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniListView", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Sortiranje", SqlDbType.VarChar).Value = SortKolona;
            //cmd.ExecuteNonQuery();
            //DataTable dt = new DataTable();
            //dt.Load(cmd.ExecuteReader());
            //kom.Connection = Kon;
            //kom.CommandText = "EXEC PuniListView";
            //Kon.Open();
            //dr = kom.ExecuteReader();

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                ListViewItem red = new ListViewItem(dr[0].ToString());
                for (int i = 1; i < 4; i++) /* i IDE DO KOLIKO POLJA VRACA PROCEDURA*/
                    red.SubItems.Add(dr[i].ToString());
                listView1.Items.Add(red);
            }
            Kon.Close();
        }
        private void SaLVNaKontrole()
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                id = Convert.ToInt32(item.SubItems[0].Text);

                txtSifra.Text = id.ToString();
                txtIme.Text = item.SubItems[1].Text;
                txtPrezime.Text = item.SubItems[2].Text;
                mskRodjen.Text = item.SubItems[3].Text;

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaLVNaKontrole();
        } 
        private void BrisiAutora()
        {
            string poruka = "Zelite da obrisete stavku?";
            string naslov = "Brisanje proizvoda";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(poruka, naslov, buttons);

            if (result == DialogResult.Yes)
            {
                Kon.Open();
                SqlCommand cmd = new SqlCommand("BrisiAutor", Kon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AutorID", SqlDbType.VarChar).Value = txtSifra.Text.ToString();

                cmd.ExecuteNonQuery();

                Kon.Close();
            }
        }

        private void brisanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrisiAutora();
            PuniLV();
        }
    }
}
