# 🏗️ System Architecture

Detailed overview of the Glint processing pipeline and service interaction.

## 🔄 Sequence Flow

The following diagram illustrates the end-to-end flow from video upload to real-time notification.

```mermaid
sequenceDiagram
    participant U as User/Frontend
    participant A as Glint.Api
    participant S as Object Storage
    participant Q as Queue (Hangfire/Redis)
    participant W as Glint.Worker
    participant DB as PostgreSQL

    U->>A: POST /api/media/upload
    A->>DB: Create Record (Status: Pending)
    A->>S: Upload Original Video
    A->>Q: Enqueue Job (FileId)
    A-->>U: 202 Accepted (FileId)
    
    Note over W: Worker picks up the job
    W->>DB: Update Status: Processing
    W->>S: Download Original Video
    W->>W: Execute FFmpeg (Conversion)
    W->>S: Upload Result (GIF/WebP)
    W->>DB: Update Status: Completed
    W->>A: Notify via SignalR
    A-->>U: Message: "Ready!"
```

---

## 🧩 Core Components

- **Glint.Api:** The entry point for all client requests, managing file uploads and status tracking.
- **Glint.Worker:** A background service dedicated to high-performance video processing using FFmpeg.
- **Redis:** Acts as both the job queue (Hangfire) and the real-time backplane (SignalR).
- **Object Storage:** Stores both original uploads and processed assets.
- **PostgreSQL:** Maintains application state, file metadata, and processing history.

---
*Built with ❤️ for the developer community*
