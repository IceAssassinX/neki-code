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

namespace InsertPrimer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Kon = new SqlConnection(@"Data Source=DESKTOP-R7KLAQU\SQLEXPRESS;Initial Catalog=4EIT_A11_EvidencijaKnjiga;Integrated Security=True"); /* MM 2 sp*/

        SqlCommand kom = new SqlCommand();

        SqlDataReader dr;

        int id = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            PuniLV();
            txtAutorID.Enabled = false;
            listView1.FullRowSelect = true;
        }

        private void PuniLV()
        {
            listView1.Items.Clear();

            Kon.Open();

            SqlCommand cmd = new SqlCommand("PuniListView", Kon);
            cmd.CommandType = CommandType.StoredProcedure;

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

        private void UnesiAutora()
        {
            Kon.Open();
            SqlCommand cmd = new SqlCommand("InsertAutor", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@ImeAutora", SqlDbType.VarChar).Value = txtIme.Text.ToString();
            cmd.Parameters.AddWithValue("@PrezimeAutora", SqlDbType.VarChar).Value = txtPrezime.Text.ToString();
            cmd.Parameters.AddWithValue("@DatumRodjena", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
            cmd.ExecuteNonQuery();
            Kon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UnesiAutora();
            PuniLV();
            PrazniKontrole();
        }

        private void PrazniKontrole()
        {
            txtAutorID.Clear();
            txtIme.Clear();
            txtPrezime.Clear();

        }

        private void BrisiAutora()
        {
            Kon.Open();
            SqlCommand cmd = new SqlCommand("BrisiAutor", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AutorID", SqlDbType.VarChar).Value = txtAutorID.Text.ToString();
            cmd.ExecuteNonQuery();
            Kon.Close();
        }

        private void IzmeniAutora()
        {
            Kon.Open();
            SqlCommand cmd = new SqlCommand("IzmeniAutor", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@AutorID", SqlDbType.VarChar).Value = txtAutorID.Text.ToString();
            cmd.Parameters.AddWithValue("@ImeAutora", SqlDbType.VarChar).Value = txtIme.Text.ToString();
            cmd.Parameters.AddWithValue("@PrezimeAutora", SqlDbType.VarChar).Value = txtPrezime.Text.ToString();
            cmd.Parameters.AddWithValue("@DatumRodjena", SqlDbType.VarChar).Value = dateTimePicker1.Value.ToString();
            cmd.ExecuteNonQuery();
            Kon.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            BrisiAutora();
            PuniLV();
            PrazniKontrole();
        }

        private void PrebaciVrednostiNaKontrole()
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                id = Convert.ToInt32(item.SubItems[0].Text);
                
                txtAutorID.Text = id.ToString();
                txtIme.Text = item.SubItems[1].Text;
                txtPrezime.Text = item.SubItems[2].Text;
                
                dateTimePicker1.Value = Convert.ToDateTime(item.SubItems[3].Text);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrebaciVrednostiNaKontrole();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            IzmeniAutora();
            PuniLV();
            PrazniKontrole();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PuniLV();
        }
    }
}
