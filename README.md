## Altman3

[![Altman version][altman-image]][altman-url]
[![.net required version][.net-image]][.net-url]
[![mono required version][mono-image]][mono-url]
[![gtksharp required version][gtksharp-image]][gtksharp-url]

[Join The Forums][altman-forums-url] | [Official Website][altman-website-url] | [中文版Readme ][readme-cn-url]

> Disclaimer: This program is only for learning and research!
> Users shall comply with relevant laws and regulations of the state!
> I shall not have any legal liability for improper use!

### Introduction
**Altman3** is a penetration testing software, which is web-hosted on `Github Pages`.

Up to now, the software is capable of:

- Webshell module: the xml definition is adopted for customized script type and function, as well as encryption/encoding.
    - Shell management plugin
	- Command execution plugin
	- File management plugin
	- Database management plugin
	- Script types supported include: asp, aspx, php, jspFull, python.
- Encoder plugin
- IP address query plugin
- Plugin management center
- Custom plugin: support for the use of `C#` or `IronPython` to program plugins or services
- Plugin service mechanism: the plugin can recall service of other plugins.

### Compile
1. Create `Build`, `Build\Bin`, `Build\Plugins`, `Build\Services` directory
2. Copy all the files under the `Resources\RunNeed` directory to the `Build` directory
3. Copy all the files under the `Libraries\IronPython` directory to the `Build\Bin` directory (if you have already installed `IronPython` on the host, skip this step).
4. Copy all the files under the `Libraries\Sqlite3` directory to the `Build` directory
5. Use `VS2012` (or higher version) or `MonoDevelop` to compile
6. **For host of Linux or Mac**, errors may be reported during plugin compiling as windows commands are incompatible in Linux or Mac.
  - Thus you can change the word `copy` in file `*.csproj` to `cp`,and the word `call` to `sh`, and then recompile.
  - Or you can ignore this error and manually copy the compiled dll plugin to the `Plugins` directory, where the path must be similar to the `Plugins\ShellCmder\ShellCmder.dll`
7. **If you need to compile a Mac version**, manually copy all files (except `Altman.Mac`) under the `Build` directory to the `Build\Altman.Mac\Contents\MonoBundle` directory before Debug or running.


#### examples

##### Windows(cygwin reference to the following)

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
# Run  batch-on-windows.bat
# Use VS2012 (or the latest version) to compile Altman
```

##### Linux
> Installation mono environment: <br/>
> Ubuntu or Debian <br/>
> `sudo apt-get install mono-devel mono-complete monodevelop` <br/>
> Other linux : <<http://www.mono-project.com/download/>>

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/ && chmod +x batch-on-linux.sh
./batch-on-linux.sh
monodevelop Source/SecurityTools.sln  
# Use monodevelop to compile Altman
```

##### Mac

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
./batch-on-mac-beforeBuild.sh
# Use monodevelop to compile Altman
# After build success
./batch-on-mac-afterBuild.sh
```

**Compiled output:**
- Altman/Build/Altman.Gtk.exe (gtk)
- Altman/Build/Altman.WinForm.exe (windows)
- Altman/Build/Altman.Mac (mac)



#### Running
**Altman3** is based on `.Net4.0`, can be perfect run in `Windows`, `Linux`, `Mac` and other platforms via [Eto.Form][eto-url].

- For `Windows`,
	- Double click to run `Altman.Winform.exe`; installment of `.Net4.0` is required.
	- Double click to run `Altman.Gtk.exe`; installment of `.Net4.0` and [gtk-sharp2][gtksharp-win-url] is required.
- For `Linux`,
	- Run `mono Altman.Gtk.exe` under command line; installment of `Mono`, `libgdiplus` and [gtk-sharp2][gtksharp-url] is required.
- For `OS X`,
	- Run `mono Altman.Gtk.exe` under command line; installment of `Mono` and [gtk-sharp2][gtksharp-url] is required.
	- Double click to run `Altman.Mac`, installment of `Mono` is required.
- [**How to install the mono environment in a more simple way**][mono-install-url]
- If you need to enable the `IronPython` support, you need to install [IronPython][ironpython-url] (if the program already includes `IronPython`, additional installment is not necessary)
- For common errors, please visit [FAQ][faq-url]

### Plugins
**Altman3** uses the MEF plugin architecture and also supports `IronPython`.

- Adopt `C#` to compile plugins/services
- Adopt `IronPython` to compile plugins/services

**Visit [wiki][wiki-url] to get the documentation on plugin development**

### Copyright
This program is subject to the GPLv2 protocol; please refer to the [LICENSE](LICENSE) file under the root directory for details.

### Improvement and suggestions
[Submit question][issues-url] OR [improve code][pulls-url]


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
[readme-cn-url]: README.cn.md
[eto-url]: https://github.com/picoe/Eto
[gtksharp-win-url]: http://download.xamarin.com/GTKforWindows/Windows/gtk-sharp-2.12.25.msi
[mono-install-url]: http://www.mono-project.com/download/
[ironpython-url]: http://ironpython.codeplex.com/
[faq-url]: https://github.com/keepwn/Altman/wiki/FAQ
[wiki-url]: https://github.com/keepwn/Altman/wiki
[issues-url]: https://github.com/keepwn/Altman/issues
[pulls-url]: https://github.com/keepwn/Altman/pulls
