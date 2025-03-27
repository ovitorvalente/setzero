# **SetZero**

> **Aplicação para redefinir valores de movimentação no banco de dados**

## **📌 Descrição do Projeto**

**SetZero** é uma aplicação desktop desenvolvida em **C#** com **WPF (Windows Presentation Foundation)**, projetada para redefinir os valores de movimentação em um banco de dados SQL Server. Através de uma interface intuitiva, o usuário pode inserir os dados necessários (Filial, Sequência e Linha) e, com um simples clique ou atalho de teclado, redefinir os valores correspondentes para zero.

>Este projeto foi desenvolvido para atender uma demanda específica de um cliente que utiliza o software **E-Trade**.

---
## **🛠 Tecnologias Utilizadas**

- **C# & .NET**
- **WPF (Windows Presentation Foundation)**
- **Dapper** – Micro ORM para interação com o SQL Server
- **SQL Server**
- **Newtonsoft.Json** – Para leitura de configurações em formato JSON

---
## **🎨 Interface do Usuário**

A interface foi projetada para simplicidade e eficiência, permitindo ao usuário inserir rapidamente os dados necessários para zerar os valores de movimentação.
### **Campos de Entrada:**
- **Filial**: Código da filial associada à movimentação
- **Sequência**: Número da sequência da movimentação
- **Linha**: Número do item que terá seu valor zerado
### **Ações e Funcionalidades:**
- **Botão "Zerar [F2]"**: Inicia o processo de redefinir os valores para zero.
- **Status Bar**: Exibe mensagens informativas durante o processo.
- **Atalho de Teclado**: Pressionando `F2`, o processo de zeragem é iniciado automaticamente.

---
## **⚙️ Como Funciona**

### **Banco de Dados**
O sistema realiza operações diretamente no banco de dados **SQL Server**, executando os seguintes passos:
- **Consulta IDE do movimento**
- **Verificação da existência da linha do item**
- **Consulta do valor do item**
- **Atualização do valor para zero, caso o valor não seja zero**
- **Atualização do valor total da movimentação**

### **Configuração do Banco de Dados**
As configurações do banco de dados são carregadas a partir do arquivo **ArqID.txt** no formato JSON. Exemplo de estrutura do arquivo:
```json
{
  "DataSource": "servidor.database.windows.net",
  "User": "usuario",
  "Password": "senha",
  "Timeout": 30
}
```

---
## **🖥 Atalhos de Teclado**

| Atalho | Ação                              |
| ------ | --------------------------------- |
| `F2`   | Zera os valores das movimentações |

---
## **🔧 Como Executar o Projeto**
1. **Baixe a versão mais recente do arquivo executável** na seção de **Releases** do repositório [SetZero no GitHub](https://github.com/ovitorvalente/SetZero/releases).
2. **Copie o arquivo `.exe`** para a pasta raiz do **E-Trade** no seu sistema.
3. **Abra o arquivo `.exe`** diretamente a partir da pasta raiz do **E-Trade** para executar o programa.
4. **Configure o arquivo `ArqID.txt`** com as credenciais do banco de dados. Este arquivo deve estar na mesma pasta onde o executável está localizado.
5. O programa está pronto para ser utilizado! Basta seguir as instruções na interface para redefinir os valores de movimentação.

---
## **📌 Contribuições**
Contribuições são bem-vindas! Se você quiser ajudar no desenvolvimento do projeto, siga as instruções abaixo:

1. **Fork o repositório**: Clique no botão "Fork" no canto superior direito para criar uma cópia do projeto.

2. **Crie uma branch**: Sempre crie uma branch separada para suas mudanças.
```bash
git checkout -b minha-nova-funcionalidade
```

3. **Faça suas alterações** e commit:
```bash
git commit -m "Adicionando nova funcionalidade"
```

4. **Push as mudanças para o seu repositório remoto**:
```bash
git push origin minha-nova-funcionalidade
```

5. **Abra um Pull Request**: Descreva as mudanças feitas e submeta o PR para revisão. 

---
## **📄 Licença**
>**© 2025 Vitor Valente. Todos os direitos reservados.**