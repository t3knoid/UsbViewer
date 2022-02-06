namespace USBViewer
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
            this.toolStripMainForm = new System.Windows.Forms.ToolStrip();
            this.tsbDetect = new System.Windows.Forms.ToolStripButton();
            this.tsbQuit = new System.Windows.Forms.ToolStripButton();
            this.lvwList = new System.Windows.Forms.ListView();
            this.clnIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnUsbstor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnUsbstorName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListMainForm = new System.Windows.Forms.ImageList(this.components);
            this.toolStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMainForm
            // 
            this.toolStripMainForm.AutoSize = false;
            this.toolStripMainForm.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDetect,
            this.tsbQuit});
            this.toolStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripMainForm.Name = "toolStripMainForm";
            this.toolStripMainForm.Size = new System.Drawing.Size(784, 69);
            this.toolStripMainForm.TabIndex = 0;
            this.toolStripMainForm.Text = "toolStrip1";
            // 
            // tsbDetect
            // 
            this.tsbDetect.AutoSize = false;
            this.tsbDetect.Image = global::USBViewer.Properties.Resources.search;
            this.tsbDetect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDetect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDetect.Name = "tsbDetect";
            this.tsbDetect.Size = new System.Drawing.Size(61, 61);
            this.tsbDetect.Text = "Detect";
            this.tsbDetect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDetect.Click += new System.EventHandler(this.tsbDetect_Click);
            // 
            // tsbQuit
            // 
            this.tsbQuit.AutoSize = false;
            this.tsbQuit.Image = global::USBViewer.Properties.Resources.quit;
            this.tsbQuit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuit.Name = "tsbQuit";
            this.tsbQuit.Size = new System.Drawing.Size(61, 61);
            this.tsbQuit.Text = "Quit";
            this.tsbQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbQuit.Click += new System.EventHandler(this.tsbQuit_Click);
            // 
            // lvwList
            // 
            this.lvwList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnIndex,
            this.clnUsbstor,
            this.clnUsbstorName});
            this.lvwList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwList.FullRowSelect = true;
            this.lvwList.GridLines = true;
            this.lvwList.HideSelection = false;
            this.lvwList.Location = new System.Drawing.Point(0, 69);
            this.lvwList.Name = "lvwList";
            this.lvwList.Size = new System.Drawing.Size(784, 540);
            this.lvwList.SmallImageList = this.imageListMainForm;
            this.lvwList.TabIndex = 1;
            this.lvwList.UseCompatibleStateImageBehavior = false;
            this.lvwList.View = System.Windows.Forms.View.Details;
            // 
            // clnIndex
            // 
            this.clnIndex.Text = "";
            this.clnIndex.Width = 40;
            // 
            // clnUsbstor
            // 
            this.clnUsbstor.Text = "USB storage device tag";
            this.clnUsbstor.Width = 400;
            // 
            // clnUsbstorName
            // 
            this.clnUsbstorName.Text = "USB Storage Device / Manufacturer Name";
            this.clnUsbstorName.Width = 400;
            // 
            // imageListMainForm
            // 
            this.imageListMainForm.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMainForm.ImageStream")));
            this.imageListMainForm.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMainForm.Images.SetKeyName(0, "usb.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 609);
            this.Controls.Add(this.lvwList);
            this.Controls.Add(this.toolStripMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "USB Storage device usage trace detection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripMainForm.ResumeLayout(false);
            this.toolStripMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMainForm;
        private System.Windows.Forms.ListView lvwList;
        private System.Windows.Forms.ColumnHeader clnIndex;
        private System.Windows.Forms.ImageList imageListMainForm;
        private System.Windows.Forms.ToolStripButton tsbDetect;
        private System.Windows.Forms.ToolStripButton tsbQuit;
        private System.Windows.Forms.ColumnHeader clnUsbstor;
        private System.Windows.Forms.ColumnHeader clnUsbstorName;
    }
}

