# Введение

## Назначение

**LegacyLego** - Пет-проект, разрабатываемый для практики веб-разработки middle уровня с использованием DDD + Clean Architecture подхода.
В данном документе будут описаны листинги кода проекта на его актуальной версии.

---

## Версия

Актуальная версия проекта: 1.10.0

## Проекты

Все существующие на данный момент проекты в решении `LegacyLego.slnx`:

1) **LegacyLego.Domain** - Содержит доменную логику проекта, является ядром всей системы и существует, чтобы описывать бизнес-логику на уровне кода;
2) **LegacyLego.Domain.Tests** - Содержит модульные тесты **LegacyLego.Domain**;
3) **LegacyLego.Application** - Описывает use-case сценарии, обеспечивающие логистику и оркестрацию системы в отношении данных и базовые контракты для будущей инфраструктуры;
4) **LegacyLego.Infrastructure** - Предназначается в первую очередь для описания персистентной модели, реализации базовых контрактов доменных и уровня приложения. Сопряжения с внешними сервисами (внешней инфраструктурой);
5) **LegacyLego.Presentation** - Является точкой входа (Program.cs) в API, описывает эндпоинты и регистрирует инфраструктурные сервисы через DI механизм.  

---



## Древовидная структура решения

```txt title="TreeStructure"
.
├── ar-SA
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── Assets
│   ├── Awake
│   │   ├── Awake.ico
│   │   ├── disabled.ico
│   │   ├── expirable.ico
│   │   ├── indefinite.ico
│   │   ├── normal.ico
│   │   ├── scheduled.ico
│   │   └── timed.ico
│   ├── ColorPicker
│   │   └── colorPicker.cur
│   ├── ImageResizer
│   │   ├── ImageResizer.ico
│   │   ├── LargeTile.png
│   │   ├── SmallTile.png
│   │   ├── SplashScreen.png
│   │   ├── Square44x44Logo.png
│   │   ├── Square150x150Logo.png
│   │   ├── storelogo.png
│   │   └── Wide310x150Logo.png
│   ├── Monaco
│   │   ├── customLanguages
│   │   │   ├── gitignore.js
│   │   │   ├── reg.js
│   │   │   └── srt.js
│   │   ├── monacoSRC
│   │   │   └── min
│   │   │       └── vs
│   │   │           ├── base
│   │   │           │   ├── browser
│   │   │           │   │   └── ui
│   │   │           │   ├── common
│   │   │           │   │   └── worker
│   │   │           │   └── worker
│   │   │           │       └── workerMain.js
│   │   │           ├── basic-languages
│   │   │           │   ├── abap
│   │   │           │   │   └── abap.js
│   │   │           │   ├── apex
│   │   │           │   │   └── apex.js
│   │   │           │   ├── azcli
│   │   │           │   │   └── azcli.js
│   │   │           │   ├── bat
│   │   │           │   │   └── bat.js
│   │   │           │   ├── bicep
│   │   │           │   │   └── bicep.js
│   │   │           │   ├── cameligo
│   │   │           │   │   └── cameligo.js
│   │   │           │   ├── clojure
│   │   │           │   │   └── clojure.js
│   │   │           │   ├── coffee
│   │   │           │   │   └── coffee.js
│   │   │           │   ├── cpp
│   │   │           │   │   └── cpp.js
│   │   │           │   ├── csharp
│   │   │           │   │   └── csharp.js
│   │   │           │   ├── csp
│   │   │           │   │   └── csp.js
│   │   │           │   ├── css
│   │   │           │   │   └── css.js
│   │   │           │   ├── cypher
│   │   │           │   │   └── cypher.js
│   │   │           │   ├── dart
│   │   │           │   │   └── dart.js
│   │   │           │   ├── dockerfile
│   │   │           │   │   └── dockerfile.js
│   │   │           │   ├── ecl
│   │   │           │   │   └── ecl.js
│   │   │           │   ├── elixir
│   │   │           │   │   └── elixir.js
│   │   │           │   ├── flow9
│   │   │           │   │   └── flow9.js
│   │   │           │   ├── freemarker2
│   │   │           │   │   └── freemarker2.js
│   │   │           │   ├── fsharp
│   │   │           │   │   └── fsharp.js
│   │   │           │   ├── go
│   │   │           │   │   └── go.js
│   │   │           │   ├── graphql
│   │   │           │   │   └── graphql.js
│   │   │           │   ├── handlebars
│   │   │           │   │   └── handlebars.js
│   │   │           │   ├── hcl
│   │   │           │   │   └── hcl.js
│   │   │           │   ├── html
│   │   │           │   │   └── html.js
│   │   │           │   ├── ini
│   │   │           │   │   └── ini.js
│   │   │           │   ├── java
│   │   │           │   │   └── java.js
│   │   │           │   ├── javascript
│   │   │           │   │   └── javascript.js
│   │   │           │   ├── julia
│   │   │           │   │   └── julia.js
│   │   │           │   ├── kotlin
│   │   │           │   │   └── kotlin.js
│   │   │           │   ├── less
│   │   │           │   │   └── less.js
│   │   │           │   ├── lexon
│   │   │           │   │   └── lexon.js
│   │   │           │   ├── liquid
│   │   │           │   │   └── liquid.js
│   │   │           │   ├── lua
│   │   │           │   │   └── lua.js
│   │   │           │   ├── m3
│   │   │           │   │   └── m3.js
│   │   │           │   ├── markdown
│   │   │           │   │   └── markdown.js
│   │   │           │   ├── mdx
│   │   │           │   │   └── mdx.js
│   │   │           │   ├── mips
│   │   │           │   │   └── mips.js
│   │   │           │   ├── msdax
│   │   │           │   │   └── msdax.js
│   │   │           │   ├── mysql
│   │   │           │   │   └── mysql.js
│   │   │           │   ├── objective-c
│   │   │           │   │   └── objective-c.js
│   │   │           │   ├── pascal
│   │   │           │   │   └── pascal.js
│   │   │           │   ├── pascaligo
│   │   │           │   │   └── pascaligo.js
│   │   │           │   ├── perl
│   │   │           │   │   └── perl.js
│   │   │           │   ├── pgsql
│   │   │           │   │   └── pgsql.js
│   │   │           │   ├── php
│   │   │           │   │   └── php.js
│   │   │           │   ├── pla
│   │   │           │   │   └── pla.js
│   │   │           │   ├── postiats
│   │   │           │   │   └── postiats.js
│   │   │           │   ├── powerquery
│   │   │           │   │   └── powerquery.js
│   │   │           │   ├── powershell
│   │   │           │   │   └── powershell.js
│   │   │           │   ├── protobuf
│   │   │           │   │   └── protobuf.js
│   │   │           │   ├── pug
│   │   │           │   │   └── pug.js
│   │   │           │   ├── python
│   │   │           │   │   └── python.js
│   │   │           │   ├── qsharp
│   │   │           │   │   └── qsharp.js
│   │   │           │   ├── r
│   │   │           │   │   └── r.js
│   │   │           │   ├── razor
│   │   │           │   │   └── razor.js
│   │   │           │   ├── redis
│   │   │           │   │   └── redis.js
│   │   │           │   ├── redshift
│   │   │           │   │   └── redshift.js
│   │   │           │   ├── restructuredtext
│   │   │           │   │   └── restructuredtext.js
│   │   │           │   ├── ruby
│   │   │           │   │   └── ruby.js
│   │   │           │   ├── rust
│   │   │           │   │   └── rust.js
│   │   │           │   ├── sb
│   │   │           │   │   └── sb.js
│   │   │           │   ├── scala
│   │   │           │   │   └── scala.js
│   │   │           │   ├── scheme
│   │   │           │   │   └── scheme.js
│   │   │           │   ├── scss
│   │   │           │   │   └── scss.js
│   │   │           │   ├── shell
│   │   │           │   │   └── shell.js
│   │   │           │   ├── solidity
│   │   │           │   │   └── solidity.js
│   │   │           │   ├── sophia
│   │   │           │   │   └── sophia.js
│   │   │           │   ├── sparql
│   │   │           │   │   └── sparql.js
│   │   │           │   ├── sql
│   │   │           │   │   └── sql.js
│   │   │           │   ├── st
│   │   │           │   │   └── st.js
│   │   │           │   ├── swift
│   │   │           │   │   └── swift.js
│   │   │           │   ├── systemverilog
│   │   │           │   │   └── systemverilog.js
│   │   │           │   ├── tcl
│   │   │           │   │   └── tcl.js
│   │   │           │   ├── twig
│   │   │           │   │   └── twig.js
│   │   │           │   ├── typescript
│   │   │           │   │   └── typescript.js
│   │   │           │   ├── vb
│   │   │           │   │   └── vb.js
│   │   │           │   ├── wgsl
│   │   │           │   │   └── wgsl.js
│   │   │           │   ├── xml
│   │   │           │   │   └── xml.js
│   │   │           │   └── yaml
│   │   │           │       └── yaml.js
│   │   │           ├── editor
│   │   │           │   ├── editor.main.css
│   │   │           │   ├── editor.main.js
│   │   │           │   ├── editor.main.nls.de.js
│   │   │           │   ├── editor.main.nls.es.js
│   │   │           │   ├── editor.main.nls.fr.js
│   │   │           │   ├── editor.main.nls.it.js
│   │   │           │   ├── editor.main.nls.ja.js
│   │   │           │   ├── editor.main.nls.js
│   │   │           │   ├── editor.main.nls.ko.js
│   │   │           │   ├── editor.main.nls.ru.js
│   │   │           │   ├── editor.main.nls.zh-cn.js
│   │   │           │   └── editor.main.nls.zh-tw.js
│   │   │           ├── language
│   │   │           │   ├── css
│   │   │           │   │   ├── cssMode.js
│   │   │           │   │   └── cssWorker.js
│   │   │           │   ├── html
│   │   │           │   │   ├── htmlMode.js
│   │   │           │   │   └── htmlWorker.js
│   │   │           │   ├── json
│   │   │           │   │   ├── jsonMode.js
│   │   │           │   │   └── jsonWorker.js
│   │   │           │   └── typescript
│   │   │           │       ├── tsMode.js
│   │   │           │       └── tsWorker.js
│   │   │           └── loader.js
│   │   ├── customTokenThemeRules.js
│   │   ├── index.html
│   │   ├── monaco_languages.json
│   │   └── monacoSpecialLanguages.js
│   ├── PowerLauncher
│   │   ├── app_error.dark.png
│   │   ├── app_error.light.png
│   │   └── RunAsset.ico
│   ├── ShortcutGuide
│   │   ├── 0.svg
│   │   ├── 1.svg
│   │   ├── 2.svg
│   │   ├── 3.svg
│   │   ├── 4.svg
│   │   ├── 5.svg
│   │   ├── 6.svg
│   │   ├── 7.svg
│   │   ├── 8.svg
│   │   ├── 9.svg
│   │   ├── no_active_window.svg
│   │   ├── overlay.svg
│   │   └── overlay_portrait.svg
│   └── Workspaces
│       ├── DefaultIcon.ico
│       └── Workspaces.ico
├── cs-CZ
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── de-DE
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── DSCModules
│   ├── Microsoft.PowerToys.Configure.psd1
│   └── Microsoft.PowerToys.Configure.psm1
├── es-ES
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── fa-IR
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── fr-FR
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── he-IL
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── hu-HU
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── it-IT
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── ja-JP
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── KeyboardManagerEditor
│   ├── Microsoft.Toolkit.Win32.UI.XamlHost.dll
│   ├── Microsoft.UI.Xaml.dll
│   ├── msvcp140.dll
│   ├── msvcp140_app.dll
│   ├── PowerToys.KeyboardManagerEditor.exe
│   ├── resources.pri
│   ├── vcruntime140.dll
│   ├── vcruntime140_1.dll
│   ├── vcruntime140_1_app.dll
│   └── vcruntime140_app.dll
├── KeyboardManagerEngine
│   └── PowerToys.KeyboardManagerEngine.exe
├── ko-KR
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── modules
│   ├── Awake
│   ├── ColorPicker
│   ├── FancyZones
│   ├── FileExplorerPreview
│   ├── FileLocksmith
│   ├── Hosts
│   ├── ImageResizer
│   ├── launcher
│   ├── MeasureTool
│   ├── MouseUtils
│   │   └── MouseJumpUI
│   ├── PowerAccent
│   └── PowerOCR
├── nl-NL
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── pl-PL
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── pt-BR
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── pt-PT
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── ru-RU
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── RunPlugins
│   ├── Calculator
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── Images
│   │   │   ├── calculator.dark.png
│   │   │   └── calculator.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Calculator.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.Calculator.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.Calculator.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Folder
│   │   ├── ar-SA
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── Images
│   │   │   ├── copy.dark.png
│   │   │   ├── copy.light.png
│   │   │   ├── delete.dark.png
│   │   │   ├── delete.light.png
│   │   │   ├── file.dark.png
│   │   │   ├── file.light.png
│   │   │   ├── folder.dark.png
│   │   │   ├── folder.light.png
│   │   │   ├── user.dark.png
│   │   │   ├── user.light.png
│   │   │   ├── Warning.dark.png
│   │   │   └── Warning.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.Plugin.Folder.resources.dll
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.Plugin.Folder.deps.json
│   │   ├── Microsoft.Plugin.Folder.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── History
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── Images
│   │   │   ├── history.dark.png
│   │   │   └── history.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.History.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.History.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.History.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Indexer
│   │   ├── ar-SA
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── Images
│   │   │   ├── indexer.dark.png
│   │   │   ├── indexer.light.png
│   │   │   ├── Warning.dark.png
│   │   │   └── Warning.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.Plugin.Indexer.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.Plugin.Indexer.deps.json
│   │   ├── Microsoft.Plugin.Indexer.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── OneNote
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── Images
│   │   │   ├── oneNote.dark.png
│   │   │   └── oneNote.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.OneNote.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.OneNote.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.OneNote.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── PowerToys
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── Images
│   │   │   ├── ColorPicker.png
│   │   │   ├── CropAndLock.png
│   │   │   ├── EnvironmentVariables.png
│   │   │   ├── FancyZones.png
│   │   │   ├── Hosts.png
│   │   │   ├── PowerOcr.png
│   │   │   ├── PowerToys.dark.png
│   │   │   ├── PowerToys.light.png
│   │   │   ├── RegistryPreview.png
│   │   │   ├── ScreenRuler.png
│   │   │   ├── ShortcutGuide.png
│   │   │   └── Workspaces.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.PowerToys.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.PowerToys.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.PowerToys.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Program
│   │   ├── ar-SA
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── Images
│   │   │   ├── app.dark.png
│   │   │   ├── app.light.png
│   │   │   ├── disable.dark.png
│   │   │   ├── disable.light.png
│   │   │   ├── folder.dark.png
│   │   │   ├── folder.light.png
│   │   │   ├── shell.dark.png
│   │   │   ├── shell.light.png
│   │   │   ├── user.dark.png
│   │   │   └── user.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.Plugin.Program.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.Plugin.Program.deps.json
│   │   ├── Microsoft.Plugin.Program.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Registry
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── Images
│   │   │   ├── reg.dark.png
│   │   │   └── reg.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Registry.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.Registry.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.Registry.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Service
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── Images
│   │   │   ├── service.dark.png
│   │   │   └── service.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.Service.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.Service.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.Service.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Shell
│   │   ├── ar-SA
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── Images
│   │   │   ├── shell.dark.png
│   │   │   ├── shell.light.png
│   │   │   ├── user.dark.png
│   │   │   └── user.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.Plugin.Shell.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.Plugin.Shell.deps.json
│   │   ├── Microsoft.Plugin.Shell.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── System
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── Images
│   │   │   ├── firmwareSettings.dark.png
│   │   │   ├── firmwareSettings.light.png
│   │   │   ├── lock.dark.png
│   │   │   ├── lock.light.png
│   │   │   ├── logoff.dark.png
│   │   │   ├── logoff.light.png
│   │   │   ├── networkAdapter.dark.png
│   │   │   ├── networkAdapter.light.png
│   │   │   ├── recyclebin.dark.png
│   │   │   ├── recyclebin.light.png
│   │   │   ├── restart.dark.png
│   │   │   ├── restart.light.png
│   │   │   ├── shutdown.dark.png
│   │   │   ├── shutdown.light.png
│   │   │   ├── sleep.dark.png
│   │   │   └── sleep.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.System.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.System.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.System.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── TimeDate
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── Images
│   │   │   ├── calendar.dark.png
│   │   │   ├── calendar.light.png
│   │   │   ├── time.dark.png
│   │   │   ├── time.light.png
│   │   │   ├── timeDate.dark.png
│   │   │   ├── timeDate.light.png
│   │   │   ├── Warning.dark.png
│   │   │   └── Warning.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.TimeDate.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.TimeDate.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.TimeDate.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── UnitConverter
│   │   ├── ar-SA
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── de-DE
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── es-ES
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── fa-IR
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── fr-FR
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── he-IL
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── hu-HU
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── Images
│   │   │   ├── unitconverter.dark.png
│   │   │   └── unitconverter.light.png
│   │   ├── it-IT
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── ja-JP
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── ko-KR
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── nl-NL
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── pl-PL
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── pt-BR
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── pt-PT
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── ru-RU
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── sv-SE
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── tr-TR
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── uk-UA
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── zh-CN
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── zh-TW
│   │   │   └── Community.PowerToys.Run.Plugin.UnitConverter.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Community.PowerToys.Run.Plugin.UnitConverter.deps.json
│   │   ├── Community.PowerToys.Run.Plugin.UnitConverter.dll
│   │   ├── Dia2Lib.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── Uri
│   │   ├── ar-SA
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── Images
│   │   │   ├── uri.dark.png
│   │   │   └── uri.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.Plugin.Uri.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.Plugin.Uri.deps.json
│   │   ├── Microsoft.Plugin.Uri.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── ValueGenerator
│   │   ├── ar-SA
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── de-DE
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── es-ES
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── fa-IR
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── fr-FR
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── he-IL
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── hu-HU
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── Images
│   │   │   ├── ValueGenerator.dark.png
│   │   │   ├── ValueGenerator.light.png
│   │   │   ├── Warning.dark.png
│   │   │   └── Warning.light.png
│   │   ├── it-IT
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── ja-JP
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── ko-KR
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── nl-NL
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── pl-PL
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── pt-BR
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── pt-PT
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── ru-RU
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── sv-SE
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── tr-TR
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── uk-UA
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── zh-CN
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── zh-TW
│   │   │   └── Community.PowerToys.Run.Plugin.ValueGenerator.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Community.PowerToys.Run.Plugin.ValueGenerator.deps.json
│   │   ├── Community.PowerToys.Run.Plugin.ValueGenerator.dll
│   │   ├── Dia2Lib.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── VSCodeWorkspaces
│   │   ├── ar-SA
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── de-DE
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── es-ES
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── fa-IR
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── fr-FR
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── he-IL
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── hu-HU
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── Images
│   │   │   ├── code.dark.png
│   │   │   ├── code.light.png
│   │   │   ├── folder.png
│   │   │   └── monitor.png
│   │   ├── it-IT
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── ja-JP
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── ko-KR
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── nl-NL
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── pl-PL
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── pt-BR
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── pt-PT
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── ru-RU
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── sv-SE
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── tr-TR
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── uk-UA
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── zh-CN
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── zh-TW
│   │   │   └── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.deps.json
│   │   ├── Community.PowerToys.Run.Plugin.VSCodeWorkspaces.dll
│   │   ├── Dia2Lib.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── WebSearch
│   │   ├── ar-SA
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── de-DE
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── es-ES
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── fa-IR
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── fr-FR
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── he-IL
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── hu-HU
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── Images
│   │   │   ├── WebSearch.dark.png
│   │   │   └── WebSearch.light.png
│   │   ├── it-IT
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── ja-JP
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── ko-KR
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── nl-NL
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── pl-PL
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── pt-BR
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── pt-PT
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── ru-RU
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── sv-SE
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── tr-TR
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── uk-UA
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── zh-CN
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── zh-TW
│   │   │   └── Community.PowerToys.Run.Plugin.WebSearch.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Community.PowerToys.Run.Plugin.WebSearch.deps.json
│   │   ├── Community.PowerToys.Run.Plugin.WebSearch.dll
│   │   ├── Dia2Lib.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── WindowsSettings
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── Images
│   │   │   ├── WindowsSettings.dark.png
│   │   │   └── WindowsSettings.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsSettings.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.WindowsSettings.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.WindowsSettings.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   ├── WindowsTerminal
│   │   ├── ar-SA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── cs-CZ
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── de-DE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── es-ES
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── fa-IR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── fr-FR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── he-IL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── hu-HU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── Images
│   │   │   ├── WindowsTerminal.dark.png
│   │   │   └── WindowsTerminal.light.png
│   │   ├── it-IT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── ja-JP
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── ko-KR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── nl-NL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── pl-PL
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── pt-BR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── pt-PT
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── ru-RU
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── sv-SE
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── tr-TR
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── uk-UA
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── zh-CN
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── zh-TW
│   │   │   └── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.resources.dll
│   │   ├── backup_restore_settings.json
│   │   ├── Dia2Lib.dll
│   │   ├── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.deps.json
│   │   ├── Microsoft.PowerToys.Run.Plugin.WindowsTerminal.dll
│   │   ├── plugin.json
│   │   ├── PowerToys.Interop.dll
│   │   ├── PowerToys.ManagedTelemetry.dll
│   │   ├── PowerToys.MouseJump.Common.dll
│   │   ├── PowerToys.ZoomItSettingsInterop.dll
│   │   └── TraceReloggerLib.dll
│   └── WindowWalker
│       ├── ar-SA
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── cs-CZ
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── de-DE
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── es-ES
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── fa-IR
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── fr-FR
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── he-IL
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── hu-HU
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── Images
│       │   ├── info.dark.png
│       │   ├── info.light.png
│       │   ├── windowwalker.dark.png
│       │   └── windowwalker.light.png
│       ├── it-IT
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── ja-JP
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── ko-KR
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── nl-NL
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── pl-PL
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── pt-BR
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── pt-PT
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── ru-RU
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── sv-SE
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── tr-TR
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── uk-UA
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── zh-CN
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── zh-TW
│       │   └── Microsoft.Plugin.WindowWalker.resources.dll
│       ├── backup_restore_settings.json
│       ├── Dia2Lib.dll
│       ├── Microsoft.Plugin.WindowWalker.deps.json
│       ├── Microsoft.Plugin.WindowWalker.dll
│       ├── plugin.json
│       ├── PowerToys.Interop.dll
│       ├── PowerToys.ManagedTelemetry.dll
│       ├── PowerToys.MouseJump.Common.dll
│       ├── PowerToys.ZoomItSettingsInterop.dll
│       └── TraceReloggerLib.dll
├── Settings
├── sv-SE
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── Tools
│   ├── PowerToys.BugReportTool.exe
│   └── PowerToys.StylesReportTool.exe
├── tr-TR
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── uk-UA
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── WinUI3Apps
│   ├── af-ZA
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ar-SA
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── Assets
│   │   ├── AdvancedPaste
│   │   │   ├── AdvancedPaste.ico
│   │   │   ├── AdvancedPaste.png
│   │   │   ├── AIIcon.png
│   │   │   ├── Gradient.png
│   │   │   ├── LockScreenLogo.scale-200.png
│   │   │   ├── SemanticKernel.svg
│   │   │   ├── SplashScreen.scale-200.png
│   │   │   ├── Square44x44Logo.scale-200.png
│   │   │   ├── Square44x44Logo.targetsize-24_altform-unplated.png
│   │   │   ├── Square150x150Logo.scale-200.png
│   │   │   ├── StoreLogo.png
│   │   │   └── Wide310x150Logo.scale-200.png
│   │   ├── EnvironmentVariables
│   │   │   ├── EnvironmentVariables.ico
│   │   │   ├── LockScreenLogo.scale-200.png
│   │   │   ├── SplashScreen.scale-200.png
│   │   │   ├── Square44x44Logo.scale-200.png
│   │   │   ├── Square44x44Logo.targetsize-24_altform-unplated.png
│   │   │   ├── Square150x150Logo.scale-200.png
│   │   │   ├── StoreLogo.png
│   │   │   └── Wide310x150Logo.scale-200.png
│   │   ├── FileLocksmith
│   │   │   ├── AppList.scale-100.png
│   │   │   ├── AppList.scale-125.png
│   │   │   ├── AppList.scale-150.png
│   │   │   ├── AppList.scale-200.png
│   │   │   ├── AppList.scale-400.png
│   │   │   ├── FileLocksmith.ico
│   │   │   ├── Icon.ico
│   │   │   ├── LargeTile.png
│   │   │   ├── SmallTile.png
│   │   │   ├── SplashScreen.png
│   │   │   ├── Square44x44Logo.png
│   │   │   ├── Square150x150Logo.png
│   │   │   ├── storelogo.png
│   │   │   └── Wide310x150Logo.png
│   │   ├── Hosts
│   │   │   ├── AppList.scale-100.png
│   │   │   ├── AppList.scale-125.png
│   │   │   ├── AppList.scale-150.png
│   │   │   ├── AppList.scale-200.png
│   │   │   ├── AppList.scale-400.png
│   │   │   └── Hosts.ico
│   │   ├── NewPlus
│   │   │   ├── Templates
│   │   │   │   ├── 'Example folder'
│   │   │   │   │   ├── 'Another example txt file.txt'
│   │   │   │   │   └── 'Example txt file.txt'
│   │   │   │   └── 'Any files or folders placed in the template folder are available via New+.txt'
│   │   │   ├── LargeTile.png
│   │   │   ├── New_dark.ico
│   │   │   ├── New_light.ico
│   │   │   ├── Open_templates_dark.ico
│   │   │   ├── Open_templates_light.ico
│   │   │   ├── SmallTile.png
│   │   │   ├── SplashScreen.png
│   │   │   ├── Square44x44Logo.png
│   │   │   ├── Square150x150Logo.png
│   │   │   ├── StoreLogo.png
│   │   │   └── Wide310x150Logo.png
│   │   ├── Peek
│   │   │   ├── AppList.scale-100.png
│   │   │   ├── AppList.scale-125.png
│   │   │   ├── AppList.scale-150.png
│   │   │   ├── AppList.scale-200.png
│   │   │   ├── AppList.scale-400.png
│   │   │   ├── DefaultFileIcon.svg
│   │   │   └── Icon.ico
│   │   ├── PowerRename
│   │   │   ├── file.png
│   │   │   ├── folder.png
│   │   │   ├── LargeTile.png
│   │   │   ├── PowerRenameUI.ico
│   │   │   ├── SmallTile.png
│   │   │   ├── SplashScreen.png
│   │   │   ├── Square44x44Logo.png
│   │   │   ├── Square150x150Logo.png
│   │   │   ├── storelogo.png
│   │   │   └── Wide310x150Logo.png
│   │   ├── RegistryPreview
│   │   │   ├── data32.png
│   │   │   ├── deleted-folder32.png
│   │   │   ├── deleted-value32.png
│   │   │   ├── error32.png
│   │   │   ├── folder32.png
│   │   │   ├── index.html
│   │   │   ├── RegistryPreview.ico
│   │   │   └── string32.png
│   │   └── Settings
│   │       ├── Icons
│   │       │   ├── Advanced.png
│   │       │   ├── AdvancedPaste.png
│   │       │   ├── AlwaysOnTop.png
│   │       │   ├── Awake.png
│   │       │   ├── CmdPal.png
│   │       │   ├── ColorPicker.png
│   │       │   ├── CommandNotFound.png
│   │       │   ├── CropAndLock.png
│   │       │   ├── EnvironmentVariables.png
│   │       │   ├── FancyZones.png
│   │       │   ├── FileExplorerPreview.png
│   │       │   ├── FileLocksmith.png
│   │       │   ├── FileManagement.png
│   │       │   ├── FindMyMouse.png
│   │       │   ├── Hosts.png
│   │       │   ├── ImageResizer.png
│   │       │   ├── InputOutput.png
│   │       │   ├── KeyboardManager.png
│   │       │   ├── MouseCrosshairs.png
│   │       │   ├── MouseHighlighter.png
│   │       │   ├── MouseJump.png
│   │       │   ├── MouseUtils.png
│   │       │   ├── MouseWithoutBorders.png
│   │       │   ├── NewPlus.png
│   │       │   ├── Peek.png
│   │       │   ├── PowerRename.png
│   │       │   ├── PowerToys.png
│   │       │   ├── PowerToysRun.png
│   │       │   ├── QuickAccent.png
│   │       │   ├── RegistryPreview.png
│   │       │   ├── ScreenRuler.png
│   │       │   ├── SemanticKernel.png
│   │       │   ├── ShortcutGuide.png
│   │       │   ├── SystemTools.png
│   │       │   ├── TextExtractor.png
│   │       │   ├── WindowingAndLayouts.png
│   │       │   ├── Workspaces.png
│   │       │   └── ZoomIt.png
│   │       ├── Modules
│   │       │   ├── OOBE
│   │       │   │   ├── AdvancedPaste.gif
│   │       │   │   ├── AlwaysOnTop.png
│   │       │   │   ├── Awake.png
│   │       │   │   ├── CmdNotFound.png
│   │       │   │   ├── CmdPal.png
│   │       │   │   ├── ColorPicker.gif
│   │       │   │   ├── CropAndLock.gif
│   │       │   │   ├── EnvironmentVariables.png
│   │       │   │   ├── FancyZones.gif
│   │       │   │   ├── FileExplorer.png
│   │       │   │   ├── FileLocksmith.gif
│   │       │   │   ├── HostsFileEditor.png
│   │       │   │   ├── ImageResizer.gif
│   │       │   │   ├── KBM.gif
│   │       │   │   ├── MouseUtils.gif
│   │       │   │   ├── MouseWithoutBorders.png
│   │       │   │   ├── NewPlus.png
│   │       │   │   ├── Peek.png
│   │       │   │   ├── PowerRename.gif
│   │       │   │   ├── PTHero.png
│   │       │   │   ├── PTHeroShort.png
│   │       │   │   ├── QuickAccent.gif
│   │       │   │   ├── RegistryPreview.png
│   │       │   │   ├── Run.gif
│   │       │   │   ├── ScreenRuler.gif
│   │       │   │   ├── ShortcutGuide.png
│   │       │   │   ├── TextExtractor.gif
│   │       │   │   ├── Workspaces.png
│   │       │   │   └── ZoomIt.gif
│   │       │   ├── AdvancedPaste.png
│   │       │   ├── AlwaysOnTop.png
│   │       │   ├── APDialog.dark.png
│   │       │   ├── APDialog.light.png
│   │       │   ├── Awake.png
│   │       │   ├── CmdNotFound.png
│   │       │   ├── CmdPal.png
│   │       │   ├── ColorPicker.png
│   │       │   ├── CropAndLock.png
│   │       │   ├── EnvironmentVariables.png
│   │       │   ├── FancyZones.png
│   │       │   ├── FileExplorerPreview.png
│   │       │   ├── FileLocksmith.png
│   │       │   ├── HostsFileEditor.png
│   │       │   ├── ImageResizer.png
│   │       │   ├── KBM.png
│   │       │   ├── MouseUtils.png
│   │       │   ├── MouseWithoutBorders.png
│   │       │   ├── NewPlus.png
│   │       │   ├── Peek.png
│   │       │   ├── PowerLauncher.png
│   │       │   ├── PowerRename.png
│   │       │   ├── PT.png
│   │       │   ├── QuickAccent.png
│   │       │   ├── RegistryPreview.png
│   │       │   ├── Run.png
│   │       │   ├── ScreenRuler.png
│   │       │   ├── ShortcutGuide.png
│   │       │   ├── TextExtractor.png
│   │       │   ├── Wallpaper.png
│   │       │   ├── Workspaces.png
│   │       │   └── ZoomIt.png
│   │       ├── Scripts
│   │       │   ├── CheckCmdNotFoundRequirements.ps1
│   │       │   ├── DisableModule.ps1
│   │       │   ├── EnableModule.ps1
│   │       │   ├── InstallPowerShell7.ps1
│   │       │   ├── InstallWinGetClientModule.ps1
│   │       │   └── UpgradeModule.ps1
│   │       ├── icon.ico
│   │       ├── LockScreenLogo.scale-200.png
│   │       ├── logo.png
│   │       ├── Logo.scale-200.png
│   │       ├── logo44.png
│   │       ├── logo150.png
│   │       ├── SplashScreen.png
│   │       ├── SplashScreen.scale-200.png
│   │       ├── Square44x44Logo.scale-200.png
│   │       ├── Square44x44Logo.targetsize-24_altform-unplated.png
│   │       ├── Square150x150Logo.scale-200.png
│   │       ├── StoreLogo.png
│   │       ├── StoreLogo.scale-100.png
│   │       └── Wide310x150Logo.scale-200.png
│   ├── az-Latn-AZ
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── bg-BG
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── bs-Latn-BA
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ca-ES
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── CmdPal
│   │   ├── Dependencies
│   │   │   └── x64
│   │   │       └── Microsoft.VCLibs.x64.14.00.Desktop.appx
│   │   └── Microsoft.CmdPal.UI_0.2.1.0_x64.msix
│   ├── cs-CZ
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── cy-GB
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── da-DK
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── de-DE
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── el-GR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── en-GB
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── en-us
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── es-ES
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── es-MX
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── et-EE
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── eu-ES
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── fa-IR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── fi-FI
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── fr-CA
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── fr-FR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── gl-ES
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── he-IL
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── hi-IN
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── hr-HR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── hu-HU
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── id-ID
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── is-IS
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── it-IT
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ja-JP
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ka-GE
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── kk-KZ
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ko-KR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── lt-LT
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── lv-LV
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── Microsoft.UI.Xaml
│   │   └── Assets
│   │       ├── map.html
│   │       └── NoiseAsset_256x256_PNG.png
│   ├── ms-MY
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── nb-NO
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── nl-NL
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── nn-NO
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── pl-PL
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── pt-BR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── pt-PT
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ro-RO
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── ru-RU
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── sk-SK
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── sl-SI
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── sq-AL
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── sr-Cyrl-RS
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── sr-Latn-RS
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── sv-SE
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── th-TH
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── tr-TR
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── uk-UA
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── vi-VN
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── zh-CN
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── zh-TW
│   │   ├── Microsoft.ui.xaml.dll.mui
│   │   └── Microsoft.UI.Xaml.Phone.dll.mui
│   ├── Accessibility.dll
│   ├── Azure.AI.OpenAI.dll
│   ├── Azure.Core.dll
│   ├── backup_restore_settings.json
│   ├── clretwrc.dll
│   ├── clrgc.dll
│   ├── clrgcexp.dll
│   ├── clrjit.dll
│   ├── ColorCode.Core.dll
│   ├── ColorCode.WinUI.dll
│   ├── CommunityToolkit.Common.dll
│   ├── CommunityToolkit.Mvvm.dll
│   ├── CommunityToolkit.WinUI.Animations.dll
│   ├── CommunityToolkit.WinUI.Collections.dll
│   ├── CommunityToolkit.WinUI.Controls.Primitives.dll
│   ├── CommunityToolkit.WinUI.Controls.Segmented.dll
│   ├── CommunityToolkit.WinUI.Controls.SettingsControls.dll
│   ├── CommunityToolkit.WinUI.Controls.Sizers.dll
│   ├── CommunityToolkit.WinUI.Converters.dll
│   ├── CommunityToolkit.WinUI.dll
│   ├── CommunityToolkit.WinUI.Extensions.dll
│   ├── CommunityToolkit.WinUI.Helpers.dll
│   ├── CommunityToolkit.WinUI.Triggers.dll
│   ├── CommunityToolkit.WinUI.UI.Controls.Core.dll
│   ├── CommunityToolkit.WinUI.UI.Controls.DataGrid.dll
│   ├── CommunityToolkit.WinUI.UI.Controls.Markdown.dll
│   ├── CommunityToolkit.WinUI.UI.Controls.Primitives.dll
│   ├── CommunityToolkit.WinUI.UI.dll
│   ├── concrt140.dll
│   ├── ControlzEx.dll
│   ├── coreclr.dll
│   ├── CoreMessagingXP.dll
│   ├── D3DCompiler_47_cor3.dll
│   ├── dcompi.dll
│   ├── Dia2Lib.dll
│   ├── DirectWriteForwarder.dll
│   ├── dwmcorei.dll
│   ├── DwmSceneI.dll
│   ├── DWriteCore.dll
│   ├── FileLocksmithContextMenuPackage.msix
│   ├── hostfxr.dll
│   ├── hostpolicy.dll
│   ├── HtmlAgilityPack.dll
│   ├── Markdig.Signed.dll
│   ├── marshal.dll
│   ├── MessagePack.Annotations.dll
│   ├── MessagePack.dll
│   ├── mfc140.dll
│   ├── mfc140chs.dll
│   ├── mfc140cht.dll
│   ├── mfc140deu.dll
│   ├── mfc140enu.dll
│   ├── mfc140esn.dll
│   ├── mfc140fra.dll
│   ├── mfc140ita.dll
│   ├── mfc140jpn.dll
│   ├── mfc140kor.dll
│   ├── mfc140rus.dll
│   ├── mfc140u.dll
│   ├── mfcm140.dll
│   ├── mfcm140u.dll
│   ├── Microsoft.Bcl.AsyncInterfaces.dll
│   ├── Microsoft.Bcl.HashCode.dll
│   ├── Microsoft.CSharp.dll
│   ├── Microsoft.Diagnostics.FastSerialization.dll
│   ├── Microsoft.Diagnostics.NETCore.Client.dll
│   ├── Microsoft.Diagnostics.Tracing.TraceEvent.dll
│   ├── Microsoft.DiaSymReader.Native.amd64.dll
│   ├── Microsoft.DirectManipulation.dll
│   ├── Microsoft.Extensions.Configuration.Abstractions.dll
│   ├── Microsoft.Extensions.Configuration.Binder.dll
│   ├── Microsoft.Extensions.Configuration.CommandLine.dll
│   ├── Microsoft.Extensions.Configuration.dll
│   ├── Microsoft.Extensions.Configuration.EnvironmentVariables.dll
│   ├── Microsoft.Extensions.Configuration.FileExtensions.dll
│   ├── Microsoft.Extensions.Configuration.Json.dll
│   ├── Microsoft.Extensions.Configuration.UserSecrets.dll
│   ├── Microsoft.Extensions.DependencyInjection.Abstractions.dll
│   ├── Microsoft.Extensions.DependencyInjection.dll
│   ├── Microsoft.Extensions.Diagnostics.Abstractions.dll
│   ├── Microsoft.Extensions.Diagnostics.dll
│   ├── Microsoft.Extensions.FileProviders.Abstractions.dll
│   ├── Microsoft.Extensions.FileProviders.Physical.dll
│   ├── Microsoft.Extensions.FileSystemGlobbing.dll
│   ├── Microsoft.Extensions.Hosting.Abstractions.dll
│   ├── Microsoft.Extensions.Hosting.dll
│   ├── Microsoft.Extensions.Logging.Abstractions.dll
│   ├── Microsoft.Extensions.Logging.Configuration.dll
│   ├── Microsoft.Extensions.Logging.Console.dll
│   ├── Microsoft.Extensions.Logging.Debug.dll
│   ├── Microsoft.Extensions.Logging.dll
│   ├── Microsoft.Extensions.Logging.EventLog.dll
│   ├── Microsoft.Extensions.Logging.EventSource.dll
│   ├── Microsoft.Extensions.ObjectPool.dll
│   ├── Microsoft.Extensions.Options.ConfigurationExtensions.dll
│   ├── Microsoft.Extensions.Options.dll
│   ├── Microsoft.Extensions.Primitives.dll
│   ├── Microsoft.Graphics.Display.dll
│   ├── Microsoft.InputStateManager.dll
│   ├── Microsoft.InteractiveExperiences.Projection.dll
│   ├── Microsoft.Internal.FrameworkUdk.dll
│   ├── Microsoft.NET.StringTools.dll
│   ├── Microsoft.Security.Authentication.OAuth.Projection.dll
│   ├── Microsoft.SemanticKernel.Abstractions.dll
│   ├── Microsoft.SemanticKernel.Connectors.OpenAI.dll
│   ├── Microsoft.SemanticKernel.Core.dll
│   ├── Microsoft.SemanticKernel.dll
│   ├── Microsoft.UI.Composition.OSSupport.dll
│   ├── Microsoft.UI.dll
│   ├── Microsoft.UI.Input.dll
│   ├── Microsoft.UI.pri
│   ├── Microsoft.UI.Windowing.Core.dll
│   ├── Microsoft.UI.Windowing.dll
│   ├── Microsoft.UI.Xaml.Controls.dll
│   ├── Microsoft.UI.Xaml.Controls.pri
│   ├── Microsoft.ui.xaml.dll
│   ├── Microsoft.UI.Xaml.Internal.dll
│   ├── Microsoft.UI.Xaml.Phone.dll
│   ├── Microsoft.ui.xaml.resources.19h1.dll
│   ├── Microsoft.ui.xaml.resources.common.dll
│   ├── Microsoft.VisualBasic.Core.dll
│   ├── Microsoft.VisualBasic.dll
│   ├── Microsoft.VisualBasic.Forms.dll
│   ├── Microsoft.VisualStudio.Threading.dll
│   ├── Microsoft.VisualStudio.Validation.dll
│   ├── Microsoft.Web.WebView2.Core.dll
│   ├── Microsoft.Web.WebView2.Core.Projection.dll
│   ├── Microsoft.Win32.Primitives.dll
│   ├── Microsoft.Win32.Registry.AccessControl.dll
│   ├── Microsoft.Win32.Registry.dll
│   ├── Microsoft.Win32.SystemEvents.dll
│   ├── Microsoft.Windows.ApplicationModel.Background.Projection.dll
│   ├── Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.dll
│   ├── Microsoft.Windows.ApplicationModel.DynamicDependency.Projection.dll
│   ├── Microsoft.Windows.ApplicationModel.Resources.dll
│   ├── Microsoft.Windows.ApplicationModel.Resources.Projection.dll
│   ├── Microsoft.Windows.ApplicationModel.WindowsAppRuntime.Projection.dll
│   ├── Microsoft.Windows.AppLifecycle.Projection.dll
│   ├── Microsoft.Windows.AppNotifications.Builder.Projection.dll
│   ├── Microsoft.Windows.AppNotifications.Projection.dll
│   ├── Microsoft.Windows.BadgeNotifications.Projection.dll
│   ├── Microsoft.Windows.Management.Deployment.Projection.dll
│   ├── Microsoft.Windows.Media.Capture.Projection.dll
│   ├── Microsoft.Windows.PushNotifications.Projection.dll
│   ├── Microsoft.Windows.SDK.NET.dll
│   ├── Microsoft.Windows.Security.AccessControl.Projection.dll
│   ├── Microsoft.Windows.Storage.Projection.dll
│   ├── Microsoft.Windows.System.Power.Projection.dll
│   ├── Microsoft.Windows.System.Projection.dll
│   ├── Microsoft.Windows.Widgets.dll
│   ├── Microsoft.Windows.Widgets.Projection.dll
│   ├── Microsoft.WindowsAppRuntime.Bootstrap.dll
│   ├── Microsoft.WindowsAppRuntime.Bootstrap.Net.dll
│   ├── Microsoft.WindowsAppRuntime.dll
│   ├── Microsoft.WindowsAppRuntime.Insights.Resource.dll
│   ├── Microsoft.WinUI.dll
│   ├── Microsoft.Xaml.Behaviors.dll
│   ├── Microsoft.Xaml.Interactions.dll
│   ├── Microsoft.Xaml.Interactivity.dll
│   ├── MRM.dll
│   ├── mscordaccore.dll
│   ├── mscordaccore_amd64_amd64_9.0.525.21509.dll
│   ├── mscordbi.dll
│   ├── mscorlib.dll
│   ├── mscorrc.dll
│   ├── msquic.dll
│   ├── msvcp140.dll
│   ├── msvcp140_1.dll
│   ├── msvcp140_2.dll
│   ├── msvcp140_atomic_wait.dll
│   ├── msvcp140_codecvt_ids.dll
│   ├── Nerdbank.Streams.dll
│   ├── netstandard.dll
│   ├── NewPlusPackage.msix
│   ├── Newtonsoft.Json.dll
│   ├── OpenAI.dll
│   ├── Peek.Common.dll
│   ├── Peek.Common.pri
│   ├── Peek.FilePreviewer.dll
│   ├── Peek.FilePreviewer.pri
│   ├── PenImc_cor3.dll
│   ├── PowerRenameContextMenuPackage.msix
│   ├── PowerRenameUI.ico
│   ├── PowerToys.AdvancedPaste.deps.json
│   ├── PowerToys.AdvancedPaste.dll
│   ├── PowerToys.AdvancedPaste.exe
│   ├── PowerToys.AdvancedPaste.pri
│   ├── PowerToys.AdvancedPaste.runtimeconfig.json
│   ├── PowerToys.AllExperiments.dll
│   ├── PowerToys.Common.UI.dll
│   ├── PowerToys.EnvironmentVariables.deps.json
│   ├── PowerToys.EnvironmentVariables.dll
│   ├── PowerToys.EnvironmentVariables.exe
│   ├── PowerToys.EnvironmentVariables.pri
│   ├── PowerToys.EnvironmentVariables.runtimeconfig.json
│   ├── PowerToys.EnvironmentVariablesModuleInterface.dll
│   ├── PowerToys.EnvironmentVariablesUILib.dll
│   ├── PowerToys.EnvironmentVariablesUILib.pri
│   ├── PowerToys.FileLocksmithContextMenu.dll
│   ├── PowerToys.FileLocksmithExt.dll
│   ├── PowerToys.FileLocksmithLib.Interop.dll
│   ├── PowerToys.FileLocksmithUI.deps.json
│   ├── PowerToys.FileLocksmithUI.dll
│   ├── PowerToys.FileLocksmithUI.exe
│   ├── PowerToys.FileLocksmithUI.pri
│   ├── PowerToys.FileLocksmithUI.runtimeconfig.json
│   ├── PowerToys.FilePreviewCommon.dll
│   ├── PowerToys.GPOWrapper.dll
│   ├── PowerToys.Hosts.deps.json
│   ├── PowerToys.Hosts.dll
│   ├── PowerToys.Hosts.exe
│   ├── PowerToys.Hosts.pri
│   ├── PowerToys.Hosts.runtimeconfig.json
│   ├── PowerToys.HostsModuleInterface.dll
│   ├── PowerToys.HostsUILib.dll
│   ├── PowerToys.HostsUILib.pri
│   ├── PowerToys.Interop.dll
│   ├── PowerToys.ManagedCommon.dll
│   ├── PowerToys.ManagedTelemetry.dll
│   ├── PowerToys.MeasureToolCore.dll
│   ├── PowerToys.MeasureToolModuleInterface.dll
│   ├── PowerToys.MeasureToolUI.deps.json
│   ├── PowerToys.MeasureToolUI.dll
│   ├── PowerToys.MeasureToolUI.exe
│   ├── PowerToys.MeasureToolUI.pri
│   ├── PowerToys.MeasureToolUI.runtimeconfig.json
│   ├── PowerToys.MouseJump.Common.dll
│   ├── PowerToys.NewPlus.ShellExtension.dll
│   ├── PowerToys.NewPlus.ShellExtension.win10.dll
│   ├── PowerToys.Peek.dll
│   ├── PowerToys.Peek.UI.deps.json
│   ├── PowerToys.Peek.UI.dll
│   ├── PowerToys.Peek.UI.exe
│   ├── PowerToys.Peek.UI.pri
│   ├── PowerToys.Peek.UI.runtimeconfig.json
│   ├── PowerToys.PowerRename.exe
│   ├── PowerToys.PowerRename.pri
│   ├── PowerToys.PowerRenameContextMenu.dll
│   ├── PowerToys.PowerRenameExt.dll
│   ├── PowerToys.RegistryPreview.deps.json
│   ├── PowerToys.RegistryPreview.dll
│   ├── PowerToys.RegistryPreview.exe
│   ├── PowerToys.RegistryPreview.pri
│   ├── PowerToys.RegistryPreview.runtimeconfig.json
│   ├── PowerToys.RegistryPreviewExt.dll
│   ├── PowerToys.RegistryPreviewUILib.dll
│   ├── PowerToys.RegistryPreviewUILib.pri
│   ├── PowerToys.Settings.deps.json
│   ├── PowerToys.Settings.dll
│   ├── PowerToys.Settings.exe
│   ├── PowerToys.Settings.pri
│   ├── PowerToys.Settings.runtimeconfig.json
│   ├── PowerToys.Settings.UI.Lib.dll
│   ├── PowerToys.ZoomItSettingsInterop.dll
│   ├── PresentationCore.dll
│   ├── PresentationFramework-SystemCore.dll
│   ├── PresentationFramework-SystemData.dll
│   ├── PresentationFramework-SystemDrawing.dll
│   ├── PresentationFramework-SystemXml.dll
│   ├── PresentationFramework-SystemXmlLinq.dll
│   ├── PresentationFramework.Aero.dll
│   ├── PresentationFramework.Aero2.dll
│   ├── PresentationFramework.AeroLite.dll
│   ├── PresentationFramework.Classic.dll
│   ├── PresentationFramework.dll
│   ├── PresentationFramework.Fluent.dll
│   ├── PresentationFramework.Luna.dll
│   ├── PresentationFramework.Royale.dll
│   ├── PresentationNative_cor3.dll
│   ├── PresentationUI.dll
│   ├── PushNotificationsLongRunningTask.ProxyStub.dll
│   ├── ReachFramework.dll
│   ├── RestartAgent.exe
│   ├── ReverseMarkdown.dll
│   ├── SharpCompress.dll
│   ├── sni.dll
│   ├── StreamJsonRpc.dll
│   ├── System.AppContext.dll
│   ├── System.Buffers.dll
│   ├── System.ClientModel.dll
│   ├── System.CodeDom.dll
│   ├── System.Collections.Concurrent.dll
│   ├── System.Collections.dll
│   ├── System.Collections.Immutable.dll
│   ├── System.Collections.NonGeneric.dll
│   ├── System.Collections.Specialized.dll
│   ├── System.ComponentModel.Annotations.dll
│   ├── System.ComponentModel.Composition.dll
│   ├── System.ComponentModel.Composition.Registration.dll
│   ├── System.ComponentModel.DataAnnotations.dll
│   ├── System.ComponentModel.dll
│   ├── System.ComponentModel.EventBasedAsync.dll
│   ├── System.ComponentModel.Primitives.dll
│   ├── System.ComponentModel.TypeConverter.dll
│   ├── System.Configuration.ConfigurationManager.dll
│   ├── System.Configuration.dll
│   ├── System.Console.dll
│   ├── System.Core.dll
│   ├── System.Data.Common.dll
│   ├── System.Data.DataSetExtensions.dll
│   ├── System.Data.dll
│   ├── System.Data.Odbc.dll
│   ├── System.Data.OleDb.dll
│   ├── System.Data.SqlClient.dll
│   ├── System.Design.dll
│   ├── System.Diagnostics.Contracts.dll
│   ├── System.Diagnostics.Debug.dll
│   ├── System.Diagnostics.DiagnosticSource.dll
│   ├── System.Diagnostics.EventLog.dll
│   ├── System.Diagnostics.EventLog.Messages.dll
│   ├── System.Diagnostics.FileVersionInfo.dll
│   ├── System.Diagnostics.PerformanceCounter.dll
│   ├── System.Diagnostics.Process.dll
│   ├── System.Diagnostics.StackTrace.dll
│   ├── System.Diagnostics.TextWriterTraceListener.dll
│   ├── System.Diagnostics.Tools.dll
│   ├── System.Diagnostics.TraceSource.dll
│   ├── System.Diagnostics.Tracing.dll
│   ├── System.DirectoryServices.AccountManagement.dll
│   ├── System.DirectoryServices.dll
│   ├── System.DirectoryServices.Protocols.dll
│   ├── System.dll
│   ├── System.Drawing.Common.dll
│   ├── System.Drawing.Design.dll
│   ├── System.Drawing.dll
│   ├── System.Drawing.Primitives.dll
│   ├── System.Dynamic.Runtime.dll
│   ├── System.Formats.Asn1.dll
│   ├── System.Formats.Nrbf.dll
│   ├── System.Formats.Tar.dll
│   ├── System.Globalization.Calendars.dll
│   ├── System.Globalization.dll
│   ├── System.Globalization.Extensions.dll
│   ├── System.IO.Abstractions.dll
│   ├── System.IO.Compression.Brotli.dll
│   ├── System.IO.Compression.dll
│   ├── System.IO.Compression.FileSystem.dll
│   ├── System.IO.Compression.Native.dll
│   ├── System.IO.Compression.ZipFile.dll
│   ├── System.IO.dll
│   ├── System.IO.FileSystem.AccessControl.dll
│   ├── System.IO.FileSystem.dll
│   ├── System.IO.FileSystem.DriveInfo.dll
│   ├── System.IO.FileSystem.Primitives.dll
│   ├── System.IO.FileSystem.Watcher.dll
│   ├── System.IO.IsolatedStorage.dll
│   ├── System.IO.MemoryMappedFiles.dll
│   ├── System.IO.Packaging.dll
│   ├── System.IO.Pipelines.dll
│   ├── System.IO.Pipes.AccessControl.dll
│   ├── System.IO.Pipes.dll
│   ├── System.IO.Ports.dll
│   ├── System.IO.UnmanagedMemoryStream.dll
│   ├── System.Linq.dll
│   ├── System.Linq.Expressions.dll
│   ├── System.Linq.Parallel.dll
│   ├── System.Linq.Queryable.dll
│   ├── System.Management.dll
│   ├── System.Memory.Data.dll
│   ├── System.Memory.dll
│   ├── System.Net.dll
│   ├── System.Net.Http.dll
│   ├── System.Net.Http.Json.dll
│   ├── System.Net.HttpListener.dll
│   ├── System.Net.Mail.dll
│   ├── System.Net.NameResolution.dll
│   ├── System.Net.NetworkInformation.dll
│   ├── System.Net.Ping.dll
│   ├── System.Net.Primitives.dll
│   ├── System.Net.Quic.dll
│   ├── System.Net.Requests.dll
│   ├── System.Net.Security.dll
│   ├── System.Net.ServicePoint.dll
│   ├── System.Net.Sockets.dll
│   ├── System.Net.WebClient.dll
│   ├── System.Net.WebHeaderCollection.dll
│   ├── System.Net.WebProxy.dll
│   ├── System.Net.WebSockets.Client.dll
│   ├── System.Net.WebSockets.dll
│   ├── System.Numerics.dll
│   ├── System.Numerics.Vectors.dll
│   ├── System.ObjectModel.dll
│   ├── System.Printing.dll
│   ├── System.Private.CoreLib.dll
│   ├── System.Private.DataContractSerialization.dll
│   ├── System.Private.ServiceModel.dll
│   ├── System.Private.Uri.dll
│   ├── System.Private.Windows.Core.dll
│   ├── System.Private.Xml.dll
│   ├── System.Private.Xml.Linq.dll
│   ├── System.Reflection.Context.dll
│   ├── System.Reflection.DispatchProxy.dll
│   ├── System.Reflection.dll
│   ├── System.Reflection.Emit.dll
│   ├── System.Reflection.Emit.ILGeneration.dll
│   ├── System.Reflection.Emit.Lightweight.dll
│   ├── System.Reflection.Extensions.dll
│   ├── System.Reflection.Metadata.dll
│   ├── System.Reflection.Primitives.dll
│   ├── System.Reflection.TypeExtensions.dll
│   ├── System.Resources.Extensions.dll
│   ├── System.Resources.Reader.dll
│   ├── System.Resources.ResourceManager.dll
│   ├── System.Resources.Writer.dll
│   ├── System.Runtime.Caching.dll
│   ├── System.Runtime.CompilerServices.Unsafe.dll
│   ├── System.Runtime.CompilerServices.VisualC.dll
│   ├── System.Runtime.dll
│   ├── System.Runtime.Extensions.dll
│   ├── System.Runtime.Handles.dll
│   ├── System.Runtime.InteropServices.dll
│   ├── System.Runtime.InteropServices.JavaScript.dll
│   ├── System.Runtime.InteropServices.RuntimeInformation.dll
│   ├── System.Runtime.Intrinsics.dll
│   ├── System.Runtime.Loader.dll
│   ├── System.Runtime.Numerics.dll
│   ├── System.Runtime.Serialization.dll
│   ├── System.Runtime.Serialization.Formatters.dll
│   ├── System.Runtime.Serialization.Json.dll
│   ├── System.Runtime.Serialization.Primitives.dll
│   ├── System.Runtime.Serialization.Xml.dll
│   ├── System.Security.AccessControl.dll
│   ├── System.Security.Claims.dll
│   ├── System.Security.Cryptography.Algorithms.dll
│   ├── System.Security.Cryptography.Cng.dll
│   ├── System.Security.Cryptography.Csp.dll
│   ├── System.Security.Cryptography.dll
│   ├── System.Security.Cryptography.Encoding.dll
│   ├── System.Security.Cryptography.OpenSsl.dll
│   ├── System.Security.Cryptography.Pkcs.dll
│   ├── System.Security.Cryptography.Primitives.dll
│   ├── System.Security.Cryptography.ProtectedData.dll
│   ├── System.Security.Cryptography.X509Certificates.dll
│   ├── System.Security.Cryptography.Xml.dll
│   ├── System.Security.dll
│   ├── System.Security.Permissions.dll
│   ├── System.Security.Principal.dll
│   ├── System.Security.Principal.Windows.dll
│   ├── System.Security.SecureString.dll
│   ├── System.ServiceModel.dll
│   ├── System.ServiceModel.Duplex.dll
│   ├── System.ServiceModel.Http.dll
│   ├── System.ServiceModel.NetTcp.dll
│   ├── System.ServiceModel.Primitives.dll
│   ├── System.ServiceModel.Security.dll
│   ├── System.ServiceModel.Syndication.dll
│   ├── System.ServiceModel.Web.dll
│   ├── System.ServiceProcess.dll
│   ├── System.ServiceProcess.ServiceController.dll
│   ├── System.Speech.dll
│   ├── System.Text.Encoding.CodePages.dll
│   ├── System.Text.Encoding.dll
│   ├── System.Text.Encoding.Extensions.dll
│   ├── System.Text.Encodings.Web.dll
│   ├── System.Text.Json.dll
│   ├── System.Text.RegularExpressions.dll
│   ├── System.Threading.AccessControl.dll
│   ├── System.Threading.Channels.dll
│   ├── System.Threading.dll
│   ├── System.Threading.Overlapped.dll
│   ├── System.Threading.Tasks.Dataflow.dll
│   ├── System.Threading.Tasks.dll
│   ├── System.Threading.Tasks.Extensions.dll
│   ├── System.Threading.Tasks.Parallel.dll
│   ├── System.Threading.Thread.dll
│   ├── System.Threading.ThreadPool.dll
│   ├── System.Threading.Timer.dll
│   ├── System.Transactions.dll
│   ├── System.Transactions.Local.dll
│   ├── System.ValueTuple.dll
│   ├── System.Web.dll
│   ├── System.Web.HttpUtility.dll
│   ├── System.Web.Services.Description.dll
│   ├── System.Windows.Controls.Ribbon.dll
│   ├── System.Windows.dll
│   ├── System.Windows.Extensions.dll
│   ├── System.Windows.Forms.Design.dll
│   ├── System.Windows.Forms.Design.Editors.dll
│   ├── System.Windows.Forms.dll
│   ├── System.Windows.Forms.Primitives.dll
│   ├── System.Windows.Input.Manipulations.dll
│   ├── System.Windows.Presentation.dll
│   ├── System.Xaml.dll
│   ├── System.Xml.dll
│   ├── System.Xml.Linq.dll
│   ├── System.Xml.ReaderWriter.dll
│   ├── System.Xml.Serialization.dll
│   ├── System.Xml.XDocument.dll
│   ├── System.Xml.XmlDocument.dll
│   ├── System.Xml.XmlSerializer.dll
│   ├── System.Xml.XPath.dll
│   ├── System.Xml.XPath.XDocument.dll
│   ├── TestableIO.System.IO.Abstractions.dll
│   ├── TestableIO.System.IO.Abstractions.Wrappers.dll
│   ├── Testably.Abstractions.FileSystem.Interface.dll
│   ├── TraceReloggerLib.dll
│   ├── UIAutomationClient.dll
│   ├── UIAutomationClientSideProviders.dll
│   ├── UIAutomationProvider.dll
│   ├── UIAutomationTypes.dll
│   ├── UtfUnknown.dll
│   ├── vcamp140.dll
│   ├── vccorlib140.dll
│   ├── vcomp140.dll
│   ├── vcruntime140.dll
│   ├── vcruntime140_1.dll
│   ├── vcruntime140_cor3.dll
│   ├── vcruntime140_threads.dll
│   ├── WebView2Loader.dll
│   ├── WindowsAppRuntime.DeploymentExtensions.OneCore.dll
│   ├── WindowsAppRuntime.png
│   ├── WindowsAppSdk.AppxDeploymentExtensions.Desktop-EventLog-Instrumentation.dll
│   ├── WindowsAppSdk.AppxDeploymentExtensions.Desktop.dll
│   ├── WindowsBase.dll
│   ├── WindowsFormsIntegration.dll
│   ├── WinRT.Runtime.dll
│   ├── WinUIEdit.dll
│   ├── WinUIEx.dll
│   ├── wpfgfx_cor3.dll
│   ├── wuceffectsi.dll
│   └── ZstdSharp.dll
├── zh-CN
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── zh-TW
│   ├── PowerToys.Awake.resources.dll
│   ├── PowerToys.ColorPickerUI.resources.dll
│   ├── PowerToys.FancyZonesEditor.resources.dll
│   ├── PowerToys.GcodePreviewHandler.resources.dll
│   ├── PowerToys.ImageResizer.resources.dll
│   ├── PowerToys.MarkdownPreviewHandler.resources.dll
│   ├── PowerToys.MonacoPreviewHandler.resources.dll
│   ├── PowerToys.PdfPreviewHandler.resources.dll
│   ├── PowerToys.PowerLauncher.resources.dll
│   ├── PowerToys.PowerOCR.resources.dll
│   ├── PowerToys.QoiPreviewHandler.resources.dll
│   ├── PowerToys.SvgPreviewHandler.resources.dll
│   └── PowerToys.WorkspacesEditor.resources.dll
├── Accessibility.dll
├── backup_restore_settings.json
├── clretwrc.dll
├── clrgc.dll
├── clrgcexp.dll
├── clrjit.dll
├── CmdPalKeyboardService.dll
├── CmdPalKeyboardService.pri
├── concrt140.dll
├── ControlzEx.dll
├── coreclr.dll
├── D3DCompiler_47_cor3.dll
├── Dia2Lib.dll
├── DirectWriteForwarder.dll
├── e_sqlite3.dll
├── HelixToolkit.Core.Wpf.dll
├── HelixToolkit.dll
├── hostfxr.dll
├── hostpolicy.dll
├── hyjiacan.py4n.dll
├── ImageResizerContextMenuPackage.msix
├── Interop.Microsoft.Office.Interop.OneNote.dll
├── LazyCache.dll
├── License.rtf
├── Mages.Core.dll
├── Markdig.Signed.dll
├── MessagePack.Annotations.dll
├── MessagePack.dll
├── mfc140.dll
├── mfc140chs.dll
├── mfc140cht.dll
├── mfc140deu.dll
├── mfc140enu.dll
├── mfc140esn.dll
├── mfc140fra.dll
├── mfc140ita.dll
├── mfc140jpn.dll
├── mfc140kor.dll
├── mfc140rus.dll
├── mfc140u.dll
├── mfcm140.dll
├── mfcm140u.dll
├── Microsoft.Bcl.AsyncInterfaces.dll
├── Microsoft.CSharp.dll
├── Microsoft.Data.Sqlite.dll
├── Microsoft.Diagnostics.FastSerialization.dll
├── Microsoft.Diagnostics.NETCore.Client.dll
├── Microsoft.Diagnostics.Tracing.TraceEvent.dll
├── Microsoft.DiaSymReader.Native.amd64.dll
├── Microsoft.Extensions.Caching.Abstractions.dll
├── Microsoft.Extensions.Caching.Memory.dll
├── Microsoft.Extensions.Configuration.Abstractions.dll
├── Microsoft.Extensions.Configuration.Binder.dll
├── Microsoft.Extensions.Configuration.CommandLine.dll
├── Microsoft.Extensions.Configuration.dll
├── Microsoft.Extensions.Configuration.EnvironmentVariables.dll
├── Microsoft.Extensions.Configuration.FileExtensions.dll
├── Microsoft.Extensions.Configuration.Json.dll
├── Microsoft.Extensions.Configuration.UserSecrets.dll
├── Microsoft.Extensions.DependencyInjection.Abstractions.dll
├── Microsoft.Extensions.DependencyInjection.dll
├── Microsoft.Extensions.Diagnostics.Abstractions.dll
├── Microsoft.Extensions.Diagnostics.dll
├── Microsoft.Extensions.FileProviders.Abstractions.dll
├── Microsoft.Extensions.FileProviders.Physical.dll
├── Microsoft.Extensions.FileSystemGlobbing.dll
├── Microsoft.Extensions.Hosting.Abstractions.dll
├── Microsoft.Extensions.Hosting.dll
├── Microsoft.Extensions.Hosting.WindowsServices.dll
├── Microsoft.Extensions.Logging.Abstractions.dll
├── Microsoft.Extensions.Logging.Configuration.dll
├── Microsoft.Extensions.Logging.Console.dll
├── Microsoft.Extensions.Logging.Debug.dll
├── Microsoft.Extensions.Logging.dll
├── Microsoft.Extensions.Logging.EventLog.dll
├── Microsoft.Extensions.Logging.EventSource.dll
├── Microsoft.Extensions.ObjectPool.dll
├── Microsoft.Extensions.Options.ConfigurationExtensions.dll
├── Microsoft.Extensions.Options.dll
├── Microsoft.Extensions.Primitives.dll
├── Microsoft.NET.StringTools.dll
├── Microsoft.Toolkit.Uwp.Notifications.dll
├── Microsoft.Toolkit.Win32.UI.XamlHost.dll
├── Microsoft.Toolkit.Win32.UI.XamlHost.pri
├── Microsoft.UI.Xaml.dll
├── Microsoft.UI.Xaml.pri
├── Microsoft.VisualBasic.Core.dll
├── Microsoft.VisualBasic.dll
├── Microsoft.VisualBasic.Forms.dll
├── Microsoft.VisualStudio.Threading.dll
├── Microsoft.VisualStudio.Validation.dll
├── Microsoft.Web.WebView2.Core.dll
├── Microsoft.Web.WebView2.WinForms.dll
├── Microsoft.Web.WebView2.Wpf.dll
├── Microsoft.Win32.Primitives.dll
├── Microsoft.Win32.Registry.AccessControl.dll
├── Microsoft.Win32.Registry.dll
├── Microsoft.Win32.SystemEvents.dll
├── Microsoft.Windows.SDK.NET.dll
├── Microsoft.Xaml.Behaviors.dll
├── ModernWpf.Controls.dll
├── ModernWpf.dll
├── mscordaccore.dll
├── mscordaccore_amd64_amd64_9.0.525.21509.dll
├── mscordbi.dll
├── mscorlib.dll
├── mscorrc.dll
├── msquic.dll
├── msvcp140.dll
├── msvcp140_1.dll
├── msvcp140_2.dll
├── msvcp140_atomic_wait.dll
├── msvcp140_codecvt_ids.dll
├── Nerdbank.Streams.dll
├── netstandard.dll
├── Newtonsoft.Json.dll
├── NLog.dll
├── NLog.Extensions.Logging.dll
├── Notice.md
├── PenImc_cor3.dll
├── PowerAccent.Core.dll
├── PowerToys.ActionRunner.exe
├── PowerToys.AdvancedPasteModuleInterface.dll
├── PowerToys.AlwaysOnTop.exe
├── PowerToys.AlwaysOnTopModuleInterface.dll
├── PowerToys.Awake.deps.json
├── PowerToys.Awake.dll
├── PowerToys.Awake.exe
├── PowerToys.Awake.runtimeconfig.json
├── PowerToys.AwakeModuleInterface.dll
├── PowerToys.BackgroundActivatorDLL.dll
├── PowerToys.CmdNotFoundModuleInterface.dll
├── PowerToys.CmdPalModuleInterface.dll
├── PowerToys.ColorPicker.dll
├── PowerToys.ColorPickerUI.deps.json
├── PowerToys.ColorPickerUI.dll
├── PowerToys.ColorPickerUI.exe
├── PowerToys.ColorPickerUI.runtimeconfig.json
├── PowerToys.Common.UI.dll
├── PowerToys.CropAndLock.exe
├── PowerToys.CropAndLockModuleInterface.dll
├── PowerToys.exe
├── PowerToys.FancyZones.exe
├── PowerToys.FancyZonesEditor.deps.json
├── PowerToys.FancyZonesEditor.dll
├── PowerToys.FancyZonesEditor.exe
├── PowerToys.FancyZonesEditor.runtimeconfig.json
├── PowerToys.FancyZonesEditorCommon.dll
├── PowerToys.FancyZonesModuleInterface.dll
├── PowerToys.FilePreviewCommon.dll
├── PowerToys.FindMyMouse.dll
├── PowerToys.GcodePreviewHandler.deps.json
├── PowerToys.GcodePreviewHandler.dll
├── PowerToys.GcodePreviewHandler.exe
├── PowerToys.GcodePreviewHandler.runtimeconfig.json
├── PowerToys.GcodePreviewHandlerCpp.dll
├── PowerToys.GcodeThumbnailProvider.deps.json
├── PowerToys.GcodeThumbnailProvider.dll
├── PowerToys.GcodeThumbnailProvider.exe
├── PowerToys.GcodeThumbnailProvider.runtimeconfig.json
├── PowerToys.GcodeThumbnailProviderCpp.dll
├── PowerToys.GPOWrapper.dll
├── PowerToys.GPOWrapperProjection.dll
├── PowerToys.ImageResizer.deps.json
├── PowerToys.ImageResizer.dll
├── PowerToys.ImageResizer.exe
├── PowerToys.ImageResizer.runtimeconfig.json
├── PowerToys.ImageResizerContextMenu.dll
├── PowerToys.ImageResizerExt.dll
├── PowerToys.Interop.dll
├── PowerToys.KeyboardManager.dll
├── PowerToys.KeyboardManagerEditorLibraryWrapper.dll
├── PowerToys.Launcher.dll
├── PowerToys.ManagedCommon.dll
├── PowerToys.ManagedTelemetry.dll
├── PowerToys.MarkdownPreviewHandler.deps.json
├── PowerToys.MarkdownPreviewHandler.dll
├── PowerToys.MarkdownPreviewHandler.exe
├── PowerToys.MarkdownPreviewHandler.runtimeconfig.json
├── PowerToys.MarkdownPreviewHandlerCpp.dll
├── PowerToys.MonacoPreviewHandler.deps.json
├── PowerToys.MonacoPreviewHandler.dll
├── PowerToys.MonacoPreviewHandler.exe
├── PowerToys.MonacoPreviewHandler.runtimeconfig.json
├── PowerToys.MonacoPreviewHandlerCpp.dll
├── PowerToys.MouseHighlighter.dll
├── PowerToys.MouseJump.Common.deps.json
├── PowerToys.MouseJump.Common.dll
├── PowerToys.MouseJump.dll
├── PowerToys.MouseJumpUI.deps.json
├── PowerToys.MouseJumpUI.dll
├── PowerToys.MouseJumpUI.exe
├── PowerToys.MouseJumpUI.runtimeconfig.json
├── PowerToys.MousePointerCrosshairs.dll
├── PowerToys.MouseWithoutBorders.deps.json
├── PowerToys.MouseWithoutBorders.dll
├── PowerToys.MouseWithoutBorders.exe
├── PowerToys.MouseWithoutBorders.runtimeconfig.json
├── PowerToys.MouseWithoutBordersHelper.deps.json
├── PowerToys.MouseWithoutBordersHelper.dll
├── PowerToys.MouseWithoutBordersHelper.exe
├── PowerToys.MouseWithoutBordersHelper.runtimeconfig.json
├── PowerToys.MouseWithoutBordersModuleInterface.dll
├── PowerToys.MouseWithoutBordersService.deps.json
├── PowerToys.MouseWithoutBordersService.dll
├── PowerToys.MouseWithoutBordersService.exe
├── PowerToys.MouseWithoutBordersService.runtimeconfig.json
├── PowerToys.PdfPreviewHandler.deps.json
├── PowerToys.PdfPreviewHandler.dll
├── PowerToys.PdfPreviewHandler.exe
├── PowerToys.PdfPreviewHandler.runtimeconfig.json
├── PowerToys.PdfPreviewHandlerCpp.dll
├── PowerToys.PdfThumbnailProvider.deps.json
├── PowerToys.PdfThumbnailProvider.dll
├── PowerToys.PdfThumbnailProvider.exe
├── PowerToys.PdfThumbnailProvider.runtimeconfig.json
├── PowerToys.PdfThumbnailProviderCpp.dll
├── PowerToys.PowerAccent.deps.json
├── PowerToys.PowerAccent.dll
├── PowerToys.PowerAccent.exe
├── PowerToys.PowerAccent.runtimeconfig.json
├── PowerToys.PowerAccentKeyboardService.dll
├── PowerToys.PowerAccentModuleInterface.dll
├── PowerToys.PowerLauncher.deps.json
├── PowerToys.PowerLauncher.dll
├── PowerToys.PowerLauncher.exe
├── PowerToys.PowerLauncher.runtimeconfig.json
├── PowerToys.PowerLauncher.Telemetry.dll
├── PowerToys.PowerOCR.deps.json
├── PowerToys.PowerOCR.dll
├── PowerToys.PowerOCR.exe
├── PowerToys.PowerOCR.runtimeconfig.json
├── PowerToys.PowerOCRModuleInterface.dll
├── PowerToys.powerpreview.dll
├── PowerToys.PreviewHandlerCommon.deps.json
├── PowerToys.PreviewHandlerCommon.dll
├── PowerToys.QoiPreviewHandler.deps.json
├── PowerToys.QoiPreviewHandler.dll
├── PowerToys.QoiPreviewHandler.exe
├── PowerToys.QoiPreviewHandler.runtimeconfig.json
├── PowerToys.QoiPreviewHandlerCpp.dll
├── PowerToys.QoiThumbnailProvider.deps.json
├── PowerToys.QoiThumbnailProvider.dll
├── PowerToys.QoiThumbnailProvider.exe
├── PowerToys.QoiThumbnailProvider.runtimeconfig.json
├── PowerToys.QoiThumbnailProviderCpp.dll
├── PowerToys.Settings.UI.Lib.dll
├── PowerToys.ShortcutGuide.exe
├── PowerToys.ShortcutGuideModuleInterface.dll
├── PowerToys.StlThumbnailProvider.deps.json
├── PowerToys.StlThumbnailProvider.dll
├── PowerToys.StlThumbnailProvider.exe
├── PowerToys.StlThumbnailProvider.runtimeconfig.json
├── PowerToys.StlThumbnailProviderCpp.dll
├── PowerToys.SvgPreviewHandler.deps.json
├── PowerToys.SvgPreviewHandler.dll
├── PowerToys.SvgPreviewHandler.exe
├── PowerToys.SvgPreviewHandler.runtimeconfig.json
├── PowerToys.SvgPreviewHandlerCpp.dll
├── PowerToys.SvgThumbnailProvider.deps.json
├── PowerToys.SvgThumbnailProvider.dll
├── PowerToys.SvgThumbnailProvider.exe
├── PowerToys.SvgThumbnailProvider.runtimeconfig.json
├── PowerToys.SvgThumbnailProviderCpp.dll
├── PowerToys.Update.exe
├── PowerToys.WorkspacesCsharpLibrary.deps.json
├── PowerToys.WorkspacesCsharpLibrary.dll
├── PowerToys.WorkspacesEditor.deps.json
├── PowerToys.WorkspacesEditor.dll
├── PowerToys.WorkspacesEditor.exe
├── PowerToys.WorkspacesEditor.runtimeconfig.json
├── PowerToys.WorkspacesLauncher.exe
├── PowerToys.WorkspacesLauncherUI.deps.json
├── PowerToys.WorkspacesLauncherUI.dll
├── PowerToys.WorkspacesLauncherUI.exe
├── PowerToys.WorkspacesLauncherUI.runtimeconfig.json
├── PowerToys.WorkspacesModuleInterface.dll
├── PowerToys.WorkspacesSnapshotTool.exe
├── PowerToys.WorkspacesWindowArranger.exe
├── PowerToys.ZoomIt.exe
├── PowerToys.ZoomItModuleInterface.dll
├── PowerToys.ZoomItSettingsInterop.dll
├── PresentationCore.dll
├── PresentationFramework-SystemCore.dll
├── PresentationFramework-SystemData.dll
├── PresentationFramework-SystemDrawing.dll
├── PresentationFramework-SystemXml.dll
├── PresentationFramework-SystemXmlLinq.dll
├── PresentationFramework.Aero.dll
├── PresentationFramework.Aero2.dll
├── PresentationFramework.AeroLite.dll
├── PresentationFramework.Classic.dll
├── PresentationFramework.dll
├── PresentationFramework.Fluent.dll
├── PresentationFramework.Luna.dll
├── PresentationFramework.Royale.dll
├── PresentationNative_cor3.dll
├── PresentationUI.dll
├── ReachFramework.dll
├── ScipBe.Common.Office.OneNote.dll
├── sni.dll
├── SQLitePCLRaw.batteries_v2.dll
├── SQLitePCLRaw.core.dll
├── SQLitePCLRaw.provider.e_sqlite3.dll
├── StreamJsonRpc.dll
├── System.AppContext.dll
├── System.Buffers.dll
├── System.CodeDom.dll
├── System.Collections.Concurrent.dll
├── System.Collections.dll
├── System.Collections.Immutable.dll
├── System.Collections.NonGeneric.dll
├── System.Collections.Specialized.dll
├── System.CommandLine.dll
├── System.ComponentModel.Annotations.dll
├── System.ComponentModel.Composition.dll
├── System.ComponentModel.Composition.Registration.dll
├── System.ComponentModel.DataAnnotations.dll
├── System.ComponentModel.dll
├── System.ComponentModel.EventBasedAsync.dll
├── System.ComponentModel.Primitives.dll
├── System.ComponentModel.TypeConverter.dll
├── System.Configuration.ConfigurationManager.dll
├── System.Configuration.dll
├── System.Console.dll
├── System.Core.dll
├── System.Data.Common.dll
├── System.Data.DataSetExtensions.dll
├── System.Data.dll
├── System.Data.Odbc.dll
├── System.Data.OleDb.dll
├── System.Data.SqlClient.dll
├── System.Design.dll
├── System.Diagnostics.Contracts.dll
├── System.Diagnostics.Debug.dll
├── System.Diagnostics.DiagnosticSource.dll
├── System.Diagnostics.EventLog.dll
├── System.Diagnostics.EventLog.Messages.dll
├── System.Diagnostics.FileVersionInfo.dll
├── System.Diagnostics.PerformanceCounter.dll
├── System.Diagnostics.Process.dll
├── System.Diagnostics.StackTrace.dll
├── System.Diagnostics.TextWriterTraceListener.dll
├── System.Diagnostics.Tools.dll
├── System.Diagnostics.TraceSource.dll
├── System.Diagnostics.Tracing.dll
├── System.DirectoryServices.AccountManagement.dll
├── System.DirectoryServices.dll
├── System.DirectoryServices.Protocols.dll
├── System.dll
├── System.Drawing.Common.dll
├── System.Drawing.Design.dll
├── System.Drawing.dll
├── System.Drawing.Primitives.dll
├── System.Dynamic.Runtime.dll
├── System.Formats.Asn1.dll
├── System.Formats.Nrbf.dll
├── System.Formats.Tar.dll
├── System.Globalization.Calendars.dll
├── System.Globalization.dll
├── System.Globalization.Extensions.dll
├── System.IO.Abstractions.dll
├── System.IO.Compression.Brotli.dll
├── System.IO.Compression.dll
├── System.IO.Compression.FileSystem.dll
├── System.IO.Compression.Native.dll
├── System.IO.Compression.ZipFile.dll
├── System.IO.dll
├── System.IO.FileSystem.AccessControl.dll
├── System.IO.FileSystem.dll
├── System.IO.FileSystem.DriveInfo.dll
├── System.IO.FileSystem.Primitives.dll
├── System.IO.FileSystem.Watcher.dll
├── System.IO.IsolatedStorage.dll
├── System.IO.MemoryMappedFiles.dll
├── System.IO.Packaging.dll
├── System.IO.Pipelines.dll
├── System.IO.Pipes.AccessControl.dll
├── System.IO.Pipes.dll
├── System.IO.Ports.dll
├── System.IO.UnmanagedMemoryStream.dll
├── System.Linq.dll
├── System.Linq.Expressions.dll
├── System.Linq.Parallel.dll
├── System.Linq.Queryable.dll
├── System.Management.dll
├── System.Memory.dll
├── System.Net.dll
├── System.Net.Http.dll
├── System.Net.Http.Json.dll
├── System.Net.HttpListener.dll
├── System.Net.Mail.dll
├── System.Net.NameResolution.dll
├── System.Net.NetworkInformation.dll
├── System.Net.Ping.dll
├── System.Net.Primitives.dll
├── System.Net.Quic.dll
├── System.Net.Requests.dll
├── System.Net.Security.dll
├── System.Net.ServicePoint.dll
├── System.Net.Sockets.dll
├── System.Net.WebClient.dll
├── System.Net.WebHeaderCollection.dll
├── System.Net.WebProxy.dll
├── System.Net.WebSockets.Client.dll
├── System.Net.WebSockets.dll
├── System.Numerics.dll
├── System.Numerics.Vectors.dll
├── System.ObjectModel.dll
├── System.Printing.dll
├── System.Private.CoreLib.dll
├── System.Private.DataContractSerialization.dll
├── System.Private.ServiceModel.dll
├── System.Private.Uri.dll
├── System.Private.Windows.Core.dll
├── System.Private.Xml.dll
├── System.Private.Xml.Linq.dll
├── System.Reactive.dll
├── System.Reflection.Context.dll
├── System.Reflection.DispatchProxy.dll
├── System.Reflection.dll
├── System.Reflection.Emit.dll
├── System.Reflection.Emit.ILGeneration.dll
├── System.Reflection.Emit.Lightweight.dll
├── System.Reflection.Extensions.dll
├── System.Reflection.Metadata.dll
├── System.Reflection.Primitives.dll
├── System.Reflection.TypeExtensions.dll
├── System.Resources.Extensions.dll
├── System.Resources.Reader.dll
├── System.Resources.ResourceManager.dll
├── System.Resources.Writer.dll
├── System.Runtime.Caching.dll
├── System.Runtime.CompilerServices.Unsafe.dll
├── System.Runtime.CompilerServices.VisualC.dll
├── System.Runtime.dll
├── System.Runtime.Extensions.dll
├── System.Runtime.Handles.dll
├── System.Runtime.InteropServices.dll
├── System.Runtime.InteropServices.JavaScript.dll
├── System.Runtime.InteropServices.RuntimeInformation.dll
├── System.Runtime.Intrinsics.dll
├── System.Runtime.Loader.dll
├── System.Runtime.Numerics.dll
├── System.Runtime.Serialization.dll
├── System.Runtime.Serialization.Formatters.dll
├── System.Runtime.Serialization.Json.dll
├── System.Runtime.Serialization.Primitives.dll
├── System.Runtime.Serialization.Xml.dll
├── System.Security.AccessControl.dll
├── System.Security.Claims.dll
├── System.Security.Cryptography.Algorithms.dll
├── System.Security.Cryptography.Cng.dll
├── System.Security.Cryptography.Csp.dll
├── System.Security.Cryptography.dll
├── System.Security.Cryptography.Encoding.dll
├── System.Security.Cryptography.OpenSsl.dll
├── System.Security.Cryptography.Pkcs.dll
├── System.Security.Cryptography.Primitives.dll
├── System.Security.Cryptography.ProtectedData.dll
├── System.Security.Cryptography.X509Certificates.dll
├── System.Security.Cryptography.Xml.dll
├── System.Security.dll
├── System.Security.Permissions.dll
├── System.Security.Principal.dll
├── System.Security.Principal.Windows.dll
├── System.Security.SecureString.dll
├── System.ServiceModel.dll
├── System.ServiceModel.Duplex.dll
├── System.ServiceModel.Http.dll
├── System.ServiceModel.NetTcp.dll
├── System.ServiceModel.Primitives.dll
├── System.ServiceModel.Security.dll
├── System.ServiceModel.Syndication.dll
├── System.ServiceModel.Web.dll
├── System.ServiceProcess.dll
├── System.ServiceProcess.ServiceController.dll
├── System.Speech.dll
├── System.Text.Encoding.CodePages.dll
├── System.Text.Encoding.dll
├── System.Text.Encoding.Extensions.dll
├── System.Text.Encodings.Web.dll
├── System.Text.Json.dll
├── System.Text.RegularExpressions.dll
├── System.Threading.AccessControl.dll
├── System.Threading.Channels.dll
├── System.Threading.dll
├── System.Threading.Overlapped.dll
├── System.Threading.Tasks.Dataflow.dll
├── System.Threading.Tasks.dll
├── System.Threading.Tasks.Extensions.dll
├── System.Threading.Tasks.Parallel.dll
├── System.Threading.Thread.dll
├── System.Threading.ThreadPool.dll
├── System.Threading.Timer.dll
├── System.Transactions.dll
├── System.Transactions.Local.dll
├── System.ValueTuple.dll
├── System.Web.dll
├── System.Web.HttpUtility.dll
├── System.Web.Services.Description.dll
├── System.Windows.Controls.Ribbon.dll
├── System.Windows.dll
├── System.Windows.Extensions.dll
├── System.Windows.Forms.Design.dll
├── System.Windows.Forms.Design.Editors.dll
├── System.Windows.Forms.dll
├── System.Windows.Forms.Primitives.dll
├── System.Windows.Input.Manipulations.dll
├── System.Windows.Presentation.dll
├── System.Xaml.dll
├── System.Xml.dll
├── System.Xml.Linq.dll
├── System.Xml.ReaderWriter.dll
├── System.Xml.Serialization.dll
├── System.Xml.XDocument.dll
├── System.Xml.XmlDocument.dll
├── System.Xml.XmlSerializer.dll
├── System.Xml.XPath.dll
├── System.Xml.XPath.XDocument.dll
├── TestableIO.System.IO.Abstractions.dll
├── TestableIO.System.IO.Abstractions.Wrappers.dll
├── Testably.Abstractions.FileSystem.Interface.dll
├── TraceReloggerLib.dll
├── UIAutomationClient.dll
├── UIAutomationClientSideProviders.dll
├── UIAutomationProvider.dll
├── UIAutomationTypes.dll
├── UnicodeInformation.dll
├── UnitsNet.dll
├── UtfUnknown.dll
├── vcamp140.dll
├── vccorlib140.dll
├── vcomp140.dll
├── vcruntime140.dll
├── vcruntime140_1.dll
├── vcruntime140_cor3.dll
├── vcruntime140_threads.dll
├── WebView2Loader.dll
├── WindowsBase.dll
├── WindowsFormsIntegration.dll
├── WinRT.Runtime.dll
├── Wox.Infrastructure.dll
├── Wox.Plugin.dll
├── Wpf.Ui.dll
└── wpfgfx_cor3.dll
```

