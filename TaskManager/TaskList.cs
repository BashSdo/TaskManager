using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class TaskList : Form
    {
        /// <summary>
        /// Текущий выбранный список
        /// </summary>
        private ListBox SelectedList;

        /// <summary>
        /// Конструктор формы списка задач
        /// </summary>
        public TaskList()
        {
            InitializeComponent();

            var records = LoadRecords();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            foreach (Record record in records)
            {
                if (record.Status == Status.Todo)
                {
                    listBox1.Items.Add(record);
                } else if (record.Status == Status.InProgress)
                {
                    listBox2.Items.Add(record);
                } else if (record.Status == Status.Done)
                {
                    listBox3.Items.Add(record);
                }
            }
        }

        /// <summary>
        /// Обработчик событий нажатия мыши на список
        /// 
        /// Отвечает за работу drag-n-drop функционала списков
        /// </summary>
        /// <param name="sender">Список, на котором произошло нажатие</param>
        /// <param name="e">Событие о нажатие мышью</param>
        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox list = sender as ListBox;

            if (e.Button != MouseButtons.Left) return;

            int index = list.IndexFromPoint(e.Location);
            list.SelectedIndex = index;
            if (index < 0) return;

            object drag_item = new DragItem(list, index, list.Items[index]);
            list.DoDragDrop(drag_item, DragDropEffects.Move);
        }

        /// <summary>
        /// Обработчик событий начала перетаскивания в списке
        /// 
        /// Отвечает за работу drag-n-drop функционала списков
        /// </summary>
        /// <param name="sender">Список, в котором началось перетаскивание</param>
        /// <param name="e">Событие о начале перетаскивания</param>
        private void listBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Обработчик событий наведения перетаскиваемого элемента на списке
        /// 
        /// Отвечает за работу drag-n-drop функционала списков
        /// </summary>
        /// <param name="sender">Список, в который происходит перетаскивание</param>
        /// <param name="e">Событие о наведение перетаскиваемого элемента</param>
        private void listBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Effect != DragDropEffects.Move) return;


            ListBox lst = sender as ListBox;

            lst.SelectedIndex =
                lst.IndexFromScreenPoint(new Point(e.X, e.Y));
        }

        /// <summary>
        /// Обработчик событий завершения перетаскивания элемента в список
        /// 
        /// Отвечает за работу drag-n-drop функционала списков
        /// </summary>
        /// <param name="sender">Список, в который происходит перетаскивание</param>
        /// <param name="e">Событие о завершении перетаскивания</param>
        private void listBox_DragDrop(object sender, DragEventArgs e)
        {
            ListBox list = sender as ListBox;

            DragItem drag_item = (DragItem)e.Data.GetData(typeof(DragItem));
            if (drag_item == null) return;

            int new_index =
                list.IndexFromScreenPoint(new Point(e.X, e.Y));

            if (new_index == -1) new_index = 0;

            drag_item.List.Items.RemoveAt(drag_item.Index);
            list.Items.Insert(new_index, drag_item.Data);
            list.SelectedIndex = new_index;

            if (list == listBox1)
            {
                ((Record)drag_item.Data).Status = Status.Todo;
            }
            else if (list == listBox2)
            {
                ((Record)drag_item.Data).Status = Status.InProgress;
            }
            else if (list == listBox3)
            {
                ((Record)drag_item.Data).Status = Status.Done;
            }

            SaveRecords(GetRecords());
        }

        /// <summary>
        /// Обработчик события изменения выбранного элемента в списке
        /// 
        /// Отвечает за переключение активного списка
        /// </summary>
        /// <param name="sender">Список, в котором изменился выбранный элемент</param>
        /// <param name="e">Событие об изменении активного элемента списка</param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;

            if (SelectedList == list) return;

            if (SelectedList != null)
            {
                SelectedList.SelectedIndex = -1;
            }
            SelectedList = list;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// 
        /// Вызывает окно создания новой задачи
        /// </summary>
        /// <param name="sender">Кнопка "Добавить"</param>
        /// <param name="e">Событие о нажатии кнопки</param>
        private void button1_Click(object sender, EventArgs e)
        {
            var view = new TaskView();
            if (view.ShowDialog() != DialogResult.OK) return;

            var record = view.Record;
            switch (record.Status)
            {
                case Status.Todo:
                    listBox1.Items.Add(record);
                    break;
                case Status.InProgress:
                    listBox2.Items.Add(record);
                    break;
                case Status.Done:
                    listBox3.Items.Add(record);
                    break;
            }

            SaveRecords(GetRecords());
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// 
        /// Удаляет задачу из списка
        /// </summary>
        /// <param name="sender">Кнопка "Удалить"</param>
        /// <param name="e">Событие о нажатии кнопки</param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedList == null || SelectedList.SelectedItem == null || SelectedList != listBox3) return;

            SelectedList.Items.RemoveAt(SelectedList.SelectedIndex);

            SaveRecords(GetRecords());
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Просмотр"
        /// 
        /// Вызывает окно просмотра задачи
        /// </summary>
        /// <param name="sender">Кнопка "Просмотр"</param>
        /// <param name="e">Событие о нажатии кнопки</param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (SelectedList == null || SelectedList.SelectedItem == null) return;

            var view = new TaskView((Record)SelectedList.SelectedItem);
            view.ShowDialog();
        }

        /// <summary>
        /// Загружает задачи из хранилища и возвращает ввиде перечисления
        /// </summary>
        /// <returns>Загруженные задачи</returns>
        private IEnumerable<Record> LoadRecords()
        {
            FileStream stream = null;
            List<Record> list = new List<Record>();
            
            try
            {
                stream = File.Open("data.json", FileMode.Open, FileAccess.Read);
                list = (List<Record>)JsonSerializer.Deserialize(stream, typeof(List<Record>));
            }
            catch (FileNotFoundException) {}
            catch (JsonException) {}
            finally
            {
                stream?.Close();
            }

            return list;
        }

        /// <summary>
        /// Сохраняет задачи в хранилище
        /// </summary>
        /// <param name="records">Задачи для сохранения</param>
        private void SaveRecords(IEnumerable<Record> records)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open("data.json", FileMode.OpenOrCreate, FileAccess.Write);
                JsonSerializer.Serialize(stream, records);
            }
            finally
            {
                stream?.Close();
            }
        }

        /// <summary>
        /// Возвращает список задач представленных в списках
        /// </summary>
        /// <returns>Задачи из списков</returns>
        private IEnumerable<Record> GetRecords()
        {
            var list = new Record[listBox1.Items.Count + listBox2.Items.Count + listBox3.Items.Count];
            listBox1.Items.CopyTo(list, 0);
            listBox2.Items.CopyTo(list, listBox1.Items.Count);
            listBox3.Items.CopyTo(list, listBox1.Items.Count + listBox2.Items.Count);
            return list.ToList();
        }
    }
}
