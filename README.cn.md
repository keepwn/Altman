## Altman3

[![Altman version][altman-image]][altman-url]
[![.net required version][.net-image]][.net-url]
[![mono required version][mono-image]][mono-url]
[![gtksharp required version][gtksharp-image]][gtksharp-url]

[加入讨论][altman-forums-url] | [官方主页][altman-website-url] | [Readme in English ][readme-en-url]

> 免责申明：本程序仅供学习和研究！
> 请使用者遵守国家相关法律法规！
> 由于使用不当造成的后果本人不承担任何责任！

### 介绍
**Altman3** 是一款渗透测试软件，网站托管于`Github Pages`。

截至目前，它可以：

- Webshell模块：采用xml定义的方式，可自定义脚本类型和脚本功能，自定义加密/编码
	- Shell管理插件
	- 命令执行插件
	- 文件管理插件
	- 数据库管理插件
	- 支持的脚本类型有：asp、aspx、php、jspFull、python
- 编码器插件
- IP地址查询插件
- 插件管理中心
- 自定义插件：支持使用`C#`或者`IronPython`来编写插件或者服务
- 插件服务机制：插件可以调用其它插件提供的服务

### 编译
1. 新建`Build`,  `Build\Bin`,  `Build\Plugins`, `Build\Services`目录
2. 复制`Resources\RunNeed`目录下的所有文件到`Build`目录
3. 复制`Libraries\IronPython`目录下所有文件到`Build\Bin`目录（如果本机已经安装IronPython，则跳过此步骤）
4. 复制`Libraries\Sqlite3`目录下所有文件到`Build`目录
5. 使用`VS2012`（或以上版本）或者`MonoDevelop`进行编译
6. **如果本机是Linux或者Mac**，则在编译插件的时候可能会报错
  由于linux或者mac下不兼容windows命令，所以
  - 你可以将`*.csproj`文件中的`copy`改为`cp`，`call`改为`sh`，然后重新编译
  - 或者你也可以忽略这个错误，手动将编译好的插件dll复制到`Plugins`目录下，路径务必类似于`Plugins\ShellCmder\ShellCmder.dll`
7. **如果需要编译成Mac版本**，则在Debug或运行前，需要手动将`Build`目录下所有文件（除`Altman.Mac`文件）复制到`Build\Altman.Mac\Contents\MonoBundle`目录下


#### examples

##### Windows(cygwin reference to the following)

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
# run  batch-on-windows.bat
# Use VS2012 (or the latest version) to compile Altman
```

##### Linux
> Installation mono environment: <br/>
> Ubuntu or Debian <br/>
> `sudo apt-get install mono-devel mono-complete monodevelop` <br/>
> Other linux : <<http://www.mono-project.com/download/>>

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
./batch-on-linux.sh
monodevelop Source/SecurityTools.sln  
# Use monodevelop to compile Altman
```

##### Mac

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
./batch-on-mac-beforeBuild.sh
# Use monodevelop compile Altman
# After build success
./batch-on-mac-afterBuild.sh
```

**Compiled output:**
- Altman/Build/Altman.Gtk.exe (gtk)
- Altman/Build/Altman.WinForm.exe (windows)
- Altman/Build/Altman.Mac (mac)



### 运行
**Altman3** 基于`.Net4.0`，依托[Eto.Form][eto-url]可以完美运行在`Windows`、`Linux`、`Mac`等多个平台。

- 在`Windows`下，
	- 双击运行`Altman.Winform.exe`，需要安装`.Net4.0`
	- 双击运行`Altman.Gtk.exe`，需要安装`.Net4.0`和[gtk-sharp2][gtksharp-win-url]
- 在`Linux`下，
	- 命令行下运行`mono Altman.Gtk.exe`，需要安装`Mono`，`libgdiplus`和[gtk-sharp2][gtksharp-url]
- 在`OS X`下，
	- 命令行下运行`mono Altman.Gtk.exe`，需要安装`Mono`和[gtk-sharp2][gtksharp-url]
	- 双击运行`Altman.Mac`，需要安装`Mono`
- [**如何更简单的安装mono环境**][mono-install-url]
- 如果需要开启`IronPython`支持，则需要安装[IronPython][ironpython-url]（如果程序已经自带`IronPython`，则不需要额外安装）
- 常见错误，请访问[FAQ][faq-url]

### 插件
**Altman3**采用了MEF插件架构，同时支持`IronPython`。

- 采用`C#`编写插件/服务
- 采用`IronPython`编写插件/服务

**访问[wiki][wiki-url]来获取关于插件开发的文档**

### 版权
本程序使用的是GPLv2协议，具体细节请参照根目录下的[LICENSE](LICENSE)文件。

### 改进及建议

[提交问题][issues-url] OR [改进代码][pulls-url]

你也可以选择加入技术讨论群: 331451473

[altman-image]: https://img.shields.io/badge/Release-v3.0.1-brightgreen.svg
[altman-url]: https://github.com/keepwn/Altman
[.net-image]: https://img.shields.io/badge/.Net-4.0-blue.svg
[.net-url]: http://www.microsoft.com/zh-cn/download/details.aspx?id=17718
[mono-image]: https://img.shields.io/badge/Mono-v3.2.6+-blue.svg
[mono-url]: http://www.mono-project.com/
[gtksharp-image]: https://img.shields.io/badge/Gtksharp-v2.0+-blue.svg
[gtksharp-url]: https://github.com/mono/gtk-sharp/
[altman-forums-url]: https://groups.google.com/forum/#!forum/altman-tool
[altman-website-url]: http://altman.keepwn.com
[readme-en-url]: README.md
[eto-url]: https://github.com/picoe/Eto
[gtksharp-win-url]: http://download.xamarin.com/GTKforWindows/Windows/gtk-sharp-2.12.25.msi
[mono-install-url]: http://www.mono-project.com/download/
[ironpython-url]: http://ironpython.codeplex.com/
[faq-url]: https://github.com/keepwn/Altman/wiki/FAQ
[wiki-url]: https://github.com/keepwn/Altman/wiki
[issues-url]: https://github.com/keepwn/Altman/issues
[pulls-url]: https://github.com/keepwn/Altman/pulls
