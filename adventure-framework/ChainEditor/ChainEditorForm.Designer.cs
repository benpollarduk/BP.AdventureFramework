namespace ChainEditor
{
    partial class ChainEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChainEditorForm));
            this.chainDataListBox = new System.Windows.Forms.ListBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backToBackChainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backToBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.frequencyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ionianScaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choromaticScaleCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzIncrementsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playFromCurrentPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.stepBackwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepFowardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addButton = new System.Windows.Forms.Button();
            this.noteLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.noteComboBox = new System.Windows.Forms.ComboBox();
            this.durationComboBox = new System.Windows.Forms.ComboBox();
            this.testButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.durationStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.durationMsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.msStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ofLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.totaCountlLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // chainDataListBox
            // 
            this.chainDataListBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chainDataListBox.FormattingEnabled = true;
            this.chainDataListBox.ItemHeight = 14;
            this.chainDataListBox.Location = new System.Drawing.Point(12, 55);
            this.chainDataListBox.Name = "chainDataListBox";
            this.chainDataListBox.Size = new System.Drawing.Size(337, 116);
            this.chainDataListBox.TabIndex = 0;
            this.chainDataListBox.SelectedIndexChanged += new System.EventHandler(this.chainDataListBox_SelectedIndexChanged);
            this.chainDataListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chainDataListBox_KeyDown);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.backToBackChainMenuItem,
            this.testToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(357, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.importToolStripMenuItem.Text = "Import Binary...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exportToolStripMenuItem.Text = "Export Binary...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(197, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // backToBackChainMenuItem
            // 
            this.backToBackChainMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invertToolStripMenuItem,
            this.backToBackToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripMenuItem2,
            this.frequencyMenuItem});
            this.backToBackChainMenuItem.Name = "backToBackChainMenuItem";
            this.backToBackChainMenuItem.Size = new System.Drawing.Size(37, 20);
            this.backToBackChainMenuItem.Text = "&Edit";
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.invertToolStripMenuItem.Text = "Invert Chain";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // backToBackToolStripMenuItem
            // 
            this.backToBackToolStripMenuItem.Name = "backToBackToolStripMenuItem";
            this.backToBackToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.backToBackToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.backToBackToolStripMenuItem.Text = "Add Inversion Of Current To End";
            this.backToBackToolStripMenuItem.Click += new System.EventHandler(this.backToBackToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.clearToolStripMenuItem.Text = "Clear Chain";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(278, 6);
            // 
            // frequencyMenuItem
            // 
            this.frequencyMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ionianScaleMenuItem,
            this.choromaticScaleCToolStripMenuItem,
            this.hzIncrementsMenuItem});
            this.frequencyMenuItem.Name = "frequencyMenuItem";
            this.frequencyMenuItem.Size = new System.Drawing.Size(281, 22);
            this.frequencyMenuItem.Text = "Frequencies...";
            // 
            // ionianScaleMenuItem
            // 
            this.ionianScaleMenuItem.Checked = true;
            this.ionianScaleMenuItem.CheckOnClick = true;
            this.ionianScaleMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ionianScaleMenuItem.Name = "ionianScaleMenuItem";
            this.ionianScaleMenuItem.Size = new System.Drawing.Size(185, 22);
            this.ionianScaleMenuItem.Text = "Ionian Scale (C)";
            this.ionianScaleMenuItem.Click += new System.EventHandler(this.ionianScaleMenuItem_Click);
            // 
            // choromaticScaleCToolStripMenuItem
            // 
            this.choromaticScaleCToolStripMenuItem.CheckOnClick = true;
            this.choromaticScaleCToolStripMenuItem.Name = "choromaticScaleCToolStripMenuItem";
            this.choromaticScaleCToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.choromaticScaleCToolStripMenuItem.Text = "Choromatic Scale (C)";
            this.choromaticScaleCToolStripMenuItem.Click += new System.EventHandler(this.choromaticScaleCToolStripMenuItem_Click);
            // 
            // hzIncrementsMenuItem
            // 
            this.hzIncrementsMenuItem.Name = "hzIncrementsMenuItem";
            this.hzIncrementsMenuItem.Size = new System.Drawing.Size(185, 22);
            this.hzIncrementsMenuItem.Text = "50Hz Increments";
            this.hzIncrementsMenuItem.Click += new System.EventHandler(this.htZIncrementsMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.playFromCurrentPositionToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem4,
            this.stepBackwardMenuItem,
            this.stepFowardMenuItem,
            this.toolStripMenuItem3,
            this.topToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.playToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.playToolStripMenuItem.Text = "Play All";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // playFromCurrentPositionToolStripMenuItem
            // 
            this.playFromCurrentPositionToolStripMenuItem.Name = "playFromCurrentPositionToolStripMenuItem";
            this.playFromCurrentPositionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.playFromCurrentPositionToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.playFromCurrentPositionToolStripMenuItem.Text = "Continue";
            this.playFromCurrentPositionToolStripMenuItem.Click += new System.EventHandler(this.playFromCurrentPositionToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(176, 6);
            // 
            // stepBackwardMenuItem
            // 
            this.stepBackwardMenuItem.Name = "stepBackwardMenuItem";
            this.stepBackwardMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.stepBackwardMenuItem.Size = new System.Drawing.Size(179, 22);
            this.stepBackwardMenuItem.Text = "Step Backward";
            this.stepBackwardMenuItem.Click += new System.EventHandler(this.stepBackwardMenuItem_Click);
            // 
            // stepFowardMenuItem
            // 
            this.stepFowardMenuItem.Name = "stepFowardMenuItem";
            this.stepFowardMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.stepFowardMenuItem.Size = new System.Drawing.Size(179, 22);
            this.stepFowardMenuItem.Text = "Step Forward";
            this.stepFowardMenuItem.Click += new System.EventHandler(this.stepFowardMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(176, 6);
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.topToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.topToolStripMenuItem.Text = "Return To Start";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.topToolStripMenuItem_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(286, 27);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(63, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // noteLabel
            // 
            this.noteLabel.AutoSize = true;
            this.noteLabel.Location = new System.Drawing.Point(12, 32);
            this.noteLabel.Name = "noteLabel";
            this.noteLabel.Size = new System.Drawing.Size(60, 13);
            this.noteLabel.TabIndex = 5;
            this.noteLabel.Text = "Frequency:";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(143, 32);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(50, 13);
            this.durationLabel.TabIndex = 6;
            this.durationLabel.Text = "Duration:";
            // 
            // noteComboBox
            // 
            this.noteComboBox.FormattingEnabled = true;
            this.noteComboBox.Items.AddRange(new object[] {
            "0",
            "100",
            "1000",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900"});
            this.noteComboBox.Location = new System.Drawing.Point(78, 29);
            this.noteComboBox.Name = "noteComboBox";
            this.noteComboBox.Size = new System.Drawing.Size(59, 21);
            this.noteComboBox.TabIndex = 7;
            // 
            // durationComboBox
            // 
            this.durationComboBox.FormattingEnabled = true;
            this.durationComboBox.Items.AddRange(new object[] {
            "10",
            "25",
            "50",
            "75",
            "100",
            "125",
            "150",
            "175",
            "200",
            "225",
            "250",
            "275",
            "300",
            "325",
            "350",
            "375",
            "400",
            "425",
            "450",
            "475",
            "500",
            "525",
            "550",
            "575",
            "600",
            "625",
            "650",
            "675",
            "700",
            "725",
            "750",
            "775",
            "800",
            "825",
            "850",
            "875",
            "900",
            "925",
            "950",
            "975",
            "1000",
            "1500",
            "2000",
            "3000",
            "4000",
            "5000"});
            this.durationComboBox.Location = new System.Drawing.Point(199, 29);
            this.durationComboBox.Name = "durationComboBox";
            this.durationComboBox.Size = new System.Drawing.Size(59, 21);
            this.durationComboBox.TabIndex = 8;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(264, 27);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(16, 23);
            this.testButton.TabIndex = 12;
            this.testButton.Text = "?";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.durationStatusLabel,
            this.durationMsLabel,
            this.msStatusLabel,
            this.currentTitleLabel,
            this.currentLabel,
            this.ofLabel,
            this.totaCountlLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 182);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(357, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip1";
            // 
            // durationStatusLabel
            // 
            this.durationStatusLabel.Name = "durationStatusLabel";
            this.durationStatusLabel.Size = new System.Drawing.Size(52, 17);
            this.durationStatusLabel.Text = "Duration:";
            // 
            // durationMsLabel
            // 
            this.durationMsLabel.Name = "durationMsLabel";
            this.durationMsLabel.Size = new System.Drawing.Size(13, 17);
            this.durationMsLabel.Text = "0";
            // 
            // msStatusLabel
            // 
            this.msStatusLabel.Name = "msStatusLabel";
            this.msStatusLabel.Size = new System.Drawing.Size(20, 17);
            this.msStatusLabel.Text = "ms";
            // 
            // currentTitleLabel
            // 
            this.currentTitleLabel.Name = "currentTitleLabel";
            this.currentTitleLabel.Size = new System.Drawing.Size(48, 17);
            this.currentTitleLabel.Text = "Current:";
            // 
            // currentLabel
            // 
            this.currentLabel.Name = "currentLabel";
            this.currentLabel.Size = new System.Drawing.Size(13, 17);
            this.currentLabel.Text = "0";
            // 
            // ofLabel
            // 
            this.ofLabel.Name = "ofLabel";
            this.ofLabel.Size = new System.Drawing.Size(17, 17);
            this.ofLabel.Text = "of";
            // 
            // totaCountlLabel
            // 
            this.totaCountlLabel.Name = "totaCountlLabel";
            this.totaCountlLabel.Size = new System.Drawing.Size(13, 17);
            this.totaCountlLabel.Text = "0";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ChainEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 204);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.durationComboBox);
            this.Controls.Add(this.noteComboBox);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.noteLabel);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.chainDataListBox);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "ChainEditorForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chain Editor";
            this.Load += new System.EventHandler(this.ChainEditorForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox chainDataListBox;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.ComboBox noteComboBox;
        private System.Windows.Forms.ComboBox durationComboBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel durationStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel durationMsLabel;
        private System.Windows.Forms.ToolStripStatusLabel msStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentLabel;
        private System.Windows.Forms.ToolStripStatusLabel ofLabel;
        private System.Windows.Forms.ToolStripStatusLabel totaCountlLabel;
        private System.Windows.Forms.ToolStripMenuItem backToBackChainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem frequencyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ionianScaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choromaticScaleCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hzIncrementsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playFromCurrentPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem stepFowardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepBackwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backToBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

