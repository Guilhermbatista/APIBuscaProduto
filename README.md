# APIBuscaProduto

<h2>Guia de Início Rápido</h2>
Bem-vindo ao APIBuscaProduto! Este é um guia rápido para ajudá-lo a começar a testar a API CRUD conectada ao MySQL em sua própria máquina.

<h2>Pré-requisitos</h2>
Antes de começar, certifique-se de ter o seguinte instalado em sua máquina:

.NET 8 SDK<br>
MySQL Server<br>
Git

<h2>Instalação</h2>
<p>
Clone o repositório do GitHub:
git clone https://github.com/seuusuario/projeto-x.git 
Acesse o diretório do projeto:
cd projeto-x
</p>

<h2>Configuração do MySQL:</h2>

<p> Certifique-se de que o MySQL Server esteja em execução em sua máquina.</p>
<p>Configure as credenciais de conexão com o MySQL no arquivo appsettings.json no projeto:</p>
<p>
{
  "ConnectionStrings": {</br>
    "DefaultConnection": "Server=localhost;Port=3306;Database=projetoxdb;Uid=seu_usuario;Pwd=sua_senha;"
  <br>}
}
</p>

<p>Crie um banco de dados MySQL para o projeto. Você pode fazer isso através de um cliente MySQL como o MySQL Workbench ou através do terminal:</p>
<p>dotnet ef add migration "ProjetoX"</p>
<p>dotnet ef update migration</p>
