using System.Text;
using System.Text.RegularExpressions;
using NAudio.Wave;
using TermoApp.Animacoes;
using TermoLib;
using Font = System.Drawing.Font;
using Timer = System.Windows.Forms.Timer;

namespace TermoApp
{
    public partial class AppTermo : Form
    {
        private Termo termo;
        private string palavraAtual = "";
        private bool jogoEncerrado = false;
        private List<Button> botoesTabuleiro = new();
        private Dictionary<char, Button> botoesTeclado = new();

        // audio
        private IWavePlayer player;
        private AudioFileReader leitor;

        // particulas
        private List<Particulas> particula = new();
        private Timer particulaTimer = new Timer();
        private Random rand = new Random();
        private ParticulaForm particulaForm;

        public AppTermo()
        {
            InitializeComponent();
            //CarregarSom();
            termo = new Termo();
            CarregaTabuleiro();
            CarregaTeclado();
            /*
            particulaTimer.Interval = 30; // 30 fps
            particulaTimer.Tick += (s, e) => {
                AtualizaParticula();
                Invalidate(); // força redesenhar
            };
            */
            this.LocationChanged += (s, e) => AtualizarPosicaoParticula();
            this.SizeChanged += (s, e) => AtualizarPosicaoParticula();
            MessageBox.Show(termo.palavraSorteada);
        }

        // ===== INTEGRACAO DO TECLADO FISICO =====
        // sobrescreve o comportamento padrao de teclas no formulario
        // eh chamado automaticamente toda vez que uma tecla eh pressionada
        // antes que o controle com foco processe a tecla
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (jogoEncerrado) return base.ProcessCmdKey(ref msg, keyData);

            // enter
            if (keyData == Keys.Enter)
            {
                EnviarPalavra();
                return true;
            }

            // backspace
            if (keyData == Keys.Back)
            {
                DeletarLetra();
                return true;
            }

