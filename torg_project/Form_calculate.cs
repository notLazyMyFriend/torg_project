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

namespace torg_project
{
    public partial class Form_calculate : Form
    {
        public Form_calculate()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = torg_project; Integrated Security = True");
        
        private void Form_calculate_Load(object sender, EventArgs e)
        {
            con.Open();
            
            var com = new SqlCommand($"select * from rate where id_personal = '{Properties.Settings.Default.id_manager.ToString()}'", con);
            var reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["id_personal"].ToString(), reader["Name"].ToString(), reader["Junior_min"], reader["Middle_min"] ,reader["Senior_min"],
                         reader["Коэффициент_для_Анализ_и_проектирование"].ToString(), reader["Коэффициент_для_Установка_оборудования"].ToString(), reader["Коэффициент_для_Техническое_обслуживание_и_сопровождение"].ToString(),
                         reader["Коэффициент_времени"].ToString(), reader["Коэффициент_сложности"].ToString(), reader["Коэффициент_для_перевода_в_денежный_эквивалент"].ToString());
                }
            }
            con.Close();
        }

        private void button_saveChanges_Click(object sender, EventArgs e)
        {
            con.Open();
            var d = dataGridView1;
            var com = new SqlCommand($"update rate set Junior_min = {d[2,0].Value.ToString()}, Middle_min = {d[3,0].Value.ToString()}, Senior_min = {d[4,0].Value.ToString()}," +
                $"Коэффициент_для_Анализ_и_проектирование = {d[5,0].Value.ToString()}, Коэффициент_для_Установка_оборудования = {d[6, 0].Value.ToString()}," +
                $"Коэффициент_для_Техническое_обслуживание_и_сопровождение = {d[7, 0].Value.ToString()}, Коэффициент_времени = {d[8, 0].Value.ToString()}," +
                $"Коэффициент_сложности = {d[9, 0].Value.ToString()}, Коэффициент_для_перевода_в_денежный_эквивалент = {d[10, 0].Value.ToString()} where id_personal = {d[0,0].Value.ToString()}", con);
            int count = com.ExecuteNonQuery();
            MessageBox.Show("Затронуто строк: "+count);
            con.Close();
        }
    }
}
