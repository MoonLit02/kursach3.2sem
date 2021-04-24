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
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD.mdb";
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
            switch (_tableName)
            {
                case "Line":
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Bus";
                    dataGridView1.Columns[2].HeaderText = "Conductor";
                    dataGridView1.Columns[3].HeaderText = "TimeStart";
                    dataGridView1.Columns[4].HeaderText = "TimeEnd";
                    dataGridView1.Columns[5].HeaderText = "CountTicket";
                    dataGridView1.Columns[6].HeaderText = "Route";
                    break;
                case "Route":
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "RouteNum";
                    dataGridView1.Columns[2].HeaderText = "CountBus";
                    dataGridView1.Columns[3].HeaderText = "PriceTicket";
                    dataGridView1.Columns[4].HeaderText = "Proceeds";
                    dataGridView1.Columns[5].HeaderText = "Trip";
                    break;
                case "Bus":
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "NumBus";
                    dataGridView1.Columns[2].HeaderText = "Year";
                    dataGridView1.Columns[3].HeaderText = "Capacity";
                    break;
                case "Conductor":
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "ConductorName";
                    dataGridView1.Columns[2].HeaderText = "CountTicket";
                    dataGridView1.Columns[3].HeaderText = "CountTrip";
                    break;
                case "Users":
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Login";
                    dataGridView1.Columns[2].HeaderText = "Password";
                    dataGridView1.Columns[3].HeaderText = "Role";
                    break;
            }
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
            int row = dataGridView1.SelectedRows[0].Index;
            string query = "UPDATE " + _tableName + " SET ";
            switch (_tableName) 
            {
                case "Line":
                    query += "Bus = '" + dataGridView1.Rows[row].Cells[1].Value + "', " +
                             "Conductor = '" + dataGridView1.Rows[row].Cells[2].Value + "', " +
                             "TimeStart = '" + dataGridView1.Rows[row].Cells[3].Value + "', " +
                             "TimeEnd = '" + dataGridView1.Rows[row].Cells[4].Value + "', " +
                             "CountTicket = '" + dataGridView1.Rows[row].Cells[5].Value + "'" +
                             "Route = '" + dataGridView1.Rows[row].Cells[6].Value + "'";
                    break;
                case "Route":
                    query += "RouteNum = '" + dataGridView1.Rows[row].Cells[1].Value + "', " +
                             "CountBus = '" + dataGridView1.Rows[row].Cells[2].Value + "', " +
                             "PriceTicket = '" + dataGridView1.Rows[row].Cells[3].Value + "', " +
                             "Proceeds = '" + dataGridView1.Rows[row].Cells[4].Value + "', " +
                             "Trip = '" + dataGridView1.Rows[row].Cells[5].Value + "'";
                    break;
                case "Bus":
                    query += "NumBus = '" + dataGridView1.Rows[row].Cells[1].Value + "', " +
                             "Year = '" + dataGridView1.Rows[row].Cells[2].Value + "', " +
                             "Capacity = '" + dataGridView1.Rows[row].Cells[3].Value + "'";
                    break;
                case "Conductor":
                    query += "ConductorName = '" + dataGridView1.Rows[row].Cells[1].Value + "', " +
                             "CountTicket = '" + dataGridView1.Rows[row].Cells[2].Value + "', " +
                             "CountTrip = '" + dataGridView1.Rows[row].Cells[3].Value + "'"; 
                    break;
                case "Users":
                    query += "Login = '" + dataGridView1.Rows[row].Cells[1].Value + "', " +
                             "Password = '" + dataGridView1.Rows[row].Cells[2].Value + "', " +
                             "Role = '" + dataGridView1.Rows[row].Cells[3].Value + "'";
                    break;
            }
            query+=" WHERE ID = "+ dataGridView1.Rows[row].Cells[0].Value;
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
