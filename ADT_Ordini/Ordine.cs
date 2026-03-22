using System.Security.Cryptography.X509Certificates;

namespace ADT_Ordini;

enum CategoriaCibo
{
    Pizza,
    Drink,
    Shushi
}
class Ordine
{
    private static int contatore = 0;

    public int ID {get; private set;}
    public string NomePiatto {get; private set;}
    public double Prezzo {get; private set;}
    public CategoriaCibo Categoria {get; private set;}
    public bool IsPronto {get; set;}

    public Ordine(string nomePiatto, double prezzo, CategoriaCibo categoria)
    {
        ID = contatore++;
        NomePiatto = nomePiatto;
        Prezzo = prezzo;
        Categoria = categoria;
        IsPronto = false;
    }

    public bool HaPriorità()
    {
        if (Categoria == CategoriaCibo.Shushi)
            return true;

        return false;
    }
    public static Ordine RandomOrdine()
    {
        return new Ordine("[NomePiatto]", Random.Shared.NextDouble()*20 + 10, (CategoriaCibo) Random.Shared.Next(0, Enum.GetNames<CategoriaCibo>().Length));
    }

    public override string ToString()
    {
        return $"[Ordine: {ID} | Pronto {IsPronto} | Priorità {HaPriorità()}]";
    }
}
