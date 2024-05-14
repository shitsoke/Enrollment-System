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

namespace Database1
{
    public partial class StudentEntry : Form
    {
        private Form1 form1;
        public StudentEntry()
        {
            InitializeComponent();
            
        }
        private void OpenSubjectEntryButton_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            form1.ShowDialog();
            Close();
            Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Desktop\C#\Database1\Database11.accdb";
        private void SaveButton_Click(object sender, EventArgs e)
        {
            
            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string Ole = "Select * From STUDENTFILE";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(Ole, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "StudentFile");

            DataRow thisRow = thisDataSet.Tables["StudentFile"].NewRow();
            thisRow["STFSTUDID"] = IdNumberTextBox.Text;
            thisRow["STFSTUDLNAME"] = LastNameTextBox.Text;
            thisRow["STFSTUDFNAME"] = FirstNameTextBox.Text;
            thisRow["STFSTUDMNAME"] = MiddleInitialTextBox.Text;
            thisRow["STFSTUDCOURSE"] = CourseTextBox.Text;
            thisRow["STFSTUDYEAR"] = YearTextBox.Text;
            thisRow["STFSTUDREMARKS"] = RemarksComboBox.Text;
            thisRow["STFSTUDSTATUS"] = "AC";

            thisDataSet.Tables["StudentFile"].Rows.Add(thisRow);
            thisAdapter.Update(thisDataSet, "studentFile");

            MessageBox.Show("Recorded");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu();
            Hide();
            main.ShowDialog();
            Close();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            IdNumberTextBox.Text = "";
            LastNameTextBox.Text = "";
            FirstNameTextBox.Text = "";
            MiddleInitialTextBox.Text = "";
            CourseTextBox.Text = "";
            YearTextBox.Text = "";
            RemarksComboBox.SelectedIndex = -1;
        }
    }
}
