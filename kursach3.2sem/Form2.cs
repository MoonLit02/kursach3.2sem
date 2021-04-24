using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace kursach3._2sem
{

    public partial class Form2 : Form
    {
        int size = 0;
        private int role;
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD.mdb"; 
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
            /*string query = "INSERT INTO Line (Bus,Conductor,TimeStart,TimeEnd,CountTicket,Route) VALUES ('27','Karl Klara','12:27','18:34', '194',5)";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();*/
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    
                    if (textBox2.Text[2] != ':'|| textBox2.Text=="")
                        throw new Exception();
                    int trip = 0;
                    string query = "SELECT * FROM Conductor WHERE(ConductorName = '" + comboBox4.Text + "')";
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    OleDbDataReader reader = command.ExecuteReader();
                    int countTicket = 0;
                    while (reader.Read())
                    {
                        countTicket = Convert.ToInt32(reader["CountTicket"].ToString());
                        trip = Convert.ToInt32(reader["CountTrip"].ToString());
                        //MessageBox.Show(reader["ConductorName"].ToString());
                    }
                    reader.Close();
                    trip++;
                    query = "UPDATE Conductor SET CountTicket = '" + (countTicket + Convert.ToInt32(textBox1.Text)) + "', CountTrip='" + trip + "' WHERE ConductorName = '" + comboBox4.Text + "'";
                    command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();

                    query = "SELECT * FROM Route WHERE(RouteNum = '" + comboBox1.Text + "')";
                    command = new OleDbCommand(query, myConnection);
                    reader = command.ExecuteReader();
                    int proceeds = 0;
                    int priceTicket = 0;
                    while (reader.Read())
                    {
                        proceeds = Convert.ToInt32(reader["Proceeds"].ToString());
                        priceTicket = Convert.ToInt32(reader["PriceTicket"].ToString());
                        trip = Convert.ToInt32(reader["Trip"].ToString());
                        //MessageBox.Show(reader["ConductorName"].ToString());
                    }
                    reader.Close();
                    trip++;
                    query = "UPDATE Route SET Proceeds = '" + (proceeds + (priceTicket * Convert.ToInt32(textBox1.Text))) + "', Trip='" + trip + "' WHERE RouteNum = '" + comboBox1.Text + "'";
                    command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();

                    query = $"INSERT INTO Line (Bus, Conductor, TimeStart, TimeEnd, CountTicket, Route) VALUES ('{comboBox5.Text}','{comboBox4.Text}','{textBox2.Text}','{textBox3.Text}','{textBox1.Text}','{comboBox1.Text}')";
                    command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();
                }
                catch { MessageBox.Show("Ошибка"); }
            }
            if (radioButton2.Checked)

                formDock(comboBox3.Text);
            
            //textBox1.Text = command.ExecuteScalar().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {


           /* string query = "UPDATE Conductor SET CountTicket = '123456' WHERE ID = 3";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();*/
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (role == 1)
            {
                pictureBox1.Visible = false;
                label7.Visible = true;
                label8.Visible = true;
                comboBox2.Visible = true;
                button5.Visible = true;
                size = 90;

            }
            if (role ==2)
            {

                button5.Visible = false;
            }
            /*string query = "SELECT * FROM Line";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["ID"].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " ");
            }
            comboBox1.Update();
            reader.Close();*/

            string query = "SELECT * FROM Conductor";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox4.Items.Add(reader["ConductorName"].ToString());
            }
            reader.Close();

            query = "SELECT * FROM Route";
            command = new OleDbCommand(query, myConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["RouteNum"].ToString());
            }
            reader.Close();

            query = "SELECT * FROM Bus";
            command = new OleDbCommand(query, myConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox5.Items.Add(reader["NumBus"].ToString());
            }
            reader.Close();
            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void formDock(string doc)
        {
            // Получить объект приложения Word.
            Word._Application word_app = new Word.Application
            {

                // Сделать Word видимым (необязательно).
                Visible = true
            };

            // Создаем документ Word.
            object missing = Type.Missing;
            Word._Document word_doc = word_app.Documents.Add(
                ref missing, ref missing, ref missing, ref missing);

            // Создаем абзац заголовка.
            Word.Paragraph para = word_doc.Paragraphs.Add(ref missing);
            
           


            // Сохраним текущий шрифт и начнем с использования Courier New.
            string old_font = para.Range.Font.Name;
            para.Range.Font.Name = "Times New Roman";
            Dictionary<string, int> routes = new Dictionary<string, int> { };
            Dictionary<string, int> conductors = new Dictionary<string, int> { };
            Dictionary<string, int> prices = new Dictionary<string, int> { };
            Dictionary<string, int> trips = new Dictionary<string, int> { };
            Dictionary<string, List<double>> load = new Dictionary<string, List<double>> { };

            string query = "SELECT * FROM Route ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                prices.Add(reader["RouteNum"].ToString(),Convert.ToInt32(reader["PriceTicket"].ToString()));
            }
            reader.Close();

            if (doc == "Ведомость по загруженности автобусов")
            {
                para.Range.Text = "Ведомость по загруженности автобусов";
                object style_name = "Заголовок 1";
                para.Range.set_Style(ref style_name);
                para.Range.InsertParagraphAfter();
                para.Range.Text = "\nЗагруженность: ";
                query = "SELECT * FROM Line";
                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (load.ContainsKey(reader["Route"].ToString()))
                    {
                        int time = (Convert.ToInt32(reader["TimeEnd"].ToString().Split(':')[0]) - Convert.ToInt32(reader["TimeStart"].ToString().Split(':')[0])) * 60 +
                            (Convert.ToInt32(reader["TimeEnd"].ToString().Split(':')[1]) - Convert.ToInt32(reader["TimeStart"].ToString().Split(':')[1]));
                        load[reader["Route"].ToString()].Add(Convert.ToInt32(reader["CountTicket"].ToString())*1.0/time);
                    }
                    else
                    {
                        int time = (Convert.ToInt32(reader["TimeEnd"].ToString().Split(':')[0]) - Convert.ToInt32(reader["TimeStart"].ToString().Split(':')[0])) * 60 +
                           (Convert.ToInt32(reader["TimeEnd"].ToString().Split(':')[1]) - Convert.ToInt32(reader["TimeStart"].ToString().Split(':')[1]));
                        load.Add(reader["Route"].ToString(),new List<double>());
                        load[reader["Route"].ToString()].Add(Convert.ToInt32(reader["CountTicket"].ToString())*1.0 / time);
                    }

                    /*para.Range.Text += reader["ConductorName"].ToString() + " - Проданных билетов: " + reader["CountTicket"].ToString() + ", на сумму: " +
                        reader["CountTicket"].ToString();*/
                }
                reader.Close();
                foreach (var a in load)
                {
                    double sum = 0;
                    foreach (var b in a.Value)
                    {
                        sum += b;
                    }
                    para.Range.Text += "На маршруте " + a.Key + " средняя загруженность " + Math.Round(sum*1.0/a.Value.Count,3)+" человек/минуту";
                }
                para.Range.InsertParagraphAfter();
            }

            if (doc== "Сравнительная характеристика кондукторов")
            {
                para.Range.Text = "Сравнительная характеристика кондукторов";
                object style_name = "Заголовок 1";
                para.Range.set_Style(ref style_name);
                para.Range.InsertParagraphAfter();
                para.Range.Text = "\nКондукторы: ";
                query = "SELECT * FROM Line";
                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (conductors.ContainsKey(reader["Conductor"].ToString()))
                    {
                        conductors[reader["Conductor"].ToString()] += Convert.ToInt32(reader["CountTicket"].ToString());
                        trips[reader["Conductor"].ToString()]++;
                    }
                    else
                    {
                        conductors.Add(reader["Conductor"].ToString(), Convert.ToInt32(reader["CountTicket"].ToString()));
                        trips.Add(reader["Conductor"].ToString(), 1);
                    }

                    /*para.Range.Text += reader["ConductorName"].ToString() + " - Проданных билетов: " + reader["CountTicket"].ToString() + ", на сумму: " +
                        reader["CountTicket"].ToString();*/
                }
                reader.Close();
                foreach(var a in conductors)
                {
                    if(a.Key!="")
                    para.Range.Text += "Кондуктор "+a.Key + " продал " + a.Value+" билетов за "+trips[a.Key]+" поездок";
                    else
                        para.Range.Text += "Водители продали " + a.Value + " билетов за " + trips[a.Key] + " поездок";

                }
                para.Range.InsertParagraphAfter();
            }
            if (doc== "Объем выручки") 
            {
                para.Range.Text = "Отчет по объему выручки";
                object style_name = "Заголовок 1";
                para.Range.set_Style(ref style_name);
                para.Range.InsertParagraphAfter();
                para.Range.Text = "\nМаршруты: ";
                query = "SELECT * FROM Line";
                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (routes.ContainsKey(reader["Route"].ToString()))
                    {
                        routes[reader["Route"].ToString()] += Convert.ToInt32(reader["CountTicket"].ToString());
                    }
                    else
                        routes.Add(reader["Route"].ToString(), Convert.ToInt32(reader["CountTicket"].ToString()));

                    /*para.Range.Text += reader["ConductorName"].ToString() + " - Проданных билетов: " + reader["CountTicket"].ToString() + ", на сумму: " +
                        reader["CountTicket"].ToString();*/
                }
                foreach (var a in routes)
                {
                    para.Range.Text += "По маршруту № "+a.Key + " было продано " + a.Value+" билетов, на сумму: "+(prices[a.Key]*a.Value)+" рублей";
                }
                reader.Close();
                para.Range.InsertParagraphAfter();
            }
            // Начнем новый абзац, а затем
            // вернемся к исходному шрифту.
            para.Range.InsertParagraphAfter();
            para.Range.Font.Name = old_font;

            // Сохраним документ.
            object filename = Environment.CurrentDirectory+"/test.doc";
            word_doc.SaveAs(ref filename, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);

            // Закрыть.
            object save_changes = false;
            word_doc.Close(ref save_changes, ref missing, ref missing);
            word_app.Quit(ref save_changes, ref missing, ref missing);
            MessageBox.Show("Документ сформирован");
            Process.Start(filename.ToString());
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(comboBox2.Text) ;
            form3.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label6.Visible = true;
                comboBox3.Visible = true;
                label5.Visible = false;
                label9.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                comboBox1.Visible = false;
                comboBox4.Visible = false;
                textBox1.Visible = false;
                comboBox5.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                this.Height = 220;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label5.Visible = true;
                label9.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                comboBox1.Visible = true;
                comboBox4.Visible = true;
                textBox1.Visible = true;
                comboBox5.Visible = true;
                label6.Visible = false;
                comboBox3.Visible = false;
                textBox2.Visible =true;
                textBox3.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                this.Height = 353;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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
