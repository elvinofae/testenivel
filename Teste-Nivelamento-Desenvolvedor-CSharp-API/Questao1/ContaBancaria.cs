using System;

namespace Questao1
{
    public class ContaBancaria {
        public int NumeroConta { get; }
        public string TitularConta { get; set; }
        public double Saldo { get; private set; }
        private const double taxaSaque = 3.50;

        public ContaBancaria(int numeroConta, string titularConta, double depositoInicial = 0.0)
        {
            NumeroConta = numeroConta;
            TitularConta = titularConta;
            Deposito(depositoInicial);
        }

        public void Deposito(double valor)
        {
            if (valor > 0)
            {
                Saldo += valor;
            }
        }

        public void Saque(double valor)
        {
            if (valor > 0 && valor <= Saldo)
            {
                Saldo -= valor;
                Saldo -= taxaSaque;
            }
            else if (valor < 0 || valor > Saldo)
            {
                Console.WriteLine("Saldo insuficiente.");
            }
        }

        public override string ToString()
        {
            return $"Conta {NumeroConta}, Titular: {TitularConta}, Saldo: $ {Saldo:F2}";
        }
    }
}
