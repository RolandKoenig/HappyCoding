# Install Protobuf Compiler (protoc) first

# Negative cases
protoc Negative/ChangeFieldType/MyTestMessageOriginal.proto --csharp_out=./Negative/ChangeFieldType
protoc Negative/ChangeFieldType/MyTestMessageUpdated.proto --csharp_out=./Negative/ChangeFieldType

# Positive cases
protoc Positive/AddedField/MyTestMessageOriginal.proto --csharp_out=./Positive/AddedField
protoc Positive/AddedField/MyTestMessageUpdated.proto --csharp_out=./Positive/AddedField

protoc Positive/ChangedFieldNames/MyTestMessageOriginal.proto --csharp_out=./Positive/ChangedFieldNames
protoc Positive/ChangedFieldNames/MyTestMessageUpdated.proto --csharp_out=./Positive/ChangedFieldNames

protoc Positive/RemovedField/MyTestMessageOriginal.proto --csharp_out=./Positive/RemovedField
protoc Positive/RemovedField/MyTestMessageUpdated.proto --csharp_out=./Positive/RemovedField