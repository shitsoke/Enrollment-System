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
    public partial class SubjectScheduleEntry : Form
    {
        public SubjectScheduleEntry()
        {
            InitializeComponent();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Desktop\C#\Database1\Database11.accdb";
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsConflict())
            {
                MessageBox.Show("There is a time or day conflict. Please check your entries.", "Conflict Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string Ole = "Select * From SUBJECTSCHEDFILE";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(Ole, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "SubjectSchedFile");

            DataRow thisRow = thisDataSet.Tables["SubjectSchedFile"].NewRow();
            thisRow["SSFEDPCODE"] = EdpCodeTextBox.Text;
            thisRow["SSFSUBJCODE"] = SubjectCodeTextBox.Text;
            thisRow["SSFSTARTTIME"] = TimeStartTextBox.Text;
            thisRow["SSFENDTIME"] = TimeEndTextBox.Text;
            thisRow["SSFDAYS"] = DaysTextBox.Text.Substring(0,3);
            thisRow["SSFSECTION"] = SectionTextBox.Text;
            thisRow["SSFROOM"] = RoomTextBox.Text;
            thisRow["SSFMAXSIZE"] = 40;
            thisRow["SSFCLASSSIZE"] = 0;
            thisRow["SSFSCHOOLYEAR"] = SchoolYearTextBox.Text;
            thisRow["SSFXM"] = AmComboBox.Text.Substring(0,2);

            thisDataSet.Tables["SubjectSchedFile"].Rows.Add(thisRow);
            thisAdapter.Update(thisDataSet, "SubjectSchedFile");

            MessageBox.Show("Recorded");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu();
            Hide();
            main.ShowDialog();
            Close();
        }
        private bool IsConflict()
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            string query = $"SELECT COUNT(*) FROM SUBJECTSCHEDFILE WHERE SSFDAYS = '{DaysTextBox.Text}' AND " +
                           $"((SSFSTARTTIME <= '{TimeStartTextBox.Text}' AND SSFENDTIME > '{TimeStartTextBox.Text}') " +
                           $"OR (SSFSTARTTIME < '{TimeEndTextBox.Text}' AND SSFENDTIME >= '{TimeEndTextBox.Text}'))";
            OleDbCommand command = new OleDbCommand(query, connection);

            try
            {
                connection.Open();
                int conflictCount = (int)command.ExecuteScalar();
                return conflictCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for conflict: " + ex.Message);
                return true;
            }
            finally
            {
                connection.Close();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            EdpCodeTextBox.Text = "";
            SubjectCodeTextBox.Text = "";
            TimeStartTextBox.Text = "";
            TimeEndTextBox.Text = "";
            DaysTextBox.Text = "";
            SectionTextBox.Text = "";
            RoomTextBox.Text = "";
            SchoolYearTextBox.Text = "";
            AmComboBox.SelectedIndex = -1;
        }

        private void SubjectCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OleDbConnection codeConnection = new OleDbConnection(connectionString);
                codeConnection.Open();
                OleDbCommand thisCommand = codeConnection.CreateCommand();
                string Ole = "Select * From SUBJECTFILE";
                string sched = "SELECT * FROM SUBJECTSCHEDFILE";
                thisCommand.CommandText = Ole;
                OleDbDataReader thisDataReader = thisCommand.ExecuteReader();
                OleDbDataAdapter schedAdapter = new OleDbDataAdapter(sched, codeConnection);
                OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(schedAdapter);
                DataSet thisDataSet = new DataSet();
                schedAdapter.Fill(thisDataSet, "SubjectPreqFile");

                bool found = false;
                string description = "";
                string code = SubjectCodeTextBox.Text;

                while (thisDataReader.Read())
                {
                    if (thisDataReader["SFSUBJCODE"].ToString().Trim().ToUpper() == SubjectCodeTextBox.Text.Trim().ToUpper())
                    {
                        found = true;
                        description = thisDataReader["SFSUBJDESC"].ToString();
                        break;
                    }

                }

                if (found == false)
                    MessageBox.Show("Subject Code Not Found");
                else
                {
                    DescriptionLabel.Text = description;
                }
            }
        }

        private void CancelButton_Click_1(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu();
            Hide();
            main.ShowDialog();
            Close();
        }
    }
}
