## 欢迎使用Altman工具##


### 它能做什么 ###
**Altman** 是一个webshell利用工具。

截至目前，它可以：

- 管理webshell
- 自定义代理
- 自定义HttpHeader
- 自定义发包命令
- 自定义脚本类型和脚本功能
- 自定义webshell通信方式（get、post、cookie、httpHeader）
- 自定义插件
- 命令执行
- 文件管理

### 如何运行它 ###
**Altman** 基于.Net4.0，兼容Mono，可以运行在windows、linux等平台。

在windows下，需要安装.Net4.0，点击`Altman.exe`即可运行。

在linux下，需要安装Mono(>=3.2.8)以及libgdiplus，在终端输入命令`mono Altman.exe`即可运行。

### 如何扩展它的功能 ###
**Altman**采用了mef插件架构，脚本类型（.type）、脚本功能(.func)也是以文件形式保存。

你可以：
- 添加或修改脚本类型，使它支持更多脚本类型（asp、aspx、php、jsp、python等）
- 添加或修改脚本功能，以供插件调用
- 扩展插件，只需要引用特定接口dll


### 能否透露开发计划 ###
当然可以，目前的开发计划有：

- 数据库管理插件
- 简易浏览器插件
- 多个代理自动切换功能


### 是否使用、修改它的代码 ###
本程序使用的是GPL协议，具体细节请参照根目录下的`LICENSE`文件，谢谢。


### 如何联系我 ###
我的邮箱keepwn@gmail.com

- 如果你有任何问题
- 如果你有任何建议
- 如果你实现了新插件或者新的脚本类型，或者对程序有帮助的地方

欢迎来信。