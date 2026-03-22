using System.Security.Cryptography.X509Certificates;
using ADT_PRZ_LinkedLists;
using ADT_PRZ_Queue;

namespace ADT_Ordini;

class ChefDashboard<T> : MyDoubleLinkedList<T> where T : Ordine
{
    public ChefDashboard() : base()
    {
        
    }

    public MyDoubleLinkedList<T> GetOrdersByStation(CategoriaCibo cat)
    {
        MyDoubleLinkedList<T> list = new();
        foreach (var ord in this)
        {
            if (ord.Categoria == cat)
                list.Add(ord);
        }
        return list;
    }

    public T GetReadyOrder()
    {
        var curr = Fine;
        int i = Size()-1;
        while (curr != null)
        {
            if (curr.Value.IsPronto)
            {
                var ordine = curr.Value;
                Remove(i);
                return ordine;
            }
            curr = curr.precedente;
            i--;
        }

        throw new Exception("Not found");
    }

    public bool MarkAsReady(int id)
    {
        int i = 0;
        /* //PERICOLOSO MODIFICARE (Remove) UNA COLLEZIONE MENTRE LA SCORRI
        foreach (var ord in this)
        {
            if (ord.ID == id)
            {
                ord.IsPronto = true;
                Remove(i);
                Add(ord);
                return true;
            }
            i++;
        }
        */
        var curr = Inizio;
        while (curr != null)
        {
            T ord = curr.Value;
            if (ord.ID == id)
            {
                ord.IsPronto = true;
                Remove(i);
                Add(ord);
                return true;
            }
            i++;
            curr = curr.successivo;
        }
        return false;
    }
}
