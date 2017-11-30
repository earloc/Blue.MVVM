param (
    [Parameter(Mandatory=$true)]
    [int]$buildNumber,
    [string]$branch
)

. ".\_build.ps1";

$packageSuffix = GetPackageSuffix($branch)

$projects = "Blue.MVVM","Blue.MVVM.Commands", "Blue.MVVM.IoC", "Blue.MVVM.Navigation"

foreach ($project in $projects) {
    BuildAndPack "./src" $project $packageSuffix
}
