## Altman3
> 免责申明：本程序仅供学习和研究！
> 请使用者遵守国家相关法律法规！
> 由于使用不当造成的后果本人不承担任何责任！

### 介绍
**Altman3** 是一款渗透测试软件，网站托管于Github Pages，[点击此处移步官网](http://altman.keepwn.com)。

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
- 自定义插件：支持使用C#或者IronPython来编写插件或者服务
- 插件服务机制：插件可以调用其它插件提供的服务

### 编译
1. 新建`Build`,  `Build\Bin`,  `Build\Plugins` 目录
2. 复制`Resources\RunNeed`目录下的所有文件到`Build`目录
3. 复制`Libraries\IronPython`目录下所有文件到`Build\Bin`目录（如果本机已经安装IronPython，则跳过此步骤）
4. 复制`Libraries\Sqlite3`目录下所有文件到`Build`目录
5. 使用VS2012（或以上版本）或者MonoDevelop进行编译
6. **如果本机是Linux或者Mac**，则在编译插件的时候可能会报错
  由于linux或者mac下不支持`copy`命令，所以
  - 你可以将*.csproj文件中的`copy`改为`cp`，然后重新再编译
  - 或者你也可以忽略这个错误，手动将编译好的插件dll复制到`Plugins`目录下，路径务必类似于`Plugins\ShellCmder\ShellCmder.dll`
7. **如果需要编译成Mac版本**，则在Debug或运行前，需要手动将`Build`目录下所有文件（除Altman.Mac文件）复制到`Build\Altman.Mac\Contents\MonoBundle`目录下


#### examples

##### Windows(cygwin reference to the following)

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
# run  batch.bat
# Using VS2012 (or the latest version) Open [Source / SecurityTools.sln] compiler
```

##### Linux
> Installation mono environment:

> Ubuntu or Debian

> `sudo apt-get install mono-devel mono-complete monodevelop`

> Other linux : <<http://www.mono-project.com/download/>>

```sh
git clone <https://github.com/keepwn/Altman.git>
cd Altman/
./batch
monodevelop Source/SecurityTools.sln  
# Use monodevelop compile Altman
```

**Compiled output:**
- Altman/Build/Altman.Gtk.exe (gtk)
- Altman/Build/Altman.WinForm.exe (windows)




### 运行
**Altman3** 基于.Net4.0，依托[Eto.Form](https://github.com/picoe/Eto)可以完美运行在Windows、Linux、Mac等多个平台。

- 在`Windows`下，
	- 双击运行Altman.Winform.exe，需要安装.Net4.0
	- 双击运行Altman.Gtk.exe，需要安装.Net4.0和[gtk-sharp2](http://download.xamarin.com/GTKforWindows/Windows/gtk-sharp-2.12.25.msi)
- 在`Linux`下，
	- 命令行下运行`mono Altman.Gtk.exe`，需要安装Mono(>=3.2.8)，libgdiplus和[gtk-sharp2](https://github.com/mono/gtk-sharp/releases/tag/gtk-sharp-2.12.27)
- 在`OS X`下，
	- 命令行下运行`mono Altman.Gtk.exe`，需要安装Mono和[gtk-sharp2](https://github.com/mono/gtk-sharp/releases/tag/gtk-sharp-2.12.27)
	- 双击运行Altman.Mac，需要安装Mono
- [**如何更简单的安装mono环境**](http://www.mono-project.com/download/)
- 如果需要开启IronPython支持，则需要安装[IronPython](http://ironpython.codeplex.com/)（如果程序已经自带IronPython，则不需要额外安装）
- 常见错误，请访问[FAQ](https://github.com/keepwn/Altman/wiki/FAQ)

### 插件
**Altman3**采用了MEF插件架构，同时支持IronPython。

- 采用C#编写插件/服务
- 采用IronPython编写插件/服务

**访问[Wiki](https://github.com/keepwn/Altman/wiki)来获取关于插件开发的文档**

### 版权
本程序使用的是GPLv2协议，具体细节请参照根目录下的`LICENSE`文件。

### 改进及建议

[提交问题](https://github.com/keepwn/Altman/issues) OR [改进代码](https://github.com/keepwn/Altman/pulls)

你也可以选择加入技术讨论群:  331451473
