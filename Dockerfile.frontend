FROM node:lts-slim AS build
WORKDIR /app
RUN npm install --silent -g @angular/cli
COPY StockExchange.AngularUI/package*.json ./
RUN npm ci
COPY StockExchange.AngularUI/. ./
RUN ng build --configuration=production

FROM nginx:stable AS final
EXPOSE 80
EXPOSE 443
# 2025/04/21 - Uncomment the line below to replace the default NGINX configuration with your custom one
#COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/stock-exchange.angular-ui/browser /usr/share/nginx/html
