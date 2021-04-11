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
    public partial class Form1 : Form
    {

        string connectionString = @"Data Source=.\SQLEXPRESS;
                            AttachDbFilename=|DataDirectory|\BD.mdf;Integrated Security=True;Connect Timeout=30";
        OleDbConnection myConnection;
        public Form1()
        {
            InitializeComponent();
           // myConnection = new OleDbConnection(connectionString);
           // myConnection.Open();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string query = "SELECT * FROM Users WHERE Login = '"+textBox1.Text+"' AND Password ='"+textBox2.Text+"'";

            OleDbCommand command = new OleDbCommand(query, myConnection);
            textBox1.Text = command.ExecuteScalar().ToString();
            myConnection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
