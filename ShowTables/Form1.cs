using ProcessinglogsAndDB;
using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowTables
{
    public partial class Form1 : Form
    {
        public static BindingList<Log> BUFER_Logs = new BindingList<Log>();
        MyDB myDB = null;

        public Form1()
        {
            InitializeComponent();
            myDB = new MyDB("DBConnection");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myDB == null) { MessageBox.Show("Connect to DB"); return; }

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Id"; //текст в шапке
            column1.Width = 100; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "name"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки
            var column2 = new DataGridViewColumn();
            column2.HeaderText = "IP Address"; 
            column2.Name = "IpAddress";
            column2.CellTemplate = new DataGridViewTextBoxCell();
            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Date Time Offset";
            column3.Width = 200;
            column3.Name = "dto";
            column3.CellTemplate = new DataGridViewTextBoxCell();
            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Type of request";
            column4.Name = "tor";
            column4.CellTemplate = new DataGridViewTextBoxCell();
            var column5 = new DataGridViewColumn();
            column5.HeaderText = "Size data";
            column5.Name = "sd";
            column5.CellTemplate = new DataGridViewTextBoxCell();
            var column6 = new DataGridViewColumn();
            column6.HeaderText = "Path to the file";
            column6.Name = "pttf";
            column6.Width = 600;
            column6.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            dataGridView1.Columns.Add(column6);

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            foreach (var item in myDB.GetLogsTable())
            {
                dataGridView1.Rows.Add(item.LogId, item.GetIpAddressToString(), item.LogTime.ToUniversalTime(), item.TypeOfRequest, item.SizeOfData, item.PathAndFileName); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myDB == null) { MessageBox.Show("Connect to DB"); return; }
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Id"; //текст в шапке
            column1.Width = 50; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "name"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки
            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Title Page";
            column2.Name = "tp";
            column2.Width = 300;
            column2.CellTemplate = new DataGridViewTextBoxCell();
            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Size of file";
            column3.Width = 50;
            column3.Name = "sof";
            column3.CellTemplate = new DataGridViewTextBoxCell();
            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Path to file";
            column4.Name = "ptf";
            column4.Width = 700;
            column4.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            foreach (var item in myDB.GetLoadedFileTable())
            {
                dataGridView1.Rows.Add(item.LogID, item.TitlePage, item.SizeOfFile, item.PathToFile);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (myDB == null) { MessageBox.Show("Connect to DB"); return; }
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Id"; //текст в шапке
            column1.Width = 50; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "name"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки
            var column2 = new DataGridViewColumn();
            column2.HeaderText = "IP Addresses";
            column2.Name = "tp";
            column2.Width = 100;
            column2.CellTemplate = new DataGridViewTextBoxCell();
            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Compani name";
            column3.Width = 200;
            column3.Name = "sof";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            foreach (var item in myDB.GetAddressesTable())
            {
                dataGridView1.Rows.Add(item.LogID, item.GetIpToString(), item.CompaniName);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;

            string fileText = System.IO.File.ReadAllText(filename);
            string nameDB =  Guid.NewGuid().ToString();
            new System.Threading.Thread(() => { MessageBox.Show("Wait please ~ 10 minutes"); new ProcessingData(nameDB).StartProcessFromString(fileText); MessageBox.Show("that is it"); }).Start();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "mdf files (*.mdf)|*.mdf";
            //openFileDialog1.InitialDirectory = System.Environment.GetEnvironmentVariable("USERPROFILE");

            //if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            //    return;

            //string filename = openFileDialog1.SafeFileName;
            //if (filename.IndexOf(".mdf")==-1)
            //{
            //    MessageBox.Show("Choose file with .mdf extention");
            //    return;
            //}
            //filename = filename.Substring(0, openFileDialog1.SafeFileName.IndexOf(".mdf"));
            //myDB = new MyDB(filename);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (myDB == null) { MessageBox.Show("Connect to DB"); return; }

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Id"; //текст в шапке
            column1.Width = 50; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "name"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки
            var column2 = new DataGridViewColumn();
            column2.HeaderText = "IP Address";
            column2.Name = "IpAddress";
            column2.CellTemplate = new DataGridViewTextBoxCell();
            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Compani name";
            column3.Width = 300;
            column3.Name = "dto";
            column3.CellTemplate = new DataGridViewTextBoxCell();
            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Date Time Offset";
            column4.Width = 150;
            column4.Name = "tor";
            column4.CellTemplate = new DataGridViewTextBoxCell();
            var column5 = new DataGridViewColumn();
            column5.HeaderText = "Type of request";
            column5.Width = 50;
            column5.Name = "sd";
            column5.CellTemplate = new DataGridViewTextBoxCell();
            var column6 = new DataGridViewColumn();
            column6.HeaderText = "Result of request";
            column6.Name = "pttf";
            column6.Width = 50;
            column6.CellTemplate = new DataGridViewTextBoxCell();
            var column7 = new DataGridViewColumn();
            column7.HeaderText = "Size data";
            column7.Name = "pttf";
            column7.Width = 50;
            column7.CellTemplate = new DataGridViewTextBoxCell();
            var column8 = new DataGridViewColumn();
            column8.HeaderText = "Title page";
            column8.Name = "pttf";
            column8.Width = 300;
            column8.CellTemplate = new DataGridViewTextBoxCell();
            var column9 = new DataGridViewColumn();
            column9.HeaderText = "Path to the file";
            column9.Name = "pttf";
            column9.Width = 800;
            column9.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            dataGridView1.Columns.Add(column6);
            dataGridView1.Columns.Add(column7);
            dataGridView1.Columns.Add(column8);
            dataGridView1.Columns.Add(column9);

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            foreach (var item in myDB.GetCombinedData())
            {
                dataGridView1.Rows.Add(item.LogId, item.IPAddressString,
                    item.CompaniName, item.LogTime, item.TypeOfRequest,
                    item.ResultOfRequest, item.SizeOfFile, item.TitlePage,
                    item.PathToFile);
            }

        }
    }
}
