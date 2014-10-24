## Altman

```
免责申明：本程序仅供学习和研究！
请使用者遵守国家相关法律法规！
由于使用不当造成的后果本人不承担任何责任！
```

### 介绍
**Altman** 是一款渗透测试软件，网站托管于Github Pages，[点击此处移步官网](http://altman.keepwn.com)。

截至目前，它可以：

- Webshell模块：采用xml定义的方式，可自定义脚本类型和脚本功能，自定义加密/编码
	- Shell管理插件
	- 命令执行插件
	- 文件管理插件
	- 数据库管理插件
	- 支持的脚本类型有：asp、aspx、php、jspFull、python
- 编码器插件
- 插件管理中心
- 自定义插件：支持使用C#或者IronPython来编写插件或者服务
- 插件服务机制：插件可以调用其它插件提供的服务

### 运行
**Altman** 基于.Net4.0，依托Eto.Form可以完美运行在Windows、Linux、Mac等多个平台。

- 在`Windows`下，
	- 运行Altman.winform.exe，需要安装.Net4.0
	- 运行Altman.gtk.exe，需要安装.Net4.0和gtk-sharp2
- 在`Linux`下，
	- 运行Altman.gtk.exe，需要安装Mono(>=3.2.8),libgdiplus和gtk-sharp2
- 在`OS X`下，
	- 运行Altman.gtk.exe，需要安装Mono和gtk-sharp2
	- 运行Altman.mac，需要安装Xcode

如果开启IronPython支持，则需要安装IronPython（或者自带安装）

### 插件
**Altman**采用了MEF插件架构，同时支持IronPython。

- 采用C#编写插件/服务
- 采用IronPython编写插件/服务

### 版权
本程序使用的是GPLv2协议，具体细节请参照根目录下的`LICENSE`文件。

### 改进及建议

[提交问题](https://github.com/keepwn/Altman/issues) OR
[改进代码](https://github.com/keepwn/Altman/pulls)

你也可以选择加入技术讨论群:  331451473