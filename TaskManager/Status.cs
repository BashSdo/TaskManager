using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    /// <summary>
    /// Статус задачи
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// К выполнению
        /// </summary>
        Todo,

        /// <summary>
        /// Выполняется
        /// </summary>
        InProgress,

        /// <summary>
        /// Завершена
        /// </summary>
        Done,
    }
}