---

# Кодовая база

## LegacyLego.Application

```xml title="LegacyLego.Application.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\LegacyLego.Domain\LegacyLego.Domain.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Orders\Commands\Cancel\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Options" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Options.DataAnnotations" Version="10.0.9" />
    <PackageReference Include="Scrutor" Version="7.0.0" />
  </ItemGroup>

</Project>
```

---

```cs title="DependencyInjection.cs"
using LegacyLego.Application.Abstractions.ExceptionHandling;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Diagnostics;
using LegacyLego.Application.Options;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyLego.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var options = services.AddOptions<OrderHistoryOptions>()
            .BindConfiguration(OrderHistoryOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)

            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        )
            
            .AddSingleton<IExceptionMapper, DomainExceptionMapper>();

        return services;
    }
}
```

---

### Abstractions

#### Data

```cs title="ICustomLogSeverity.cs"
namespace LegacyLego.Application.Abstractions.Data;

public interface ICustomLogSeverity
{
    bool IsWarning => false;
}
```

---

```cs title="IUnitOfWork.cs"
namespace LegacyLego.Application.Abstractions.Data;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

---

#### ExceptionHandling

```cs title="AppFailureDescription.cs"
namespace LegacyLego.Application.Abstractions.ExceptionHandling;

public record AppFailureDescription(
    ExceptionFailureKind Kind,
    string Title,
    string Detail,
    string? ErrorCode = null);
