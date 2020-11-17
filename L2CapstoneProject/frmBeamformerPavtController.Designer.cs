﻿namespace L2CapstoneProject
{
    partial class frmBeamformerPavtController
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
            this.lsvOffsets = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddOffset = new System.Windows.Forms.Button();
            this.btnEditOffset = new System.Windows.Forms.Button();
            this.configurationGroupBox = new System.Windows.Forms.GroupBox();
            this.measurementLengthNumeric = new System.Windows.Forms.NumericUpDown();
            this.frequencyNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.measurementOffsetNumeric = new System.Windows.Forms.NumericUpDown();
            this.powerLevelNumeric = new System.Windows.Forms.NumericUpDown();
            this.powerLevelLabel = new System.Windows.Forms.Label();
            this.btnDeleteOffset = new System.Windows.Forms.Button();
            this.resourceNameLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.rfsgNameComboBox = new System.Windows.Forms.ComboBox();
            this.errorTextBox = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStepped = new System.Windows.Forms.RadioButton();
            this.btnSequenced = new System.Windows.Forms.RadioButton();
            this.rfsaNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lsvResults = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configurationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.measurementLengthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.measurementOffsetNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerLevelNumeric)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvOffsets
            // 
            this.lsvOffsets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvOffsets.FullRowSelect = true;
            this.lsvOffsets.GridLines = true;
            this.lsvOffsets.HideSelection = false;
            this.lsvOffsets.Location = new System.Drawing.Point(36, 30);
            this.lsvOffsets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lsvOffsets.Name = "lsvOffsets";
            this.lsvOffsets.Size = new System.Drawing.Size(315, 180);
            this.lsvOffsets.TabIndex = 1;
            this.lsvOffsets.UseCompatibleStateImageBehavior = false;
            this.lsvOffsets.View = System.Windows.Forms.View.Details;
            this.lsvOffsets.SelectedIndexChanged += new System.EventHandler(this.lsvOffsets_SelectedIndexChanged);
            this.lsvOffsets.DoubleClick += new System.EventHandler(this.EditListViewItem);
            this.lsvOffsets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvOffsets_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Phase (°)";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amplitude (dB)";
            this.columnHeader2.Width = 133;
            // 
            // btnAddOffset
            // 
            this.btnAddOffset.Location = new System.Drawing.Point(36, 216);
            this.btnAddOffset.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddOffset.Name = "btnAddOffset";
            this.btnAddOffset.Size = new System.Drawing.Size(100, 28);
            this.btnAddOffset.TabIndex = 2;
            this.btnAddOffset.Text = "&Add";
            this.btnAddOffset.UseVisualStyleBackColor = true;
            this.btnAddOffset.Click += new System.EventHandler(this.btnAddOffset_Click);
            // 
            // btnEditOffset
            // 
            this.btnEditOffset.Enabled = false;
            this.btnEditOffset.Location = new System.Drawing.Point(144, 216);
            this.btnEditOffset.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditOffset.Name = "btnEditOffset";
            this.btnEditOffset.Size = new System.Drawing.Size(100, 28);
            this.btnEditOffset.TabIndex = 2;
            this.btnEditOffset.Text = "&Edit";
            this.btnEditOffset.UseVisualStyleBackColor = true;
            this.btnEditOffset.Click += new System.EventHandler(this.EditListViewItem);
            // 
            // configurationGroupBox
            // 
            this.configurationGroupBox.Controls.Add(this.measurementLengthNumeric);
            this.configurationGroupBox.Controls.Add(this.frequencyNumeric);
            this.configurationGroupBox.Controls.Add(this.label4);
            this.configurationGroupBox.Controls.Add(this.label3);
            this.configurationGroupBox.Controls.Add(this.frequencyLabel);
            this.configurationGroupBox.Controls.Add(this.measurementOffsetNumeric);
            this.configurationGroupBox.Controls.Add(this.powerLevelNumeric);
            this.configurationGroupBox.Controls.Add(this.powerLevelLabel);
            this.configurationGroupBox.Location = new System.Drawing.Point(37, 90);
            this.configurationGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.configurationGroupBox.Name = "configurationGroupBox";
            this.configurationGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.configurationGroupBox.Size = new System.Drawing.Size(397, 153);
            this.configurationGroupBox.TabIndex = 13;
            this.configurationGroupBox.TabStop = false;
            this.configurationGroupBox.Text = "Configuration";
            // 
            // measurementLengthNumeric
            // 
            this.measurementLengthNumeric.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.measurementLengthNumeric.Location = new System.Drawing.Point(212, 48);
            this.measurementLengthNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.measurementLengthNumeric.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.measurementLengthNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.measurementLengthNumeric.Name = "measurementLengthNumeric";
            this.measurementLengthNumeric.Size = new System.Drawing.Size(128, 22);
            this.measurementLengthNumeric.TabIndex = 1;
            this.measurementLengthNumeric.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // frequencyNumeric
            // 
            this.frequencyNumeric.DecimalPlaces = 2;
            this.frequencyNumeric.Increment = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.frequencyNumeric.Location = new System.Drawing.Point(17, 48);
            this.frequencyNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.frequencyNumeric.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.frequencyNumeric.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.frequencyNumeric.Name = "frequencyNumeric";
            this.frequencyNumeric.Size = new System.Drawing.Size(128, 22);
            this.frequencyNumeric.TabIndex = 1;
            this.frequencyNumeric.Value = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Measurement Offset (us)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Measurement Length (us)";
            // 
            // frequencyLabel
            // 
            this.frequencyLabel.AutoSize = true;
            this.frequencyLabel.Location = new System.Drawing.Point(17, 28);
            this.frequencyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(150, 17);
            this.frequencyLabel.TabIndex = 1;
            this.frequencyLabel.Text = "Center Frequency [Hz]";
            // 
            // measurementOffsetNumeric
            // 
            this.measurementOffsetNumeric.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.measurementOffsetNumeric.Location = new System.Drawing.Point(212, 107);
            this.measurementOffsetNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.measurementOffsetNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.measurementOffsetNumeric.Name = "measurementOffsetNumeric";
            this.measurementOffsetNumeric.Size = new System.Drawing.Size(128, 22);
            this.measurementOffsetNumeric.TabIndex = 2;
            this.measurementOffsetNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // powerLevelNumeric
            // 
            this.powerLevelNumeric.DecimalPlaces = 2;
            this.powerLevelNumeric.Location = new System.Drawing.Point(17, 107);
            this.powerLevelNumeric.Margin = new System.Windows.Forms.Padding(4);
            this.powerLevelNumeric.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.powerLevelNumeric.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.powerLevelNumeric.Name = "powerLevelNumeric";
            this.powerLevelNumeric.Size = new System.Drawing.Size(128, 22);
            this.powerLevelNumeric.TabIndex = 2;
            this.powerLevelNumeric.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // powerLevelLabel
            // 
            this.powerLevelLabel.AutoSize = true;
            this.powerLevelLabel.Location = new System.Drawing.Point(17, 87);
            this.powerLevelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.powerLevelLabel.Name = "powerLevelLabel";
            this.powerLevelLabel.Size = new System.Drawing.Size(125, 17);
            this.powerLevelLabel.TabIndex = 2;
            this.powerLevelLabel.Text = "Power Level [dBm]";
            // 
            // btnDeleteOffset
            // 
            this.btnDeleteOffset.Enabled = false;
            this.btnDeleteOffset.Location = new System.Drawing.Point(252, 216);
            this.btnDeleteOffset.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteOffset.Name = "btnDeleteOffset";
            this.btnDeleteOffset.Size = new System.Drawing.Size(100, 28);
            this.btnDeleteOffset.TabIndex = 2;
            this.btnDeleteOffset.Text = "&Delete";
            this.btnDeleteOffset.UseVisualStyleBackColor = true;
            this.btnDeleteOffset.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // resourceNameLabel
            // 
            this.resourceNameLabel.AutoSize = true;
            this.resourceNameLabel.Location = new System.Drawing.Point(35, 20);
            this.resourceNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.resourceNameLabel.Name = "resourceNameLabel";
            this.resourceNameLabel.Size = new System.Drawing.Size(179, 17);
            this.resourceNameLabel.TabIndex = 11;
            this.resourceNameLabel.Text = "Generator Resource Name";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(33, 549);
            this.errorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(103, 17);
            this.errorLabel.TabIndex = 15;
            this.errorLabel.Text = "Current Status:";
            // 
            // rfsgNameComboBox
            // 
            this.rfsgNameComboBox.Location = new System.Drawing.Point(37, 39);
            this.rfsgNameComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.rfsgNameComboBox.Name = "rfsgNameComboBox";
            this.rfsgNameComboBox.Size = new System.Drawing.Size(159, 24);
            this.rfsgNameComboBox.TabIndex = 12;
            // 
            // errorTextBox
            // 
            this.errorTextBox.Location = new System.Drawing.Point(37, 569);
            this.errorTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.errorTextBox.Multiline = true;
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.ReadOnly = true;
            this.errorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.errorTextBox.Size = new System.Drawing.Size(745, 73);
            this.errorTextBox.TabIndex = 17;
            this.errorTextBox.TabStop = false;
            this.errorTextBox.Text = "No error.";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(507, 36);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStepped);
            this.groupBox1.Controls.Add(this.btnSequenced);
            this.groupBox1.Controls.Add(this.lsvOffsets);
            this.groupBox1.Controls.Add(this.btnEditOffset);
            this.groupBox1.Controls.Add(this.btnDeleteOffset);
            this.groupBox1.Controls.Add(this.btnAddOffset);
            this.groupBox1.Location = new System.Drawing.Point(37, 250);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(397, 283);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Phase and Amplitude Offsets";
            // 
            // btnStepped
            // 
            this.btnStepped.AutoSize = true;
            this.btnStepped.Location = new System.Drawing.Point(205, 255);
            this.btnStepped.Name = "btnStepped";
            this.btnStepped.Size = new System.Drawing.Size(163, 21);
            this.btnStepped.TabIndex = 4;
            this.btnStepped.Text = "Stepped Beamformer";
            this.btnStepped.UseVisualStyleBackColor = true;
            this.btnStepped.CheckedChanged += new System.EventHandler(this.btnStepped_CheckedChanged);
            this.btnStepped.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStepped_MouseDown);
            // 
            // btnSequenced
            // 
            this.btnSequenced.AutoSize = true;
            this.btnSequenced.Location = new System.Drawing.Point(17, 255);
            this.btnSequenced.Name = "btnSequenced";
            this.btnSequenced.Size = new System.Drawing.Size(182, 21);
            this.btnSequenced.TabIndex = 4;
            this.btnSequenced.Text = "Sequenced Beamformer";
            this.btnSequenced.UseVisualStyleBackColor = true;
            this.btnSequenced.CheckedChanged += new System.EventHandler(this.btnSequenced_CheckedChanged);
            this.btnSequenced.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSequenced_MouseDown);
            // 
            // rfsaNameComboBox
            // 
            this.rfsaNameComboBox.Location = new System.Drawing.Point(229, 39);
            this.rfsaNameComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.rfsaNameComboBox.Name = "rfsaNameComboBox";
            this.rfsaNameComboBox.Size = new System.Drawing.Size(159, 24);
            this.rfsaNameComboBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Analyzer Resource Name";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(615, 36);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 28);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "St&op";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lsvResults);
            this.groupBox2.Location = new System.Drawing.Point(456, 91);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(328, 442);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Relative Results";
            // 
            // lsvResults
            // 
            this.lsvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6});
            this.lsvResults.FullRowSelect = true;
            this.lsvResults.GridLines = true;
            this.lsvResults.HideSelection = false;
            this.lsvResults.Location = new System.Drawing.Point(15, 47);
            this.lsvResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lsvResults.Name = "lsvResults";
            this.lsvResults.Size = new System.Drawing.Size(293, 345);
            this.lsvResults.TabIndex = 0;
            this.lsvResults.UseCompatibleStateImageBehavior = false;
            this.lsvResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Index";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Phase (°)";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Amplitude (dB)";
            this.columnHeader6.Width = 97;
            // 
            // frmBeamformerPavtController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 668);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.configurationGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resourceNameLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.rfsaNameComboBox);
            this.Controls.Add(this.rfsgNameComboBox);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBeamformerPavtController";
            this.Text = "Beamformer PAVT Measurement";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBeamformerPavtController_FormClosing);
            this.configurationGroupBox.ResumeLayout(false);
            this.configurationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.measurementLengthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.measurementOffsetNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerLevelNumeric)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvOffsets;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAddOffset;
        private System.Windows.Forms.Button btnEditOffset;
        private System.Windows.Forms.GroupBox configurationGroupBox;
        private System.Windows.Forms.NumericUpDown frequencyNumeric;
        private System.Windows.Forms.Label frequencyLabel;
        private System.Windows.Forms.NumericUpDown powerLevelNumeric;
        private System.Windows.Forms.Label powerLevelLabel;
        private System.Windows.Forms.Button btnDeleteOffset;
        private System.Windows.Forms.Label resourceNameLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.ComboBox rfsgNameComboBox;
        private System.Windows.Forms.TextBox errorTextBox;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox rfsaNameComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lsvResults;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown measurementLengthNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown measurementOffsetNumeric;
        private System.Windows.Forms.RadioButton btnStepped;
        private System.Windows.Forms.RadioButton btnSequenced;
    }
}

