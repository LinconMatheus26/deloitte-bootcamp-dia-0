namespace SistemaChamados;

public interface IRepositorioChamados
{
    void Criar(Chamado chamado);
    Chamado ObterPorId(int id);
    IEnumerable<Chamado> Listar();
    void Excluir(int id);
}