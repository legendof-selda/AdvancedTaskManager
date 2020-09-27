using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskManager
{
    class ProcessInfo
    {
        public string ProcessName
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int PID
        {
            get;
            set;
        }
        public DateTime StartTime
        {
            get;
            set;
        }
        public TimeSpan TotalProcessorTime
        {
            get;
            set;
        }
        public long WorkingSet64
        {
            get;
            set;
        }
        public long PeakWorkingSet64
        {
            get;
            set;
        }
        public int HandleCount
        {
            get;
            set;
        }
        public int ThreadCount
        {
            get;
            set;
        }
        public string status{
            get;
            set;
        }
        public string user
        {
            get;
            set;
        }
        public string FileDescription
        {
            get;
            set;
        }
        public TimeSpan OldTotalProcessorTime
        {
            get;
            set;
        }
        public bool[] updates = new bool[] { false, false, false, false, false, false, false };
        public bool HasBeenUpdated
        {
            get
            {
                foreach(bool u in updates){
                    if(u) return true;
                }
                return false;
            }
        }
        public ProcessInfo(System.Diagnostics.Process p)
        {
            ProcessName = p.ProcessName;
            PID = p.Id;
            //Update Array Starts here
            StartTime = p.StartTime;
            TotalProcessorTime = p.TotalProcessorTime;
            WorkingSet64 = p.WorkingSet64;
            PeakWorkingSet64 = p.PeakWorkingSet64;
            HandleCount = p.HandleCount;
            ThreadCount = p.Threads.Count;
            status = (p.Responding ? "Running" : "Suspended");
            //Update Array Ends Here
            this.user = GetUser(PID);
            FileDescription = p.MainModule.FileVersionInfo.FileDescription;
            if (FileDescription.Equals(""))
                Name = p.MainModule.FileVersionInfo.FileName;
            else
                Name = FileDescription;
            if (Name.Equals(""))
                Name = ProcessName;
            OldTotalProcessorTime = TotalProcessorTime;
        }
        public string GetUser(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query);
            System.Management.ManagementObjectCollection plist = searcher.Get();

            foreach (System.Management.ManagementObject obj in plist)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    return argList[1] + "\\" + argList[0];
                }
            }

            return "NO OWNER";
        }
        public string DiskUsage(int processId, bool percent)
        {
            //string query = "Select IODataOperationsPersec From Win32_PerfFormattedData_PerfProc_Process Where IDProcess = " + processId;
            //System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query);
            //System.Management.ManagementObjectCollection plist = searcher.Get();
            //double d=0.0;
            //foreach (System.Management.ManagementObject obj in plist)
            //{
            //    d = Double.Parse(obj["IODataOperationsPersec"].ToString());
            //}
            if (percent)
            {
                //query = "Select IODataOperationsPersec From Win32_PerfFormattedData_PerfProc_Process Where IDProcess = 0";
                //searcher = new System.Management.ManagementObjectSearcher(query);
                //plist = searcher.Get();
                //foreach (System.Management.ManagementObject obj in plist)
                //{
                //    double d1 = Double.Parse(obj["IODataOperationsPersec"].ToString());
                //    d /= d1;
                //    if (Double.IsNaN(d))
                //        return 0 + "%";
                //    return Math.Round(d, 2) + "%";
                //}
                return "0%";
            }
            //d /= 1048576.0;
            //if (Double.IsNaN(d))
            //    return 0 + " MB/s";
            //return Math.Round(d, 2) + " MB/s";
            return "0 MB/s";
        }

        public void Update(System.Diagnostics.Process p)
        {
            ConfirmUpdate();
            if (StartTime!=p.StartTime)
            {
                StartTime = p.StartTime; updates[0] = true;
            }
            if (TotalProcessorTime != p.TotalProcessorTime)
            {
                OldTotalProcessorTime = TotalProcessorTime;
                TotalProcessorTime = p.TotalProcessorTime; updates[1] = true;
            }
            if (WorkingSet64 != p.WorkingSet64)
            {
                WorkingSet64 = p.WorkingSet64; updates[2] = true;
            }
            if (PeakWorkingSet64 != p.PeakWorkingSet64)
            {
                PeakWorkingSet64 = p.PeakWorkingSet64; updates[3] = true;
            }
            if (HandleCount != p.HandleCount)
            {
                HandleCount = p.HandleCount; updates[4] = true;
            }
            if (ThreadCount != p.Threads.Count)
            {
                ThreadCount = p.Threads.Count; updates[5] = true;
            }
            string s = (p.Responding ? "Running" : "Suspended");
            if (!status.Equals(s))
            {
                status = s; updates[6] = true;
            }
        }
        public void ConfirmUpdate()
        {
            updates = new bool[] { false, false, false, false, false, false, false };
        }
        public string[] ForProcessAdvList()
        {
            return new string[]{ProcessName, PID.ToString(), StartTime.ToShortTimeString(),
                    TotalProcessorTime.Duration().Hours.ToString()+":"+
                    TotalProcessorTime.Duration().Minutes.ToString()+":"+
                    TotalProcessorTime.Duration().Seconds.ToString()+"."+
                    TotalProcessorTime.Duration().Milliseconds.ToString()
                    , (WorkingSet64/1024)+" K", (PeakWorkingSet64/1024)+" K", HandleCount.ToString(), ThreadCount.ToString(), status, user, FileDescription};
        }
        private double ToMB(long memory)
        {
            return Math.Round(memory / 1048576.0 ,2);
        }
        private double ToKB(long memory)
        {
            return Math.Round(memory / 1024.0 ,2);
        }
        private string ToBestUnit(long memory)
        {
            if (memory < 1024)
                return memory + " B";
            else if (ToKB(memory) < 1024)
                return ToKB(memory) + " KB";
            else
                return ToMB(memory) + " MB";
        }
        private double ToMB(double memory)
        {
            return Math.Round(memory / 1048576.0, 2);
        }
        private double ToKB(double memory)
        {
            return Math.Round(memory / 1024.0);
        }
        private string ToBestUnit(double memory)
        {
            if (memory < 1024)
                return memory + " B";
            else if (ToKB(memory) < 1024)
                return ToKB(memory) + " KB";
            else
                return ToMB(memory) + " MB";
        }
        public string[] ForProcessDetails(double TotalCpuUsage, long ramMemory, bool[] format)
        {
            double cpu;
            if (TotalCpuUsage == 0)
                cpu = 0.0;
            else
                cpu = ((TotalProcessorTime - OldTotalProcessorTime).TotalSeconds / TotalCpuUsage) * 100.0;

            string mem;

            if(format[0])
                mem = ToMB(WorkingSet64) + " MB";
            else
                mem = Math.Round(((double)WorkingSet64 / (double)ramMemory) * 100.0, 2) + "%";
            return new string[]{Name,PID.ToString(), Math.Round(cpu, 2)+"%", mem, DiskUsage(PID,format[1])};
        }
        public double[] ForUsers(double TotalCpuUsage, long ramMemory)
        {
            double cpu;
            if (TotalCpuUsage == 0)
                cpu = 0.0;
            else
                cpu = ((TotalProcessorTime - OldTotalProcessorTime).TotalSeconds / TotalCpuUsage) * 100.0;
            return new double[] { Math.Round(cpu, 2), ToMB(WorkingSet64)};
        }
    }
}
