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

        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD.mdb";
        OleDbConnection myConnection;
        public Form1()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectionString);
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            string query = "SELECT * FROM Users WHERE Login = '"+textBox1.Text+"' AND Password ='"+textBox2.Text+"'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            try
            {
                string a = command.ExecuteScalar().ToString();
                Form2 form2 =new Form2(Convert.ToInt32(a));
                form2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный логин или пароль ");
            }
            myConnection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
    }
}
