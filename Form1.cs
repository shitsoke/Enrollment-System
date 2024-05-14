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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Desktop\C#\Database1\Database11.accdb";
        private void SaveButton_Click(object sender, EventArgs e)
        {
            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string Ole = "Select * From SUBJECTFILE";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(Ole, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "SubjectFile");

            DataRow thisRow = thisDataSet.Tables["SubjectFile"].NewRow();
            thisRow["SFSUBJCODE"] = SubjectCodeTextBox.Text;
            thisRow["SFSUBJDESC"] = DescriptionTextBox.Text;
            thisRow["SFSUBJUNITS"] = UnitsTextBox.Text;
            thisRow["SFSUBJREGOFRNG"] = OfferingComboBox.Text.Substring(0, 1);
            thisRow["SFSUBJCATEGORY"] = CategoryComboBox.Text.Substring(0, 3);
            thisRow["SFSUBJSTATUS"] = StatusComboBox.Text.Substring(0, 2);
            thisRow["SFSUBJCOURSECODE"] = CourseCodeComboBox.Text;
            thisRow["SFSUBJCURRCODE"] = CurriculumTextBox.Text;

            thisDataSet.Tables["SubjectFile"].Rows.Add(thisRow);
            thisAdapter.Update(thisDataSet, "SubjectFile");

            MessageBox.Show("Recorded");
        }

        private void RequisiteTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OleDbConnection requisiteConnection = new OleDbConnection(connectionString);
                requisiteConnection.Open();
                OleDbCommand thisCommand = requisiteConnection.CreateCommand();
                string Ole = "Select * From SUBJECTFILE";
                string requisite = "SELECT * FROM SUBJECTPREQFILE";
                thisCommand.CommandText = Ole;
                OleDbDataReader thisDataReader = thisCommand.ExecuteReader();
                OleDbDataAdapter requisiteAdapter = new OleDbDataAdapter(requisite,requisiteConnection);
                OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(requisiteAdapter);
                DataSet thisDataSet = new DataSet();
                requisiteAdapter.Fill(thisDataSet, "SubjectPreqFile");
                bool found = false;
                string subjectCode = "";
                string description = "";
                string units = "";
                string preRequisite = RequisiteTextBox.Text;

                while (thisDataReader.Read())
                {
                    if (thisDataReader["SFSUBJCODE"].ToString().Trim().ToUpper() == RequisiteTextBox.Text.Trim().ToUpper())
                    {
                        found = true;
                        subjectCode = thisDataReader["SFSUBJCODE"].ToString();
                        description = thisDataReader["SFSUBJDESC"].ToString();
                        units = thisDataReader["SFSUBJUNITS"].ToString();
                        break;
                    }

                }

                int index;
                if (found == false)
                    MessageBox.Show("Subject Code Not Found");
                else
                {
                    DataRow thisRow;
                    index = SubjectDataGridView.Rows.Add();
                    SubjectDataGridView.Rows[index].Cells["SUBJCODE"].Value = subjectCode;
                    SubjectDataGridView.Rows[index].Cells["SUBJDESC"].Value = description;
                    SubjectDataGridView.Rows[index].Cells["SUBJUNITS"].Value = units;

                    if (PreRequisiteRadioButton.Checked)
                    {
                        thisRow = thisDataSet.Tables["SubjectPreqFile"].NewRow();
                        thisRow["SUBJCODE"] = preRequisite;
                        thisRow["SUBJPRECODE"] = subjectCode;
                        thisRow["SUBJCATEGORY"] = "PR";
                        thisDataSet.Tables["SubjectPreqFile"].Rows.Add(thisRow);
                        requisiteAdapter.Update(thisDataSet, "SubjectPreqFile");
                    }
                    else if(CoRequisiteRadioButton.Checked)
                    {
                        thisRow = thisDataSet.Tables["SubjectPreqFile"].NewRow();
                        thisRow["SUBJCODE"] = preRequisite;
                        thisRow["SUBJPRECODE"] = subjectCode;
                        thisRow["SUBJCATEGORY"] = "CR";
                        thisDataSet.Tables["SubjectPreqFile"].Rows.Add(thisRow);
                        requisiteAdapter.Update(thisDataSet, "SubjectPreqFile");
                    }
                    else
                    {
                        MessageBox.Show("Select a Category");
                    }
                }
            }

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
            SubjectCodeTextBox.Text = "";
            DescriptionTextBox.Text = "";
            UnitsTextBox.Text = "";
            OfferingComboBox.SelectedIndex = -1;
            CategoryComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
            CourseCodeComboBox.SelectedIndex = -1;
            CurriculumTextBox.Text = "";
        }
    }
}

