namespace ADT_Ordini;

class TerminaleUtente
{
    private static int contatore;
    public int ID {get; private set;}
    private CervelloCentrale cervelloCentrale;
    public TerminaleUtente(CervelloCentrale cc)
    {
        ID = contatore++;
        cervelloCentrale = cc;
    }

    public async Task EseguiTerminaleUtente(CancellationToken ct)
    {
        try
        {
            while (true)
            {
                var o = Ordine.RandomOrdine();
                Printer.Print($"Terminale inserisce nuovo ordine {o}", nameof(TerminaleUtente));
                await cervelloCentrale.InserisciInCoda(o);
                await Task.Delay(100);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }
}
