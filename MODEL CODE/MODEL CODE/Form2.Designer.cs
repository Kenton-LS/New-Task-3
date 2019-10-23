namespace MODEL_CODE
{
    partial class frm2
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
            this.btnConfrim = new System.Windows.Forms.Button();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConfrim
            // 
            this.btnConfrim.CausesValidation = false;
            this.btnConfrim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfrim.Location = new System.Drawing.Point(21, 182);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Size = new System.Drawing.Size(155, 115);
            this.btnConfrim.TabIndex = 0;
            this.btnConfrim.Text = "CONFIRM MAP SIZE";
            this.btnConfrim.UseVisualStyleBackColor = true;
            this.btnConfrim.Click += new System.EventHandler(this.btnConfrim_Click);
            // 
            // lblX
            // 
            this.lblX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblX.Location = new System.Drawing.Point(21, 63);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(32, 23);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "X: ";
            // 
            // lblY
            // 
            this.lblY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblY.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY.Location = new System.Drawing.Point(21, 124);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(32, 23);
            this.lblY.TabIndex = 2;
            this.lblY.Text = "Y: ";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(76, 64);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(116, 22);
            this.txtX.TabIndex = 3;
            this.txtX.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(76, 125);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(116, 22);
            this.txtY.TabIndex = 4;
            // 
            // frm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 340);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.btnConfrim);
            this.Name = "frm2";
            this.Text = "MAP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfrim;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
    }
}