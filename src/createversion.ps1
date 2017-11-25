$releaseFolder = Join-Path $PSScriptRoot "release"

# create release directory
New-Item -ItemType Directory -Force -Path $releaseFolder

function Copy-ToRealeseFolder {
	param ( [string]$Platform, [string]$ReleaseFolder )
	
	$PlatformFolder = Join-Path $ReleaseFolder $Platform
	
	New-Item -ItemType Directory -Force -Path $PlatformFolder
	
	$BaseFolder = Join-Path $PSScriptRoot $Platform
	Write-Host $BaseFolder
	Copy-Item (Join-Path $BaseFolder "Extensions\bin\Release\Fuuko.dll") $PlatformFolder
	Copy-Item (Join-Path $BaseFolder "Extensions\bin\Release\Fuuko.Extensions.dll") $PlatformFolder
	Copy-Item (Join-Path $BaseFolder "Extensions\bin\Release\Fuuko.Readers.dll") $PlatformFolder	
}

#copy files
Copy-ToRealeseFolder -platform "NET_4.5" -releaseFolder $releaseFolder
Copy-ToRealeseFolder -platform "NET_4.5.1" -releaseFolder $releaseFolder
Copy-ToRealeseFolder -platform "NET_4.5.2" -releaseFolder $releaseFolder
Copy-ToRealeseFolder -platform "NET_4.6" -releaseFolder $releaseFolder
Copy-ToRealeseFolder -platform "NET_4.6.1" -releaseFolder $releaseFolder
Copy-ToRealeseFolder -platform "NET_4.6.2" -releaseFolder $releaseFolder
Copy-ToRealeseFolder -platform "UWP" -releaseFolder $releaseFolder

#create archive
Compress-Archive -Path $releaseFolder -DestinationPath (Join-Path $PSScriptRoot "release.zip") -Force

#remove files
Remove-Item -Recurse -Force $releaseFolder