
namespace MechanicalAutoClicker
{
    partial class Form1
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
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.ModeCheckBox = new System.Windows.Forms.CheckBox();
            this.RandomTapCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoTouchCkeckBox = new System.Windows.Forms.CheckBox();
            this.LoadTapsBtn = new System.Windows.Forms.Button();
            this.FileLabel = new System.Windows.Forms.Label();
            this.SliderSpeedTextBox = new System.Windows.Forms.TextBox();
            this.ScaleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.VMultTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.MoveImmediatelyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DrawPanel
            // 
            this.DrawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DrawPanel.Location = new System.Drawing.Point(12, 12);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(263, 437);
            this.DrawPanel.TabIndex = 0;
            // 
            // ModeCheckBox
            // 
            this.ModeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModeCheckBox.AutoSize = true;
            this.ModeCheckBox.Checked = true;
            this.ModeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ModeCheckBox.Location = new System.Drawing.Point(293, 12);
            this.ModeCheckBox.Name = "ModeCheckBox";
            this.ModeCheckBox.Size = new System.Drawing.Size(81, 17);
            this.ModeCheckBox.TabIndex = 1;
            this.ModeCheckBox.Text = "Touch Only";
            this.ModeCheckBox.UseVisualStyleBackColor = true;
            this.ModeCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RandomTapCheckBox
            // 
            this.RandomTapCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RandomTapCheckBox.AutoSize = true;
            this.RandomTapCheckBox.Location = new System.Drawing.Point(281, 35);
            this.RandomTapCheckBox.Name = "RandomTapCheckBox";
            this.RandomTapCheckBox.Size = new System.Drawing.Size(93, 17);
            this.RandomTapCheckBox.TabIndex = 2;
            this.RandomTapCheckBox.Text = "Random Taps";
            this.RandomTapCheckBox.UseVisualStyleBackColor = true;
            this.RandomTapCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // AutoTouchCkeckBox
            // 
            this.AutoTouchCkeckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoTouchCkeckBox.AutoSize = true;
            this.AutoTouchCkeckBox.Location = new System.Drawing.Point(292, 58);
            this.AutoTouchCkeckBox.Name = "AutoTouchCkeckBox";
            this.AutoTouchCkeckBox.Size = new System.Drawing.Size(82, 17);
            this.AutoTouchCkeckBox.TabIndex = 3;
            this.AutoTouchCkeckBox.Text = "Auto Touch";
            this.AutoTouchCkeckBox.UseVisualStyleBackColor = true;
            this.AutoTouchCkeckBox.CheckedChanged += new System.EventHandler(this.AutoTouchCkeckBox_CheckedChanged);
            // 
            // LoadTapsBtn
            // 
            this.LoadTapsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadTapsBtn.Location = new System.Drawing.Point(299, 121);
            this.LoadTapsBtn.Name = "LoadTapsBtn";
            this.LoadTapsBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadTapsBtn.TabIndex = 4;
            this.LoadTapsBtn.Text = "Load Taps";
            this.LoadTapsBtn.UseVisualStyleBackColor = true;
            this.LoadTapsBtn.Click += new System.EventHandler(this.LoadTapsBtn_Click);
            // 
            // FileLabel
            // 
            this.FileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FileLabel.Location = new System.Drawing.Point(281, 147);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(93, 17);
            this.FileLabel.TabIndex = 5;
            this.FileLabel.Text = "Nothing Loaded";
            this.FileLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SliderSpeedTextBox
            // 
            this.SliderSpeedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SliderSpeedTextBox.Location = new System.Drawing.Point(328, 167);
            this.SliderSpeedTextBox.Name = "SliderSpeedTextBox";
            this.SliderSpeedTextBox.Size = new System.Drawing.Size(46, 20);
            this.SliderSpeedTextBox.TabIndex = 6;
            this.SliderSpeedTextBox.Text = "1";
            // 
            // ScaleTextBox
            // 
            this.ScaleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScaleTextBox.Location = new System.Drawing.Point(328, 193);
            this.ScaleTextBox.Name = "ScaleTextBox";
            this.ScaleTextBox.Size = new System.Drawing.Size(46, 20);
            this.ScaleTextBox.TabIndex = 7;
            this.ScaleTextBox.Text = "0.135";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(281, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "SlMult";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(281, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Scale";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(281, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "VMult";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // VMultTextBox
            // 
            this.VMultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VMultTextBox.Location = new System.Drawing.Point(328, 219);
            this.VMultTextBox.Name = "VMultTextBox";
            this.VMultTextBox.Size = new System.Drawing.Size(46, 20);
            this.VMultTextBox.TabIndex = 10;
            this.VMultTextBox.Text = "1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(281, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "V";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(328, 271);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(46, 20);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "450";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(281, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Acc";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(328, 245);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(46, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "8000";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // MoveImmediatelyCheckBox
            // 
            this.MoveImmediatelyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveImmediatelyCheckBox.AutoSize = true;
            this.MoveImmediatelyCheckBox.Checked = true;
            this.MoveImmediatelyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MoveImmediatelyCheckBox.Location = new System.Drawing.Point(281, 81);
            this.MoveImmediatelyCheckBox.Name = "MoveImmediatelyCheckBox";
            this.MoveImmediatelyCheckBox.Size = new System.Drawing.Size(111, 17);
            this.MoveImmediatelyCheckBox.TabIndex = 16;
            this.MoveImmediatelyCheckBox.Text = "Move Immediately";
            this.MoveImmediatelyCheckBox.UseVisualStyleBackColor = true;
            this.MoveImmediatelyCheckBox.CheckedChanged += new System.EventHandler(this.MoveImmediatelyCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 461);
            this.Controls.Add(this.MoveImmediatelyCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VMultTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScaleTextBox);
            this.Controls.Add(this.SliderSpeedTextBox);
            this.Controls.Add(this.FileLabel);
            this.Controls.Add(this.LoadTapsBtn);
            this.Controls.Add(this.AutoTouchCkeckBox);
            this.Controls.Add(this.RandomTapCheckBox);
            this.Controls.Add(this.ModeCheckBox);
            this.Controls.Add(this.DrawPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.CheckBox ModeCheckBox;
        private System.Windows.Forms.CheckBox RandomTapCheckBox;
        private System.Windows.Forms.CheckBox AutoTouchCkeckBox;
        private System.Windows.Forms.Button LoadTapsBtn;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.TextBox SliderSpeedTextBox;
        private System.Windows.Forms.TextBox ScaleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox VMultTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox MoveImmediatelyCheckBox;
    }
}

