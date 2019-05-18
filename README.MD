Solução construído utilizando o .Net Core versão 2.1.1
Estrutura da solução:
	A solução contém os projetos
	BexsTestDomain:
		Contém as classes de domínio utilizadas no projeto.
	BexsTestBS:
		Projeto com as classes de lógica de negócio, leitura e escrita de arquivos.
	BexsTest:
		Console Application para a execução do projeto.
	APIBexsTest:
		Web Api para consumo das classes de negócio.
		
Execução do programa:
	Console Application:
		Visual Studio - Configurar o projeto BexsTest com o argumento de inicialização para com o caminho para o arquivo de input.
		
		Shell do windows - executar no caminho da compilação o comando "dotnet BexsTest.dll CAMINHO_DO_ARQUIVO"
		
	WEB API:
		O arquivo de input da API deve estar na pasta Resources dentro do caminho de compilação
	
		Visual Studio - Definir o projeto APIBexsTest como projeto de inicialização, executar o projeto e utilizar algum client de geração de requests (Postman, SoapUI) para realizar o teste.
		Exemplos de requisição:
			Requisição POST https://localhost:44394/api/BexsTest/WriteNewRoute?origin=BRC&destination=TKY&cost=28
			Adiciona ao arquivo de input uma nova Rota de BRC para TKY com o custo de 28.
			
			Requisição GET
			https://localhost:44394/api/BexsTest/FindBestPath?origin=GRU&destination=ORL
			Obtém um objeto serializado em JSON do tipo TravelPath, com todos os aeroportos utilizados para encontrar o caminho com o menor custo
			
		Shell do windows - executar no caminho da copilação o comando "dotnet APIBexsTest.dll" e utilizar algum client de geração de requests (Postman, SoapUI) para realizar o teste.
		Exemplos de requisição:
			Requisição POST https://localhost:5001/api/BexsTest/WriteNewRoute?origin=BRC&destination=TKY&cost=28
			Adiciona ao arquivo de input uma nova Rota de BRC para TKY com o custo de 28.
			
			Requisição GET
			https://localhost:5001/api/BexsTest/FindBestPath?origin=GRU&destination=ORL
			Obtém um objeto serializado em JSON do tipo TravelPath, com todos os aeroportos utilizados para encontrar o caminho com o menor custo
		
Codificação:
	Visando a facilidade de implementação de testes e possiveis expansões do programa a injeção de dependência foi utilizada em todo o código.
	Também utilizei uma logica de singleton com cache para a leitura do arquivo de input.
	Foram criados testes unitários utilizando NUnit para as funcionalidades de negócio da aplicação, utilizando classes de Mock injetadas para a manipulação dos arquivos .
