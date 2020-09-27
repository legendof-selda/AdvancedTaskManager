using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedTaskManager
{
    public partial class ProcessMonitor : Form
    {
        private int ProcessID;
        private string ProcessName;
        private Process process;
        private TimeSpan start;
        private int refreshrate = 350;
        PerformanceCounter CPU;
        PerformanceCounter Memory;
        private long rammemory = 0;
        public ProcessMonitor(int ProcessID, long ram)
        {
            InitializeComponent();
            this.ProcessID = ProcessID;
            rammemory = ram;
            rammemory /= 1024;
        }

        private void ProcessMonitor_Load(object sender, EventArgs e)
        {
            try{
                process = Process.GetProcessById(ProcessID);
                ProcessName = process.ProcessName;
            }
            catch{
                listView1.Items.Add(new ListViewItem(new string[] { ProcessName, ProcessID + "", start.ToString(), "Error" }));
                return;
            }
            if (process == null)
            {
                listView1.Items.Add(new ListViewItem(new string[] { ProcessName, ProcessID + "", start.ToString(), "Process Terminated" }));
                return;
            }
            start = new TimeSpan();
            listView1.Items.Add(new ListViewItem(new string[] { ProcessName, ProcessID + "", start.ToString(), "Ready" }));
            start = DateTime.Now.TimeOfDay;
            CPU = new PerformanceCounter("Process", "% Processor Time", ProcessName);
            CPU.NextValue();
            Memory = new PerformanceCounter("Process", "Working Set", ProcessName);
            Memory.NextValue();
            timer1.Interval = refreshrate;
            timer1.Start();
            //try
            //{
            //    Thread thread = new Thread(new ThreadStart(delegate()
            //    {
            //        while (!stop)
            //        {
            //            try
            //            {
            //                process.Refresh();
            //                string[] details = new string[]{
            //                    process.ProcessName, process.Id+"", DateTime.Now.TimeOfDay.Subtract(start).ToString(),
            //                    (process.Responding ? "Running" : "Not Responding"), process.WorkingSet64+"",process.PeakWorkingSet64+"",
            //                    process.HandleCount+"", process.Threads.Count+"",
            //                    process.TotalProcessorTime.Duration().Hours.ToString()+":"+
            //                    process.TotalProcessorTime.Duration().Minutes.ToString()+":"+
            //                    process.TotalProcessorTime.Duration().Seconds.ToString()+"."+
            //                    process.TotalProcessorTime.Duration().Milliseconds.ToString(),
            //                    process.UserProcessorTime.Duration().Hours.ToString()+":"+
            //                    process.UserProcessorTime.Duration().Minutes.ToString()+":"+
            //                    process.UserProcessorTime.Duration().Seconds.ToString()+"."+
            //                    process.UserProcessorTime.Duration().Milliseconds.ToString(),
            //                    process.PrivilegedProcessorTime.Duration().Hours.ToString()+":"+
            //                    process.PrivilegedProcessorTime.Duration().Minutes.ToString()+":"+
            //                    process.PrivilegedProcessorTime.Duration().Seconds.ToString()+"."+
            //                    process.PrivilegedProcessorTime.Duration().Milliseconds.ToString(),
            //                    process.VirtualMemorySize64+"", process.PrivateMemorySize64+"", process.PagedMemorySize64+"", process.PagedSystemMemorySize64+""
            //                };
            //                this.listView1.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
            //                {
            //                    listView1.Items.Add(new ListViewItem(details));
            //                }));
            //                this.chart1.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
            //                {

            //                }));
            //                this.chart2.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
            //                {

            //                }));
            //                Thread.Sleep(refreshrate);
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.ToString());
            //            }
            //        }
            //    }));
            //}
            //catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void stopMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            listView1.Items.Add(new ListViewItem(new string[] { ProcessName, ProcessID + "", (DateTime.Now.TimeOfDay - start).ToString(), "Stopped" }));
        }

        private void endTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you Want to Kill " + ProcessName + "(" + ProcessID + ")?\n It could effect the system!", "Confirm Process Kill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            timer1.Stop();
            Process.GetProcessById(ProcessID).Kill();
            listView1.Items.Add(new ListViewItem(new string[] { ProcessName, ProcessID + "", (DateTime.Now.TimeOfDay - start).ToString(), "Terminated By User" }));
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int r = Int32.Parse(toolStripTextBox1.Text);
                r = Math.Abs(r);
                refreshrate = r;
                timer1.Interval = refreshrate;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                process.Refresh();
                if (process.HasExited)
                {
                    timer1.Stop();
                    listView1.Items.Add(new ListViewItem(new string[] { ProcessName, ProcessID + "", (DateTime.Now.TimeOfDay - start).ToString(), "Exited with exit code "+process.ExitCode }));
                    return;
                }
                string[] details = new string[]{
                    process.ProcessName, process.Id+"", DateTime.Now.TimeOfDay.Subtract(start).ToString(),
                    (process.Responding ? "Running" : "Not Responding"), process.WorkingSet64+"",process.PeakWorkingSet64+"",
                    process.HandleCount+"", process.Threads.Count+"",
                    process.TotalProcessorTime.Duration().Hours.ToString()+":"+
                    process.TotalProcessorTime.Duration().Minutes.ToString()+":"+
                    process.TotalProcessorTime.Duration().Seconds.ToString()+"."+
                    process.TotalProcessorTime.Duration().Milliseconds.ToString(),
                    process.UserProcessorTime.Duration().Hours.ToString()+":"+
                    process.UserProcessorTime.Duration().Minutes.ToString()+":"+
                    process.UserProcessorTime.Duration().Seconds.ToString()+"."+
                    process.UserProcessorTime.Duration().Milliseconds.ToString(),
                    process.PrivilegedProcessorTime.Duration().Hours.ToString()+":"+
                    process.PrivilegedProcessorTime.Duration().Minutes.ToString()+":"+
                    process.PrivilegedProcessorTime.Duration().Seconds.ToString()+"."+
                    process.PrivilegedProcessorTime.Duration().Milliseconds.ToString(),
                    process.VirtualMemorySize64+"", process.PrivateMemorySize64+"", process.PagedMemorySize64+"", process.PagedSystemMemorySize64+""
                };
                this.listView1.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                {
                    listView1.Items.Add(new ListViewItem(details)).Selected = true;
                    listView1.SelectedItems[0].EnsureVisible();
                }));
                this.chart1.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                {
                    chart1.Series[0].Points.AddY(CPU.NextValue());
                    if (chart1.Series[0].Points.Count > 30)
                        chart1.Series[0].Points.RemoveAt(0);
                }));
                this.chart2.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                {
                    double perc = Memory.NextValue() / 1024.0;
                    perc /= rammemory;
                    perc *= 100;
                    chart2.Series[0].Points.AddY(perc+5);
                    if (chart2.Series[0].Points.Count > 30)
                        chart2.Series[0].Points.RemoveAt(0);
                }));
            }
            catch { };
        }
    }
}
