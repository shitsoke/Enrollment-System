using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            DataConnectionString connection = new DataConnectionString();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SubjectEntryButton_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide();
            form1.ShowDialog();
            Close();
        }

        private void StudentEntryButton_Click(object sender, EventArgs e)
        {
            StudentEntry studentEntry = new StudentEntry();
            Hide();
            studentEntry.ShowDialog();
            Close();
        }

        private void SubjectScheduleButton_Click(object sender, EventArgs e)
        {
            SubjectScheduleEntry subjectScheduleEntry = new SubjectScheduleEntry();
            Hide();
            subjectScheduleEntry.ShowDialog();
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StudentEnrollmentEntry studentEnrollment = new StudentEnrollmentEntry();
            Hide();
            studentEnrollment.ShowDialog();
            Close();

        }
    }
}
