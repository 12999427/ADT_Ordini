using System.Threading.Tasks;
using ADT_PRZ_Stack;

namespace ADT_Ordini;

class Program
{
    static async Task Main(string[] args)
    {   
        CervelloCentrale cc = new();
        TerminaleUtente tu = new(cc);
        Chef ch = new(cc);
        Drone dr = new(cc);

        CancellationTokenSource cts = new();
        List<Task> tasks = new();
        tasks.Add(cc.EseguiCervelloCentrale(cts.Token));
        tasks.Add(tu.EseguiTerminaleUtente(cts.Token));
        tasks.Add(ch.EseguiChef(cts.Token));
        tasks.Add(dr.EseguiDrone(cts.Token));
        
        await Task.Delay(10000);

        cts.Cancel();
    }
}
