---

# 💰 ProjetoFinanceiro - Sistema de Controle Financeiro

Sistema backend robusto e estruturado desenvolvido em C# sob medida para a gestão de receitas, despesas e fluxo de caixa, com foco em integridade de dados e validação de regras de negócio.

---

## 🎨 Identidade Visual e Console
Como o projeto roda em ambiente .NET, a estrutura de cores sugerida para o console e relatórios de log é:
- **Verde:** (Para receitas, saldos positivos e mensagens de sucesso)
- **Vermelho:** (Para despesas, saldos negativos e alertas/erros)
- **Amarelo:** (Para categorias e avisos de sistema)
- **Branco/Cinza Claro:** (Para textos gerais de console e logs padrão)

---

## 🚀 Stack Tecnológica
- **Plataforma:** .NET 10.0 (Framework de alta performance e versões mais recentes da Microsoft).
- **Linguagem:** C# 10+.
- **Qualidade e Validação:** Framework de testes unitários nativo para garantia de estabilidade do código (`ControleFinanceiroTests.cs`).

---

## 📂 Arquitetura de Pastas

```text
ProjetoFinanceiro/
│
├── Program.cs                  # Ponto de entrada da aplicação e inicialização
├── ProjetoFinanceiro.csproj    # Configurações do projeto e dependências do .NET 10.0
│
├── Models/                     # (Estrutura lógica das classes)
│   ├── Transacao.cs            # Classe base que define o esqueleto de qualquer movimentação
│   ├── Receita.cs              # Especialização para fluxo de entrada de capital
│   ├── Despesa.cs              # Especialização para fluxo de saída de capital
│   └── CategoriaDespesas.cs    # Enumeração/Classificação de categorias de gastos
│
├── Services/                   # (Regras de Negócio)
│   └── ControleFinanceiro.cs   # Motor central que calcula saldo, cadastra e lista transações
│
└── Tests/                      # (Auditoria de Código)
    └── ControleFinanceiroTests.cs # Suíte de testes automatizados para validar a lógica financeira
```
*(Nota: as pastas lógicas refletem a organização do escopo do seu código)*

---

## 📋 Regras de Negócio Implementadas

### 1. Gestão de Transações (Polimorfismo)
- **Sistema Base:** Toda operação herda de `Transacao.cs`, garantindo que toda entrada e saída tenha data, valor e descrição obrigatórios.
- **Segregação:** Separação estrita entre `Receita` (Entradas) e `Despesa` (Saídas) para facilitar a geração de relatórios de fechamento.

### 2. Classificação de Despesas
- **Categorização:** Utilização de `CategoriaDespesas.cs` para agrupar saídas (ex: Alimentação, Transporte, Lazer, Custos Fixos).
- Permite futura expansão para criação de gráficos e limites de gastos por categoria.

### 3. Motor Financeiro (ControleFinanceiro)
- **Cadastro Centralizado:** Método único para processar e consolidar as adições de receitas e despesas.
- **Cálculo de Saldo em Tempo Real:** Consolidação matemática automática (`Total de Receitas` - `Total de Despesas`), impedindo erros de cálculo manual.
- **Histórico:** Mantém em memória a lista de todas as operações realizadas na sessão atual.

### 4. Validação e Testes (Auditoria Contínua)
- **Qualidade de Software:** O arquivo `ControleFinanceiroTests.cs` garante que modificações futuras no código não quebrem a matemática do fluxo de caixa.
- **Cenários Testados:** Valida se uma receita soma corretamente ao saldo e se uma despesa subtrai o valor exato, bloqueando deploys defeituosos.

---

## 💻 Como Executar

### Pré-requisitos
Certifique-se de ter o **SDK do .NET 10.0** instalado na sua máquina.

### Opção 1: Executar a Aplicação (Terminal)
1. Abra o terminal ou PowerShell na pasta raiz do projeto.
2. Compile e rode o código com o comando:
   ```bash
   dotnet run
   ```

### Opção 2: Executar a Suíte de Testes
Para garantir que toda a lógica e matemática financeira do sistema está correta antes de colocá-lo em uso:
1. Abra o terminal na pasta raiz.
2. Rode o comando de auditoria:
   ```bash
   dotnet test
   ```
*(O console mostrará em verde todos os testes do `ControleFinanceiroTests.cs` que passaram com sucesso).*
