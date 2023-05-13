# Install Protobuf Compiler (protoc) first

protoc MyTestMessage.proto --csharp_out=.
protoc MyTestMessageWithOneOf.proto --csharp_out=.
protoc MyTestMessageWithChildMessage.proto --csharp_out=.
protoc MyTestMessageWithTimestamps.proto --csharp_out=.