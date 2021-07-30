using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Test5
{
    public class Connessione
    {
        private string connectionString;

        private SqlConnection conn;
        private DataSet dsMagazzino = new DataSet();
        private SqlDataAdapter prodottiAdapter;

        public Connessione(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal void ListaProdotti()
        {
            DataTable table = dsMagazzino.Tables["Prodotto"];
            
            foreach(DataRow row in table.Rows)
            {
                Console.WriteLine($"{row[0]} {row[1]} {row[2]} {row[3]} {row[4]} {row[5]}");
            }
        }

        internal void Init()
        {
            using (conn = new(connectionString))
            {
               // conn.Open();

                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Connessione non riuscita");

                this.prodottiAdapter = new();

                prodottiAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                SqlCommand selectProdotti = new SqlCommand("SELECT * FROM Prodotto", conn);

                prodottiAdapter.SelectCommand = selectProdotti;

                prodottiAdapter.Fill(dsMagazzino, "Prodotto");
            }
        }

        internal void InserisciProdotto()
        {
            /* DataTable table = dsMagazzino.Tables["Prodotto"];

             foreach (DataRow row in table.Rows)
             {
                 Console.WriteLine($"{row[0]} {row[1]} {row[2]} {row[3]} {row[4]} {row[5]}");
             }*/
            Console.WriteLine("Inserisci il codice del prodotto");
            string codice = Console.ReadLine();
            bool errore = true;
            string cat = " ";
            do
            {
                Console.WriteLine("Inserisci la categoria: Alimentari, Cancelleria oppure Sanitari");
                string categoria = Console.ReadLine();
                
                if ((categoria == "Alimentari") || (categoria == "Cancelleria") || (categoria == "Sanitari"))
                {
                    cat = categoria;
                    errore = false;
                }
                else
                    Console.WriteLine("Errore nell'inserimento della categoria");

            } while (errore);
             Console.WriteLine("Inserisci la descrizione del prodotto");
            string descrizione = Console.ReadLine();
            Console.WriteLine("Inserisci il prezzo unitario");
            decimal.TryParse(Console.ReadLine(), out decimal pUnitario);
            Console.WriteLine("Inserisci il quantitativo il magazzino");
            int.TryParse(Console.ReadLine(), out int quantitativo);
            DataRow newRow = dsMagazzino.Tables["Prodotto"].NewRow();

            newRow["CodiceProdotto"] = codice;
            newRow["Categoria"] = cat;
            newRow["Descrizione"] = descrizione;
            newRow["PrezzoUnitario"] = pUnitario;
            newRow["QuantitaDisponibile"] = quantitativo;

            dsMagazzino.Tables["Prodotto"].Rows.Add(newRow);
            Aggiorna();
        }

        public bool Aggiorna()
        {
            using (conn = new(connectionString))
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                    return false;
                prodottiAdapter.SelectCommand.Connection = conn;
                prodottiAdapter.InsertCommand.Connection = conn;
                prodottiAdapter.DeleteCommand.Connection = conn;
                prodottiAdapter.UpdateCommand.Connection = conn;
                prodottiAdapter.Update(dsMagazzino, "Prodotto");

                prodottiAdapter.Fill(dsMagazzino, "Prodotto");
            }

            return true;
        }

    }
}