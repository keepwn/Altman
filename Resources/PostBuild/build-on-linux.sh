#!/bin/bash

ConfigurationName=$1
SolutionDir=$2
TargetDir=$3
TargetName=$4
TargetExt=$5
OutDir="${SolutionDir}../Build/Plugins/${TargetName}/"
PluginsDir="${SolutionDir}../Build/Plugins/"

if [ ! -d $PluginsDir ]; then
	mkdir $PluginsDir
fi

if [ ! -d $OutDir ]; then
	mkdir $OutDir
fi

if [ $ConfigurationName = "Release" ]; then
	cp $TargetDir$TargetName$TargetExt $OutDir
	cp $TargetDir"*.lng" $OutDir
else
   	cp $TargetDir$TargetName$TargetExt $OutDir
	cp $TargetDir$TargetName$TargetExt".mdb" $OutDir
	cp $TargetDir"*.lng" $OutDir
fi

exit 0