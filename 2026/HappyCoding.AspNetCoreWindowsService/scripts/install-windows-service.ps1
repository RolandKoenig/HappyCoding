$ErrorActionPreference = "Stop"

$serviceName = "HappyCoding.AspNetCoreWindowsService"
$displayName = "HappyCoding.AspNetCoreWindowsService"
$publishPath = Resolve-Path (Join-Path $PSScriptRoot "..\publish")
$exePath = Join-Path $publishPath "HappyCoding.AspNetCoreWindowsService.exe"

if (-not (Test-Path $exePath)) {
    throw "Application executable was not found at '$exePath'. Publish the application first."
}

if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(
        [Security.Principal.WindowsBuiltInRole]::Administrator
)) {
    throw "This script must be run as Administrator."
}

# Stop / uninstall existing service
$existingService = Get-Service -Name $serviceName -ErrorAction SilentlyContinue
if ($existingService) {
    Write-Host "Service '$serviceName' already exists."

    if ($existingService.Status -ne "Stopped") {
        Write-Host "Stopping service '$serviceName'..."
        Stop-Service -Name $serviceName -Force
        $existingService.WaitForStatus("Stopped", "00:00:30")
    }

    Write-Host "Deleting existing service '$serviceName'..."
    sc.exe delete $serviceName | Out-Host

    Start-Sleep -Seconds 2
}

## Install and start the service
Write-Host "Installing service '$serviceName' from '$exePath'..."
New-Service `
    -Name $serviceName `
    -DisplayName $displayName `
    -BinaryPathName "`"$exePath`"" `
    -StartupType Automatic `
    -Description "HappyCoding ASP.NET Windows Service"

Write-Host "Starting service '$serviceName'..."
Start-Service -Name $serviceName

Write-Host "Service '$serviceName' installed and started successfully."

# Wait for the user to press enter
Read-Host "Press Enter to exit"