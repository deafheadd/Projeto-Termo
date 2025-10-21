using System;
using System.Windows.Forms;
using TermoApp;

namespace TermoApp
{
    public partial class ConfigForm : Form
    {
        private AppTermo jogoPrincipal;

        public ConfigForm(AppTermo app)
        {
            InitializeComponent();
            jogoPrincipal = app;
            InicializaParametros();
        }
        public ConfigForm()
        {
            InitializeComponent();
            InicializaParametros();
        }

        private void InicializaParametros()
        {
            sldVolume.Minimum = 0;
            sldVolume.Maximum = 100;
            sldVolume.Value = (int)(AudioConfig.VolumeAtual * 100);
            sldVolume.TickFrequency = 10;
            sldVolume.Scroll += sldVolume_Scroll;

            numVolume.Minimum = 0;
            numVolume.Maximum = 100;
            numVolume.Value = sldVolume.Value;
            numVolume.ValueChanged += numVolume_ValueChanged;

            chkBoxMutado.Checked = AudioConfig.Mutado;
            chkBoxContraste.Checked = AppConfig.AltoContraste;
        }

        private void sldVolume_Scroll(object sender, EventArgs e)
        {
            AudioConfig.VolumeAtual = sldVolume.Value / 100f;
            numVolume.Value = sldVolume.Value; // sincroniza com o numUpDown
        }

        private void numVolume_ValueChanged(object sender, EventArgs e)
        {
            sldVolume.Value = (int)numVolume.Value; // sincroniza com o slider
            AudioConfig.VolumeAtual = (float)numVolume.Value / 100f;
        }

        private void chkBoxMutado_CheckedChanged(object sender, EventArgs e) => AudioConfig.Mutado = chkBoxMutado.Checked;

        private void chkBoxContraste_CheckedChanged(object sender, EventArgs e)
        {
            AppConfig.AltoContraste = chkBoxContraste.Checked;
            jogoPrincipal.AtualizaTema();
        }
    }
}