```

---

```cs title="ExceptionFailureKind.cs"
namespace LegacyLego.Application.Abstractions.ExceptionHandling;

/// <summary>
/// Описывает конкретный тип ошибки-исключения
/// </summary>
/// <remarks>
/// Используется в случае с возникновением исключения
/// </remarks>
public enum ExceptionFailureKind : byte
{
    /// <summary>
    /// Ошибка на уровне бизнес-логики
    /// </summary>
    DomainLevelException = 0, 
    /// <summary>
    /// Ошибка на уровне интеграции с инфраструктурой
    /// </summary>
    InfrastructureLevelException,   
    /// <summary>
    /// ошибка интеграции с внешним api
    /// </summary>
    UnhandledNetworkLevelException,
    /// <summary>
    /// ошибка неизвестного типа
    /// </summary>
    Unknown
}
```

---

```cs title="IExceptionMapper.cs"
namespace LegacyLego.Application.Abstractions.ExceptionHandling;

public interface IExceptionMapper
{
    public bool TryMap(Exception exception, out AppFailureDescription? description);
}
```

---

#### ExternalServices

```cs title="ICommandBackgroundJobService.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface ICommandBackgroundJobService
{
    public void Schedule<TResult>(ICommand<TResult> command, TimeSpan delay);

    public void Schedule(ICommand command, TimeSpan delay);
}
```

---

```cs title="ICursorSerializer.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface ICursorSerializer
{
    public string Serialize<T>(T cursorData) where T : struct;

    public Result<T> Deserialize<T>(string cursor) where T : struct;
}
```

---

```cs title="IPaymentProvider.cs"
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IPaymentProvider
{
    public Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        Guid orderId,
        decimal amount,
        string currency,
        int scale,
        CancellationToken ct);

    Task<Result> RequestRefundAsync(
        Guid orderId,
        decimal amount,
        string currency,
        string transactionId,
        CancellationToken ct);
}
```

---

#### Messaging

```cs title="ICommandDispatcher.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface ICommandDispatcher
{
    public Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken ct = default);

    public Task<Result> DispatchAsync(ICommand command, CancellationToken ct = default);
}
```

---

```cs title="IDomainEventDispatcher.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    public Task DispatchAsync(
        IDomainEvent domainEvents,
        CancellationToken ct = default);
}
```

---

```cs title="IEventPublisher.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken ct = default)
        where TEvent : IDomainEvent;
}
```

---

```cs title="IQueryDispatcher.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IQueryDispatcher
{
    public Task<Result<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default);
}
```

---

##### Command

```cs title="IBaseCommand.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface IBaseCommand;
```

---

```cs title="ICommand.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface ICommand : IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;
```

---

```cs title="ICommandHandler.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    public Task<Result> HandleAsync(TCommand command,CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
```

---

##### Event

###### Domain

```cs title="IDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Event.Domain;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public Task HandleAsync(TDomainEvent notification, CancellationToken cancellationToken);
}
```

---

###### Integration

```cs title="IIntegrationEvent.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Event.Integration;

public interface IIntegrationEvent
{
    public DateTime OccurredOnUtc { get; }
}
```

---

```cs title="IIntegrationEventPublisher.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Event.Integration;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken ct);
}
```

---

##### Query

```cs title="IQuery.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Query;

public interface IQuery<TResponse>;
```

---

```cs title="IQueryHandler.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Query;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TQuery query,CancellationToken cancellationToken);
}
```

---

### Diagnostics

```cs title="DomainExceptionMapper.cs"
using LegacyLego.Application.Abstractions.ExceptionHandling;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Diagnostics;

public sealed class DomainExceptionMapper : IExceptionMapper
{
    public bool TryMap(Exception exception, out AppFailureDescription? description)
    {
        if (exception is DomainException domainException)
        {
            description = new AppFailureDescription(
                Kind: ExceptionFailureKind.DomainLevelException,
                Title: "Критическое нарушение бизнес-состояния системы",
                Detail: domainException.Error.Message,
                ErrorCode: domainException.Error.Code
            );
            return true;
        }

        description = null;
        return false;
    }
}
```

---

### Errors

```cs title="PaymentProviderErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class PaymentProviderErrors
{
    public const string SessionNotFoundCode = "PaymentProvider.SessionNotFoundByPaymentId";

    public static Error GetSessionNotFoundByPaymentIdError(Guid paymentId)
    {
        return new(
            Code: SessionNotFoundCode,
            Message: $"По следующему OrderPayment Id: {paymentId} не было найдено ни одной активной сессии оплаты");
    }

}
```

---

### ExceptionalErrors

```cs title="UnitOfWorkExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.ExceptionalErrors;

public static class UnitOfWorkExceptionalErrors
{
    public const string DatabaseSaveErrorCode = "UnitOfWork.SaveError";

    public static ExceptionalError GetDatabaseSaveError(Guid orderId, string internalMessage)
    {
        return new(
            Code: DatabaseSaveErrorCode,
            Message: $"Критическая ошибка при сохранении заказа {orderId}. Внутренняя ошибка: {internalMessage}"
        );
    }
}
```

---

### Exceptions

```cs title="InfrastructureException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Exceptions;

public abstract class InfrastructureException : Exception
{
    public ExceptionalError Error { get; }

    protected InfrastructureException(ExceptionalError error)
        : base(error.Code + ": " + error.Message)
    {
        Error = error;
    }
}
```

---

```cs title="PersistenceException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Exceptions;

public class PersistenceException : InfrastructureException
{
    public PersistenceException(ExceptionalError error) : base(error) { }
}
```

---

```cs title="UniqueConstraintViolation.cs"
using LegacyLego.Domain.Shared;
namespace LegacyLego.Application.Exceptions;

public class UniqueConstraintViolation : InfrastructureException
{
    public UniqueConstraintViolation(ExceptionalError error) : base(error) { }
}
```

---

### Options

```cs title="OrderHistoryOptions.cs"
using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Application.Options;

public class OrderHistoryOptions
{
    public const string SectionName = "OrderHistory";

    [Range(1, 15, ErrorMessage = "Значение OrderHistory PageSize должно быть в диапазоне от 1 до 15.")]
    public int PageSize { get; set; } = 5;

}
```

---

### Orders

#### Commands

##### Cancel

```cs title="CancelletionOrderDetails.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record CancelletionOrderDetails : ICustomLogSeverity
{
    public const string AlreadyCancelledDetailsCode = "Order.Cancelletion.AlreadyCancelled";
    public const string CancelledSuccessfullyCode = "Order.Cancelletion.CancelledSuccessfully";

    public string Code { get; }
    public Guid OrderId { get; }
    public string Message { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyCancelledDetailsCode => true,
        CancelledSuccessfullyCode => false,
        _ => false
    };

    private CancelletionOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static CancelletionOrderDetails GetAlreadyCancelledDetails(Guid orderId)
    {
        return new CancelletionOrderDetails(
            Code: AlreadyCancelledDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already cancelled",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            false);
    }

    internal static CancelletionOrderDetails GetCancelledSuccessfullyDetails(Guid orderId)
    {
        return new CancelletionOrderDetails(
            Code: CancelledSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully cancelled",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }
}
```

---

```cs title="CancelOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand<CancelletionOrderDetails>;
```

---

```cs title="CancelOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed class CancelOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand, CancelletionOrderDetails>
{
    public async Task<Result<CancelletionOrderDetails>> HandleAsync(CancelOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<CancelletionOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Cancel();
        if (result.IsFailure && order.Status is OrderStatus.Cancelled)
            return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetAlreadyCancelledDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<CancelletionOrderDetails>.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(ct);
        return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetCancelledSuccessfullyDetails(orderIdGuid));
    }
}
```

---

##### Create

```cs title="CreateOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    Guid ClientId,
    string CurrencyCode,
    OrderAddressDto OrderAddress,
    List<OrderItemDto> Items) : ICommand<Guid>;
```

---

```cs title="CreateOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateOrderCommand command, CancellationToken ct)
    {
        var currencyResult = Currency.FromCode(command.CurrencyCode);
        if (currencyResult.IsFailure) return Result<Guid>.Failure(currencyResult.Error);
        var currency = currencyResult.Value;

        var addressResult = OrderAddress.Create(
            command.OrderAddress.Country,
            command.OrderAddress.City,
            command.OrderAddress.Street,
            command.OrderAddress.PostalCode);
        if (addressResult.IsFailure) return Result<Guid>.Failure(addressResult.Error);
        var address = addressResult.Value;

        var itemsResult = CreateItems(command.Items, currency);
        if (itemsResult.IsFailure) return Result<Guid>.Failure(itemsResult.Error);
        var items = itemsResult.Value;
       
        var orderResult = Order.Create(address, command.ClientId, items);
        if (orderResult.IsFailure) return Result<Guid>.Failure(orderResult.Error);
        var order = orderResult.Value;

        orderRepository.Add(order);
        await unitOfWork.SaveChangesAsync(ct);

        return Result<Guid>.Success(order.Id.Value);
    }

    private static Result<List<OrderItem>> CreateItems(IEnumerable<OrderItemDto> requests, Currency currency)
    {
        var items = new List<OrderItem>();
        foreach (var request in requests)
        {
            var priceResult = Price.Create(request.UnitPriceAmount, currency);
            if (priceResult.IsFailure) return Result<List<OrderItem>>.Failure(priceResult.Error);

            var itemResult = OrderItem.Create(request.Title, request.Quantity, request.ProductId, priceResult.Value);
            if (itemResult.IsFailure) return Result<List<OrderItem>>.Failure(itemResult.Error);

            items.Add(itemResult.Value);
        }
        return Result<List<OrderItem>>.Success(items);
    }
}
```

---

```cs title="CreateOrderDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Commands.Create;

public class CreateOrderDomainEventHandler(ICommandBackgroundJobService jobService)
: IDomainEventHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated notification, CancellationToken ct)
    {
        jobService.Schedule(new ExpireOrderCommand(notification.OrderId.Value), TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }
}
```

---

##### Expire

```cs title="ExpirationOrderDetails.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpirationOrderDetails : ICustomLogSeverity
{
    public const string AlreadyExpiredDetailsCode = "Order.Expiretion.AlreadyExpired";
    public const string ExpiredSuccessfullyCode = "Order.Expiretion.ExpiredSuccessfully";

    public string Code { get; }
    public Guid OrderId { get; }
    public string Message { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyExpiredDetailsCode => true,
        ExpiredSuccessfullyCode => false,
        _ => false
    };

    private ExpirationOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static ExpirationOrderDetails GetAlreadyExpiredDetails(Guid orderId)
    {
        return new ExpirationOrderDetails(
            Code: AlreadyExpiredDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already expired",
            CurrentStatus: OrderStatus.Expired.ToString(),
            false);
    }

    internal static ExpirationOrderDetails GetExpiredSuccessfullyDetails(Guid orderId)
    {
        return new ExpirationOrderDetails(
            Code: ExpiredSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully expired",
            CurrentStatus: OrderStatus.Expired.ToString(),
            true);
    }
}
```

---

```cs title="ExpireOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpireOrderCommand(Guid OrderId) : ICommand<ExpirationOrderDetails>;
```

---

```cs title="ExpireOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed class ExpireOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<ExpireOrderCommand, ExpirationOrderDetails>
{
    public async Task<Result<ExpirationOrderDetails>> HandleAsync(ExpireOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);
        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<ExpirationOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Expire();
        if (result.IsFailure && order.Status is OrderStatus.Expired)
            return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetAlreadyExpiredDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<ExpirationOrderDetails>.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(ct);
        return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetExpiredSuccessfullyDetails(orderIdGuid));
    }
}
```

---

##### Pay

```cs title="PayOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Pay;

public sealed record PayOrderCommand(Guid OrderId) : ICommand<PayOrderDetails>;
```

---

```cs title="PayOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.ExceptionalErrors;
using LegacyLego.Application.Exceptions;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Application.Orders.Commands.Refund;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Pay;

public sealed class PayOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<PayOrderCommand, PayOrderDetails>
{
    public async Task<Result<PayOrderDetails>> HandleAsync(PayOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<PayOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Pay();
        if (result.IsFailure && order.Status is OrderStatus.Paid)
            return Result<PayOrderDetails>.Success(PayOrderDetails.GetAlreadyPaidDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<PayOrderDetails>.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(ct);
        return Result<PayOrderDetails>.Success(PayOrderDetails.GetPaidSuccessfullyDetails(orderIdGuid));
    }

}
```

---

```cs title="PayOrderDetails.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record PayOrderDetails : ICustomLogSeverity
{
    public const string AlreadyPaidDetailsCode = "Order.Payment.AlreadyPaid";
    public const string PaidSuccessfullyCode = "Order.Payment.PaidSuccessfully";

    public string Code { get; }
    public  Guid OrderId { get; }
    public string Message { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyPaidDetailsCode => true,
        PaidSuccessfullyCode => false,
        _ => false
    };

    private PayOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static PayOrderDetails GetAlreadyPaidDetails(Guid orderId)
    {
        return new PayOrderDetails(
            Code: AlreadyPaidDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already paid",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            false);
    }

    internal static PayOrderDetails GetPaidSuccessfullyDetails(Guid orderId)
    {
        return new PayOrderDetails(
            Code: PaidSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully paid",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }
}
```

---

##### Refund

```cs title="RefundOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderCommand(Guid OrderId) : ICommand<RefundOrderDetails>;
```

---

```cs title="RefundOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Refund;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed class RefundOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<RefundOrderCommand, RefundOrderDetails>
{
    public async Task<Result<RefundOrderDetails>> HandleAsync(RefundOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<RefundOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Refund();
        if (result.IsFailure && order.Status is OrderStatus.Refunded)
            return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetAlreadyRefundedDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<RefundOrderDetails>.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(ct);
        return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetRefundedSuccessfullyDetails(orderIdGuid));
    }
}
```

---

```cs title="RefundOrderDetails.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderDetails : ICustomLogSeverity
{
    public const string AlreadyRefundedDetailsCode = "Order.Refund.AlreadyRefunded";
    public const string RefundedSuccessfullyCode = "Order.Refund.RefundedSuccessfully";

    public string Code { get; }
    public Guid OrderId { get; }
    public string Message { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyRefundedDetailsCode => true,
        RefundedSuccessfullyCode => false,
        _ => false
    };

    private RefundOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static RefundOrderDetails GetAlreadyRefundedDetails(Guid orderId)
    {
        return new RefundOrderDetails(
            Code: AlreadyRefundedDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already refunded",
            CurrentStatus: OrderStatus.Refunded.ToString(),
            false);
    }

    internal static RefundOrderDetails GetRefundedSuccessfullyDetails(Guid orderId)
    {
        return new RefundOrderDetails(
            Code: RefundedSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully refunded",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }
}
```

---

#### Common

```cs title="OrderAddressDto.cs"
namespace LegacyLego.Application.Orders.Common;

public sealed record OrderAddressDto(
    string Country,
    string City,
    string Street,
    string PostalCode);
```

---

```cs title="OrderItemDto.cs"
namespace LegacyLego.Application.Orders.Common;

public sealed record OrderItemDto(
    string Title,
    int Quantity,
    Guid ProductId,
    decimal UnitPriceAmount);
```

---

```cs title="OrderSummaryDto.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Common;

public sealed record OrderSummaryDto(
    Guid OrderId,
    OrderStatus Status,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAt,
    int ItemsCount
);
```

---

##### Projections

```cs title="OrderProjections.cs"
using LegacyLego.Application.Orders.Queries.OrderDetails;
using LegacyLego.Domain.Aggregates;
using System.Linq.Expressions;

namespace LegacyLego.Application.Orders.Common.Projections;

public static class OrderProjections
{
    public static Expression<Func<Order, OrderSummaryDto>> Summary =>
        order => new OrderSummaryDto(
            order.Id.Value,
            order.Status,
            order.Items.Sum(x => x.UnitPrice.Sum * x.Quantity),
            order.Currency.Code,
            order.CreationDateUtc,
            order.Items.Count
        );

    public static Expression<Func<Order, OrderDetailsDto>> Details =>
            order => new OrderDetailsDto(
                order.Id.Value,
                order.Status,
                order.CreationDateUtc,
                new AddressDetailsDto(
                    order.Address.Country,
                    order.Address.City,
                    order.Address.Street,
                    order.Address.PostalCode),
                order.Items.Select(item => new OrderItemDetailsDto(
                    item.ProductId,
                    item.Title,
                    item.Quantity,
                    item.UnitPrice.Sum,
                    item.UnitPrice.Sum * item.Quantity)),
                order.TotalPrice.Sum,
                order.Currency.Code
            );
}
```

---

#### Errors

```cs title="OrderApplicationErrors.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Errors;

public static class OrderApplicationErrors
{
    public const string UnauthorizedAccessToOrderByClientIdCode = "Order.UnauthorizedAccessToOrderByClientId";

    public static Error GetUnauthorizedAccessToOrderByClientIdError(OrderId orderId, Guid clientId)
    {
        return new(
            Code: UnauthorizedAccessToOrderByClientIdCode,
            Message: $"Запрещено обращение к заказу: {orderId.Value} по следующему идентификатору клиента: {clientId}"
        );
    }
}
```

---

#### Queries

##### ActiveOrders

```cs title="ActiveOrderSpecification.cs"
using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class ActiveOrderSpecification : Specification<Order, OrderId, OrderSummaryDto>
{
    public ActiveOrderSpecification(Guid clientId) : base(OrderProjections.Summary)
    {
        AddFilter(order => order.ClientId == clientId);
        AddFilter(order => order.Status == OrderStatus.PendingPayment);
    }
}
```

---

```cs title="GetActiveOrdersQuery.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetActiveOrdersQuery(Guid UserId) : IQuery<IReadOnlyList<OrderSummaryDto>>;
```

---

```cs title="GetActiveOrdersQueryHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetActiveOrdersQueryHandler(IOrderRepository repository) : IQueryHandler<GetActiveOrdersQuery, IReadOnlyList<OrderSummaryDto>>
{
    public async Task<Result<IReadOnlyList<OrderSummaryDto>>> HandleAsync(GetActiveOrdersQuery query, CancellationToken ct)
    {
        var specification = new ActiveOrderSpecification(query.UserId);

        var result = await repository.GetOrdersAsync(specification, ct);

        return Result<IReadOnlyList<OrderSummaryDto>>.Success(result);
    }
}
```

---

##### OrderDetails

```cs title="ActiveOrderSpecification.cs"
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public class OrderDetailsSpecification : Specification<Order, OrderId, OrderDetailsDto>
{
    public OrderDetailsSpecification(Guid clientId, OrderId orderId) : base(OrderProjections.Details)
    {
        AddFilter(order => order.ClientId == clientId);
        AddFilter(order => order.Id == orderId);
    }
}
```

---

```cs title="GetOrderDetailsQuery.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrderDetails;


namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetOrderDetailsQuery(Guid UserId, Guid OrderId) : IQuery<OrderDetailsDto>;
```

---

```cs title="GetOrderDetailsQueryHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public class GetOrderDetailsQueryHandler(IOrderRepository repository) : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<Result<OrderDetailsDto>> HandleAsync(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var orderId = OrderId.From(query.OrderId);

        var specification = new OrderDetailsSpecification(query.UserId, orderId);

        var orderDetails = await repository.GetOrderAsync(specification, ct);

        if (orderDetails is null)
            return Result<OrderDetailsDto>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        return Result<OrderDetailsDto>.Success(orderDetails);
    }
}
```

---

```cs title="OrderDetailsDto.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public sealed record OrderDetailsDto(
    Guid OrderId,
    OrderStatus Status,
    DateTime CreatedAt,
    AddressDetailsDto DeliveryAddress,
    IEnumerable<OrderItemDetailsDto> Items,
    decimal TotalAmount,
    string Currency
);

public sealed record OrderItemDetailsDto(
    Guid ProductId,
    string Title,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);

public sealed record AddressDetailsDto(
    string Country,
    string City,
    string Street,
    string PostalCode
);
```

---

##### OrdersHistory

```cs title="GetOrdersHistoryQuery.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrdersHistory;


namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetOrdersHistoryQuery(Guid UserId, OrderHistoryRequest Filter) : IQuery<OrdersHistoryResponse>;
```

---

```cs title="GetOrdersHistoryQueryHandler.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Options;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetOrdersHistoryQueryHandler(
    IOrderRepository repository,
    ICursorSerializer cursorSerializer,
    IOptions<OrderHistoryOptions> options) : IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>
{
    public async Task<Result<OrdersHistoryResponse>> HandleAsync(GetOrdersHistoryQuery query, CancellationToken ct)
    {
        var pageSize = options.Value.PageSize;
        var takeLimit = pageSize + 1;

        DateTime? cursorDate = null;
        Guid? cursorId = null;
        OrderId? cursorOrderId = null;

        if (!string.IsNullOrWhiteSpace(query.Filter.Cursor))
        {
            var parseResult = cursorSerializer.Deserialize<(DateTime Date, Guid Id)>(query.Filter.Cursor);

            if (parseResult.IsFailure) 
                return Result<OrdersHistoryResponse>.Failure(parseResult.Error);

            (cursorDate, cursorId) = parseResult.Value;

            cursorOrderId = OrderId.From(cursorId.Value);
        }

        var specification = new OrderHistorySpecification(
             clientId: query.UserId,
             cursorDate: cursorDate,
             cursorOrderId: cursorOrderId,
             limit: takeLimit);

        var orders = await repository.GetOrdersAsync(specification, ct);

        string? nextCursor = null;

        if (orders.Count == takeLimit)
        {
            var lastPagedOrder = orders[pageSize - 1];
            nextCursor = cursorSerializer.Serialize((lastPagedOrder.CreatedAt, lastPagedOrder.OrderId));
        }

        var resultOrders = orders.Count > pageSize
        ? orders.Take(pageSize).ToList()
        : orders;

        var result = new OrdersHistoryResponse(resultOrders, nextCursor);

        return Result<OrdersHistoryResponse>.Success(result);
    }
}
```

---

```cs title="OrderHistoryRequest.cs"
namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public record OrderHistoryRequest(
    string? Cursor = null   // Base64 токен
);
```

---

```cs title="OrderHistorySpecification.cs"
using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public class OrderHistorySpecification : Specification<Order, OrderId, OrderSummaryDto>
{
    public OrderHistorySpecification(Guid clientId, DateTime? cursorDate, OrderId? cursorOrderId, int limit)
        : base(OrderProjections.Summary)
    {
        AddFilter(o => o.ClientId == clientId);

        var historyStatuses = new[] { OrderStatus.Paid, OrderStatus.Cancelled, OrderStatus.Refunded };
        AddFilter(o => historyStatuses.Contains(o.Status));

        // Keyset Pagination
        if (cursorDate.HasValue && cursorOrderId is not null)
        {
            // дата меньше курсорной, ИЛИ (дата равна курсорной, но ID меньше курсорного)
            AddFilter(o => o.CreationDateUtc < cursorDate.Value ||
                          (o.CreationDateUtc == cursorDate.Value && o.Id < cursorOrderId));
        }

        AddOrderByDescending(o => o.CreationDateUtc);
        AddOrderByDescending(o => o.Id);

        SetLimitNum(limit);
    }
}
```

---

```cs title="OrdersHistoryResponse.cs"
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public record OrdersHistoryResponse(
    IReadOnlyCollection<OrderSummaryDto> Orders,
    string? NextCursor);
```

---

### Payments

#### Commands

##### PocessPaymentWebhook

```cs title="OrderPaymentSucceededDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public class OrderPaymentSucceededDomainEventHandler(ICommandDispatcher dispatcher)
: IDomainEventHandler<OrderPaymentSucceeded>
{
    public async Task HandleAsync(OrderPaymentSucceeded notification, CancellationToken ct)
    {
        var command = new PayOrderCommand(notification.OrderId.Value);

        await dispatcher.DispatchAsync(command, ct);
    }
}
```

---

```cs title="ProcessPaymentDetails.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record ProcessPaymentDetails : ICustomLogSeverity
{
    public const string AlreadyProcessedWithTransactionIdCode = "OrderPayment.AlreadyProcessedWithTransactionId";
    public const string AlreadyProcessedCode = "OrderPayment.AlreadyProcessed";

    public const string SetRefundRequestedCode = "OrderPayment.RefundRequested";
    public const string SetSuccessedCode = "OrderPayment.SuccessfullySuccessed";
    public const string SetFailedCode = "OrderPayment.SuccessfullyFailed";
    public const string SetRefundedCode = "OrderPayment.SuccessfullyRefunded";

    public string Code { get; }
    public string Message { get; }
    public Guid OrderId { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyProcessedWithTransactionIdCode => true,
        AlreadyProcessedCode => true,
        SetSuccessedCode => false,
        SetFailedCode => true,
        SetRefundedCode => false,
        SetRefundRequestedCode => true,
        _ => false
    };

    private ProcessPaymentDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static ProcessPaymentDetails GetAlreadyProcessedWithTransactionIdDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: AlreadyProcessedWithTransactionIdCode,
            OrderId: orderId,
            Message: $"Payment with transactionId: {transactionId} is already processed",
            CurrentStatus: PaymentStatus.Succeeded.ToString(),
            StateChanged: false);
    }

    internal static ProcessPaymentDetails GetAlreadyProcessedDetails(string transactionId, PaymentStatus status, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: AlreadyProcessedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId: {transactionId} is already processed with {status.ToString()} state earlier",
            CurrentStatus: status.ToString(),
            StateChanged: false);
    }

    internal static ProcessPaymentDetails GetSetRefundRequestedDetails(
        string transactionId,
        Guid orderId, 
        decimal amount,
        string currencyCode)
    {
        return new ProcessPaymentDetails(
            Code: SetRefundRequestedCode,
            OrderId: orderId,
            Message: $"For Payment with transactionId:{transactionId} was RefundRequested with {amount} {currencyCode}",
            CurrentStatus: PaymentStatus.RefundRequested.ToString(),
            StateChanged: true);
    }

    internal static ProcessPaymentDetails GetSetSuccessedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetSuccessedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Successed",
            CurrentStatus: PaymentStatus.Succeeded.ToString(),
            StateChanged: true);
    }

    internal static ProcessPaymentDetails GetSetFailedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetFailedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Failed",
            CurrentStatus: PaymentStatus.Failed.ToString(),
            StateChanged: true);
    }

    internal static ProcessPaymentDetails GetSetRefundedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetRefundedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Refunded",
            CurrentStatus: PaymentStatus.Refunded.ToString(),
            StateChanged: true);
    }
}
```

---

```cs title="ProcessPaymentErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class ProcessPaymentErrors
{
    public const string InvalidAmountCode = "Payment.InvalidAmount";
    public const string TotalPricesMismatchCode = "ProcessPayment.TotalPricesMismatch";
    public const string UnknownStatusCode = "ProcessPayment.UnknownStatus";
    public const string TransactionConflictCode = "ProcessPayment.TransactionConflict";
    public const string PaymentNotFoundForWebhookCode = "ProcessPayment.PaymentNotFoundForWebhook";

    public const string EmptyTransactionIdWithSucceededWebhookCode = "ProcessPayment.EmptyTransactionIdWithSucceededWebhook";
    public const string UnexpectedPaymentStatusAfterRegistrationCode = "ProcessPayment.UnexpectedPaymentStatusAfterRegistration";

    public const string CanNotCreateCurrencyFromCodeWebhookAnomalyCode = "ProcessPayment.CanNotCreateCurrencyFromCodeWebhookAnomaly";
    public const string CanNotCreatePriceFromAmountWebhookAnomalyCode = "ProcessPayment.CanNotCreatePriceFromAmountWebhookAnomaly";

    public static Error GetInvalidAmountCodeError(decimal amount)
    {
        return new(
            Code: InvalidAmountCode,
            Message: $"Amount must be greater than zero, but it was {amount}");
    }

    public static Error GetTotalPricesMismatchError(decimal webhookAmount, decimal orderTotal)
    {
        return new(
            Code: TotalPricesMismatchCode,
            Message: $"Webhook amount:{webhookAmount} must be equivalent to order's total price: {orderTotal}");
    }

    public static Error GetEmptyTransactionIdWithSucceededWebhookError()
    {
        return new(
            Code: EmptyTransactionIdWithSucceededWebhookCode,
            Message: $"TransactionId can not be null or empty for secceeded status webhook");
    }

    public static Error GetUnknownStatusCodeError(PaymentStatus unknownStatus)
    {
        return new(
            Code: UnknownStatusCode,
            Message: $"Unknown {unknownStatus} status code from Webhook");
    }

    public static Error GetUnexpectedPaymentStatusAfterRegistrationError(PaymentStatus unexpectedStatus)
    {
        return new(
            Code: UnexpectedPaymentStatusAfterRegistrationCode,
            Message: $"Unexpected OrderPayment status after succeeded peyment registration. Is was {unexpectedStatus}");
    }

    public static Error GetTransactionConflictError(string TransactionId, string webhookTransactionId)
    {
        return new(
            Code: TransactionConflictCode,
            Message: $"For successed payment system get more then one different transactions by {TransactionId} and {webhookTransactionId} transactionId's");
    }

    public static Error GetPaymentNotFoundForWebhookError(string webhookExternalSessionId, string? webhookTransactionId)
    {
        return new(
            Code: TransactionConflictCode,
            Message: $"Payment was not fount for this webhook with " +
            $"ExternalSessionId: {webhookExternalSessionId} and" +
            $"TransactionId: {webhookTransactionId ?? "null"}");
    }

    public static Error GetCanNotCreateCurrencyFromCodeWebhookAnomalyError(string currencyCode)
    {
        return new(
            Code: CanNotCreateCurrencyFromCodeWebhookAnomalyCode,
            Message: $"Can not create Currency from {currencyCode} code from webhook");
    }

    public static Error GetCanNotCreatePriceFromAmountWebhookAnomalyError(decimal amount)
    {
        return new(
            Code: CanNotCreatePriceFromAmountWebhookAnomalyCode,
            Message: $"Can not create Price from {amount} amount from webhook");
    }
}
```

---

```cs title="ProcessPaymentWebhookCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Common;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record ProcessPaymentWebhookCommand(PaymentWebhook Webhook) : ICommand<ProcessPaymentDetails>;
```

---

```cs title="ProcessPaymentWebhookCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed class ProcessPaymentWebhookCommandHandler(
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ProcessPaymentWebhookCommand, ProcessPaymentDetails>
{
    public async Task<Result<ProcessPaymentDetails>> HandleAsync(ProcessPaymentWebhookCommand command, CancellationToken ct)
    {
        var webhook = command.Webhook;

        var orderId = OrderId.From(webhook.OrderId);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null)
            return Result<ProcessPaymentDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var payment = webhook.TransactionId == null ?
            null : await paymentRepository.GetByTransactionIdAsync(webhook.TransactionId!, ct);

        payment ??= await paymentRepository.GetByExternalSessionIdAsync(webhook.ExternalSessionId, ct);

        if (payment is null)
        {
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetPaymentNotFoundForWebhookError(webhook.ExternalSessionId, webhook.ExternalSessionId));
        }

        var result = webhook.Status switch
        {
            PaymentStatus.Refunded => HandleRefunded(payment, webhook.TransactionId),

            PaymentStatus.Failed => HandleFailed(payment),

            PaymentStatus.Succeeded => HandleSucceeded(payment, webhook, order),

            _ => Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetUnknownStatusCodeError(webhook.Status))
        };

        await unitOfWork.SaveChangesAsync(ct);
        return result;
    }
    
    private static Result<ProcessPaymentDetails> HandleFailed(
        OrderPayment payment)
    {
        if (payment.Status is PaymentStatus.Failed)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var paymentResult = payment.MarkAsFailed();
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetFailedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result<ProcessPaymentDetails> HandleRefunded(
        OrderPayment payment, string transactionId)
    {
        if (payment.Status is PaymentStatus.Refunded)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var paymentResult = payment.MarkAsRefunded(transactionId);
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetRefundedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result<ProcessPaymentDetails> HandleSucceeded(
        OrderPayment payment, 
        PaymentWebhook webhook, 
        Order order)
    {
        if (string.IsNullOrWhiteSpace(webhook.TransactionId))
            Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetEmptyTransactionIdWithSucceededWebhookError());

        if (payment.Status is PaymentStatus.Succeeded && payment.TransactionId != webhook.TransactionId)
        {
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetTransactionConflictError(payment.TransactionId!, webhook.TransactionId!));
        }
        else if (payment.Status is PaymentStatus.Succeeded)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        if (webhook.Amount <= 0)
            return Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetInvalidAmountCodeError(webhook.Amount));

        var currency = Currency.FromCode(webhook.Currency);
        if (currency.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetCanNotCreateCurrencyFromCodeWebhookAnomalyError(webhook.Currency));

        var webhookAmountPrice = Price.Create(webhook.Amount, currency.Value);
        if (webhookAmountPrice.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetCanNotCreatePriceFromAmountWebhookAnomalyError(webhook.Amount));

        var paymentRegistration = payment.RegisterPaymentReceipt(webhook.TransactionId!, webhookAmountPrice.Value);
        if (paymentRegistration.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentRegistration.Error);

        var result = payment.Status switch
        {
            PaymentStatus.Succeeded => Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetSetSuccessedDetails(payment.TransactionId!, payment.OrderId.Value)),

            PaymentStatus.RefundRequested => Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetRefundRequestedDetails(
                payment.TransactionId!,
                payment.OrderId.Value,
                payment.ActualAmount!.Sum,
                payment.ActualAmount!.Currency.Code)),

            _ => Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetUnexpectedPaymentStatusAfterRegistrationError(payment.Status))
        };

        return result;
    }

}
```

---

##### RefundRequested

```cs title="RefundRequestedOrderPaymentDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Orders.Commands.Create;

