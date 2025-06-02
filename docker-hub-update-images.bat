@echo off
echo Building Docker images via docker-compose...
docker compose build --no-cache

echo Tagging Docker images...
docker tag stockexchange.angularui:latest rodrigocdellu/stockexchange.angularui:1.0.0
docker tag stockexchange.reactui:latest rodrigocdellu/stockexchange.reactui:1.0.0
docker tag stockexchange.webapi:latest rodrigocdellu/stockexchange.webapi:1.0.0

echo Logging in to Docker Hub...
docker login

echo Pushing Docker images...
docker push rodrigocdellu/stockexchange.angularui:1.0.0
echo Waiting 15 seconds...
timeout /T 15 /NOBREAK >nul

docker push rodrigocdellu/stockexchange.reactui:1.0.0
echo Waiting 15 seconds...
timeout /T 15 /NOBREAK >nul

docker push rodrigocdellu/stockexchange.webapi:1.0.0
echo Waiting 15 seconds...
timeout /T 15 /NOBREAK >nul

echo Logging out from Docker Hub...
docker logout

echo Removing tagged images locally...
docker rmi rodrigocdellu/stockexchange.angularui:1.0.0
docker rmi rodrigocdellu/stockexchange.reactui:1.0.0
docker rmi rodrigocdellu/stockexchange.webapi:1.0.0

echo Done.
pause
