FROM node:24.13.0 AS pnpm
RUN corepack enable && corepack prepare pnpm@10.28.2 --activate

FROM pnpm AS dependencies
WORKDIR /glint/src
COPY package.json pnpm-lock.yaml pnpm-workspace.yaml ./
RUN pnpm install --frozen-lockfile

FROM dependencies AS build
COPY . .
RUN pnpm run build

FROM dependencies AS development
# No COPY needed — source files come from volume mount.
ENTRYPOINT ["pnpm", "run", "dev", "--host"]

FROM nginx:alpine AS final
WORKDIR /glint/app
COPY --from=build /glint/src/dist ./html
COPY ./docker/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]