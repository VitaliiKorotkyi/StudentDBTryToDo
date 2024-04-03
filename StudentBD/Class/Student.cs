using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentBD.Class
{
    public class Student
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public byte Age { get; private set; }
        public string Addres { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }
        public double score;
            public double Score
        {
            get // Метод доступа для чтения
            {
                return score; // Возвращает значение поля score
            }
            private set // Метод доступа для записи
            {
                if (value >= 0 && value <= 100) // Проверка на допустимые значения
                {
                    score = value; // Устанавливает значение поля score, если оно находится в допустимом диапазоне
                }
                else
                {
                    throw new ArgumentException("Значение среднего бала должно быть в диапазоне от 0 до 100"); // Возбуждение исключения при попытке установить недопустимое значение
                }
            }
        }
        public Student(int id, string firstName, string lastName, byte age, string addres, string email, string telephone, double score)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Addres = addres;
            Email = email;
            Telephone = telephone;
            Score = score;
        }
      

      
    }
}
