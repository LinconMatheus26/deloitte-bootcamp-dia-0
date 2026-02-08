using System;

namespace SistemaBancario
{
    public class ContaCorrente
    {
        private string numero;
        private double saldo;
        private double limite;

        // Propriedades com validação básica
        public string Numero 
        { 
            get => numero; 
            private set => numero = !string.IsNullOrWhiteSpace(value) ? value : throw new Exception("Número inválido.");
        }

        public double Saldo => saldo;
        public bool IsEspecial { get; private set; }
        public double Limite => limite;

        public ContaCorrente(string numero, bool especial, double limite)
        {
            Numero = numero;
            IsEspecial = especial;
            // Se não for especial, ignora o limite enviado
            this.limite = especial ? Math.Abs(limite) : 0;
            this.saldo = 0;
        }

        public void Depositar(double valor)
        {
            if (valor <= 0) throw new Exception("O depósito deve ser maior que zero.");
            saldo += valor;
        }

        public bool Sacar(double valor)
        {
            if (valor <= 0) throw new Exception("O valor do saque deve ser positivo.");

            // Regra: Saldo + Limite disponível
            if (valor <= (saldo + limite))
            {
                saldo -= valor;
                return true;
            }
            return false;
        }

        public bool UsandoChequeEspecial() => saldo < 0;
    }
}