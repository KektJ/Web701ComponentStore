namespace ComponentStore
{
    partial class FrmNew
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
            this.NUMWarranty = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUMWarranty)).BeginInit();
            this.SuspendLayout();
            // 
            // NUMWarranty
            // 
            this.NUMWarranty.Location = new System.Drawing.Point(108, 213);
            this.NUMWarranty.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.NUMWarranty.Name = "NUMWarranty";
            this.NUMWarranty.Size = new System.Drawing.Size(120, 20);
            this.NUMWarranty.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Warranty (Months)";
            // 
            // FrmNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(325, 281);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.NUMWarranty);
            this.Name = "FrmNew";
            this.Text = "New Product";
            this.Controls.SetChildIndex(this.NUMWarranty, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            ((System.ComponentModel.ISupportInitialize)(this.NUMWarranty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NUMWarranty;
        private System.Windows.Forms.Label label7;
    }
}