            // letras A-Z
            if (keyData >= Keys.A && keyData <= Keys.Z)
            {
                char letra = keyData.ToString()[0];
                AdicionarLetra(letra);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ===== CARREGAR ARQUIVO DE AUDIO =====
        /*
        private void CarregarSom()
        {
            try
            {
                leitor = new AudioFileReader("Efeitos Sonoros/keypresssoundeffect.wav");
                player = new WaveOutEvent();
                player.Init(leitor);
                // TESTE
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar som: " + ex.Message);
            }
        }
        */

        private void TocarSom(string arquivo)
        {
            try
            {
                var leitorTmp = new AudioFileReader(arquivo);
                // aplica volume global e mute
                leitorTmp.Volume = AudioConfig.Mutado ? 0f : AudioConfig.VolumeAtual;

                var playerTmp = new WaveOutEvent();
                playerTmp.Init(leitorTmp);
                playerTmp.Play();

                // descarta quando terminar
                playerTmp.PlaybackStopped += (s, e) =>
                {
                    playerTmp.Dispose();
                    leitorTmp.Dispose();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tocar som: " + ex.Message);
            }
        }

        public void SomForms(string arquivo)
        {
            TocarSom(arquivo);
        }

        // ===== TESTE =====
        /*
        private void CarregaParticula()
        {
            particulaTimer.Interval = 30; // 30 fps
            particulaTimer.Tick += (s, e) => {
                AtualizaParticula();
                Invalidate(); // força redesenhar
            };
        }
        */
        /*

        private void IniciaParticula()
        {
            particula.Clear();
            for (int i = 0; i < 100; i++)
                particula.Add(new Particulas(rand, ClientSize.Width, ClientSize.Height));
            particulaAtiva = true;
            particulaTimer.Start();
        }

        private void AtualizaParticula()
        {
            foreach (var p in particula)
                p.Atualiza(ClientSize.Width, ClientSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (particulaAtiva)
            {
                foreach (var p in particula)
                {
                    p.Desenha(e.Graphics);
                }
            }
        }
        */

        private void AtualizarPosicaoParticula()
        {
            if (particulaForm != null && particulaForm.Visible)
            {
                particulaForm.Location = this.PointToScreen(new Point(0, 0));
                particulaForm.Size = this.ClientSize;
            }
        }

        private void MostrarConfetti()
        {
            if (particulaForm == null)
            {
                particulaForm = new ParticulaForm(this)
                {
                    Owner = this // garante que o confetti acompanha o form principal
                };
            }

            // atualiza tamanho e posição antes de iniciar
            particulaForm.Location = this.PointToScreen(new Point(0, 0));
            particulaForm.Size = this.ClientSize;

            particulaForm.IniciaParticula();
        }


        // ===== CRIACAO DO TABULEIRO E TECLADO =====
        private void CarregaTabuleiro()
        {
            int tamanho = 60;
            int espaco = 8;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button btn = new Button();
                    btn.Width = tamanho;
                    btn.Height = tamanho;
                    btn.Font = new Font("JetBrains Mono Medium", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    btn.BackColor = Color.LightSteelBlue;
                    btn.Location = new Point(
                        j * (tamanho + espaco),
                        i * (tamanho + espaco)
                    );
                    panelBtn.Controls.Add(btn);
                    botoesTabuleiro.Add(btn);
                }
            }
        }

        private void CarregaTeclado()
        {
            string linha1 = "QWERTYUIOP";
            string linha2 = "ASDFGHJKL";
            string linha3 = "ZXCVBNM";

            CriarLinhaTeclado(panelKeyb1, linha1, 50, 10);
            CriarLinhaTeclado(panelKeyb2, linha2, 50, 10);
            CriarLinhaTeclado(panelKeyb3, linha3, 50, 10);
        }

        private void CriarLinhaTeclado(Panel panel, string letras, int tamanho, int espaco)
        {
            for (int j = 0; j < letras.Length; j++)
            {
                char letra = letras[j];
                Button keyboard = CriarBotao(tamanho, tamanho);
                keyboard.Location = new Point(j * (tamanho + espaco), 0);
                keyboard.Text = letra.ToString();
                keyboard.Font = new Font("JetBrains Mono Medium", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                keyboard.Click += (s, e) => AdicionarLetra(letra);
                panel.Controls.Add(keyboard);
                botoesTeclado[letra] = keyboard;
            }
        }
        private Button CriarBotao(int largura, int altura)
        {
            return new Button
            {
                Width = largura,
                Height = altura,
                BackColor = Color.LightSteelBlue
            };
        }

        // ===== ADICIONAR LETRA AO TABULEIRO =====
        private void AdicionarLetra(char letra)
        {
            if (jogoEncerrado) return;
            if (palavraAtual.Length < 5)
            {
                palavraAtual += letra;
                AtualizarTabuleiro();

                // tocar som ao digitar
                TocarSom("Efeitos Sonoros/keypresssoundeffect.wav");
            }
        }

        private void AtualizarTabuleiro()
        {
            int linhaIndex = termo.palavraAtual - 1;
            if (linhaIndex >= 6) return;
            for (int i = 0; i < palavraAtual.Length; i++)
            {
                var btn = botoesTabuleiro[linhaIndex * 5 + i];
                btn.Text = palavraAtual[i].ToString();
            }
        }

        // ===== ATUALIZAR COR =====
        private void AtualizarCorTabuleiro()
        {
            var corV = AppConfig.AltoContraste ? Color.Lime : Color.LightGreen;
            var corA = AppConfig.AltoContraste ? Color.Yellow : Color.Gold;
            var corP = AppConfig.AltoContraste ? Color.White : Color.LightGray;

            for (int index = 0; index < termo.tabuleiro.Count; index++)
            {
                var linha = termo.tabuleiro[index];

                for (int i = 0; i < linha.Count(); i++)
                {
                    var btn = botoesTabuleiro[index * 5 + i];

                    switch (linha[i].Cor)
                    {
                        case 'V': btn.BackColor = corV; break;
                        case 'A': btn.BackColor = corA; break;
                        case 'P': btn.BackColor = corP; break;
                    }
                }
            }
            AtualizarTeclado();
        }

        private void AtualizarTeclado()
        {
            var corV = AppConfig.AltoContraste ? Color.Lime : Color.LightGreen;
            var corA = AppConfig.AltoContraste ? Color.Yellow : Color.Gold;
            var corP = AppConfig.AltoContraste ? Color.White : Color.LightGray;

            foreach (var kv in termo.teclado)
            {
                if (!botoesTeclado.ContainsKey(kv.Key)) continue;

                var b = botoesTeclado[kv.Key];
                switch (kv.Value)
                {
                    case 'V': b.BackColor = corV; break;
                    case 'A': b.BackColor = corA; break;
                    case 'P': b.BackColor = corP; break;
                    default: b.BackColor = Color.LightSteelBlue; break;
                }
            }
        }

        public void AtualizaTema()
        {
            AtualizarCorTabuleiro();
            AtualizarTabuleiro();
        }

        // ===== REMOVER ACENTOS =====
        string RemoverAcentos(string text)
        {
            return Regex.Replace(text.Normalize(NormalizationForm.FormD), @"\p{Mn}", "");
        }

        // ===== DELETAR LETRA E ENVIAR PALAVRA =====
        private void DeletarLetra()
        {
            if (jogoEncerrado) return;
            if (palavraAtual.Length > 0)
            {
                palavraAtual = palavraAtual.Substring(0, palavraAtual.Length - 1);

                int linhaIndex = termo.palavraAtual - 1;
                int pos = palavraAtual.Length;

                var btn = botoesTabuleiro[linhaIndex * 5 + pos];
                btn.Text = "";

                // tocar som ao digitar
                TocarSom("Efeitos Sonoros/keypresssoundeffect.wav");
            }
        }

        private void EnviarPalavra()
        {
            if (jogoEncerrado) return;
            if (palavraAtual.Length < 5)
            {
                MessageBox.Show("Palavra incompleta.");
                return;
            }

            // remove acentos da palavra digitada
            string palavraFormatada = RemoverAcentos(palavraAtual.ToLower());

            // verifica se a palavra esta contida no .txt
            bool existe = termo.palavras.Any(p => RemoverAcentos(p.ToLower()) == palavraFormatada);

            if (!existe)
            {
                MessageBox.Show("Palavra inválida.");
                return;
            }

            termo.ChecaPalavra(palavraAtual);

            AtualizarCorTabuleiro();

            ChecaVitoria();

            palavraAtual = "";
        }

        private void buttonDel_Click(object sender, EventArgs e) => DeletarLetra();

        private void buttonEnter_Click(object sender, EventArgs e) => EnviarPalavra();

        // ===== CHECAR CONDICAO DE VITORIA =====
        private void ChecaVitoria()
        {
            if (termo.tabuleiro.Last().All(x => x.Cor == 'V'))
            {
                MostrarConfetti();
                MessageBox.Show("Parabéns, você ganhou!");
                jogoEncerrado = true;
                return;
            }

            if (termo.tabuleiro.Count >= 6)
            {
                MessageBox.Show("Fim de jogo! A palavra era: " + termo.palavraSorteada);
                jogoEncerrado = true;
                return;
            }
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            TocarSom("Efeitos Sonoros/mouseclick.wav");
            ConfigForm config = new ConfigForm(this);
            // centraliza AudioConfig em relação ao form AppTermo
            config.Location = new Point(
                this.Location.X + (this.Width - config.Width) / 2,
                this.Location.Y + (this.Height - config.Height) / 2
            );
            config.Show();
        }

        // ===== REINICIAR =====
        private void Reiniciar()
        {
            palavraAtual = "";
            jogoEncerrado = false;

            termo = new Termo();

            foreach (var btn in botoesTabuleiro)
            {
                btn.Text = "";
                btn.BackColor = Color.LightSteelBlue;
            }

            AtualizarCorTabuleiro();

            foreach (var kv in botoesTeclado)
                kv.Value.BackColor = Color.LightSteelBlue;

            if (particulaForm != null)
            {
                particulaForm.Hide();
                particulaForm.listaParticulas.Clear();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            TocarSom("Efeitos Sonoros/mouseclick.wav");
            Reiniciar();
        }
    }
}
