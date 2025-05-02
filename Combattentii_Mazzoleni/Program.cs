using Combattentii_Mazzoleni;

internal class Program
{
    static void Main(string[] args)
    {
        Arena campo = new Arena();
        List<Thread> threads = new List<Thread>();

        Console.WriteLine("=== INIZIO TORNEO ===");

        for (int i = 0; i < 10; i++)
        {
            Giocatore g = new Giocatore("Combattente" + (i + 1));
            Thread t = new Thread(() => g.Lotta(campo));
            threads.Add(t);
            t.Start();
        }

        foreach (Thread t in threads)
        {
            t.Join();
        }

       
        Console.WriteLine("\n=== TORNEO TERMINATO ===");
    }
}

