# MinecraftDatapackReloadHelper
[![build](https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper/actions/workflows/dotnet.yml)
![GitHub Release](https://img.shields.io/github/v/release/Kyuri-jp/MinecraftDatapackReloadHelper)
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/Kyuri-jp/MinecraftDatapackReloadHelper/total)
![Stars](https://img.shields.io/github/stars/Kyuri-jp/MinecraftDatapackReloadHelper)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Kyuri-jp/MinecraftDatapackReloadHelper)
![GitHub License](https://img.shields.io/github/license/Kyuri-jp/MinecraftDatapackReloadHelper)


データパックの再読み込みをちょっと楽にします
## Commands
コマンド、引数の大文字小文字は区別されません。

|Command|Discription|
|-----|----|
|ConnectionTest|Rconの接続をテストします|
|Exit|アプリを終了します|
|Extract|ワールドフォルダをZip形式で書き出します|
|Help|コマンドの説明を表示します|
|Java|Javaに関する操作を行います|
|Reload|データパックをコピーした後、データパックを再読み込みします|
|Server|サーバーに関する操作を行います|
|Terminal|コマンドを実行できるターミナルを起動します|
|Setting|Rconなどの設定を変更できます|
|Version|現在のバージョンを表示します|

### CommandArgs
引数は`command --args=[<value>]`という形で使用します。

### Extract
|Args|Discription|Syntax|
|----|----|----|
|Additional|フォルダ名に追加で文字列を追加します|--additional=[\<string>]|
|Extractdatapack|フォルダ内のデータパックを全て圧縮して出力します|--extractdatapack|
|CustomPath|書き出す対象のフォルダを指定します|--custompath=[\<path>]|
|NonClean|dataやstatsの削除を無効化します|--nonclean|
|NotOpen|書き出し後のフォルダ表示を無効化します|--notopen|

#### Help
|Args|Discription|Syntax|
|----|----|----|
|More|コマンドの詳細を表示します|--more=[\<command>]|

#### Java
このコマンドは引数を必ず指定してください

|Args|Discription|Syntax|
|----|----|----|
|GetInstalledJava|`JAVA_HOME`変数に登録されているJavaバイナリと同じディレクトリにあるJavaを取得します|--getinstalledjava|

#### Reload
|Args|Discription|Syntax|
|----|----|----|
|Copyonly|RconによるReloadコマンドの送信を行わず、データパックのコピーのみを行います|--copyonly|

#### Server
引数無しでコマンドを使用するとサーバーを起動できます

|Args|Discription|Syntax|
|----|----|----|
|InvokeConfig|サーバーフォルダ内に生成された`mdrh.ujv`ファイルの情報を無視します|--invokeconfig|
|RemoveConfig|サーバーフォルダ内に生成された`mdrh.ujv`ファイルを削除します|--removeconfig|
|Setting|`server.properties`の内容を編集します|--setting=[\<keys>]|
|Show|`server.properties`の内容を表示します。値を指定しない場合は全ての設定が表示されます|--setting --show=[\<key>]|

#### Setting
|Args|Discription|Syntax|
|----|----|----|
|Auto|IPアドレスやポートを自動で設定します|--rcon --auto|
|Rcon|Rconの設定を変更します|--rcon|
|Path|パスの設定を変更します|--path|
|Show|設定を表示します|--show=[\<category>]|

### Version
|Args|Discription|Syntax|
|----|----|----|
|UpdateCheck|最新のリリースのタグを確認します|--updatecheck|


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
