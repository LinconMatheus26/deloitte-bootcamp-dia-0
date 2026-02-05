public class Producao
{
    public int id;
    public decimal volume;

    // Regra de Negócio: Só processa se o minério for "ouro"
    public void ProcessarExtração(Mina mina)
    {
        Minerio extraido = mina.acessarExtrairMinerio();

        if (extraido.tipo == "ouro")
        {
            this.volume += 10;
            Console.WriteLine("Sucesso: Ouro processado e adicionado ao volume.");
        }
        else
        {
            Console.WriteLine("Aviso: Esta linha de produção só aceita ouro.");
        }
    }
}