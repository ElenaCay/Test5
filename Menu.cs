using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test5
{
    class Menu
    {
        public static void Start()
        {
            Console.WriteLine("Benvenuto, gestisci un magazzino!");

            char choice;
            const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Magazzino;Trusted_Connection=True;";
            Connessione cn = new(connectionString);
            cn.Init(); //leggo i dati iniziali
            do
            {
                Console.WriteLine("Premi 1 per elencare i prodotti");
                Console.WriteLine("Premi 2 per inserire un prodotto");
                Console.WriteLine("Premi 3 per modificare un prodotto");
                Console.WriteLine("Premi 4 per eliminare un prodotto dal magazzino");
                Console.WriteLine("Premi 5 per visualizzare l'elenco dei prodotti con giacenza limitata (Quantità Disponibile < 10)");
                Console.WriteLine("Premi 6 per visualizzare Il numero di Prodotti per ogni Categoria");

                Console.WriteLine("Premi q per uscire");

                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        cn.ListaProdotti();
                        Console.WriteLine();
                        break;
                    case '2':
                        Console.Clear();
                        cn.InserisciProdotto();
                        cn.ListaProdotti();
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case 'q':
                        //Esci
                        Console.WriteLine("\nCiao!");
                        return;
                    default:
                        Console.WriteLine("Scelta non disponibile. Riprova!");
                        break;
                }

            } while (!(choice == 'q'));
        }
    }
}
