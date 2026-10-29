using System;
using ProjetoFinanceiro.Models;
using ProjetoFinanceiro.Services;

namespace ProjetoFinanceiro.Tests;

public class ControleFinanceiroTests
{
    public static void ExecutarTodos()
    {
        Console.WriteLine("\n=== INICIANDO TESTES AUTOMATIZADOS ===\n");
        var controle = new ControleFinanceiro();

        // 1. Adicionando uma receita
        controle.AdicionarReceita("Salário Mensal", 5000m, new DateTime(2024, 1, 5));

        // 2. Definindo limites para testar o alerta
        controle.DefinirLimiteCategoria(CategoriaDespesa.Moradia, 2000m);
        controle.DefinirLimiteCategoria(CategoriaDespesa.Alimentacao, 700m);

        // 3. Inserindo as despesas exigidas (isso testará a criação e o alerta de limite)
        controle.AdicionarDespesa("Aluguel", 1500m, new DateTime(2024, 1, 10), CategoriaDespesa.Moradia, true);
        
        // Esta despesa vai ultrapassar o limite de 700m definido acima e deve exibir o alerta em vermelho
        controle.AdicionarDespesa("Mercado", 800m, new DateTime(2024, 1, 12), CategoriaDespesa.Alimentacao, false);

        // 4. Testando Relatórios
        controle.ExibirResumoFinanceiro();
        controle.RelatorioPorCategoria();
        controle.RelatorioPorPeriodo(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));

        Console.WriteLine("=== TESTES FINALIZADOS ===\n");
    }
}