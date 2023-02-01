namespace Chromaticity
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.bezierCurvePictureBox = new System.Windows.Forms.PictureBox();
            this.horseshoePictureBox = new System.Windows.Forms.PictureBox();
            this.numberOfPointsLabel = new System.Windows.Forms.Label();
            this.numberOfPointsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.backgroundCheckBox = new System.Windows.Forms.CheckBox();
            this.blackDotsCheckBox = new System.Windows.Forms.CheckBox();
            this.createImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bezierCurvePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horseshoePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPointsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // bezierCurvePictureBox
            // 
            this.bezierCurvePictureBox.Location = new System.Drawing.Point(8, 12);
            this.bezierCurvePictureBox.Name = "bezierCurvePictureBox";
            this.bezierCurvePictureBox.Size = new System.Drawing.Size(567, 537);
            this.bezierCurvePictureBox.TabIndex = 0;
            this.bezierCurvePictureBox.TabStop = false;
            this.bezierCurvePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.BezierCurvePictureBox_Paint);
            this.bezierCurvePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BezierCurvePictureBox_MouseDown);
            this.bezierCurvePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BezierCurvePictureBox_MouseMove);
            this.bezierCurvePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BezierCurvePictureBox_MouseUp);
            // 
            // horseshoePictureBox
            // 
            this.horseshoePictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.horseshoePictureBox.Location = new System.Drawing.Point(593, 12);
            this.horseshoePictureBox.Name = "horseshoePictureBox";
            this.horseshoePictureBox.Size = new System.Drawing.Size(579, 537);
            this.horseshoePictureBox.TabIndex = 1;
            this.horseshoePictureBox.TabStop = false;
            this.horseshoePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.HorseshoePictureBox_Paint);
            // 
            // numberOfPointsLabel
            // 
            this.numberOfPointsLabel.AutoSize = true;
            this.numberOfPointsLabel.BackColor = System.Drawing.SystemColors.Control;
            this.numberOfPointsLabel.Location = new System.Drawing.Point(462, 23);
            this.numberOfPointsLabel.Name = "numberOfPointsLabel";
            this.numberOfPointsLabel.Size = new System.Drawing.Size(104, 15);
            this.numberOfPointsLabel.TabIndex = 2;
            this.numberOfPointsLabel.Text = "Number of points:";
            // 
            // numberOfPointsNumericUpDown
            // 
            this.numberOfPointsNumericUpDown.Location = new System.Drawing.Point(474, 41);
            this.numberOfPointsNumericUpDown.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numberOfPointsNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numberOfPointsNumericUpDown.Name = "numberOfPointsNumericUpDown";
            this.numberOfPointsNumericUpDown.Size = new System.Drawing.Size(92, 23);
            this.numberOfPointsNumericUpDown.TabIndex = 3;
            this.numberOfPointsNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numberOfPointsNumericUpDown.ValueChanged += new System.EventHandler(this.NumberOfPointsNumericUpDown_ValueChanged);
            // 
            // backgroundCheckBox
            // 
            this.backgroundCheckBox.AutoSize = true;
            this.backgroundCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.backgroundCheckBox.Checked = true;
            this.backgroundCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backgroundCheckBox.Location = new System.Drawing.Point(1073, 19);
            this.backgroundCheckBox.Name = "backgroundCheckBox";
            this.backgroundCheckBox.Size = new System.Drawing.Size(90, 19);
            this.backgroundCheckBox.TabIndex = 4;
            this.backgroundCheckBox.Text = "Background";
            this.backgroundCheckBox.UseVisualStyleBackColor = false;
            this.backgroundCheckBox.CheckedChanged += new System.EventHandler(this.BackgroundCheckBox_CheckedChanged);
            // 
            // blackDotsCheckBox
            // 
            this.blackDotsCheckBox.AutoSize = true;
            this.blackDotsCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.blackDotsCheckBox.Checked = true;
            this.blackDotsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blackDotsCheckBox.Location = new System.Drawing.Point(1073, 50);
            this.blackDotsCheckBox.Name = "blackDotsCheckBox";
            this.blackDotsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.blackDotsCheckBox.Size = new System.Drawing.Size(80, 19);
            this.blackDotsCheckBox.TabIndex = 5;
            this.blackDotsCheckBox.Text = "Black dots";
            this.blackDotsCheckBox.UseVisualStyleBackColor = false;
            this.blackDotsCheckBox.CheckedChanged += new System.EventHandler(this.BlackDotsCheckBox_CheckedChanged);
            // 
            // createImageButton
            // 
            this.createImageButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.createImageButton.Location = new System.Drawing.Point(1073, 84);
            this.createImageButton.Name = "createImageButton";
            this.createImageButton.Size = new System.Drawing.Size(90, 43);
            this.createImageButton.TabIndex = 6;
            this.createImageButton.Text = "Create Image";
            this.createImageButton.UseVisualStyleBackColor = false;
            this.createImageButton.Click += new System.EventHandler(this.CreateImageButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 557);
            this.Controls.Add(this.createImageButton);
            this.Controls.Add(this.blackDotsCheckBox);
            this.Controls.Add(this.backgroundCheckBox);
            this.Controls.Add(this.numberOfPointsNumericUpDown);
            this.Controls.Add(this.numberOfPointsLabel);
            this.Controls.Add(this.horseshoePictureBox);
            this.Controls.Add(this.bezierCurvePictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 596);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1200, 596);
            this.Name = "MainWindow";
            this.Text = "CIEXYZ";
            ((System.ComponentModel.ISupportInitialize)(this.bezierCurvePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horseshoePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPointsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox bezierCurvePictureBox;
        private PictureBox horseshoePictureBox;
        private Label numberOfPointsLabel;
        private NumericUpDown numberOfPointsNumericUpDown;
        private CheckBox backgroundCheckBox;
        private CheckBox blackDotsCheckBox;
        private Button createImageButton;
    }
}