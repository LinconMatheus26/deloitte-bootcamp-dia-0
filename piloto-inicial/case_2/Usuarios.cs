using System;

public static class UsuarioConfig
{
    public static void ExecutarCadastro()
    {
        try
        {
            Console.WriteLine("=== Cadastro de Usuário ===");

            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome não pode ser vazio.");
            }

            Console.Write("Digite a idade: ");
            int idade = int.Parse(Console.ReadLine());

            if (idade <= 0)
            {
                throw new ArgumentException("A idade deve ser maior que zero.");
            }

            var usuario = new UsuarioRequest(nome, idade);

            Console.WriteLine("\nUsuário cadastrado com sucesso!");
            Console.WriteLine($"Nome: {usuario.Nome}");
            Console.WriteLine($"Idade: {usuario.Idade}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Erro: A idade deve ser um número válido.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (Exception)
        {
            Console.WriteLine("Erro inesperado ao cadastrar o usuário.");
        }
    }
}

public record UsuarioRequest(string Nome, int Idade);