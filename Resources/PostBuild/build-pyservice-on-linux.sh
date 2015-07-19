#!/bin/bash

SolutionDir=$1
TargetDir=$2
TargetFileName=$3

OutDir="${SolutionDir}../Build/Services/"

mkdir $OutDir
cp $TargetDir$TargetFileName $OutDir

exit 0