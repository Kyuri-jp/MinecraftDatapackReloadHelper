# MinecraftDatapackReloadHelper
[![build](https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper/actions/workflows/dotnet.yml)
![GitHub Release](https://img.shields.io/github/v/release/Kyuri-jp/MinecraftDatapackReloadHelper)
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/Kyuri-jp/MinecraftDatapackReloadHelper/total)
![Stars](https://img.shields.io/github/stars/Kyuri-jp/MinecraftDatapackReloadHelper)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Kyuri-jp/MinecraftDatapackReloadHelper)
![GitHub License](https://img.shields.io/github/license/Kyuri-jp/MinecraftDatapackReloadHelper)


データパックの再読み込みをちょっと楽にします
## Commands
|Command|Discription|
|-----|----|
|appsetting|Rconなどの設定を変更できます|
|connectiontest|Rconの接続をテストします|
|help|コマンドの説明を表示します|
|pathsetting|データパックのパスを変更できます|
|reload|データパックをコピーした後、データパックを再読み込みします|
|showsetting|設定を表示します|
|terminal|コマンドを実行できるターミナルを起動します|
|upload|ワールドフォルダをZip形式で書き出します|

### CommandArgs
引数は`コマンド -なんとか`という形で使用します。

#### reload
|Args|Discription|
|----|----|
|copyonly|RconによるReloadコマンドの送信を行わず、データパックのコピーのみを行います|

### version
|Args|Discription|
|----|----|
|updatecheck|最新のリリースのタグを確認します|

### upload
|Args|Discription|
|----|----|
|additional|フォルダ名に追加で文字列を追加します|
|nonclean|dataやstatsの削除を無効化します|
|notopen|書き出し後のフォルダ表示を無効化します|

## .NET
このツールは.NET8を使用しています。

本ツールを使用する場合は.NET8の[ランタイム](https://dotnet.microsoft.com/ja-jp/download/dotnet/8.0)が必要です。

## Libraries
- CoreRCON v5.4.1 / MIT License Copyright (c) 2017 Scott Kaye
- System.Configuration.ConfigurationManager v9.0.0 / MIT License Copyright (c) .NET Foundation and Contributors

## License
このツールはMIT Licenseの下で配布されます。

This tool is released by MIT License
> MIT License Copyright (c) 2024 Kyuri
