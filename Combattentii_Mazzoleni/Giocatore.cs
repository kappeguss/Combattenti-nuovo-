using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combattentii_Mazzoleni
{
    internal class Giocatore
    {
        public int PuntiFerita { get; private set; }
        public int PuntiFeritaIniziali { get; protected set; }
        public int DannoAttacco { get; private set; }
        public int ProbabibilitàDifesa { get; private set; }
        public string Nome { get; private set; }

        private Random rnd = new Random();

        public Giocatore(string nome)
        {
            Nome = nome;
            PuntiFerita = rnd.Next(3000, 8000);
            PuntiFeritaIniziali = PuntiFerita;
            DannoAttacco = rnd.Next(500, 2000);
            ProbabibilitàDifesa = rnd.Next(1, 101);
        }

        public void Lotta(Arena arena)
        {
            Console.WriteLine($"{Nome} è pronto a combattere.");
            arena.EntraNellArena(this);
        }

        public void Difendi(Giocatore attaccante)
        {
            int danno;

            if (attaccante.Attacca() <= ProbabibilitàDifesa)
            {
                danno = attaccante.DannoAttacco - (attaccante.DannoAttacco * rnd.Next(50, 101) / 100);
            }
            else
            {
                danno = attaccante.DannoAttacco;
            }

            PuntiFeritaIniziali -= danno;
            Console.WriteLine($"{attaccante.Nome} ha inflitto {danno} danni a {Nome}. Punti ferita rimanenti: {PuntiFeritaIniziali}");

            if (PuntiFeritaIniziali <= 0)
            {
                Console.WriteLine($"{Nome} è stato sconfitto!");
            }
        }

        public int Attacca()
        {
            return rnd.Next(10, 100);
        }

        public void Ripristino()
        {
            PuntiFeritaIniziali = PuntiFerita;
        }
    }

}