public class RefundRequestedOrderPaymentDomainEventHandler(
    TimeProvider timeProvider,
    IIntegrationEventPublisher eventPublisher)
: IDomainEventHandler<OrderPaymentAmountMismatchedAndRefundRequested>
{
    public async Task HandleAsync(OrderPaymentAmountMismatchedAndRefundRequested notification, CancellationToken ct)
    {
        var @event = new OrderPaymentRefundRequestedIntegrationEvent(
            PaymentId: notification.Paymentid,
            OrderId: notification.OrderId.Value,
            Amount: notification.ActualAmount.Sum,
            Currency: notification.ActualAmount.Currency.Code,
            TransactionId: notification.TransactionId,
            OccurredOnUtc: timeProvider.GetUtcNow().UtcDateTime);

        await eventPublisher.PublishAsync(@event, ct);
    }
}
```

---

##### StartPayment

```cs title="StartOrderPaymentCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

namespace LegacyLego.Application.Payments.Commands.StartPayment;

public sealed record StartOrderPaymentCommand(Guid OrderId, Guid ClientId) : ICommand<StartOrderPaymentDetails>;
```

---

```cs title="StartOrderPaymentCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Exceptions;
using LegacyLego.Application.Orders.Errors;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Commands.StartPayment;

public sealed class StartOrderPaymentCommandHandler(
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    IPaymentProvider paymentProvider,
    TimeProvider timeProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<StartOrderPaymentCommand, StartOrderPaymentDetails>
{
    private enum ConstraintCheckTimeline : byte
    {
        AfterConstraintCheck,
        BeforeConstraintCheck
    }

    public async Task<Result<StartOrderPaymentDetails>> HandleAsync(StartOrderPaymentCommand command, CancellationToken ct)
    {
        var orderId = OrderId.From(command.OrderId);
        var clientId = command.ClientId;

        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<StartOrderPaymentDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        if (order.ClientId != command.ClientId)
            return Result<StartOrderPaymentDetails>.Failure(OrderApplicationErrors.GetUnauthorizedAccessToOrderByClientIdError(orderId, clientId));

        if (order.Status != OrderStatus.PendingPayment)
            return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetOrderIsNotInPendingPaymentError(command.OrderId, order.Status));

        if (await paymentRepository.ExistsSucceededAsync(orderId, ct))
            return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetForOrderIsAlreadyExistsSuccessedPaymentError(command.OrderId));

        var existingBeforeCheckUniqConstraint = await paymentRepository.GetPendingByOrderIdAsync(orderId, ct);

        if (existingBeforeCheckUniqConstraint is not null)
        {
            return await EnsureSession(
                existingBeforeCheckUniqConstraint,
                order,
                paymentProvider,
                unitOfWork,
                timeProvider,
                ConstraintCheckTimeline.BeforeConstraintCheck,
                ct);
        }

        var now = timeProvider.GetUtcNow().UtcDateTime;
        var paymentResult = OrderPayment.Create(orderId, order.TotalPrice, now);

        if(paymentResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(paymentResult.Error);

        var payment = paymentResult.Value;

        paymentRepository.Add(payment);

        try
        {
            await unitOfWork.SaveChangesAsync(ct);
        }
        catch (UniqueConstraintViolation)
        {
            var existingAfterCheckUniqConstraint = await paymentRepository.GetPendingByOrderIdAsync(orderId, ct);

            if (existingAfterCheckUniqConstraint is null)
                return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetCanNotFindPendingPaymentAfterCheckConstraintError(command.OrderId));

            return await EnsureSession(
                existingAfterCheckUniqConstraint,
                order,
                paymentProvider,
                unitOfWork,
                timeProvider,
                ConstraintCheckTimeline.AfterConstraintCheck,
                ct);
        }

        var sessionResult = await paymentProvider.CreatePaymentSessionAsync(
                    paymentId: payment.Id.Value,
                    orderId: order.Id.Value,
                    amount: order.TotalPrice.Sum,
                    currency: order.TotalPrice.Currency.Code,
                    scale: order.Currency.Scale,
                    ct: ct);

        if (sessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(sessionResult.Error);

        var session = sessionResult.Value;

        var extrernalSessionResult = ExternalSession.Create(
            session.ExternalSessionId,
            session.CheckoutUrl,
            session.ExpiresAtUtc);
        if (extrernalSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(extrernalSessionResult.Error);

        payment.AttachSession(extrernalSessionResult.Value, timeProvider.GetUtcNow().UtcDateTime);

        await unitOfWork.SaveChangesAsync(ct);

        return Result<StartOrderPaymentDetails>.Success(
            StartOrderPaymentDetails.GetNewPaymentWithNewSessionDetails(session, orderId.Value));
    }

    private static async Task<Result<StartOrderPaymentDetails>> EnsureSession(
        OrderPayment payment,
        Order order,
        IPaymentProvider paymentProvider,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        ConstraintCheckTimeline timeline,
        CancellationToken ct = default)
    {
        PaymentSession session;

        if (payment.HasSession && !payment.ExternalSession!.IsExpired(timeProvider.GetUtcNow().UtcDateTime))
        {
            session = new PaymentSession(
                payment.Id.Value,
                payment.ExternalSession.ExternalId,
                payment.ExternalSession.CheckoutUrl,
                payment.ExternalSession.ExpiresAtUtc);


            return timeline switch
            {
                ConstraintCheckTimeline.BeforeConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                    StartOrderPaymentDetails.GetExistingPaymentWithExistingSessionBeforeCheckConstraintDetails(session, order.Id.Value)),

                ConstraintCheckTimeline.AfterConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                    StartOrderPaymentDetails.GetExistingPaymentWithExistingSessionAfterCheckConstraintDetails(session, order.Id.Value)),

                _ => throw new InvalidOperationException($"Unknown CheckConstraint timeline in StartOrderPaymentCommandHandler.EnsureSession. It was {timeline}")
            };
        }

        var newSessionResult = await paymentProvider.CreatePaymentSessionAsync(
                paymentId: payment.Id.Value,
                orderId: order.Id.Value,
                amount: order.TotalPrice.Sum,
                currency: order.TotalPrice.Currency.Code,
                scale: order.Currency.Scale,
                ct: ct);

        if (newSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(newSessionResult.Error);

        session = newSessionResult.Value;

        var extrernalSessionResult = ExternalSession.Create(
            session.ExternalSessionId,
            session.CheckoutUrl,
            session.ExpiresAtUtc);

        if (extrernalSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(extrernalSessionResult.Error);


        payment.AttachSession(extrernalSessionResult.Value, timeProvider.GetUtcNow().UtcDateTime);

        await unitOfWork.SaveChangesAsync(ct);

        return timeline switch
        {
            ConstraintCheckTimeline.BeforeConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                StartOrderPaymentDetails.GetExistingPaymentWithNewSessionBeforeCheckConstraintDetails(session, order.Id.Value)),

            ConstraintCheckTimeline.AfterConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                StartOrderPaymentDetails.GetExistingPaymentWithNewSessionAfterCheckConstraintDetails(session, order.Id.Value)),

            _ => throw new InvalidOperationException($"Unknown CheckConstraint timeline in StartOrderPaymentCommandHandler.EnsureSession. It was {timeline}")
        };
    }
}
```

---

```cs title="StartOrderPaymentDetails.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Payments.Common;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record StartOrderPaymentDetails : ICustomLogSeverity
{
    public const string NewPaymentWithNewSessionCode = "StartOrderPayment.NewPaymentWithNewSession";

    public const string ExistingPaymentWithNewSessionBeforeCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithNewSessionBeforeCheckConstraint";
    public const string ExistingPaymentWithNewSessionAfterCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithNewSessionAfterCheckConstraint";

    public const string ExistingPaymentWithExistingSessionAfterCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithExistingSessionAfterCheckConstraint";
    public const string ExistingPaymentWithExistingSessionBeforeCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithExistingSessionBeforeCheckConstraint";

    public string Code { get; }
    public string Message { get; }
    public Guid OrderId { get; }
    public PaymentSession Session { get; }

    public bool IsWarning => Code switch
    {
        NewPaymentWithNewSessionCode => false,
        ExistingPaymentWithNewSessionBeforeCheckConstraintCode => false,
        ExistingPaymentWithNewSessionAfterCheckConstraintCode => true,
        ExistingPaymentWithExistingSessionAfterCheckConstraintCode => false,
        ExistingPaymentWithExistingSessionBeforeCheckConstraintCode => true,
        _ => false
    };

    private StartOrderPaymentDetails(string code,
    string message,
    PaymentSession session,
    Guid orderId)
    {
        Code = code;
        OrderId = orderId;
        Message = message;
        Session = session;
    }

    internal static StartOrderPaymentDetails GetNewPaymentWithNewSessionDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: NewPaymentWithNewSessionCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"created new Payment with PaymentId: {session.PaymentId} " +
            $"and session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithNewSessionBeforeCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithNewSessionBeforeCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and created new session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithNewSessionAfterCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithNewSessionAfterCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and created new session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithExistingSessionBeforeCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithExistingSessionBeforeCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and already exists session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithExistingSessionAfterCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithExistingSessionAfterCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and already exists session with ExternalSessionId: {session.ExternalSessionId}");
    }
}
```

---

```cs title="StartOrderPaymentErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class StartOrderPaymentErrors
{
    public const string OrderIsNotInPendingPaymentCode = "StartOrderPatyment.OrderIsNotInPendingPayment";
    public const string ForOrderIsAlreadyExistsSuccessedPaymentCode = "StartOrderPatyment.ForOrderIsAlreadyExistsSuccessedPayment";
    public const string CanNotFindPendingPaymentAfterCheckConstraintCode = "StartOrderPatyment.CanNotFindPendingPaymentAfterCheckConstraint";

    public static Error GetOrderIsNotInPendingPaymentError(Guid orderId, OrderStatus status)
    {
        return new(
            Code: OrderIsNotInPendingPaymentCode,
            Message: $"The order being processed with OrderId: {orderId} is not waiting payment. Its in {status} status now");
    }

    public static Error GetForOrderIsAlreadyExistsSuccessedPaymentError(Guid orderId)
    {
        return new(
            Code: ForOrderIsAlreadyExistsSuccessedPaymentCode,
            Message: $"For order being processed with OrderId: {orderId} is already exists successed payment");
    }

    public static Error GetCanNotFindPendingPaymentAfterCheckConstraintError(Guid orderId)
    {
        return new(
            Code: CanNotFindPendingPaymentAfterCheckConstraintCode,
            Message: $"For order being processed with OrderId: {orderId} can not find existing pending payment after ConstraintCheck");
    }
}
```

---

#### Common

```cs title="PaymentSession.cs"
namespace LegacyLego.Application.Payments.Common;

public sealed record PaymentSession(
    Guid PaymentId,
    string ExternalSessionId,
    string CheckoutUrl,
    DateTime ExpiresAtUtc);
```

---

```cs title="PaymentWebhook.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Payments.Common;

public record PaymentWebhook(
    string ExternalSessionId,
    string? TransactionId,
    Guid OrderId,
    decimal Amount,
    string Currency,
    PaymentStatus Status);
```

---

#### IntegrationEvents

```cs title="OrderPaymentRefundRequestedIntegrationEvent.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.IntegrationEvents;

public sealed record OrderPaymentRefundRequestedIntegrationEvent(
    OrderPaymentId PaymentId,
    Guid OrderId,
    decimal Amount,
    string Currency,       
    string TransactionId,
    DateTime OccurredOnUtc) : IIntegrationEvent;
```

---

## LegacyLego.Domain

```xml title="LegacyLego.Domain.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

---

### Abstractions

```cs title="IOrderRepository.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IOrderRepository
{
    public Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<TResult>> GetOrdersAsync<TResult>(Specification<Order,OrderId, TResult> specification, CancellationToken cancellationToken = default);

    public Task<TResult?> GetOrderAsync<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default);

    public Task<int> GetOrdersCountAsync(Specification<Order, OrderId> specification, CancellationToken cancellationToken = default);

    public void Add(Order order);
}
```

---

```cs title="IPaymentRepository.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IPaymentRepository
{
    public Task<OrderPayment?> GetByTransactionIdAsync(string id, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetPendingByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetByExternalSessionIdAsync(string externalSessionId, CancellationToken cancellationToken = default);

    public Task<bool> ExistsSucceededAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public void Add(OrderPayment payment);
}
```

---

### Aggregates

```cs title="Order.cs"
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Domain.Exceptions;

namespace LegacyLego.Domain.Aggregates;

public class Order : AggregateRoot<OrderId>
{
    public Guid ClientId { get; }

    public Currency Currency { get; }

    private Price? _frozenTotalPrice;

    public Price TotalPrice
    {
        get
        {
            return Status switch
            {
                OrderStatus.PendingPayment or OrderStatus.Expired
                    => CalculateTotalPrice(Items),
                OrderStatus.Paid or OrderStatus.Cancelled or OrderStatus.Refunded
                    => _frozenTotalPrice ?? throw new InvalidDomainStateException(
                        OrderExceptionalErrors.GetFrozenTotalPriceNotCalculatedError(Status)),
                _ => throw new InvalidDomainStateException(
                        OrderExceptionalErrors.GetWrongOrderStatusToGetTotalPriceError(Status)),
            };
        }
    }

    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items;

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public OrderAddress Address { get; }

    public DateTime CreationDateUtc { get; }

    private decimal? FrozenTotalSum => _frozenTotalPrice?.Sum;
    /// <summary>
    /// Приватный конструктор, используемый фабричным методом для создания нового
    /// валидного с точки зрения бизнеса заказа 
    /// </summary>
    /// <param name="id"> идентификатор заказа</param>
    /// <param name="clientId">идентификатор клиента, создавшего заказ</param>
    /// <param name="currency">валюты заказа</param>
    /// <param name="status">статус заказа</param>
    /// <param name="items">список позиций заказа</param>
    /// <param name="address">адрес доставки заказа</param>
    /// <param name="creationDateUtc">время создания заказа в формате Utc</param>
    private Order(
        OrderId id,
        Guid clientId,
        Currency currency,
        OrderStatus status,
        List<OrderItem> items,
        OrderAddress address,
        DateTime creationDateUtc) 
        : base(id)
    {
        ClientId = clientId;
        Status = status;
        _items = items;
        Address = address;
        CreationDateUtc = creationDateUtc;
        Currency = currency;
    }
    /// <summary>
    /// Приватный конструктор, используемый для материализации объекта Order
    /// EF ORM системой в соответствии с конфигурациями (Не используется бизнесом!)
    /// </summary>
    /// <param name="id"> идентификатор заказа</param>
    /// <param name="clientId">идентификатор клиента, создавшего заказ</param>
    /// <param name="currency">валюты заказа</param>
    /// <param name="status">статус заказа</param>
    /// <param name="frozenTotalSum">decimal занчение общей стоимсоти заказа</param>
    /// <param name="creationDateUtc">время создания заказа в формате Utc</param>
    private Order(
        OrderId id,
        Guid clientId,
        OrderStatus status,
        Currency currency,
        decimal? frozenTotalSum,
        DateTime creationDateUtc)
        : base(id)
    {
        ClientId = clientId;
        Status = status;
        _items = new List<OrderItem>();
        Currency = currency;
        CreationDateUtc = creationDateUtc;

        _frozenTotalPrice = frozenTotalSum.HasValue
            ? Price.Create(frozenTotalSum.Value, currency).Value
            : null;
    }


    /// <summary>
    /// Фабричный метод для создания нового заказа в системе.
    /// Инкапсулирует первичные бизнес-правила создания заказа.
    /// </summary>
    /// <param name="clientId">Идентификатор клиента, совершающего заказ.</param>
    /// <param name="address">Валидный адрес доставки (Value Object).</param>
    /// <returns>
    /// Экземпляр <see cref="Result{Order}"/>, содержащий объект заказа при успехе,
    /// либо ошибку доменной логики Result.Failure (например, если нарушены базовые контракты).
    /// </returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если параметр 
    /// <paramref name="address"/> или парметр <paramref name="items"/> равен null.</exception>
    /// <exception cref="ArgumentException">Выбрасывается, если параметр коллекции 
    /// <paramref name="items"/> содержит в коллекции хоть 1 тгдд элемент.</exception>
    public static Result<Order> Create(
        OrderAddress address,
        Guid clientId,
        List<OrderItem> items)
    {
        ArgumentNullException.ThrowIfNull(items,nameof(items));
        ArgumentNullException.ThrowIfNull(address, nameof(address));

        if (items.Any(x => x is null))
            throw new ArgumentException("Items collection contains null");

        if (clientId == Guid.Empty)
            return Result<Order>.Failure(OrderErrors.GetClientIdGuidInvalidError(clientId));

        var itemsCount = items.Count;
        // общее количество позиций не меньше одной
        if (itemsCount < 1)
            return Result<Order>.Failure(OrderErrors.GetItemsCountInvalidError(itemsCount));

        var firstCurrency = items.First().UnitPrice.Currency;

        if (items.Any(x => !x.UnitPrice.Currency.Equals(firstCurrency)))
            return Result<Order>.Failure(OrderErrors.GetItemsCurrenciesMismatchError());

        var total = CalculateTotalPrice(items);

        // общая стоимость всех позиций заказа больше нуля
        if (total.Sum <= 0)
            return Result<Order>.Failure(OrderErrors.GetItemsTotalBelowZeroError(total.Sum));

        var createdAt = DateTime.UtcNow;
        var orderId = OrderId.New();

        var order = new Order(
            id: orderId,
            clientId: clientId,
            currency: firstCurrency,
            status: OrderStatus.PendingPayment,
            items: items,
            address: address,
            creationDateUtc: createdAt);

        order.Raise(new OrderCreated(orderId, clientId, createdAt));

        return Result<Order>.Success(order);
    }

    public Result Pay()
    {
        var orderAction = OrderAction.Pay;
        var nextStatus = OrderStatus.Paid;

        if (Status is not (OrderStatus.PendingPayment or OrderStatus.Expired))
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;
        _frozenTotalPrice = CalculateTotalPrice(Items);

        base.Raise(new OrderPaid(Id,DateTime.UtcNow));

        return Result.Success();
    }

    public Result Cancel()
    {
        var orderAction = OrderAction.Cancel;
        var nextStatus = OrderStatus.Cancelled;

        if (Status is not (OrderStatus.PendingPayment or OrderStatus.Expired))
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;
        _frozenTotalPrice = CalculateTotalPrice(Items);

        base.Raise(new OrderCanceled(Id, DateTime.UtcNow));

        return Result.Success();
    }

    public Result Expire()
    {
        var orderAction = OrderAction.Expire;
        var nextStatus = OrderStatus.Expired;

        if (Status is not OrderStatus.PendingPayment)
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderExpired(Id, DateTime.UtcNow));

        return Result.Success();
    }

    public Result Refund()
    {
        var orderAction = OrderAction.Refund;
        var nextStatus = OrderStatus.Refunded;

        if (Status is not OrderStatus.Paid)
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderRefunded(Id, DateTime.UtcNow));

        return Result.Success();
    }

    private static Price CalculateTotalPrice(IReadOnlyList<OrderItem> items)
    {
        // при текущих инвариантах это невозможно, но станет актуально в случае, если добавятся функции добавления/удаления позиции товара
        if (items.Count == 0)
            throw new InvariantViolationException(
                OrderExceptionalErrors.GetOrderContainsNoItemsError());

        var currency = items.First().UnitPrice.Currency;

        var total = Price.Zero(currency);

        foreach (var item in items)
            total = total.Plus(item.GetTotalPrice());

        return total;
    }
}
```

---

```cs title="OrderPayment.cs"
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Aggregates;

public class OrderPayment : AggregateRoot<OrderPaymentId>
{
    public string? TransactionId { get; private set; }

    public OrderId OrderId { get; }

    public PaymentStatus Status { get; private set; }

    public Price ExpectedAmount { get; }

    public Price? ActualAmount { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public ExternalSession? ExternalSession { get; private set; }

    public bool HasSession => ExternalSession is not null;

    public bool IsRefundRequested => Status is PaymentStatus.RefundRequested;

    private OrderPayment(
        OrderPaymentId id,
        OrderId orderId,
        DateTime createdAtUtc,
        PaymentStatus status,
        Price expectedAmount) : base(id)
    {
        OrderId = orderId;
        Status = status;
        ExpectedAmount = expectedAmount;
        CreatedAtUtc = createdAtUtc;
    }
    /// <summary>
    /// Для материализации ORM
    /// </summary>
    /// <param name="id"></param>
    /// <param name="orderId"></param>
    /// <param name="createdAtUtc"></param>
    /// <param name="status"></param>
    private OrderPayment(
    OrderPaymentId id,
    OrderId orderId,
    DateTime createdAtUtc,
    PaymentStatus status) : base(id)
    {
        OrderId = orderId;
        Status = status;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<OrderPayment> Create(OrderId orderId, Price expectedAmount, DateTime createdAt)
    {
        ArgumentNullException.ThrowIfNull(orderId, nameof(orderId));
        ArgumentNullException.ThrowIfNull(expectedAmount, nameof(expectedAmount));

        if (createdAt == default) throw new ArgumentException("Date must be provided.", nameof(createdAt));
        if (createdAt.Kind is not DateTimeKind.Utc)
            return Result<OrderPayment>.Failure(
                OrderPaymentErrors.GetCreationTimeWasNotUtcError(createdAt.Kind));

        var status = PaymentStatus.Pending;
        var id = OrderPaymentId.New();

        var payment = new OrderPayment(id, orderId, createdAt, status, expectedAmount);

        payment.Raise(new OrderPaymentCreated(id, orderId, expectedAmount, createdAt));

        return Result<OrderPayment>.Success(payment);
    }

    public Result AttachSession(ExternalSession newSession, DateTime nowUtc)
    {
        ArgumentNullException.ThrowIfNull(newSession, nameof(newSession));

        if (nowUtc.Kind is not DateTimeKind.Utc)
            return Result.Failure(
                OrderPaymentErrors.GetNowTimeWasNotUtcForAttachSessionError(nowUtc.Kind));

        if (HasSession && !ExternalSession!.IsExpired(nowUtc))
            return Result.Failure(
                OrderPaymentErrors.GetEnsuredSessionIsNotExpiredTransitionFailureError(
                    ExternalSession.ExternalId,
                    newSession.ExternalId,
                    Id));

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(
                OrderPaymentErrors.GetWrongStatusForExternalSessionTransitionError(Id,
                    Status,
                    newSession.ExternalId));

        ExternalSession = newSession;

        return Result.Success();
    }

    public Result RegisterPaymentReceipt(string transactionId, Price actualAmount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);
        ArgumentNullException.ThrowIfNull(actualAmount);

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(
                OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(
                OrderPaymentErrors.GetStatusTransitionFailureError(PaymentAction.Success, Status, PaymentStatus.Succeeded));

        TransactionId = transactionId;
        ActualAmount = actualAmount;

        if (ActualAmount != ExpectedAmount)
        {
            Status = PaymentStatus.RefundRequested;

            base.Raise(new OrderPaymentAmountMismatchedAndRefundRequested(
                Id,
                OrderId,
                ExpectedAmount,
                ActualAmount,
                TransactionId));

            return Result.Success();
        }

        Status = PaymentStatus.Succeeded;
        base.Raise(new OrderPaymentSucceeded(Id, OrderId, ActualAmount, TransactionId));

        return Result.Success();
    }

    public Result MarkAsFailed()
    {
        var paymentAction = PaymentAction.Fail;
        var nextStatus = PaymentStatus.Failed;

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderPaymentFailed(Id));

        return Result.Success();
    }

    public Result MarkAsRefunded(string transactionId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        var paymentAction = PaymentAction.Refund;
        var nextStatus = PaymentStatus.Refunded;

        if (Status is not PaymentStatus.RefundRequested
            && Status is not PaymentStatus.Succeeded
            && Status is not PaymentStatus.Pending)
        {
            return Result.Failure(
                OrderPaymentErrors.GetStatusTransitionFailureError(
                    paymentAction, Status, nextStatus));
        }

        TransactionId ??= transactionId;
        Status = nextStatus;

        base.Raise(new OrderPaymentRefunded(Id, TransactionId!));

        return Result.Success();
    }
}
```

---

### DomainEvents

```cs title="OrderCanceled.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderCanceled(
    OrderId OrderId,
    DateTime CanceledAt) : IDomainEvent;
```

---

```cs title="OrderCreated.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderCreated(
    OrderId OrderId,
    Guid ClientId,
    DateTime CreatedAt) : IDomainEvent;
```

---

```cs title="OrderExpired.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderExpired(
    OrderId OrderId,
    DateTime ExpiredAt) : IDomainEvent;
```

---

```cs title="OrderPaid.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaid(
    OrderId OrderId,
    DateTime PaidAt) : IDomainEvent;
```

---

```cs title="OrderPaymentAmountMismatchedAndRefundRequested.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentAmountMismatchedAndRefundRequested(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    Price ExpectedAmount,
    Price ActualAmount,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderPaymentCreated.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentCreated(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    Price ExpectedAmount,
    DateTime CreatedAt) : IDomainEvent;
```

---

```cs title="OrderPaymentFailed.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentFailed(
    OrderPaymentId Paymentid) : IDomainEvent;
```

---

```cs title="OrderPaymentRefunded.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefunded(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderPaymentSucceeded.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentSucceeded(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    Price PaidAmount,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderRefunded.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderRefunded(
    OrderId OrderId,
    DateTime RefundedAt) : IDomainEvent;
```

---

### Enums

```cs title="OrderAction.cs"
namespace LegacyLego.Domain.Enums;

public enum OrderAction : byte
{
    Create,
    Pay,
    Expire,
    Cancel,
    Refund
}
```

---

```cs title="OrderStatus.cs"
namespace LegacyLego.Domain.Enums;

public enum OrderStatus : byte
{
    PendingPayment,
    Paid,
    Cancelled,
    Expired,
    Refunded
}
```

---

```cs title="PaymentAction.cs"
namespace LegacyLego.Domain.Enums;

public enum PaymentAction : byte
{
    Success,
    Fail,
    Refund,
    RefundRequest
}
```

---

```cs title="PaymentStatus.cs"
namespace LegacyLego.Domain.Enums;

public enum PaymentStatus : byte
{
    Pending,
    Succeeded,
    Failed,
    Refunded,
    RefundRequested
}
```

---

### Errors

```cs title="CurrencyErrors.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class CurrencyErrors
{
    public const string NotSupportedCode = "Currency.NotSupportedCode";
    public const string WrongCodeLengthCode = "Currency.WrongCodeLength";

    public static Error GetWrongCodeError(
        int actualCodeLength,
        string codeString)
    {
        return new(
            Code: WrongCodeLengthCode,
            Message: $"Код валюты должен состоять ровно из 3 символов. Код {codeString} содержит {actualCodeLength}");
    }

    public static Error GetNotSupportedError(string codeString)
    {
        return new(
            Code: NotSupportedCode,
            Message: $"Выбранная вами валюта {codeString} не поддерживается системой");
    }
}
```

---

```cs title="ExternalSessionErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class ExternalSessionErrors
{
    public const string ExpirationTimeWasNotUtcCode= "ExternalSession.ExpirationTimeWasNotUtc";

    public static Error GetExpirationTimeWasNotUtceError(DateTimeKind timeKind)
    {
        return new(
            Code: ExpirationTimeWasNotUtcCode,
            Message: $"Тип передаваемого времени должен быть представлен Utc, но был {timeKind}");
    }
}
```

---

```cs title="OrderErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class OrderErrors
{
    public const string StatusTransitionFailureCode = "Order.StatusTransitionFailure";
    public const string ItemsCountInvalidCode = "Order.ItemsCountInvalid";
    public const string ItemsCurrenciesMismatchCode = "Order.ItemsCurrenciesMismatch";
    public const string ItemsTotalBelowZeroCode = "Order.ItemsTotalBelowZero";
    public const string ClientIdGuidInvalidCode = "Order.ClientIdGuidInvalid";
    public const string NotFoundByOrderId = "Order.NotFoundByOrderId";

    public static Error GetStatusTransitionFailureError(
        OrderAction action,
        OrderStatus actualStatus,
        OrderStatus nextStatus)
    {
        return new(
            Code: StatusTransitionFailureCode,
            Message: $"Action {action.ToString()} не позволяет перевести Order из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetItemsCountInvalidError(int itemsCount)
    {
        return new(
            Code: ItemsCountInvalidCode,
            Message: $"Невозможно создать заказ с общим количеством позиций {itemsCount}, должна быть хотя бы 1 позиция");
    }

    public static Error GetItemsCurrenciesMismatchError()
    {
        return new(
            Code: ItemsCurrenciesMismatchCode,
            Message: "Стоимости всех позиций заказа не должны быть представлены разными валютами");
    }

    public static Error GetItemsTotalBelowZeroError(decimal total)
    {
        return new(
            Code: ItemsTotalBelowZeroCode,
            Message: $"Общая стоимость всех позиций заказа не должна быть меньше 0, {total} не подходит");
    }

    public static Error GetClientIdGuidInvalidError(Guid guid)
    {
        return new(
            Code: ClientIdGuidInvalidCode,
            Message: $"Полученный ProductId GUID: {guid} Не прошел валидацию"
        );
    }

    public static Error GetNotFoundByOrderIdError(OrderId orderId)
    {
        return new(
            Code: NotFoundByOrderId,
            Message: $"Не удалось найти заказ по заданному первичному ключу: {orderId.Value}"
        );
    }
}
```

---

```cs title="OrderItemErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class OrderItemErrors
{
    public const string TitleInvalidCode = $"OrderItem.TitleInvalid";
    public const string QuantityBelowOneCode = $"OrderItem.QuantityBelowOne";
    public const string ProductIDGuidInvalidCode = $"OrderItem.ProductIDGuidInvalid";

    public static Error GetTitleInvalidError()
    {
        return new(
            Code: TitleInvalidCode,
            Message: "В названии товара не должно быть пустой строки"
        );
    }

    public static Error GetProductIDGuidInvalidError(Guid invalidGuid)
    {
        return new(
            Code: ProductIDGuidInvalidCode,
            Message: $"Полученный ProductId GUID: {invalidGuid} Не прошел валидацию"
        );
    }

    public static Error GetQuantityBelowOneError(int quantity)
    {
        return new(
            Code: QuantityBelowOneCode,
            Message: "Позиция заказа не может быть создана в количестве меньшем единице. " +
                     $"Значение {quantity} не соответствует правилам валидации"
        );
    }
}
```

---

```cs title="OrderPaymentErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class OrderPaymentErrors
{
    public const string StatusTransitionFailureCode = "OrderPayment.StatusTransitionFailure";
    public const string WrongTransactionIdExchangeCode = "OrderPayment.WrongTransactionIdExchange";
    public const string CreationTimeWasNotUtcCode = "OrderPayment.CreationTimeWasNotUtc";

    public const string NowTimeWasNotUtcForAttachSessionCode = "OrderPayment.NowTimeWasNotUtcForAttachSession";
    public const string WrongStatusForExternalSessionTransitionCode = "OrderPayment.WrongStatusForExternalSessionTransition";
    public const string EnsuredSessionIsNotExpiredTransitionFailureCode = "OrderPayment.EnsuredSessionIsNotExpiredTransitionFailure";

    public static Error GetStatusTransitionFailureError(
        PaymentAction action,
        PaymentStatus actualStatus,
        PaymentStatus nextStatus)
    {
        return new(
            Code: StatusTransitionFailureCode,
            Message: $"Action {action.ToString()} не позволяет перевести OrderPayment из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetWrongTransactionIdExchangeError(string currentId,string nextId)
    {
        return new(
            Code: WrongTransactionIdExchangeCode,
            Message: $"Недопустимая замена текущего TransactionId:{currentId} на {nextId} в MarkAsSucceeded операции");
    }


    public static Error GetCreationTimeWasNotUtcError(DateTimeKind timeKind)
    {
        return new(
            Code: CreationTimeWasNotUtcCode,
            Message: $"Тип передаваемого времени создания OrderPayment должен быть представлен Utc, но был {timeKind}");
    }

    public static Error GetNowTimeWasNotUtcForAttachSessionError(DateTimeKind timeKind)
    {
        return new(
            Code: NowTimeWasNotUtcForAttachSessionCode,
            Message: $"Тип передаваемого времени в AttachSession должен быть представлен Utc, но был {timeKind}");
    }

    public static Error GetWrongStatusForExternalSessionTransitionError(
        OrderPaymentId paymentId,
        PaymentStatus status,
        string newSession)
    {
        return new(
            Code: WrongStatusForExternalSessionTransitionCode,
            Message: $"Для оплаты {paymentId.Value} невозможно установить сессию {newSession}," +
            $" так какдля статуса {status} не подразумевается возможности установки сессии");
    }

    public static Error GetEnsuredSessionIsNotExpiredTransitionFailureError(
        string oldSessionId,
        string newSessionId,
        OrderPaymentId paymentId)
    {
        return new(
            Code: EnsuredSessionIsNotExpiredTransitionFailureCode,
            Message: $"Не получилось установить внешнюю сессию: {newSessionId} для оплаты {paymentId.Value}" +
            $", так как для  данной оплаты уже установлена сессия: {oldSessionId}, которая ещё не просрочена");
    }

}
```

---

```cs title="PriceErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class PriceErrors
{
    public const string SumBelowZeroCode = $"Price.SumBelowZero";
    public static Error GetSumBelowZeroError(
        decimal sum)
    {
        return new(
            Code: SumBelowZeroCode,
            Message: $"значение цены не должно равняться нулю или опускаться ниже, текущее значение {sum} некорректно"
        );
    }
}
```

---

### ExceptionalErrors

```cs title="CurrencyExceptionalErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class CurrencyExceptionalErrors
{
    public const string InvalidScaleValue = "Currency.InvalidScaleValue";

    public static ExceptionalError GetInvalidScaleValueError(int actualScale)
    {
        return new(
            Code: InvalidScaleValue,
            Message: $"Scale валюты не должен опускаться ниже нуля, значение: {actualScale} нарушает целостность системы");
    }
}
```

---

```cs title="ExternalSessionExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class ExternalSessionExceptionalErrors
{
    public const string IsExpiredCompressionParameterIsNotUtcCode = "ExternalSession.IsExpiredCompressionParameterIsNotUtcCode ";

    public static ExceptionalError GetIsExpiredCompressionParameterIsNotUtcError(DateTimeKind timeKind)
    {
        return new(
            Code: IsExpiredCompressionParameterIsNotUtcCode,
            Message: $"Тип передаваемого времени передаваемого параметра в метод IsExpired должен быть представлен Utc, но был {timeKind}");
    }
}
```

---

