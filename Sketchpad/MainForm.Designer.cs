namespace FillCut
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.PolygonColorPictureBox = new System.Windows.Forms.PictureBox();
            this.texturePictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.lightColorPictureBox = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.constantNormalVectorRadioButton = new System.Windows.Forms.RadioButton();
            this.normalMapRadioButton = new System.Windows.Forms.RadioButton();
            this.normalMapPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.constatnLightVectorRadioButton = new System.Windows.Forms.RadioButton();
            this.movingLightVectorRadioButton4 = new System.Windows.Forms.RadioButton();
            this.movingLightVectorTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lackOfDisorderRadioButton = new System.Windows.Forms.RadioButton();
            this.heightMapRadioButton = new System.Windows.Forms.RadioButton();
            this.heightMapPictureBox = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PolygonColorColorDialog = new System.Windows.Forms.ColorDialog();
            this.LightColorColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PolygonColorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.texturePictureBox)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightColorPictureBox)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.normalMapPictureBox)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightMapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(260, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(512, 512);
            this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.36776F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.63224F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.canvas, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(794, 616);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(794, 616);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 616);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.MaximumSize = new System.Drawing.Size(250, 610);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(250, 610);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 12;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.37441F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.62559F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(250, 610);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.PolygonColorPictureBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.texturePictureBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(244, 99);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // PolygonColorPictureBox
            // 
            this.PolygonColorPictureBox.BackColor = System.Drawing.Color.White;
            this.PolygonColorPictureBox.Location = new System.Drawing.Point(125, 3);
            this.PolygonColorPictureBox.Name = "PolygonColorPictureBox";
            this.PolygonColorPictureBox.Size = new System.Drawing.Size(60, 43);
            this.PolygonColorPictureBox.TabIndex = 9;
            this.PolygonColorPictureBox.TabStop = false;
            this.PolygonColorPictureBox.Click += new System.EventHandler(this.PolygonColorPictureBox_Click);
            // 
            // texturePictureBox
            // 
            this.texturePictureBox.BackColor = System.Drawing.Color.White;
            this.texturePictureBox.Location = new System.Drawing.Point(125, 52);
            this.texturePictureBox.Name = "texturePictureBox";
            this.texturePictureBox.Size = new System.Drawing.Size(60, 44);
            this.texturePictureBox.TabIndex = 11;
            this.texturePictureBox.TabStop = false;
            this.texturePictureBox.Click += new System.EventHandler(this.texturePictureBox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Stały";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tekstura";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Polygon Color";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Light Color";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lightColorPictureBox, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 146);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(244, 54);
            this.tableLayoutPanel4.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "I_L";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lightColorPictureBox
            // 
            this.lightColorPictureBox.BackColor = System.Drawing.Color.White;
            this.lightColorPictureBox.Location = new System.Drawing.Point(125, 3);
            this.lightColorPictureBox.Name = "lightColorPictureBox";
            this.lightColorPictureBox.Size = new System.Drawing.Size(60, 44);
            this.lightColorPictureBox.TabIndex = 1;
            this.lightColorPictureBox.TabStop = false;
            this.lightColorPictureBox.Click += new System.EventHandler(this.lightColorPictureBox_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Wektor normalny";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.constantNormalVectorRadioButton, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.normalMapRadioButton, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.normalMapPictureBox, 1, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 222);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.48936F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.51064F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(244, 94);
            this.tableLayoutPanel5.TabIndex = 14;
            // 
            // constantNormalVectorRadioButton
            // 
            this.constantNormalVectorRadioButton.AutoSize = true;
            this.constantNormalVectorRadioButton.Location = new System.Drawing.Point(3, 3);
            this.constantNormalVectorRadioButton.Name = "constantNormalVectorRadioButton";
            this.constantNormalVectorRadioButton.Size = new System.Drawing.Size(89, 17);
            this.constantNormalVectorRadioButton.TabIndex = 1;
            this.constantNormalVectorRadioButton.Text = "Stały [0, 0, 1]";
            this.constantNormalVectorRadioButton.UseVisualStyleBackColor = true;
            this.constantNormalVectorRadioButton.CheckedChanged += new System.EventHandler(this.constantNormalVectorRadioButton_CheckedChanged);
            // 
            // normalMapRadioButton
            // 
            this.normalMapRadioButton.AutoSize = true;
            this.normalMapRadioButton.Checked = true;
            this.normalMapRadioButton.Location = new System.Drawing.Point(3, 41);
            this.normalMapRadioButton.Name = "normalMapRadioButton";
            this.normalMapRadioButton.Size = new System.Drawing.Size(116, 17);
            this.normalMapRadioButton.TabIndex = 2;
            this.normalMapRadioButton.TabStop = true;
            this.normalMapRadioButton.Text = "Z tekstury NormalMap";
            this.normalMapRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.normalMapRadioButton.UseVisualStyleBackColor = true;
            this.normalMapRadioButton.CheckedChanged += new System.EventHandler(this.normalMapRadioButton_CheckedChanged);
            // 
            // normalMapPictureBox
            // 
            this.normalMapPictureBox.BackColor = System.Drawing.Color.White;
            this.normalMapPictureBox.Location = new System.Drawing.Point(125, 41);
            this.normalMapPictureBox.Name = "normalMapPictureBox";
            this.normalMapPictureBox.Size = new System.Drawing.Size(60, 44);
            this.normalMapPictureBox.TabIndex = 3;
            this.normalMapPictureBox.TabStop = false;
            this.normalMapPictureBox.Click += new System.EventHandler(this.normalMapPictureBox_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.11475F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.88525F));
            this.tableLayoutPanel6.Controls.Add(this.constatnLightVectorRadioButton, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.movingLightVectorRadioButton4, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.movingLightVectorTextBox, 1, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 341);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(244, 86);
            this.tableLayoutPanel6.TabIndex = 16;
            // 
            // constatnLightVectorRadioButton
            // 
            this.constatnLightVectorRadioButton.AutoSize = true;
            this.constatnLightVectorRadioButton.Checked = true;
            this.constatnLightVectorRadioButton.Location = new System.Drawing.Point(3, 3);
            this.constatnLightVectorRadioButton.Name = "constatnLightVectorRadioButton";
            this.constatnLightVectorRadioButton.Size = new System.Drawing.Size(89, 17);
            this.constatnLightVectorRadioButton.TabIndex = 0;
            this.constatnLightVectorRadioButton.TabStop = true;
            this.constatnLightVectorRadioButton.Text = "Stały [0, 0, 1]";
            this.constatnLightVectorRadioButton.UseVisualStyleBackColor = true;
            this.constatnLightVectorRadioButton.CheckedChanged += new System.EventHandler(this.constatnLightVectorRadioButton_CheckedChanged);
            // 
            // movingLightVectorRadioButton4
            // 
            this.movingLightVectorRadioButton4.AutoSize = true;
            this.movingLightVectorRadioButton4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.movingLightVectorRadioButton4.Location = new System.Drawing.Point(3, 46);
            this.movingLightVectorRadioButton4.Name = "movingLightVectorRadioButton4";
            this.movingLightVectorRadioButton4.Size = new System.Drawing.Size(146, 17);
            this.movingLightVectorRadioButton4.TabIndex = 1;
            this.movingLightVectorRadioButton4.TabStop = true;
            this.movingLightVectorRadioButton4.Text = "Animowane po sferze R =";
            this.movingLightVectorRadioButton4.UseVisualStyleBackColor = true;
            this.movingLightVectorRadioButton4.CheckedChanged += new System.EventHandler(this.movingLightVectorRadioButton4_CheckedChanged);
            // 
            // movingLightVectorTextBox
            // 
            this.movingLightVectorTextBox.Location = new System.Drawing.Point(156, 46);
            this.movingLightVectorTextBox.Name = "movingLightVectorTextBox";
            this.movingLightVectorTextBox.Size = new System.Drawing.Size(85, 20);
            this.movingLightVectorTextBox.TabIndex = 2;
            this.movingLightVectorTextBox.TextChanged += new System.EventHandler(this.movingLightVectorTextBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 430);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Zaburzenie D";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.lackOfDisorderRadioButton, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.heightMapRadioButton, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.heightMapPictureBox, 1, 1);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 465);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(244, 118);
            this.tableLayoutPanel7.TabIndex = 18;
            // 
            // lackOfDisorderRadioButton
            // 
            this.lackOfDisorderRadioButton.AutoSize = true;
            this.lackOfDisorderRadioButton.Location = new System.Drawing.Point(3, 3);
            this.lackOfDisorderRadioButton.Name = "lackOfDisorderRadioButton";
            this.lackOfDisorderRadioButton.Size = new System.Drawing.Size(86, 17);
            this.lackOfDisorderRadioButton.TabIndex = 0;
            this.lackOfDisorderRadioButton.TabStop = true;
            this.lackOfDisorderRadioButton.Text = "Brak [0, 0, 0]";
            this.lackOfDisorderRadioButton.UseVisualStyleBackColor = true;
            this.lackOfDisorderRadioButton.CheckedChanged += new System.EventHandler(this.lackOfDisorderRadioButton_CheckedChanged);
            // 
            // heightMapRadioButton
            // 
            this.heightMapRadioButton.AutoSize = true;
            this.heightMapRadioButton.Checked = true;
            this.heightMapRadioButton.Location = new System.Drawing.Point(3, 62);
            this.heightMapRadioButton.Name = "heightMapRadioButton";
            this.heightMapRadioButton.Size = new System.Drawing.Size(116, 17);
            this.heightMapRadioButton.TabIndex = 1;
            this.heightMapRadioButton.TabStop = true;
            this.heightMapRadioButton.Text = "Z tekstury Height Map";
            this.heightMapRadioButton.UseVisualStyleBackColor = true;
            this.heightMapRadioButton.CheckedChanged += new System.EventHandler(this.heightMapRadioButton_CheckedChanged);
            // 
            // heightMapPictureBox
            // 
            this.heightMapPictureBox.BackColor = System.Drawing.Color.White;
            this.heightMapPictureBox.Location = new System.Drawing.Point(125, 62);
            this.heightMapPictureBox.Name = "heightMapPictureBox";
            this.heightMapPictureBox.Size = new System.Drawing.Size(60, 44);
            this.heightMapPictureBox.TabIndex = 2;
            this.heightMapPictureBox.TabStop = false;
            this.heightMapPictureBox.Click += new System.EventHandler(this.heightMapPictureBox_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Wektor źródła światła";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 591);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "SketchPad";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PolygonColorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.texturePictureBox)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightColorPictureBox)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.normalMapPictureBox)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightMapPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ColorDialog PolygonColorColorDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox PolygonColorPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox texturePictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox lightColorPictureBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.RadioButton constantNormalVectorRadioButton;
        private System.Windows.Forms.RadioButton normalMapRadioButton;
        private System.Windows.Forms.PictureBox normalMapPictureBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.RadioButton constatnLightVectorRadioButton;
        private System.Windows.Forms.RadioButton movingLightVectorRadioButton4;
        private System.Windows.Forms.TextBox movingLightVectorTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.RadioButton lackOfDisorderRadioButton;
        private System.Windows.Forms.RadioButton heightMapRadioButton;
        private System.Windows.Forms.PictureBox heightMapPictureBox;
        private System.Windows.Forms.ColorDialog LightColorColorDialog;
    }
}

