namespace Mahou
{
	partial class LangPanel
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lbl_LayoutName;
		private System.Windows.Forms.PictureBox pct_Flag;
		private System.Windows.Forms.PictureBox pct_Upper;
		private System.Windows.Forms.FlowLayoutPanel flowRoot;

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.flowRoot = new System.Windows.Forms.FlowLayoutPanel();
			this.lbl_LayoutName = new System.Windows.Forms.Label();
			this.pct_Flag = new System.Windows.Forms.PictureBox();
			this.pct_Upper = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pct_Flag)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Upper)).BeginInit();
			this.flowRoot.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowRoot
			// 
			this.flowRoot.AutoSize = true;
			this.flowRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowRoot.Controls.Add(this.pct_Flag);
			this.flowRoot.Controls.Add(this.pct_Upper);
			this.flowRoot.Controls.Add(this.lbl_LayoutName);
			this.flowRoot.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
			this.flowRoot.Location = new System.Drawing.Point(0, 0);
			this.flowRoot.Margin = new System.Windows.Forms.Padding(0);
			this.flowRoot.Name = "flowRoot";
			this.flowRoot.Padding = new System.Windows.Forms.Padding(4);
			this.flowRoot.Size = new System.Drawing.Size(24, 24);
			this.flowRoot.TabIndex = 0;
			this.flowRoot.WrapContents = false;
			// 
			// pct_Flag
			// 
			this.pct_Flag.BackgroundImage = global::Mahou.Properties.Resources.jp;
			this.pct_Flag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pct_Flag.InitialImage = null;
			this.pct_Flag.Location = new System.Drawing.Point(7, 7);
			this.pct_Flag.Margin = new System.Windows.Forms.Padding(0);
			this.pct_Flag.Name = "pct_Flag";
			this.pct_Flag.Size = new System.Drawing.Size(16, 16);
			this.pct_Flag.TabIndex = 1;
			this.pct_Flag.TabStop = false;
			this.pct_Flag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseDown);
			this.pct_Flag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseMove);
			this.pct_Flag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseUp);
			// 
			// pct_Upper
			// 
			this.pct_Upper.BackgroundImage = global::Mahou.Properties.Resources.up;
			this.pct_Upper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pct_Upper.InitialImage = null;
			this.pct_Upper.Location = new System.Drawing.Point(23, 7);
			this.pct_Upper.Margin = new System.Windows.Forms.Padding(0);
			this.pct_Upper.Name = "pct_Upper";
			this.pct_Upper.Size = new System.Drawing.Size(16, 16);
			this.pct_Upper.TabIndex = 2;
			this.pct_Upper.TabStop = false;
			this.pct_Upper.Visible = false;
			this.pct_Upper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseDown);
			this.pct_Upper.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseMove);
			this.pct_Upper.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseUp);
			// 
			// lbl_LayoutName
			// 
			this.lbl_LayoutName.AutoSize = true;
			this.lbl_LayoutName.Location = new System.Drawing.Point(39, 7);
			this.lbl_LayoutName.Margin = new System.Windows.Forms.Padding(0);
			this.lbl_LayoutName.MinimumSize = new System.Drawing.Size(40, 0);
			this.lbl_LayoutName.Name = "lbl_LayoutName";
			this.lbl_LayoutName.TabIndex = 0;
			this.lbl_LayoutName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_LayoutName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseDown);
			this.lbl_LayoutName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseMove);
			this.lbl_LayoutName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseUp);
			// 
			// LangPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(40, 24);
			this.ControlBox = false;
			this.Controls.Add(this.flowRoot);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(400, 400);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(8, 8);
			this.Name = "LangPanel";
			this.Opacity = 0.9D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "LangPanel";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Peru;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LangPanelMouseUp);
			((System.ComponentModel.ISupportInitialize)(this.pct_Flag)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Upper)).EndInit();
			this.flowRoot.ResumeLayout(false);
			this.flowRoot.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
