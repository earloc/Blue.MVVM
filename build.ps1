param (
    [Parameter(Mandatory=$true)]
    [int]$buildNumber = $env.APPVEYOR_BUILD_NUMBER,
    [string]$packageSuffix = $env.APPVEYOR_REPO_BRANCH
)

if ($packageSuffix -eq 'master') {
    $packageSuffix = ''
}
else {
    $packageSuffix = "-$packageSuffix"
}


$projects = "Blue.MVVM","Blue.MVVM.Commands", "Blue.MVVM.IoC", "Blue.MVVM.Navigation"

foreach ($project in $projects) {

    $projectFile=".\src/$project/$project.csproj"

    [xml]$xml = Get-Content $projectFile
    $version = Select-Xml "//Version" $xml | ForEach-Object {$_.Node.'#text'}

    $patchedVersion = "$version.$buildNumber"
    $packageVersion = "$patchedVersion$packageSuffix"

    Write-Host "Building $project with version $patchedVersion"
    dotnet build $projectFile /p:Version=$patchedVersion

    Write-Host "packing $project with package version $packageVersion"
    dotnet pack  $projectFile /p:Version=$packageVersion
}
