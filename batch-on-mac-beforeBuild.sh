#!/bin/sh

mkdir ./Build/
sed -i ".bak" -e "s/copy /cp /g" -e "s/call /sh /g" -e "s#\\\Build\\\Plugins\\\#/Build/Altman.Mac.app/Contents/MonoBundle/Plugins/#g" -e "s/build-on-windows.bat /build-on-mac.sh /g" -e "s/build-pyservice-on-windows.bat /build-pyservice-on-mac.sh /g" `grep --include "*.csproj" -rl  "PostBuildEvent" ./ `