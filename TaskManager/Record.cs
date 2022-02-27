using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    /// <summary>
    /// Представление задачи
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Текстовое описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Срок исполнения задачи
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ответственный за задачу
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        /// Текущий статус задачи
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Метод отображения задачи в текстовом формате
        /// </summary>
        /// <returns>Строка представление задачи</returns>
        public override string ToString()
        {
            return Description;
        }
    }
}
