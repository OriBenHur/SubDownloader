namespace SubDownloader
{
    partial class ProgramSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.numUpDownScanInterval = new System.Windows.Forms.NumericUpDown();
            this.chkBoxRunAtStartup = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbTranslators = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveTranslator = new System.Windows.Forms.Button();
            this.btnAddTranslator = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTranslatorWith = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTranslatorReplace = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDownSimConnections = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownScanInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSimConnections)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folders scan interval:";
            // 
            // numUpDownScanInterval
            // 
            this.numUpDownScanInterval.Increment = new decimal(new int[] {
                5,
                0,
                0,
                0});
            this.numUpDownScanInterval.Location = new System.Drawing.Point(223, 18);
            this.numUpDownScanInterval.Maximum = new decimal(new int[] {
                1440,
                0,
                0,
                0});
            this.numUpDownScanInterval.Minimum = new decimal(new int[] {
                5,
                0,
                0,
                0});
            this.numUpDownScanInterval.Name = "numUpDownScanInterval";
            this.numUpDownScanInterval.ReadOnly = true;
            this.numUpDownScanInterval.Size = new System.Drawing.Size(36, 20);
            this.numUpDownScanInterval.TabIndex = 1;
            this.numUpDownScanInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownScanInterval.Value = new decimal(new int[] {
                15,
                0,
                0,
                0});
            // 
            // chkBoxRunAtStartup
            // 
            this.chkBoxRunAtStartup.AutoSize = true;
            this.chkBoxRunAtStartup.Location = new System.Drawing.Point(223, 69);
            this.chkBoxRunAtStartup.Name = "chkBoxRunAtStartup";
            this.chkBoxRunAtStartup.Size = new System.Drawing.Size(15, 14);
            this.chkBoxRunAtStartup.TabIndex = 2;
            this.chkBoxRunAtStartup.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(167, 248);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(86, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbTranslators
            // 
            this.lbTranslators.FormattingEnabled = true;
            this.lbTranslators.Location = new System.Drawing.Point(9, 42);
            this.lbTranslators.Name = "lbTranslators";
            this.lbTranslators.Size = new System.Drawing.Size(410, 82);
            this.lbTranslators.TabIndex = 5;
            this.lbTranslators.SelectedIndexChanged += new System.EventHandler(this.lbTranslators_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveTranslator);
            this.groupBox1.Controls.Add(this.btnAddTranslator);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTranslatorWith);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTranslatorReplace);
            this.groupBox1.Controls.Add(this.lbTranslators);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 134);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Name translator";
            // 
            // btnRemoveTranslator
            // 
            this.btnRemoveTranslator.Location = new System.Drawing.Point(364, 14);
            this.btnRemoveTranslator.Name = "btnRemoveTranslator";
            this.btnRemoveTranslator.Size = new System.Drawing.Size(55, 23);
            this.btnRemoveTranslator.TabIndex = 11;
            this.btnRemoveTranslator.Text = "Remove";
            this.btnRemoveTranslator.UseVisualStyleBackColor = true;
            this.btnRemoveTranslator.Click += new System.EventHandler(this.btnRemoveTranslator_Click);
            // 
            // btnAddTranslator
            // 
            this.btnAddTranslator.Location = new System.Drawing.Point(303, 14);
            this.btnAddTranslator.Name = "btnAddTranslator";
            this.btnAddTranslator.Size = new System.Drawing.Size(55, 23);
            this.btnAddTranslator.TabIndex = 10;
            this.btnAddTranslator.Text = "Add";
            this.btnAddTranslator.UseVisualStyleBackColor = true;
            this.btnAddTranslator.Click += new System.EventHandler(this.btnAddTranslator_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Replace";
            // 
            // txtTranslatorWith
            // 
            this.txtTranslatorWith.Location = new System.Drawing.Point(197, 16);
            this.txtTranslatorWith.Name = "txtTranslatorWith";
            this.txtTranslatorWith.Size = new System.Drawing.Size(100, 20);
            this.txtTranslatorWith.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "with";
            // 
            // txtTranslatorReplace
            // 
            this.txtTranslatorReplace.Location = new System.Drawing.Point(59, 16);
            this.txtTranslatorReplace.Name = "txtTranslatorReplace";
            this.txtTranslatorReplace.Size = new System.Drawing.Size(100, 20);
            this.txtTranslatorReplace.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numUpDownSimConnections);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numUpDownScanInterval);
            this.groupBox2.Controls.Add(this.chkBoxRunAtStartup);
            this.groupBox2.Location = new System.Drawing.Point(12, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Run on startup:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Max. number of simultaneous connections:";
            // 
            // numUpDownSimConnections
            // 
            this.numUpDownSimConnections.Location = new System.Drawing.Point(223, 43);
            this.numUpDownSimConnections.Maximum = new decimal(new int[] {
                20,
                0,
                0,
                0});
            this.numUpDownSimConnections.Minimum = new decimal(new int[] {
                1,
                0,
                0,
                0});
            this.numUpDownSimConnections.Name = "numUpDownSimConnections";
            this.numUpDownSimConnections.ReadOnly = true;
            this.numUpDownSimConnections.Size = new System.Drawing.Size(36, 20);
            this.numUpDownSimConnections.TabIndex = 4;
            this.numUpDownSimConnections.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownSimConnections.Value = new decimal(new int[] {
                1,
                0,
                0,
                0});
            // 
            // ProgramSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(453, 283);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProgramSettings";
            this.ShowInTaskbar = false;
            this.Text = "ProgramSettings";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownScanInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSimConnections)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Collections.Generic.Dictionary<string, string> _tempTranslators;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUpDownScanInterval;
        private System.Windows.Forms.CheckBox chkBoxRunAtStartup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbTranslators;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveTranslator;
        private System.Windows.Forms.Button btnAddTranslator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTranslatorWith;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTranslatorReplace;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUpDownSimConnections;
        #endregion
    }
}