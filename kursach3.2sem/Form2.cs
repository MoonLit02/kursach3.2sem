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
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\crax0\source\repos\kursach3.2sem\kursach3.2sem\BD.mdf;Integrated Security=True;Connect Timeout=30";
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
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Bus";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            textBox1.Text = command.ExecuteScalar().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (role ==1)
            {

            }
        }
        /*string query = "INSERT INTO Bus (name, role, salary) VALUES ('Михаил', 'Водитель', 20000)";
        OleDbCommand command = new OleDbCommand(query, myConnection);
        command.ExecuteNonQuery();

        string query = "UPDATE Bus SET salary = 123456 WHERE id = 3";
    OleDbCommand command = new OleDbCommand(query, myConnection);
    command.ExecuteNonQuery();

        string query = "DELETE FROM Bus WHERE w_id < 3";
    OleDbCommand command = new OleDbCommand(query, myConnection);
    command.ExecuteNonQuery();*/
    }
}
