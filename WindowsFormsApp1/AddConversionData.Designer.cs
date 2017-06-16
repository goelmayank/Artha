namespace WindowsFormsApp1
{
    partial class AddConversionData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddConversionData));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.English = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arabic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.German = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Italian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Japanese = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Korean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Norwegian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spanish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Swedish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.English,
            this.Arabic,
            this.German,
            this.Italian,
            this.Japanese,
            this.Korean,
            this.Norwegian,
            this.Spanish,
            this.Swedish});
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(943, 150);
            this.dataGridView1.TabIndex = 17;
            // 
            // English
            // 
            this.English.HeaderText = "English";
            this.English.Name = "English";
            // 
            // Arabic
            // 
            this.Arabic.HeaderText = "Arabic";
            this.Arabic.Name = "Arabic";
            // 
            // German
            // 
            this.German.HeaderText = "German";
            this.German.Name = "German";
            // 
            // Italian
            // 
            this.Italian.HeaderText = "Italian";
            this.Italian.Name = "Italian";
            // 
            // Japanese
            // 
            this.Japanese.HeaderText = "Japanese";
            this.Japanese.Name = "Japanese";
            // 
            // Korean
            // 
            this.Korean.HeaderText = "Korean";
            this.Korean.Name = "Korean";
            // 
            // Norwegian
            // 
            this.Norwegian.HeaderText = "Norwegian";
            this.Norwegian.Name = "Norwegian";
            // 
            // Spanish
            // 
            this.Spanish.HeaderText = "Spanish";
            this.Spanish.Name = "Spanish";
            // 
            // Swedish
            // 
            this.Swedish.HeaderText = "Swedish";
            this.Swedish.Name = "Swedish";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.confirmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(966, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // confirmToolStripMenuItem
            // 
            this.confirmToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("confirmToolStripMenuItem.Image")));
            this.confirmToolStripMenuItem.Name = "confirmToolStripMenuItem";
            this.confirmToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.confirmToolStripMenuItem.Text = "Save";
            this.confirmToolStripMenuItem.Click += new System.EventHandler(this.confirmToolStripMenuItem_Click);
            // 
            // AddConversionData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 185);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddConversionData";
            this.Text = "Add Conversion Data";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn English;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arabic;
        private System.Windows.Forms.DataGridViewTextBoxColumn German;
        private System.Windows.Forms.DataGridViewTextBoxColumn Italian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Japanese;
        private System.Windows.Forms.DataGridViewTextBoxColumn Korean;
        private System.Windows.Forms.DataGridViewTextBoxColumn Norwegian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spanish;
        private System.Windows.Forms.DataGridViewTextBoxColumn Swedish;
    }
}