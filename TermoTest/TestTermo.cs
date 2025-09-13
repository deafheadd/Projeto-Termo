using System.Diagnostics;
using TermoAula;

namespace TermoTest
{
    [TestClass]
    public sealed class TestTermo
    {
        [TestMethod]
        public void TestReadFile()
        {
            Termo termo = new Termo();
            termo.CarregaPalavras("palavras.txt");
            Console.WriteLine(String.Join("\n", termo.palavras));
        }

        [TestMethod]
        public void TestJogo()
        {
            Termo termo = new Termo();
            ImprimirJogo(termo);
            termo.ChecaPalavra("APOIO");
            ImprimirJogo(termo);
        }

        public void ImprimirJogo(Termo termo)
        {
            Console.WriteLine("Palavra sorteada: " + termo.palavraSorteada);

            Console.WriteLine("TABULEIRO");
            foreach (var palavra in termo.tabuleiro)
            {
                foreach (var tecla in palavra)
                {
                    Console.Write(tecla.Caracter + ": " + tecla.Cor + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");

            Console.WriteLine("TECLADO");
            foreach(var tecla in termo.teclado)
            {
                Console.Write(tecla.Key + ": " + tecla.Value + " | ");
            }
            Console.WriteLine();
            Console.WriteLine("");
        }
    }
}
