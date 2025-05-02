using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combattentii_Mazzoleni
{
    internal class Arena
    {
        private Giocatore vincitore = null;
        private object lockObj = new object();
        public Semaphore accessi = new Semaphore(1, 1);
       

        public void EntraNellArena(Giocatore nuovoGiocatore)
        {
            accessi.WaitOne();

            lock (lockObj)
            {
                if (vincitore == null)
                {
                    vincitore = nuovoGiocatore;
                    Console.WriteLine($"{vincitore.Nome} è in attesa di uno sfidante.");
                    accessi.Release();
                    return;
                }
            }

            
            Console.WriteLine("\n Statistiche: \n");
            MostraStatistiche(vincitore);
            MostraStatistiche(nuovoGiocatore);
            Console.WriteLine("-------------------------------------\n");

            Console.WriteLine($"Combattimento iniziato: {vincitore.Nome} vs {nuovoGiocatore.Nome}");
            Thread.Sleep(1000);

            while (vincitore.PuntiFeritaIniziali > 0 && nuovoGiocatore.PuntiFeritaIniziali > 0)
            {
                vincitore.Difendi(nuovoGiocatore);
                if (vincitore.PuntiFeritaIniziali > 0)
                    nuovoGiocatore.Difendi(vincitore);
            }

            lock (lockObj)
            {
                if (vincitore.PuntiFeritaIniziali > 0)
                {
                    Console.WriteLine($"{vincitore.Nome} ha vinto e aspetta il prossimo sfidante!");
                    vincitore.Ripristino();
                }
                else
                {
                    Console.WriteLine($"{nuovoGiocatore.Nome} ha vinto e aspetta il prossimo sfidante!");
                    vincitore = nuovoGiocatore;
                    vincitore.Ripristino();
                }
            }

            accessi.Release();
        }

        private void MostraStatistiche(Giocatore g)
        {
            Console.WriteLine($"{g.Nome} - Punti Ferita: {g.PuntiFeritaIniziali}, Attacco: {g.DannoAttacco}, Difesa: {g.ProbabibilitàDifesa}%");
        }
    }

}
