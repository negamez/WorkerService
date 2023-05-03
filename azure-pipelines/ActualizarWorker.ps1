$server = $env:SERVER
$userName = $env:USERNAME
if (-not ($userName.StartsWith("BANCOLOMBIA") -or $userName.StartsWith("BANCOAGRICOLA")))
{
    $userName = "$($server)\$($userName)"
}
$securePassword = ConvertTo-SecureString -AsPlainText -String $env:PASSWORD -Force
$credential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $userName, $securePassword

$serviceName = $env:SERVICENAME
$servicePath = $env:SERVICEPATH
$tempPath = $env:TEMPPATH

Write-Host "Iniciando sesion en servidor $($server) con usuario $($userName)"
$serviceSession = New-PSSession $server -Credential $credential -ConfigurationName AzurePipelines
$copySession = New-PSSession $server -Credential $credential

Write-Host "Deteniendo servicio $($serviceName)"
Invoke-Command -Session $serviceSession -ScriptBlock {
    Stop-Service -Name $using:serviceName
    Start-Sleep -Seconds 10
}

Write-Host "Copiando archivos hacia $($servicePath)"
Invoke-Command -Session $copySession -ScriptBlock {
    Copy-Item -Path "$($using:tempPath)\*" -Destination $using:servicePath -Exclude 'appsettings*.json' -Recurse -Force -ErrorAction:SilentlyContinue -ErrorVariable e
    if ($e) {
        Start-Sleep -Seconds 10
        Copy-Item -Path "$($using:tempPath)\*" -Destination $using:servicePath -Exclude 'appsettings*.json' -Recurse -Force
    }
}

Write-Host "Iniciando servicio $($serviceName)"
Invoke-Command -Session $serviceSession -ScriptBlock {
    Start-Service -Name $using:serviceName -ErrorAction:SilentlyContinue -ErrorVariable e
    if ($e) {
        Start-Service -Name $using:serviceName
    }
}