# CRUD de Filmes - Backend

## Como rodar o projeto

Instale as dependências:

npm install

Crie um arquivo chamado `.env` na raiz do projeto e adicione:

DATABASE_URL="file:./app.db"

Configure o banco de dados:

npx prisma migrate dev

Gere o Prisma Client:

npx prisma generate

Inicie o servidor:

npm run dev

O backend será iniciado em:

http://localhost:3001


## Autor

Feito por Gabriel Amaral.