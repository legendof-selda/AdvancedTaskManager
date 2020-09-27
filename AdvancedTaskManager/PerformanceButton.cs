using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedTaskManager
{
    public partial class PerformanceButton : UserControl
    {
        public Image icon
        {
            get {return performanceSampleGraph.Image; }
            set { performanceSampleGraph.Image = value; }
        }
        public String Title
        {
            get { return performanceTitle.Text; }
            set { performanceTitle.Text = value; }
        }
        public String Status
        {
            get { return performanceStatus.Text; }
            set { performanceStatus.Text = value; }
        }
        private bool selected;
        public bool Selected
        {
            get{return selected;}
            set
            { 
                selected = value;
                if(selected)
                    panel4.BackColor = System.Drawing.SystemColors.Control;
                else
                    panel4.BackColor = Color.Transparent;
            }
        }
        public PerformanceButton()
        {
            InitializeComponent();
        }
        public PerformanceButton(string title)
        {
            InitializeComponent();
            performanceTitle.Text = title;
        }
        private void panel4_MouseHover(object sender, EventArgs e)
        {
            panel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            if(selected)
                    panel4.BackColor = System.Drawing.SystemColors.Control;
                else
                    panel4.BackColor = Color.Transparent;
        }
    }
}
