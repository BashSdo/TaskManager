using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    /// <summary>
    /// Класс представление пользователя приложения
    /// </summary>
    public class User
    {
        private static UInt32 CurrentID = 0;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public UInt32 ID { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор безымянного пользователя
        /// </summary>
        public User() : this("") { }

        /// <summary>
        /// Конструктор пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        public User(string name)
        {
            ID = CurrentID;
            Name = name;

            CurrentID += 1;
        }

        /// <summary>
        /// Метод отображения пользователя в текстовом формате
        /// </summary>
        /// <returns>Строка представление пользователя</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
