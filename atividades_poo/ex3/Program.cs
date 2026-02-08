using System;

namespace ExerciciosPOO
{
    public class Aluno
    {
        private string nome;
        // Propriedade com validação no "set"
        public string Nome 
        { 
            get => nome; 
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("O nome não pode ser vazio.");
                nome = value;
            }
        }

        public string Matricula { get; set; }
        public string Curso { get; set; }
        
        private string[] disciplinas = new string[3];
        private double[] notas = new double[3];

        public void SetDisciplinaENota(int indice, string nomeDisc, double nota)
        {
            // Validação de índice
            if (indice < 0 || indice > 2) return;

            // Validação de Nome da Disciplina
            if (string.IsNullOrWhiteSpace(nomeDisc))
                throw new Exception("O nome da disciplina é obrigatório.");

            // Validação de Nota (Não permite negativa nem maior que 10)
            if (nota < 0 || nota > 10)
                throw new Exception("Nota inválida! A nota deve ser entre 0 e 10.");

            disciplinas[indice] = nomeDisc;
            notas[indice] = nota;
        }

        public string GetNomeDisciplina(int indice) => disciplinas[indice];
        public double GetNotaDisciplina(int indice) => notas[indice];

        public bool VerificarAprovacao(int indice) => notas[indice] >= 7.0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Aluno aluno = new Aluno();

            // Usamos um Try/Catch para capturar os erros que definimos na classe
            try 
            {
                Console.Write("Nome: ");
                aluno.Nome = Console.ReadLine();

                Console.Write("Matrícula: ");
                aluno.Matricula = Console.ReadLine();

                Console.Write("Curso: ");
                aluno.Curso = Console.ReadLine();

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"\n--- {i + 1}ª Disciplina ---");
                    Console.Write("Nome: ");
                    string disc = Console.ReadLine();
                    
                    Console.Write("Nota: ");
                    double nota = double.Parse(Console.ReadLine());
                    
                    // Se a nota for negativa, o método abaixo vai barrar
                    aluno.SetDisciplinaENota(i, disc, nota);
                }

                // Exibição... (igual ao anterior)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERRO DE VALIDAÇÃO: {ex.Message}");
                Console.WriteLine("O programa será encerrado. Tente novamente com dados válidos.");
            }
        }
    }
}