namespace TermoApp
{
    partial class ConfigForm
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
            sldVolume = new TrackBar();
            lblVolume = new Label();
            numVolume = new NumericUpDown();
            lblMute = new Label();
            chkBoxMutado = new CheckBox();
            lblContraste = new Label();
            chkBoxContraste = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)sldVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVolume).BeginInit();
            SuspendLayout();
            // 
            // sldVolume
            // 
            sldVolume.Location = new Point(42, 137);
            sldVolume.Name = "sldVolume";
            sldVolume.Size = new Size(332, 56);
            sldVolume.TabIndex = 0;
            sldVolume.Scroll += sldVolume_Scroll;
            // 
            // lblVolume
            // 
            lblVolume.AutoSize = true;
            lblVolume.Font = new Font("JetBrains Mono Medium", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVolume.ForeColor = Color.White;
            lblVolume.Location = new Point(82, 74);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(283, 30);
            lblVolume.TabIndex = 1;
            lblVolume.Text = "Volume dos efeitos";
            // 
            // numVolume
            // 
            numVolume.Font = new Font("JetBrains Mono Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numVolume.Location = new Point(380, 137);
            numVolume.Name = "numVolume";
            numVolume.Size = new Size(55, 27);
            numVolume.TabIndex = 2;
            numVolume.TextAlign = HorizontalAlignment.Right;
            numVolume.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblMute
            // 
            lblMute.AutoSize = true;
            lblMute.Font = new Font("JetBrains Mono Medium", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMute.ForeColor = Color.White;
            lblMute.Location = new Point(82, 201);
            lblMute.Name = "lblMute";
            lblMute.Size = new Size(253, 30);
            lblMute.TabIndex = 3;
            lblMute.Text = "Silenciar volume";
            // 
            // chkBoxMutado
            // 
            chkBoxMutado.AutoSize = true;
            chkBoxMutado.Location = new Point(356, 211);
            chkBoxMutado.Name = "chkBoxMutado";
            chkBoxMutado.Size = new Size(18, 17);
            chkBoxMutado.TabIndex = 4;
            chkBoxMutado.UseVisualStyleBackColor = true;
            chkBoxMutado.CheckedChanged += chkBoxMutado_CheckedChanged;
            // 
            // lblContraste
            // 
            lblContraste.AutoSize = true;
            lblContraste.Font = new Font("JetBrains Mono Medium", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContraste.ForeColor = Color.White;
            lblContraste.Location = new Point(82, 291);
            lblContraste.Name = "lblContraste";
            lblContraste.Size = new Size(223, 30);
            lblContraste.TabIndex = 5;
            lblContraste.Text = "Contraste alto";
            // 
            // chkBoxContraste
            // 
            chkBoxContraste.AutoSize = true;
            chkBoxContraste.Location = new Point(356, 301);
            chkBoxContraste.Name = "chkBoxContraste";
            chkBoxContraste.Size = new Size(18, 17);
            chkBoxContraste.TabIndex = 6;
            chkBoxContraste.UseVisualStyleBackColor = true;
            chkBoxContraste.CheckedChanged += chkBoxContraste_CheckedChanged;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(483, 382);
            Controls.Add(chkBoxContraste);
            Controls.Add(lblContraste);
            Controls.Add(chkBoxMutado);
            Controls.Add(lblMute);
            Controls.Add(numVolume);
            Controls.Add(lblVolume);
            Controls.Add(sldVolume);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ConfigForm";
            StartPosition = FormStartPosition.Manual;
            Text = "ConfigFom";
            ((System.ComponentModel.ISupportInitialize)sldVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar sldVolume;
        private Label lblVolume;
        private NumericUpDown numVolume;
        private Label lblMute;
        private CheckBox chkBoxMutado;
        private Label lblContraste;
        private CheckBox chkBoxContraste;
    }
}