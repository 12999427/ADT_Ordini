using System.Security.Cryptography.X509Certificates;
using ADT_PRZ_Queue;

namespace ADT_Ordini;

class RestaurantQueue<T> : MyQueue<T> where T : Ordine
{
    public RestaurantQueue() : base()
    {
        
    }

    public double GetTotalRevenue()
    {
        double tot = .0;
        Node<T>? curr = Start;
        while (curr != null)
        {
            tot += curr.Value.Prezzo;
            curr = curr.Next;
        }
        return tot;
    }

    public T ExtractPriorityOrder()
    {
        if (IsEmpty())
            throw new InvalidOperationException();
        
        if (Peek().HaPriorità())
        {
            return Dequeue();
        }
        
        Node<T> prev = Start!;
        Node<T>? curr = Start!.Next;
        while (curr != null)
        {
            if (curr.Value.HaPriorità())
            {
                prev.Next = curr.Next;
                if (curr.Next == null) //curr è l'ultimo, e quindi curr = End (dangling pointer)
                {
                    End = prev;
                }
                return curr.Value;
            }
            curr = curr.Next;
        }

        return Dequeue();
    }
}
