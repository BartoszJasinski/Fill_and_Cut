namespace Sketchpad
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
            this.canvas = new System.Windows.Forms.FlowLayoutPanel();
            this.constraintsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verticalEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixAngleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constraintsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.ContextMenuStrip = this.constraintsMenuStrip;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(622, 524);
            this.canvas.TabIndex = 3;
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // constraintsMenuStrip
            // 
            this.constraintsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verticalEdgeToolStripMenuItem,
            this.horizontalEdgeToolStripMenuItem,
            this.fixAngleToolStripMenuItem});
            this.constraintsMenuStrip.Name = "contextMenuStrip1";
            this.constraintsMenuStrip.Size = new System.Drawing.Size(191, 92);
            // 
            // verticalEdgeToolStripMenuItem
            // 
            this.verticalEdgeToolStripMenuItem.Name = "verticalEdgeToolStripMenuItem";
            this.verticalEdgeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.verticalEdgeToolStripMenuItem.Text = "Make Edge Vertical";
            this.verticalEdgeToolStripMenuItem.Click += new System.EventHandler(this.verticalEdgeToolStripMenuItem_Click);
            // 
            // horizontalEdgeToolStripMenuItem
            // 
            this.horizontalEdgeToolStripMenuItem.Name = "horizontalEdgeToolStripMenuItem";
            this.horizontalEdgeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.horizontalEdgeToolStripMenuItem.Text = "Make Edge Horizontal";
            this.horizontalEdgeToolStripMenuItem.Click += new System.EventHandler(this.horizontalEdgeToolStripMenuItem_Click);
            // 
            // fixAngleToolStripMenuItem
            // 
            this.fixAngleToolStripMenuItem.Name = "fixAngleToolStripMenuItem";
            this.fixAngleToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.fixAngleToolStripMenuItem.Text = "Fix Angle";
            this.fixAngleToolStripMenuItem.Click += new System.EventHandler(this.fixAngleToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 524);
            this.Controls.Add(this.canvas);
            this.Name = "MainForm";
            this.Text = "SketchPad";
            this.constraintsMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel canvas;
        private System.Windows.Forms.ContextMenuStrip constraintsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem verticalEdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalEdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixAngleToolStripMenuItem;
    }
}

