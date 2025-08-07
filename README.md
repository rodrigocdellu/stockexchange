# 📈 StockExchange

Aplicativo desenvolvido como parte de um desafio prático voltado à avaliação de competências em **.NET Core (C#)** e **Angular (TypeScript)**, com foco em arquitetura de software, testes e performance. O projeto simula uma aplicação financeira para cálculo de investimentos em CDB, considerando regras tributárias e rendimentos mensais compostos.

A solução é composta por uma API REST em **.NET Core** (Back-end), responsável pela lógica de negócio e cálculos financeiros, e duas Interfaces Web (Front-end) responsivas, sendo uma em **Angular** e a outra em **React**. Ambas permitem a mesma entrada e saída de dados, ou seja, exibem o resultado bruto e líquido de um investimento CDB.

Este repositório possui fins educacionais e demonstra a aplicação de princípios SOLID, cobertura de testes unitários, boas práticas de versionamento e qualidade de código. Tudo foi implementado com foco em aprendizado e consolidação de tecnologias modernas para o desenvolvimento web.

## 📦 Conteúdo do Repositório

Este repositório possui 4 aplicações, sendo:
- StockExchange.AngularUI: Front-end Angular com Material responsável por fornecer uma interface com o usuário e realizar requisições aos serviços financeiras do Back-end .NET Core.
- StockExchange.ReactUI: Front-end React com Material responsável por fornecer uma interface com o usuário e realizar requisições aos serviços financeiras do Back-end .NET Core.
- StockExchange.WebAPI: Back-end .NET Core responsável por fornecer os serviços financeiros via API REST.
- StockExchange.WebAPI.Test: NUnit .NET Core responsável por testar o Back-end .NET Core de forma integrada e automática.

## 🛠️ Ferramentas Utilizadas

- Sistema Operacional 1: [Windows 10 Pro](#)
- Sistema Operacional 2: [Ubuntu 24.04 LTS](https://ubuntu.com/download/desktop)  
- Editor de Código 1: [Visual Studio Code v1.99.3](https://code.visualstudio.com/download)
- Editor de Código 2: [Visual Studio 2022 Community v17.13.6](https://visualstudio.microsoft.com/pt-br/downloads)  
- Gerenciador de Contêineres: [Docker Desktop v4.40.0](https://www.docker.com/products/docker-desktop)
- Analisador de Código Estático e Testes: [SonarQube Cloud](https://sonarcloud.io)  

### 🎨 Pacotes Utilizados no Front-end Angular

- Runtime JavaScript: [Node.js v22.15.0](https://nodejs.org/pt)
- Gerenciador de Pacotes: [NPM v10.9.2](https://www.npmjs.com/package/npm/v/10.9.2)
- Framework Front-end: [Angular CLI v19.2.8](https://github.com/angular/angular-cli)
- Estilização: [Angular Material v19.2.10](https://github.com/angular/angular-cli)

### 🎨 Pacotes Utilizados no Front-end React

- Runtime JavaScript: [Node.js v22.15.0](https://nodejs.org/pt)
- Gerenciador de Pacotes: [NPM v10.9.2](https://www.npmjs.com/package/npm/v/10.9.2)
- Framework Front-end: [Vite v6.3.5](https://www.npmjs.com/package/vite/v/6.3.5)
- Estilização: [React Material v7.1.0](https://www.npmjs.com/package/@mui/material/v/7.1.0)

### 🔧 Pacotes Utilizados no Back-end .NET Core

- Framework: [.NET v6.0.36 (LTS)](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0) com suporte ao [.NET v8.0.408 (LTS)](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)  
- Framework de Validação: [FluentValidation.AspNetCore v11.3.0](https://www.nuget.org/packages/fluentvalidation.aspnetcore/11.3.0)
- Framework de Teste da Microsoft: [Microsoft.NET.Test.Sdk v17.8.0](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/17.8.0)
- Framework de Teste: [NUnit v3.14.0](https://www.nuget.org/packages/NUnit/3.14.0)
- Framework de Teste Mock: [Moq v4.20.72](https://www.nuget.org/packages/Moq/4.20.72)  
- Ferramenta para Coletar Code Coverage: [coverlet.collector v6.0.4](https://www.nuget.org/packages/coverlet.collector/6.0.4)

## 🚀 Ambientes de Execução

**IMPORTANTE:**
Certifique-se de que todas as ferramentas e pacotes utilizados estejam instaladas e funcionando.
Antes de executar, verifique se as portas 4200, 5173 e 5041 estão disponíveis.
Os comandos a seguir podem ser executados no **PowerShell (Windows)** ou no **Terminal (Linux)**.

1. Baixe o repositório do [GitHub](https://github.com/rodrigocdellu/stockexchange)

```
git clone git@github.com:rodrigocdellu/stockexchange.git;
```

2. Execute a Interface com Usuário Angular com Material (Front-end Angular):

```
cd stockexchange/StockExchange.AngularUI/; npm install; ng serve
```

3. Execute a Interface com Usuário React com Material (Front-end React):

```
cd stockexchange/StockExchange.ReactUI/; npm install; npm run dev
```

4. Execute a API Web (Back-end .NET Core):

```
cd stockexchange/StockExchange.WebAPI/; dotnet run
```

5. Após a execução, você pode acessar as aplicações através dos seguintes endereços:

- Front-end Angular: [http://localhost:4200](http://localhost:4200)
- Front-end React: [http://localhost:5173](http://localhost:5173)
- Back-end .NET Core: [http://localhost:5041](http://localhost:5041)

## 💻 Ambiente de Desenvolvimento

**IMPORTANTE:**
Certifique-se de que todas as ferramentas e pacotes utilizados estejam instaladas e funcionando.
Antes de executar, verifique se as portas 4200, 5173 e 5041 estão disponíveis.
Os comandos a seguir podem ser executados no **PowerShell (Windows)** ou no **Terminal (Linux)**.
Se quiser, mude o parametro 'porta' de 5041 para 7200 do arquivo [`cdb.service.ts`](./StockExchange.AngularUI/src/app/services/cdb.service.ts) do Front-end Angular ou do arquivo [`CdbService.ts`](./StockExchange.ReactUI/src/app/services/CdbService.ts) do Front-end React. Isso alinha a porta do serviço a ser consumido.

1. Com os repositório já baixados, execute o seguinte comando para desenvolver o Front-end Angular:

```
cd stockexchange/StockExchange.AngularUI/; code .
```

2. Você também pode executar o comando abaixo para desenvolver o Front-end React:

```
cd stockexchange/StockExchange.ReactUI/; code .
```

3. E ainda pode executar o comando abaixo para desenvolver o Back-end .NET Core:

```
cd stockexchange/StockExchange.WebAPI/; code .
```

4. **Opcional**: Caso queira, você pode abrir todos os projetos no Visual Studio 2022 Community através do arquivo [`StockExchange.sln`](./stockexchange.sln).

## 🐳 Ambiente de Produção (Docker)

1. Com o **Docker** devidamente instalado, execute o seguinte comando na pasta 'stockexchange' para criar uma imagem do Front-end Angular:

```
docker build -f Dockerfile.angularui -t stockexchange.angularui .
```

2. Para o Front-end React execute o seguinte comando na pasta 'stockexchange':

```
docker build -f Dockerfile.reactui -t stockexchange.reactui .
```

3. Para o Back-end .NET Core execute o seguinte comando na pasta 'stockexchange':

```
docker build -f Dockerfile.webapi -t stockexchange.webapi .
```

4. Após a criação da imagem, inicie o contêiner do Front-end Angular com o comando:

```
docker run --name stockexchange.angularui -d -p 7000:80 stockexchange.angularui
```

5. Repita o processo para o Front-end React:

```
docker run --name stockexchange.reactui -d -p 7100:80 stockexchange.reactui
```

6. Repita o processo para o Back-end .NET Core:

```
docker run --name stockexchange.webapi -d -p 7200:80 stockexchange.webapi
```

7. Após a execução dos containers, você pode acessar as aplicações através dos seguintes endereços:

- Front-end Angular: [http://localhost:7000](http://localhost:7000)
- Front-end React: [http://localhost:7100](http://localhost:7100)
- Back-end .NET Core: [http://localhost:7200](http://localhost:7200)

8. **Opcional**: Caso não consiga construir as imagens, você pode baixá-las do meu Docker Hub:

- [Front-end Angular](https://hub.docker.com/r/rodrigocdellu/stockexchange.angularui)
- [Front-end React](https://hub.docker.com/r/rodrigocdellu/stockexchange.reactui)
- [Back-end .NET Core](https://hub.docker.com/r/rodrigocdellu/stockexchange.webapi)

9. **Opcional**: Após baixadas as imagens do meu Docker Hub, é só executar os seguintes comnandos:

```
docker run --name stockexchange.angularui -d -p 7000:80 rodrigocdellu/stockexchange.angularui:1.0.0;
docker run --name stockexchange.reactui -d -p 7100:80 rodrigocdellu/stockexchange.reactui:1.0.0;
docker run --name stockexchange.webapi -d -p 7200:80 rodrigocdellu/stockexchange.webapi:1.0.0;
```

10. **Opcional**: Você também pode subir o ambiente de produção via Docker-Compose. Para isso execute o comando abaixo na pasta raiz do projeto (stockexchange):

```
docker compose up
```

## 🤍 Clean Code

Aqui disponibilizo os [resultados da analise estática de cógido](https://sonarcloud.io/project/overview?id=rodrigocdellu_stockexchange) com as configurações padrão do SonarQube Cloud.

## 🤝 Contribuições

Contribuições são bem-vindas! Se você deseja sugerir melhorias, corrigir bugs ou adicionar novas funcionalidades, sinta-se à vontade para abrir uma [Issue](https://github.com/rodrigocdellu/stockexchange/issues) ou enviar um *Pull Request*.

Por favor, siga as boas práticas de desenvolvimento e, se possível, adicione testes automatizados relacionados às suas alterações.

## 📄 Licença

Este projeto está licenciado sob os termos da **MIT License**. Para mais informações, consulte o arquivo [`LICENSE`](./LICENSE.md).

## 📬 Contato

Caso tenha dúvidas, sugestões ou feedbacks, entre em contato:

- Email: [rodrigocdellu.trabalho@outlook.com](mailto:rodrigocdellu.trabalho@outlook.com)
- LinkedIn: [linkedin.com/in/rodrigocdellu](https://linkedin.com/in/rodrigocdellu)

---

> "_O SENHOR é misericordioso e compassivo, tardio em irar-se e grande em amor leal._"  
> — Salmos 145:8
