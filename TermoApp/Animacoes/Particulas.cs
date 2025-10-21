namespace TermoApp
{
    public class Particulas
    {
        // cores das particulas
        private static readonly Color[] cores = new Color[]
        {
            Color.FromName("DodgerBlue"),
            Color.FromName("OliveDrab"),
            Color.FromName("Gold"),
            Color.FromName("Pink"),
            Color.FromName("SlateBlue"),
            Color.FromName("LightBlue"),
            Color.FromName("Violet"),
            Color.FromName("PaleGreen"),
            Color.FromName("SteelBlue"),
            Color.FromName("SandyBrown"),
            Color.FromName("Chocolate"),
            Color.FromName("Crimson")
        };

        public float X, Y; // posicao atual da particula
        public float tam; // tamanho da particula (espessura da linha)
        public float tilt; // inclinacao da linha
        public float anguloTilt; 
        public float incrementoTilt; // velocidade do angulo de inclinacao
        public float velocidade; // velocidade vertical da particula (profundidade)
        public Color Cor;
        private Random rand;

        public Particulas(Random r, Size limite)
        {
            rand = r;
            Resetar(limite);
        }

        // criar nova particula
        public void Resetar(Size limite)
        {
            // cor aleatoria
            Cor = cores[rand.Next(cores.Length)];
            // posicao horizontal aleatoria
            X = (float)(rand.NextDouble() * limite.Width); 
            // posicao vertical aleatoria
            Y = (float)(rand.NextDouble() * limite.Height) - limite.Height;
            // profundidade aleatoria entre 0.3 e 0.1
            float profundidade = (float)(rand.NextDouble() * 0.7 + 0.3);
            // tamanho proporcional a profundidade
            tam = (float)(rand.NextDouble() * 10 * 5) * profundidade + 5f; // gera um valor entre 5 e 50 multiplicado pela profundidade
            // inclinacao proporcional a profundidade
            tilt = (float)(rand.NextDouble() * 10 - 10) * profundidade;
            // velocidade de inclinacao proporcional a profundidade
            incrementoTilt = (float)(rand.NextDouble() * 0.07 + 0.05) * profundidade;
            // angulo de inclinacao
            anguloTilt = 0;
            // velocidade vertical proporcional a profundidade
            velocidade = 2 * profundidade; 
        }

        // atualizar posicao da particula
        public void Atualiza(Size limite)
        {
            anguloTilt += anguloTilt;
            X += (float)Math.Sin(anguloTilt);
            Y += (float)((Math.Cos(anguloTilt) + tam + 2) * 0.5 * velocidade);

            tilt = (float)Math.Sin(anguloTilt) * 15;

            // se a particula sair da area do form, reinicia sua posicao no topo
            if (Y > limite.Height || X < -20 || X > limite.Width + 20)
            {
                Resetar(limite);
                Y = -10; // posicao acima do topo
            }
        }

        // desenhar particula na tela
        public void Desenha(Graphics g)
        {
            float x = X + anguloTilt; // posicao x considerando a inclinacao
            using (var pen = new Pen(Cor, tam)) // cor e espessura
            {
                g.DrawLine(pen, x + tam / 2, Y, x, Y + tilt + tam / 2);
            }
        }
    }
}
