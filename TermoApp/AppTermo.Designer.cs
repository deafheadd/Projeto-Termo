namespace TermoApp
{
    partial class AppTermo
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
            panelBtn = new Panel();
            labelTitulo = new Label();
            panelKeyb1 = new Panel();
            panelKeyb2 = new Panel();
            panelKeyb3 = new Panel();
            buttonDel = new Button();
            buttonEnter = new Button();
            SuspendLayout();
            // 
            // panelBtn
            // 
            panelBtn.Location = new Point(234, 161);
            panelBtn.Name = "panelBtn";
            panelBtn.Size = new Size(334, 402);
            panelBtn.TabIndex = 0;
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("JetBrains Mono Medium", 28.1999989F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.ForeColor = SystemColors.ActiveCaptionText;
            labelTitulo.Location = new Point(313, 46);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(172, 62);
            labelTitulo.TabIndex = 1;
            labelTitulo.Text = "TERMO";
            // 
            // panelKeyb1
            // 
            panelKeyb1.Location = new Point(91, 597);
            panelKeyb1.Name = "panelKeyb1";
            panelKeyb1.Size = new Size(592, 52);
            panelKeyb1.TabIndex = 2;
            // 
            // panelKeyb2
            // 
            panelKeyb2.Location = new Point(101, 655);
            panelKeyb2.Name = "panelKeyb2";
            panelKeyb2.Size = new Size(532, 52);
            panelKeyb2.TabIndex = 3;
            // 
            // panelKeyb3
            // 
            panelKeyb3.Location = new Point(121, 713);
            panelKeyb3.Name = "panelKeyb3";
            panelKeyb3.Size = new Size(412, 52);
            panelKeyb3.TabIndex = 4;
            // 
            // buttonDel
            // 
            buttonDel.BackColor = Color.LightSteelBlue;
            buttonDel.Font = new Font("JetBrains Mono Medium", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonDel.Location = new Point(651, 655);
            buttonDel.Name = "buttonDel";
            buttonDel.RightToLeft = RightToLeft.No;
            buttonDel.Size = new Size(52, 52);
            buttonDel.TabIndex = 5;
            buttonDel.Text = "⌫";
            buttonDel.UseVisualStyleBackColor = false;
            buttonDel.Click += buttonDel_Click;
            // 
            // buttonEnter
            // 
            buttonEnter.BackColor = Color.LightSteelBlue;
            buttonEnter.Font = new Font("JetBrains Mono Medium", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEnter.Location = new Point(555, 713);
            buttonEnter.Name = "buttonEnter";
            buttonEnter.RightToLeft = RightToLeft.No;
            buttonEnter.Size = new Size(102, 52);
            buttonEnter.TabIndex = 6;
            buttonEnter.Text = "ENTER";
            buttonEnter.UseVisualStyleBackColor = false;
            buttonEnter.Click += buttonEnter_Click;
            // 
            // AppTermo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(800, 843);
            Controls.Add(buttonEnter);
            Controls.Add(buttonDel);
            Controls.Add(panelKeyb3);
            Controls.Add(panelKeyb2);
            Controls.Add(panelKeyb1);
            Controls.Add(labelTitulo);
            Controls.Add(panelBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AppTermo";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBtn;
        private Label labelTitulo;
        private Panel panelKeyb1;
        private Panel panelKeyb2;
        private Panel panelKeyb3;
        private Button buttonDel;
        private Button buttonEnter;
    }
}
