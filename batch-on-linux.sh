#!/bin/sh

mkdir ./Build/ ./Build/Bin ./Build/Plugins ./Build/Services
sed -i -e "s/copy /cp /g" -e "s/call /sh /g" -e "s/build-on-windows.bat /build-on-linux.sh /g" -e "s/build-pyservice-on-windows.bat /build-pyservice-on-linux.sh /g" `grep --include "*.csproj" -rl  "PostBuildEvent" ./ `
cp -Rv ./Resources/RunNeed/* ./Build/
cp -Rv ./Libraries/IronPython/* ./Build/
cp -Rv ./Libraries/Sqlite3/* ./Build/
