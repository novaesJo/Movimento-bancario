namespace ProjetoFinanceiro.Models;

public class Despesa : Transacao
{
    public CategoriaDespesa Categoria { get; private set; }
    public bool IsFixa { get; private set; }

    public Despesa(int id, string descricao, decimal valor, DateTime data, CategoriaDespesa categoria, bool isFixa)
        : base(id, descricao, valor, data)
    {
        Categoria = categoria;
        IsFixa = isFixa;
    }

    public override string ObterTipo() => "Despesa";

    public override string ToString()
    {
        string fixaStr = IsFixa ? "[FIXA]" : "      ";
        return $"[DESPESA ] {base.ToString()}   Categoria: {Categoria,-15} {fixaStr}";
    }
}