namespace MainForm
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnSetScale = new System.Windows.Forms.Button();
            this.viewScaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblViewScale = new System.Windows.Forms.Label();
            this.lblCurScaleValue = new System.Windows.Forms.Label();
            this.lblCurScale = new System.Windows.Forms.Label();
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewScaleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.showToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1347, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItemClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainContainer.Location = new System.Drawing.Point(0, 28);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mainContainer.Name = "mainContainer";
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.AutoScroll = true;
            this.mainContainer.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mainContainer.Panel1.Controls.Add(this.btnProcess);
            this.mainContainer.Panel1.Controls.Add(this.btnSetScale);
            this.mainContainer.Panel1.Controls.Add(this.viewScaleUpDown);
            this.mainContainer.Panel1.Controls.Add(this.lblViewScale);
            this.mainContainer.Panel1.Controls.Add(this.lblCurScaleValue);
            this.mainContainer.Panel1.Controls.Add(this.lblCurScale);
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.Controls.Add(this.mapPictureBox);
            this.mainContainer.Size = new System.Drawing.Size(1347, 710);
            this.mainContainer.SplitterDistance = 251;
            this.mainContainer.TabIndex = 1;
            // 
            // btnProcess
            // 
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(0, 380);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(252, 42);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.BtnProcessClick);
            // 
            // btnSetScale
            // 
            this.btnSetScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetScale.Location = new System.Drawing.Point(183, 28);
            this.btnSetScale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetScale.Name = "btnSetScale";
            this.btnSetScale.Size = new System.Drawing.Size(66, 27);
            this.btnSetScale.TabIndex = 4;
            this.btnSetScale.Text = "Set";
            this.btnSetScale.UseVisualStyleBackColor = true;
            this.btnSetScale.Click += new System.EventHandler(this.BtnSetScaleClick);
            // 
            // viewScaleUpDown
            // 
            this.viewScaleUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewScaleUpDown.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.viewScaleUpDown.Location = new System.Drawing.Point(98, 28);
            this.viewScaleUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.viewScaleUpDown.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.viewScaleUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.viewScaleUpDown.Name = "viewScaleUpDown";
            this.viewScaleUpDown.Size = new System.Drawing.Size(81, 27);
            this.viewScaleUpDown.TabIndex = 3;
            this.viewScaleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.viewScaleUpDown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.viewScaleUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblViewScale
            // 
            this.lblViewScale.AutoSize = true;
            this.lblViewScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewScale.Location = new System.Drawing.Point(3, 33);
            this.lblViewScale.Name = "lblViewScale";
            this.lblViewScale.Size = new System.Drawing.Size(95, 20);
            this.lblViewScale.TabIndex = 2;
            this.lblViewScale.Text = "View scale:";
            // 
            // lblCurScaleValue
            // 
            this.lblCurScaleValue.AutoSize = true;
            this.lblCurScaleValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurScaleValue.Location = new System.Drawing.Point(94, 2);
            this.lblCurScaleValue.Name = "lblCurScaleValue";
            this.lblCurScaleValue.Size = new System.Drawing.Size(19, 20);
            this.lblCurScaleValue.TabIndex = 1;
            this.lblCurScaleValue.Text = "0";
            // 
            // lblCurScale
            // 
            this.lblCurScale.AutoSize = true;
            this.lblCurScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurScale.Location = new System.Drawing.Point(3, 2);
            this.lblCurScale.Name = "lblCurScale";
            this.lblCurScale.Size = new System.Drawing.Size(85, 20);
            this.lblCurScale.TabIndex = 0;
            this.lblCurScale.Text = "Cur.scale:";
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mapPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(1092, 710);
            this.mapPictureBox.TabIndex = 0;
            this.mapPictureBox.TabStop = false;
            this.mapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.MapPictureBoxPaint);
            this.mapPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MapPictureBoxMouseDoubleClick);
            this.mapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapPictureBoxMouseDown);
            this.mapPictureBox.MouseEnter += new System.EventHandler(this.MapPictureBoxMouseEnter);
            this.mapPictureBox.MouseLeave += new System.EventHandler(this.MapPictureBoxMouseLeave);
            this.mapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapPictureBoxMouseMove);
            this.mapPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapPictureBoxMouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 738);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "AlgorithmsComparision";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel1.PerformLayout();
            this.mainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewScaleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainContainer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox mapPictureBox;
        private System.Windows.Forms.Button btnSetScale;
        private System.Windows.Forms.NumericUpDown viewScaleUpDown;
        private System.Windows.Forms.Label lblViewScale;
        private System.Windows.Forms.Label lblCurScaleValue;
        private System.Windows.Forms.Label lblCurScale;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    }
}

