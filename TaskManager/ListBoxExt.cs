using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    /// <summary>
    /// Класс-расширение ListBox
    /// </summary>
    public static class ListBoxExt
    {
        /// <summary>
        /// Метод вычисления позиции в списке по экранным координатам
        /// </summary>
        /// <param name="list">ListBox, позицию элемента которого необходимо вычислить</param>
        /// <param name="point">Точка на экране, которую необходимо перевести в индекс элемента ListBox</param>
        /// <returns>Индекс элемента ListBox</returns>
        public static int IndexFromScreenPoint(this ListBox list, Point point)
        {
            point = list.PointToClient(point);
            return list.IndexFromPoint(point);
        }
    }
}
