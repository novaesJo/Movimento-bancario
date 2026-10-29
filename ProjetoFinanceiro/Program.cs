using ProjetoFinanceiro.Models;
using ProjetoFinanceiro.Services;
using ProjetoFinanceiro.Tests;

namespace ProjetoFinanceiro;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--testes")
        {
            ControleFinanceiroTests.ExecutarTodos();
            return;
        }

        var controle = new ControleFinanceiro();
        bool executando = true;

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (executando)
        {
            ExibirMenu();
            string opcao = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (opcao)
            {
                case "1": AdicionarReceita(controle); break;
                case "2": AdicionarDespesa(controle); break;
                case "3": ListarReceitas(controle); break;
                case "4": ListarDespesas(controle); break;
                case "5": ExibirSaldo(controle); break;
                case "6": controle.ExibirResumoFinanceiro(); break;
                case "7": controle.RelatorioPorCategoria(); break;
                case "8": RelatorioPorPeriodo(controle); break;
                case "9": DefinirLimiteCategoria(controle); break;
                case "0":
                    executando = false;
                    Console.WriteLine("\nEncerrando o sistema. Até logo!\n");
                    break;
                default:
                    Console.WriteLine("\n[!] Opção inválida. Tente novamente.\n");
                    break;
            }
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║     SISTEMA DE CONTROLE FINANCEIRO       ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║   1. Adicionar Receita                   ║");
        Console.WriteLine("║   2. Adicionar Despesa                   ║");
        Console.WriteLine("║   3. Listar Receitas                     ║");
        Console.WriteLine("║   4. Listar Despesas                     ║");
        Console.WriteLine("║   5. Exibir Saldo                        ║");
        Console.WriteLine("║   6. Exibir Resumo Financeiro            ║");
        Console.WriteLine("║   7. Relatório por Categoria             ║");
        Console.WriteLine("║   8. Relatório por Período               ║");
        Console.WriteLine("║   9. Definir Limite por Categoria        ║");
        Console.WriteLine("║   0. Sair                                ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.Write("  Escolha uma opção: ");
    }

    static void AdicionarReceita(ControleFinanceiro controle)
    {
        Console.WriteLine("\n--- Adicionar Receita ---");
        Console.Write("Descrição  : ");
        string descricao = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(descricao)) return;

        Console.Write("Valor (R$) : ");
        if (!decimal.TryParse(Console.ReadLine()?.Replace(".", ","), out decimal valor) || valor <= 0) return;

        Console.Write("Data (dd/MM/yyyy) : ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data)) return;

        controle.AdicionarReceita(descricao, valor, data);
        Console.WriteLine("[✓] Receita adicionada com sucesso!\n");
    }

    static void AdicionarDespesa(ControleFinanceiro controle)
    {
        Console.WriteLine("\n--- Adicionar Despesa ---");
        Console.Write("Descrição  : ");
        string descricao = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(descricao)) return;

        Console.Write("Valor (R$) : ");
        if (!decimal.TryParse(Console.ReadLine()?.Replace(".", ","), out decimal valor) || valor <= 0) return;

        Console.Write("Data (dd/MM/yyyy) : ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data)) return;

        Console.WriteLine("Categorias disponíveis:");
        foreach (var cat in Enum.GetValues(typeof(CategoriaDespesa)))
        {
            Console.WriteLine($"{(int)cat}. {cat}");
        }
        Console.Write("Escolha o número da categoria: ");
        if (!Enum.TryParse(Console.ReadLine(), out CategoriaDespesa categoria) || !Enum.IsDefined(typeof(CategoriaDespesa), categoria))
        {
            Console.WriteLine("[!] Categoria inválida.\n");
            return;
        }

        Console.Write("Esta é uma despesa fixa mensal? (S/N): ");
        bool isFixa = Console.ReadLine()?.Trim().ToUpper() == "S";

        controle.AdicionarDespesa(descricao, valor, data, categoria, isFixa);
        Console.WriteLine("[✓] Despesa adicionada com sucesso!\n");
    }

    static void DefinirLimiteCategoria(ControleFinanceiro controle)
    {
        Console.WriteLine("\n--- Definir Limite de Gasto ---");
        Console.WriteLine("Categorias disponíveis:");
        foreach (var cat in Enum.GetValues(typeof(CategoriaDespesa)))
            Console.WriteLine($"{(int)cat}. {cat}");
        
        Console.Write("Escolha a categoria: ");
        if (!Enum.TryParse(Console.ReadLine(), out CategoriaDespesa categoria) || !Enum.IsDefined(typeof(CategoriaDespesa), categoria))
            return;

        Console.Write("Informe o valor limite (R$): ");
        if (decimal.TryParse(Console.ReadLine()?.Replace(".", ","), out decimal limite))
        {
            controle.DefinirLimiteCategoria(categoria, limite);
            Console.WriteLine($"[✓] Limite de R$ {limite:F2} definido para a categoria {categoria}.\n");
        }
    }

    static void RelatorioPorPeriodo(ControleFinanceiro controle)
    {
        Console.WriteLine("\n--- Relatório por Período ---");
        Console.Write("Data Início (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime inicio)) return;

        Console.Write("Data Fim (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fim)) return;

        controle.RelatorioPorPeriodo(inicio, fim);
    }

    // ListarReceitas, ListarDespesas e ExibirSaldo permanecem iguais ao original...
    static void ListarReceitas(ControleFinanceiro controle)
    {
        var receitas = controle.ObterReceitas();
        if (receitas.Count == 0) { Console.WriteLine("Nenhuma receita.\n"); return; }
        foreach (var r in receitas) Console.WriteLine(r);
    }

    static void ListarDespesas(ControleFinanceiro controle)
    {
        var despesas = controle.ObterDespesas();
        if (despesas.Count == 0) { Console.WriteLine("Nenhuma despesa.\n"); return; }
        foreach (var d in despesas) Console.WriteLine(d);
    }

    static void ExibirSaldo(ControleFinanceiro controle)
    {
        decimal saldo = controle.CalcularSaldo();
        Console.WriteLine($"\n  Saldo atual : R$ {saldo:F2}\n");
    }
}