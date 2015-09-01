When using `ExtractAll()`, an "Path is empty" exception message is triggered by that DotNetZip tries to pass "" to Directory.Create() in ZipEntry.Extract.
 
The cause is that `targetFileName` contain the path using `\` as directory separator. When run on linux/osx the path is interpreted as a single filename making the directory path "".

Change `\` to `Path.DirectorySeparatorChar`, it will works well.

The dll version `1.9.1.9000` was compiled by KeePwn.