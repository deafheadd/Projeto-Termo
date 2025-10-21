using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TermoApp.Animacoes
{
    public partial class ParticulaForm : Form
    {
        private Timer timer = new Timer();
        public List<Particulas> listaParticulas = new();

        public ParticulaForm(Form parent)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Lime; // cor de transparência
            this.TransparencyKey = Color.Lime;
            this.ShowInTaskbar = false;

            this.StartPosition = FormStartPosition.Manual;

            // ClientRectangle do parent
            this.Location = parent.PointToScreen(new Point(0, 0));
            this.Size = parent.ClientSize;

            timer.Interval = 24; // 24 fps
            timer.Tick += (s, e) =>
            {
                AtualizaParticula();
                this.Invalidate();
            };
        }

        public void IniciaParticula()
        {
            Random rnd = new Random();
            listaParticulas.Clear();

            for (int i = 0; i < 100; i++)
                listaParticulas.Add(new Particulas(rnd, this.ClientSize));

            this.Show();
            timer.Start();
        }

        private void AtualizaParticula()
        {
            foreach (var p in listaParticulas)
                p.Atualiza(this.ClientSize);
        }

        public void EncerrarParticula()
        {
            timer.Stop();
            this.Hide();
            listaParticulas.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var p in listaParticulas)
                p.Desenha(e.Graphics);
        }
    }
}
