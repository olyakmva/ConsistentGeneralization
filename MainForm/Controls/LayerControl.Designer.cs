
namespace MainForm.Controls
{
    partial class LayerControl
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
            this.layerCheckBox = new System.Windows.Forms.CheckBox();
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // layerCheckBox
            // 
            this.layerCheckBox.AutoSize = true;
            this.layerCheckBox.Location = new System.Drawing.Point(4, 14);
            this.layerCheckBox.Name = "layerCheckBox";
            this.layerCheckBox.Size = new System.Drawing.Size(98, 21);
            this.layerCheckBox.TabIndex = 0;
            this.layerCheckBox.Text = "layerName";
            this.layerCheckBox.UseVisualStyleBackColor = true;
            this.layerCheckBox.CheckedChanged += new System.EventHandler(this.LayerCheckBoxCheckedChanged);
            // 
            // colorBox
            // 
            this.colorBox.Location = new System.Drawing.Point(141, 14);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(24, 21);
            this.colorBox.TabIndex = 1;
            this.colorBox.TabStop = false;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(171, 14);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(26, 17);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "N=";
            // 
            // LayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.layerCheckBox);
            this.Name = "LayerControl";
            this.Size = new System.Drawing.Size(241, 48);
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox layerCheckBox;
        private System.Windows.Forms.PictureBox colorBox;
        private System.Windows.Forms.Label lbl;
    }
}
