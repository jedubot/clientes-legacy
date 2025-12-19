# Clientes.WebApi   

## 1. Introdução

Esta aplicação é uma Web API simples que realiza as operaçÕes CRUD para uma única entidade Cliente. 

Foi construída utilizando a seguinte infraestrutura:

1) Windows 11 Pro
2) Visual Studio 2026 Enterprise
3) .NET 10.0
4) C# 14

## 2. Estrutura de Pastas   

- ./ docs
    - Clientes.WebApi.drawio
    - Clientes.WebApi.postman_collection.json
- ./ src
    - ./ Clientes.WebApi
        - ./ Controllers
            - ClientesController.cs
        - ./ Interfaces
            - IClienteService.cs
            - IRepository.cs
        - ./ Models
            - Cliente.cs
        - ./ Properties
            - launchSettings.json
        - ./ Repositories
            - ./ EF
                - ClienteMapping.cs
                - ClientesDbContext.cs
            - Repository.cs
        - ./ Services
            - ClienteService.cs
        - appsettings.json
        - Clientes.WebApi.csproj
        - packages.config
        - Program.cs
    - Clientes.slnx
- README.md

## 3. Arquitetura

### 3.1. Apresentação

#### Controllers/ClientesController.cs

Ponto de entrada da API, recebe requisições HTTP, executa a lógica da aplicação (via serviços) e retorna respostas HTTP.

### 3.2. Serviços

#### Services/ClienteService.cs

Responsável por executar a lógica da aplicação, deixando o Controller responsável somente por lidar com HTTP, rotas e códigos de status. Implementa a interface "Interfaces/IClienteService.cs".

### 3.3. Dados

#### Repositories/EF/ClienteDbContext.cs

Esta classe herda de DbContext, classe do Entity Framework Core (ORM .NET) responsável por agir como um gateway para o banco de dados. Gerencia a conexão ao banco, possui coleções que representam tabelas, rastreiam as entidades e realizam operações de leitura e escrita na base.

A aplicação vem com uma base de dados em memória, ou seja, existe somente durante o ciclo de vida da aplicação. Foi feito desta forma para funcionar imediatamente em qualquer ambiente.

#### Repositories/EF/ClienteMapping.cs

Ainda no contexto do Entity Framework Core, esta classe mapeia as propriedades da classe Cliente às colunas de sua tabela correspondente no banco de dados.

#### Repositories/Repository.cs

Esta classe se situa entre a camada de serviço e o DbContext, conferindo à aplicação um jeito limpo, testável e consistente de acessar o banco de dados, abstraindo os detalhes do Entity Framework Core do restante da aplicação. Implementa, de forma genérica, as principais operações de banco de dados, podendo ser reusada pelas entidades da aplicação. Implementa a interface "Interfaces/IRepository.cs".

### 3.4. Domínio

#### Models/Cliente.cs

A classe Cliente perpassa todas as camadas, atuando como Model na API e como Entidade na camada de dados. É uma entidade anêmica; ou seja, sem comportamento, apresentando somente propriedades.









