namespace ADT_Ordini;

class Chef
{
    private CervelloCentrale cervelloCentrale;
    public Chef(CervelloCentrale cc)
    {
        cervelloCentrale = cc;
    }

    public async Task EseguiChef(CancellationToken ct)
    {
        try
        {
            while (true)
            {
                Printer.Print("Chef attende che ci siano ordini nella sua lista", nameof(Chef));
                await cervelloCentrale.S_DisponibiliPerChef.WaitAsync();

                await cervelloCentrale.M_ListaChef.WaitAsync();
                    Ordine o = cervelloCentrale.ListaChef.At(0);
                    int id = o.ID;
                cervelloCentrale.M_ListaChef.Release();
                Printer.Print($"Chef sta preparando ordine {o}", nameof(Chef));

                await Task.Delay(Random.Shared.Next(500, 1001));

                await cervelloCentrale.M_ListaChef.WaitAsync();
                    cervelloCentrale.ListaChef.Remove(0);
                    cervelloCentrale.ListaChef.Add(o);
                    cervelloCentrale.ListaChef.MarkAsReady(id);
                cervelloCentrale.M_ListaChef.Release();
                
                Printer.Print($"Chef ha finito di prepare ordine {o} e lo mette a fine lista", nameof(Chef));

                cervelloCentrale.S_ProntiInListaChef.Release();

            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }
}