param(
    [Parameter(Mandatory=$true)]
    [string]$version
)

$rootPath = Split-Path -Parent $PSScriptRoot
$templatePath = "$rootPath/docs/automation/templates/LegacyLego Code documentation template.md"
$docsPath = "$rootPath/docs/automation/generated/LegacyLego Code documentation.md"

# Новые "безопасные" маркеры
$markerVersion = "{{@VERSION}}"
$markerStart   = "{{@STARTCODE}}"
$markerEnd     = "{{@ENDCODE}}"
$bt = '```'

function Get-FormattedTree {
    if (Get-Command eza -ErrorAction SilentlyContinue) {
        $ignoreString = "bin|obj|.git|.vs|.idea|node_modules|dist"
        return (eza --tree --level 5 --ignore-glob $ignoreString --color=never --group-directories-first | Out-String).Trim()
    }
    return "eza not found."
}

function Get-ProjectContent {
    $result = ""
    $projects = Get-ChildItem -Path $rootPath -Filter *.csproj -Recurse | Where-Object { $_.FullName -notmatch "obj|bin" }

    foreach ($project in $projects) {
        $projectName = $project.BaseName
        $result += "## $projectName`n`n"
        $result += "$bt" + "xml title=""$($project.Name)""`n" + (Get-Content $project.FullName -Raw).Trim() + "`n$bt`n`n"

        $csFiles = Get-ChildItem -Path $project.DirectoryName -Filter *.cs -Recurse | Where-Object { $_.FullName -notmatch "obj|bin" }
        foreach ($group in ($csFiles | Group-Object DirectoryName)) {
            $folderName = Split-Path $group.Name -Leaf
            if ($folderName -eq $projectName) { $folderName = "Root" }
            $result += "### $folderName`n`n"
            foreach ($file in $group.Group) {
                $result += "$bt" + "cs title=""$($file.Name)""`n" + (Get-Content $file.FullName -Raw).Trim() + "`n$bt`n`n---`n`n"
            }
        }
    }
    return $result
}

if (Test-Path $templatePath) {
    # 1. Загружаем шаблон
    $content = [System.IO.File]::ReadAllText($templatePath)

    # 2. Мгновенная замена версии (маркер исчезает сам, заменяясь текстом)
    $content = $content.Replace($markerVersion, $version)

    # 3. Генерируем динамический блок
    $treeSection = "## Древовидная структура решения`n`n" + "$bt" + "txt title=""TreeStructure""`n" + (Get-FormattedTree) + "`n$bt`n`n---`n`n"
    $codeSection = "# Кодовая база`n`n" + (Get-ProjectContent)
    $fullAutoContent = "`n" + $treeSection + $codeSection

    # 4. Регулярка для поиска и замены содержимого МЕЖДУ маркерами
    $escapedStart = [Regex]::Escape($markerStart)
    $escapedEnd = [Regex]::Escape($markerEnd)
    $regex = "(?s)$escapedStart.*$escapedEnd"
    
    if ($content -match $regex) {
        # Заменяем вместе с маркерами на временный блок
        $tempContent = [Regex]::Replace($content, $regex, "$markerStart`n$fullAutoContent`n$markerEnd")
        
        # 5. ФИНАЛЬНАЯ ОЧИСТКА: Удаляем сами маркеры из итогового текста
        $finalContent = $tempContent.Replace($markerStart, "").Replace($markerEnd, "")

        # 6. Сохранение в целевой файл
        $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
        [System.IO.File]::WriteAllText($docsPath, $finalContent, $utf8NoBom)
        
        Write-Host "Success: Документация листингов кода для проекта LegacyLego версии: v$version собрана успешно!" -ForegroundColor Green
    } else {
        Write-Host "Error: В шаблоне не найдены структурные маркеры $markerStart / $markerEnd" -ForegroundColor Red
    }
}