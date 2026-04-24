Sobre o projeto
Este projeto foi desenvolvido como um sistema de registro de pedidos para a lanchonete Good Hamburger. O objetivo foi construir uma API REST completa em C# com .NET 8 e ASP.NET Core, seguindo boas práticas de arquitetura e organização de código, além de um frontend em Blazor WebAssembly para consumir a API.

Como executar
Pré-requisitos

.NET 8 SDK instalado
Visual Studio 2022

Passos
Clone o repositório:
git clone https://github.com/seu-usuario/GoodHamburger.git
Abra o arquivo GoodHamburger.sln no Visual Studio.
Clique com o botão direito na Solution → Properties → Multiple Startup Projects e configure tanto GoodHamburger.API quanto GoodHamburger.Web com a action Start.
Pressione F5 para rodar. A API vai abrir em http://localhost:5000 e o frontend Blazor em https://localhost:7283.
Para rodar os testes abra o menu Test → Test Explorer e clique em Run All.

Arquitetura
Optei por utilizar Clean Architecture dividindo o projeto em quatro camadas com responsabilidades bem definidas.
GoodHamburger.Domain
É o núcleo do sistema e não depende de nenhuma outra camada. Aqui ficam as entidades, enums, exceções de domínio e value objects. A decisão de colocar as regras de negócio aqui foi intencional — toda a lógica de cálculo de desconto e validação de itens duplicados vive dentro da entidade Pedido, sem depender de banco de dados, HTTP ou qualquer detalhe externo. Isso garante que as regras funcionem de forma isolada e possam ser testadas com facilidade.
GoodHamburger.Application
Contém os casos de uso do sistema através do PedidoServico. Também define as interfaces IPedidoRepositorio e IPedidoServico que são contratos seguidos pelas camadas externas. Os DTOs ficam aqui para evitar expor as entidades diretamente para o cliente. A decisão de usar interfaces nessa camada foi para desacoplar a lógica de aplicação dos detalhes de implementação, permitindo por exemplo trocar o banco de dados sem alterar nada no Application.
GoodHamburger.Infrastructure
Responsável pela persistência dos dados. Aqui ficam o AppDbContext e o PedidoRepositorio. Utilizei o Entity Framework Core com banco de dados InMemory para facilitar a execução local sem necessidade de instalar ou configurar um banco de dados externo. Os value objects MenuItem foram mapeados como owned entities no EF Core através do método OwnsOne, o que evita a criação de tabelas separadas para eles.
GoodHamburger.API
Camada de apresentação que expõe os endpoints REST. Os controllers são intencionalmente simples — recebem a requisição, delegam para o service e devolvem a resposta. Todo o tratamento de erros foi centralizado no ExcecaoMiddleware, que captura exceções do domínio e devolve respostas JSON padronizadas com os status HTTP corretos, evitando a necessidade de blocos try/catch em cada controller.

Tecnologias utilizadas
ASP.NET Core 8 foi escolhido por ser o framework padrão da Microsoft para APIs REST em .NET, com excelente suporte a injeção de dependência, middlewares e Swagger.
Entity Framework Core com InMemory foi utilizado para simplificar a execução local. Em um ambiente de produção bastaria trocar a linha UseInMemoryDatabase por UseSqlServer com a connection string do banco.
Blazor WebAssembly foi escolhido para o frontend por permitir escrever a interface em C#, mantendo consistência com o restante do projeto. O Blazor se comunica com a API via HttpClient fazendo chamadas REST da mesma forma que qualquer frontend moderno.
xUnit foi utilizado para os testes automatizados por ser o framework de testes mais adotado no ecossistema .NET. Os testes cobrem todos os cenários de desconto e as validações de itens duplicados diretamente na entidade Pedido, sem necessidade de banco de dados ou mocks.

Regras de negócio
O sistema implementa as seguintes regras de desconto:

Lanche + Batata Frita + Refrigerante: 20% de desconto
Lanche + Refrigerante: 15% de desconto
Lanche + Batata Frita: 10% de desconto
Apenas lanche ou qualquer combinação sem lanche: sem desconto

Cada pedido aceita no máximo um item de cada tipo. Caso o cliente tente adicionar dois lanches, duas batatas ou dois refrigerantes a API retorna um erro 400 com uma mensagem clara informando o problema.

Endpoints disponíveis

GET /api/cardapio — retorna todos os itens do cardápio
GET /api/pedidos — retorna todos os pedidos
GET /api/pedidos/{id} — retorna um pedido pelo ID
POST /api/pedidos — cria um novo pedido
PUT /api/pedidos/{id} — atualiza os itens de um pedido existente
DELETE /api/pedidos/{id} — remove um pedido


Decisões técnicas
Por que Clean Architecture? Para separar as responsabilidades de forma clara. As regras de negócio ficam isoladas no Domain e não são afetadas por mudanças na API ou no banco de dados.
Por que interfaces para o repositório e o serviço? Para desacoplar as camadas. O controller não sabe como o serviço é implementado e o serviço não sabe como o repositório acessa o banco. Isso facilita testes e manutenção.
Por que centralizar os erros no middleware? Para evitar repetição de código. Sem o middleware cada controller precisaria de try/catch para tratar os mesmos erros. Com o middleware isso é feito em um único lugar para toda a aplicação.
Por que record para os DTOs e value objects? O tipo record em C# é imutável por padrão e tem igualdade por valor, o que é exatamente o comportamento esperado para DTOs e value objects que não devem ser alterados depois de criados.