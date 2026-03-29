param(
    [Parameter(Mandatory=$true)]
    [string]$version
)

$rootPath = Split-Path -Parent $PSScriptRoot
$templatePath = "$rootPath/docs/automation/templates/LegacyLego Code documentation template.md"
$docsPath = "$rootPath/docs/automation/generated/LegacyLego Code documentation.md"

$markerVersion = "{{@VERSION}}"
$markerStart   = "{{@STARTCODE}}"
$markerEnd     = "{{@ENDCODE}}"
$bt = '```'
function Get-FormattedTree {
    if (Get-Command eza -ErrorAction SilentlyContinue) {
        # Добавляем TestResults в список игнорирования для eza
        $ignoreString = "bin|obj|.git|.vs|.idea|node_modules|dist|TestResults"
        return (eza --tree --level 5 --ignore-glob $ignoreString --color=never --group-directories-first | Out-String).Trim()
    }
    return "eza not found."
}

function Get-RecursiveFolderContent {
    param(
        [string]$currentPath,
        [string]$projectRootPath,
        [int]$depth
    )
    
    $result = ""
    
    # Обновляем регулярку, добавляя TestResults
    $hasAnyCodeInBranch = Get-ChildItem -Path $currentPath -Filter *.cs -Recurse | 
                          Where-Object { $_.FullName -notmatch "\\(obj|bin|TestResults)\\" }
    if (-not $hasAnyCodeInBranch) { return "" }

    if ($currentPath -ne $projectRootPath) {
        $headerLevel = if ($depth -gt 7) { 7 } else { $depth }
        $h = "#" * $headerLevel
        $folderName = Split-Path $currentPath -Leaf
        $result += "$h $folderName`n`n"
    }

    $files = Get-ChildItem -Path $currentPath -Filter *.cs | 
             Where-Object { $_.FullName -notmatch "\\(obj|bin|TestResults)\\" } | 
             Sort-Object Name
             
    foreach ($file in $files) {
        $result += "$bt" + "cs title=""$($file.Name)""`n" + (Get-Content $file.FullName -Raw).Trim() + "`n$bt`n`n---`n`n"
    }

    # Здесь используем строгое соответствие, чтобы случайно не зацепить что-то нужное
    $subFolders = Get-ChildItem -Path $currentPath -Directory | 
                  Where-Object { $_.Name -notmatch "^(bin|obj|TestResults)$" } | 
                  Sort-Object Name

    foreach ($sub in $subFolders) {
        $nextDepth = if ($currentPath -eq $projectRootPath) { 3 } else { $depth + 1 }
        $result += Get-RecursiveFolderContent -currentPath $sub.FullName -projectRootPath $projectRootPath -depth $nextDepth
    }

    return $result
}

function Get-ProjectContent {
    $result = ""
    # И тут тоже добавляем фильтр для поиска самих .csproj
    $projects = Get-ChildItem -Path $rootPath -Filter *.csproj -Recurse | 
                Where-Object { $_.FullName -notmatch "\\(obj|bin|TestResults)\\" } | 
                Sort-Object Name

    foreach ($project in $projects) {
        $projectName = $project.BaseName
        $result += "## $projectName`n`n"
        $result += "$bt" + "xml title=""$($project.Name)""`n" + (Get-Content $project.FullName -Raw).Trim() + "`n$bt`n`n---`n`n"
        $result += Get-RecursiveFolderContent -currentPath $project.DirectoryName -projectRootPath $project.DirectoryName -depth 3
    }
    return $result
}

# Блок записи в файл (без изменений)
if (Test-Path $templatePath) {
    $content = [System.IO.File]::ReadAllText($templatePath)
    $content = $content.Replace($markerVersion, $version)

    $treeSection = "## Древовидная структура решения`n`n" + "$bt" + "txt title=""TreeStructure""`n" + (Get-FormattedTree) + "`n$bt`n`n---`n`n"
    $codeSection = "# Кодовая база`n`n" + (Get-ProjectContent)
    $fullAutoContent = "`n" + $treeSection + $codeSection

    $escapedStart = [Regex]::Escape($markerStart)
    $escapedEnd = [Regex]::Escape($markerEnd)
    $regex = "(?s)$escapedStart.*$escapedEnd"
    
    if ($content -match $regex) {
        $tempContent = [Regex]::Replace($content, $regex, "$markerStart`n$fullAutoContent`n$markerEnd")
        $finalContent = $tempContent.Replace($markerStart, "").Replace($markerEnd, "")
        $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
        [System.IO.File]::WriteAllText($docsPath, $finalContent, $utf8NoBom)
        Write-Host "Success: Документация листингов кода для проекта LegacyLego версии: v$version собрана успешно!" -ForegroundColor Green
    }
}