using System;

Mina mina = new Mina();
mina.nome = "Mina Nordeste";

// Chamando o método público que você criou
Minerio minerio = mina.acessarExtrairMinerio();

Console.WriteLine($"Tipo de minério: {minerio.tipo}");