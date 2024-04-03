using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentBD
{
    public partial class StudentManager : Form
    {

        private SqlConnection _sqlConnection = null;


        public StudentManager()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["StudentManager"].ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);



        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentManager"].ConnectionString;
            using (AddStudent addStudentForm = new AddStudent(connectionString))
            {
                var result = addStudentForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Обновить данные в DataGridView после закрытия формы добавления
                    LoadDataFromDatabase();
                }
            }
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                string query = "SELECT * FROM StudentManager"; // Замените "Students" на имя вашей таблицы

                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    
                    adapter.Fill(dataTable);

                    // Преобразуйте столбец "Addres" в "Address" если он существует
                    if (dataTable.Columns.Contains("Addres"))
                    {
                        dataTable.Columns["Addres"].ColumnName = "Addres";
                    }

                    // Установите источник данных для DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error loading data from database: {ex.Message}");

            }

        }

        private void StudentManager_Load(object sender, EventArgs e)
        {
            try
            {
                _sqlConnection.Open();

                if (_sqlConnection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Соединение установлено");

                    // Загрузка данных из базы данных в DataGridView
                    LoadDataFromDatabase();
                }
                else
                {
                    MessageBox.Show("Нет соединения");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                _sqlConnection?.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentManager"].ConnectionString;
            using (var studentDeleteForm = new StudentDelite(connectionString))
            {
                var result = studentDeleteForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Обновить данные в DataGridView после закрытия формы удаления студента
                    LoadDataFromDatabase();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"LastName LIKE '%{textBox1.Text}%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Score <=25";
                    break;
                case 1:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Score >=25 AND Score <=50";
                    break;
                case 2:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Score >=50 AND Score <=75";
                    break;
                case 3:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Score >=75 AND Score <=100";
                    break;
            }
        }
    }
}
