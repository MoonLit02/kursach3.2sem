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
    public partial class Form3 : Form
    {
        int count;
        string _tableName="";
        OleDbConnection myConnection;
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\crax0\source\repos\kursach3.2sem\kursach3.2sem\bin\Debug\BD.mdb";
        public Form3(string tableName)
        {
            _tableName = tableName;
            InitializeComponent();
            myConnection = new OleDbConnection(connectionString);
            myConnection.Open();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            /*string query = "SELECT COLUMN_NAME,*" +
            " FROM INFORMATION_SCHEMA.COLUMNS" +
            " WHERE TABLE_NAME = '" + _tableName + "'";*/
            string query = "SELECT * FROM " + _tableName;
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            
            
            dataGridView1.RowCount = 1;
            while (reader.Read())
            {
                //var table=reader.GetSchemaTable();
                dataGridView1.ColumnCount = reader.FieldCount;
                dataGridView1.Rows.Add();
                count = reader.FieldCount;
                for(int i=0;i<reader.FieldCount;i++)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[i].Value = reader[i];
                }
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO "+_tableName+" VALUES ";
            string param = "('"+(dataGridView1.Rows.Count-1)+"',";
            int row = dataGridView1.SelectedRows[0].Index;
            for (int i = 1; i<count;i++)
            {
                param += "'"+dataGridView1.Rows[row].Cells[i].Value+"',";
            }
            param = param.TrimEnd(',');
            query += param + ")";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "UPDATE "+_tableName+" SET salary = 123456 WHERE id = 3";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows[0].Index;
            string query = "DELETE FROM " + _tableName + " WHERE ID = "+ dataGridView1.Rows[row].Cells[0].Value;
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            dataGridView1.Rows.RemoveAt(row);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
    }
}