```cs title="OrderExceptionalErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class OrderExceptionalErrors
{
    public const string ContainsNoItemsErrorCode = "OrderContainsNoItems";
    public const string FrozenTotalPriceNotCalculatedErrorCode = "Order.FrozenTotalPriceNotCalculated";
    public const string WrongOrderStatusToGetTotalPriceErrorCode = "Order.WrongOrderStatusToGetTotalPrice";
    public static ExceptionalError GetOrderContainsNoItemsError()
    {
        return new(
            Code: ContainsNoItemsErrorCode,
            Message: $"Order не может не содержать ни 1 позиции при расчёте TotalPrice"
        );
    }

    public static ExceptionalError GetFrozenTotalPriceNotCalculatedError(OrderStatus status)
    {
        return new(
            Code: FrozenTotalPriceNotCalculatedErrorCode,
            Message: $"При обращении к полю _frozenTotalPrice произошла ошибка: значение свойства не расчитано." +
            $"в том случае, если текущий Order Status имеет значение {status.ToString()}, занчение _frozenTotalPrice уже должно быть рассчитано"
        );
    }

    public static ExceptionalError GetWrongOrderStatusToGetTotalPriceError(OrderStatus status)
    {
        return new(
            Code: WrongOrderStatusToGetTotalPriceErrorCode,
            Message: $"Для статуса {status.ToString()} рассчитать значение TotalPrice невозможно"
        );
    }
}
```

---

```cs title="PriceExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class PriceExceptionalErrors
{

    public const string MultiplyBelowZeroErrorCode = "Price.SumBelowZero";
    public const string CurrencyMismatchErrorCode = "Price.CurrencyMismatch";
    public const string DecimalSumOverflowErrorCode = "Price.DecimalSumOverflow";

    public static ExceptionalError GetMultiplyBelowZeroError(
        int factor)
    {
        return new(
            Code: MultiplyBelowZeroErrorCode,
            Message: $"Множитель стоимости не должен опускаться ниже нуля, текущее значение {factor} нарушает доменную логику"
        );
    }

    public static ExceptionalError GetCurrencyMismatchError(
        string currencyCode,
        string otherCurrencyCode)
    {
        return new(
            Code: CurrencyMismatchErrorCode,
            Message: $"Складывать значения различных валют недопустимо: " +
            $"({currencyCode} + {otherCurrencyCode}) считается недопустимой операцией, нарушающей доменную логику"
        );
    }

    public static ExceptionalError GetAdditionSumOverflowError(
        decimal firstValue,
        decimal otherValue
        )
    {
        return new(
            Code: DecimalSumOverflowErrorCode,
            Message: $"В результате выполнения математической операции сложения" +
            $"со значениями цены: {firstValue} и {otherValue} произошел decimal Overflow");
    }

    public static ExceptionalError GetMultiplySumOverflowError(
        decimal firstValue,
        int factor
        )
    {
        return new(
            Code: DecimalSumOverflowErrorCode,
            Message: $"В результате выполнения математической операции умножения " +
            $"цены: {firstValue} на множитель {factor} произошел decimal Overflow");
    }
}
```

---

```cs title="ResultExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class ResultExceptionalErrors
{
    public const string UnexpectedValueAccessErrorCode = "Result.UnexpectedValueAccess";

    public const string InvalidInitializationErrorCode = "Result.InvalidInitialization";

    public static ExceptionalError GetUnexpectedValueAccessError()
    {
        return new(
            Code: UnexpectedValueAccessErrorCode,
            Message: $"Некорректное обращение к Result.Value в случае, когда IsSuccess = false"
        );
    }

    public static ExceptionalError GetInvalidResultInitializationError(
        bool isSuccess,
        bool isErrorContains)
    {
        var message = InvalidInitializationErrorCode;

        switch (isSuccess)
        {
            case true when isErrorContains:
                message = "Result не может быть успешным (IsSuccess) и одновременно содержать ошибку (Error), это нарушение состояния"; break;

            case false when !isErrorContains:
                message = "Result не может быть инициализирован как Failure и одновременно с тем не содержать ошибки (Error.None)"; break;
        }

        return new(
            Code: InvalidInitializationErrorCode,
            Message: message!
        );
    }
}
```

---

### Exceptions

```cs title="InvalidDomainStateException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Exceptions;

public class InvalidDomainStateException : DomainException
{
    public InvalidDomainStateException(ExceptionalError error) : base(error) { }
}
```

---

```cs title="InvariantViolationException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Exceptions;

public class InvariantViolationException : DomainException
{
    public InvariantViolationException(ExceptionalError error) : base(error) { }
}
```

---

### Shared

```cs title="AggregateRoot.cs"
namespace LegacyLego.Domain.Shared;

public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvents
    where TId : ValueObject
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(TId id) : base(id)
    {

    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
```

---

```cs title="DomainException.cs"
namespace LegacyLego.Domain.Shared;

public abstract class DomainException : Exception
{
    public ExceptionalError Error { get; }

    protected DomainException(ExceptionalError error)
        : base(error.Code + ": " + error.Message) 
    {
        Error = error;
    }
}
```

---

```cs title="Entity.cs"
using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Domain.Shared;

public abstract class Entity<TId> : IEquatable<Entity<TId>> 
    where TId : ValueObject
{

    public TId Id { get; init; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Equals(entity);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        return Id.Equals(other.Id);

    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```

---

```cs title="Error.cs"
namespace LegacyLego.Domain.Shared;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new("", "");
}
```

---

```cs title="ExceptionalError.cs"
namespace LegacyLego.Domain.Shared;

public sealed record ExceptionalError(string Code, string Message);
```

---

```cs title="IDomainEvent.cs"
namespace LegacyLego.Domain.Shared;

public interface IDomainEvent
{

}
```

---

```cs title="IHasDomainEvents.cs"
namespace LegacyLego.Domain.Shared;

public interface IHasDomainEvents
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
```

---

```cs title="Result.cs"
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;

namespace LegacyLego.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
            case false when error == Error.None:
                throw new InvalidDomainStateException(
                    ResultExceptionalErrors.GetInvalidResultInitializationError(isSuccess, error != Error.None));
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() =>
        new(true, Error.None);

    public static Result Failure(Error error) =>
        new(false, error);
}
```

---

```cs title="ResultT.cs"
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.ExceptionalErrors;

namespace LegacyLego.Domain.Shared;

public class Result<T> : Result
{
    private readonly T _value;

    public T Value => IsSuccess
        ? _value
        : throw new InvalidDomainStateException(
            ResultExceptionalErrors.GetUnexpectedValueAccessError());

    private Result(T value)
        : base(true, Error.None)
    {
        _value = value;
    }

    private Result(Error error)
        : base(false, error)
    {
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }
}
```

---

```cs title="Specification.cs"
using System.Linq.Expressions;

namespace LegacyLego.Domain.Shared;

public abstract class Specification<TEntity, TId, TResult> : Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : ValueObject
{
    public Expression<Func<TEntity, TResult>> Selector { get; }

    protected Specification(
        Expression<Func<TEntity, TResult>> selector)
    {
        Selector = selector;
    }
}

public abstract class Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : ValueObject
{
    public int? SkipNum { get; private set; }

    public int? LimitNum { get; private set; }

    public List<Expression<Func<TEntity, bool>>> FilterExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> OrderByExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> OrderByDescendingExpressions { get; } = new();

    protected Specification() { }

    protected void AddFilter(Expression<Func<TEntity, bool>> filterExpression) =>
        FilterExpressions.Add(filterExpression);

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        IncludeExpressions.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpressions.Add(orderByExpression);

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpressions.Add(orderByDescendingExpression);

    protected void SetSkipNum(int skipNum) =>
        SkipNum = skipNum;

    protected void SetLimitNum(int limitNum) =>
        LimitNum = limitNum;

    protected void DropSkip() =>
        SkipNum = null;

    protected void DropLimit() =>
        LimitNum = null;
}
```

---

```cs title="ValueObject.cs"
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.Shared;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other)
            return false;

        return Equals(other);
    }

    public bool Equals(ValueObject? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        return ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(new HashCode(),
                (hash, value) =>
                {
                    hash.Add(value);
                    return hash;
                }).ToHashCode();
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

}
```

---

### ValueObjects

```cs title="Currency.cs"
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Shared;
using System.IO.IsolatedStorage;

namespace LegacyLego.Domain.ValueObjects;

public class Currency : ValueObject
{
    private static readonly Dictionary<string, Currency> Codes;

    public static readonly Currency Usd = new("USD", "$", 2);

    public static readonly Currency Eur = new("EUR", "€", 2);

    public static readonly Currency Rub = new("RUB", "₽", 2);

    public string Code { get; }

    public string Symbol { get; }

    public int Scale { get; }

    static Currency()
    {
        Codes = new Dictionary<string, Currency>()
        {
            { Usd.Code, Usd},
            { Eur.Code, Eur},
            { Rub.Code, Rub}
        };
    }

    private Currency(string code, string symbol, int scale = 2)
    {
        if (scale < 0)
           throw new InvariantViolationException(
                CurrencyExceptionalErrors.GetInvalidScaleValueError(scale));

        Code = code.ToUpperInvariant();
        Symbol = symbol;
        Scale = scale;

    }

    public static Result<Currency> FromCode(string code)
    {
        if(code is null) 
            throw new ArgumentNullException(nameof(code));

        var codeString = code.Trim().ToUpperInvariant();

        if (codeString.Length != 3)
            return Result<Currency>.Failure(
                CurrencyErrors.GetWrongCodeError(codeString.Length, codeString));

        if (!Codes.TryGetValue(codeString, out var currency))
            return Result<Currency>.Failure(
                CurrencyErrors.GetNotSupportedError(codeString));

        var scale = currency.Scale;


        return Result<Currency>.Success(currency);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
    }
}
```

---

```cs title="ExternalSession.cs"
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class ExternalSession : ValueObject
{
    public string ExternalId { get; }
    public string CheckoutUrl { get; }
    public DateTime ExpiresAtUtc { get; }

    private ExternalSession(string externalId, string checkoutUrl, DateTime expiresAtUtc)
    {
        ExternalId = externalId;
        CheckoutUrl = checkoutUrl;
        ExpiresAtUtc = expiresAtUtc;
    }

    public static Result<ExternalSession> Create(string externalId, string checkoutUrl, DateTime expiresAtUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(externalId, nameof(externalId));
        ArgumentException.ThrowIfNullOrWhiteSpace(checkoutUrl, nameof(checkoutUrl));

        if (expiresAtUtc.Kind is not DateTimeKind.Utc)
            return Result<ExternalSession>.Failure(
                ExternalSessionErrors.GetExpirationTimeWasNotUtceError(expiresAtUtc.Kind));

        return Result<ExternalSession>.Success(new ExternalSession(externalId, checkoutUrl, expiresAtUtc));
    }

    public bool IsExpired(DateTime nowUtc)
    {
        if (nowUtc.Kind is not DateTimeKind.Utc)
        {
            throw new InvariantViolationException(ExternalSessionExceptionalErrors.GetIsExpiredCompressionParameterIsNotUtcError(nowUtc.Kind));
        }

        return ExpiresAtUtc <= nowUtc;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ExternalId;
        yield return CheckoutUrl;
        yield return ExpiresAtUtc;
    }
}
```

---

```cs title="OrderAddress.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class OrderAddress : ValueObject
{
    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string PostalCode { get; }

    private OrderAddress(
        string country,
        string city,
        string street,
        string postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public static Result<OrderAddress> Create(
        string country,
        string city,
        string street,
        string postalCode)
    {
        return Result<OrderAddress>.Success(new OrderAddress(
            country,
            city,
            street,
            postalCode
        ));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return PostalCode;
    }
}
```

---

```cs title="OrderId.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderId : ValueObject, IComparable<OrderId>
{
    public Guid Value { get; }

    public OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());

    public static OrderId From(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public int CompareTo(OrderId? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator <(OrderId? left, OrderId? right) => Compare(left, right) < 0;
    public static bool operator >(OrderId? left, OrderId? right) => Compare(left, right) > 0;
    public static bool operator <=(OrderId? left, OrderId? right) => Compare(left, right) <= 0;
    public static bool operator >=(OrderId? left, OrderId? right) => Compare(left, right) >= 0;

    private static int Compare(OrderId? left, OrderId? right)
    {
        if (ReferenceEquals(left, right)) return 0;
        if (left is null) return -1;

        return left.CompareTo(right);
    }
}
```

---

```cs title="OrderItem.cs"
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class OrderItem : ValueObject
{
    public string Title { get; }

    public int Quantity { get; }

    public Guid ProductId { get; }

    public Price UnitPrice { get; }

    private OrderItem(
        string title,
        int quantity,
        Guid productId,
        Price unitPrice)
    {
        Title = title;
        Quantity = quantity;
        ProductId = productId;
        UnitPrice = unitPrice;
    }

    private OrderItem(
        string title,
        int quantity,
        Guid productId)
    {
        Title = title;
        Quantity = quantity;
        ProductId = productId;
    }

    public static Result<OrderItem> Create(
        string title,
        int quantity,
        Guid productId,
        Price unitPrice)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        ArgumentNullException.ThrowIfNull(unitPrice, nameof(unitPrice));

        title = title.Trim();

        if (productId == Guid.Empty)
            return Result<OrderItem>.Failure(OrderItemErrors.GetProductIDGuidInvalidError(productId));

        if (quantity < 1)
            return Result<OrderItem>.Failure(OrderItemErrors.GetQuantityBelowOneError(quantity));

        return Result<OrderItem>.Success(new OrderItem(title, quantity, productId, unitPrice));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ProductId;
        yield return Title;
        yield return Quantity;
        yield return UnitPrice;
    }

    public Price GetTotalPrice()
    {
        return UnitPrice.MultiplyByQuantity(Quantity);
    }
}
```

---

```cs title="OrderPaymentId.cs"
using LegacyLego.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderPaymentId : ValueObject
{
    public Guid Value { get; }

    public OrderPaymentId(Guid value)
    {
        Value = value;
    }

    public static OrderPaymentId New() => new(Guid.NewGuid());

    public static OrderPaymentId From(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
```

---

```cs title="Price.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Sum { get; }

    public Currency Currency { get; }

    public bool IsPositive => Sum > 0;

    public bool IsZero => Sum == 0m;

    private Price(decimal sum,Currency currency)
    {
        Sum = Normalize(sum, currency.Scale);
        Currency = currency;   
    }

    private static decimal Normalize(decimal value, int scale)
    {
        return Math.Round(value, scale, MidpointRounding.ToEven);
    }

    internal static Price Zero(Currency currency)
    {
        return new Price(0m, currency);
    }

    public static Result<Price> Create(decimal sum, Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);

        var normalized = Normalize(sum, currency.Scale);

        if (normalized <= 0)
        {
            return Result<Price>.Failure(PriceErrors.GetSumBelowZeroError(sum));
        }

        var price = new Price(normalized, currency);

        return Result<Price>.Success(price);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Sum;
    }

    public Price Plus(Price other)
    {
        if (!this.Currency.Equals(other.Currency))
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetCurrencyMismatchError(
                    this.Currency.Code,other.Currency.Code));
        }

        decimal sum;

        try
        {
            sum = checked(this.Sum + other.Sum);
        }
        catch (OverflowException)
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetAdditionSumOverflowError(this.Sum,other.Sum));
        }

        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }

    public Price MultiplyByQuantity(int factor)
    {
        if (factor < 0)
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetMultiplyBelowZeroError(factor));
        }

        decimal sum;

        try
        {
            sum = checked(this.Sum * factor);
        }
        catch (OverflowException)
        {
            throw new InvariantViolationException(PriceExceptionalErrors
                .GetMultiplySumOverflowError(this.Sum, factor));
        }

        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }
}
```

---

## LegacyLego.Domain.Tests

```xml title="LegacyLego.Domain.Tests.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <OutputType>Exe</OutputType>
    <TUnit_Parallel>true</TUnit_Parallel>
    <TUnit_DefaultTimeout>00:05:00</TUnit_DefaultTimeout>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.TimeProvider.Testing" Version="10.5.0" />
    <PackageReference Include="TUnit" Version="1.22.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\LegacyLego.Domain\LegacyLego.Domain.csproj" />
  </ItemGroup>

</Project>
```

---

```cs title="GlobalUsings.cs"
global using LegacyLego.Domain.Aggregates;
global using LegacyLego.Domain.DomainEvents;
global using LegacyLego.Domain.Enums;
global using LegacyLego.Domain.Errors;
global using LegacyLego.Domain.ExceptionalErrors;
global using LegacyLego.Domain.Exceptions;
global using LegacyLego.Domain.Shared;
global using LegacyLego.Domain.ValueObjects;
global using LegacyLego.Domain.Tests.Common.Builders;

global using static LegacyLego.Domain.Tests.Common.Factories.OrderDataFactory;
global using static LegacyLego.Domain.Tests.Common.Factories.OrderPaymentDataFactory;
```

---

### Common

#### Builders

```cs title="OrderBuilder.cs"
namespace LegacyLego.Domain.Tests.Common.Builders;

public class OrderBuilder
{
    private OrderAddress? _address = null!;
    private Guid? _clientId = null!;
    private List<OrderItem>? _items = null!;

    public OrderBuilder WithAddress(OrderAddress address)
    {
        _address = address;
        return this;
    }

    public OrderBuilder WithNullAddress()
    {
        _address = null!;
        return this;
    }

    public OrderBuilder WithClientId(Guid clientId)
    {
        _clientId = clientId;
        return this;
    }

    public OrderBuilder WithEmptyClientId()
    {
        _clientId = Guid.Empty;
        return this;
    }

    public OrderBuilder WithItems(List<OrderItem> items)
    {
        _items = items;
        return this;
    }

    public OrderBuilder WithNullItems()
    {
        _items = null!;
        return this;
    }

    public OrderBuilder WithNoItems()
    {
        _items = new List<OrderItem>();
        return this;
    }

    public OrderBuilder AddItem(OrderItem item)
    {
        _items?.Add(item);
        return this;
    }

    public OrderBuilder AddNullItem()
    {
        _items?.Add(null!);
        return this;
    }

    public Result<Order> BuildResult()
    {
        return Order.Create(_address!, _clientId ?? Guid.Empty, _items!);
    }

    public Order BuildValue()
    {
        return Order.Create(_address!, _clientId ?? Guid.Empty, _items!).Value;
    }
}
```

---

#### Factories

```cs title="OrderDataFactory.cs"
namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class OrderDataFactory
{
    public static Order CreateDefaultOrder()
    {
        var address = OrderAddress.Create("US", "Berlin", "New York", "90210").Value;
        var items = new List<OrderItem>
        {
            OrderItem.Create("Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value
        };
        return Order.Create(address, Guid.NewGuid(), items).Value;
    }
}
```

---

```cs title="OrderPaymentDataFactory.cs"
namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class OrderPaymentDataFactory
{
    public static OrderPayment CreateDefaultOrderPayment()
        => CreateDefaultOrderPayment(100m);

    public static OrderPayment CreateDefaultOrderPayment(decimal amount)
    {
        var price = PriceDataFactory.CreatePrice(amount);
        return OrderPayment.Create(OrderId.New(), price, DateTime.UtcNow).Value;
    }

    public static OrderPayment CreateSucceededOrderPayment(decimal amount = 100m, string txId = "tx-123")
    {
        var payment = CreateDefaultOrderPayment(amount);
        var price = PriceDataFactory.CreatePrice(amount);
        payment.RegisterPaymentReceipt(txId, price);
        payment.ClearDomainEvents();
        return payment;
    }

    public static OrderPayment CreateRefundRequestedOrderPayment(decimal expectedAmount = 100m, decimal actualAmount = 50m, string txId = "tx-123")
    {
        var payment = CreateDefaultOrderPayment(expectedAmount);
        var mismatchedPrice = PriceDataFactory.CreatePrice(actualAmount);
        payment.RegisterPaymentReceipt(txId, mismatchedPrice);
        payment.ClearDomainEvents();
        return payment;
    }

    public static OrderPayment CreateRefundedOrderPayment(string txId = "tx-123")
    {
        var payment = CreateRefundRequestedOrderPayment(txId: txId);
        payment.MarkAsRefunded(txId);
        payment.ClearDomainEvents();
        return payment;
    }

    public static OrderPayment CreateFailedOrderPayment()
    {
        var payment = CreateDefaultOrderPayment();
        payment.MarkAsFailed();
        payment.ClearDomainEvents();
        return payment;
    }
}
```

---

```cs title="PriceDataFactory.cs"
namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class PriceDataFactory
{
    public static Price CreatePrice(decimal sum = 100m, string currencyCode = "RUB")
    {
        var currency = Currency.FromCode(currencyCode).Value;
        return Price.Create(sum, currency).Value;
    }
}
```

---

### CurrencyTests

#### Equality

```cs title="CurrencyEqualityTests.cs"
namespace LegacyLego.Domain.Tests.CurrencyTests;

public class CurrencyEqualityTests
{
    [Test]
    public async Task Equals_WithSameCode_ShouldBeTrue()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("usd").Value;

        await Assert.That(c1.Equals(c2)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentCode_ShouldBeFalse()
    {
        var usd = Currency.FromCode("USD").Value;
        var rub = Currency.FromCode("RUB").Value;

        await Assert.That(usd.Equals(rub)).IsFalse();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithEqualsOperator()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("USD").Value;

        await Assert.That(c1 == c2).IsTrue();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithNotEqualsOperator()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("RUB").Value;

        await Assert.That(c1 != c2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("usd").Value;

        await Assert.That(c1.GetHashCode()).IsEqualTo(c2.GetHashCode());
    }

    [Test]
    public async Task Equals_ShouldDependOnlyOnCode()
    {
        var usd = Currency.FromCode("USD").Value;

        await Assert.That(usd.Code).IsEqualTo("USD");
    }
}
```

---

#### FromCode

```cs title="CurrencyFromCodeTests.cs"
namespace LegacyLego.Domain.Tests.CurrencyTests;

public class CurrencyFromCodeTests
{
    [Test]
    public async Task FromCode_WithValidCodeUSD_ShouldReturnSuccess()
    {
        var result =  Currency.FromCode("USD");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithUnknownValidCode_ShouldReturnNotSupported()
    {
        var result = Currency.FromCode("ABC");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).Member(error => error.Code,name => name.EqualTo(CurrencyErrors.NotSupportedCode));
    }

    [Test]
    public async Task FromCode_WithEmptyString_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    public async Task FromCode_WithInvalidCodeLength_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("USDDD");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error)
            .Member(error => error.Code, code => code.EqualTo(CurrencyErrors.WrongCodeLengthCode));
    }

    [Test]
    public async Task FromCode_WithNullCode_ShouldThrowArgumentNullException()
    {
        var exception = await Assert.That(() => Currency.FromCode(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("usd")]
    [Arguments("Usd")]
    [Arguments("uSd")]
    [Arguments("usD")]
    [Arguments("UsD")]
    public async Task FromCode_WithLowerCaseValidCode_ShouldReturnSuccess(string inputLowerCode)
    {
        var result = Currency.FromCode(inputLowerCode);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithValidUntrimmedCode_ShouldReturnSuccess()
    {
        var result = Currency.FromCode("    USD  ");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithInvalidUntrimmedCode_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("    USDDDDD  ");

        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    [Arguments("U")]
    [Arguments("US")]
    public async Task FromCode_WithShortCode_ShouldReturnWrongCodeLengthError(string code)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    [Arguments("USD", "$", 2)]
    [Arguments("RUB", "₽", 2)]
    [Arguments("EUR", "€", 2)]
    public async Task FromCode_ShouldReturnCurrencyWithCorrectProperties(string code,string symbol,int scale)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Symbol).IsEqualTo(symbol);
        await Assert.That(result.Value.Scale).IsEqualTo(scale);
    }
}
```

---

### ExternalSessionTests

#### Create

```cs title="ExternalSessionCreateTests.cs"
namespace LegacyLego.Domain.Tests.ExternalSessionTests;

public class ExternalSessionCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldPreserve()
    {
        var id = "id";
        var url = "url";
        var time = DateTime.UtcNow.AddMinutes(60);

        var r = ExternalSession.Create(id, url, time);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value)
            .Member(x => x.ExternalId, m => m.IsEqualTo(id))
            .And.Member(x => x.CheckoutUrl, m => m.IsEqualTo(url))
            .And.Member(x => x.ExpiresAtUtc, m => m.IsEqualTo(time));
    }

    [Test]
    public async Task Create_WithNullId_ShouldThrowArgumentNullException()
    {
        var action = () => ExternalSession.Create(null!, "url", DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithNullUrl_ShouldThrowArgumentNullException()
    {
        var action = () => ExternalSession.Create("id", null!, DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithWhiteId_ShouldThrowArgumentException()
    {
        var action = () => ExternalSession.Create("", "url", DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithWhiteUrl_ShouldThrowArgumentException()
    {
        var action = () => ExternalSession.Create("id", "", DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithAlreadyExpiredTime_ShouldReturnSuccess()
    {
        var id = "id";
        var url = "url";
        var time = DateTime.UtcNow.AddMinutes(-10);

        var r = ExternalSession.Create(id, url, time);
        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithNotUtcExpireTime_ShouldReturnFailureWithExpirationTimeWasNotUtceError()
    {
        var id = "id";
        var url = "url";
        var timeNotUtc = new DateTime(2026, 5, 7, 10, 0, 0, DateTimeKind.Local); ;

        var r = ExternalSession.Create(id, url, timeNotUtc);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(ExternalSessionErrors.ExpirationTimeWasNotUtcCode);
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var time = DateTime.UtcNow;

        var session1 = ExternalSession.Create("id", "url", time).Value;
        var session2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(session1).IsEqualTo(session2);
        await Assert.That(ReferenceEquals(session1, session2)).IsFalse();
    }

    [Test]
    public async Task Create_EqualObjects_ShouldHaveSameHashCode()
    {
        var time = DateTime.UtcNow;

        var session1 = ExternalSession.Create("id", "url", time).Value;
        var session2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(session1.GetHashCode()).IsEqualTo(session2.GetHashCode());
    }
}
```

---

#### Equality

```cs title="ExternalSessionEqualityTests.cs"
namespace LegacyLego.Domain.Tests.ExternalSessionTests;

public record ExternalSessionWrongEqualityTestCase(ExternalSession Session1, ExternalSession Session2);

public static class SessionTestData
{
    public static IEnumerable<TestDataRow<ExternalSessionWrongEqualityTestCase>> GetWrongComparisonCases()
    {
        var time = DateTime.UtcNow;

        yield return new(
            new ExternalSessionWrongEqualityTestCase(
                ExternalSession.Create("id1", "url", time).Value,
                ExternalSession.Create("id2", "url", time).Value
                ),
            DisplayName: "Sessions with different Id"
        );

        yield return new(
            new ExternalSessionWrongEqualityTestCase(
                ExternalSession.Create("id", "url1", time).Value,
                ExternalSession.Create("id", "url2", time).Value
                ),
            DisplayName: "Sessions with different Url"
        );

        yield return new(
            new ExternalSessionWrongEqualityTestCase(
                ExternalSession.Create("id", "url", DateTime.UtcNow.AddMinutes(10)).Value,
                ExternalSession.Create("id", "url", DateTime.UtcNow.AddMinutes(20)).Value
                ),
            DisplayName: "Sessions with different ExpiresAtUtc"
        );
    }
}

public class ExternalSessionEqualityTests
{
    [Test]
    public async Task Equals_WithSameValues_ShouldBeTrue()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id","url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1).IsEqualTo(s2);
    }

    [Test]
    [MethodDataSource(typeof(SessionTestData), nameof(SessionTestData.GetWrongComparisonCases))]
    public async Task Equals_WithDifferentValues_ShouldBeFalse(ExternalSessionWrongEqualityTestCase testCase)
    {
        await Assert.That(testCase.Session1).IsNotEqualTo(testCase.Session2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeTrue()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1 == s2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var time1 = DateTime.UtcNow.AddMinutes(10);
        var time2 = DateTime.UtcNow.AddMinutes(20);

        var s1 = ExternalSession.Create("id1", "url1", time1).Value;
        var s2 = ExternalSession.Create("id2", "url2", time2).Value;

        await Assert.That(s1 == s2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1 != s2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var time1 = DateTime.UtcNow.AddMinutes(10);
        var time2 = DateTime.UtcNow.AddMinutes(20);

        var s1 = ExternalSession.Create("id1", "url1", time1).Value;
        var s2 = ExternalSession.Create("id2", "url2", time2).Value;

        await Assert.That(s1 != s2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1.GetHashCode()).IsEqualTo(s2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_ForDifferentObjects_ShouldBeDifferent()
    {
        var time1 = DateTime.UtcNow.AddMinutes(10);
        var time2 = DateTime.UtcNow.AddMinutes(20);

        var s1 = ExternalSession.Create("id1", "url1", time1).Value;
        var s2 = ExternalSession.Create("id2", "url2", time2).Value;

        await Assert.That(s1.GetHashCode()).IsNotEqualTo(s2.GetHashCode());
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnDifferentInstances()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(ReferenceEquals(s1, s2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeTrue()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s.Equals("not a session")).IsFalse();
    }
}
```

---

#### IsExpired

```cs title="ExternalSessionCreateTests.cs"
namespace LegacyLego.Domain.Tests.ExternalSessionTests;

public class ExternalSessionIsExpiredTests
{
    [Test]
    public async Task IsExpired_WithLowerUtc_ShouldReturnFalse()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMinutes(-30);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;
        
        await Assert.That(session.IsExpired(time)).IsFalse();
    }

    [Test]
    public async Task IsExpired_WithMinimalLowerUtc_ShouldReturnFalse()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMicroseconds(-1);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(time)).IsFalse();
    }

    [Test]
    public async Task IsExpired_WithSameValues_ShouldReturnTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(expiresAt)).IsTrue();
    }

    [Test]
    public async Task IsExpired_WithBiggerUtc_ShouldReturnTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMinutes(30);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(time)).IsTrue();
    }

    [Test]
    public async Task IsExpired_WithMinimalBiggerUtc_ShouldReturnTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMicroseconds(1);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(time)).IsTrue();
    }

    [Test]
    public async Task IsExpired_WithLocalDateTimeKind_ShouldThrowInvariantViolationExceptionWithIsExpiredCompresionParameterIsNotUtcCode()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var local = DateTime.Now; 

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        var action = () => session.IsExpired(local);

        var exception = await Assert.That(action).ThrowsExactly<InvariantViolationException>();
        await Assert.That(exception!.Error.Code).EqualTo(ExternalSessionExceptionalErrors.IsExpiredCompressionParameterIsNotUtcCode);
    }

    [Test]
    public async Task IsExpired_WithUnspecifiedDateTimeKind_ShouldThrowInvariantViolationExceptionWithIsExpiredCompressionParameterIsNotUtcCode()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var unspecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        var action = () => session.IsExpired(unspecified);

        var exception = await Assert.That(action).ThrowsExactly<InvariantViolationException>();
        await Assert.That(exception!.Error.Code).EqualTo(ExternalSessionExceptionalErrors.IsExpiredCompressionParameterIsNotUtcCode);
    }

    [Test]
    public async Task IsExpired_ExpiresAtUtcShouldStayImmutable()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMinutes(30);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.ExpiresAtUtc).IsEquivalentTo(expiresAt);

        session.IsExpired(time); 

        await Assert.That(session.ExpiresAtUtc).IsEquivalentTo(expiresAt);
    }
}
```

---

### OrderItemTests

#### Create

```cs title="OrderItemCreateTests.cs"
namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldPreserveCurrency()
    {
        var guid = Guid.NewGuid();
        var title = "New Item";
        var quantity = 3;
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(title, quantity, guid, p);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value)
            .Member(x => x.ProductId, m => m.IsEqualTo(guid))
            .And.Member(x => x.Title, m => m.IsEqualTo(title))
            .And.Member(x => x.Quantity, m => m.IsEqualTo(quantity))
            .And.Member(x => x.UnitPrice, m => m.IsEqualTo(p));
    }

    [Test]
    public async Task Create_WithUntrimmedTitle_ShouldReturnSuccess()
    {
        var untrimmed = "    untrimmed     ";
        var trimmed = untrimmed.Trim();
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(untrimmed, 3, Guid.NewGuid(), p);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value.Title).IsEqualTo(trimmed);
    }

    [Test]
    public async Task Create_WithNullTitle_ShouldReturnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var action = () => OrderItem.Create(null!, 3, Guid.NewGuid(), p);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithWhiteTitle_ShouldReturnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var action = () => OrderItem.Create("", 3, Guid.NewGuid(), p);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithEmptyGuid_ShouldReturnFailureWithProductIDGuidInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 3, Guid.Empty, p);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderItemErrors.ProductIDGuidInvalidCode);
    }

    [Test]
    [Arguments(0)]
    [Arguments(-10)]
    public async Task Create_InvalidQuantity_ShouldReturnFailureWithQuantityBelowOne(int quantity)
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", quantity, Guid.NewGuid(), p);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderItemErrors.QuantityBelowOneCode);
    }

    [Test]
    public async Task Create_WithMinimalValidQuantity_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 1, Guid.NewGuid(), p);

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithNullUnitPrice_ShouldThrowArgumentNullException()
    {
        var action = () => OrderItem.Create("New Item", 3, Guid.NewGuid(), null!);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithZeroPrice_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 1, Guid.NewGuid(), p.MultiplyByQuantity(0));

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p).Value;
        var item2 = OrderItem.Create("New Item", 1, guid, p).Value;

        await Assert.That(item1).IsEqualTo(item2);
        await Assert.That(ReferenceEquals(item1, item2)).IsFalse();
    }

    [Test]
    public async Task Create_EqualObjects_ShouldHaveSameHashCode()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p).Value;
        var item2 = OrderItem.Create("New Item", 1, guid, p).Value;

        await Assert.That(item1.GetHashCode()).IsEqualTo(item2.GetHashCode());
    }
}
```

---

#### Equality

```cs title="OrderItemEqualityTests.cs"
namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemEqualityTests
{
    [Test]
    public async Task Equals_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;


        await Assert.That(item1).IsEqualTo(item2);
        await Assert.That(item1).IsNotSameReferenceAs(item2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeTrue()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;

        await Assert.That(item1 == item2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        var item1 = OrderItem.Create("New Item", 1, Guid.NewGuid(), p1).Value;
        var item2 = OrderItem.Create("New Item2", 2, Guid.NewGuid(), p2).Value;

        await Assert.That(item1 == item2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;

        await Assert.That(item1 != item2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        var item1 = OrderItem.Create("New Item", 1, Guid.NewGuid(), p1).Value;
        var item2 = OrderItem.Create("New Item2", 2, Guid.NewGuid(), p2).Value;

        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithSameParameters_ShouldNotBeSameReference()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;

        await Assert.That(item1).IsNotSameReferenceAs(item2);
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeTrue()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create( "New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item.Equals("not a order item")).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithBothNull_ShouldBeTrue()
    {
        OrderItem? a = null;
        OrderItem? b = null;

        await Assert.That(a == b).IsTrue();
    }

    [Test]
    public async Task NotEqualsOperator_WithBothNull_ShouldBeFalse()
    {
        OrderItem? a = null;
        OrderItem? b = null;

        await Assert.That(a != b).IsFalse();
    }

    [Test]
    public async Task Equals_WithSameReference_ShouldBeTrue()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item.Equals(item)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentTitle_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p1).Value;

        var item2 = OrderItem.Create("New Item2", 1, guid, p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentQuantity_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p1).Value;
        var item2 = OrderItem.Create("New Item", 2, guid, p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentProductId_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create("New Item",1,Guid.NewGuid(),p1).Value;
        var item2 = OrderItem.Create("New Item", 1,Guid.NewGuid(),p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentUnitPrice_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item",1,guid,p1).Value;
        var item2 = OrderItem.Create("New Item",1,guid,p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }
}
```

---

#### GetTotalPriceTests

```cs title="OrderItemGetTotalPriceTests.cs"
namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemGetTotalPriceTests
{
    [Test]
    public async Task GetTotalPrice_WithNormalizedPrice_ShouldEqualsExpected()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var expectedPrice = Price.Create(30m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task GetTotalPrice_CurrencyShouldStayConsistent()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var expectedPrice = Price.Create(30m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total.Currency).IsEqualTo(item.UnitPrice.Currency);
    }

    [Test]
    public async Task GetTotalPrice_WithUnnormalizedPrice_ShouldEqualsExpected()
    {
        // gonna be normalized here at first to 10.56
        var p = Price.Create(10.5552m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var expectedPrice = Price.Create(31.68m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task GetTotalPrice_UnitPriceSholdStayConsistent()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        item.GetTotalPrice();

        await Assert.That(item.UnitPrice).IsEqualTo(p);
        await Assert.That(item.UnitPrice).IsSameReferenceAs(p);
    }

    [Test]
    public async Task GetTotalPrice_ItemImmutability()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var originalItem = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var item = originalItem;

        item.GetTotalPrice();

        await Assert.That(item).IsEqualTo(originalItem);
    }

    [Test]
    public async Task GetTotalPrice_WithQuantityOne_ShouldReturnUnitPrice()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 1, Guid.NewGuid(), p).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).IsEqualTo(p);
    }
}
```

---

### OrderPaymentTests

#### AttachSession

```cs title="OrderPaymentAttachSessionTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentAttachSessionTests
{
    private static readonly Price DefaultPrice = PriceDataFactory.CreatePrice();

    [Test]
    public async Task Attach_WhenStatusIsPending_ShouldReturnSuccess()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;
        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
    }

    [Test]
    public async Task Attach_WhenStatusIsSucceeded_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: "transactionId");
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    [Test]
    public async Task Attach_WhenStatusIsFailed_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var failure = payment.MarkAsFailed();
        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    [Test]
    public async Task Attach_WhenStatusIsRefundRequested_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment(txId: "transactionId");
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    [Test]
    public async Task Attach_WhenStatusIsRefunded_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var refund = payment.MarkAsRefunded("transactionId");
        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    #region Guard Clauses

    [Test]
    public async Task Attach_WithNullNewSession_ShouldThrowArgumentNullException()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;

        var action = () => { payment.AttachSession(null!, now); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Attach_WithLocalNowTime_ShouldResultFailureWithNowTimeWasNotUtcForAttachSessionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now).Value;

        var attach = payment.AttachSession(session, DateTime.Now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.NowTimeWasNotUtcForAttachSessionCode);
    }

    [Test]
    public async Task Attach_WithUnspecifiedNowTime_ShouldResultFailureWithNowTimeWasNotUtcForAttachSessionCode()
    {
        var now = DateTime.UtcNow;
        var nowUnspecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var attach = payment.AttachSession(session, nowUnspecified);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.NowTimeWasNotUtcForAttachSessionCode);
    }

    #endregion

    [Test]
    public async Task Attach_WhenSessionStillActive_ShouldReturnFailureWithEnsuredSessionIsNotExpiredTransitionFailureCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var firstAttach = payment.AttachSession(session, now);
        var secondAttach = payment.AttachSession(session, now);

        await Assert.That(secondAttach.IsFailure).IsTrue();
        await Assert.That(secondAttach.Error.Code).IsEqualTo(OrderPaymentErrors.EnsuredSessionIsNotExpiredTransitionFailureCode);
    }

    [Test]
    public async Task Attach_WhenSessionExpired_ShouldSucceed()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(10)).Value;

        var firstAttach = payment.AttachSession(session, now.AddHours(24));
        var secondAttach = payment.AttachSession(session, now.AddHours(24));

        await Assert.That(secondAttach.IsSuccess).IsTrue();
    }
}
```

---

#### Create

