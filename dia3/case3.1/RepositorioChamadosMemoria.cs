namespace SistemaChamados;

public class RepositorioChamadosMemoria : IRepositorioChamados
{
    private readonly List<Chamado> _chamados = new();

    public void Criar(Chamado chamado) => _chamados.Add(chamado);

    public Chamado ObterPorId(int id) => _chamados.FirstOrDefault(c => c.Id == id);

    public IEnumerable<Chamado> Listar() => _chamados;

    public void Excluir(int id)
    {
        var c = ObterPorId(id) ?? throw new DominioException("Chamado não encontrado para exclusão.");
        if (c.Status != StatusChamado.Aberto)
            throw new DominioException("Apenas chamados com status 'Aberto' podem ser excluídos.");
        
        _chamados.Remove(c);
    }
}