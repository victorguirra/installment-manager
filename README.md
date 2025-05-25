📄 Installment Manager

Este projeto tem como objetivo o gerenciamento de **contratos** e **antecipações de parcelas**, permitindo que usuários possam solicitar, aprovar ou reprovar antecipações por meio de uma aplicação web simples e funcional.

---

## 🧱 Tecnologias Utilizadas

### Backend
- **Linguagem:** C#
- **Framework:** .NET 8
- **Banco de Dados:** PostgreSQL (executado via Docker)
- **Arquitetura:** Clean Architecture + DDD
- **Autenticação:** JWT (JSON Web Token)

### Frontend
- **Framework:** React
- **Linguagem:** TypeScript
- **Empacotador:** Vite
- **Bibliotecas:**
  - Axios (requisições HTTP)
  - Material UI (componentes visuais)
  - Context API (gerenciamento de autenticação)

---

## 🖼️ Funcionalidades

### 🔐 Autenticação
- Tela de login e cadastro de usuários
- Geração de **JWT** após login
- O token JWT deve ser enviado no cabeçalho das requisições autenticadas

### 📄 Contratos
- Listagem de contratos do usuário logado
- Criação de novo contrato com:
  - Descrição
  - Valor total
  - Número de parcelas
- Exclusão de contrato com modal de confirmação

### 💰 Parcelas
- Listagem de parcelas vinculadas a um contrato
- Exibição do status de antecipação de cada parcela
- Botão "Antecipar parcelas" permite selecionar múltiplas parcelas
- Envio de solicitação de antecipação
- Acompanhamento das solicitações pendentes
- Aprovação ou reprovação das antecipações
- Exibição de mensagens de erro ou regras violadas na interface

---

## ⚙️ Requisitos para Executar

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Node.js (v18+)](https://nodejs.org/)
- [Docker](https://www.docker.com/) e [Docker Compose](https://docs.docker.com/compose/)

---

## 🐘 Banco de Dados PostgreSQL

O sistema exige que um banco de dados PostgreSQL esteja **rodando localmente na porta 5432** com os seguintes dados:

- **Host:** localhost  
- **Port:** 5432  
- **Database:** INSTALLMENTMANAGER  
- **Username:** postgres  
- **Password:** postgres  

Você pode executar com Docker usando o seguinte comando:

```bash
docker run --name installment-postgres \\
  -e POSTGRES_USER=postgres \\
  -e POSTGRES_PASSWORD=postgres \\
  -e POSTGRES_DB=INSTALLMENTMANAGER \\
  -p 5432:5432 \\
  -d postgres
```

## Observação
Devido ao prazo curto e à falta de especificação clara, foi implementada a funcionalidade de o próprio cliente poder aprovar sua solicitação de antecipação. No entanto, é de conhecimento do desenvolvedor que, em um cenário ideal, esse tipo de aprovação deveria ser realizado por um outro perfil de usuário, com permissões específicas.

Além disso, não foram implementados todos os serviços e funcionalidades esperadas em um sistema completo, como por exemplo:
- Edição de contratos
- Edição de parcelas
- Cancelamento de antecipações
- Logout
- Gestão de perfis e permissões

Essas funcionalidades foram deixadas de fora para priorizar a entrega das funcionalidades principais dentro do prazo estabelecido.
