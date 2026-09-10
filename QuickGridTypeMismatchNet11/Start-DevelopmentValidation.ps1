$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $root

$timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$evidenceDirectory = Join-Path $root "evidence\development-$timestamp"
New-Item -ItemType Directory -Path $evidenceDirectory -Force | Out-Null

function Invoke-DotNetCapture {
    param(
        [Parameter(Mandatory = $true)]
        [string[]] $Arguments,

        [Parameter(Mandatory = $true)]
        [string] $LogFile
    )

    & dotnet @Arguments 2>&1 | Tee-Object -FilePath $LogFile

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet $($Arguments -join ' ') failed with exit code $LASTEXITCODE. See $LogFile."
    }
}

$expectedSdk = "11.0.100-rc.1.26425.128"
$expectedPackage = "11.0.0-rc.1.26425.128"
$actualSdk = (dotnet --version).Trim()
$actualSdk | Tee-Object -FilePath (Join-Path $evidenceDirectory "E-DEV-01-dotnet-version.txt")

if ($actualSdk -ne $expectedSdk) {
    throw "Expected SDK $expectedSdk but dotnet selected $actualSdk. Install the pinned SDK and run again."
}

Invoke-DotNetCapture `
    -Arguments @("--info") `
    -LogFile (Join-Path $evidenceDirectory "E-DEV-02-dotnet-info.txt")

Invoke-DotNetCapture `
    -Arguments @("restore") `
    -LogFile (Join-Path $evidenceDirectory "E-DEV-03-restore.log")

$packageLog = Join-Path $evidenceDirectory "E-DEV-04-packages.log"
$packageOutput = (& dotnet list package 2>&1 | Tee-Object -FilePath $packageLog | Out-String)

if ($LASTEXITCODE -ne 0) {
    throw "dotnet list package failed with exit code $LASTEXITCODE. See $packageLog."
}

if ($packageOutput -notmatch [regex]::Escape($expectedPackage)) {
    throw "QuickGrid package $expectedPackage was not found in the resolved package list. See $packageLog."
}

Invoke-DotNetCapture `
    -Arguments @("build", "--no-restore") `
    -LogFile (Join-Path $evidenceDirectory "E-DEV-05-build.log")

Write-Host ""
Write-Host "Development evidence folder: $evidenceDirectory"
Write-Host "Keep this window open while following TEST_STEPS.md."
Write-Host "Press Ctrl+C only after every Development scenario is complete."
Write-Host ""

$ErrorActionPreference = "Continue"
dotnet run --no-build --environment Development |
    Tee-Object -FilePath (Join-Path $evidenceDirectory "E-DEV-06-server.log")
