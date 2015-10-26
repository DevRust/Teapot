properties {
	$BaseDir = Resolve-Path "..\"
	$SolutionFile = "$BaseDir\TeaPot.sln"
	$Nuspec = "$BaseDir\TeaPot.nuspec"
}

Task Build {
	Exec { msbuild $SolutionFile /t:Build}
}

Task Pack -depends Build {
	Write-Host $Nuspec
	Exec { nuget pack $Nuspec}
}

Task default -depends Pack