namespace MainForm.Controls
{
    partial class AlgParamControl
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblOutScale = new System.Windows.Forms.Label();
            this.scaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.paramUpDown = new System.Windows.Forms.NumericUpDown();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkRun = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.detailUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scaleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paramUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(43, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // lblOutScale
            // 
            this.lblOutScale.AutoSize = true;
            this.lblOutScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutScale.Location = new System.Drawing.Point(0, 24);
            this.lblOutScale.Name = "lblOutScale";
            this.lblOutScale.Size = new System.Drawing.Size(79, 20);
            this.lblOutScale.TabIndex = 1;
            this.lblOutScale.Text = "Cell size:";
            // 
            // scaleUpDown
            // 
            this.scaleUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleUpDown.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.scaleUpDown.Location = new System.Drawing.Point(108, 23);
            this.scaleUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.scaleUpDown.Name = "scaleUpDown";
            this.scaleUpDown.Size = new System.Drawing.Size(91, 24);
            this.scaleUpDown.TabIndex = 2;
            this.scaleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.scaleUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTolerance.Location = new System.Drawing.Point(3, 53);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(94, 20);
            this.lblTolerance.TabIndex = 3;
            this.lblTolerance.Text = "Alg. param:";
            // 
            // paramUpDown
            // 
            this.paramUpDown.DecimalPlaces = 2;
            this.paramUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.paramUpDown.Location = new System.Drawing.Point(108, 53);
            this.paramUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.paramUpDown.Name = "paramUpDown";
            this.paramUpDown.Size = new System.Drawing.Size(89, 24);
            this.paramUpDown.TabIndex = 4;
            this.paramUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.paramUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(220, 0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(29, 23);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "C";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.BtnCopyClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "m/mm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(200, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "mm";
            // 
            // checkRun
            // 
            this.checkRun.AutoSize = true;
            this.checkRun.Checked = true;
            this.checkRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRun.Location = new System.Drawing.Point(6, 4);
            this.checkRun.Name = "checkRun";
            this.checkRun.Size = new System.Drawing.Size(18, 17);
            this.checkRun.TabIndex = 11;
            this.checkRun.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(199, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "mm";
            // 
            // detailUpDown
            // 
            this.detailUpDown.DecimalPlaces = 1;
            this.detailUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.detailUpDown.Location = new System.Drawing.Point(108, 83);
            this.detailUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.detailUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.detailUpDown.Name = "detailUpDown";
            this.detailUpDown.Size = new System.Drawing.Size(87, 24);
            this.detailUpDown.TabIndex = 7;
            this.detailUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.detailUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "MinDetail:";
            // 
            // AlgParamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkRun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.detailUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.paramUpDown);
            this.Controls.Add(this.lblTolerance);
            this.Controls.Add(this.scaleUpDown);
            this.Controls.Add(this.lblOutScale);
            this.Controls.Add(this.lblName);
            this.Name = "AlgParamControl";
            this.Size = new System.Drawing.Size(246, 116);
            ((System.ComponentModel.ISupportInitialize)(this.scaleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paramUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblOutScale;
        private System.Windows.Forms.NumericUpDown scaleUpDown;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.NumericUpDown paramUpDown;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkRun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown detailUpDown;
        private System.Windows.Forms.Label label4;
    }
}
