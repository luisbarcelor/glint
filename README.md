# 🎬 Glint

### **High-Performance Video Assets for Creators & Developers**

> Glint is a specialized engine designed to transform screen captures into professional-grade **GIFs** and **WebP** assets. Perfectly optimized for technical documentation, GitHub READMEs, and high-impact social media sharing.

---

## 📖 Documentation

Explore the following guides to learn more about Glint:

| Document                                            | Description                                 |
|:----------------------------------------------------|:--------------------------------------------|
| 🚀 **[Getting Started](#-getting-started)**         | Quick setup and local installation guide.   |
| 🏗️ **[System Architecture](docs/ARCHITECTURE.md)** | Deep dive into the system flow and design.  |
| 🛠️ **[Development Guide](DEVELOPMENT.md)**         | Contribution workflow and coding standards. |
| 🔌 **[API Reference](API.md)**                      | Detailed endpoint documentation.            |
| 🚢 **[Deployment Guide](DEPLOYMENT.md)**            | Production-ready strategies and scaling.    |

---

## ✨ Key Features

- **⚡ Lightning Fast:** High-performance background processing using FFmpeg.
- **🔄 Real-time Updates:** Instant progress notifications via SignalR.
- **🐳 Container First:** Fully dockerized environment for seamless setup.
- **🛠️ Developer Friendly:** Built with .NET 10, Redis, and React.

---

## 🧰 Tech Stack

- **Core:** .NET 10 (Web API + Background Services)
- **Engine:** FFmpeg + [FFMpegCore](https://github.com/rosenbjerg/FFMpegCore)
- **Data:** PostgreSQL + EF Core
- **Queue:** Hangfire + **Redis** (Persistent job orchestration)
- **Real-time:** SignalR + **Redis Backplane**
- **UI:** React (Vite + Tailwind CSS + Mantine)

---

## 🚀 Getting Started

### 📋 Prerequisites

Ensure you have the following installed:
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) or [Docker Engine](https://docs.docker.com/engine/install/)
- A `.env` file in the root directory with:
  ```env
  REDIS_PASSWORD=your_secure_password
  ```

### ⚡ Launching the Environment

Start all services in development mode with a single command:

```bash
docker compose up -d --build
```

#### 🌐 Service Map
- **Frontend:** [http://localhost:3000](http://localhost:3000)
- **API:** [http://localhost:5000](http://localhost:5000)
- **Redis:** `localhost:6379`

---

## 🔍 Verification & Health Checks

Verify that your environment is running correctly:

### 1. Redis Connectivity
```bash
docker exec glint-redis-dev redis-cli -a your_redis_password ping
```
*Expected: `PONG`*

### 2. FFmpeg Availability
```bash
docker exec glint-media-worker-dev ffmpeg -version
```
*Expected: `ffmpeg version 6.1.1...`*

---

## 🛠️ Troubleshooting

<details>
<summary><b>Services won't start</b></summary>

```bash
# View logs
docker compose logs -f

# Hard restart
docker compose down && docker compose up -d --build
```
</details>

<details>
<summary><b>Redis connection issues</b></summary>

- Verify `REDIS_PASSWORD` in `.env` matches your expectations.
- Ensure port `6379` is not occupied by another instance.
</details>

<details>
<summary><b>FFmpeg not found</b></summary>

```bash
# Force rebuild the worker
docker compose up -d --build glint-media-worker-dev
```
</details>

---

## 📝 License

Distributed under the **Apache License 2.0**. See **[LICENSE](LICENSE)** for more information.

---

## 🤝 Contributing

We welcome contributions! Please check out our **[Development Guide](DEVELOPMENT.md)** to get started.

---

**Built with ❤️ by the developer community**
