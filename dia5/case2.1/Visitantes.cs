namespace Sistema;

public class Visitante 
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Documento { get; set; } = "";
    public DateTime Entrada { get; set; }
    public DateTime? Saida { get; set; }
    public bool EhPrimeira { get; set; }
}

