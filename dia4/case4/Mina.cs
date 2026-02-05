public class Mina
{
    public Minerio minerio = new Minerio();
    
    // Tornando os atributos visíveis para o Program.cs
    public string codigo;
    public string nome;
    public string capacidade;

    public Minerio acessarExtrairMinerio()
    {
        return this.extrairMinerio();
    }

    private Minerio extrairMinerio()
    {
        minerio.codigo = "1";
        minerio.tipo = "ouro";
        return minerio;
    }
}