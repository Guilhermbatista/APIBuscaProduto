# APIBuscaProduto

Guia de Início Rápido
Bem-vindo ao APIBuscaProduto! Este é um guia rápido para ajudá-lo a começar a testar a API CRUD conectada ao MySQL em sua própria máquina.

Pré-requisitos
Antes de começar, certifique-se de ter o seguinte instalado em sua máquina:

.NET 8 SDK
MySQL Server
Git

Instalação
Clone o repositório do GitHub:
git clone https://github.com/seuusuario/projeto-x.git
Acesse o diretório do projeto:
cd projeto-x

Configuração do MySQL

Certifique-se de que o MySQL Server esteja em execução em sua máquina.
Configure as credenciais de conexão com o MySQL no arquivo appsettings.json no projeto:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=projetoxdb;Uid=seu_usuario;Pwd=sua_senha;"
  }
}

Crie um banco de dados MySQL para o projeto. Você pode fazer isso através de um cliente MySQL como o MySQL Workbench ou através do terminal:
dotnet ef add migration "ProjetoX"
dotnet ef update migration
