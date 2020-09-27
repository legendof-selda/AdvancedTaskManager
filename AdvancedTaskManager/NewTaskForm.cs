using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedTaskManager
{
    public partial class NewTaskForm : Form
    {
        public NewTaskForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                textBox1.Text = openFileDialog1.FileName;
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.newProcessPath = textBox1.Text;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.newProcessPath = textBox1.Text; MessageBox.Show(textBox1.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
