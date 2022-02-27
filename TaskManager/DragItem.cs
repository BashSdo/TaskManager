using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    /// <summary>
    /// Класс представляющий переносимый в ListBox объект
    /// </summary>
    public class DragItem
    {
        /// <summary>
        /// Индекс переносимого элемента
        /// </summary>
        public int Index;

        /// <summary>
        /// Переносимый элемент
        /// </summary>
        public object Data;

        /// <summary>
        /// ListBox-владелец переносимого объекта
        /// </summary>
        public ListBox List;

        /// <summary>
        /// Конструктор переносимого объекта
        /// </summary>
        /// <param name="list">ListBox-владелец переносимого объекта</param>
        /// <param name="index">Индекс переносимого элемента</param>
        /// <param name="data">Переносимый элемент</param>
        public DragItem(ListBox list, int index, object data)
        {
            List = list;
            Index = index;
            Data = data;
        }
    }
}
