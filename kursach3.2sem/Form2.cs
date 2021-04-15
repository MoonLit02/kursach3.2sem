using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach3._2sem
{
    public partial class Form2 : Form
    {
        private int role;
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\crax0\source\repos\kursach3.2sem\kursach3.2sem\bin\Debug\BD.mdb";
        OleDbConnection myConnection;
        public Form2(int Role)
        {
            role = Role;
            InitializeComponent();
            myConnection = new OleDbConnection(connectionString);
            myConnection.Open();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Line (Bus,Conductor,TimeStart,TimeEnd,CountTicket) VALUES ('27','Karl Klara','12:27','18:34', '194')";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Line";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["ID"].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " ");
            }
            comboBox1.Update();
            reader.Close();
            //textBox1.Text = command.ExecuteScalar().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Line SET CountTicket = '123456' WHERE ID = 3";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (role == 1)
            {
                label7.Visible = true;
                label8.Visible = true;
                radioButton3.Visible = true;
                comboBox2.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
            if (role ==2)
            {
                button4.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(comboBox2.Text) ;
            form3.Show();
        }
        /*string query = "INSERT INTO Bus (name, role, salary) VALUES ('Михаил', 'Водитель', 20000)";
OleDbCommand command = new OleDbCommand(query, myConnection);
command.ExecuteNonQuery();

string query = "UPDATE Bus SET salary = 123456 WHERE id = 3";
OleDbCommand command = new OleDbCommand(query, myConnection);
command.ExecuteNonQuery();

string query = "DELETE FROM Bus WHERE w_id < 3";
OleDbCommand command = new OleDbCommand(query, myConnection);
command.ExecuteNonQuery();


string query = "SELECT * FROM Bus ";
OleDbCommand command = new OleDbCommand(query, myConnection);
OleDbDataReader reader = command.ExecuteReader();
listBox1.Items.Clear();
while(reader.Read())
{      
listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " ");
}
reader.Close();
*/
    }
}
