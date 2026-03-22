using System.Security.Cryptography.X509Certificates;
using ADT_PRZ_LinkedLists;
using ADT_PRZ_Queue;
using ADT_PRZ_Stack;

namespace ADT_Ordini;

class DeliveryStack<T> : MyStack<T> where T : Ordine
{
    public DeliveryStack() : base()
    {
        
    }

    public new void Push(T value)
    {
        if (value.IsPronto)
        {
            base.Push(value);
        }
        else
            throw new OrderNotReadyException();
    }

    /*
    Il rischio: Se qualcuno scrive MyStack<Ordine> s = new DeliveryStack<Ordine>();, e poi chiama s.Push(ordineNonPronto), il controllo di sicurezza verrà saltato perché verrà eseguito il Push della classe base.

    Fix: Dovresti rendere virtual il metodo Push nella classe madre e usare override nella figlia. Se non puoi toccare la madre, il new è l'unica opzione, ma è "pericoloso
    */
}
