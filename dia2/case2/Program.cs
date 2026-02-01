using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


class Produto
{
    public int Id { get; private set; } 
    public string Nome { get; set; }            
    public decimal Preco { get; set; }           
    public int Quantidade { get; set; }          

    
    public Produto(int id, string nome, decimal preco, int quantidade)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }
}

class Program
{
    static string caminhoCsv = "produtos.csv";

    static void Main()
    {
        try
        {
            bool rodando = true;

            while (rodando)
            {
                Console.WriteLine("\nMenu Produtos");
                Console.WriteLine("1 - Listar produtos");
                Console.WriteLine("2 - Adicionar produto");
                Console.WriteLine("3 - Editar produto");
                Console.WriteLine("4 - Remover produto");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": Listar(); break;
                    case "2": Adicionar(); break;
                    case "3": Editar(); break;
                    case "4": Remover(); break;
                    case "5": rodando = false; break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
        }
    }

    
    static List<Produto> LerCsv()
    {
        var lista = new List<Produto>();
        if (!File.Exists(caminhoCsv)) return lista;

        var linhas = File.ReadAllLines(caminhoCsv);
        foreach (var linha in linhas.Skip(1))
        {
            var col = linha.Split(',');
            lista.Add(new Produto(
                int.Parse(col[0]),
                col[1],
                decimal.Parse(col[2], CultureInfo.InvariantCulture),
                int.Parse(col[3])
            ));
        }
        return lista;
    }

    
    static void SalvarCsv(List<Produto> lista)
    {
        var linhas = new List<string> { "Id,Nome,Preco,Quantidade" };
        linhas.AddRange(lista.Select(p => $"{p.Id},{p.Nome},{p.Preco.ToString(CultureInfo.InvariantCulture)},{p.Quantidade}"));
        File.WriteAllLines(caminhoCsv, linhas);
    }

    static void Listar()
    {
        var lista = LerCsv();
        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("\nProdutos cadastrados:");
        foreach (var p in lista)
        {
            Console.WriteLine($"{p.Id} - {p.Nome} - R${p.Preco} - Qtd: {p.Quantidade}");
        }
    }

    static void Adicionar()
{
    var lista = LerCsv();
    
    Console.Write("Nome do produto: ");
    string nome = Console.ReadLine() ?? "Sem Nome";
    
    Console.Write("Preço do produto: ");
    decimal preco = decimal.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);
    
    Console.Write("Quantidade: ");
    int qtd = int.Parse(Console.ReadLine() ?? "0");

    
    if (preco > 0 && qtd > 0 )
    {
        
        int id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
        lista.Add(new Produto(id, nome, preco, qtd));

        SalvarCsv(lista);
        Console.WriteLine("Sucesso: Produto adicionado!");
    }
    else
    {
        
        Console.WriteLine("Erro: Preço e Quantidade devem ser maiores que zero. Operação cancelada.");
    }
}
    static void Editar()
{
    var lista = LerCsv();
    
    Console.Write("Nome do produto: ");
    string nome = Console.ReadLine() ?? "";
    
    Console.Write("Preço do produto (deve ser > 0): ");
    string entradaPreco = Console.ReadLine() ?? "0";
    
    Console.Write("Quantidade (não pode ser negativa): ");
    string entradaQtd = Console.ReadLine() ?? "0";

    // Conversão segura dos valores
    bool precoValido = decimal.TryParse(entradaPreco, CultureInfo.InvariantCulture, out decimal preco);
    bool qtdValida = int.TryParse(entradaQtd, out int qtd);

    
    if (!string.IsNullOrWhiteSpace(nome) && precoValido && qtdValida && preco > 0 && qtd >= 0)
    {
        int id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
        lista.Add(new Produto(id, nome, preco, qtd));

        SalvarCsv(lista);
        Console.WriteLine("Sucesso: Produto adicionado!");
    }
    else
    {
        Console.WriteLine("\n--- ERRO DE VALIDAÇÃO ---");
        if (string.IsNullOrWhiteSpace(nome)) 
            Console.WriteLine("- O nome do produto não pode ser vazio.");
        
        if (!precoValido || preco <= 0) 
            Console.WriteLine("- O preço deve ser um número maior que zero.");
        
        if (!qtdValida || qtd < 0) 
            Console.WriteLine("- A quantidade não pode ser negativa.");
        
        Console.WriteLine("--------------------------");
    }
}
    static void Remover()
    {
        var lista = LerCsv();
        Console.Write("Id do produto para remover: ");
        int id = int.Parse(Console.ReadLine());

        lista.RemoveAll(p => p.Id == id);
        SalvarCsv(lista);
        Console.WriteLine("Produto removido!");
    }
}
