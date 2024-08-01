function Get-CsprojFiles {
    param (
        [string]$FolderPath
    )
    
    if (-not (Test-Path $FolderPath)) {
        Write-Error "Folder '$FolderPath' is not exist."
        return
    }

    $csprojFiles = Get-ChildItem -Path $FolderPath -Recurse -Filter *.csproj -File

    $csprojFilePaths = $csprojFiles.FullName

    return $csprojFilePaths
}

#------------------------------------------------------------------

function Show-Menu {
    Clear-Host
    Write-Host "----------------------------------------" -ForegroundColor Cyan
    Write-Host "Upgrade Version Script" -ForegroundColor Yellow
    Write-Host "----------------------------------------" -ForegroundColor Cyan
    Write-Host "  [1] Catalog Service" -ForegroundColor Green
    Write-Host "  [2] Basket Service" -ForegroundColor Green
    Write-Host "  [3] Discount Service" -ForegroundColor Green
    Write-Host "  [4] Ordering Service" -ForegroundColor Green
    Write-Host "----------------------------------------" -ForegroundColor Cyan
    Write-Host "  [Q] Quit" -ForegroundColor Red
    Write-Host "----------------------------------------" -ForegroundColor Cyan
    Write-Host ""
}

#------------------------------------------------------------------

function Get-Current-Version {
	param (
		[string]$CsprojFile
	)

	$content = Get-Content -Path $CsprojFile

	$content | ForEach-Object {
		if ($_ -match "<FileVersion>(.*)<\/FileVersion>") {
			$version = $Matches[1]
			return $version
		}
	}
}

#------------------------------------------------------------------

function Is-Version-Valid {
	param (
		[string]$Version
	)

	$isValid = $Version -match "^\d+\.\d+\.\d+$"

	return $isValid
}

#------------------------------------------------------------------

function Is-New-Version-Greater-Current-Version {
	param (
		[string]$CurrentVersion,
		[string]$NewVersion
	)

	$currentVersion = [Version]::new($CurrentVersion)
	$newVersion = [Version]::new($NewVersion)

	$isGreater = $newVersion -gt $currentVersion

	return $isGreater
}

#------------------------------------------------------------------

function Ugrade-Version-Service {
	param (
		[string]$ServiceName
	)

	$serviceFolderPath = "../src/Services/$ServiceName"

	$csprojFiles = Get-CsprojFiles -FolderPath $serviceFolderPath

	if ($csprojFiles.Count -eq 0) {
		Write-Host "No csproj file found in $ServiceName service." -ForegroundColor Red
		return
	}

	$currentVersion = Get-Current-Version -CsprojFile $csprojFiles[0]

	Clear-Host
	Write-Host "Current version of $ServiceName service is $currentVersion" -ForegroundColor Yellow

	Write-Host "Enter new version for $ServiceName service: "
	$newVersion = Read-Host

	if (-not (Is-Version-Valid -Version $newVersion)) {
		Write-Host "Invalid version format." -ForegroundColor Red
		return
	}

	if (-not (Is-New-Version-Greater-Current-Version -CurrentVersion $currentVersion -NewVersion $newVersion)) {
		Write-Host "New version must be greater than current version." -ForegroundColor Red
		return
	}

	foreach ($csprojFile in $csprojFiles) {
		$content = Get-Content -Path $csprojFile -Encoding UTF8
		$newContent = $null

		$content | ForEach-Object {
			$newLine = $null

			if ($_ -match "<FileVersion>(.*)<\/FileVersion>") {
				$oldVersion = $Matches[1]
				$newLine = $_ -replace $oldVersion, $newVersion
			}

			if ($_ -match "<AssemblyVersion>(.*)<\/AssemblyVersion>") {
				$oldVersion = $Matches[1]
				$newLine = $_ -replace $oldVersion, $newVersion
			}

			if ($_ -match "<VersionPrefix>(.*)<\/VersionPrefix>") {
				$oldVersion = $Matches[1]
				$newLine = $_ -replace $oldVersion, $newVersion
			}

			$newContent += if ($newLine) { $newLine } else { $_ }
			$newContent += "`r`n"
		}

		$newContent = $newContent.TrimEnd("`r", "`n")
		Set-Content -Path $csprojFile -Value $newContent -Encoding UTF8 -NoNewline

		$csprojFileName = [System.IO.Path]::GetFileName($csprojFile)
		Write-Host "Version of $csprojFileName has been updated to $newVersion" -ForegroundColor Green
	}

	Read-Host
}

#------------------------------------------------------------------

function Main {
	Clear-Host

	$host.UI.RawUI.BackgroundColor = 'Black'
	$host.UI.RawUI.ForegroundColor = 'Gray'
	[console]::Title = "Upgrade Version Script"
	
	Show-Menu
	$choice = Read-Host "Select a service to upgrade (1-4 or Q to Quit)"

	switch ($choice) {
		'1' {
			Ugrade-Version-Service -ServiceName "Catalog"
		}
		'2' {
			Ugrade-Version-Service -ServiceName "Basket"
		}
		'3' {
			Ugrade-Version-Service -ServiceName "Discount"
		}
		'4' {
			Ugrade-Version-Service -ServiceName "Ordering"
		}
		'Q' {
			Write-Host "Exiting..." -ForegroundColor Red
			break
		}
		default {
			Write-Host "Invalid choice. Please select a valid option." -ForegroundColor Red
		}
	}

	Write-Host "Press any key to exit..."
	Read-Host
}

#------------------------------------------------------------------

Main