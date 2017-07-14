namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tmrCursorPos = new System.Windows.Forms.Timer(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToConvewrsionTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeTheTextReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // tmrCursorPos
            // 
            this.tmrCursorPos.Interval = 5;
            this.tmrCursorPos.Tick += new System.EventHandler(this.tmrCursorPos_Tick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // returnToConvewrsionTableToolStripMenuItem
            // 
            this.returnToConvewrsionTableToolStripMenuItem.Name = "returnToConvewrsionTableToolStripMenuItem";
            this.returnToConvewrsionTableToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // storeTheTextReadToolStripMenuItem
            // 
            this.storeTheTextReadToolStripMenuItem.Name = "storeTheTextReadToolStripMenuItem";
            this.storeTheTextReadToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(200, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Artha";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrCursorPos;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem; 
        private System.Windows.Forms.ToolStripMenuItem returnToConvewrsionTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeTheTextReadToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}
