using System;

namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Abertura de Conta ===");
                Console.Write("Número da Conta: ");
                string num = Console.ReadLine();

                Console.Write("É conta especial? (S/N): ");
                bool especial = Console.ReadLine().ToUpper() == "S";

                double limite = 0;
                if (especial)
                {
                    Console.Write("Qual o limite aprovado? ");
                    limite = double.Parse(Console.ReadLine());
                }

                // Instanciação
                ContaCorrente conta = new ContaCorrente(num, especial, limite);

                bool rodando = true;
                while (rodando)
                {
                    Console.WriteLine("\n1-Depositar | 2-Sacar | 3-Saldo | 4-Sair");
                    Console.Write("Opção: ");
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            Console.Write("Valor do depósito: ");
                            conta.Depositar(double.Parse(Console.ReadLine()));
                            break;
                        case "2":
                            Console.Write("Valor do saque: ");
                            double vSaque = double.Parse(Console.ReadLine());
                            if (conta.Sacar(vSaque)) Console.WriteLine("Saque efetuado!");
                            else Console.WriteLine("Saldo/Limite insuficiente!");
                            break;
                        case "3":
                            ExibirStatus(conta);
                            break;
                        case "4":
                            rodando = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro fatal: {ex.Message}");
            }
        }

        static void ExibirStatus(ContaCorrente c)
        {
            Console.WriteLine("\n--- STATUS ATUAL ---");
            Console.WriteLine($"Saldo: R$ {c.Saldo:F2}");
            Console.WriteLine($"Limite: R$ {c.Limite:F2}");
            Console.WriteLine($"Cheque Especial: {(c.UsandoChequeEspecial() ? "EM USO" : "NÃO UTILIZADO")}");
        }
    }
}