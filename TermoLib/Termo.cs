using System.Text;
using System.Text.RegularExpressions;

namespace TermoLib
{
    public class Letra
    {
        public Letra(char caracter, char cor)
        {
            Caracter = caracter;
            Cor = cor;
        }
        public char Caracter;
        public char Cor;
    }

    public class Termo
    {
        public List<string> palavras;
        public string palavraSorteada;
        public List<List<Letra>> tabuleiro = [];
        public Dictionary<char, char> teclado;
        public int palavraAtual;

        public Termo()
        {
            CarregaPalavras("Palavras_5_Letras_PT-BR.txt");
            SorteiaPalavra();
            palavraAtual = 1;
            tabuleiro = new List<List<Letra>>();
            teclado = new Dictionary<char, char>();
            for (int i = 65; i <= 90; i++)
            {
                teclado.Add((char)i, 'c');
            }
        }

        string RemoverAcentos(string text)
        {
            return Regex.Replace(text.Normalize(NormalizationForm.FormD), @"\p{Mn}", "");
        }

        public void CarregaPalavras(string fileName)
        {
            palavras = File.ReadAllLines(fileName).Select(p => RemoverAcentos(p.ToUpper())).ToList();
        }

        public void SorteiaPalavra()
        {
            Random rdn = new Random();
            var index = rdn.Next(0, palavras.Count() - 1);
            palavraSorteada = palavras[index];
        }

        public void ChecaPalavra(string palavra)
        {
            if (palavra.Length != 5)
                throw new Exception("Palavra com tamanho incorreto.");

            var palavraTabuleiro = new List<Letra>();
            bool[] usada = new bool[5];

            // marcar caracteres verdes (posicao correta)
            for (int i = 0; i < palavra.Length; i++)
            {
                if (palavra[i] == palavraSorteada[i])
                {
                    palavraTabuleiro.Add(new Letra(palavra[i], 'V'));
                    usada[i] = true;
                }
                else
                {
                    palavraTabuleiro.Add(new Letra(palavra[i], ' ')); // se nao for verde, ainda fica indefinido
                }
            }

            // marcar caracteres amarelos (posicao incorreta) e pretos (nao contem)
            for (int i = 0; i < palavra.Length; i++)
            {
                if (palavraTabuleiro[i].Cor == ' ')
                {
                    bool achou = false;
                    for (int j = 0; j < palavraSorteada.Length; j++)
                    {
                        if (!usada[j] && palavra[i] == palavraSorteada[j])
                        {
                            palavraTabuleiro[i].Cor = 'A';
                            usada[j] = true;
                            achou = true;
                            break;
                        }
                    }
                    if(!achou)
                    {
                        palavraTabuleiro[i].Cor = 'P';
                    }
                }
            }
            // atualiza o teclado
            foreach (var letra in palavraTabuleiro)
            {
                if (!teclado.ContainsKey(letra.Caracter)) continue;

                char corAtual = teclado[letra.Caracter];
                if (corAtual == 'V') continue; // ja eh verde, nao rebaixa
                if (corAtual == 'A' && letra.Cor == 'P') continue; // ja eh amarelo, nao rebaixa
                teclado[letra.Caracter] = letra.Cor;
            }

            tabuleiro.Add(palavraTabuleiro);
            palavraAtual++;
        }
    }
}
