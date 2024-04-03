using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows.Forms;

namespace StudentBD
{
    public partial class StudentDelite : Form
    {
        private string _connectionString;
        public StudentDelite(string connectionString)
        {
            InitializeComponent(); // Предполагается, что у вас есть метод инициализации компонентов
            _connectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string LastName=textBox1.Text;
                int id = Convert.ToInt32(textBox2.Text);


                Class.StudentManager studentManager = new Class.StudentManager(_connectionString);

                string result = await studentManager.DeleteStudentAsync(LastName,id);
                MessageBox.Show(result);

                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from database: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
