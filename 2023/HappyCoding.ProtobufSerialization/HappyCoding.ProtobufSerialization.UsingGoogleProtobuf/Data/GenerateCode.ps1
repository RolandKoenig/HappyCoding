# Install Protobuf Compiler (protoc) first

protoc MyTestMessage.proto --csharp_out=.
protoc MyTestMessage_DifferentNames.proto --csharp_out=.
protoc MyTestMessage_LessProperties.proto --csharp_out=.
protoc MyTestMessage_MoreProperties.proto --csharp_out=.