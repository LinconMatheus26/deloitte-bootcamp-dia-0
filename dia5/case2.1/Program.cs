using Sistema;


string op = "";

while (op != "0") {
    Console.WriteLine("\n1-Cadastrar  \n2-Listar  \n3-Registrar Saída  \n4-Buscar Nome  \n5-Filtro 1ª Vez  \n6-Ordenar ID  \n0-Sair");
    Console.Write("Opção: ");
    op = Console.ReadLine() ?? "";
 
    try {
        switch (op) {
            case "1":
                Console.Write("Nome: "); string n = Console.ReadLine()!;
                if (double.TryParse(n, out _) || string.IsNullOrWhiteSpace(n)) 
                    throw new Exception("Nome inválido!");

                Console.Write("CPF: "); string cpf = Console.ReadLine()!;
                if (!Validador.CpfValido(cpf)) throw new Exception("CPF inválido.");
                
                Console.Write("É primeira vez? (s/n): ");
                bool primeira = Console.ReadLine()?.ToLower() == "s";

                lista.Add(new Visitante { 
                    Id = idCont++, Nome = n, Documento = cpf, Entrada = DateTime.Now, EhPrimeira = primeira 
                });
                Console.WriteLine("Cadastrado com sucesso!");
                break;

            case "2":
                if (lista.Count == 0) Console.WriteLine("Lista vazia.");
                lista.ForEach(v => {
                    string s = v.Saida.HasValue ? v.Saida.Value.ToString("HH:mm") : "--:--";
                    Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome} | Entrada: {v.Entrada:HH:mm} | Saída: {s}");
                });
                break;

            case "3":
                Console.Write("Digite o ID: "); 
                int id = int.Parse(Console.ReadLine()!);
                var vis = lista.Find(x => x.Id == id) ?? throw new Exception("ID não encontrado.");
                vis.Saida = DateTime.Now;
                Console.WriteLine("Saída registrada.");
                break;

            case "4":
                Console.Write("Buscar nome: "); 
                string b = Console.ReadLine()?.ToLower() ?? "";
               
                var encontrados = lista.Where(x => x.Nome.ToLower().Contains(b)).ToList();
                
                if (encontrados.Any())
                    encontrados.ForEach(x => Console.WriteLine($"Encontrado: ID {x.Id} - {x.Nome}"));
                else
                    Console.WriteLine("Nenhum visitante encontrado.");
                break;

            case "5":
                Console.WriteLine("Visitantes de primeira vez:");
                lista.Where(x => x.EhPrimeira).ToList().ForEach(x => Console.WriteLine(x.Nome));
                break;

            case "6":
                
                lista = lista.OrderBy(x => x.Id).ToList();
                Console.WriteLine("Lista ordenada por ID com sucesso!");
                break;
        }
    } 
    catch (Exception ex) {
        Console.WriteLine($"[ERRO]: {ex.Message}");
    }
}