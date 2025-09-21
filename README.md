# Semantic Kernel + Mem0 Memory  C# .NET Example

This repository demonstrates **how to integrate memory into Microsoft Semantic Kernel** using a lightweight in-process memory provider called **Mem0**. The solution is structured as a multi-project .NET application that showcases storing, retrieving, and augmenting prompts with contextual memory in a Retrieval-Augmented Generation (RAG) flow.

---

## Solution structure

- **`api/`**  ASP.NET Core API exposing endpoints for working with memory and AI agents.
- **`service/`**  Core services and abstractions, with dependency injection setup.
- **`infrastructure/`**  Agents and infrastructure wiring, including Semantic Kernel + Mem0 integration.
- **`model/`**  Shared models/DTOs (e.g., `AgentResponse`).


---

## Key concepts

- **Semantic Kernel**  orchestrates calls to LLMs,  and memory.
- **Mem0**  minimal in-memory memory store that saves embeddings + text for semantic retrieval.

---

## Features

- ASP.NET API to interact with Semantic Kernel and memory.
- Save and query conversation snippets in Mem0.
- Retrieve top-K relevant items with cosine similarity search.
- Demonstration of augmenting prompts with contextual memory.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- An LLM provider (Azure OpenAI or OpenAI API key)
- (Optional) Docker Desktop for running via `docker-compose`

---