```cs title="OrderPaymentCreateTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentCreateTests
{
    private static readonly Price DefaultPrice = PriceDataFactory.CreatePrice();

    [Test]
    public async Task Create_WithValidValues_ShouldResultSuccess()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, DefaultPrice, now);

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithValidValues_ShouldBeSameValues()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;
        var price = PriceDataFactory.CreatePrice(1500m, "RUB");

        var r = OrderPayment.Create(id, price, now);

        await Assert.That(r.Value)
            .Member(v => v.OrderId, m => m.IsEqualTo(id))
            .And.Member(v => v.ExpectedAmount, m => m.IsEqualTo(price))
            .And.Member(v => v.CreatedAtUtc, m => m.IsEqualTo(now))
            .And.Member(v => v.Id, m => m.IsNotNull());
    }

    [Test]
    public async Task Create_WithValidValues_ShouldReturnOrderPaymentInPendingStatus()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, DefaultPrice, now);

        await Assert.That(r.Value.Status).IsEqualTo(PaymentStatus.Pending);
    }

    [Test]
    public async Task Create_WithValidValues_ShouldRaiseOrderPaymentCreatedDomainEvent()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, DefaultPrice, now);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value.Status).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(r.Value.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentCreated));
    }

    #region Guard Clauses

    [Test]
    public async Task Create_WithNullOrderId_ShouldThrowArgumentNullException()
    {
        var action = () => { _ = OrderPayment.Create(null!, DefaultPrice, DateTime.UtcNow); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithNullExpectedAmount_ShouldThrowArgumentNullException()
    {
        var action = () => { _ = OrderPayment.Create(OrderId.New(), null!, DateTime.UtcNow); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithDefaultDateTime_ShouldThrowArgumentException()
    {
        var action = () => { _ = OrderPayment.Create(OrderId.New(), DefaultPrice, default); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithLocalCreatedAtTime_ShouldResultFailureWithCreationTimeWasNotUtcCode()
    {
        var id = OrderId.New();
        var nowLocal = DateTime.Now;

        var r = OrderPayment.Create(id, DefaultPrice, nowLocal);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderPaymentErrors.CreationTimeWasNotUtcCode);
    }

    [Test]
    public async Task Create_WithUnspecifiedCreatedAtTime_ShouldResultFailureWithCreationTimeWasNotUtcCode()
    {
        var id = OrderId.New();
        var nowUnspecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var r = OrderPayment.Create(id, DefaultPrice, nowUnspecified);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderPaymentErrors.CreationTimeWasNotUtcCode);
    }

    #endregion
}
```

---

#### StateTransitions

##### MarkAsFailed

```cs title="OrderPaymentMarkAsFailedTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsFailedTests
{
    [Test]
    public async Task MarkAsFailed_FromPending_ShouldSucceedAndChangeStatus()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var statusBefore = payment.Status;

        var failure = payment.MarkAsFailed();
        var statusAfter = payment.Status;

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(statusAfter).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    public async Task MarkAsFailed_ShouldRaiseOrderPaymentFailedDomainEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentFailed));
    }

    [Test]
    public async Task MarkAsFailed_WithFailedStatus_ShouldResultFailure()
    {
        var payment = OrderPaymentDataFactory.CreateFailedOrderPayment();

        var secondFailure = payment.MarkAsFailed();

        await Assert.That(secondFailure.IsFailure).IsTrue();
        await Assert.That(secondFailure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    public async Task MarkAsFailed_WithSucceededStatus_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    public async Task MarkAsFailed_WithRefundRequestedStatus_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    public async Task MarkAsFailed_WithRefundedStatus_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundedOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task MarkAsFailed_ValuesAreSameAfterFailure()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var id = payment.Id;
        var orderId = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderId))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    public async Task MarkAsFailed_ExternalSessionIsSameAfterFailure()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var session = ExternalSession.Create("id", "url", DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }
}
```

---

##### MarkAsRefunded

```cs title="OrderPaymentMarkAsRefundedTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsRefundedTests
{
    private const string DefaultTxId = "tx-123";
    private const string DifferentTxId = "tx-different-456";

    #region Happy Paths (Valid Transitions)

    [Test]
    public async Task MarkAsRefunded_FromPending_ShouldSucceedAndChangeStatus()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var statusBefore = payment.Status;

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task MarkAsRefunded_ShouldRaiseOrderPaymentRefundedDomainEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
    }

    [Test]
    public async Task MarkAsRefunded_AfterSucceededWithSameTransactionId_ShouldResultSuccess()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task MarkAsRefunded_AfterRefundRequestedWithSameTransactionId_ShouldResultSuccess()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    #endregion

    #region Invalid Transitions & TransactionId Mismatch

    [Test]
    public async Task MarkAsRefunded_AfterSucceededWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DifferentTxId);

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    public async Task MarkAsRefunded_AfterRefundRequestedWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DifferentTxId);

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    public async Task MarkAsRefunded_WhenStatusIsFailed_ShouldResultFailureWithStatusTransitionFailureError()
    {
        // Arrange
        var payment = OrderPaymentDataFactory.CreateFailedOrderPayment();

        // Act
        var refund = payment.MarkAsRefunded(DefaultTxId);

        // Assert
        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    public async Task MarkAsRefunded_WhenStatusIsRefunded_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundedOrderPayment(txId: DefaultTxId);

        var secondRefund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(secondRefund.IsFailure).IsTrue();
        await Assert.That(secondRefund.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    #endregion

    #region Guard Clauses

    [Test]
    public async Task MarkAsRefunded_WithTransactionIdNull_ShouldThrowArgumentNullException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.MarkAsRefunded(null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task MarkAsRefunded_WithTransactionIdEmpty_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.MarkAsRefunded(string.Empty); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task MarkAsRefunded_WithTransactionIdWhiteSpace_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.MarkAsRefunded("   "); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    #endregion

    #region State Invariance Checks

    [Test]
    public async Task MarkAsRefunded_ValuesAreSameAfterRefund()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var id = payment.Id;
        var orderId = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderId))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    public async Task MarkAsRefunded_TransactionIdIsSameAfterRefund()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.TransactionId).IsEqualTo(DefaultTxId);
    }

    [Test]
    public async Task MarkAsRefunded_ExternalSessionIsSameAfterRefund()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var session = ExternalSession.Create("session-id", "url", DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }

    #endregion
}
```

---

##### RegisterPaymentReceipt

```cs title="OrderPaymentRegisterPaymentReceiptTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentRegisterPaymentReceiptTests
{
    private const string DefaultTxId = "tx-123";
    private const string DifferentTxId = "tx-different-456";

    #region Happy Paths (Valid Transitions)

    [Test]
    public async Task RegisterPaymentReceipt_WhenAmountMatches_ShouldSetStatusToSucceededAndRaiseSucceededEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(100m);
        var exactPrice = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, exactPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
        await Assert.That(payment.ActualAmount).IsEqualTo(exactPrice);
        await Assert.That(payment.TransactionId).IsEqualTo(DefaultTxId);
        await Assert.That(payment.DomainEvents)
            .HasSingleItem(e => e.GetType() == typeof(OrderPaymentSucceeded));
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenAmountMismatches_ShouldSetStatusToRefundRequestedAndRaiseMismatchedEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(amount: 100m);
        var mismatchedPrice = PriceDataFactory.CreatePrice(150m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, mismatchedPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
        await Assert.That(payment.ActualAmount).IsEqualTo(mismatchedPrice);
        await Assert.That(payment.TransactionId).IsEqualTo(DefaultTxId);
        await Assert.That(payment.DomainEvents)
            .HasSingleItem(e => e.GetType() == typeof(OrderPaymentAmountMismatchedAndRefundRequested));
    }

    #endregion

    #region Invalid Status Transitions

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsSucceeded_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsRefundRequested_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsRefunded_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateRefundedOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsFailed_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateFailedOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    #endregion

    #region Transaction ID Mismatch Checks

    [Test]
    public async Task RegisterPaymentReceipt_WhenTransactionIdAlreadySetAndDifferent_ShouldReturnWrongTransactionIdExchangeError()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: DefaultTxId);
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DifferentTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
    }

    #endregion

    #region Guard Clauses

    [Test]
    public async Task RegisterPaymentReceipt_WithNullTransactionId_ShouldThrowArgumentNullException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var action = () => { _ = payment.RegisterPaymentReceipt(null!, price); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task RegisterPaymentReceipt_WithEmptyTransactionId_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var action = () => { _ = payment.RegisterPaymentReceipt(string.Empty, price); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task RegisterPaymentReceipt_WithWhiteSpaceTransactionId_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var action = () => { _ = payment.RegisterPaymentReceipt("   ", price); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task RegisterPaymentReceipt_WithNullActualAmount_ShouldThrowArgumentNullException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.RegisterPaymentReceipt(DefaultTxId, null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    #endregion

    #region State Invariance Checks

    [Test]
    public async Task RegisterPaymentReceipt_PreservesExistingProperties()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(100m);
        var id = payment.Id;
        var orderId = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;
        var exactPrice = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, exactPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderId))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    public async Task RegisterPaymentReceipt_ExternalSessionIsSameAfterRegistration()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(100m);
        var session = ExternalSession.Create("session-1", "url", DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);
        var exactPrice = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, exactPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }

    #endregion
}
```

---

### OrderTests

#### Create

```cs title="OrderCreateTests.cs"
namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderCreateTests
{
    private static OrderAddress DefaultAddress =>
    OrderAddress.Create("US", "Berlin", "New York", "90210").Value;

    private static List<OrderItem> DefaultItems => new List<OrderItem>()
    {
            OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m,Currency.Usd).Value).Value,
            OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Usd).Value).Value,
            OrderItem.Create("New Item3", 3, Guid.NewGuid(), Price.Create(300m, Currency.Usd).Value).Value
    };

    [Test]
    public async Task Create_WithValidValues_ShouldResultSuccess()
    {
        var clientId = Guid.NewGuid();
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(items)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value)
            .Member(o => o.Address, m => m.IsEqualTo(DefaultAddress))
            .And.Member(o => o.ClientId, m => m.EqualTo(clientId))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(items));
    }

    [Test]
    public async Task Create_OrderItemsListShouldNotBeSameReference()
    {
        var clientId = Guid.NewGuid();
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(items)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Items).IsNotSameReferenceAs(items);
    }

    [Test]
    public async Task Create_OrderItemsListShouldNotMutate()
    {
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        items.Clear();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Items).IsNotEquivalentTo(items);
    }

    #region Guard Clauses

    [Test]
    public async Task Create_WithAddressNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithNullAddress()
            .WithItems(DefaultItems)
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithOrderItemsNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNullItems()
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithOrderItemsListContainsNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .AddNullItem()
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    #endregion

    #region Validation Invariants

    [Test]
    public async Task Create_WithClientIdEmpty_ShouldThrowArgumentException()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithEmptyClientId()
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ClientIdGuidInvalidCode);
    }

    [Test]
    public async Task Create_WithOrderItemsEmptyList_ShouldResultFailure()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsCountInvalidCode);
    }

    [Test]
    public async Task Create_WithOrderItemsCurrenciesMismatch_ShouldResultFailure()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Rub).Value).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsCurrenciesMismatchCode);
    }

    [Test]
    public async Task Create_WithOrderItemsTotalPriceZero_ShouldResultFailureWithItemsTotalBelowZeroError()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsTotalBelowZeroCode);
    }

    [Test]
    public async Task Create_WithOrderItemsZeroPrices_ShouldResultSuccess()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
    }

    #endregion

    [Test]
    public async Task Create_WithValidValues_ShouldReturnOrderWithPendingPaymentStatus()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Status).IsEqualTo(Enums.OrderStatus.PendingPayment);
    }

    [Test]
    public async Task Create_CreationDateUtcShouldNotBeDefoult()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.CreationDateUtc).IsNotDefault();
    }

    [Test]
    public async Task Create_WithValidValues_ShouldRiseOrderCreatedDomainEvent()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderCreated));
    }

    [Test]
    public async Task Create_TotalPriceShouldBeEquivalentAsExpected()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item3", 3, Guid.NewGuid(), Price.Create(300m, Currency.Usd).Value).Value)
            .WithClientId(clientId)
            .BuildResult();

        var expected = 1400m;

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.TotalPrice.Sum).IsEqualTo(expected);
    }
}
```

---

#### StateTransitions

##### Cancel

