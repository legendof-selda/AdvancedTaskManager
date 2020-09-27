using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management;
using System.ServiceProcess;
using System.Threading;

namespace AdvancedTaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lvcs = new ListViewColumnSorter(0, SortOrder.Ascending);
            lvcs2 = new ListViewColumnSorter(0, SortOrder.Ascending);
            lvcs3 = new ListViewColumnSorter(0, SortOrder.Ascending);
            this.servListView.ListViewItemSorter = lvcs;
            servListView.SetSortIcon(0, SortOrder.Ascending);
            this.processAdvDetails.ListViewItemSorter = lvcs2;
            processAdvDetails.SetSortIcon(0, SortOrder.Ascending);
            this.processDetails.ListViewItemSorter = lvcs3;
            processDetails.SetSortIcon(0, SortOrder.Ascending);
            cpuCounter = new PerformanceCounter("Process", "% Processor Time", "Idle");
            ramCounter = new PerformanceCounter("Memory", "Available Bytes", String.Empty);

            format = new bool[] { true, true, true };
        }
        bool[] format;
        public double CPU
            {
                get;
                set;
            }
        public double RAM
        {
            get;
            set;
        }
        public double CPUClock
        {
            get;
            set;
        }
        public string UpTime
        {
            get
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();
                    TimeSpan ts = TimeSpan.FromSeconds(uptime.NextValue());
                    return ts.Days + ":" + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
                }
            }
        }
        private ListViewColumnSorter lvcs;
        private ListViewColumnSorter lvcs2;
        private ListViewColumnSorter lvcs3;

        bool isDrawing = true;
        bool isUpdating = true;
        public int refreshrate = 350;
        private PerformanceCounter cpuCounter = null;
        private PerformanceCounter ramCounter = null;
        private PerformanceCounter diskRead = null;
        private PerformanceCounter diskWrite = null;
        private PerformanceCounter diskusage = null;

        public System.Threading.Timer t = null;
        int threadscount = 0, handlescount = 0;
        private long ramMemory = 0;
        ServiceController[] svc;
        public string machineName;
        ImageList icons = new ImageList();
        public int tabIndex = 0;

        public static string newProcessPath;
        Dictionary<int, ProcessInfo> ProcessList;
        List<string> Users;

        delegate void PrintConsoleCallback(string text);

        private void PrintToConsole(string text)
        {
            if (this.console.InvokeRequired)
            {
                PrintConsoleCallback d = new PrintConsoleCallback(PrintToConsole);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.console.AppendText(text);
            }
        }
        public void PerformanceUpdater(object temp)
        {
            try
            {
                CPU = Math.Abs(100.0-cpuCounter.NextValue()/Environment.ProcessorCount);
                RAM = ramCounter.NextValue();
                ManagementObject obj = new ManagementObject("Win32_Processor.DeviceID='CPU0'");
                CPUClock = (Double.Parse(obj.Properties["CurrentClockSpeed"].Value.ToString()) / 1000.0);
                obj.Dispose();
            }
            catch (Exception ex)
            {
                PrintToConsole(ex.Message + Environment.NewLine);
            }
        }
        public void GetChartsReady()
        {
            string drive = (new PerformanceCounterCategory("PhysicalDisk")).GetInstanceNames()[0];
            diskRead = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", drive);
            diskWrite = new PerformanceCounter("PhysicalDisk", "% Disk Write Time", drive);
            diskusage = new PerformanceCounter("PhysicalDisk", "% Disk Time", drive);
            double size = 0;
            System.Management.ManagementObjectSearcher ms = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Name=\"\\\\\\\\.\\\\PHYSICALDRIVE" + drive[0] + "\"");
            foreach (System.Management.ManagementObject mo in ms.Get())
            {
                label71.Text = mo["Caption"].ToString();
                size = Double.Parse(mo["Size"].ToString());
                DiskCylindersLabel.Text = mo["TotalCylinders"].ToString();
                DiskHeadsLabel.Text = mo["TotalHeads"].ToString();
                DiskSectorsLabel.Text = mo["TotalSectors"].ToString();
                DiskTracksLabel.Text = mo["TotalTracks"].ToString();
            }
            System.IO.DriveInfo[] alldrives = System.IO.DriveInfo.GetDrives();
            string[] drivename = drive.Remove(0, 2).Split(' ');
            double freespace = 0;
            foreach (System.IO.DriveInfo d in alldrives)
            {
                foreach (string s in drivename)
                {
                    if (d.Name[0] == s[0])
                    {
                        freespace += d.AvailableFreeSpace;
                        break;
                    }
                }
            }
            chart2.Series[0].Points[0].SetValueY((freespace / size) * 100.0);
            chart2.Series[0].Points[1].SetValueY(((size - freespace) / size) * 100.0);
            int i = 0;
            string unit = "GB";
            while (size > 1024.0)
            {
                size /= 1024.0;
                i++;
            }
            switch (i)
            {
                case 0:
                    unit = " Bytes";
                    break;
                case 1:
                    unit = " KB";
                    break;
                case 2:
                    unit = " MB";
                    break;
                case 3:
                    unit = " GB";
                    break;
                case 4:
                    unit = " TB";
                    break;
            }
            DiskTotalLabel.Text = Math.Round(size, 2) + unit;

            i = 0;
            while (freespace > 1024.0)
            {
                freespace /= 1024.0;
                i++;
            }

            switch (i)
            {
                case 0:
                    unit = " Bytes";
                    break;
                case 1:
                    unit = " KB";
                    break;
                case 2:
                    unit = " MB";
                    break;
                case 3:
                    unit = " GB";
                    break;
                case 4:
                    unit = " TB";
                    break;
            }
            DiskFreeLabel.Text = Math.Round(freespace, 2) + unit;
            DiskUsedLabel.Text = Math.Round(size - freespace, 2) + unit;
            label70.Text = "Disk " + drive;
            DiskPerformanceButton.Title = label70.Text;
            try
            {
                Thread thread = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        while (isDrawing)
                        {
                            //CPU
                            this.CPUchart.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                            {
                                CPUchart.Series[0].Points.AddY(CPU + 2);
                                if (CPUchart.Series[0].Points.Count > 30)
                                    CPUchart.Series[0].Points.RemoveAt(0);
                                if (tabIndex == 2)
                                {
                                    PerfCPUperc.Text = Math.Round(CPU) + "%";
                                    PerfSpeed.Text = CPUClock + " GHz";
                                    CPUPerformanceButton.Status = PerfCPUperc.Text + " " + PerfSpeed.Text;
                                    PerfUptime.Text = UpTime;
                                    PerfHandles.Text = handlescount + "";
                                    PerfThreads.Text = threadscount + "";
                                    PerfProcesses.Text = ProcessList.Count + "";
                                }
                            }));
                            Thread.Sleep(refreshrate);//Thread sleep                             
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }));

                thread.Priority = ThreadPriority.AboveNormal;
                thread.IsBackground = true;
                thread.Start();
                //Memory
                thread = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        while (isDrawing)
                        {
                            this.MemoryChart.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                            {
                                MemoryChart.Series[0].Points.AddY(((1 - (RAM / (double)ramMemory)) * 100.0) + 1);
                                if (MemoryChart.Series[0].Points.Count > 30)
                                    MemoryChart.Series[0].Points.RemoveAt(0);
                                if (tabIndex == 2)
                                {
                                    double av = RAM, t=ramMemory;
                                    i = 0;
                                    while (av > 1024.0)
                                    {
                                        av /= 1024.0;
                                        t /= 1024.0;
                                        i++;
                                    }

                                    switch (i)
                                    {
                                        case 0:
                                            unit = " Bytes";
                                            break;
                                        case 1:
                                            unit = " KB";
                                            break;
                                        case 2:
                                            unit = " MB";
                                            break;
                                        case 3:
                                            unit = " GB";
                                            break;
                                        case 4:
                                            unit = " TB";
                                            break;
                                    }
                                    label50.Text = Math.Round(av, 2) + unit;
                                    label52.Text = Math.Round(t - av, 2) + unit;
                                    MemoryPerformanceButton.Status = Math.Round(t - av,1) + "/" + Math.Round(t,1) + unit + " (" + Math.Round((t - av) / t * 100.0) + "%)";
                                }
                            }));
                            Thread.Sleep(refreshrate);//Thread sleep 
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }));

                thread.Priority = ThreadPriority.AboveNormal;
                thread.IsBackground = true;
                thread.Start();
                //Disk
                thread = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        while (isDrawing)
                        {
                            this.diskUsageChart.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                            {
                                double perc = diskusage.NextValue();
                                diskUsageChart.Series[0].Points.AddY(perc);
                                if (diskUsageChart.Series[0].Points.Count > 30)
                                    diskUsageChart.Series[0].Points.RemoveAt(0);
                                if (tabIndex == 2)
                                {
                                    DiskPerformanceButton.Status = Math.Round(perc) + "%";
                                }
                            }));
                            this.diskReadWriteChart.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                            {
                                double r = diskRead.NextValue();
                                double w = diskWrite.NextValue();
                                diskReadWriteChart.Series[0].Points.AddY(r * 100.0);
                                diskReadWriteChart.Series[1].Points.AddY(w * 100.0);
                                if (diskReadWriteChart.Series[0].Points.Count > 30)
                                    diskReadWriteChart.Series[0].Points.RemoveAt(0);
                                if (diskReadWriteChart.Series[1].Points.Count > 30)
                                    diskReadWriteChart.Series[1].Points.RemoveAt(0);
                                if (tabIndex == 2)
                                {
                                    DiskReadSpeedLabel.Text = Math.Round(r * 100, 3) + " KB/s";
                                    DiskWriteSpeedLabel.Text = Math.Round(w*100,3) + " KB/s";

                                }
                            }));
                            Thread.Sleep(refreshrate);//Thread sleep 
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }));

                thread.Priority = ThreadPriority.Normal;
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                PrintToConsole(ex.Message + Environment.NewLine);
            }
        }
        public void GetProcessListReady()
        {
            ProcessList = new Dictionary<int, ProcessInfo>();
            processAdvDetails.SmallImageList = icons;
            processDetails.SmallImageList = icons;
            Users = new List<string>();
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_UserAccount WHERE Status='OK'");
            ManagementObjectCollection users = usersSearcher.Get();
            foreach (ManagementObject obj in users)
            {
                Users.Add(obj["Name"].ToString());
            }
            LoadAllProcessesOnStartup();
            try
            {
                Thread thread = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        while (isUpdating)
                        {
                            //this.processAdvDetails.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                            //{
                            LoadAllProcesses(null);
                            //}));

                            Thread.Sleep(refreshrate);//Thread sleep 
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }));

                thread.Priority = ThreadPriority.AboveNormal;
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                PrintToConsole(ex.Message + Environment.NewLine);
            }
        }
        public void GetServicesReady()
        {
            ListAllServicesOnStartUp();
            try
            {
                Thread thread = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        while (isUpdating)
                        {
                            if (tabIndex != 5) continue;
                            this.servListView.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                            {
                                UpdateAllServices();
                            }));
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintToConsole(ex.Message + Environment.NewLine);
                    }
                }));
            }
            catch (Exception ex)
            {
                PrintToConsole(ex.Message + Environment.NewLine);
            }
        }
        public void LoadAllProcessesOnStartup()
        {
            Process[] processes = null;
            try
            {
                processes = Process.GetProcesses();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString(), "Error ocuured: " + e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole(e.StackTrace.ToString() + Environment.NewLine);
                return;
            }
            threadscount = 0; handlescount = 0;
            foreach (Process p in processes)
            {
                ProcessInfo pi = null;
                try
                {
                    pi = new ProcessInfo(p); ;
                    icons.Images.Add(p.Id+"", Icon.ExtractAssociatedIcon(p.MainModule.FileName));
                }
                catch (System.ComponentModel.Win32Exception w32exp) {
                    PrintToConsole("Load all process on startup \n" + w32exp.Message + Environment.NewLine);
                }
                catch (Exception e) { PrintToConsole(e.StackTrace.ToString() + Environment.NewLine); }
                finally
                {
                    
                    threadscount += p.Threads.Count;
                    handlescount += p.HandleCount;
                    if (pi != null)
                    {
                        processAdvDetails.Items.Add(new ListViewItem(pi.ForProcessAdvList())).ImageIndex = icons.Images.Count - 1;
                        processDetails.Items.Add(new ListViewItem(pi.ForProcessDetails(CPU, ramMemory, format))).ImageIndex = icons.Images.Count - 1;
                        PCPUperc.Text = Math.Round(CPU, 2) + "%";
                        PMemoryPerc.Text = Math.Round((1 - (RAM / (double)ramMemory)) * 100.0, 2) + "%";
                    }
                }
            }
            AdvStatus.Text = "Processes: " + processes.Length + "  Threads: " + threadscount + "  Handles: " + handlescount;
        }
        public void LoadAllProcesses(object temp)
        {
            Process[] processes = null;
            try
            {
                //ProcessList.Clear();
                List<int> orgids = new List<int>();
                processes = Process.GetProcesses();
                int threadscount = 0, handlescount = 0;
                foreach (Process p in processes)
                {
                    ProcessInfo pi = null;
                    try
                    {
                        pi = new ProcessInfo(p);
                        if (!icons.Images.ContainsKey(p.Id.ToString()))
                            icons.Images.Add(p.Id.ToString(), Icon.ExtractAssociatedIcon(p.MainModule.FileName));
                    }
                    catch (System.ComponentModel.Win32Exception w32exp)
                    {
                        PrintToConsole("Load all process \n" + w32exp.Message + Environment.NewLine);
                    }
                    catch (Exception e) { PrintToConsole(e.StackTrace.ToString() + Environment.NewLine); }
                    finally
                    {
                        
                        threadscount += p.Threads.Count;
                        handlescount += p.HandleCount;
                        if (pi != null)
                        {
                            if (!ProcessList.ContainsKey(p.Id))
                                ProcessList.Add(p.Id, pi);
                            else
                                ProcessList[p.Id].Update(p);
                            orgids.Add(p.Id);
                        }
                    }
                }
                if (tabIndex == 4)
                {
                    processAdvDetails.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                    {
                        bool changed = false;
                        List<int> ids = new List<int>(orgids);
                        foreach (ListViewItem lvi in processAdvDetails.Items) //Removing and updating the loist
                        {
                            int id = Convert.ToInt32(lvi.SubItems[1].Text);
                            if (!ids.Contains(id))
                            {
                                //lvi.SubItems[8].Text=lvi.SubItems[1].Text.ToString()+" removed";
                                if (icons.Images.ContainsKey(id.ToString()))
                                    icons.Images.RemoveByKey(id.ToString());
                                processAdvDetails.Items.RemoveAt(lvi.Index); //Removes the process from Listview
                                if (ProcessList.ContainsKey(id))
                                    ProcessList.Remove(id);
                                changed = true;
                            }
                            else
                            {
                                bool valchanged = false;
                                string[] details = ProcessList[id].ForProcessAdvList();
                                for (int i = 0; i < 7; i++)
                                {
                                    if (ProcessList[id].updates[i])
                                    {
                                        lvi.SubItems[3 + i].Text = details[3 + i];
                                        if (!valchanged) valchanged = true;
                                    }
                                }
                                //if (!lvi.SubItems[3].Text.ToString().Equals(details[3].ToString()))
                                //{
                                //    lvi.SubItems[3].Text = details[3].ToString(); valchanged = true;
                                //}
                                //if (!lvi.SubItems[4].Text.ToString().Equals(details[4].ToString()))
                                //{
                                //    lvi.SubItems[4].Text = details[4].ToString(); valchanged = true;
                                //}
                                //if (!lvi.SubItems[5].Text.ToString().Equals(details[5].ToString()))
                                //{
                                //    lvi.SubItems[5].Text = details[5].ToString(); valchanged = true;
                                //}
                                //if (!lvi.SubItems[6].Text.ToString().Equals(details[6].ToString()))
                                //{
                                //    lvi.SubItems[6].Text = details[6].ToString(); valchanged = true;
                                //}
                                //if (!lvi.SubItems[7].Text.ToString().Equals(details[7].ToString()))
                                //{
                                //    lvi.SubItems[7].Text = details[7].ToString(); valchanged = true;
                                //}
                                //if (!lvi.SubItems[7].Text.ToString().Equals(details[7].ToString()))
                                //{
                                //    lvi.SubItems[7].Text = details[7].ToString(); valchanged = true;
                                //}
                                //if (!lvi.SubItems[8].Text.ToString().Equals(details[8].ToString()))
                                //{
                                //    lvi.SubItems[8].Text = details[8].ToString(); valchanged = true;
                                //}
                                ids.Remove(id); //Removes the process from the list
                                if (currentprocessColoring.Checked)
                                {
                                    if (valchanged)
                                    { lvi.BackColor = Color.Yellow; changed = true; }
                                    else if (!valchanged && lvi.BackColor == Color.Yellow)
                                        lvi.BackColor = System.Drawing.SystemColors.Window;
                                }
                                ProcessList[id].ConfirmUpdate();
                            }
                        }
                        foreach (int i in ids) // Add extra processes to listview
                        {
                            ListViewItem lvi = new ListViewItem(ProcessList[i].ForProcessAdvList());
                            if (currentprocessColoring.Checked)
                                lvi.BackColor = Color.Yellow;
                            else
                                lvi.BackColor = System.Drawing.SystemColors.Window;
                            lvi.ImageKey = i.ToString();
                            processAdvDetails.Items.Add(lvi);
                            changed = true;
                        }
                        AdvStatus.Text = "Processes: " + processes.Length + "  Threads: " + threadscount + "  Handles: " + handlescount;
                        if (changed && (lvcs2.ColumnToSort >= 1 && lvcs2.ColumnToSort <= 7))
                            processAdvDetails.Sort();
                    }));
                }
                else if (tabIndex == 1)
                {
                    processDetails.Invoke(new System.Windows.Forms.MethodInvoker(delegate()
                    {
                        PCPUperc.Text = Math.Round(CPU, 2) + "%";
                        foreach (ListViewItem lvi in processDetails.Items)
                        {
                            int id = Convert.ToInt32(lvi.SubItems[1].Text);
                            if (!orgids.Contains(id))
                            {
                                //lvi.SubItems[8].Text=lvi.SubItems[1].Text.ToString()+" removed";
                                if (icons.Images.ContainsKey(id.ToString()))
                                    icons.Images.RemoveByKey(id.ToString());
                                processDetails.Items.RemoveAt(lvi.Index); //Removes the process from Listview
                                if (ProcessList.ContainsKey(id))
                                    ProcessList.Remove(id);
                            }
                            else
                            {
                                string[] details = ProcessList[id].ForProcessDetails(CPU, ramMemory, format);
                                if (!lvi.SubItems[2].Text.Equals(details[2]))
                                    lvi.SubItems[2].Text = details[2];
                                if (!lvi.SubItems[3].Text.Equals(details[3]))
                                    lvi.SubItems[3].Text = details[3];
                                if (!lvi.SubItems[4].Text.Equals(details[4]))
                                    lvi.SubItems[4].Text = details[4];
                                orgids.Remove(id);
                            }
                        }
                        foreach (int i in orgids) // Add extra processes to listview
                        {
                            ListViewItem lvi = new ListViewItem(ProcessList[i].ForProcessDetails(CPU, ramMemory, format));
                            lvi.ImageKey = i.ToString();
                            processDetails.Items.Add(lvi);
                        }
                        PCPUperc.Text = Math.Round(CPU, 2) + "%";
                        PMemoryPerc.Text = Math.Round((1 - (RAM / (double)ramMemory)) * 100.0, 2) + "%";
                    }));
                }
            }
            catch (Exception e) {PrintToConsole(e.StackTrace.ToString()+Environment.NewLine); }
        }
        private string ToString(object ManagementObj)
        {
            if (ManagementObj == null)
                return "";
            else
                return ManagementObj.ToString();
        }
        private void ListAllServicesOnStartUp()
        {
            svc = ServiceController.GetServices();   
            int i=0;
            foreach (ServiceController service in svc)
            {
                try
                {
                    ManagementPath p = new ManagementPath("Win32_Service.Name='" + service.ServiceName + "'");
                    ManagementObject ManagementObj = new ManagementObject(p);
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = service.ServiceName;
                    {
                        string id = ToString(ManagementObj["ProcessId"]);
                        if (id.Equals("0"))
                            id = "";
                        lvi.SubItems.Add(id);
                    }
                    lvi.SubItems.Add(ManagementObj["Description"] == null ? ToString(ManagementObj["Caption"]) : ManagementObj["Description"].ToString());
                    lvi.SubItems.Add(service.Status+"");
                    lvi.SubItems.Add(ToString(ManagementObj["StartMode"]));
                    lvi.SubItems.Add(ToString(ManagementObj["startname"]));
                    string path = ToString(ManagementObj["PathName"]).Replace("\"", "");
                    if (path.Contains("-k"))
                    {
                        string[] details = path.Split(new string[]{" -k "}, 2, StringSplitOptions.None);
                        lvi.SubItems.Add(details[1]);
                        lvi.SubItems.Add(details[0]);
                    }
                    else
                    {
                        lvi.SubItems.Add("");
                        lvi.SubItems.Add(path);
                    }
                    lvi.SubItems.Add(service.CanPauseAndContinue?"Yes":"No");
                    lvi.SubItems.Add(service.CanStop ? "Yes" : "No");
                    servListView.Items.Add(lvi).Tag = i++;
                }
                catch (Exception e) { PrintToConsole(service.ServiceName + " "+ e.Message + Environment.NewLine+ e.StackTrace.ToString() + "\n"); }
            }
        }
        private void UpdateAllServices()
        {
            if (tabIndex != 5) return;
            bool changed = false;
            svc = ServiceController.GetServices();
            int i = 0;
            for (i = 0; i < svc.Length; i++)
            {
                ListViewItem lvi;
                try
                {
                    lvi = servListView.Items[i];
                }
                catch { i--; continue; }
                if (!lvi.SubItems[3].Text.Equals(svc[Convert.ToInt32(lvi.Tag.ToString())].Status.ToString()))
                {
                    lvi.SubItems[3].Text = svc[i].Status.ToString();
                    try
                    {
                        lvi.SubItems[1].Text = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(lvi.SubItems[7].Text))[0].Id + "";
                    }
                    catch
                    {
                        lvi.SubItems[1].Text = "";
                    }
                    changed = true;
                }
            }
            if(changed && (lvcs.ColumnToSort==3 || lvcs.ColumnToSort==1))
                servListView.Sort();
        }
        
        private void GetStartUpApps()
        {
            ManagementClass cls = new ManagementClass("Win32_StartupCommand");
            ManagementObjectCollection coll = cls.GetInstances();
            List<string> items = new List<string>();

            foreach (ManagementObject obj in coll)
            {
                String Name = obj["Name"].ToString();
                String Command = obj["Command"].ToString();
                String Location = obj["Location"].ToString();
                String User = obj["User"].ToString();

                ListViewItem newItem = new ListViewItem(Name);
                newItem.SubItems.Add(Command);
                newItem.SubItems.Add(Location);
                newItem.SubItems.Add(User);
                listView1.Items.Add(newItem);
            }
        }
        private void GetComputerDetails()
        {
            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            PropertyDataCollection pdc = mc.Properties;
            foreach (ManagementObject obj in moc)
            {
                foreach (PropertyData property in pdc)
                {
                    if (property.Name.Equals("Caption"))
                    {
                        try
                        {
                            OSName.Text = obj.Properties["Caption"].Value.ToString();
                        }
                        catch
                        {
                        }
                    }
                    if (property.Name.Equals("BuildNumber"))
                    {
                        try
                        {
                            OSVersion.Text = obj.Properties["BuildNumber"].Value.ToString();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            OSDir.Text = Environment.SystemDirectory + "";
            mc = new ManagementClass("Win32_Processor");
            moc = mc.GetInstances();
            pdc = mc.Properties;
            foreach (ManagementObject obj in moc)
            {
                foreach (PropertyData property in pdc)
                {
                    if (property.Name.Equals("Name"))
                    {
                        try
                        {
                            SYSProcessor.Text = obj.Properties["Name"].Value.ToString();
                            PerfCPUName.Text = SYSProcessor.Text;
                            label32.Text = (Double.Parse(obj.Properties["MaxClockSpeed"].Value.ToString())/1000.0) + " GHz";
                            label33.Text = "1";
                            label34.Text = obj.Properties["NumberOfCores"].Value.ToString();
                            label35.Text = obj.Properties["NumberOfLogicalProcessors"].Value.ToString();
                            label36.Text = obj.Properties["VirtualizationFirmwareEnabled"].Value.ToString();
                            label37.Text = obj.Properties["AddressWidth"].Value.ToString() + "bits";
                            label38.Text = obj.Properties["ExtClock"].Value.ToString()+"MHz";
                        }
                        catch
                        {
                        }
                    }
                }
            }
            mc = new ManagementClass("Win32_ComputerSystem");
            moc = mc.GetInstances();
            pdc = mc.Properties;
            foreach (ManagementObject obj in moc)
            {
                foreach (PropertyData property in pdc)
                {
                    //if (property.Name.Equals("TotalPhysicalMemory"))
                    {
                        try
                        {
                            ramMemory = Convert.ToInt64(obj.Properties["TotalPhysicalMemory"].Value.ToString());
                            double mem = ramMemory;
                            mem /= 1048576;
                            if (mem > 1024)
                            {
                                mem /= 1024;
                                SYSMemory.Text = Math.Round(mem)+" GB (installed "+Math.Round(mem, 3) + " GB)";
                            }
                            else
                            {
                                SYSMemory.Text = Math.Round(mem) + " MB (installed " + Math.Round(mem, 3) + " MB)";
                            }
                            if (SYSManufacturer.Text.Equals("<Manufacturer and model>"))
                            {
                                SYSManufacturer.Text = obj.Properties["SystemSKUNumber"].Value.ToString().Replace('_', ' ');
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (Environment.Is64BitOperatingSystem)
                SYSType.Text = "64-bit Operating System, x64-based Processor";
            else
                SYSType.Text = "32-bit Operating System, x86-based Processor";
            mc = new ManagementClass("Win32_VideoController");
            moc = mc.GetInstances();
            pdc = mc.Properties;
            foreach (ManagementObject obj in moc)
            {
                double mem = 0.0;
                if (!SYSGPU.Text.Equals(""))
                    SYSGPU.Text = SYSGPU.Text + " and ";
                foreach (PropertyData property in pdc)
                {

                    if (property.Name.Equals("Caption"))
                    {
                        try
                        {
                            SYSGPU.Text = SYSGPU.Text + obj.Properties["Caption"].Value.ToString() + " ";
                            if (mem >= 1024)
                            {
                                mem /= 1024;
                                SYSGPU.Text = SYSGPU.Text + Math.Round(mem, 3) + " GB";
                            }
                            else
                            {
                                SYSGPU.Text = SYSGPU.Text + Math.Round(mem, 3) + " MB";
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (property.Name.Equals("AdapterRAM"))
                    {
                        try
                        {
                            mem = Convert.ToDouble(obj.Properties["AdapterRAM"].Value.ToString());
                            mem /= 1048576;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            WSDNS.Text = System.Net.Dns.GetHostName();
            WSIP.Text = System.Net.Dns.GetHostEntry(WSDNS.Text).AddressList[0].MapToIPv4().ToString();
            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Ethernet) continue;
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    WSMAC.Text = nic.GetPhysicalAddress().ToString(); break;
                }
            }
            WSFullName.Text = WSDNS.Text + "\\" + WSName.Text;
            mc = new ManagementClass("Win32_PerfFormattedData_PerfOS_Memory");
            moc = mc.GetInstances();
            foreach (ManagementObject obj in moc)
            {
                label46.Text = Double.Parse(obj["CacheBytes"].ToString()) / 1048576 > 1024 ? Math.Round(Double.Parse(obj["CacheBytes"].ToString()) / 1073741824, 2) + " GB" : Math.Round(Double.Parse(obj["CacheBytes"].ToString()) / 1048576, 2) + " MB";
                label48.Text = Double.Parse(obj["CommittedBytes"].ToString()) / 1048576 > 1024 ? Math.Round(Double.Parse(obj["CommittedBytes"].ToString()) / 1073741824, 2) + " GB" : Math.Round(Double.Parse(obj["CommittedBytes"].ToString()) / 1048576, 2) + " MB";
                label44.Text = Double.Parse(obj["PoolPagedBytes"].ToString()) / 1048576 > 1024 ? Math.Round(Double.Parse(obj["PoolPagedBytes"].ToString()) / 1073741824, 2) + " GB" : Math.Round(Double.Parse(obj["PoolPagedBytes"].ToString()) / 1048576, 2) + " MB";
                label42.Text = Double.Parse(obj["PoolNonpagedBytes"].ToString()) / 1048576 > 1024 ? Math.Round(Double.Parse(obj["PoolNonpagedBytes"].ToString()) / 1073741824, 2) + " GB" : Math.Round(Double.Parse(obj["PoolNonpagedBytes"].ToString()) / 1048576, 2) + " MB";
                label63.Text = Double.Parse(obj["CommitLimit"].ToString()) / 1048576 > 1024 ? Math.Round(Double.Parse(obj["CommitLimit"].ToString()) / 1073741824, 2) + " GB" : Math.Round(Double.Parse(obj["CommitLimit"].ToString()) / 1048576, 2) + " MB";
            }
            mc = new ManagementClass("Win32_PhysicalMemoryArray");
            moc = mc.GetInstances();
            foreach (ManagementObject obj in moc)
            {
                label64.Text = " out of "+obj["MemoryDevices"];
            }
            mc = new ManagementClass("Win32_PhysicalMemory");
            moc = mc.GetInstances();
            label64.Text = moc.Count + label64.Text;
            foreach (ManagementObject obj in moc)
            {
                label62.Text = obj["Speed"] + " MHz";
                int i = Int32.Parse(obj["SMBIOSMemoryType"].ToString());
                switch(i){
                    
                    case 0: label58.Text="Unknown";break;
                    case 1: label58.Text="Other";break;
                    case 2: label58.Text="DRAM";break;
                    case 3: label58.Text="Synchronous DRAM";break;
                    case 4: label58.Text="Cache DRAM";break;
                    case 5: label58.Text="EDO";break;
                    case 6: label58.Text="EDRAM";break;
                    case 7: label58.Text="DRAM";break;
                    case 8: label58.Text="SRAM";break;
                    case 9: label58.Text="RAM";break;
                    case 10: label58.Text="ROM";break;
                    case 11: label58.Text="Flash";break;
                    case 12: label58.Text="EEPROM";break; 
                    case 13: label58.Text="FEPROM";break;
                    case 14: label58.Text="EPROM";break; 
                    case 15: label58.Text="DRAM";break; 
                    case 16: label58.Text="3DRAM";break;
                    case 17: label58.Text="SDRAM";break;
                    case 18: label58.Text="SGRAM";break;
                    case 19: label58.Text="RDRAM";break;
                    case 20: label58.Text="DDR";break; 
                    case 21: label58.Text="DDR2";break;
                    case 22: label58.Text="DDR2 FB-DIMM";break;
                    case 24: label58.Text="DDR3";break;
                    case 25: label58.Text="FBD2";break;
                }
                label41.Text = SYSMemory.Text+ " " + label58.Text;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetComputerDetails();
            WSName.Text = Environment.UserDomainName + "";
            WSUser.Text = Environment.UserName + "";
            machineName = WSName.Text;
            //StartUp Apps
            GetStartUpApps();
            //Processes
            GetProcessListReady();
            //Services
            GetServicesReady();
            //For other parameters
            System.Threading.TimerCallback tc = new System.Threading.TimerCallback(PerformanceUpdater);
            t = new System.Threading.Timer(tc, null, 1000, 1000);
            GetChartsReady();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoreSystemDetails msd = new MoreSystemDetails();
            msd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count <= 0) return;
            try
            {
                int selectedpid = Convert.ToInt32(processAdvDetails.SelectedItems[0].SubItems[1].Text.ToString());
                if (MessageBox.Show("Are you sure you Want to Kill " + processAdvDetails.SelectedItems[0].SubItems[0].Text + "(" + selectedpid + ")?\n It could effect the system!", "Confirm Process Kill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                Process.GetProcessById(selectedpid).Kill();
                processAdvDetails.Items.Remove(processAdvDetails.SelectedItems[0]);
            }
            catch (Exception ex) { PrintToConsole(ex.StackTrace.ToString() + "\n"); }
        }

        private void endTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count <= 0) return;
            try
            {
                int selectedpid = Convert.ToInt32(processAdvDetails.SelectedItems[0].SubItems[1].Text.ToString());
                if (MessageBox.Show("Are you sure you Want to Kill " + processAdvDetails.SelectedItems[0].SubItems[0].Text + "(" + selectedpid + ")?\n It could effect the system!", "Confirm Process Kill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                Process.GetProcessById(selectedpid).Kill();
                processAdvDetails.Items.Remove(processAdvDetails.SelectedItems[0]);
            }
            catch (Exception ex) { PrintToConsole(ex.StackTrace.ToString() + "\n"); }
        }

        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count <= 0) return;
            string filepath = Process.GetProcessById(Convert.ToInt32(processAdvDetails.SelectedItems[0].SubItems[1].Text.ToString())).MainModule.FileName;
            Process.Start("explorer.exe", "/select, " + filepath);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void processAdvDetailsContextMenu_Opened(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count <= 0) return;
            string priority = Process.GetProcessById(Convert.ToInt32(processAdvDetails.SelectedItems[0].SubItems[1].Text.ToString())).PriorityClass.ToString().ToUpper();
            if (priority == "IDLE")
                priority = "LOW";
            foreach (ToolStripMenuItem mi in setPriorityToolStripMenuItem.DropDown.Items)
            {
                string mnutext = mi.Text.ToUpper().Replace(" ","");
					if(mnutext != priority)
						mi.Checked = false;
					else
					{
						mi.Checked = true;
					}
				}
        }

        private void changePriority(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count <= 0) return;
            if (MessageBox.Show("Are you sure you Want to change " + processAdvDetails.SelectedItems[0].SubItems[0].Text + " Priority?\n It could effect the system!", "Confirm Priority Change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            int selectedpid = Convert.ToInt32(processAdvDetails.SelectedItems[0].SubItems[1].Text.ToString());
            string priority = ((ToolStripMenuItem)sender).Text.ToUpper();
            try{
                Process selectedprocess = Process.GetProcessById(selectedpid);
                if(priority == "HIGH")
					selectedprocess.PriorityClass = ProcessPriorityClass.High;
				else if(priority == "LOW")
					selectedprocess.PriorityClass = ProcessPriorityClass.Idle;
				else if(priority == "REALTIME")
					selectedprocess.PriorityClass = ProcessPriorityClass.RealTime;
				else if(priority == "ABOVE NORMAL")
					selectedprocess.PriorityClass = ProcessPriorityClass.AboveNormal;
				else if(priority == "BELOW NORMAL")
					selectedprocess.PriorityClass = ProcessPriorityClass.BelowNormal;
				else if(priority == "NORMAL")
					selectedprocess.PriorityClass = ProcessPriorityClass.Normal;
				foreach(ToolStripMenuItem mnuitem in setPriorityToolStripMenuItem.DropDown.Items)
				{
					mnuitem.Checked = false;
				}
				((ToolStripMenuItem)sender).Checked = true;
            }
            catch (Exception ex) {
                if (ex.Message.Equals("Access is denied"))
                {
                    MessageBox.Show(ex.Message, "Unable to change Priority", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                PrintToConsole(ex.ToString()+"\n"+ex.Message+"\n"+ex.StackTrace.ToString() + "\n"); MessageBox.Show(ex.StackTrace.ToString()); 
            }
        }

        private void currentprocessColoring_Click(object sender, EventArgs e)
        {
            if (currentprocessColoring.Checked == false)
            {
                foreach(ListViewItem lvi in processAdvDetails.Items)
                    lvi.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void endTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int selectedpid;
            if (tabIndex == 4)
            {
                if (processAdvDetails.SelectedItems.Count <= 0) return;
                selectedpid = Convert.ToInt32(processAdvDetails.SelectedItems[0].SubItems[1].Text.ToString());
            }
            else if (tabIndex == 1)
            {
                if (processDetails.SelectedItems.Count <= 0) return;
                selectedpid = Convert.ToInt32(processDetails.SelectedItems[0].SubItems[1].Text.ToString());
            }
            else
                return;
            try
            {
                if (MessageBox.Show("Are you sure you Want to Kill " + processAdvDetails.SelectedItems[0].SubItems[0].Text + "(" + selectedpid + ")?\n It could effect the system!", "Confirm Process Kill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                Process.GetProcessById(selectedpid).Kill();
                processAdvDetails.Items.Remove(processAdvDetails.SelectedItems[0]);
            }
            catch (Exception ex) { PrintToConsole(ex.StackTrace.ToString() + "\n"); }
        }

        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewTaskForm objnewprocess = new NewTaskForm();
                objnewprocess.ShowDialog();
                if (newProcessPath.Length != 0)
                {
                    if (newProcessPath.IndexOf("\\") == -1)
                    {
                        string[] newprocdetails = newProcessPath.Split(' ');
                        if (newprocdetails.Length > 1)
                        {
                            Process newprocess = Process.Start(newprocdetails[0].ToString(), newprocdetails[1].ToString());
                        }
                        else
                        {
                            Process newprocess = Process.Start(newprocdetails[0].ToString());
                        }
                    }
                    else
                    {
                        string procname = newProcessPath.Substring(newProcessPath.LastIndexOf("\\") + 1);
                        string[] newprocdetails = procname.Split(' ');
                        if (newprocdetails.Length > 1)
                        {
                            Process newprocess = Process.Start(newProcessPath.Replace(newprocdetails[1].ToString(), ""), newprocdetails[1].ToString());
                        }
                        else
                        {
                            Process newprocess = Process.Start(newProcessPath);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Couldn't Create New Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole(ex.StackTrace.ToString() + "\n");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabIndex = tabControl1.SelectedIndex;
            //PrintToConsole("Tab changed " + tabControl1.SelectedIndex + " : " + tabIndex);
        }

        private void serviceContextMenu_Opened(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            goToProcessDetailsToolStripMenuItem.Enabled = !servListView.SelectedItems[0].SubItems[1].Text.Equals("");
            if (servListView.SelectedItems[0].SubItems[3].Text.Equals("Running"))
            {
                startToolStripMenuItem.Enabled = false;
                if (servListView.SelectedItems[0].SubItems[9].Text.Equals("Yes"))
                    stopToolStripMenuItem.Enabled = true;
                else
                    stopToolStripMenuItem.Enabled = false;
                restartToolStripMenuItem.Enabled = true;
                if (servListView.SelectedItems[0].SubItems[8].Text.Equals("Yes"))
                    pauseToolStripMenuItem.Enabled = true;
                else
                    pauseToolStripMenuItem.Enabled = false;
                resumeToolStripMenuItem.Enabled = false;
            }
            else if (servListView.SelectedItems[0].SubItems[3].Text.Equals("Paused"))
            {
                startToolStripMenuItem.Enabled = false;
                if (servListView.SelectedItems[0].SubItems[9].Text.Equals("Yes"))
                    stopToolStripMenuItem.Enabled = true;
                else
                    stopToolStripMenuItem.Enabled = false;
                restartToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = false;
                resumeToolStripMenuItem.Enabled = true;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
                restartToolStripMenuItem.Enabled = false;
                pauseToolStripMenuItem.Enabled = false;
                resumeToolStripMenuItem.Enabled = false;
            }
            
            if (servListView.SelectedItems[0].SubItems[4].Text.Equals("Automatic"))
            {
                automaticToolStripMenuItem1.Checked = true;
                manualToolStripMenuItem.Checked = false;
                disabledToolStripMenuItem.Checked = false;
            }
            else if (servListView.SelectedItems[0].SubItems[4].Text.Equals("Manual"))
            {
                automaticToolStripMenuItem1.Checked = false;
                manualToolStripMenuItem.Checked = true;
                disabledToolStripMenuItem.Checked = false;
            }
            else
            {
                automaticToolStripMenuItem1.Checked = false;
                manualToolStripMenuItem.Checked = false;
                disabledToolStripMenuItem.Checked = true;
            }
        }

        private void openServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("services.msc");
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            ServiceController service = new ServiceController();
            service.ServiceName = servListView.SelectedItems[0].SubItems[0].Text;
            try
            {
                service.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while starting Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole("Error while starting Service "+ex.Message + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            ServiceController service = new ServiceController();
            service.ServiceName = servListView.SelectedItems[0].SubItems[0].Text;
            try
            {
                service.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while stoping Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole("Error while stoping Service " + ex.Message + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine);
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            ServiceController service = new ServiceController();
            service.ServiceName = servListView.SelectedItems[0].SubItems[0].Text;
            try
            {
                service.Pause();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while pausing Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole("Error while pausing Service " + ex.Message + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine);
            }
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            ServiceController service = new ServiceController();
            service.ServiceName = servListView.SelectedItems[0].SubItems[0].Text;
            try
            {
                service.Continue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while resuming Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole("Error while resuming Service " + ex.Message  + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine);
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            ServiceController service = new ServiceController();
            service.ServiceName = servListView.SelectedItems[0].SubItems[0].Text;
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(2500);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while restarting Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrintToConsole("Error while restarting Service " + ex.Message + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine);
            }
        }

        private void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0) { return; }
            ServiceController service = new ServiceController(servListView.SelectedItems[0].SubItems[0].Text);
        }
        private void servListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvcs.ColumnToSort)
            {
                if (lvcs.OrderOfSort == SortOrder.Ascending)
                {
                    lvcs.OrderOfSort = SortOrder.Descending;
                }
                else
                {
                    lvcs.OrderOfSort = SortOrder.Ascending;
                }
            }
            else
            {
                lvcs.ColumnToSort = e.Column;
                lvcs.OrderOfSort = SortOrder.Ascending;
            }
            this.servListView.Sort();
            servListView.SetSortIcon(lvcs.ColumnToSort, lvcs.OrderOfSort);
        }

        private void processAdvDetails_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvcs2.ColumnToSort)
            {
                if (lvcs2.OrderOfSort == SortOrder.Ascending)
                {
                    lvcs2.OrderOfSort = SortOrder.Descending;
                }
                else
                {
                    lvcs2.OrderOfSort = SortOrder.Ascending;
                }
            }
            else
            {
                lvcs2.ColumnToSort = e.Column;
                if(lvcs2.ColumnToSort >= 3 && lvcs2.ColumnToSort<=7)
                    lvcs2.OrderOfSort = SortOrder.Descending;
                else
                    lvcs2.OrderOfSort = SortOrder.Ascending;
            }
            this.processAdvDetails.Sort();
            processAdvDetails.SetSortIcon(lvcs2.ColumnToSort, lvcs2.OrderOfSort);
        }

        private void goToProcessDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servListView.SelectedItems.Count <= 0 || goToProcessDetailsToolStripMenuItem.Enabled==false) { return; }
            string pid = servListView.SelectedItems[0].SubItems[1].Text;
            foreach (ListViewItem lvi in processAdvDetails.Items)
            {
                if (lvi.SubItems[1].Text.Equals(pid))
                {
                    tabControl1.SelectedIndex = 5;
                    processAdvDetails.Focus();
                    lvi.Selected = true;
                    processAdvDetails.Items[lvi.Index].EnsureVisible();
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (processDetails.SelectedItems.Count <= 0) return;
            try
            {
                int selectedpid = Convert.ToInt32(processDetails.SelectedItems[0].SubItems[1].Text.ToString());
                if (MessageBox.Show("Are you sure you Want to Kill " + processDetails.SelectedItems[0].SubItems[0].Text + "(" + selectedpid + ")?\n It could effect the system!", "Confirm Process Kill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                Process.GetProcessById(selectedpid).Kill();
                processDetails.Items.Remove(processAdvDetails.SelectedItems[0]);
            }
            catch (Exception ex) { PrintToConsole(ex.StackTrace.ToString() + "\n"); }
        }

        private void processDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (processDetails.SelectedItems.Count > 0)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        private void processAdvDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count > 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isDrawing = false;
            isUpdating = false;
        }
        //For ProcessList ContextMenu
        private void endTaskToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (processDetails.SelectedItems.Count <= 0) return;
            try
            {
                int selectedpid = Convert.ToInt32(processDetails.SelectedItems[0].SubItems[1].Text.ToString());
                if (MessageBox.Show("Are you sure you Want to Kill " + processDetails.SelectedItems[0].SubItems[0].Text + "(" + selectedpid + ")?\n It could effect the system!", "Confirm Process Kill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                Process.GetProcessById(selectedpid).Kill();
                processDetails.Items.Remove(processDetails.SelectedItems[0]);
            }
            catch (Exception ex) { PrintToConsole(ex.StackTrace.ToString() + "\n"); }
        }

        private void goToDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processDetails.SelectedItems.Count <= 0) { return; }
            string pid = processDetails.SelectedItems[0].SubItems[1].Text;
            foreach (ListViewItem lvi in processAdvDetails.Items)
            {
                if (lvi.SubItems[1].Text.Equals(pid))
                {
                    tabControl1.SelectedIndex = 5;
                    processAdvDetails.Focus();
                    lvi.Selected = true;
                    processAdvDetails.Items[lvi.Index].EnsureVisible();
                    return;
                }
            }
        }

        private void openFileLocationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (processDetails.SelectedItems.Count <= 0) return;
            string filepath = Process.GetProcessById(Convert.ToInt32(processDetails.SelectedItems[0].SubItems[1].Text.ToString())).MainModule.FileName;
            Process.Start("explorer.exe", "/select, " + filepath);
        }

        private void valuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            percentsToolStripMenuItem.Checked = false;
            format[0] = true;
        }

        private void percentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            valuesToolStripMenuItem.Checked = false;
            format[0] = false;
        }

        private void valuesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            percentsToolStripMenuItem1.Checked = false;
            format[1] = true;
        }

        private void percentsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            valuesToolStripMenuItem1.Checked = false;
            format[1] = false;
        }

        private void valuesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            percentsToolStripMenuItem2.Checked = false;
            format[2] = true;
        }

        private void percentsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            valuesToolStripMenuItem2.Checked = false;
            format[2] = false;
        }

        private void processDetails_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvcs3.ColumnToSort)
            {
                if (lvcs3.OrderOfSort == SortOrder.Ascending)
                {
                    lvcs3.OrderOfSort = SortOrder.Descending;
                }
                else
                {
                    lvcs3.OrderOfSort = SortOrder.Ascending;
                }
            }
            else
            {
                lvcs3.ColumnToSort = e.Column;
                if (lvcs3.ColumnToSort >= 3 && lvcs3.ColumnToSort <= 7)
                    lvcs3.OrderOfSort = SortOrder.Descending;
                else
                    lvcs3.OrderOfSort = SortOrder.Ascending;
            }
            this.processDetails.Sort();
            processDetails.SetSortIcon(lvcs3.ColumnToSort, lvcs3.OrderOfSort);
        }

        private void CPUPerformanceButton_Click(object sender, EventArgs e)
        {
            multiView1.SelectedIndex = 0;
            CPUPerformanceButton.Selected = true;
            MemoryPerformanceButton.Selected = false;
            DiskPerformanceButton.Selected = false;
        }

        private void MemoryPerformanceButton_Click(object sender, EventArgs e)
        {
            multiView1.SelectedIndex = 1;
            CPUPerformanceButton.Selected = false;
            MemoryPerformanceButton.Selected = true;
            DiskPerformanceButton.Selected = false;
        }

        private void DiskPerformanceButton_Click(object sender, EventArgs e)
        {
            multiView1.SelectedIndex = 2;
            CPUPerformanceButton.Selected = false;
            MemoryPerformanceButton.Selected = false;
            DiskPerformanceButton.Selected = true;
        }

        private void veryHighToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshrate = 150;
            highToolStripMenuItem1.Checked = false;
            normalToolStripMenuItem1.Checked = false;
            lowToolStripMenuItem1.Checked = false;
        }

        private void highToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            refreshrate = 350;
            veryHighToolStripMenuItem.Checked = false;
            normalToolStripMenuItem1.Checked = false;
            lowToolStripMenuItem1.Checked = false;
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            refreshrate = 1000;
            veryHighToolStripMenuItem.Checked = false;
            highToolStripMenuItem1.Checked = false;
            lowToolStripMenuItem1.Checked = false;
        }

        private void lowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            refreshrate = 2000;
            veryHighToolStripMenuItem.Checked = false;
            highToolStripMenuItem1.Checked = false;
            normalToolStripMenuItem1.Checked = false;
        }

        private void monitorProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processAdvDetails.SelectedItems.Count <= 0) { return; }
            int pid = Int32.Parse(processAdvDetails.SelectedItems[0].SubItems[1].Text);
            ProcessMonitor pm = new ProcessMonitor(pid,ramMemory);
            pm.Show();
        }

        private void resourceMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("perfmon.exe");
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("services.msc");
        }
    }
}
