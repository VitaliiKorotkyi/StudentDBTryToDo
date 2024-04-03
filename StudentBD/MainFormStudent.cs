using System.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace StudentBD
{
    public partial class AddStudent : Form
    {
        private string connectionString;

        public AddStudent(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
               
                string firstName = textBoxName.Text;
                string lastName = textBox1LastName.Text;
                byte age = Convert.ToByte(textBoxAge.Text);
                string address = textBoxAddres.Text;
                string email = textBoxEmail.Text;
                string telephone = textBoxTelephone.Text;
                double score = Convert.ToDouble(textBoxScore.Text);
                
           Class.StudentManager studentManager = new Class.StudentManager(connectionString);

                string result = await studentManager.AddStudentAsync(firstName, lastName, age, address, email, telephone, score);
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

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Отменить обработку события для введенной цифры
            }
        }
        private void textBox1LastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Отменить обработку события для введенной цифры
            }
        }
        private void textBoxAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            { 
            e.Handled= true;
            }
        }
        private void textBoxTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            { 
            e.Handled  =true;
            }
        }
        private void textBoxScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            { 
            e.Handled=true;
            }
        }
    }
}
