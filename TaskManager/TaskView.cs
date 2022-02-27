using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class TaskView : Form
    {
        /// <summary>
        /// Текущая открытая задача
        /// </summary>
        public Record Record;

        /// <summary>
        /// Конструктор формы просмотра задачи для созадния новой задачи
        /// </summary>
        public TaskView(): this(new Record { Description = "Введите текст задачи", Date = DateTime.Now, Responsible = "Иванов Иван Иванович", Status = Status.Todo })
        {
            button1.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
        }

        /// <summary>
        /// Конструктор формы просмотра задачи для просмотра существующей задачи
        /// </summary>
        /// <param name="record">Задача для просмотра</param>
        public TaskView(Record record)
        {
            Record = record;

            InitializeComponent();

            foreach (Status s in Enum.GetValues(typeof(Status)))
            {
                comboBox1.Items.Add(s);
            }

            textBox1.Text = record.Description;
            textBox2.Text = record.Date.ToLongDateString();
            textBox3.Text = record.Responsible;
            comboBox1.SelectedItem = record.Status;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Ок"
        /// </summary>
        /// <param name="sender">Кнопка "Ок"</param>
        /// <param name="e">Событие нажатия на кнопку</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Record.Description = textBox1.Text;
            Record.Date = DateTime.Parse(textBox2.Text);
            Record.Responsible = textBox3.Text;
            Record.Status = (Status)comboBox1.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
