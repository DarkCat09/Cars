using System;
using System.Windows.Forms;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Formula1
{
    public partial class Form1 : Form
    {
        private string connString =
            "Server = VH287.spaceweb.ru; Database = beavisabra_cars;" +
            "port = 3306; User Id = beavisabra_cars; password = Beavis1989";
        public Form1()
        {
            InitializeComponent();

            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand sqlRequest = new MySqlCommand("SELECT * from cars", conn);
            DbDataReader dbdr = sqlRequest.ExecuteReader();
            while (dbdr.Read())
            {
                Label carLabel = new Label() {
                    Text = dbdr.GetValue(0).ToString(),
                    Location = new System.Drawing.Point(13, 13),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12)
                };

                PictureBox carPicture = new PictureBox()
                {
                    Location = new System.Drawing.Point(13, 25),
                    ImageLocation = "Pictures\\" + dbdr.GetValue(1).ToString(),
                    SizeMode = PictureBoxSizeMode.AutoSize
                };
                string carWikiLink = dbdr.GetValue(6).ToString();
                carPicture.Click += new EventHandler((sender, e) => { System.Diagnostics.Process.Start(carWikiLink); });

                tableLayoutPanel1.Controls.Add(new Panel() { 
                    Controls = { carLabel, carPicture }
                });
            }

            dbdr.Close();
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }
    }
}
