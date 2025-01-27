PROJETO TESTE

Instruções para execução:

Configuração do banco de dados:
Altere a connectionString no arquivo appsettings.json para apontar para um banco de dados SQL Server disponível.

Aplicar migrações:
Após configurar a connectionString, execute o comando Update-Database para aplicar a migração inicial no banco de dados.

Estrutura do Projeto:
O projeto é organizado em duas camadas principais:
Controllers: Responsável por expor os endpoints da API, servindo como uma interface para as requisições externas.
Services: Contém a lógica de negócios e as regras de aplicação. A camada de serviços também gerencia o acesso ao banco de dados por meio do DbContext, encapsulando as operações mais sensíveis.

Tecnologias Utilizadas:
Entity Framework: Utilizado para mapear as entidades do projeto para um banco de dados SQL Server (MSSQL).
LINQ: Utilizado para consultas mais complexas, sendo mais legível e integrado ao código.
Variáveis de ambiente: Estão expostas para facilitar o uso e a configuração da aplicação, tornando o processo de desenvolvimento mais ágil. Porém, em um ambiente de produção, recomenda-se tomar medidas de segurança adicionais.

Documentação e Testes da API:
Ao rodar o projeto, a documentação da API será gerada automaticamente no Swagger, onde os usuários poderão testar as rotas disponíveis.
Para testar a rota protegida, é necessário adicionar um token de autenticação. O token pode ser obtido por meio da rota Auth e, em seguida, deve ser adicionado no campo "Authorize" no Swagger como Bearer %TOKEN%.

Funcionalidade da API:
O projeto oferece funcionalidades de CRUD para todas as entidades.
Além disso, existem rotas específicas que implementam lógicas de negócios detalhadas.
