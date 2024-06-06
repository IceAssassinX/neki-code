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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace a13dvdkolekcija
{
    public partial class Form1 : Form
    {
        SqlConnection Kon = new SqlConnection(@"Data Source = DESKTOP-OJSP2II\SQLEXPRESS; Initial Catalog = 4EIT_A13_DVDKolekcija; Integrated Security = True");
        SqlCommand kom = new SqlCommand();
        SqlDataReader dr;
        int id = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PuniListview();
            textBox1.Enabled = false;
        }
        private void PuniListview()
        {
            listView1.Items.Clear();
            Kon.Open();
            SqlCommand cmd = new SqlCommand("PuniListView", Kon);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem red = new ListViewItem(dr[0].ToString());
                for (int i = 1; i < 10; i++) /* i IDE DO KOLIKO POLJA VRACA PROCEDURA*/
                    red.SubItems.Add(dr[i].ToString());
                listView1.Items.Add(red);
            }
            Kon.Close();
        }
        private void ListViewnaKontrole()
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                id = Convert.ToInt32(item.SubItems[0].Text);

                textBox1.Text = id.ToString();
                textBox2.Text = item.SubItems[1].Text;
                textBox3.Text = item.SubItems[2].Text;

            }
        }
        private void cistibox()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
