using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace AdvancedTaskManager
{
    public partial class MoreSystemDetails : Form
    {
        public MoreSystemDetails()
        {
            InitializeComponent();
        }

        private void MoreSystemDetails_Load(object sender, EventArgs e)
        {
            StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
            try
            {
                StringBuilder1.AppendFormat("Operation System:  {0}\r\n", Environment.OSVersion);
                if (Environment.Is64BitOperatingSystem)
                    StringBuilder1.AppendFormat("\t\t  64 Bit Operating System\r\n");
                else
                    StringBuilder1.AppendFormat("\t\t  32 Bit Operating System\r\n");
                StringBuilder1.AppendFormat("SystemDirectory:  {0}\r\n", Environment.SystemDirectory);
                StringBuilder1.AppendFormat("ProcessorCount:  {0}\r\n", Environment.ProcessorCount);
                StringBuilder1.AppendFormat("UserDomainName:  {0}\r\n", Environment.UserDomainName);
                StringBuilder1.AppendFormat("UserName: {0}\r\n", Environment.UserName);
                //Drives
                StringBuilder1.AppendFormat("LogicalDrives:\r\n");
                foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                {
                    try
                    {
                        StringBuilder1.AppendFormat("\t Drive: {0}\n\t\t VolumeLabel: {1}\n\t\t DriveType: {2}\n\t\t DriveFormat: {3}\n\t\t TotalSize: {4}\n\t\t AvailableFreeSpace: {5}\r\n",
                            DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType, DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                    }
                    catch
                    {
                    }
                }
                StringBuilder1.AppendFormat("SystemPageSize:  {0}\n", Environment.SystemPageSize);
                StringBuilder1.AppendFormat("Version:  {0}", Environment.Version);
            }
            catch
            {
            }
            textBox1.Text = StringBuilder1.ToString();
            textBox2.Text = GetInfoFromClass("Win32_Processor");
            textBox3.Text = GetInfoFromClass("Win32_VideoController");
            textBox4.Text = GetInfoFromClass("Win32_LogicalDisk");
            textBox5.Text = GetInfoFromClass("Win32_OperatingSystem");

            comboBox1.SelectedIndexChanged -= new EventHandler(comboBox1_SelectedIndexChanged);
            List<string> classes = AdvancedTaskManager.Properties.Resources.stringWin32classes.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string stringWin32class in classes)
            {
                comboBox1.Items.Add(stringWin32class);
            }
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }
        private string GetInfoFromClass(string mcs)
        {
            StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
            try
            {
                ManagementClass ManagementClass1 = new ManagementClass(mcs);
                ManagementObjectCollection ManagemenobjCol = ManagementClass1.GetInstances();
                PropertyDataCollection properties = ManagementClass1.Properties;
                foreach (ManagementObject obj in ManagemenobjCol)
                {
                    foreach (PropertyData property in properties)
                    {
                        try
                        {
                            StringBuilder1.AppendLine(property.Name + ":  " + obj.Properties[property.Name].Value.ToString());
                        }
                        catch
                        {
                        }
                    }
                    StringBuilder1.AppendLine();
                }
            }
            catch
            {
            }
            return StringBuilder1.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;
            textBox6.Text = GetInfoFromClass("Win32_"+comboBox1.SelectedItem.ToString());
        }
    }
}
