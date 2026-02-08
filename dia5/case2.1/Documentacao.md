**Documentação CASE – Sistema de Controle de Check-in 
de Visitantes**

**Introdução**
 Criação de um sistema de validação de visitantes de um coworking, possuindo cadatro de visitante, lista de visitantes cadastrados, busca de visitantes e registro de saída. o sistema possui um validador de cpf, para ter o risco de usuários insrirem números inválidos.


 **Sistema**
 O sistema possui validações e verifiações rodando no seu back-end, com conceito de programação orientado a objeto. Utilizamos varios tipos de váriaveis.

- int para o Id. 
- string para o castro do nome do visitante e documento.
- DateTime para a inserção de tempo de entrada e saída do visitante.
- bool para a validação de verdadeiro ou falso, perguntado a usuário se é a primeira vez no coworking.

**Estrutura de código do sistema**
O programa possui três arquivos:
 
- Programa.cs
O ponto de entrada do código, onde possui o list para armazenar os visitantes, com o list não temos a limitação da quantidade de usuários cadastrados.

- try e catch para para caso o usuário inserir alguma informção errada a compilação ser encerrada.

 **Execução**
Para executar o programa é necessario ter instalado no computador:

- IDE(visul studio code)
- .NET 
- extensão do C# no vscode

Após a instação desses componentes, abra o terminarl dentro da pasta e siga o pass a passo abaixo:

- dotnet new console 
- dotnet run