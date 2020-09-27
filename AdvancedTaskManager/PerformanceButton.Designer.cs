namespace AdvancedTaskManager
{
    partial class PerformanceButton
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel4 = new System.Windows.Forms.Panel();
            this.performanceStatus = new System.Windows.Forms.Label();
            this.performanceSampleGraph = new System.Windows.Forms.PictureBox();
            this.performanceTitle = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performanceSampleGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.performanceStatus);
            this.panel4.Controls.Add(this.performanceSampleGraph);
            this.panel4.Controls.Add(this.performanceTitle);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(178, 77);
            this.panel4.TabIndex = 2;
            this.panel4.MouseLeave += new System.EventHandler(this.panel4_MouseLeave);
            this.panel4.MouseHover += new System.EventHandler(this.panel4_MouseHover);
            // 
            // performanceStatus
            // 
            this.performanceStatus.AutoSize = true;
            this.performanceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.performanceStatus.Location = new System.Drawing.Point(87, 33);
            this.performanceStatus.Name = "performanceStatus";
            this.performanceStatus.Size = new System.Drawing.Size(0, 13);
            this.performanceStatus.TabIndex = 2;
            // 
            // performanceSampleGraph
            // 
            this.performanceSampleGraph.Location = new System.Drawing.Point(3, 14);
            this.performanceSampleGraph.Name = "performanceSampleGraph";
            this.performanceSampleGraph.Size = new System.Drawing.Size(73, 45);
            this.performanceSampleGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.performanceSampleGraph.TabIndex = 1;
            this.performanceSampleGraph.TabStop = false;
            // 
            // performanceTitle
            // 
            this.performanceTitle.AutoSize = true;
            this.performanceTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.performanceTitle.Location = new System.Drawing.Point(82, 14);
            this.performanceTitle.Name = "performanceTitle";
            this.performanceTitle.Size = new System.Drawing.Size(0, 18);
            this.performanceTitle.TabIndex = 1;
            // 
            // PerformanceButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel4);
            this.Name = "PerformanceButton";
            this.Size = new System.Drawing.Size(179, 78);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performanceSampleGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.PictureBox performanceSampleGraph;
        public System.Windows.Forms.Label performanceTitle;
        public System.Windows.Forms.Label performanceStatus;
    }
}
