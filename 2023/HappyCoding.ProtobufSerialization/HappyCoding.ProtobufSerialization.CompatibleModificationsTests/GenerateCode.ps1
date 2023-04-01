# Install Protobuf Compiler (protoc) first

$currentLocation = Get-Location
"Current Directory: $currentLocation"

$protoFiles = Get-ChildItem -Path *.proto -Recurse -Force
foreach ($actProtoFile in $protoFiles)
{
    $actProtoFileLocal = $actProtoFile.ToString().Replace("$currentLocation/", "").Replace("$currentLocation\", "")

	"Compiling $actProtoFileLocal"
    
	$actProtoDirectoryPath = Split-Path -Path $actProtoFileLocal;
	protoc "$actProtoFileLocal" "--csharp_out=$actProtoDirectoryPath"
}