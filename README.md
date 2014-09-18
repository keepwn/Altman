## Altman网站管理工具

```
免责申明：本程序仅供学习和研究！
请使用者遵守国家相关法律法规！
由于使用不当造成的后果本人不承担任何责任！
```

### 介绍
**Altman** 是一款网站管理工具，网站托管于Github Pages，[点击此处移步官网](http://altman.keepwn.com)。

截至目前，它可以：

- 自定义代理
- 自定义HttpHeader
- 自定义脚本类型和脚本功能
- 自定义加密/编码
- 自定义插件
- Shell管理插件
- 命令执行插件
- 文件管理插件
- 数据库管理插件
- 插件管理中心

目前支持的脚本类型有：asp、aspx、php、jspFull、python

### 运行
**Altman** 基于.Net4.0，兼容Mono，可以完美运行在Windows、Linux、Mac等多个平台。

在`Windows`下，需要安装.Net4.0，点击`Altman.WinForm.exe`即可运行。

在`Linux`下，需要安装Mono(>=3.2.8)，libgdiplus和gtk-sharp2，在终端输入命令`mono Altman.Gtk.exe`即可运行。

在`Mac`下，需要安装Mono，在终端输入命令`mono Altman.Mac.exe`即可运行。

### 扩展
**Altman**采用了MEF插件架构，脚本类型（.type）、脚本功能(.func)也是以文件形式保存。

你可以：

- 添加或修改脚本类型，使它支持更多脚本类型（asp、aspx、php、jsp、python等）
- 扩展插件，只需要引用特定接口dll

### 版权
本程序使用的是GPLv2协议，具体细节请参照根目录下的`LICENSE`文件。

### 改进及建议

[提交问题](https://github.com/keepwn/Altman/issues) OR
[改进代码](https://github.com/keepwn/Altman/pulls)

你也可以选择加入技术讨论群:  331451473
