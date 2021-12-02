[System.Collections.ArrayList] $inputData = @()
foreach ($line in Get-Content -Path "../../inputs/day2.txt") {
    $command, $units = $line.Split(' ');
    $units = [int]::Parse($units)
    $inputData.Add(($command, $units)) | Out-Null
}

#region Part 1

function Complete-PartOne {
    param (
        [System.Collections.ArrayList] $data
    )

    [int] $position = 0;
    [int] $depth = 0;

    foreach ($entry in $data) {
        [string] $command, [int] $units = $entry
        
        Switch ($command) {
            "forward" { $position += $units }
            "down" { $depth += $units }
            "up" { $depth -= $units }
        }
    }

    return $position * $depth;
}

[int] $resultPart1 = Complete-PartOne($inputData)
Write-Host "[Part 1]: $($resultPart1)"

#endregion

#region Part 2

function Complete-PartTwo {
    param (
        [System.Collections.ArrayList] $data
    )

    [int] $position = 0;
    [int] $depth = 0;
    [int] $aim = 0;

    foreach ($entry in $data) {
        [string] $command, [int] $units = $entry

        Switch ($command) {
            "forward" { $position += $units; $depth += $aim * $units }
            "down" { $aim += $units }
            "up" { $aim -= $units }
        }
    }

    return $position * $depth;
}

[int] $resultPart2 = Complete-PartTwo($inputData)
Write-Host "[Part 2]: $($resultPart2)"

#endregion