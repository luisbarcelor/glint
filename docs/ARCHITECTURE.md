# 🏗️ System Architecture

## Sequence Flow
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
