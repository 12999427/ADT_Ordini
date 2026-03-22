namespace ADT_Ordini;

class Drone
{
    private CervelloCentrale cervelloCentrale;

    private DeliveryStack<Ordine> deliveryStack;
    public Drone(CervelloCentrale cc)
    {
        cervelloCentrale = cc;
        deliveryStack = new();
    }

    public async Task EseguiDrone(CancellationToken ct)
    {
        try
        {
            while (true)
            {
                Printer.Print("Drone attende che ci sia un ordine da prelevare dalla lista", nameof(Drone));
                await cervelloCentrale.S_ProntiInListaChef.WaitAsync();

                await cervelloCentrale.M_ListaChef.WaitAsync();
                var o = cervelloCentrale.ListaChef.GetReadyOrder();
                Printer.Print($"Drone mette l'ordine {o} nel proprio zaino", nameof(Drone));
                cervelloCentrale.M_ListaChef.Release();

                deliveryStack.Push(o);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }
}