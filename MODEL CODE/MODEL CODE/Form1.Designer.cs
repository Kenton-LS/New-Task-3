namespace MODEL_CODE
{
    partial class frm1
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
            this.lblMap = new System.Windows.Forms.Label();
            this.rtbUnitInfo = new System.Windows.Forms.RichTextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblRound = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.rtbUnitInfo2 = new System.Windows.Forms.RichTextBox();
            this.lblBuildings = new System.Windows.Forms.Label();
            this.lblUnits = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMap
            // 
            this.lblMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMap.Location = new System.Drawing.Point(315, 23);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(401, 364);
            this.lblMap.TabIndex = 0;
            this.lblMap.Click += new System.EventHandler(this.lblMap_Click);
            // 
            // rtbUnitInfo
            // 
            this.rtbUnitInfo.Location = new System.Drawing.Point(722, 7);
            this.rtbUnitInfo.Name = "rtbUnitInfo";
            this.rtbUnitInfo.Size = new System.Drawing.Size(341, 397);
            this.rtbUnitInfo.TabIndex = 1;
            this.rtbUnitInfo.Text = " ";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(222, 410);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(597, 63);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "START / PAUSE";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblRound
            // 
            this.lblRound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRound.Location = new System.Drawing.Point(12, 440);
            this.lblRound.Name = "lblRound";
            this.lblRound.Size = new System.Drawing.Size(204, 23);
            this.lblRound.TabIndex = 4;
            this.lblRound.Text = "ROUND: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(825, 440);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(944, 440);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(119, 23);
            this.btnRead.TabIndex = 6;
            this.btnRead.Text = "LOAD";
            this.btnRead.UseVisualStyleBackColor = true;
            // 
            // rtbUnitInfo2
            // 
            this.rtbUnitInfo2.Location = new System.Drawing.Point(12, 7);
            this.rtbUnitInfo2.Name = "rtbUnitInfo2";
            this.rtbUnitInfo2.Size = new System.Drawing.Size(297, 397);
            this.rtbUnitInfo2.TabIndex = 7;
            this.rtbUnitInfo2.Text = "";
            // 
            // lblBuildings
            // 
            this.lblBuildings.Location = new System.Drawing.Point(12, 410);
            this.lblBuildings.Name = "lblBuildings";
            this.lblBuildings.Size = new System.Drawing.Size(204, 23);
            this.lblBuildings.TabIndex = 8;
            this.lblBuildings.Text = "BUILDINGS";
            // 
            // lblUnits
            // 
            this.lblUnits.Location = new System.Drawing.Point(825, 407);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(201, 23);
            this.lblUnits.TabIndex = 9;
            this.lblUnits.Text = "UNITS";
            // 
            // frm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 485);
            this.Controls.Add(this.lblUnits);
            this.Controls.Add(this.lblBuildings);
            this.Controls.Add(this.rtbUnitInfo2);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblRound);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.rtbUnitInfo);
            this.Controls.Add(this.lblMap);
            this.Name = "frm1";
            this.Text = "THE WAR FOR BOEREWORS";
            this.Load += new System.EventHandler(this.frm1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.RichTextBox rtbUnitInfo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.RichTextBox rtbUnitInfo2;
        private System.Windows.Forms.Label lblBuildings;
        private System.Windows.Forms.Label lblUnits;
    }
}

