docker build -t happy-coding-2023-optimized-docker-build -f ./HappyCoding.OptimizedDockerBuild/Dockerfile .

docker run --name happy-coding-2023-optimized-docker-build-container happy-coding-2023-optimized-docker-build

docker rm happy-coding-2023-optimized-docker-build-container