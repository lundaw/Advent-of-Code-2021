[System.Collections.ArrayList] $inputData = @()
foreach ($line in Get-Content -Path "../inputs/day1.txt") {
    $inputData.Add([int]::Parse($line)) | Out-Null
}

#region Part 1

[int] $increaseCountPart1 = 0;
for ($i = 1; $i -lt $inputData.Count; $i++) {
    if ($inputData[$i - 1] -lt $inputData[$i]) {
        $increaseCountPart1++;
    }
}
Write-Host "[Part 1]: $($increaseCountPart1)"

#endregion

#region Part 2

[int] $increaseCountPart2 = 0;
for ($i = 0; $i -lt $inputData.Count - 2; $i++) {
    [int] $firstWindow = $inputData | Select-Object -Skip $i -First 3 | Measure-Object -Sum | Select-Object -ExpandProperty Sum
    [int] $secondWindow = $inputData | Select-Object -Skip ($i+1) -First 3 | Measure-Object -Sum | Select-Object -ExpandProperty Sum

    if ($firstWindow -lt $secondWindow) {
        $increaseCountPart2++;
    }
}
Write-Host "[Part 2]: $($increaseCountPart2)"

#endregion