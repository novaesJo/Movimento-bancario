using ProjetoFinanceiro.Models;

namespace ProjetoFinanceiro.Services;

public class ControleFinanceiro
{
    private readonly List<Receita> _receitas;
    private readonly List<Despesa> _despesas;
    private readonly Dictionary<CategoriaDespesa, decimal> _limitesPorCategoria;
    private int _proximoId;

    public ControleFinanceiro()
    {
        _receitas = new List<Receita>();
        _despesas = new List<Despesa>();
        _limitesPorCategoria = new Dictionary<CategoriaDespesa, decimal>();
        _proximoId = 1;
    }

    public void AdicionarReceita(string descricao, decimal valor, DateTime data)
    {
        var receita = new Receita(_proximoId++, descricao, valor, data);
        _receitas.Add(receita);
    }

    public void AdicionarDespesa(string descricao, decimal valor, DateTime data, CategoriaDespesa categoria, bool isFixa)
    {
        var despesa = new Despesa(_proximoId++, descricao, valor, data, categoria, isFixa);
        _despesas.Add(despesa);

        VerificarLimiteGasto(categoria);
    }

    public void DefinirLimiteCategoria(CategoriaDespesa categoria, decimal limite)
    {
        _limitesPorCategoria[categoria] = limite;
    }

    private void VerificarLimiteGasto(CategoriaDespesa categoria)
    {
        if (_limitesPorCategoria.TryGetValue(categoria, out decimal limite))
        {
            decimal totalGastoCategoria = _despesas.Where(d => d.Categoria == categoria).Sum(d => d.Valor);
            if (totalGastoCategoria > limite)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ALERTA] O limite da categoria {categoria} foi excedido! Limite: R${limite:F2} | Gasto Atual: R${totalGastoCategoria:F2}");
                Console.ResetColor();
            }
        }
    }

    public IReadOnlyList<Receita> ObterReceitas() => _receitas.AsReadOnly();
    public IReadOnlyList<Despesa> ObterDespesas() => _despesas.AsReadOnly();
    public decimal CalcularTotalReceitas() => _receitas.Sum(r => r.Valor);
    public decimal CalcularTotalDespesas() => _despesas.Sum(d => d.Valor);
    public decimal CalcularSaldo() => CalcularTotalReceitas() - CalcularTotalDespesas();

    public void RelatorioPorPeriodo(DateTime inicio, DateTime fim)
    {
        var recs = _receitas.Where(r => r.Data >= inicio && r.Data <= fim).ToList();
        var desps = _despesas.Where(d => d.Data >= inicio && d.Data <= fim).ToList();

        Console.WriteLine($"\n=== RELATÓRIO DE {inicio:dd/MM/yyyy} ATÉ {fim:dd/MM/yyyy} ===");
        Console.WriteLine($"Total Receitas no Período: R$ {recs.Sum(r => r.Valor):F2}");
        Console.WriteLine($"Total Despesas no Período: R$ {desps.Sum(d => d.Valor):F2}");
        Console.WriteLine($"Saldo no Período: R$ {(recs.Sum(r => r.Valor) - desps.Sum(d => d.Valor)):F2}\n");
    }

    public void RelatorioPorCategoria()
    {
        Console.WriteLine("\n=== RELATÓRIO POR CATEGORIA DE DESPESA ===");
        var agrupado = _despesas.GroupBy(d => d.Categoria);
        
        if (!agrupado.Any())
        {
            Console.WriteLine("Nenhuma despesa cadastrada.");
            return;
        }

        foreach (var grupo in agrupado)
        {
            decimal total = grupo.Sum(d => d.Valor);
            Console.WriteLine($"- {grupo.Key,-15}: R$ {total:F2} ({grupo.Count()} transações)");
        }
        Console.WriteLine();
    }

    public void ExibirResumoFinanceiro()
    {
        decimal totalReceitas = CalcularTotalReceitas();
        decimal totalDespesas = CalcularTotalDespesas();
        decimal saldo = CalcularSaldo();

        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║          RESUMO FINANCEIRO               ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine($"║  Receitas cadastradas : {_receitas.Count,5}              ║");
        Console.WriteLine($"║  Despesas cadastradas : {_despesas.Count,5}              ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine($"║  Total de Receitas  : R$ {totalReceitas,14:F2}   ║");
        Console.WriteLine($"║  Total de Despesas  : R$ {totalDespesas,14:F2}   ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine($"║  Saldo Atual        : R$ {saldo,14:F2}   ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine();
    }
}