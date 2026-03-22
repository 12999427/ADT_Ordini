namespace ADT_Ordini;

class CervelloCentrale
{
    public RestaurantQueue<Ordine> CodaOrdini {get; private set;}
    private SemaphoreSlim M_CodaOrdini;
    public ChefDashboard<Ordine> ListaChef {get; private set;}
    public SemaphoreSlim M_ListaChef;
    private SemaphoreSlim S_DisponibiliInCoda;
    public SemaphoreSlim S_DisponibiliPerChef;
    public SemaphoreSlim S_ProntiInListaChef;

    public CervelloCentrale()
    {
        CodaOrdini = new();
        M_CodaOrdini = new (1, 1);
        ListaChef = new();
        M_ListaChef = new(1, 1);
        S_DisponibiliInCoda = new(0);
        S_DisponibiliPerChef = new(0);
        S_ProntiInListaChef = new(0);
    }

    public async Task EseguiCervelloCentrale(CancellationToken ct)
    {
        try
        {
            while (true)
            {
                Printer.Print("Cervello attende che ci siano ordini in coda", nameof(CervelloCentrale));
                Ordine o = await PrelevaDaCoda();
                Printer.Print($"Cervello inizia a processare l'ordine {o} (sclto con priorità)", nameof(CervelloCentrale));
                await Task.Delay(Random.Shared.Next(500, 1001));
                Printer.Print($"Cervello inserisce l'ordine {o} nella lista dello chef", nameof(CervelloCentrale));
                await InserisciInListaChef(o);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    public async Task InserisciInCoda(Ordine o)
    {
        await M_CodaOrdini.WaitAsync();
        CodaOrdini.Enqueue(o);
        M_CodaOrdini.Release();
        S_DisponibiliInCoda.Release();
    }

    public async Task<Ordine> PrelevaDaCoda()
    {
        await S_DisponibiliInCoda.WaitAsync();
        await M_CodaOrdini.WaitAsync();
        Ordine o = CodaOrdini.ExtractPriorityOrder();
        M_CodaOrdini.Release();
        return o;
    }

    public async Task InserisciInListaChef(Ordine o)
    {
        await M_ListaChef.WaitAsync();
        ListaChef.Add(o);
        M_ListaChef.Release();
        S_DisponibiliPerChef.Release();
    }
}
