$releaseFolder = Join-Path $PSScriptRoot "release"

# create release directory
New-Item -ItemType Directory -Force -Path $releaseFolder

#copy files
Copy-Item (Join-Path $PSScriptRoot "HttFluent\bin\Release\Fuuko.dll") $releaseFolder
Copy-Item (Join-Path $PSScriptRoot "HttFluent.NET451\bin\Release\Fuuko.NET451.dll") $releaseFolder
Copy-Item (Join-Path $PSScriptRoot "Fuuko.NET452\bin\Release\Fuuko.NET452.dll") $releaseFolder
Copy-Item (Join-Path $PSScriptRoot "Fuuko.NET46\bin\Release\Fuuko.NET46.dll") $releaseFolder
Copy-Item (Join-Path $PSScriptRoot "Fuuko.NET461\bin\Release\Fuuko.NET461.dll") $releaseFolder
Copy-Item (Join-Path $PSScriptRoot "Fuuko.NET462\bin\Release\Fuuko.NET462.dll") $releaseFolder

#create archive
Compress-Archive -Path $releaseFolder -DestinationPath (Join-Path $PSScriptRoot "release.zip") -Force

#remove files
Remove-Item -Recurse -Force $releaseFolder