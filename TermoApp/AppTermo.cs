using System.Drawing;
using Microsoft.VisualBasic.Devices;
using TermoLib;
using Font = System.Drawing.Font;

namespace TermoApp
{
    public partial class AppTermo : Form
    {
        private Termo termo;
        private string palavraAtual = "";
        private bool jogoEncerrado = false;
        private List<Button> botoesTabuleiro = new();
        private Dictionary<char, Button> botoesTeclado = new();

        public AppTermo()
        {
            InitializeComponent();
            termo = new Termo();
            CarregaTabuleiro();
            CarregaTeclado();
        }

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

        private void AdicionarLetra(char letra)
        {
            if (jogoEncerrado) return;
            if (palavraAtual.Length < 5)
            {
                palavraAtual += letra;
                AtualizarTabuleiro();
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

        private void AtualizarCorTabuleiro()
        {
            int linhaIndex = termo.tabuleiro.Count - 1;
            if (linhaIndex < 0) return;
            var linha = termo.tabuleiro[linhaIndex];
            for (int i = 0; i < linha.Count(); i++)
            {
                var btn = botoesTabuleiro[linhaIndex * 5 + i];

                switch (linha[i].Cor)
                {
                    case 'V': btn.BackColor = Color.LightGreen; break;
                    case 'A': btn.BackColor = Color.Gold; break;
                    case 'P': btn.BackColor = Color.LightGray; break;
                }
            }

            AtualizarTeclado();
        }

        private void AtualizarTeclado()
        {
            foreach (var kv in termo.teclado)
            {
                if (!botoesTeclado.ContainsKey(kv.Key)) continue;

                var b = botoesTeclado[kv.Key];
                switch (kv.Value)
                {
                    case 'V': b.BackColor = Color.LightGreen; break;
                    case 'A': b.BackColor = Color.Gold; break;
                    case 'P': b.BackColor = Color.LightGray; break;
                    default: b.BackColor = Color.LightSteelBlue; break;
                }
            }
        }


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

            termo.ChecaPalavra(palavraAtual);

            AtualizarCorTabuleiro();

            ChecaVitoria();

            palavraAtual = "";
        }

        private void ChecaVitoria()
        {
            if (termo.tabuleiro.Last().All(x => x.Cor == 'V'))
            {
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

        private void buttonDel_Click(object sender, EventArgs e)
        {
            DeletarLetra();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            EnviarPalavra();
        }
    }
}
