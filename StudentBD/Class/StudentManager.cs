using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudentBD.Class
{

    internal class StudentManager
    {
        private string connectionString;
        public ConcurrentDictionary<int, Student> Students { get; private set; }

        public StudentManager(string connectionString)
        {
            this.connectionString = connectionString;
            Students = new ConcurrentDictionary<int, Student>();
        }

        public async Task<string> AddStudentAsync(string firstName, string lastName, byte age, string address, string email, string telephone, double score)
        {
            try
            {
                // Проверка на пустые строки
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(telephone))
                {
                    return "Ошибка: Пожалуйста, заполните все поля.";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"INSERT INTO StudentManager (FirstName, LastName, Age, Addres, Email, Telephone, Score) 
                                     VALUES (@FirstName, @LastName, @Age, @Addres, @Email, @Telephone, @Score);
                                     SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Addres", address);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Telephone", telephone);
                        command.Parameters.AddWithValue("@Score", score);

                        int id = Convert.ToInt32(await command.ExecuteScalarAsync());
                        var student = new Student(id, firstName, lastName, age, address, email, telephone, score);
                        Students.TryAdd(id, student);

                        return "Студент успешно добавлен.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка при добавлении студента: {ex.Message}";
            }
        }

        public async Task<string> DeleteStudentAsync(string lastName, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"DELETE FROM StudentManager WHERE LastName = @LastName AND Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Id", id);

                        await command.ExecuteNonQueryAsync();

                        // Remove the student from the dictionary
                        Student removedStudent;
                        Students.TryRemove(id, out removedStudent);

                        return "Студент успешно удален.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка при удалении студента: {ex.Message}";
            }
        }
    }
}
