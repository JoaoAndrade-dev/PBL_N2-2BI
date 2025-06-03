# Sistema de Monitoramento Térmico IoT para Estufas de Motores Elétricos

Projeto desenvolvido como parte das disciplinas dos cursos de Engenharia, com foco em aplicar conceitos de Internet das Coisas (IoT), controle térmico e automação industrial, promovendo a modernização e eficiência dos processos de secagem de motores elétricos.

---

## 👨‍🔧 Descrição do Projeto

Este projeto tem como foco o desenvolvimento de um sistema de **monitoramento térmico baseado em IoT** voltado para **estufas utilizadas na secagem de motores elétricos**. O sistema visa proporcionar **maior controle e segurança térmica**, reduzindo desperdícios energéticos e promovendo maior uniformidade no processo de secagem.

A solução integra sensores conectados a um **ESP32**, que transmite os dados via **protocolo MQTT** ao **FIWARE Orion Context Broker**. As informações captadas são armazenadas e visualizadas por meio de uma **aplicação web ASP.NET Core MVC**, oferecendo **dashboards interativos, histórico de leituras e ferramentas de consulta para análise técnica**.

---
## 🧱 Estrutura do Sistema (Arquitetura FIWARE + MQTT)

A arquitetura do sistema foi implementada com base em contêineres Docker, utilizando componentes da plataforma **FIWARE** para coleta, gerenciamento e visualização de dados térmicos provenientes de sensores conectados via **protocolo MQTT**. A estrutura é composta por três camadas principais:

