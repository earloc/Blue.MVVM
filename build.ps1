param (
    [Parameter(Mandatory=$true)]
    [int]$buildNumber,
    [string]$packageSuffix
)

if ($packageSuffix -eq 'master') {
    $packageSuffix = ''
}
else {
    $packageSuffix = "-$packageSuffix"
}


$projects = "Blue.MVVM","Blue.MVVM.Commands", "Blue.MVVM.IoC", "Blue.MVVM.Navigation"

foreach ($project in $projects) {

    $directory = ".\src\$project"

    $projectFile="$directory\$project.csproj"

    [xml]$projectXml = Get-Content $projectFile
    $version = Select-Xml "//Version" $projectXml | ForEach-Object {$_.Node.'#text'}

    $packageOverridesFile="$directory\Package.Overrides.xml"


    $packageOverridesFile

    if (Test-Path $packageOverridesFile ) {
        [xml]$overridesXml = Get-Content $packageOverridesFile
        $packageSuffixOverride = Select-Xml "/Package/Suffix" $overridesXml | ForEach-Object {$_.Node.'#text'}

        if ($packageSuffixOverride -ne '') {
            Write-Host "-------------------------------------------------------"
            Write-Host "overriding package suffix to: '$packageSuffixOverride'"
            Write-Host "-------------------------------------------------------"
            $packageSuffix = "-$packageSuffixOverride"
        }
    }

    $patchedVersion = "$version.$buildNumber"
    $packageVersion = "$patchedVersion$packageSuffix"

    Write-Host "-------------------------------------------------------"
    Write-Host "Building $project with version $patchedVersion"
    Write-Host "-------------------------------------------------------"
    dotnet build $projectFile /p:Version=$patchedVersion

    Write-Host "-------------------------------------------------------"
    Write-Host "packing $project with package version $packageVersion"
    Write-Host "-------------------------------------------------------"
    dotnet pack  $projectFile /p:Version=$packageVersion --no-build
}
