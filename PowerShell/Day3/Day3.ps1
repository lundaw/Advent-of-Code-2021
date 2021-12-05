[System.Collections.ArrayList] $inputData = @()
foreach ($line in Get-Content -Path "../../inputs/day3.txt") {
    $inputData.Add($line) | Out-Null
}

#region Part 1

function Complete-Part1 {
    param (
        [Parameter(Mandatory)]
        [System.Collections.ArrayList] $Data
    )

    [int] $gamma = 0
    [int] $epsilon = 0

    for ($i = 0; $i -lt $Data[0].length; $i++) {
        [int] $zeroCount = 0
        [int] $oneCount = 0

        foreach ($entry in $Data) {
            if ($entry[$i] -eq '0') { $zeroCount++ } else { $oneCount++ }
        }

        $gamma = $gamma -shl 1 -bor ($zeroCount -lt $oneCount)
        $epsilon = -bnot $gamma -band 0xFFF
    }

    return $gamma * $epsilon
}

$resultPart1 = Complete-Part1 -Data $inputData
Write-Host "[Part 1]: $($resultPart1)"

#endregion

#region Part 2

function Get-RemainingBinaryResults {
    param (
        [Parameter(Mandatory)]
        [System.Collections.ArrayList] $Data,
        
        [Parameter(Mandatory)]
        [ValidateRange(0, 11)]
        [int] $Position,
        
        [Parameter(Mandatory)]
        [bool] $IsMost
    )

    [int] $zeroCount = 0
    [int] $oneCount = 0

    foreach ($entry in $Data) {
        if ($entry[$Position] -eq '0') { $zeroCount++ } else { $oneCount++ }
    }

    if ($IsMost) {
        [char] $digit = $zeroCount -gt $oneCount ? '0' : '1'
        return $Data | Where-Object { $_[$Position] -eq $digit }
    } else {
        [char] $digit = $zeroCount -gt $oneCount ? '1' : '0'
        return $Data | Where-Object { $_[$Position] -eq $digit }
    }
}

function Complete-Part2 {
    param (
        [Parameter(Mandatory)]
        [System.Collections.ArrayList] $Data
    )

    New-Variable -Name "oxygen" -Value $data.ToArray()
    New-Variable -Name "scrubber" -Value $data.ToArray()
    
    for ($i = 0; $i -lt $Data[0].length; $i++) {
        # Oxygen
        if ($oxygen.Count -gt 1) {
            $oxygen = Get-RemainingBinaryResults -Data $oxygen -Position $i -IsMost $true
        }

        # Scrubber
        if ($scrubber.Count -gt 1) {
            $scrubber = Get-RemainingBinaryResults -Data $scrubber -Position $i -IsMost $false
        }
    }

    return [Convert]::ToInt32($oxygen, 2) * [Convert]::ToInt32($scrubber, 2)
}

[int] $resultPart2 = Complete-Part2 -Data $inputData
Write-Host "[Part 2]: $($resultPart2)"

#endregion