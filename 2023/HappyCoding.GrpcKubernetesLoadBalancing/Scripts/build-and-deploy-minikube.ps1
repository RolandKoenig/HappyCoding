# Build image in minikube
# see https://levelup.gitconnected.com/two-easy-ways-to-use-local-docker-images-in-minikube-cd4dcb1a5379
minikube image build -t happycodinggrpckubernetesloadbalancing-grpc-server ../HappyCoding.GrpcKubernetesLoadBalancing.Server
minikube image build -t happycodinggrpckubernetesloadbalancing-grpc-client ../HappyCoding.GrpcKubernetesLoadBalancing.Client

