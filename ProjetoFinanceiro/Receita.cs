namespace ProjetoFinanceiro.Models;

public class Receita : Transacao
{
    public Receita(int id, string descricao, decimal valor, DateTime data)
        : base(id, descricao, valor, data)
    {
    }

    public override string ObterTipo() => "Receita";

    public override string ToString()
    {
        return $"[RECEITA ] {base.ToString()}";
    }
}
