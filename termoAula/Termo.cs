﻿namespace TermoLib
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
            CarregaPalavras("palavras.txt");
            SorteiaPalavra();
            palavraAtual = 1;
            tabuleiro = new List<List<Letra>>();
            teclado = new Dictionary<char, char>();
            for (int i = 65; i <= 90; i++)
            {
                teclado.Add((char)i, 'c');
            }
        }

        public void CarregaPalavras(string fileName)
        {
            palavras = File.ReadAllLines(fileName).ToList();
        }

        public void SorteiaPalavra()
        {
            Random rdn = new Random();
            var index = rdn.Next(0, palavras.Count() - 1);
            palavraSorteada = palavras[index];
        }

        public void ChecaPalavra(string palavra)
        {
            if(palavra.Length != 5)
                throw new Exception("Palavra com tamanho incorreto.");

            var palavraTabuleiro = new List<Letra>();
            char cor;
            for (int i = 0; i < palavra.Length; i++)
            {
                if (palavra[i] == palavraSorteada[i])
                {
                    cor = 'V';
                }
                else if (palavraSorteada.Contains(palavra[i]))
                {
                    cor = 'A';
                }
                else
                {
                    cor = 'P';
                }
                palavraTabuleiro.Add(new Letra(palavra[i], cor));
                teclado[palavra[i]] = cor;
            }
            tabuleiro.Add(palavraTabuleiro);
            palavraAtual++;
        }
    }
}
