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
                Console.WriteLine("\n=== Menu Produtos ===");
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
        string nome = Console.ReadLine();
        Console.Write("Preço do produto: ");
        decimal preco = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        Console.Write("Quantidade: ");
        int qtd = int.Parse(Console.ReadLine());

       
        int id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
        lista.Add(new Produto(id, nome, preco, qtd));

        SalvarCsv(lista);
        Console.WriteLine("Produto adicionado!");
    }

    static void Editar()
    {
        var lista = LerCsv();
        Console.Write("Id do produto para editar: ");
        int id = int.Parse(Console.ReadLine());

        var p = lista.FirstOrDefault(x => x.Id == id);
        if (p == null) { Console.WriteLine("Produto não encontrado!"); return; }

        Console.Write("Novo nome: ");
        p.Nome = Console.ReadLine();
        Console.Write("Novo preço: ");
        p.Preco = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        Console.Write("Nova quantidade: ");
        p.Quantidade = int.Parse(Console.ReadLine());

        SalvarCsv(lista);
        Console.WriteLine("Produto atualizado!");
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