```cs title="OrderCancelTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderCancelTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var cancel = order.Cancel();
        var statusAfter = order.Status;

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_ShouldRiseOrderCancelledDomainEvent(Order order)
    {
        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterExpire_ShouldResultSuccess(Order order)
    {
        var expire = order.Expire();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderExpired))
            .And.Contains(e => e.GetType() == typeof(OrderCanceled));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();
        order.ClearDomainEvents();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsFailure).IsTrue();
        await Assert.That(cancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var paiment = order.Pay();

        var rafund = order.Refund();
        order.ClearDomainEvents();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsFailure).IsTrue();
        await Assert.That(cancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstCancel = order.Cancel();
        order.ClearDomainEvents();
        var secondCancel = order.Cancel();

        await Assert.That(firstCancel.IsSuccess).IsTrue();
        await Assert.That(secondCancel.IsFailure).IsTrue();
        await Assert.That(secondCancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_ValuesAreSameAfterCancel(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

##### Expire

```cs title="OrderExpireTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderExpireTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var expire = order.Expire();
        var statusAfter = order.Status;

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ShouldRiseOrderExpiredDomainEvent(Order order)
    {
        var expire = order.Expire();

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderExpired));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterExpire_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstExpire = order.Expire();
        order.ClearDomainEvents();
        var secondExpire = order.Expire();

        await Assert.That(firstExpire.IsSuccess).IsTrue();
        await Assert.That(secondExpire.IsFailure).IsTrue();
        await Assert.That(secondExpire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderExpired));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();
        order.ClearDomainEvents();

        var expire = order.Expire();

        await Assert.That(expire.IsFailure).IsTrue();
        await Assert.That(expire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderExpired));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();

        var refund = order.Refund();
        order.ClearDomainEvents();

        var expire = order.Expire();

        await Assert.That(expire.IsFailure).IsTrue();
        await Assert.That(expire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderExpired));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var expire = order.Expire();

        await Assert.That(expire.IsFailure).IsTrue();
        await Assert.That(expire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ValuesAreSameAfterExpire(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var expire = order.Expire();

        await Assert.That(expire.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

##### Pay

```cs title="OrderPayTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderPayTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var payment = order.Pay();
        var statusAfter = order.Status;

        await Assert.That(payment.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ShouldRiseOrderPaidDomainEvent(Order order)
    {
        var payment = order.Pay();

        await Assert.That(payment.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstPay = order.Pay();
        order.ClearDomainEvents();
        var secondPay = order.Pay();

        await Assert.That(firstPay.IsSuccess).IsTrue();
        await Assert.That(secondPay.IsFailure).IsTrue();
        await Assert.That(secondPay.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterExpire_ShouldResultSuccess(Order order)
    {
        var expire = order.Expire();

        var paiment = order.Pay();

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(paiment.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderExpired))
            .And.Contains(e => e.GetType() == typeof(OrderPaid));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstPayment = order.Pay();

        var refund = order.Refund();
        order.ClearDomainEvents();

        var secondPayment = order.Pay();

        await Assert.That(firstPayment.IsSuccess).IsTrue();
        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(secondPayment.IsFailure).IsTrue();
        await Assert.That(secondPayment.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var payment = order.Pay();

        await Assert.That(cancell.IsSuccess).IsTrue();
        await Assert.That(payment.IsFailure).IsTrue();
        await Assert.That(payment.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ValuesAreSameAfterPayment(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var payment = order.Pay();

        await Assert.That(payment.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

##### Refund

```cs title="OrderRefundTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderRefundTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_FromPending_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var refund = order.Refund();
        var statusAfter = order.Status;

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.PendingPayment);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterExpire_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var expire = order.Expire();
        order.ClearDomainEvents();
        var refund = order.Refund();

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterPay_ShouldResultSuccess(Order order)
    {
        var payment = order.Pay();

        var statusBefore = order.Status;

        var refund = order.Refund();

        var statusAfter = order.Status;

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderPaid))
            .And.Contains(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(statusBefore).IsEqualTo(OrderStatus.Paid);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();

        var firstRefund = order.Refund();
        order.ClearDomainEvents();

        var secondRefund = order.Refund();

        await Assert.That(firstRefund.IsSuccess).IsTrue();
        await Assert.That(secondRefund.IsFailure).IsTrue();

        await Assert.That(secondRefund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(order.Status).IsEqualTo(OrderStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var refund = order.Refund();

        await Assert.That(refund.IsFailure).IsTrue();

        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_ValuesAreSameAfterRefund(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var payment = order.Pay();
        var refund = order.Refund();

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

#### TotalPrice

```cs title="OrderTotalPriceTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderTotalPriceTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task TotalPrice_WhenPending_ShouldEqualSumOfItems(Order order)
    {
        var expected = order.Items
            .Select(i => i.GetTotalPrice())
            .Aggregate((a, b) => a.Plus(b));

        await Assert.That(order.TotalPrice).IsEqualTo(expected);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task TotalPrice_AfterPay_ShouldRemainSame(Order order)
    {
        var before = order.TotalPrice;

        order.Pay();

        var after = order.TotalPrice;

        await Assert.That(after).IsEqualTo(before);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task TotalPrice_WhenExpired_ShouldStillBeCalculated(Order order)
    {
        var before = order.TotalPrice;

        order.Expire();

        var after = order.TotalPrice;

        await Assert.That(after).IsEqualTo(before);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ShouldSetFrozenTotalPrice_Implicitly(Order order)
    {
        order.Pay();

        // если _frozenTotalPrice не установлен,
        // этот вызов бросит InvalidDomainStateException
        var total = order.TotalPrice;

        await Assert.That(total).IsNotNull();
    }
}
```

---

### PriceTests

#### Create

```cs title="PriceCreateTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceCreateTests
{
    [Test]
    public async Task Create_ShouldPreserveCurrency()
    {
        var result = Price.Create(10m, Currency.Eur);

        await Assert.That(result.Value.Currency).IsEqualTo(Currency.Eur);
    }

    [Test]
    public async Task Create_WithNullCurrency_ShouldThrowArgumentNullException()
    {
        await Assert.That(() => Price.Create(100m, null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithValidParameters_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(100m, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(100m);
        await Assert.That(result.Value.Currency).IsEqualTo(currency);
    }

    [Test]
    public async Task Create_WithNegativeSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(-100m, currency);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithZeroSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(0m, currency);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithZeroNormalizedSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(0.0004m, currency);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithMaximalDecimalSum_ShoulReturnSuccess()
    {
        var currency = Currency.Usd;
        var result = Price.Create(decimal.MaxValue, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(decimal.MaxValue);
        await Assert.That(result.Value.Currency).IsEqualTo(currency);
    }

    [Test]
    public async Task Create_ShouldNormalizeSumAccordingToCurrencyScale()
    {
        var currency = Currency.Usd;

        // 10.555 → 10.56 (banker's rounding)
        var result = Price.Create(10.555m, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(10.56m);
    }

    [Test]
    public async Task Create_WithMinimalPositiveValue_ShouldReturnSuccess()
    {
        var currency = Currency.Usd;

        var result = Price.Create(0.01m, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(0.01m);
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var currency = Currency.Usd;

        var p1 = Price.Create(10m, currency).Value;
        var p2 = Price.Create(10m, currency).Value;

        await Assert.That(p1).IsEqualTo(p2);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task EqualObjects_ShouldHaveSameHashCode()
    {
        var currency = Currency.Usd;

        var p1 = Price.Create(10m, currency).Value;
        var p2 = Price.Create(10m, currency).Value;

        await Assert.That(p1.GetHashCode()).IsEqualTo(p2.GetHashCode());
    }
}
```

---

#### Equality

```cs title="PriceEqualityTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceEqualityTests
{
    [Test]
    public async Task Equals_WithSamePrice_ShouldBeTrue()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsEqualTo(p2);
    }

    [Test]
    public async Task Equals_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Rub).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsNotEqualTo(p2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1 == p2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1 == p2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1 != p2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1 != p2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1.GetHashCode()).IsEqualTo(p2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_ForDifferentObjects_ShouldBeDifferent()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1.GetHashCode()).IsNotEqualTo(p2.GetHashCode());
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnDifferentInstances()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price.Equals("not a price")).IsFalse();
    }

    [Test]
    public async Task Equals_WithNormalizedValues_ShouldBeTrue()
    {
        var p1 = Price.Create(100.000m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsEqualTo(p2);
    }
}
```

---

#### MultiplyByQuantity

```cs title="PriceMultiplyByQuantityTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceMultiplyByQuantityTests
{
    [Test]
    public async Task MultiplyByQuantity_WithValidFactor_ShouldReturnExpectedPrice()
    {
        int factor = 3;
        decimal initSum = 100m;
        decimal expectedSum = initSum * factor;

        var p1 = Price.Create(initSum,Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(3);

        await Assert.That(p2.Sum).IsEqualTo(expectedSum);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_WithBelowZeroFactor_ShouldThrowInvariantViolationException()
    {
        int factor = -2;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.MultiplyByQuantity(factor))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code,code => code.EqualTo(PriceExceptionalErrors.MultiplyBelowZeroErrorCode));
    }

    [Test]
    public async Task MultiplyByQuantity_WithZeroFactor_ShouldReturnValidPrice()
    {
        int factor = 0;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(factor);

        await Assert.That(p2.Sum).IsEqualTo(0);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_WithMinimalValidFactor_ShouldReturnValidPrice()
    {
        int factor = 1;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(factor);

        await Assert.That(p2.Sum).IsEqualTo(initSum);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_DecimalOverflow_ShouldThrowInvariantViolationException()
    {
        int factor = 2;
        decimal initSum = decimal.MaxValue;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.MultiplyByQuantity(factor))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code, code => code.EqualTo(PriceExceptionalErrors.DecimalSumOverflowErrorCode));
    }

    [Test]
    public async Task MultiplyByQuantity_ShouldNormalizeResult()
    {
        int factor = 3;
        decimal initSum = 10.555m;

        var p = Price.Create(initSum, Currency.Usd).Value;

        var result = p.MultiplyByQuantity(factor);

        await Assert.That(result.Sum).IsEqualTo(31.68m);
    }

    [Test]
    public async Task MultiplyByQuantity_ShouldNotModifyOriginalPrice()
    {
        int factor = 3;
        decimal initSum = 100m;

        var p = Price.Create(initSum, Currency.Usd).Value;

        var result = p.MultiplyByQuantity(factor);

        await Assert.That(p).IsEqualTo(Price.Create(initSum, Currency.Usd).Value);
    }
}
```

---

#### Plus

```cs title="PricePlusTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PricePlusTests
{
    [Test]
    public async Task Plus_WithValidPrices_ShouldReturnExpectedPrice()
    {
        decimal initSum = 100m;
        decimal expectedSum = 200m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(pSum).IsEqualTo(expectedPrice);

        await Assert.That(ReferenceEquals(pSum, p1)).IsFalse();
        await Assert.That(ReferenceEquals(pSum, p2)).IsFalse();
    }

    [Test]
    public async Task Plus_WithUnnormalizedPrices_ShouldReturnExpectedPrice()
    {
        decimal initSum = 10.5551m;
        decimal expectedSum = 21.12m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(pSum).IsEqualTo(expectedPrice);

        await Assert.That(ReferenceEquals(pSum, p1)).IsFalse();
        await Assert.That(ReferenceEquals(pSum, p2)).IsFalse();
    }

    [Test]
    public async Task Plus_WithDifferentCurrencies_ShouldThrowInvariantViolationExceptionWithCurrencyMismatchCode()
    {
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Rub).Value;

        var exception = await Assert.That(() => p1.Plus(p2))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code, code => code.EqualTo(PriceExceptionalErrors.CurrencyMismatchErrorCode));
    }

    [Test]
    public async Task Plus_WithSumDecimalOverflow_ShouldThrowInvariantViolationExceptionWithDecimalSumOverflowCode()
    {
        decimal initSum = decimal.MaxValue;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.Plus(p2))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(err => err.Code, code => code.EqualTo(PriceExceptionalErrors.DecimalSumOverflowErrorCode));
    }

    [Test]
    public async Task Plus_WithBoundaryValues_ShouldNotOverflow()
    {
        var p1 = Price.Create(decimal.MaxValue - 1, Currency.Usd).Value;
        var p2 = Price.Create(1m, Currency.Usd).Value;

        var result = p1.Plus(p2);

        await Assert.That(result.Sum).IsEqualTo(decimal.MaxValue);
    }

    [Test]
    public async Task Plus_ShouldNotModifyOriginalPrices()
    {
        decimal initSum = 100m;
        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(p1.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);

        await Assert.That(p1.Sum).IsEqualTo(initSum);
        await Assert.That(p2.Sum).IsEqualTo(initSum);
    }

    [Test]
    public async Task Plus_WithMultiplyByQuantityAsParametersNormalized_ShouldReturnExpectedPrice()
    {
        decimal initSum = 100m;
        decimal expectedSum = 600m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.MultiplyByQuantity(2).Plus(p2.MultiplyByQuantity(4));

        await Assert.That(pSum).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task Plus_WithMultiplyByQuantityAsParametersZero_ShouldReturnZeroPrice()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        var pSum = p1.MultiplyByQuantity(0).Plus(p2.MultiplyByQuantity(0));

        await Assert.That(pSum.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(pSum.Sum).IsEqualTo(0);
    }

    [Test]
    public async Task Plus_WithZeroPrice_ShouldReturnSamePrice()
    {
        var p = Price.Create(100m, Currency.Usd).Value;
        var zero = p.MultiplyByQuantity(0);

        var result = p.Plus(zero);

        await Assert.That(result).IsEqualTo(p);
    }

    [Test]
    public async Task Plus_ShouldBeCommutative()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        var r1 = p1.Plus(p2);
        var r2 = p2.Plus(p1);

        await Assert.That(r1).IsEqualTo(r2);
    }

    [Test]
    public async Task Plus_ShouldBeAssociative()
    {
        var p1 = Price.Create(10.555m, Currency.Usd).Value;
        var p2 = Price.Create(20.111m, Currency.Usd).Value;
        var p3 = Price.Create(30.333m, Currency.Usd).Value;

        var r1 = p1.Plus(p2).Plus(p3);
        var r2 = p1.Plus(p2.Plus(p3));

        await Assert.That(r1).IsEqualTo(r2);
    }

    [Test]
    public async Task Plus_ShouldBeCommutativeWithZero()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var zero = p1.MultiplyByQuantity(0);

        var r1 = p1.Plus(zero);

        await Assert.That(r1).IsEqualTo(p1);
    }
}
```

---

## LegacyLego.Infrastructure

```xml title="LegacyLego.Infrastructure.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
	<UserSecretsId>4c1ab31b-79ef-45cf-b0db-bef1bd60998f</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Hangfire.AspNetCore" Version="1.8.23" />
    <PackageReference Include="Hangfire.PostgreSql" Version="1.21.1" />
    <PackageReference Include="Microsoft.AspNetCore.WebUtilities" Version="10.0.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.9">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.Caching.StackExchangeRedis" Version="10.0.10" />
    <PackageReference Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Options.DataAnnotations" Version="10.0.9" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="10.0.3" />
    <PackageReference Include="Scrutor" Version="7.0.0" />
    <PackageReference Include="Serilog.Sinks.Seq" Version="9.1.0" />
  </ItemGroup>

	<ItemGroup>
		<ProjectReference Include="..\LegacyLego.Application\LegacyLego.Application.csproj" />
		<ProjectReference Include="..\LegacyLego.Domain\LegacyLego.Domain.csproj" />
	</ItemGroup>

	<ItemGroup>
	  <Folder Include="Migrations\" />
	</ItemGroup>

</Project>
```

---

```cs title="DependencyInjection.cs"
using Hangfire;
using Hangfire.PostgreSql;
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExceptionHandling;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrderDetails;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Infrastructure.BackgroundJobs;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Caching.Decorators.Query.Order;
using LegacyLego.Infrastructure.Caching.Invalidators;
using LegacyLego.Infrastructure.Caching.Services;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Diagnostics;
using LegacyLego.Infrastructure.Logging.Decoretors;
using LegacyLego.Infrastructure.Messaging.Abstractions;
using LegacyLego.Infrastructure.Messaging.Bus;
using LegacyLego.Infrastructure.Messaging.Dispatchers;
using LegacyLego.Infrastructure.Messaging.Publishers;
using LegacyLego.Infrastructure.Options;
using LegacyLego.Infrastructure.Repositories;
using LegacyLego.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Order = LegacyLego.Domain.Aggregates.Order;

namespace LegacyLego.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.AddOptions<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<OutboxBackgroundWorkerOptions>()
            .BindConfiguration(OutboxBackgroundWorkerOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<PaymentProviderOptions>()
            .BindConfiguration(PaymentProviderOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<HangfireOptions>()
            .BindConfiguration(HangfireOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<CacheOptions>()
            .BindConfiguration(CacheOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>()
                              ?? new DatabaseOptions();

        var hangfireOptions = configuration.GetSection(HangfireOptions.SectionName).Get<HangfireOptions>()
                              ?? new HangfireOptions();

        var hangfirePostgreSqlStorageOptions = new PostgreSqlStorageOptions();
        hangfirePostgreSqlStorageOptions.QueuePollInterval = TimeSpan.FromSeconds(hangfireOptions.QueuePollInterval);
        hangfirePostgreSqlStorageOptions.SchemaName = "internal_jobs";

        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)

            .AddClasses(classes => classes.AssignableTo(typeof(IIntegrationEventConsumer<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime())

            .AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(databaseOptions.ConnectionString);
            }, hangfirePostgreSqlStorageOptions))
            .AddHangfireServer(options =>
            {
                options.WorkerCount = hangfireOptions.WorkerCount;
                options.Queues = new[] { HangfireOptions.CommandHangfireQueueName, "default" };
            })

            .AddDbContext<OrderContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseNpgsql(dbOptions.ConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(dbOptions.CommandTimeoutSeconds);

                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: dbOptions.MaxRetryCount,
                        maxRetryDelay: TimeSpan.FromSeconds(dbOptions.MaxRetryDelaySeconds),
                        errorCodesToAdd: null);
                });

                if (dbOptions.EnableSensitiveDataLogging)
                {
                    options.EnableSensitiveDataLogging();
                }

                if (dbOptions.EnableDetailedErrors)
                {
                    options.EnableDetailedErrors();
                }
            })

            .AddScoped<IUnitOfWork, UnitOfWork>()

            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IPaymentRepository, PaymentRepository>()

            .AddSingleton<IIntegrationEventBus, InMemoryIntegrationEventBus>()
            .AddScoped<IIntegrationEventPublisher, LocalIntegrationEventPublisher>()

            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddScoped<IQueryDispatcher, QueryDispatcher>()

            .AddSingleton<IExceptionMapper, InfrastructureExceptionMapper>()

            .AddSingleton(TimeProvider.System)

            .AddScoped<ICommandBackgroundJobService, HangfireCommandBackgroundJobService>()

            .AddScoped<ICursorSerializer, Base64JsonCursorSerializer>()

            .AddHostedService<OutboxBackgroundWorker>();

        services.AddHttpClient<IPaymentProvider, MockPaymentProvider>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<PaymentProviderOptions>>().Value;

            //базовый адрес для ВСЕХ запросов этого клиента
            client.BaseAddress = new Uri(options.ApiBaseUrl);
        });

        if (services.Any(s => s.ServiceType.IsGenericType && s.ServiceType.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
        {
            services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        }

        if (services.Any(s => s.ServiceType.IsGenericType && s.ServiceType.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
        {
            services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingCommandHandlerDecorator<,>));
        }

        #region Настройка кэширования
        services.AddSingleton<ICacheService, RedisCacheService>();

        services.AddScoped<ICacheInvalidator, CacheInvalidator>();

        services.AddScoped<IEntityInvalidator<Order>, OrderEntityInvalidator>();

        services.Decorate<IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>,
            GetOrdersHistoryQueryCachingDecorator>();

        services.Decorate<IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>,
            GetOrderDetailsQueryCachingDecorator>();

        string redisConnectionString = configuration["RedisConnectionString"]!;

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            return ConnectionMultiplexer.Connect(redisConnectionString);
        }); 
        #endregion

        return services;
    }
}
```

---

```cs title="UnitOfWork.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Options;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace LegacyLego.Infrastructure;

public sealed class UnitOfWork: IUnitOfWork
{
    private readonly OrderContext _orderContext;
    private readonly TimeProvider _timeProvider;
    private readonly ICacheInvalidator _cacheInvalidator;

    public UnitOfWork(
        OrderContext orderContext,
        TimeProvider timeProvider,
        ICacheInvalidator cacheInvalidator)
    {
        _orderContext = orderContext;
        _timeProvider = timeProvider;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OutboxingDomainEvents();

        var modifiedEntities = GetModifiedEntities();

        // 3. Сохраняем всё в БД в рамках единой транзакции
        var result = await _orderContext.SaveChangesAsync(cancellationToken);

        // 4. Если запись в БД прошла успешно — запускаем конвейер инвалидации
        if (result > 0 && modifiedEntities.Any())
        {
            await _cacheInvalidator.InvalidateAsync(modifiedEntities, cancellationToken);
        }

        return result;
    }

    private List<object> GetModifiedEntities()
    {
        return _orderContext.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToList();
    }

    private void OutboxingDomainEvents()
    {
        var entitiesWithEvents = GetAllWithDomainEvents();

        var domainEvents = TakeDomainEvents(entitiesWithEvents);

        foreach (var entity in entitiesWithEvents)
            entity.ClearDomainEvents();

        var outboxMessages = ConvertDomainEventsToOutboxMessages(domainEvents);

        _orderContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private List<IHasDomainEvents> GetAllWithDomainEvents()
    {
        return _orderContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .Where(entity => entity.DomainEvents.Any())
            .ToList();
    }

    private List<IDomainEvent> TakeDomainEvents(List<IHasDomainEvents> hasEventsList)
    {
        return hasEventsList
            .SelectMany(entity => entity.DomainEvents)
            .ToList();
    }

    private List<OutboxMessage> ConvertDomainEventsToOutboxMessages(List<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(domainEvent => new OutboxMessage(
            id: Guid.NewGuid(),
            type: domainEvent.GetType().AssemblyQualifiedName ?? domainEvent.GetType().FullName!,
            content: JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), OutboxSerializerOptions.Options),
            occurredOnUtc: _timeProvider.GetUtcNow().UtcDateTime
        )).ToList();
    }
}
```

---

### BackgroundJobs

```cs title="HangfireCommandBackgroundJobService.cs"
using Hangfire;
using Hangfire.States;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Infrastructure.Options;

namespace LegacyLego.Infrastructure.BackgroundJobs;

public sealed class HangfireCommandBackgroundJobService : ICommandBackgroundJobService
{
    private readonly IBackgroundJobClient _jobClient;

    public HangfireCommandBackgroundJobService(IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
    }

    public void Schedule<TResult>(ICommand<TResult> command, TimeSpan delay)
    {
        var methodCall = Hangfire.Common.Job.FromExpression<ICommandDispatcher>(
            dispatcher => dispatcher.DispatchAsync(command, CancellationToken.None), HangfireOptions.CommandHangfireQueueName);

        var state = new ScheduledState(delay);

        _jobClient.Create(methodCall, state);
    }

    public void Schedule(ICommand command, TimeSpan delay)
    {
        var methodCall = Hangfire.Common.Job.FromExpression<ICommandDispatcher>(
            dispatcher => dispatcher.DispatchAsync(command, CancellationToken.None), HangfireOptions.CommandHangfireQueueName);

        var state = new ScheduledState(delay);

        _jobClient.Create(methodCall, state);
    }
}
```

---

```cs title="OutboxBackgroundWorker.cs"
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Converters.JsonConverters;
using LegacyLego.Infrastructure.Options;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LegacyLego.Infrastructure.BackgroundJobs;

public sealed class OutboxBackgroundWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxBackgroundWorker> _logger;

    private readonly IOptionsMonitor<OutboxBackgroundWorkerOptions> _optionsMonitor;

    private TimeSpan Period
    {
        get => TimeSpan.FromSeconds(_optionsMonitor.CurrentValue.SecondsPeriod);
    }

    private int TakeRecordsNum
    {
        get => _optionsMonitor.CurrentValue.TakeRecordsNum;
    }

    public OutboxBackgroundWorker(
        IServiceProvider serviceProvider,
        ILogger<OutboxBackgroundWorker> logger,
        IOptionsMonitor<OutboxBackgroundWorkerOptions> optionsMonitor)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _optionsMonitor = optionsMonitor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Outbox Background Worker успешно запущен.");

        using var timer = new PeriodicTimer(Period);

        while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessOutboxMessagesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Критическая ошибка при выполнении фоновой задачи Outbox.");
            }
        }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken ct)
    {
        List<OutboxMessage> messages;
        using (var readScope = _serviceProvider.CreateScope())
        {
            var context = readScope.ServiceProvider.GetRequiredService<OrderContext>();
            messages = context.Set<OutboxMessage>()
                .TagWith("OutboxPolling")
                .Where(m => m.ProcessedOnUtc == null)
                .OrderBy(m => m.OccurredOnUtc)
                .Take(TakeRecordsNum)
                .ToList();
        }

        if (messages.Count == 0) return;

        _logger.LogInformation("Найдено {Count} необработанных сообщений в Outbox.", messages.Count);

        foreach (var message in messages)
        {
            using var actionScope = _serviceProvider.CreateScope();
            var context = actionScope.ServiceProvider.GetRequiredService<OrderContext>();
            var domainEventDispatcher = actionScope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
            var timeProvider = actionScope.ServiceProvider.GetRequiredService<TimeProvider>();

            context.Attach(message);

            try
            {
                // 1. Восстанавливаем .NET тип из строки AssemblyQualifiedName
                Type? eventType = Type.GetType(message.Type);
                if (eventType == null) 
                {
                    _logger.LogError("Не удалось восстановить тип события: {Type}", message.Type);
                    message.Error = $"Тип .NET '{message.Type}' не найден в сборках.";
                    message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
                    continue;
                }

                // 2. Десериализуем JSON контент обратно в объект доменного события
                var domainEvent = JsonSerializer.Deserialize(message.Content, eventType, OutboxSerializerOptions.Options);
                if (domainEvent is not IDomainEvent validEvent) 
                {
                    _logger.LogError("Объект сообщения не реализует IDomainEvent");
                    message.Error = "Объект десериализации не является IDomainEvent.";
                    message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
                    continue;
                }

                await domainEventDispatcher.DispatchAsync(validEvent, ct);
                // в случае успеха маркируем дату успешной обработки
                message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
                message.Error = null; // очистить ошибки, если были ранее
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке Outbox сообщения с Id: {Id}", message.Id);
                message.Error = ex.ToString();
            }

            await context.SaveChangesAsync(ct);
        }
    }
}
```

---

### Caching

#### Abstractions

```cs title="ICacheInvalidator.cs"
namespace LegacyLego.Infrastructure.Caching.Abstractions;

public interface ICacheInvalidator
{
    public Task InvalidateAsync(IEnumerable<object> entities, CancellationToken ct);
}
```

---

```cs title="ICacheService.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Infrastructure.Caching.Abstractions;

public interface ICacheService
{
    public Task<Result<T>> GetOrCreateAsync<T>(
        string cacheGroup,
        string specificKey,
        Func<Task<Result<T>>> factory,
        TimeSpan ttl,
        CancellationToken ct);
}
```

---

```cs title="IEntityInvalidator.cs"
namespace LegacyLego.Infrastructure.Caching.Abstractions;

public interface IEntityInvalidator<in TEntity> where TEntity : class
{
    public Task InvalidateAsync(IEnumerable<TEntity> entities, CancellationToken ct);
}
```

---

#### Decorators

##### Query

###### Order

```cs title="GetOrderDetailsQueryCachingDecorator.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrderDetails;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Options;
namespace LegacyLego.Infrastructure.Caching.Decorators.Query.Order;

public sealed class GetOrderDetailsQueryCachingDecorator
    : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto> _inner;
    private readonly ICacheService _cacheService;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public GetOrderDetailsQueryCachingDecorator(
        IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto> inner,
        ICacheService cacheService,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _inner = inner;
        _cacheService = cacheService;
        _cacheOptions = cacheOptions;
    }

    public Task<Result<OrderDetailsDto>> HandleAsync(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var cacheGroup = $"order:{query.OrderId}";

        var specificKey = "details";

        return _cacheService.GetOrCreateAsync(
            cacheGroup,
            specificKey,
            factory: () => _inner.HandleAsync(query, ct),
            ttl: TimeSpan.FromMinutes(_cacheOptions.CurrentValue.OrderDetailsMinutesTtl),
            ct);
    }
}
```

---

```cs title="GetOrdersHistoryQueryCachingDecorator.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace LegacyLego.Infrastructure.Caching.Decorators.Query.Order;

public sealed class GetOrdersHistoryQueryCachingDecorator
    : IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>
{
    private readonly IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse> _inner;
    private readonly ICacheService _cacheService;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public GetOrdersHistoryQueryCachingDecorator(
        IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse> inner,
        ICacheService cacheService,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _inner = inner;
        _cacheService = cacheService;
        _cacheOptions = cacheOptions;
    }

    public Task<Result<OrdersHistoryResponse>> HandleAsync(GetOrdersHistoryQuery query, CancellationToken ct)
    {
        var cacheGroup = $"orders:{query.UserId}";

        var safeCursor = string.IsNullOrWhiteSpace(query.Filter.Cursor) ? "first" : query.Filter.Cursor;
        var specificKey = $"cursor:{safeCursor}";

        return _cacheService.GetOrCreateAsync(
            cacheGroup,
            specificKey,
            factory: () => _inner.HandleAsync(query, ct),
            ttl: TimeSpan.FromMinutes(_cacheOptions.CurrentValue.OrdersHistoryMinutesTtl),
            ct);
    }
}
```

---

#### Invalidators

```cs title="OrderEntityInvalidator.cs"
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Order = LegacyLego.Domain.Aggregates.Order;

namespace LegacyLego.Infrastructure.Caching.Invalidators;

public sealed class OrderEntityInvalidator : IEntityInvalidator<Order>
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public OrderEntityInvalidator(
        IConnectionMultiplexer redis,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _redis = redis;
        _cacheOptions = cacheOptions;
    }

    public async Task InvalidateAsync(IEnumerable<Order> entities, CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        var batch = db.CreateBatch();
        var groupTtl = TimeSpan.FromDays(_cacheOptions.CurrentValue.OrderGroupDaysTtl);

        foreach (var order in entities)
        {
            var userVersionKey = $"orders:{order.ClientId}:version";
            var orderVersionKey = $"order:{order.Id.Value}:version";

            _ = batch.StringIncrementAsync(userVersionKey);
            _ = batch.StringIncrementAsync(orderVersionKey);

            _ = batch.KeyExpireAsync(userVersionKey, groupTtl);
            _ = batch.KeyExpireAsync(orderVersionKey, groupTtl);
        }

        batch.Execute();
        await Task.CompletedTask;
    }
}
```

---

#### Services

```cs title="CacheInvalidator.cs"
using LegacyLego.Infrastructure.Caching.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyLego.Infrastructure.Caching.Services;

public sealed class CacheInvalidator : ICacheInvalidator
{
    private readonly IServiceProvider _serviceProvider;

    public CacheInvalidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InvalidateAsync(IEnumerable<object> entities, CancellationToken ct)
    {
        var groupedEntities = entities.GroupBy(e => e.GetType());

        foreach (var group in groupedEntities)
        {
            var entityType = group.Key;

            var invalidatorType = typeof(IEntityInvalidator<>).MakeGenericType(entityType);

            var invalidator = _serviceProvider.GetService(invalidatorType);

            if (invalidator is null)
                continue;

            var method = invalidatorType.GetMethod(nameof(IEntityInvalidator<object>.InvalidateAsync));

            if (method is not null)
            {
                var typedList = CastList(group, entityType);

                await (Task)method.Invoke(invalidator, [typedList, ct])!;
            }
        }
    }

    private static object CastList(IEnumerable<object> source, Type targetType)
    {
        var castMethod = typeof(Enumerable)
            .GetMethod(nameof(Enumerable.Cast))!
            .MakeGenericMethod(targetType);

        var toListMethod = typeof(Enumerable)
            .GetMethod(nameof(Enumerable.ToList))!
            .MakeGenericMethod(targetType);

        var casted = castMethod.Invoke(null, [source]);
        return toListMethod.Invoke(null, [casted])!;
    }
}
```

---

```cs title="RedisCacheService.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace LegacyLego.Infrastructure.Caching.Services;

public sealed class RedisCacheService : ICacheService
{
    private readonly ILogger<RedisCacheService> _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly IOptionsMonitor<CacheOptions> _cacheOptions;

    public RedisCacheService(IConnectionMultiplexer redis,
        ILogger<RedisCacheService> logger,
        IOptionsMonitor<CacheOptions> cacheOptions)
    {
        _redis = redis;
        _logger = logger;
        _cacheOptions = cacheOptions;
    }

    public async Task<Result<T>> GetOrCreateAsync<T>(
        string cacheGroup,
        string specificKey,
        Func<Task<Result<T>>> factory,
        TimeSpan ttl,
        CancellationToken ct)
    {
        var db = _redis.GetDatabase();

        var versionKey = $"{cacheGroup}:version";
        var version = await db.StringGetAsync(versionKey);

        if (!version.HasValue)
        {
            var groupTtl = TimeSpan.FromDays(_cacheOptions.CurrentValue.OrderGroupDaysTtl);
            await db.StringSetAsync(versionKey, "1", groupTtl, When.NotExists);
            version = "1";
        }

        var dataKey = $"{cacheGroup}:v{version}:{specificKey}";

        var cachedData = await db.StringGetAsync(dataKey);
        if (cachedData.HasValue) // Кэш-хит
        {
            try
            {
                var deserialized = JsonSerializer.Deserialize<T>((byte[])cachedData!);
                if (deserialized is not null)
                {
                    _logger.LogDebug("Cache HIT for key: {CacheKey}", dataKey);
                    return Result<T>.Success(deserialized);
                }
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Failed to deserialize cache payload for key: {CacheKey}. Falling back to database.",
                    dataKey);
            }
        }

        // Кэш-мисс
        _logger.LogDebug("Cache MISS for key: {CacheKey}. Fetching data from source.", dataKey);
        var result = await factory();

        if (result.IsSuccess)
        {
            var serialized = JsonSerializer.Serialize(result.Value);
            await db.StringSetAsync(dataKey, serialized, ttl);
        }

        return result;
    }
}
```

---

### Common

```cs title="SpecificationEvaluator.cs"
using LegacyLego.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Common;

internal static class SpecificationEvaluator
{
    internal static IQueryable<TResult> GetQuery<TEntity, TId, TResult>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId, TResult> specification)
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        var queryable = ApplyBaseSpecifications(inputQueryable, specification);

        return queryable.Select(specification.Selector);
    }

    internal static IQueryable<TEntity> GetQuery<TEntity, TId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId> specification)
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        var queryable = ApplyBaseSpecifications(inputQueryable, specification);

        foreach (var exp in specification.IncludeExpressions)
            queryable = queryable.Include(exp);

        return queryable;
    }

    private static IQueryable<TEntity> ApplyBaseSpecifications<TEntity, TId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId> specification)
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        IQueryable<TEntity> queryable = inputQueryable;

        foreach (var exp in specification.FilterExpressions)
            queryable = queryable.Where(exp);

        if (specification.OrderByExpressions.Any())
        {
            var ordered = queryable.OrderBy(specification.OrderByExpressions[0]);
            for (int i = 1; i < specification.OrderByExpressions.Count; i++)
                ordered = ordered.ThenBy(specification.OrderByExpressions[i]);
            queryable = ordered;
        }

        if (specification.OrderByDescendingExpressions.Any())
        {
            var ordered = queryable.OrderByDescending(specification.OrderByDescendingExpressions[0]);
            for (int i = 1; i < specification.OrderByDescendingExpressions.Count; i++)
                ordered = ordered.ThenByDescending(specification.OrderByDescendingExpressions[i]);
            queryable = ordered;
        }

        if (specification.SkipNum.HasValue)
            queryable = queryable.Skip(specification.SkipNum.Value);

        if (specification.LimitNum.HasValue)
            queryable = queryable.Take(specification.LimitNum.Value);

        return queryable;
    }
}
```

---

### Configuration

```cs title="ExternalSessionConfiguration.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class ExternalSessionConfiguration : IEntityTypeConfiguration<ExternalSession>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_external_session";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "External_session";
    private const string ID_COLUMN_NAME = "order_payment_id";
    private const string EXTERNAL_ID_COLUMN_NAME = "external_id";
    private const string CHECKOUT_URL_COLUMN_NAME = "checkout_url";
    private const string EXPIRES_AT_UTC_COLUMN_NAME = "expires_at_utc";
    #endregion

    public void Configure(EntityTypeBuilder<ExternalSession> builder)
    {
        builder.ToTable(TABLE_NAME);

        #region order_payment_id
        builder.Property<OrderPaymentId>("OrderPaymentId")
            .HasColumnName(ID_COLUMN_NAME)
            .HasConversion(id => id.Value, value => OrderPaymentId.From(value))
            .HasColumnType(Uuid)
            .IsRequired();

        builder.HasKey("OrderPaymentId")
            .HasName(PK_CONSTRAINT_NAME); 
        #endregion

        #region external_id
        builder.Property(x => x.ExternalId)
            .HasColumnName(EXTERNAL_ID_COLUMN_NAME)
            .HasPostgresVarchar(255)
            .IsRequired(); 
        #endregion

        #region checkout_url
        builder.Property(x => x.CheckoutUrl)
            .HasColumnName(CHECKOUT_URL_COLUMN_NAME)
            .HasColumnType(Text)
            .IsRequired(); 
        #endregion

        #region expires_at_utc
        builder.Property(x => x.ExpiresAtUtc)
            .HasColumnName(EXPIRES_AT_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired();
        #endregion
    }
}
```

---

```cs title="OrderConfiguration.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_order";
    private const string CHECK_ORDER_STATUS_CONSTRAINT_NAME = "check_order_status";
    private const string CHECK_FROZEN_TOTAL_SUM_CONSTRAINT_NAME = "check_order_frozen_total_sun";
    private const string FK_ORDER_ORDER_ITEMS_CONSTRAINT_NAME = "fk_order_order_items";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Order";
    private const string ID_COLUMN_NAME = "id";
    private const string STATUS_COLUMN_NAME = "status";
    private const string CREATED_AT_UTC_COLUMN_NAME = "created_at_utc";
    private const string CLIENT_ID_COLUMN_NAME = "client_id";
    private const string FROZEN_TOTAL_SUM_COLUMN_NAME = "frozen_total_sum";
    private const string CURRENCY_CODE_COLUMN_NAME = "currency_code";

    private const string ADDRESS_COUNTRY_COLUMT_NAME = "address_country";
    private const string ADDRESS_CITY_COLUMT_NAME = "address_city";
    private const string ADDRESS_STREET_COLUMT_NAME = "address_street";
    private const string ADDRESS_POSTAL_CODE_COLUMT_NAME = "address_postal_code";
    #endregion

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TABLE_NAME,
               t =>
               {
                   t.HasEnumCheckConstraint<OrderStatus>(CHECK_ORDER_STATUS_CONSTRAINT_NAME, STATUS_COLUMN_NAME);
                   t.HasCheckConstraint(CHECK_FROZEN_TOTAL_SUM_CONSTRAINT_NAME, $"\"{FROZEN_TOTAL_SUM_COLUMN_NAME}\" >= 0");
               });

        #region id
        builder.HasKey(o => o.Id).HasName(PK_CONSTRAINT_NAME);

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasColumnType(Uuid)
            .HasConversion(id => id.Value, value => OrderId.From(value))
            .HasColumnName(ID_COLUMN_NAME);
        #endregion

        #region status
        builder.Property(o => o.Status)
            .HasPostgresVarchar(50, allowStringConversion: true)
            .HasColumnName(STATUS_COLUMN_NAME)
            .IsRequired();
        #endregion

        #region created_at_utc
        builder.Property(o => o.CreationDateUtc)
            .HasColumnName(CREATED_AT_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired();
        #endregion

        #region currency_code
        builder.Property(o => o.Currency)
            .HasColumnName(CURRENCY_CODE_COLUMN_NAME)
            .HasPostgresVarchar(3)
            .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
            .IsRequired();
        #endregion

        #region Address VO
        builder.ComplexProperty(o => o.Address, address =>
        {
            address.Property(a => a.Country)
                .HasColumnName(ADDRESS_COUNTRY_COLUMT_NAME)
                .HasPostgresVarchar(100)
                .IsRequired();

            address.Property(a => a.City)
                .HasColumnName(ADDRESS_CITY_COLUMT_NAME)
                .HasPostgresVarchar(100)
                .IsRequired();

            address.Property(a => a.Street)
                .HasColumnName(ADDRESS_STREET_COLUMT_NAME)
                .HasPostgresVarchar(255)
                .IsRequired();

            address.Property(a => a.PostalCode)
                .HasColumnName(ADDRESS_POSTAL_CODE_COLUMT_NAME)
                .HasPostgresVarchar(20)
                .IsRequired();
        });
        #endregion

        #region Items FK
        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .HasConstraintName(FK_ORDER_ORDER_ITEMS_CONSTRAINT_NAME); 
        #endregion

        #region frozen_total_sum
        builder.Property<decimal?>("FrozenTotalSum")
            .HasColumnName(FROZEN_TOTAL_SUM_COLUMN_NAME)
            .HasColumnType(Numeric(15,2))
            .IsRequired(false);
        #endregion

        //TODO FK when Client implemented !!!
        #region client_id 
        builder.Property(o => o.ClientId)
            .HasColumnName(CLIENT_ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired();
        #endregion
    }
}
```

---

```cs title="OrderItemConfiguration.cs"
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_order_item";
    private const string CHECK_UNIT_PRICE_CONSTRAINT_NAME = "check_unit_price_status";
    private const string CHECK_QUANTITY_CONSTRAINT_NAME = "check_quantity";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Order_item";
    private const string ID_COLUMN_NAME = "id";
    private const string TITLE_COLUMN_NAME = "title";
    private const string QUANTITY_COLUMN_NAME = "quantity";
    private const string PRODUCT_ID_COLUMN_NAME = "product_id";
    private const string ORDER_ID_COLUMN_NAME = "order_id";

    private const string UNIT_PRICE_COLUMN_NAME = "unit_price";
    private const string CURRENCY_CODE_COLUMN_NAME = "currency_code"; 
    #endregion

    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(TABLE_NAME,
               t =>
               {
                   t.HasCheckConstraint(CHECK_QUANTITY_CONSTRAINT_NAME, $"\"{QUANTITY_COLUMN_NAME}\" >= 1");
                   t.HasCheckConstraint(CHECK_UNIT_PRICE_CONSTRAINT_NAME, $"\"{UNIT_PRICE_COLUMN_NAME}\" > 0");
               });

        #region id
        // Shadow property
        builder.Property<Guid>("Id")
            .HasColumnName(ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasKey("Id").HasName(PK_CONSTRAINT_NAME);
        #endregion

        #region title
        builder.Property(x => x.Title)
            .HasColumnName(TITLE_COLUMN_NAME)
            .HasPostgresVarchar(255)
            .IsRequired();
        #endregion

        #region quantity
        builder.Property(x => x.Quantity)
            .HasColumnName(QUANTITY_COLUMN_NAME)
            .HasColumnType(SmallInt)
            .IsRequired();
        #endregion

        #region order_id
        // Shadow property
        builder.Property<OrderId>("OrderId")
             .HasColumnName(ORDER_ID_COLUMN_NAME)
             .HasConversion(id => id.Value, value => OrderId.From(value))
             .IsRequired();
        #endregion

        #region UnitPrice VO
        builder.ComplexProperty(x => x.UnitPrice, price =>
        {
            price.Property(p => p.Sum)
                .HasColumnName(UNIT_PRICE_COLUMN_NAME)
                .HasColumnType(Numeric(15,2))
                .IsRequired();

            price.Property(p => p.Currency)
                .HasColumnName(CURRENCY_CODE_COLUMN_NAME)
                .HasPostgresVarchar(3)
                .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
                .IsRequired();
        });
        #endregion

        #region product_id
        //TODO FK when Product implemented !!!
        builder.Property(x => x.ProductId)
            .HasColumnName(PRODUCT_ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired(); 
        #endregion
    }
}
```

---

```cs title="OrderPaymentConfiguration.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LegacyLego.Infrastructure.Configuration.Common;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class OrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
{
    #region CostraintNames
    private const string CHECK_ORDER_PAYMENT_CONSTRAINT_NAME = "check_order_payment_status";
    private const string PK_CONSTRAINT_NAME = "pk_order_payment";
    private const string FK_ORDER_PAYMENT_ORDERS_CONSTRAINT_NAME = "fk_order_payment_orders"; 
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Order_payment";
    private const string ID_COLUMN_NAME = "id";
    private const string STATUS_COLUMN_NAME = "status";
    private const string TRANSACTION_ID_COLUMN_NAME = "transaction_id";
    private const string CREATED_AT_UTC_COLUMN_NAME = "created_at_utc";
    private const string ORDER_ID_COLUMN_NAME = "order_id";

    private const string EXPECTED_AMOUNT_COLUMN_NAME = "expected_amount";
    private const string EXPECTED_CURRENCY_COLUMN_NAME = "expected_currency";

    private const string ACTUAL_AMOUNT_COLUMN_NAME = "actual_amount";
    private const string ACTUAL_CURRENCY_COLUMN_NAME = "actual_currency";
    #endregion

    public void Configure(EntityTypeBuilder<OrderPayment> builder)
    {
        builder.ToTable(TABLE_NAME,
               t => t.HasEnumCheckConstraint<PaymentStatus>(CHECK_ORDER_PAYMENT_CONSTRAINT_NAME, STATUS_COLUMN_NAME));

        #region id
        builder.HasKey(x => x.Id).HasName(PK_CONSTRAINT_NAME);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasColumnType(Uuid)
            .HasConversion(id => id.Value, value => OrderPaymentId.From(value))
            .HasColumnName(ID_COLUMN_NAME);
        #endregion

        #region transaction_id
        builder.HasIndex(x => x.TransactionId)
            .IsUnique()
            .HasFilter("transaction_id IS NOT NULL");
        builder.Property(x => x.TransactionId)
            .HasPostgresVarchar(255)
            .IsRequired(false)
            .HasColumnName(TRANSACTION_ID_COLUMN_NAME);
        #endregion

        #region created_at_utc
        builder.Property(p => p.CreatedAtUtc)
            .HasColumnType(TimeStampTz)
            .IsRequired()
            .HasColumnName(CREATED_AT_UTC_COLUMN_NAME);
        #endregion

        #region status
        builder.Property(p => p.Status)
            .HasPostgresVarchar(50, allowStringConversion:true)
            .IsRequired()
            .HasColumnName(STATUS_COLUMN_NAME);
        #endregion

        #region order_id
        builder.Property(p => p.OrderId)
            .HasConversion(id => id.Value, value => OrderId.From(value))
            .HasColumnName(ORDER_ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired();

        #region Price ExpectedAmount (NOT NULL)
        builder.ComplexProperty(x => x.ExpectedAmount, priceBuilder =>
        {
            priceBuilder.Property(p => p.Sum)
                .HasColumnType(Numeric(15, 2))
                .HasColumnName(EXPECTED_AMOUNT_COLUMN_NAME)
                .IsRequired();

            priceBuilder.Property(p => p.Currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
                .HasPostgresVarchar(3)
                .HasColumnName(EXPECTED_CURRENCY_COLUMN_NAME)
                .IsRequired();
        });
        #endregion

        #region Price ActualAmount (NULLABLE)
        builder.ComplexProperty(x => x.ActualAmount, priceBuilder =>
        {
            priceBuilder.Property(p => p.Sum)
                .HasPrecision(15, 2)
                .HasColumnName(ACTUAL_AMOUNT_COLUMN_NAME);

            priceBuilder.Property(p => p.Currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
                .HasPostgresVarchar(3)
                .HasColumnName(ACTUAL_CURRENCY_COLUMN_NAME);
        });
        #endregion

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(p => p.OrderId)
            .HasConstraintName(FK_ORDER_PAYMENT_ORDERS_CONSTRAINT_NAME);
        #endregion

        #region ExternalSession
        builder.HasOne(p => p.ExternalSession)
            .WithOne()
            .HasForeignKey<ExternalSession>("OrderPaymentId")
            .IsRequired(false); 
        #endregion
    }
}
```

---

```cs title="OutboxMessageConfiguration.cs"
using LegacyLego.Infrastructure.Configuration.Common;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_outbox_messages";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Outbox_messages";
    private const string ID_COLUMN_NAME = "id";
    private const string TYPE_COLUMN_NAME = "type";
    private const string CONTENT_COLUMN_NAME = "content";
    private const string OCCURRED_ON_UTC_COLUMN_NAME = "occurred_on_utc";

    private const string PROCESSED_ON_UTC_COLUMN_NAME = "processed_on_utc";
    private const string ERROR_COLUMN_NAME = "error";
    #endregion

    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TABLE_NAME);

        #region id
        builder.HasKey(x => x.Id)
            .HasName(PK_CONSTRAINT_NAME);

        builder.Property(x => x.Id)
            .HasColumnName(ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired(); 
        #endregion

        #region type
        builder.Property(x => x.Type)
            .HasColumnName(TYPE_COLUMN_NAME)
            .HasPostgresVarchar(255)
            .IsRequired(); 
        #endregion

        #region content
        builder.Property(x => x.Content)
            .HasColumnName(CONTENT_COLUMN_NAME)
            .HasColumnType(Text)
            .IsRequired(); 
        #endregion

        #region occurred_on_utc
        builder.Property(x => x.OccurredOnUtc)
            .HasColumnName(OCCURRED_ON_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired(); 
        #endregion

        #region processed_on_utc
        builder.Property(x => x.ProcessedOnUtc)
            .HasColumnName(PROCESSED_ON_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired(false); 
        #endregion

        #region error
        builder.Property(x => x.Error)
            .HasColumnName(ERROR_COLUMN_NAME)
            .HasColumnType(Text)
            .IsRequired(false); 
        #endregion
    }
}
```

---

#### Common

```cs title="EntityTypeBuilderExtensions.cs"
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegacyLego.Infrastructure.Configuration.Common;

public static class EntityTypeBuilderExtensions
{
    public static CheckConstraintBuilder HasEnumCheckConstraint<TEnum>(
        this TableBuilder tableBuilder,
        string constraintName,
        string columnName) where TEnum : struct, Enum
    {
        var sb = new StringBuilder($"\"{columnName}\" IN (");

        foreach (var status in Enum.GetValues<TEnum>())
        {
            sb.Append($"'{status}', ");
        }

        sb.Remove(sb.Length - 2, 2);
        sb.Append(")");

        return tableBuilder.HasCheckConstraint(constraintName, sb.ToString());
    }
}
```

---

```cs title="PostgresTypes.cs"
namespace LegacyLego.Infrastructure.Configuration.Common;

/// <summary>
/// Централизованное хранилище строковых литералов типов данных СУБД PostgreSQL.
/// Используется во Fluent API конфигурациях для обеспечения строгой типизации и предотвращения опечаток.
/// </summary>
internal static class PostgresTypes
{
    internal const string TimeStampTz = "timestamptz";
    internal const string Uuid = "uuid";
    internal const string SmallInt = "smallint";
    internal const string Text = "text";

    /// <summary>
    /// Генерирует строку точного численного типа данных <c>numeric(precision, scale)</c>.
    /// </summary>
    /// <param name="precision">Общее количество десятичных цифр в числе (как до, так и после запятой).</param>
    /// <param name="scale">Количество цифр в дробной части (после запятой).</param>
    /// <returns>Строковое представление numeric(x,n) типа данных для передачи в <c>HasColumnType()</c>.</returns>
    internal static string Numeric(int precision, int scale) => $"numeric({precision},{scale})";
}
```

---

```cs title="PropertyBuilderExtensions.cs"
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegacyLego.Infrastructure.Configuration.Common;

internal static class PropertyBuilderExtensions
{
    internal static PropertyBuilder<T> HasPostgresVarchar<T>(this PropertyBuilder<T> builder, int length, bool allowStringConversion = false)
    {
        if (allowStringConversion)
            builder.HasConversion<string>();

        return builder
            .HasColumnType($"varchar({length})")
            .HasMaxLength(length);
    }

    internal static ComplexTypePropertyBuilder<T> HasPostgresVarchar<T>(this ComplexTypePropertyBuilder<T> builder, int length, bool allowStringConversion = false)
    {
        if (allowStringConversion)
            builder.HasConversion<string>();

        return builder
            .HasColumnType($"varchar({length})")
            .HasMaxLength(length);
    }
}
```

---

### Context

```cs title="OrderContext.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Infrastructure.Configuration;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Context;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderPayment> OrderPayments => Set<OrderPayment>();

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public OrderContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ExternalSessionConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
```

---

### Converters

#### JsonConverters

```cs title="CurrencyJsonConverter.cs"
using LegacyLego.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.Infrastructure.Converters.JsonConverters;

public sealed class CurrencyJsonConverter : JsonConverter<Currency>
{
    public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Expected string for Currency, but got {reader.TokenType}.");

        var code = reader.GetString();

        if (string.IsNullOrWhiteSpace(code))
            throw new JsonException("Currency code in JSON is null or empty.");

        var result = Currency.FromCode(code);

        if (result.IsFailure)
            throw new JsonException($"Failed to deserialize Currency: {result.Error.Message}");

        return result.Value;
    }

    public override void Write(Utf8JsonWriter writer, Currency value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Code);
    }
}
```

---

```cs title="PriceJsonConverter.cs"
using LegacyLego.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.Infrastructure.Converters.JsonConverters
{
    public sealed class PriceJsonConverter : JsonConverter<Price>
    {
        public override Price Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException($"Expected StartObject for Price, but got {reader.TokenType}.");

            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!TryGetProperty(root, "Sum", "sum", out var sumElement) || !sumElement.TryGetDecimal(out var sum))
                throw new JsonException("Property 'Sum' is missing or not a valid decimal.");

            if (!TryGetProperty(root, "Currency", "currency", out var currencyElement))
                throw new JsonException("Property 'Currency' is missing.");

            var currency = JsonSerializer.Deserialize<Currency>(currencyElement.GetRawText(), options)
                           ?? throw new JsonException("Failed to deserialize Currency inside Price.");

            var result = Price.Create(sum, currency);

            if (result.IsFailure)
                throw new JsonException($"Failed to create Price during deserialization: {result.Error.Message}");

            return result.Value;
        }

        public override void Write(Utf8JsonWriter writer, Price value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            var sumPropertyName = options.PropertyNamingPolicy?.ConvertName("Sum") ?? "Sum";
            var currencyPropertyName = options.PropertyNamingPolicy?.ConvertName("Currency") ?? "Currency";

            writer.WriteNumber(sumPropertyName, value.Sum);

            writer.WritePropertyName(currencyPropertyName);
            JsonSerializer.Serialize(writer, value.Currency, options);

            writer.WriteEndObject();
        }

        private static bool TryGetProperty(JsonElement element, string pascalName, string camelName, out JsonElement value)
        {
            if (element.TryGetProperty(pascalName, out value)) return true;
            if (element.TryGetProperty(camelName, out value)) return true;
            return false;
        }
    }
}
```

---

### Design

```cs title="OrderContextFactory.cs"
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace LegacyLego.Infrastructure.Design;

public sealed class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        if (environment == "Development")
        {
            builder.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);
        }

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("Database")
                               ?? configuration["Database:ConnectionString"];

        if (string.IsNullOrWhiteSpace(connectionString))
            connectionString = "Host=localhost;Database=dummy;Username=dummy;Password=dummy";

        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new OrderContext(optionsBuilder.Options);
    }
}
```

---

### Diagnostics

```cs title="InfrastructureExceptionMapper.cs"
using LegacyLego.Application.Abstractions.ExceptionHandling;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Diagnostics;

public sealed class InfrastructureExceptionMapper : IExceptionMapper
{
    public bool TryMap(Exception exception, out AppFailureDescription? description)
    {
        if (exception is DbUpdateException)
        {
            description = new AppFailureDescription(
                Kind: ExceptionFailureKind.InfrastructureLevelException,
                Title: "Хранилище данных временно недоступно",
                Detail: "Ошибка при выполнении операции с базой данных PostgreSQL."
            );
            return true;
        }

        description = null;
        return false;
    }
}
```

---

### Logging

#### Decorators

```cs title="LoggingCommandHandlerDecorator.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LegacyLego.Infrastructure.Logging.Decoretors;

public sealed class LoggingCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> _inner,
    ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var commandName = typeof(TCommand).Name;

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["CommandType"] = commandName,
            ["CommandData"] = command!
        });

        var stopwatch = Stopwatch.StartNew();
        var result = await _inner.HandleAsync(command, cancellationToken);
        stopwatch.Stop();

        if (result.IsSuccess)
        {
            _logger.LogInformation(
                "Команда {CommandName} успешно выполнена за {ElapsedMs} мс.",
                commandName,
                stopwatch.ElapsedMilliseconds);
        }
        else
        {
            _logger.LogError(
                "Ошибка выполнения команды {CommandName} ({ElapsedMs} мс). Код ошибки: {ErrorCode}. Причина: {ErrorMessage}",
                commandName,
                stopwatch.ElapsedMilliseconds,
                result.Error.Code,
                result.Error.Message);
        }

        return result;
    }
}

public sealed class LoggingCommandHandlerDecorator<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> _inner,
    ILogger<LoggingCommandHandlerDecorator<TCommand, TResponse>> _logger)
    : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var commandName = typeof(TCommand).Name;

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["CommandType"] = commandName,
            ["CommandData"] = command!
        });

        var stopwatch = Stopwatch.StartNew();
        var result = await _inner.HandleAsync(command, cancellationToken);
        stopwatch.Stop();

        if (result.IsSuccess)
        {
            var isWarning = result.Value is ICustomLogSeverity customLog && customLog.IsWarning;

            if (isWarning)
            {
                _logger.LogWarning(
                    "Команда {CommandName} выполнена с предупреждением за {ElapsedMs} мс. Результат: {@Details}",
                    commandName,
                    stopwatch.ElapsedMilliseconds,
                    result.Value);
            }
            else
            {
                _logger.LogInformation(
                    "Команда {CommandName} успешно выполнена за {ElapsedMs} мс. Результат: {@Details}",
                    commandName,
                    stopwatch.ElapsedMilliseconds,
                    result.Value);
            }
        }
        else
        {
            _logger.LogError(
                "Ошибка выполнения команды {CommandName} ({ElapsedMs} мс). Код ошибки: {ErrorCode}. Причина: {ErrorMessage}",
                commandName,
                stopwatch.ElapsedMilliseconds,
                result.Error.Code,
                result.Error.Message);
        }

        return result;
    }
}
```

---

### Messaging

#### Abstractions

```cs title="IIntegrationEventBus.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;

namespace LegacyLego.Infrastructure.Messaging.Abstractions;

public interface IIntegrationEventBus
{
    public Task DispatchAsync(
        IIntegrationEvent @event,
        CancellationToken ct = default);
}
```

---

```cs title="IIntegrationEventConsumer.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;

namespace LegacyLego.Infrastructure.Messaging.Abstractions;

public interface IIntegrationEventConsumer<in TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    public Task HandleAsync(TIntegrationEvent notification, CancellationToken ct);
}
```

---

#### Bus

```cs title="InMemoryIntegrationEventBus.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Infrastructure.Messaging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging.Bus;

public sealed class InMemoryIntegrationEventBus : IIntegrationEventBus
{
    private readonly IServiceProvider _serviceProvider;

    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public InMemoryIntegrationEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IIntegrationEvent @event, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(@event, nameof(@event));

        var eventType = @event.GetType();

        var wrapper = WrapperCache.GetOrAdd(eventType, type =>
        {
            var concreteWrapperType = typeof(IntegrationEventWrapper<>).MakeGenericType(type);
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        using var scope = _serviceProvider.CreateScope();
        await ((IntegrationEventWrapper)wrapper).HandleAsync(@event, scope.ServiceProvider, ct);
    }
}

file abstract class IntegrationEventWrapper
{
    public abstract Task HandleAsync(IIntegrationEvent @event, IServiceProvider serviceProvider, CancellationToken ct);
}

file sealed class IntegrationEventWrapper<TIntegrationEvent> : IntegrationEventWrapper
    where TIntegrationEvent : IIntegrationEvent
{
    public override async Task HandleAsync(IIntegrationEvent @event, IServiceProvider serviceProvider, CancellationToken ct)
    {
        var consumers = serviceProvider.GetServices<IIntegrationEventConsumer<TIntegrationEvent>>();

        foreach (var consumer in consumers)
        {
            if (consumer is null) continue;

            await consumer.HandleAsync((TIntegrationEvent)@event, ct);
        }
    }
}
```

---

#### Consumers

```cs title="OrderPaymentRefundRequestedIntegrationConsumer.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Infrastructure.Messaging.Abstractions;

namespace LegacyLego.Infrastructure.Messaging.Consumers;

public class OrderPaymentRefundRequestedIntegrationConsumer : IIntegrationEventConsumer<OrderPaymentRefundRequestedIntegrationEvent>
{
    private readonly IPaymentProvider _paymentProvider;

    public OrderPaymentRefundRequestedIntegrationConsumer(IPaymentProvider paymentProvider)
    {
        _paymentProvider = paymentProvider;
    }

    public async Task HandleAsync(OrderPaymentRefundRequestedIntegrationEvent notification, CancellationToken ct)
    {
        var result = await _paymentProvider.RequestRefundAsync(
             notification.OrderId,
             notification.Amount,
             notification.Currency,
             notification.TransactionId,
             ct);

        if (result.IsFailure)
        {
            throw new InvalidOperationException(result.Error.Message);
        }
    }
}
```

---

#### Dispatchers

```cs title="CommandDispatcher.cs"
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging.Dispatchers;

public sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        var commandType = command.GetType();

        var wrapper = WrapperCache.GetOrAdd(commandType, type =>
        {
            var concreteWrapperType = typeof(CommandWrapperR<,>).MakeGenericType(type, typeof(TResult));
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        return ((CommandWrapperR<TResult>)wrapper).HandleAsync(command, serviceProvider, ct);
    }

    public Task<Result> DispatchAsync(ICommand command, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        var commandType = command.GetType();

        var wrapper = WrapperCache.GetOrAdd(commandType, type =>
        {
            var concreteWrapperType = typeof(CommandWrapper<>).MakeGenericType(type);
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        return ((CommandWrapper)wrapper).HandleAsync(command, serviceProvider, ct);
    }
}

file abstract class CommandWrapperR<TResult>
{
    public abstract Task<Result<TResult>> HandleAsync(ICommand<TResult> command, IServiceProvider provider, CancellationToken ct);
}

file sealed class CommandWrapperR<TCommand, TResult> : CommandWrapperR<TResult>
    where TCommand : ICommand<TResult>
{
    public override Task<Result<TResult>> HandleAsync(ICommand<TResult> command, IServiceProvider provider, CancellationToken ct)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync((TCommand)command, ct);
    }
}

file abstract class CommandWrapper
{
    public abstract Task<Result> HandleAsync(ICommand command, IServiceProvider provider, CancellationToken ct);
}

file sealed class CommandWrapper<TCommand> : CommandWrapper
    where TCommand : ICommand
{
    public override Task<Result> HandleAsync(ICommand command, IServiceProvider provider, CancellationToken ct)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();
        return handler.HandleAsync((TCommand)command, ct);
    }
}
```

---

```cs title="DomainEventDispatcher.cs"
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging.Dispatchers;

public sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));

        var domainEventType = domainEvent.GetType();

        var wrapper = WrapperCache.GetOrAdd(domainEventType, type =>
        {
            var concreteWrapperType = typeof(DomainEventWrapper<>).MakeGenericType(type);
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        await ((DomainEventWrapper)wrapper).HandleAsync(domainEvent, _serviceProvider, ct);
    }
}

file abstract class DomainEventWrapper
{
    public abstract Task HandleAsync(IDomainEvent domainEvent, IServiceProvider provider, CancellationToken ct);
}

file sealed class DomainEventWrapper<TDomainEvent> : DomainEventWrapper
    where TDomainEvent : IDomainEvent
{
    public override async Task HandleAsync(IDomainEvent domainEvent, IServiceProvider provider, CancellationToken ct)
    {
        var handlers = provider.GetServices<IDomainEventHandler<TDomainEvent>>();

        foreach (var handler in handlers)
        {
            if (handler is null) continue;

            await handler.HandleAsync((TDomainEvent)domainEvent, ct);
        }
    }
}
```

---

```cs title="QueryDispatcher.cs"
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging.Dispatchers;

public sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public Task<Result<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        var queryType = query.GetType();

        var wrapper = WrapperCache.GetOrAdd(queryType, type =>
        {
            var concreteWrapperType = typeof(QueryWrapper<,>).MakeGenericType(type, typeof(TResult));
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        return ((QueryWrapper<TResult>)wrapper).HandleAsync(query, serviceProvider, ct);
    }
}

file abstract class QueryWrapper<TResult>
{
    public abstract Task<Result<TResult>> HandleAsync(IQuery<TResult> query, IServiceProvider provider, CancellationToken ct);
}

file sealed class QueryWrapper<TQuery, TResult> : QueryWrapper<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<Result<TResult>> HandleAsync(IQuery<TResult> query, IServiceProvider provider, CancellationToken ct)
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.HandleAsync((TQuery)query, ct);
    }
}
```

---

#### Publishers

```cs title="LocalIntegrationEventPublisher.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Infrastructure.Messaging.Abstractions;

namespace LegacyLego.Infrastructure.Messaging.Publishers;

public sealed class LocalIntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IIntegrationEventBus _bus;

    public LocalIntegrationEventPublisher(IIntegrationEventBus bus) => _bus = bus;

    public Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken ct)
        => _bus.DispatchAsync(integrationEvent, ct);
}
```

---

### Migrations

```cs title="20260622101458__Init.cs"
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    frozen_total_sum = table.Column<decimal>(type: "numeric(15,2)", nullable: true),
                    address_city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    address_country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    address_postal_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    address_street = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.CheckConstraint("check_order_frozen_total_sun", "\"frozen_total_sum\" >= 0");
                    table.CheckConstraint("check_order_status", "\"status\" IN ('PendingPayment', 'Paid', 'Cancelled', 'Expired', 'Refunded')");
                });

            migrationBuilder.CreateTable(
                name: "Outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order_item",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    quantity = table.Column<short>(type: "smallint", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.order_id);
                    table.CheckConstraint("check_quantity", "\"quantity\" >= 1");
                    table.CheckConstraint("check_unit_price_status", "\"unit_price\" > 0");
                    table.ForeignKey(
                        name: "fk_order_order_items",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_payment", x => x.id);
                    table.CheckConstraint("check_order_payment_status", "\"status\" IN ('Pending', 'Succeeded', 'Failed', 'Refunded', 'RefundRequested')");
                    table.ForeignKey(
                        name: "fk_order_payment_orders",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "External_session",
                columns: table => new
                {
                    order_payment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    checkout_url = table.Column<string>(type: "text", nullable: false),
                    expires_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_external_session", x => x.order_payment_id);
                    table.ForeignKey(
                        name: "FK_External_session_Order_payment_order_payment_id",
                        column: x => x.order_payment_id,
                        principalTable: "Order_payment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_payment_order_id",
                table: "Order_payment",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_payment_transaction_id",
                table: "Order_payment",
                column: "transaction_id",
                unique: true,
                filter: "transaction_id IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "External_session");

            migrationBuilder.DropTable(
                name: "Order_item");

            migrationBuilder.DropTable(
                name: "Outbox_messages");

            migrationBuilder.DropTable(
                name: "Order_payment");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
```

---

```cs title="20260622101458__Init.Designer.cs"
// <auto-generated />
using System;
using System.Collections.Generic;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20260622101458__Init")]
    partial class _Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid")
                        .HasColumnName("client_id");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<decimal?>("FrozenTotalSum")
                        .HasColumnType("numeric(15,2)")
                        .HasColumnName("frozen_total_sum");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "Address", "LegacyLego.Domain.Aggregates.Order.Address#OrderAddress", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(20)")
                                .HasColumnName("address_postal_code");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("address_street");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.ToTable("Order", null, t =>
                        {
                            t.HasCheckConstraint("check_order_frozen_total_sun", "\"frozen_total_sum\" >= 0");

                            t.HasCheckConstraint("check_order_status", "\"status\" IN ('PendingPayment', 'Paid', 'Cancelled', 'Expired', 'Refunded')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("transaction_id");

                    b.HasKey("Id")
                        .HasName("pk_order_payment");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransactionId")
                        .IsUnique()
                        .HasFilter("transaction_id IS NOT NULL");

                    b.ToTable("Order_payment", null, t =>
                        {
                            t.HasCheckConstraint("check_order_payment_status", "\"status\" IN ('Pending', 'Succeeded', 'Failed', 'Refunded', 'RefundRequested')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.Property<Guid>("OrderPaymentId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_payment_id");

                    b.Property<string>("CheckoutUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("checkout_url");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("expires_at_utc");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("external_id");

                    b.HasKey("OrderPaymentId")
                        .HasName("pk_external_session");

                    b.ToTable("External_session", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint")
                        .HasColumnName("quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "UnitPrice", "LegacyLego.Domain.ValueObjects.OrderItem.UnitPrice#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("currency_code");

                            b1.Property<decimal>("Sum")
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("unit_price");
                        });

                    b.HasKey("OrderId")
                        .HasName("pk_order_item");

                    b.ToTable("Order_item", null, t =>
                        {
                            t.HasCheckConstraint("check_quantity", "\"quantity\" >= 1");

                            t.HasCheckConstraint("check_unit_price_status", "\"unit_price\" > 0");
                        });
                });

            modelBuilder.Entity("LegacyLego.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("Outbox_messages", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_payment_orders");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.OrderPayment", null)
                        .WithOne("ExternalSession")
                        .HasForeignKey("LegacyLego.Domain.ValueObjects.ExternalSession", "OrderPaymentId");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_order_items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Navigation("ExternalSession");
                });
#pragma warning restore 612, 618
        }
    }
}
```

---

```cs title="20260622144846_FixExternalSessionPkShadowPropertyMapping.cs"
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixExternalSessionPkShadowPropertyMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "Order_item");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "Order_item",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "Order_item",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_item_order_id",
                table: "Order_item",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "Order_item");

            migrationBuilder.DropIndex(
                name: "IX_Order_item_order_id",
                table: "Order_item");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Order_item");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "Order_item",
                column: "order_id");
        }
    }
}
```

---

```cs title="20260622144846_FixExternalSessionPkShadowPropertyMapping.Designer.cs"
// <auto-generated />
using System;
using System.Collections.Generic;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20260622144846_FixExternalSessionPkShadowPropertyMapping")]
    partial class FixExternalSessionPkShadowPropertyMapping
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid")
                        .HasColumnName("client_id");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<decimal?>("FrozenTotalSum")
                        .HasColumnType("numeric(15,2)")
                        .HasColumnName("frozen_total_sum");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "Address", "LegacyLego.Domain.Aggregates.Order.Address#OrderAddress", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(20)")
                                .HasColumnName("address_postal_code");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("address_street");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.ToTable("Order", null, t =>
                        {
                            t.HasCheckConstraint("check_order_frozen_total_sun", "\"frozen_total_sum\" >= 0");

                            t.HasCheckConstraint("check_order_status", "\"status\" IN ('PendingPayment', 'Paid', 'Cancelled', 'Expired', 'Refunded')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("transaction_id");

                    b.HasKey("Id")
                        .HasName("pk_order_payment");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransactionId")
                        .IsUnique()
                        .HasFilter("transaction_id IS NOT NULL");

                    b.ToTable("Order_payment", null, t =>
                        {
                            t.HasCheckConstraint("check_order_payment_status", "\"status\" IN ('Pending', 'Succeeded', 'Failed', 'Refunded', 'RefundRequested')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.Property<Guid>("OrderPaymentId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_payment_id");

                    b.Property<string>("CheckoutUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("checkout_url");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("expires_at_utc");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("external_id");

                    b.HasKey("OrderPaymentId")
                        .HasName("pk_external_session");

                    b.ToTable("External_session", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint")
                        .HasColumnName("quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "UnitPrice", "LegacyLego.Domain.ValueObjects.OrderItem.UnitPrice#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("currency_code");

                            b1.Property<decimal>("Sum")
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("unit_price");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order_item");

                    b.HasIndex("OrderId");

                    b.ToTable("Order_item", null, t =>
                        {
                            t.HasCheckConstraint("check_quantity", "\"quantity\" >= 1");

                            t.HasCheckConstraint("check_unit_price_status", "\"unit_price\" > 0");
                        });
                });

            modelBuilder.Entity("LegacyLego.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("Outbox_messages", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_payment_orders");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.OrderPayment", null)
                        .WithOne("ExternalSession")
                        .HasForeignKey("LegacyLego.Domain.ValueObjects.ExternalSession", "OrderPaymentId");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_order_items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Navigation("ExternalSession");
                });
#pragma warning restore 612, 618
        }
    }
}
```

---

```cs title="20260803143309_AddOrderPaymentActualAndExpectedPriceAmounts.cs"
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderPaymentActualAndExpectedPriceAmounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "actual_amount",
                table: "Order_payment",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "actual_currency",
                table: "Order_payment",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "expected_amount",
                table: "Order_payment",
                type: "numeric(15,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "expected_currency",
                table: "Order_payment",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actual_amount",
                table: "Order_payment");

            migrationBuilder.DropColumn(
                name: "actual_currency",
                table: "Order_payment");

            migrationBuilder.DropColumn(
                name: "expected_amount",
                table: "Order_payment");

            migrationBuilder.DropColumn(
                name: "expected_currency",
                table: "Order_payment");
        }
    }
}
```

---

```cs title="20260803143309_AddOrderPaymentActualAndExpectedPriceAmounts.Designer.cs"
// <auto-generated />
using System;
using System.Collections.Generic;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20260803143309_AddOrderPaymentActualAndExpectedPriceAmounts")]
    partial class AddOrderPaymentActualAndExpectedPriceAmounts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid")
                        .HasColumnName("client_id");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<decimal?>("FrozenTotalSum")
                        .HasColumnType("numeric(15,2)")
                        .HasColumnName("frozen_total_sum");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "Address", "LegacyLego.Domain.Aggregates.Order.Address#OrderAddress", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(20)")
                                .HasColumnName("address_postal_code");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("address_street");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.ToTable("Order", null, t =>
                        {
                            t.HasCheckConstraint("check_order_frozen_total_sun", "\"frozen_total_sum\" >= 0");

                            t.HasCheckConstraint("check_order_status", "\"status\" IN ('PendingPayment', 'Paid', 'Cancelled', 'Expired', 'Refunded')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("transaction_id");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "ActualAmount", "LegacyLego.Domain.Aggregates.OrderPayment.ActualAmount#Price", b1 =>
                        {
                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("actual_currency");

                            b1.Property<decimal>("Sum")
                                .HasPrecision(15, 2)
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("actual_amount");
                        });

                    b.ComplexProperty(typeof(Dictionary<string, object>), "ExpectedAmount", "LegacyLego.Domain.Aggregates.OrderPayment.ExpectedAmount#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("expected_currency");

                            b1.Property<decimal>("Sum")
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("expected_amount");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order_payment");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransactionId")
                        .IsUnique()
                        .HasFilter("transaction_id IS NOT NULL");

                    b.ToTable("Order_payment", null, t =>
                        {
                            t.HasCheckConstraint("check_order_payment_status", "\"status\" IN ('Pending', 'Succeeded', 'Failed', 'Refunded', 'RefundRequested')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.Property<Guid>("OrderPaymentId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_payment_id");

                    b.Property<string>("CheckoutUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("checkout_url");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("expires_at_utc");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("external_id");

                    b.HasKey("OrderPaymentId")
                        .HasName("pk_external_session");

                    b.ToTable("External_session", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint")
                        .HasColumnName("quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "UnitPrice", "LegacyLego.Domain.ValueObjects.OrderItem.UnitPrice#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("currency_code");

                            b1.Property<decimal>("Sum")
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("unit_price");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order_item");

                    b.HasIndex("OrderId");

                    b.ToTable("Order_item", null, t =>
                        {
                            t.HasCheckConstraint("check_quantity", "\"quantity\" >= 1");

                            t.HasCheckConstraint("check_unit_price_status", "\"unit_price\" > 0");
                        });
                });

            modelBuilder.Entity("LegacyLego.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("Outbox_messages", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_payment_orders");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.OrderPayment", null)
                        .WithOne("ExternalSession")
                        .HasForeignKey("LegacyLego.Domain.ValueObjects.ExternalSession", "OrderPaymentId");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_order_items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Navigation("ExternalSession");
                });
#pragma warning restore 612, 618
        }
    }
}
```

---

```cs title="OrderContextModelSnapshot.cs"
// <auto-generated />
using System;
using System.Collections.Generic;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid")
                        .HasColumnName("client_id");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("currency_code");

                    b.Property<decimal?>("FrozenTotalSum")
                        .HasColumnType("numeric(15,2)")
                        .HasColumnName("frozen_total_sum");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "Address", "LegacyLego.Domain.Aggregates.Order.Address#OrderAddress", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("address_country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar(20)")
                                .HasColumnName("address_postal_code");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("address_street");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.ToTable("Order", null, t =>
                        {
                            t.HasCheckConstraint("check_order_frozen_total_sun", "\"frozen_total_sum\" >= 0");

                            t.HasCheckConstraint("check_order_status", "\"status\" IN ('PendingPayment', 'Paid', 'Cancelled', 'Expired', 'Refunded')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at_utc");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("transaction_id");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "ActualAmount", "LegacyLego.Domain.Aggregates.OrderPayment.ActualAmount#Price", b1 =>
                        {
                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("actual_currency");

                            b1.Property<decimal>("Sum")
                                .HasPrecision(15, 2)
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("actual_amount");
                        });

                    b.ComplexProperty(typeof(Dictionary<string, object>), "ExpectedAmount", "LegacyLego.Domain.Aggregates.OrderPayment.ExpectedAmount#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("expected_currency");

                            b1.Property<decimal>("Sum")
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("expected_amount");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order_payment");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransactionId")
                        .IsUnique()
                        .HasFilter("transaction_id IS NOT NULL");

                    b.ToTable("Order_payment", null, t =>
                        {
                            t.HasCheckConstraint("check_order_payment_status", "\"status\" IN ('Pending', 'Succeeded', 'Failed', 'Refunded', 'RefundRequested')");
                        });
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.Property<Guid>("OrderPaymentId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_payment_id");

                    b.Property<string>("CheckoutUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("checkout_url");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("expires_at_utc");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("external_id");

                    b.HasKey("OrderPaymentId")
                        .HasName("pk_external_session");

                    b.ToTable("External_session", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint")
                        .HasColumnName("quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.ComplexProperty(typeof(Dictionary<string, object>), "UnitPrice", "LegacyLego.Domain.ValueObjects.OrderItem.UnitPrice#Price", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("varchar(3)")
                                .HasColumnName("currency_code");

                            b1.Property<decimal>("Sum")
                                .HasColumnType("numeric(15,2)")
                                .HasColumnName("unit_price");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order_item");

                    b.HasIndex("OrderId");

                    b.ToTable("Order_item", null, t =>
                        {
                            t.HasCheckConstraint("check_quantity", "\"quantity\" >= 1");

                            t.HasCheckConstraint("check_unit_price_status", "\"unit_price\" > 0");
                        });
                });

            modelBuilder.Entity("LegacyLego.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("Outbox_messages", (string)null);
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_payment_orders");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.ExternalSession", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.OrderPayment", null)
                        .WithOne("ExternalSession")
                        .HasForeignKey("LegacyLego.Domain.ValueObjects.ExternalSession", "OrderPaymentId");
                });

            modelBuilder.Entity("LegacyLego.Domain.ValueObjects.OrderItem", b =>
                {
                    b.HasOne("LegacyLego.Domain.Aggregates.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_order_items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LegacyLego.Domain.Aggregates.OrderPayment", b =>
                {
                    b.Navigation("ExternalSession");
                });
#pragma warning restore 612, 618
        }
    }
}
```

---

### Options

```cs title="CacheOptions.cs"
using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class CacheOptions
{
    public const string SectionName = "CacheOptions";

    [Range(10, 30, ErrorMessage = "OrdersHistoryTtl должен быть от 10 до 30 минут.")]
    public int OrdersHistoryMinutesTtl { get; set; } = 10;

    [Range(10, 60, ErrorMessage = "OrderDetailsTtl должен быть от 10 до 60 минут.")]
    public int OrderDetailsMinutesTtl { get; set; } = 30;

    [Range(1, 7, ErrorMessage = "OrderGroupDaysTtl должен быть от 1 до 7 дней.")]
    public int OrderGroupDaysTtl { get; set; } = 1;
}
```

---

```cs title="DatabaseOptions.cs"
using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    [Required(ErrorMessage = "Строка подключения обязательна.")]
    public string ConnectionString { get; set; } = string.Empty;

    [Range(1, 60, ErrorMessage = "CommandTimeoutSeconds должен быть от 1 до 60 секунд.")]
    public int CommandTimeoutSeconds { get; set; } = 30;

    [Range(1, 10, ErrorMessage = "MaxRetryCount должен быть в диапазоне от 1 до 10.")]
    public int MaxRetryCount { get; set; } = 3;

    [Range(1, 30, ErrorMessage = "MaxRetryDelaySeconds должен быть в диапазоне от 1 до 30.")]
    public int MaxRetryDelaySeconds { get; set; } = 5;

    public bool EnableSensitiveDataLogging { get; set; }
    public bool EnableDetailedErrors { get; set; }
}
```

---

```cs title="HangfireOptions.cs"
using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class HangfireOptions
{
    public const string SectionName = "Hangfire";
    public const string CommandHangfireQueueName = "command";

    [Range(1, 60, ErrorMessage = "QueuePollInterval должен быть от 1 до 60 секунд.")]
    public int QueuePollInterval { get; set; } = 15;

    [Range(1, 20, ErrorMessage = "WorkerCount должен быть в диапазоне от 1 до 20.")]
    public int WorkerCount { get; set; } = 2;
}
```

---

```cs title="OutboxBackgroundWorkerOptions.cs"
using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class OutboxBackgroundWorkerOptions
{
    public const string SectionName = "OutboxBackgroundWorkerOptions";

    [Range(1, 60, ErrorMessage = "Период воркера должен быть от 1 до 60 секунд.")]
    public int SecondsPeriod { get; set; } = 2;

    [Range(1, 100, ErrorMessage = "За раз можно взять от 1 до 100 записей.")]
    public int TakeRecordsNum { get; set; } = 20;
}
```

---

```cs title="OutboxSerializerOptions.cs"
using LegacyLego.Infrastructure.Converters.JsonConverters;
using System.Text.Json;

namespace LegacyLego.Infrastructure.Options;

public static class OutboxSerializerOptions
{
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        Converters =
        {
            new CurrencyJsonConverter(),
            new PriceJsonConverter()
        }
    };
}
```

---

```cs title="PaymentProviderOptions.cs"
using System.ComponentModel.DataAnnotations;

namespace LegacyLego.Infrastructure.Options;

public sealed class PaymentProviderOptions
{
    public const string SectionName = "PaymentProviderOptions";

    [Required(ErrorMessage = "ApiBaseUrl is required")]
    public string ApiBaseUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "WebhookRoute is required")]
    public string WebhookRoute { get; set; } = string.Empty;

    [Required(ErrorMessage = "CheckoutPagePath is required")]
    public string CheckoutPagePath { get; set; } = string.Empty;

    [Range(1, 60, ErrorMessage = "ExpiresAtMinutes должен быть от 1 до 60 минут.")]
    public int ExpiresAtMinutes { get; set; } = 10;
}
```

---

### Outbox

```cs title="OutboxMessage.cs"
namespace LegacyLego.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    /// <summary>
    /// Конструктор для создания нового экземпляра OutboxMessage 
    /// в целях его дальнейшей записи в хранилище
    /// </summary>
    /// <param name="id"> идентификатор сообщения</param>
    /// <param name="type">тип сообщения</param>
    /// <param name="content">содержание сообщение json</param>
    /// <param name="occurredOnUtc">дата и время появления сообщения в формате utc</param>
    public OutboxMessage(Guid id, string type, string content, DateTime occurredOnUtc)
    {
        Id = id;
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }

    /// <summary>
    /// Приватный конструктор, используемый для материализации объекта OutboxMessage
    /// EF ORM системой в соответствии с конфигурациями
    /// </summary>
    private OutboxMessage() { }

    public Guid Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime OccurredOnUtc { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }
    public string? Error { get; set; }
}
```

---

### Repositories

```cs title="OrderRepository.cs"
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Common;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Repositories;

internal class OrderRepository(OrderContext context) : IOrderRepository
{
    public void Add(Order order) => context.Orders.Add(order);

    public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        return await context.Orders.Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<TResult?> GetOrderAsync<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
           context.Set<Order>()
           , specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> GetOrdersAsync<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
           context.Set<Order>()
           ,specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetOrdersCountAsync(Specification<Order, OrderId> specification, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
           context.Set<Order>()
           , specification)
            .CountAsync(cancellationToken);
    }
}
```

---

```cs title="PaymentRepository.cs"
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Repositories;

internal class PaymentRepository(OrderContext context) : IPaymentRepository
{
    public void Add(OrderPayment payment) => context.OrderPayments.Add(payment);

    public async Task<bool> ExistsSucceededAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
    await context.OrderPayments
        .AnyAsync(p => p.OrderId == orderId && p.Status == PaymentStatus.Succeeded, cancellationToken);

    public async Task<OrderPayment?> GetByOrderIdAsync(OrderId orderId,
        CancellationToken cancellationToken = default) => 
        await context.OrderPayments.FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);

    public async Task<OrderPayment?> GetByTransactionIdAsync(string transactionId, CancellationToken cancellationToken = default) =>
        await context.OrderPayments.FirstOrDefaultAsync(p => p.TransactionId == transactionId, cancellationToken);

    public async Task<OrderPayment?> GetPendingByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
        await context.OrderPayments.Where(p => p.OrderId == orderId && p.Status == PaymentStatus.Pending)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<OrderPayment?> GetByExternalSessionIdAsync(
        string externalSessionId, CancellationToken cancellationToken = default) =>
         await context.OrderPayments.FirstOrDefaultAsync(p => p.ExternalSession != null && p.ExternalSession.ExternalId == externalSessionId, cancellationToken);
}
```

---

### Services

```cs title="Base64JsonCursorSerializer.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Domain.Shared;
using System.Text;
using System.Text.Json;

namespace LegacyLego.Infrastructure.Services;

public class Base64JsonCursorSerializer : ICursorSerializer
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        IncludeFields = true
    };

    public string Serialize<T>(T cursorData) where T : struct
    {
        string json = JsonSerializer.Serialize(cursorData, SerializerOptions);

        byte[] bytes = Encoding.UTF8.GetBytes(json);
        return Convert.ToBase64String(bytes);
    }

    public Result<T> Deserialize<T>(string cursor) where T : struct
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cursor))
                return Result<T>.Failure(new Error("Cursor.Empty", "Курсор не может быть пустым"));

            byte[] bytes = Convert.FromBase64String(cursor);
            string json = Encoding.UTF8.GetString(bytes);

            var result = JsonSerializer.Deserialize<T>(json, SerializerOptions);

            return result.Equals(default(T))
                ? Result<T>.Failure(new Error("Cursor.Invalid", "Не удалось десериализовать данные курсора"))
                : Result<T>.Success(result);
        }
        catch (Exception)
        {
            return Result<T>.Failure(new Error("Cursor.Corrupted", "Токен курсора поврежден или невалиден"));
        }
    }
}
```

---

```cs title="MockPaymentProvider.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace LegacyLego.Infrastructure.Services;

public sealed class MockPaymentProvider : IPaymentProvider
{
    private readonly HttpClient _httpClient;
    private readonly TimeProvider _timeProvider;
    private readonly PaymentProviderOptions _options;

    public MockPaymentProvider(
        HttpClient httpClient,
        TimeProvider timeProvider,
        IOptions<PaymentProviderOptions> options)
    {
        _httpClient = httpClient;
        _timeProvider = timeProvider;
        _options = options.Value;
    }

    public async Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        Guid orderId,
        decimal amount,
        string currency,
        int scale,
        CancellationToken ct)
    {
        string externalSessionId = GenerateExternalSession();

        var expiresAtUtc = _timeProvider.GetUtcNow().AddMinutes(_options.ExpiresAtMinutes).UtcDateTime;

        var queryParams = new Dictionary<string, string?>
        {
            { "paymentId", paymentId.ToString() },
            { "orderId", orderId.ToString() },
            { "amount", amount.ToString($"F{scale}",System.Globalization.CultureInfo.InvariantCulture) },
            { "currency", currency },
            { "externalSessionId", externalSessionId }
        };

        string baseCheckoutUrl = new Uri(new Uri(_options.ApiBaseUrl), _options.CheckoutPagePath).ToString();
        string checkoutUrl = QueryHelpers.AddQueryString(baseCheckoutUrl, queryParams);

        var session = new PaymentSession(
            PaymentId: paymentId,
            ExternalSessionId: externalSessionId,
            CheckoutUrl: checkoutUrl,
            ExpiresAtUtc: expiresAtUtc
        );

        return Result<PaymentSession>.Success(session);
    }

    public async Task<Result> RequestRefundAsync(
        Guid orderId,
        decimal amount,
        string currency,
        string transactionId,
        CancellationToken ct)
    {
        await Task.Delay(500, ct);

        var payload = new ExternalStripeWebhookSimulation(
            OrderId: orderId,
            Amount: amount,
            Currency: currency,
            TransactionId: transactionId,
            Status: "refund");

        var response = await _httpClient.PostAsJsonAsync(_options.WebhookRoute, payload, ct);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure(new Error(
                "MockPayment.RefundFailed",
                $"Имитация вебхука возврата завершилась ошибкой: {response.StatusCode}"));
        }

        return Result.Success();
    }

    private string GenerateExternalSession() =>  $"ext_sess_{Guid.NewGuid():N}";


}

file sealed record ExternalStripeWebhookSimulation(
        Guid OrderId,
        decimal Amount,
        string Currency,
        string TransactionId,
        string Status);
```

---

## LegacyLego.Presentation

```xml title="LegacyLego.Presentation.csproj"
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>4c1ab31b-79ef-45cf-b0db-bef1bd60998f</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.9">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.ApiDescription.Server" Version="10.0.9">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Scalar.AspNetCore" Version="2.16.10" />
    <PackageReference Include="Serilog" Version="4.3.1" />
    <PackageReference Include="Serilog.AspNetCore" Version="10.0.0" />
    <PackageReference Include="Serilog.Expressions" Version="5.0.0" />
    <PackageReference Include="Serilog.Settings.Configuration" Version="10.0.1" />
  </ItemGroup>

	<PropertyGroup>
		<OpenApiGenerateDocumentsOnBuild>true</OpenApiGenerateDocumentsOnBuild>

		<OpenApiDocumentsDirectory>$(MSBuildProjectDirectory)</OpenApiDocumentsDirectory>
	</PropertyGroup>

	<ItemGroup>
    <ProjectReference Include="..\LegacyLego.Application\LegacyLego.Application.csproj" />
    <ProjectReference Include="..\LegacyLego.Domain\LegacyLego.Domain.csproj" />
    <ProjectReference Include="..\LegacyLego.Infrastructure\LegacyLego.Infrastructure.csproj" />
  </ItemGroup>

	<ItemGroup>
	  <Folder Include="wwwroot\" />
	</ItemGroup>

</Project>
```

---

```cs title="Program.cs"
using LegacyLego.Application;
using LegacyLego.Infrastructure;
using LegacyLego.Presentation.Middleware;
using LegacyLego.Presentation.OpenApi;
using LegacyLego.Presentation.Orders;
using LegacyLego.Presentation.Payments;
using Microsoft.AspNetCore.HttpOverrides;
using Scalar.AspNetCore;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Logging.ClearProviders();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

Serilog.Debugging.SelfLog.Enable(Console.Error);

try
{
    Log.Information("Запуск приложения LegacyLego...");

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        // превращает целочисленный указатель enum в строковое представление значения
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.AddApplication()
        .AddInfrastructure(configuration)
        .AddPresentationOpenApi();

    builder.Services.AddExceptionHandler<DynamicGlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddHealthChecks();

    var app = builder.Build();

    app.UseExceptionHandler(); // стоит самый первый в пайплайне

    app.UseForwardedHeaders(new ForwardedHeadersOptions // для Nginx
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();

        app.MapScalarApiReference("/docs/scalar", options =>
        {
            options.WithTitle("LegacyLego Documentation")
                .WithTheme(ScalarTheme.DeepSpace)
                .WithClassicLayout()
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }

    app.UseStaticFiles();

    app.MapHealthChecks("/healthz");

    app.MapOrdersEndpoints();
    app.MapPaymentEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение LegacyLego аварийно завершило работу во время запуска");
}
finally
{
    Log.CloseAndFlush(); // Гарантирует, что все логи из буфера долетят до инфраструктурной базы логгов перед закрытием
}
```

---

### Middleware

```cs title="DynamicGlobalExceptionHandler.cs"
using LegacyLego.Application.Abstractions.ExceptionHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LegacyLego.Presentation.Middleware;

public sealed class DynamicGlobalExceptionHandler : IExceptionHandler
{
    private readonly IEnumerable<IExceptionMapper> _mappers;
    private readonly ILogger<DynamicGlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IProblemDetailsService _problemDetailsService;

    public DynamicGlobalExceptionHandler(
        IEnumerable<IExceptionMapper> mappers,
        ILogger<DynamicGlobalExceptionHandler> logger,
        IWebHostEnvironment env,
        IProblemDetailsService problemDetailsService)
    {
        _mappers = mappers;
        _logger = logger;
        _env = env;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        AppFailureDescription? failureDescription = null;
        foreach (var mapper in _mappers)
        {
            if (mapper.TryMap(exception, out failureDescription) && failureDescription is not null)
                break;
        }

        failureDescription ??= new AppFailureDescription(
            ExceptionFailureKind.Unknown,
            "Внутренняя ошибка сервера",
            "Произошла непредвиденная ошибка в работе системы.");

        LogException(exception, failureDescription, httpContext);

        return await WriteProblemDetailsAsync(httpContext, failureDescription, cancellationToken);
    }

    private void LogException(Exception exception, AppFailureDescription failure, HttpContext context)
    {
        var logLevel = failure.Kind switch
        {
            ExceptionFailureKind.DomainLevelException => LogLevel.Critical,
            ExceptionFailureKind.InfrastructureLevelException => LogLevel.Error,
            _ => LogLevel.Error
        };

        _logger.Log(
            logLevel,
            exception,
            "Перехвачено исключение [{ErrorCode}]: {Title}. Маршрут: {Method} {Path}",
            failure.ErrorCode ?? "Core.Unknown",
            failure.Title,
            context.Request.Method,
            context.Request.Path);
    }

    private async ValueTask<bool> WriteProblemDetailsAsync(HttpContext context, AppFailureDescription failure, CancellationToken ct)
    {
        var statusCode = failure.Kind switch
        {
            ExceptionFailureKind.DomainLevelException => StatusCodes.Status500InternalServerError,
            ExceptionFailureKind.InfrastructureLevelException => StatusCodes.Status503ServiceUnavailable,
            ExceptionFailureKind.UnhandledNetworkLevelException => StatusCodes.Status502BadGateway,
            _ => StatusCodes.Status500InternalServerError
        };

        
        string publicDetail;

        if (_env.IsDevelopment())
        {
            publicDetail = failure.Detail;
        }
        else
        {
            publicDetail = failure.Kind switch
            {
                ExceptionFailureKind.InfrastructureLevelException => "Сервис временно недоступен. Пожалуйста, повторите попытку позже.",
                _ => $"Внутренняя ошибка сервера. Пжалуйста сообщите данный код техподдержке в случае обращения за помощью: {context.TraceIdentifier}" // TraceIdentifier пользователю, чтобы тот сообщил поддержке 
            };
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = _env.IsDevelopment() ? failure.Title : "Внутренняя ошибка сервера",
            Detail = publicDetail,
            Instance = context.Request.Path
        };

        if (_env.IsDevelopment() && !string.IsNullOrEmpty(failure.ErrorCode))
        {
            problemDetails.Extensions["errorCode"] = failure.ErrorCode;
        }

        var problemContext = new ProblemDetailsContext
        {
            HttpContext = context,
            ProblemDetails = problemDetails
        };

        return await _problemDetailsService.TryWriteAsync(problemContext);
    }
}
```

---

### OpenApi

```cs title="ApiMetadataTransformer.cs"
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LegacyLego.Presentation.OpenApi;

internal sealed class ApiMetadataTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info.Title = "LegacyLego E-Commerce API";
        document.Info.Description = "Внутреннее API интернет-магазина конструкторов Lego. " +
                                     "Обеспечивает работу с заказами, корзиной и платежными шлюзами.";

        // ПРАВИЛЬНЫЙ ВАРИАНТ: Берем имя документа из контекста .NET OpenAPI!
        // Если зарегистрирован документ "v1", то версия будет "v1"
        document.Info.Version = $"openapi.{context.DocumentName}";

        return Task.CompletedTask;
    }
}
```

---

```cs title="OpenApiExtensions.cs"
namespace LegacyLego.Presentation.OpenApi;

public static class OpenApiExtensions
{
    public const string ApiVersion = "v1";

    public static IServiceCollection AddPresentationOpenApi(this IServiceCollection services)
    {
        return services.AddOpenApi(ApiVersion, options =>
        {
            options.AddDocumentTransformer<ApiMetadataTransformer>();
        });
    }
}
```

---

### Orders

```cs title="OrderEndpoints.cs"
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Commands.Create;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Presentation.Orders.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace LegacyLego.Presentation.Orders;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var ordersGroup = app.MapGroup("/orders")
            .WithDisplayName("Orders")
            .WithDescription("Управление заказами")
            .WithTags("Orders");

        ordersGroup.MapPost("", Create);
        ordersGroup.MapGet("/active", GetActiveOrders); // Сделали явный чистый эндпоинт
        ordersGroup.MapGet("/history", GetOrdersHistory);
        ordersGroup.MapGet("/{orderId:guid}", GetOrderDetails);

        return app;
    }

    private static async Task<Results<Created<Guid>, BadRequest<ProblemDetails>>> Create(
        [FromBody] CreateOrderRequest request,
        ICommandDispatcher commandDispatcher,
        ClaimsPrincipal user,
        ILogger<Program> logger,
        HttpContext httpContext,
        CancellationToken ct)
    {
        var clientIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        // Временно для тестов, пока не настроен JWT:
        if (!Guid.TryParse(clientIdString, out var clientId))
        {
            clientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        var command = new CreateOrderCommand(
            ClientId: clientId,
            CurrencyCode: request.CurrencyCode,
            OrderAddress: request.OrderAddress,
            Items: request.Items
        );

        var result = await commandDispatcher.DispatchAsync(command, ct);

        if (result.IsFailure)
        {
            logger.LogWarning("Запрос отклонен. Код: {ErrorCode}. Детали: {Message}",
                result.Error.Code, result.Error.Message);

            return TypedResults.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Произошла ошибка обработки запроса",
                Detail = "Запрос не прошел валидацию. Подробности см. в параметре errorCode.",
                Instance = httpContext.Request.Path,
                // Передаем код ошибки для фронтенда:
                Extensions = { ["errorCode"] = result.Error.Code }
            });
        }

        return TypedResults.Created($"/orders/{result.Value}", result.Value);
    }

    private static async Task<IResult> GetActiveOrders(
        IQueryDispatcher queryDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var query = new GetActiveOrdersQuery(GetClientId(user));
        var result = await queryDispatcher.DispatchAsync(query, ct);

        return ToHttpResponse(result);
    }

    private static async Task<IResult> GetOrdersHistory(
        [AsParameters] OrderHistoryRequest request,
        IQueryDispatcher queryDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var query = new GetOrdersHistoryQuery(GetClientId(user), request);
        var result = await queryDispatcher.DispatchAsync(query, ct);

        return ToHttpResponse(result);
    }

    private static async Task<IResult> GetOrderDetails(
        [FromRoute] Guid orderId,
        IQueryDispatcher queryDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var query = new GetOrderDetailsQuery(GetClientId(user), orderId);
        var result = await queryDispatcher.DispatchAsync(query, ct);

        return ToHttpResponse(result);
    }

    private static Guid GetClientId(ClaimsPrincipal user)
    {
        var clientIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        // Временно для тестов, пока не настроен JWT:
        if (!Guid.TryParse(clientIdString, out var clientId))
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        return clientId;
    }

    private static IResult ToHttpResponse<T>(Result<T> result)
    {
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }
}
```

---

#### Dto

```cs title="CreateOrderRequest.cs"
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Presentation.Orders.Dto;

public sealed record CreateOrderRequest(
    string CurrencyCode,
    OrderAddressDto OrderAddress,
    List<OrderItemDto> Items);
```

---

### Payments

```cs title="PaymentEndpoints.cs"
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Errors;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Commands.StartPayment;
using LegacyLego.Presentation.Mock.Common.Dto.Request;
using LegacyLego.Presentation.Payments.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LegacyLego.Presentation.Payments;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var mockGroup = app.MapGroup("/mock")
            .WithDisplayName("Payment")
            .WithDescription("Тестировочные эндпоинты оплаты заказа")
            .WithTags("Payments");

        mockGroup.MapPost("/api/webhooks/payment", HandleWebhook);
        mockGroup.MapPost("/{orderId:guid}/pay", StartPayment);

        return app;
    }

    private static async Task<Results<
        Ok<ProcessPaymentDetails>,
        BadRequest<ProblemDetails>,
        Conflict<ProblemDetails>>> HandleWebhook(
            [FromBody] PaymentProviderWebhookRequest request,
            ICommandDispatcher commandDispatcher,
            CancellationToken ct)
    {
        ProcessPaymentWebhookCommand command;
        try
        {
            command = PaymentWebhookMapper.MapToPaymentWebhookCommand(request);
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Request Status",
                Detail = ex.Message
            });
        }

        var result = await commandDispatcher.DispatchAsync(command, ct);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                ProcessPaymentErrors.InvalidAmountCode or
                ProcessPaymentErrors.UnknownStatusCode =>
                    TypedResults.BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                ProcessPaymentErrors.TransactionConflictCode =>
                    TypedResults.Conflict(new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                ProcessPaymentErrors.TotalPricesMismatchCode =>
                    TypedResults.Conflict(new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                _ => TypedResults.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = error.Code,
                    Detail = error.Message
                })
            };
        }

        return TypedResults.Ok(result.Value);
    }

    private static async Task<Results<
    Ok<StartPaymentResponse>,
    ForbidHttpResult,
    Conflict<ProblemDetails>,
    NotFound<ProblemDetails>,
    BadRequest<ProblemDetails>>> StartPayment(
        [FromRoute] Guid orderId,
        ICommandDispatcher commandDispatcher,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        // ⌚ Временно для тестов, пока не настроен JWT:
        var clientIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(clientIdString, out var clientId))
        {
            clientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        var command = new StartOrderPaymentCommand(OrderId: orderId, ClientId: clientId);

        var result = await commandDispatcher.DispatchAsync(command, ct);

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.Code switch
            {
                OrderApplicationErrors.UnauthorizedAccessToOrderByClientIdCode =>
                    TypedResults.Forbid(),

                StartOrderPaymentErrors.OrderIsNotInPendingPaymentCode or
                StartOrderPaymentErrors.ForOrderIsAlreadyExistsSuccessedPaymentCode =>
                    TypedResults.Conflict(new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                StartOrderPaymentErrors.CanNotFindPendingPaymentAfterCheckConstraintCode =>
                    TypedResults.NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = error.Code,
                        Detail = error.Message
                    }),

                _ => TypedResults.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = error.Code,
                    Detail = error.Message
                })
            };
        }

        var details = result.Value;

        var response = new StartPaymentResponse(
            CheckoutUrl: details.Session.CheckoutUrl,
            ExpiresAtUtc: details.Session.ExpiresAtUtc
        );

        return TypedResults.Ok(response);
    }
}
```

---

```cs title="PaymentWebhookMapper.cs"
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Enums;
using LegacyLego.Presentation.Mock.Common.Dto.Request;

namespace LegacyLego.Presentation.Payments;

public static class PaymentWebhookMapper
{
    private const string REFUND_STATUS = "refund";
    private const string SUCCESS_STATUS = "success";
    private const string FAILED_STATUS = "fail";

    public static ProcessPaymentWebhookCommand MapToPaymentWebhookCommand(PaymentProviderWebhookRequest request)
    {
        var webhook = new PaymentWebhook(
            ExternalSessionId: request.ExternalSessionId,
            TransactionId: request.TransactionId,
            OrderId: request.OrderId,
            Amount: request.Amount,
            Currency: request.Currency,
            Status: MapRequestStatusToPaymentStatus(request.Status));

        return new ProcessPaymentWebhookCommand(webhook);
    }

    private static PaymentStatus MapRequestStatusToPaymentStatus(string requestStatus) => requestStatus switch
    {
        REFUND_STATUS => PaymentStatus.Refunded,
        SUCCESS_STATUS => PaymentStatus.Succeeded,
        FAILED_STATUS => PaymentStatus.Failed,
        _ => throw new ArgumentException(
            $"Unsupported status value: '{requestStatus}'. Allowed values: {SUCCESS_STATUS}, {FAILED_STATUS}, {REFUND_STATUS}")
    };
}
```

---

#### Dto

```cs title="PaymentProviderWebhookRequest.cs"
namespace LegacyLego.Presentation.Mock.Common.Dto.Request;

public sealed record PaymentProviderWebhookRequest(
    string ExternalSessionId,
    string? TransactionId, // Nullable! Может быть null, если транзакция не создалась
    Guid OrderId,
    decimal Amount,
    string Currency,
    string Status); // "success", "failed", "refunded"
```

---

```cs title="StartPaymentResponse.cs"
namespace LegacyLego.Presentation.Payments.Dto;

public sealed record StartPaymentResponse(
    string CheckoutUrl,
    DateTime ExpiresAtUtc);
```

---


