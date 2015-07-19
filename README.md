## Altman3
> Disclaimer: This program is only for learning and research! 
> Users shall comply with relevant laws and regulations of the state!
> I shall not have any legal liability for improper use!  

> 免责申明：本程序仅供学习和研究！
> 请使用者遵守国家相关法律法规！
> 由于使用不当造成的后果本人不承担任何责任


### Introduction
**Altman3** is a penetration testing software, which is web-hosted on Github Pages; [click here to visit the official website](http://altman.keepwn.com).

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
- Custom plugin: support for the use of C# or IronPython to program plugins or services
- Plugin service mechanism: the plugin can recall service of other plugins.

### Compile
1. Create `Build`, `Build\Bin`, `Build\Plugins`, `Build\Services` directory
2. Copy all the files under the `Resources\RunNeed` directory to the `Build` directory
3. Copy all the files under the `Libraries\IronPython` directory to the `Build\Bin` directory (if you have already installed IronPython on the host, skip this step).
4. Copy all the files under the `Libraries\Sqlite3` directory to the `Build` directory
5. Use VS2012 (or higher version) or MonoDevelop to compile
6. **If the host uses Linux or Mac**, it may report errors due to the incompatibleness in command between Linux or Mac and windows.

  - Thus you can change the word `copy` in file *.csproj to `cp`,and the word `call` to `sh`, and then recompile.
  - Or you can ignore this error and manually copy the compiled dll plugin to the `Plugins` directory, where the path must be similar to the `Plugins\ShellCmder\ShellCmder.dll`
7. **If you need to compile a Mac version**, manually copy all files (except Altman.Mac) under the `Build` directory to the `Build\Altman.Mac\Contents\MonoBundle` directory before Debug or running.


#### examples

##### Windows(cygwin reference to the following)

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
# run  batch-on-windows.bat
# Using VS2012 (or the latest version) Open [Source / SecurityTools.sln] compiler
```

##### Linux
> Installation mono environment:

> Ubuntu or Debian

> `sudo apt-get install mono-devel mono-complete monodevelop`

> Other linux : <<http://www.mono-project.com/download/>>

```sh
git clone https://github.com/keepwn/Altman.git
cd Altman/
./batch-on-linux.sh
monodevelop Source/SecurityTools.sln  
# Use monodevelop compile Altman
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



#### Running
**Altman3** is based on.Net4.0, can be perfect run in Windows, Linux, Mac and other platforms via [Eto.Form](https://github.com/picoe/Eto).

- For `Windows`,
	- Double click to run Altman.Winform.exe; installment of .Net4.0 is required.
	- Double click to run Altman.Gtk.exe; installment of .Net4.0 and [gtk-sharp2](http://download.xamarin.com/GTKforWindows/Windows/gtk-sharp-2.12.25.msi) is required.
- For `Linux`,
	- Run `mono Altman.Gtk.exe` under command line; installment of Mono (>=3.2.8), libgdiplus and [gtk-sharp2](https://github.com/mono/gtk-sharp/releases/tag/gtk-sharp-2.12.27) is required.
- For `OS X`,
	- Run `mono Altman.Gtk.exe` under command line; installment of Mono and [gtk-sharp2](https://github.com/mono/gtk-sharp/releases/tag/gtk-sharp-2.12.27) is required.
	- Double click to run Altman.Mac, installment of Mono is required.
- [**How to install the mono environment in a more simple way**](http://www.mono-project.com/download/)
- If you need to enable the IronPython support, you need to install [IronPython](http://ironpython.codeplex.com/)(if the program already includes IronPython, additional installment is not necessary)
- For common errors, please visit [FAQ](https://github.com/keepwn/Altman/wiki/FAQ)

### Plugins
**Altman3** uses the MEF plugin architecture and also supports IronPython.

- Adopt C# to compile plugins/services
- Adopt IronPython to compile plugins / services

**Visit [Wiki](https://github.com/keepwn/Altman/wiki) to get the documentation on plugin development**

### Copyright
This program is subject to the GPLv2 protocol; please refer to the LICENSE file under the root directory for details.

### Improvement and suggestions
[Submit question](https://github.com/keepwn/Altman/issues) OR [improve code](https://github.com/keepwn/Altman/pulls)

You can also join the tencent echnical discussion group: 331451473 
