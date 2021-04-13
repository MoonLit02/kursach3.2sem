using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach3._2sem
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDDataSet1.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.bDDataSet1.Users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDDataSet1.Route". При необходимости она может быть перемещена или удалена.
            //  this.routeTableAdapter.Fill(this.bDDataSet1.Route);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDDataSet1.Line". При необходимости она может быть перемещена или удалена.
            //this.lineTableAdapter.Fill(this.bDDataSet1.Line);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDDataSet1.Bus". При необходимости она может быть перемещена или удалена.
            //this.busTableAdapter.Fill(this.bDDataSet1.Bus);
           
            dataGridView1.DataSource = usersTableAdapter;
            dataGridView1.Update();
        }
    }
}
