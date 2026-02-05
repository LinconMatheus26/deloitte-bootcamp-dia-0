using SistemaChamados;

IRepositorioChamados repo = new RepositorioChamadosMemoria();
int contadorId = 1;

while (true)
{
    Console.WriteLine("\n--- TI SUPPORT SYSTEM ---");
    Console.WriteLine("1. Abrir Chamado | 2. Listar | 3. Mudar Status | 4. Mudar Prioridade | 5. Detalhes | 0. Sair");
    
    try
    {
        string op = Console.ReadLine();
        if (op == "0") break;

        switch (op)
        {
            case "1":
                Console.Write("Título: "); string t = Console.ReadLine();
                Console.Write("Solicitante: "); string s = Console.ReadLine();
                Console.Write("Descrição do Problema: "); string desc = Console.ReadLine();
                Console.Write("Prioridade (0-Baixa, 1-Media, 2-Alta): ");
                Prioridade p = (Prioridade)int.Parse(Console.ReadLine());
                Console.Write("Tipo (1-Infra, 2-App): "); string tipo = Console.ReadLine();
                
                Chamado novo = tipo == "1" 
                    ? new ChamadoInfra(contadorId++, t, s, desc, p) 
                    : new ChamadoAplicacao(contadorId++, t, s, desc, p);
                
                repo.Criar(novo);
                Console.WriteLine("Chamado cadastrado com sucesso!");
                break;

            case "2":
                Console.WriteLine("\nID | Título | Status | Prioridade");
                foreach (var c in repo.Listar())
                    Console.WriteLine($"{c.Id} | {c.Titulo} | {c.Status} | {c.Prioridade}");
                break;

            case "3":
                Console.Write("ID do Chamado: "); int idAtu = int.Parse(Console.ReadLine());
                var chamAtu = repo.ObterPorId(idAtu) ?? throw new DominioException("Não encontrado.");
                Console.WriteLine("Novo Status (1-EmAndamento, 2-Resolvido, 3-Fechado): ");
                string st = Console.ReadLine();
                StatusChamado novoSt = st == "1" ? StatusChamado.EmAndamento : st == "2" ? StatusChamado.Resolvido : StatusChamado.Fechado;
                chamAtu.AtualizarStatus(novoSt);
                Console.WriteLine("Status atualizado!");
                break;

            case "4": // NOVA OPÇÃO
                Console.Write("ID do Chamado: "); int idPri = int.Parse(Console.ReadLine());
                var chamPri = repo.ObterPorId(idPri) ?? throw new DominioException("Não encontrado.");
                Console.Write("Nova Prioridade (0-Baixa, 1-Media, 2-Alta): ");
                Prioridade novaP = (Prioridade)int.Parse(Console.ReadLine());
                chamPri.AlterarPrioridade(novaP);
                Console.WriteLine("Prioridade atualizada!");
                break;

            case "5":
                Console.Write("ID: "); int idDet = int.Parse(Console.ReadLine());
                var cDet = repo.ObterPorId(idDet) ?? throw new DominioException("Não encontrado.");
                Console.WriteLine($"\n>> {cDet.ObterResumoTipo()}");
                Console.WriteLine($"Descrição: {cDet.Descricao}");
                Console.WriteLine("--- Histórico ---");
                cDet.Historico.ForEach(h => Console.WriteLine(h));
                break;
        }
    }
    catch (DominioException ex) { Console.WriteLine($"\n[AVISO]: {ex.Message}"); }
    catch (Exception) { Console.WriteLine("\n[ERRO]: Entrada inválida. Digite números onde solicitado."); }
}