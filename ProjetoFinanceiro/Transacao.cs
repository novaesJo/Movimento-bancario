namespace ProjetoFinanceiro.Models;

public abstract class Transacao
{
    public int Id { get; protected set; }
    public string Descricao { get; protected set; }
    public decimal Valor { get; protected set; }
    public DateTime Data { get; protected set; }

    protected Transacao(int id, string descricao, decimal valor, DateTime data)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        Data = data;
    }

    public abstract string ObterTipo();

    public override string ToString()
    {
        return $"[ID: {Id:D3}] {Descricao,-30} R$ {Valor,10:F2}   {Data:dd/MM/yyyy}";
    }
}
