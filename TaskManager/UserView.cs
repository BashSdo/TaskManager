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
    /// <summary>
    /// Форма просмотра/редактирования пользователя
    /// </summary>
    public partial class UserView : Form
    {
        /// <summary>
        /// Просматриваемый/редактируемый пользователь
        /// </summary>
        public User User;

        /// <summary>
        /// Конструктор формы просмотра/редактирования пользователя
        /// </summary>
        public UserView()
        {
            User = new User("");
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User.Name = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
