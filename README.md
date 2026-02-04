# 🎬 Glint — High-Performance Video Assets for Those Who Build & Share

> Glint is a specialized engine designed for **creators** and **developers**. It seamlessly transforms screen captures into professional-grade GIFs and WebP assets, optimized for technical documentation, GitHub READMEs, and high-impact social media sharing.

---

## 📚 Documentation

| Document                                             | Description                                      |
|------------------------------------------------------|--------------------------------------------------|
| **[Getting Started](#-getting-started)**             | Quick setup and installation guide               |
| **[ARCHITECTURE.md](docs/ARCHITECTURE.md)**          | Detailed overview of the system flow             |
| **[DEVELOPMENT.md](DEVELOPMENT.md)** *(coming soon)* | Development guidelines and contribution workflow |
| **[API.md](API.md)** *(coming soon)*                 | API reference and endpoint documentation         |
| **[DEPLOYMENT.md](DEPLOYMENT.md)** *(coming soon)*   | Production deployment strategies                 |

---

## 🛠️ Tech Stack
- **Backend:** .NET 10 (Web API + Background Services)
- **Processing:** FFmpeg + FFMpegCore
- **Database:** PostgreSQL + EF Core
- **Job Orchestration:** Hangfire + **Redis** (High-performance queuing)
- **Frontend:** React (Vite + Tailwind CSS + Mantine)
- **Communication:** SignalR + **Redis Backplane** (Real-time updates)

---

## 🚀 Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) or [Docker Engine](https://docs.docker.com/engine/install/)
- A `.env` file in the root directory with the following content:
  ```env
  REDIS_PASSWORD=your_secure_password
  ```

### Launching the Development Environment

To start all services in development mode, run:

```bash
docker compose up -d --build
```

This will spin up:
- **Frontend:** http://localhost:3000
- **API:** http://localhost:5000
- **Redis:** localhost:6379

### Verifying Services

#### 1. Check Redis
Verify that Redis is running and authenticated:
```bash
docker exec glint-redis-dev redis-cli -a your_redis_password ping
```
*Expected output: `PONG`*

#### 2. Check FFmpeg
Verify that FFmpeg is available in the Media Worker container:
```bash
docker exec glint-media-worker-dev ffmpeg -version
```
*Expected output: `ffmpeg version 6.1.1...`*

---

## 🐛 Troubleshooting

### Services won't start
```bash
# Check container logs
docker-compose logs -f

# Restart all services
docker-compose down && docker-compose up -d --build
```

### Redis connection issues
- Verify `REDIS_PASSWORD` in `.env` matches `docker-compose.yml`
- Ensure no other services are using port 6379

### FFmpeg not found
```bash
# Rebuild the media worker container
docker-compose up -d --build glint-media-worker-dev
```

---

## 📝 License

This project is licensed under the [Apache License 2.0](LICENSE).  
You may use, modify, and distribute the code in accordance with the license terms.

---

## 🤝 Contributing

Contributions are welcome! Please see **[DEVELOPMENT.md](DEVELOPMENT.md)** *(coming soon)* for guidelines.

For major changes, open an issue first to discuss proposed modifications.

---

**Built with ❤️ for the developer community**