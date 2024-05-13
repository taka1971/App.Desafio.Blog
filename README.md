
API para realizar a publicação de um BLOG
====

Tecnologias aplicadas: NET 8, c#, Entity Framework Core, SignalR
Aplicado padrões de projeto: Como Clean Code, SOLID, Design Patterns como:
Singleton, Factory, Repository, Observer.

Resumo
===========
Esta aplicação tem por finalidade realizar 
um blog simples, contendo apenas o Título e 
a descrição da publicação.

Para realizar uma publicação o usuário, deverá
se cadastrar para isso, onde deverá informar
um email e uma senha.

Para leitura de publicações o usuário não
necessita de estar autenticado.

Para demais operações em relação ao Blog,
como adicionar, alterar ou excluir, deverá
estar autenticado.

Instruções para Uso
===================

1-Possuir o Docker instalado
2-Clonar o repositorio
3-localizar o diretório \App.Desafio.Blog onde se encontrar os arquivos do docker 
para utilização ( dockerfile e docker-compose.yml)
4-No prompt do seu sistema ( windows ou unix ) executar o comando
docker-compose up --build

5-Na pasta \App.Desafio.Blog\App.Desafio.Blog.Infra.Data
executar as migrations:
  5.1 - dotnet ef database update

6-Abrir a solution \App.Desafio.Blog\App.Desafio.Blog.sln

7-Executar a aplicação.

Para validação referente a notificação de novos posts,
foi criado um background service utilizando o SignalR
como um Hub.

Quando uma mensagem nova for adicionada uma mensagem no console 
vai ser printada.









