using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AdvancedTaskManager
{
    class DiskView
    {
        string drive;

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        public System.Windows.Forms.Label label70;
        public System.Windows.Forms.Label label71;
        public System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Label DiskWriteSpeedLabel;
        public System.Windows.Forms.Label label77;
        public System.Windows.Forms.Label DiskReadSpeedLabel;
        public System.Windows.Forms.Label label79;
        public System.Windows.Forms.Label label80;
        public System.Windows.Forms.Label DiskActiveLabel;
        public System.Windows.Forms.Label label83;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        public System.Windows.Forms.Label label87;
        public System.Windows.Forms.Label label89;
        public System.Windows.Forms.Label label90;
        public System.Windows.Forms.Label label91;
        public System.Windows.Forms.Label DiskTotalLabel;
        public System.Windows.Forms.Label DiskFreeLabel;
        public System.Windows.Forms.Label DiskHeadsLabel;
        public System.Windows.Forms.Label DiskSectorsLabel;
        public System.Windows.Forms.Label DiskTracksLabel;
        public System.Windows.Forms.Label DiskUsedLabel;
        public System.Windows.Forms.Label DiskCylindersLabel;
        public System.Windows.Forms.Label label72;
        public System.Windows.Forms.Label label86;
        public System.Windows.Forms.Label label88;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        public System.Windows.Forms.DataVisualization.Charting.Chart diskReadWriteChart;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        public System.Windows.Forms.DataVisualization.Charting.Chart diskUsageChart;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        public PerformanceButton pb;
        public DiskView(string drive)
        {
            this.drive = drive;

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint17 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint18 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint19 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint20 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint21 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint22 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint23 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint24 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint25 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint26 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint27 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint28 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint29 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint30 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint31 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint32 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint33 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint34 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint35 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint36 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint37 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint38 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint39 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint40 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint41 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint42 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint43 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint44 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint45 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint46 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint47 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint48 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint49 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint50 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint51 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint52 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint53 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint54 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint55 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint56 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint57 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint58 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint59 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint60 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint61 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint62 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint63 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint64 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint65 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint66 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint67 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint68 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint69 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint70 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint71 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint72 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint73 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint74 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint75 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint76 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint77 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint78 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint79 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint80 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint81 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint82 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint83 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint84 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint85 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint86 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint87 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint88 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint89 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint90 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint91 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint92 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint93 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint156 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 100D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint157 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 100D);
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.DiskWriteSpeedLabel = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.DiskReadSpeedLabel = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.DiskActiveLabel = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label87 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.DiskTotalLabel = new System.Windows.Forms.Label();
            this.DiskFreeLabel = new System.Windows.Forms.Label();
            this.DiskHeadsLabel = new System.Windows.Forms.Label();
            this.DiskSectorsLabel = new System.Windows.Forms.Label();
            this.DiskTracksLabel = new System.Windows.Forms.Label();
            this.DiskUsedLabel = new System.Windows.Forms.Label();
            this.DiskCylindersLabel = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.diskReadWriteChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.diskUsageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel12.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diskReadWriteChart)).BeginInit();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diskUsageChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.label70, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.label71, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.panel7, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel14, 0, 1);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.925408F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.23776F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.06993F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(284, 261);
            this.tableLayoutPanel12.TabIndex = 3;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(3, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(136, 20);
            this.label70.TabIndex = 1;
            this.label70.Text = "Disk"+drive;
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label71.Location = new System.Drawing.Point(145, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(136, 20);
            this.label71.TabIndex = 2;
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.DiskWriteSpeedLabel);
            this.panel7.Controls.Add(this.label77);
            this.panel7.Controls.Add(this.DiskReadSpeedLabel);
            this.panel7.Controls.Add(this.label79);
            this.panel7.Controls.Add(this.label80);
            this.panel7.Controls.Add(this.DiskActiveLabel);
            this.panel7.Controls.Add(this.label83);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 182);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(142, 79);
            this.panel7.TabIndex = 4;
            // 
            // DiskWriteSpeedLabel
            // 
            this.DiskWriteSpeedLabel.AutoSize = true;
            this.DiskWriteSpeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskWriteSpeedLabel.Location = new System.Drawing.Point(82, 50);
            this.DiskWriteSpeedLabel.Name = "DiskWriteSpeedLabel";
            this.DiskWriteSpeedLabel.Size = new System.Drawing.Size(0, 25);
            this.DiskWriteSpeedLabel.TabIndex = 10;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label77.Location = new System.Drawing.Point(79, 37);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(66, 13);
            this.label77.TabIndex = 9;
            this.label77.Text = "Write Speed";
            // 
            // DiskReadSpeedLabel
            // 
            this.DiskReadSpeedLabel.AutoSize = true;
            this.DiskReadSpeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskReadSpeedLabel.Location = new System.Drawing.Point(3, 50);
            this.DiskReadSpeedLabel.Name = "DiskReadSpeedLabel";
            this.DiskReadSpeedLabel.Size = new System.Drawing.Size(0, 25);
            this.DiskReadSpeedLabel.TabIndex = 8;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label79.Location = new System.Drawing.Point(0, 37);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(67, 13);
            this.label79.TabIndex = 7;
            this.label79.Text = "Read Speed";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(82, 13);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(0, 25);
            this.label80.TabIndex = 6;
            // 
            // DiskActiveLabel
            // 
            this.DiskActiveLabel.AutoSize = true;
            this.DiskActiveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskActiveLabel.Location = new System.Drawing.Point(3, 13);
            this.DiskActiveLabel.Name = "DiskActiveLabel";
            this.DiskActiveLabel.Size = new System.Drawing.Size(0, 25);
            this.DiskActiveLabel.TabIndex = 4;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label83.Location = new System.Drawing.Point(0, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(63, 13);
            this.label83.TabIndex = 3;
            this.label83.Text = "Active Time";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Controls.Add(this.label87, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.label89, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.label90, 0, 4);
            this.tableLayoutPanel13.Controls.Add(this.label91, 0, 5);
            this.tableLayoutPanel13.Controls.Add(this.DiskTotalLabel, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.DiskFreeLabel, 1, 1);
            this.tableLayoutPanel13.Controls.Add(this.DiskHeadsLabel, 1, 4);
            this.tableLayoutPanel13.Controls.Add(this.DiskSectorsLabel, 1, 5);
            this.tableLayoutPanel13.Controls.Add(this.DiskTracksLabel, 1, 6);
            this.tableLayoutPanel13.Controls.Add(this.DiskUsedLabel, 1, 2);
            this.tableLayoutPanel13.Controls.Add(this.DiskCylindersLabel, 1, 3);
            this.tableLayoutPanel13.Controls.Add(this.label72, 0, 6);
            this.tableLayoutPanel13.Controls.Add(this.label86, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.label88, 0, 2);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(142, 182);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 8;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(142, 79);
            this.tableLayoutPanel13.TabIndex = 5;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label87.Location = new System.Drawing.Point(3, 18);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(65, 9);
            this.label87.TabIndex = 6;
            this.label87.Text = "Free Space:";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label89.Location = new System.Drawing.Point(3, 36);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(52, 9);
            this.label89.TabIndex = 8;
            this.label89.Text = "Cylinders:";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label90.Location = new System.Drawing.Point(3, 45);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(41, 9);
            this.label90.TabIndex = 10;
            this.label90.Text = "Heads:";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label91.Location = new System.Drawing.Point(3, 54);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(46, 9);
            this.label91.TabIndex = 11;
            this.label91.Text = "Sectors:";
            // 
            // DiskTotalLabel
            // 
            this.DiskTotalLabel.AutoSize = true;
            this.DiskTotalLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskTotalLabel.Location = new System.Drawing.Point(74, 9);
            this.DiskTotalLabel.Name = "DiskTotalLabel";
            this.DiskTotalLabel.Size = new System.Drawing.Size(0, 9);
            this.DiskTotalLabel.TabIndex = 14;
            // 
            // DiskFreeLabel
            // 
            this.DiskFreeLabel.AutoSize = true;
            this.DiskFreeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskFreeLabel.Location = new System.Drawing.Point(74, 18);
            this.DiskFreeLabel.Name = "DiskFreeLabel";
            this.DiskFreeLabel.Size = new System.Drawing.Size(0, 9);
            this.DiskFreeLabel.TabIndex = 15;
            // 
            // DiskHeadsLabel
            // 
            this.DiskHeadsLabel.AutoSize = true;
            this.DiskHeadsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskHeadsLabel.Location = new System.Drawing.Point(74, 45);
            this.DiskHeadsLabel.Name = "DiskHeadsLabel";
            this.DiskHeadsLabel.Size = new System.Drawing.Size(0, 9);
            this.DiskHeadsLabel.TabIndex = 18;
            // 
            // DiskSectorsLabel
            // 
            this.DiskSectorsLabel.AutoSize = true;
            this.DiskSectorsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskSectorsLabel.Location = new System.Drawing.Point(74, 54);
            this.DiskSectorsLabel.Name = "DiskSectorsLabel";
            this.DiskSectorsLabel.Size = new System.Drawing.Size(0, 9);
            this.DiskSectorsLabel.TabIndex = 19;
            // 
            // DiskTracksLabel
            // 
            this.DiskTracksLabel.AutoSize = true;
            this.DiskTracksLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskTracksLabel.Location = new System.Drawing.Point(74, 63);
            this.DiskTracksLabel.Name = "DiskTracksLabel";
            this.DiskTracksLabel.Size = new System.Drawing.Size(0, 13);
            this.DiskTracksLabel.TabIndex = 20;
            // 
            // DiskUsedLabel
            // 
            this.DiskUsedLabel.AutoSize = true;
            this.DiskUsedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskUsedLabel.Location = new System.Drawing.Point(74, 27);
            this.DiskUsedLabel.Name = "DiskUsedLabel";
            this.DiskUsedLabel.Size = new System.Drawing.Size(0, 9);
            this.DiskUsedLabel.TabIndex = 16;
            // 
            // DiskCylindersLabel
            // 
            this.DiskCylindersLabel.AutoSize = true;
            this.DiskCylindersLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiskCylindersLabel.Location = new System.Drawing.Point(74, 36);
            this.DiskCylindersLabel.Name = "DiskCylindersLabel";
            this.DiskCylindersLabel.Size = new System.Drawing.Size(0, 9);
            this.DiskCylindersLabel.TabIndex = 17;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label72.Location = new System.Drawing.Point(3, 63);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(43, 13);
            this.label72.TabIndex = 21;
            this.label72.Text = "Tracks:";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label86.Location = new System.Drawing.Point(3, 9);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(41, 9);
            this.label86.TabIndex = 5;
            this.label86.Text = "Total Space:";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label88.Location = new System.Drawing.Point(3, 27);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(41, 9);
            this.label88.TabIndex = 7;
            this.label88.Text = "Used Space:";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel12.SetColumnSpan(this.tableLayoutPanel14, 2);
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel14.Controls.Add(this.diskReadWriteChart, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 20);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 3;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.66412F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.33588F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(284, 162);
            this.tableLayoutPanel14.TabIndex = 6;
            // 
            // diskReadWriteChart
            // 
            this.diskReadWriteChart.BorderlineColor = System.Drawing.Color.RoyalBlue;
            this.diskReadWriteChart.BorderlineWidth = 2;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Violet;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Interval = 0D;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MajorGrid.Interval = 0D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Violet;
            chartArea1.AxisY.MajorTickMark.Interval = 0D;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.diskReadWriteChart.ChartAreas.Add(chartArea1);
            this.tableLayoutPanel14.SetColumnSpan(this.diskReadWriteChart, 2);
            this.diskReadWriteChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.diskReadWriteChart.Legends.Add(legend1);
            this.diskReadWriteChart.Location = new System.Drawing.Point(0, 104);
            this.diskReadWriteChart.Margin = new System.Windows.Forms.Padding(0);
            this.diskReadWriteChart.Name = "diskReadWriteChart";
            this.diskReadWriteChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.BorderColor = System.Drawing.Color.MediumOrchid;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Color = System.Drawing.Color.LightPink;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Read";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.Points.Add(dataPoint7);
            series1.Points.Add(dataPoint8);
            series1.Points.Add(dataPoint9);
            series1.Points.Add(dataPoint10);
            series1.Points.Add(dataPoint11);
            series1.Points.Add(dataPoint12);
            series1.Points.Add(dataPoint13);
            series1.Points.Add(dataPoint14);
            series1.Points.Add(dataPoint15);
            series1.Points.Add(dataPoint16);
            series1.Points.Add(dataPoint17);
            series1.Points.Add(dataPoint18);
            series1.Points.Add(dataPoint19);
            series1.Points.Add(dataPoint20);
            series1.Points.Add(dataPoint21);
            series1.Points.Add(dataPoint22);
            series1.Points.Add(dataPoint23);
            series1.Points.Add(dataPoint24);
            series1.Points.Add(dataPoint25);
            series1.Points.Add(dataPoint26);
            series1.Points.Add(dataPoint27);
            series1.Points.Add(dataPoint28);
            series1.Points.Add(dataPoint29);
            series1.Points.Add(dataPoint30);
            series1.Points.Add(dataPoint31);
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderColor = System.Drawing.Color.MediumOrchid;
            series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.Purple;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Write";
            series2.Points.Add(dataPoint32);
            series2.Points.Add(dataPoint33);
            series2.Points.Add(dataPoint34);
            series2.Points.Add(dataPoint35);
            series2.Points.Add(dataPoint36);
            series2.Points.Add(dataPoint37);
            series2.Points.Add(dataPoint38);
            series2.Points.Add(dataPoint39);
            series2.Points.Add(dataPoint40);
            series2.Points.Add(dataPoint41);
            series2.Points.Add(dataPoint42);
            series2.Points.Add(dataPoint43);
            series2.Points.Add(dataPoint44);
            series2.Points.Add(dataPoint45);
            series2.Points.Add(dataPoint46);
            series2.Points.Add(dataPoint47);
            series2.Points.Add(dataPoint48);
            series2.Points.Add(dataPoint49);
            series2.Points.Add(dataPoint50);
            series2.Points.Add(dataPoint51);
            series2.Points.Add(dataPoint52);
            series2.Points.Add(dataPoint53);
            series2.Points.Add(dataPoint54);
            series2.Points.Add(dataPoint55);
            series2.Points.Add(dataPoint56);
            series2.Points.Add(dataPoint57);
            series2.Points.Add(dataPoint58);
            series2.Points.Add(dataPoint59);
            series2.Points.Add(dataPoint60);
            series2.Points.Add(dataPoint61);
            series2.Points.Add(dataPoint62);
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.diskReadWriteChart.Series.Add(series1);
            this.diskReadWriteChart.Series.Add(series2);
            this.diskReadWriteChart.Size = new System.Drawing.Size(284, 37);
            this.diskReadWriteChart.TabIndex = 2;
            this.diskReadWriteChart.Text = "chart1";
            this.diskReadWriteChart.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.3913F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.6087F));
            this.tableLayoutPanel15.Controls.Add(this.diskUsageChart, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.chart2, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(284, 104);
            this.tableLayoutPanel15.TabIndex = 3;
            // 
            // diskUsageChart
            // 
            this.diskUsageChart.BorderlineColor = System.Drawing.Color.RoyalBlue;
            this.diskUsageChart.BorderlineWidth = 2;
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.IsMarginVisible = false;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea2.AxisX.MajorGrid.Interval = 0D;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Violet;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Interval = 0D;
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea2.AxisY.MajorGrid.Interval = 0D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Violet;
            chartArea2.AxisY.MajorTickMark.Interval = 0D;
            chartArea2.AxisY.Maximum = 100D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "ChartArea1";
            this.diskUsageChart.ChartAreas.Add(chartArea2);
            this.diskUsageChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.diskUsageChart.Legends.Add(legend2);
            this.diskUsageChart.Location = new System.Drawing.Point(0, 0);
            this.diskUsageChart.Margin = new System.Windows.Forms.Padding(0);
            this.diskUsageChart.Name = "diskUsageChart";
            this.diskUsageChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series3.BorderColor = System.Drawing.Color.MediumOrchid;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series3.Color = System.Drawing.Color.LightPink;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.Points.Add(dataPoint63);
            series3.Points.Add(dataPoint64);
            series3.Points.Add(dataPoint65);
            series3.Points.Add(dataPoint66);
            series3.Points.Add(dataPoint67);
            series3.Points.Add(dataPoint68);
            series3.Points.Add(dataPoint69);
            series3.Points.Add(dataPoint70);
            series3.Points.Add(dataPoint71);
            series3.Points.Add(dataPoint72);
            series3.Points.Add(dataPoint73);
            series3.Points.Add(dataPoint74);
            series3.Points.Add(dataPoint75);
            series3.Points.Add(dataPoint76);
            series3.Points.Add(dataPoint77);
            series3.Points.Add(dataPoint78);
            series3.Points.Add(dataPoint79);
            series3.Points.Add(dataPoint80);
            series3.Points.Add(dataPoint81);
            series3.Points.Add(dataPoint82);
            series3.Points.Add(dataPoint83);
            series3.Points.Add(dataPoint84);
            series3.Points.Add(dataPoint85);
            series3.Points.Add(dataPoint86);
            series3.Points.Add(dataPoint87);
            series3.Points.Add(dataPoint88);
            series3.Points.Add(dataPoint89);
            series3.Points.Add(dataPoint90);
            series3.Points.Add(dataPoint91);
            series3.Points.Add(dataPoint92);
            series3.Points.Add(dataPoint93);
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.diskUsageChart.Series.Add(series3);
            this.diskUsageChart.Size = new System.Drawing.Size(191, 104);
            this.diskUsageChart.TabIndex = 1;
            this.diskUsageChart.Text = "chart1";
            this.diskUsageChart.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // chart2
            // 
            chartArea6.AxisX.ToolTip = "Disk Memory Composition";
            chartArea6.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart2.Legends.Add(legend6);
            this.chart2.Location = new System.Drawing.Point(312, 3);
            this.chart2.Name = "chart2";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            dataPoint156.LegendText = "Free Space";
            dataPoint157.LegendText = "Used Space";
            series7.Points.Add(dataPoint156);
            series7.Points.Add(dataPoint157);
            this.chart2.Series.Add(series7);
            this.chart2.Size = new System.Drawing.Size(145, 172);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "chart2";
            // 
            // Form2
            // 
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diskReadWriteChart)).EndInit();
            this.tableLayoutPanel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diskUsageChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            GetInfo();
        }
        void GetInfo()
        {
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
            DiskTotalLabel.Text = Math.Round(size,2) + unit;

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
            DiskFreeLabel.Text = Math.Round(freespace,2) + unit;
            DiskUsedLabel.Text = Math.Round(size - freespace,2) + unit;
        }
        public TableLayoutPanel GetTheTable()
        {
            return tableLayoutPanel12;
        }
        public void AddToTimeChart(float value)
        {
            diskUsageChart.Series[0].Points.AddY(value);
            if (diskUsageChart.Series[0].Points.Count > 30)
                diskUsageChart.Series[0].Points.RemoveAt(0);
        }
        public void AddToReadWriteChart(float value1, float value2)
        {
            diskReadWriteChart.Series[0].Points.AddY(value1 * 100.0);
            diskReadWriteChart.Series[1].Points.AddY(value2 * 100.0);
            if (diskReadWriteChart.Series[0].Points.Count > 30)
                diskReadWriteChart.Series[0].Points.RemoveAt(0);
            if (diskReadWriteChart.Series[1].Points.Count > 30)
                diskReadWriteChart.Series[1].Points.RemoveAt(0);
        }
    }
}