![Diagrama de Arquitetura](https://exemplo.com/FiwareDeploy_Final.jpg)

### 🔌 1. Camada IoT

- **Sensores**: Dispositivos instalados na estufa para realizar a medição da temperatura em tempo real.
- **MQTT Broker (Mosquitto)**: Responsável por receber as mensagens dos sensores. Funciona como intermediário na comunicação MQTT.
  - Porta: `1883`
  - Protocolo: MQTT

### 🖥️ 2. Camada Back-end (FIWARE)

#### a) **IoT Agent MQTT**
- Traduz mensagens MQTT vindas dos sensores em entidades compreendidas pelo **FIWARE Orion Context Broker**.
- Conecta-se ao MongoDB Interno para armazenar metadados de contexto.
- Comunicação bidirecional com o MQTT Broker e Orion.

#### b) **Orion Context Broker**
- Componente central do FIWARE que gerencia o estado atual das entidades (neste caso, sensores de temperatura).
- Atua como ponto de integração entre dispositivos e aplicações.
- Porta: `1026`

#### c) **MongoDB (Interno)**
- Armazena os dados de contexto das entidades ativas que o Orion utiliza para fornecer os dados atuais.

#### d) **STH-Comet**
- Componente FIWARE responsável por armazenar o **histórico de alterações das entidades** registradas no Orion.
- Funciona em conjunto com um **MongoDB Histórico**, onde ficam armazenadas as séries temporais.

#### e) **MongoDB (Histórico)**
- Base de dados para persistência dos dados históricos que alimentam os dashboards e análises futuras.

### 🧑‍💼 3. Camada Application (Frontend)

#### a) **RunTime Dashboard**
- Dashboard de monitoramento em **tempo real**, consumindo dados diretamente do Orion Context Broker via API REST.
- Porta: `4041`

#### b) **Historical Dashboard**
- Interface para **consultas e análises históricas**, utilizando os dados armazenados no MongoDB Histórico via STH-Comet.
- Porta: `8666`

### 📦 Orquestração com Docker Compose

Todos os serviços acima (Orion, IoT Agent, STH-Comet, MongoDB, Mosquitto) são instanciados via **Docker Compose**, o que facilita o deploy, escalabilidade e manutenção do sistema em ambientes de desenvolvimento ou produção.

### 🔁 Fluxo de Dados Resumido

1. **Sensores** → enviam dados via **MQTT** → para o **MQTT Broker (Mosquitto)**  
2. **MQTT Broker** → encaminha os dados para o **IoT Agent MQTT**  
3. **IoT Agent** → traduz os dados e envia para o **Orion Context Broker**  
4. **Orion** → atualiza os estados das entidades e armazena no **MongoDB Interno**  
5. **STH-Comet** → captura alterações e grava histórico no **MongoDB Histórico**  
6. **Dashboards (Application)** → consomem os dados atuais (RunTime) e históricos (Historical) via APIs REST

---

## 🎯 Objetivos

- Coletar e exibir dados em tempo real de temperatura;
- Armazenar informações para consulta e análise histórica;
- Oferecer dashboards e filtros inteligentes para acompanhamento;
- Facilitar a tomada de decisões sobre o processo de secagem;
- Garantir que os motores estejam dentro das **classes térmicas seguras** estabelecidas por normas técnicas.

---

## 🌡️ Classes de Isolamento Térmico

As classes térmicas seguem normas como **IEC 60085, NEMA e ABNT**, e indicam a **temperatura máxima contínua** que os materiais de isolamento dos motores suportam.

| Classe | Temperatura Máxima | Materiais Utilizados                                                   |
|--------|---------------------|------------------------------------------------------------------------|
| A      | Até 105°C           | Papel, celulose ou algodão                                             |
| E      | Até 120°C           | Papéis celulósicos, poliéster, vernizes sintéticos                     |
| B      | Até 130°C           | Mica com vernizes de poliéster ou epóxi, fibra de vidro               |
| F      | Até 155°C           | Mica, filmes de poliéster e fibra de vidro tratada                    |
| H      | Até 180°C           | Mica, silicone, poliimida                                              |
| N      | Até 200°C           | Poliimida, poliéster-imida, vernizes modificados ou fluorados         |
| R      | Até 220°C           | Poliimida de alta performance, Teflon                                  |
| S      | Até 240°C           | Compósitos de poliimida de alta performance, Teflon, fibras sintéticas |

---

## 👥 Público-alvo

- Engenheiros e técnicos de manutenção elétrica;
- Operadores de estufas industriais;
- Gestores de qualidade e produção;
- Equipes de engenharia de processo e automação.

---

## ❗ Problemas que o projeto visa resolver

- Monitoramento impreciso da temperatura em estufas industriais;
- Riscos de superaquecimento e danos aos motores;
- Falta de rastreabilidade histórica das condições de secagem;
- Baixa eficiência energética;
- Ausência de controle remoto e análises gráficas dos dados.

---

## 🗃️ Componentes do Sistema

### 🔌 Hardware

- **ESP32**: Microcontrolador responsável pela coleta e transmissão dos dados;
- **Sensores de temperatura**: Dispositivos conectados ao ESP32 para capturar as leituras térmicas;
- **Rede Wi-Fi/MQTT Broker**: Comunicação com o FIWARE Orion Context Broker.

### 💻 Software

- **FIWARE Orion Context Broker**: Gerencia os dados IoT recebidos em tempo real;
- **ASP.NET Core MVC**: Aplicação web para visualização dos dados;
- **Banco de Dados SQL Server**: Armazenamento das informações de sensores e histórico;
- **Dashboard Web**: Visualização de gráficos, dados em tempo real e histórico.

---

## 💻 Tecnologias Utilizadas

| Tecnologia            | Descrição                                                      |
|------------------------|----------------------------------------------------------------|
| ASP.NET Core MVC       | Desenvolvimento da aplicação web e dashboards                 |
| C#                     | Lógica de backend                                              |
| HTML5 / CSS3 / JS      | Interface gráfica e interações                                |
| SQL Server             | Armazenamento dos dados históricos                            |
| ESP32                  | Microcontrolador IoT                                           |
| MQTT                   | Protocolo leve de comunicação entre dispositivos e servidores |
| FIWARE Orion Context Broker | Plataforma de gerenciamento de contexto para IoT             |

---

## 🧪 Testes e Visualização

- A aplicação permite testes com dados reais via sensores;
- Exibição em tempo real na aplicação web;
- Visualização histórica e filtros por data/classe térmica;
- Dashboards interativos com alertas de temperatura fora dos padrões.

---

## ▶️ Como Rodar o Projeto

1. **Configurar o Banco de Dados**:
   - Criar um banco de dados SQL Server local ou em nuvem;
   - Atualizar a `connection string` no `appsettings.json`.

2. **Executar a Aplicação Web**:
   - Abrir o projeto na IDE (Visual Studio ou VS Code);
   - Executar o projeto ASP.NET Core MVC.

3. **Conectar o ESP32**:
   - Subir o firmware no ESP32 com a lógica de leitura e envio via MQTT;
   - Garantir que o broker MQTT e o FIWARE estejam configurados e ativos.

---

## 👨‍💻 Integrantes do Grupo

| Nome                             | RA           |
|----------------------------------|--------------|
| Aline Cristina Ribeiro de Barros| 081230021    |
| Ezequiel Rodrigues Pereira       | 081230008    |
| Luis Gustavo de Oliveira Carneiro| 081230029    |
| João Victor Pereira Andrade      | 081230010    |
| Roger Rocha da Silva             | 081230045    |

---

## 🎓 Orientadores

- Prof. **Eduardo Rosalem Marcelino** – Linguagem de Programação I  
- Prof. **Fábio Cabrini** – Sistemas Embarcados  
- Prof. **Ricardo Calvo Costa** – Fenômenos de Transporte  
- Prof. **Marcones Brito** – Controle e Automação  
- Prof. **Márcio Rodrigues da Silva** – Mecânica dos Sólidos  

---

## 📌 Observações

Este projeto possui finalidade educacional, podendo ser expandido futuramente com:
- Aplicativo mobile para visualização dos dados;
- Alertas e notificações via e-mail ou push;
- Inteligência artificial para previsão de falhas térmicas;
- Integração com sistemas de controle industrial (SCADA, PLCs, etc).

---
