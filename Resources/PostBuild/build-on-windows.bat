set ConfigurationName=%1
set SolutionDir=%2
set TargetDir=%3
set TargetName=%4
set TargetExt=%5
set OutDir=%SolutionDir%..\Build\Plugins\%TargetName%\
set PluginsDir=%SolutionDir%..\Build\Plugins\

IF Not EXIST %PluginsDir% (
  mkdir %PluginsDir%
)

IF Not EXIST %OutDir% (
  mkdir %OutDir%
)

IF "%ConfigurationName%" == "Release" (
  copy %TargetDir%%TargetName%%TargetExt% %OutDir%
  copy %TargetDir%*.lng %OutDir%
) ELSE (
  copy %TargetDir%%TargetName%%TargetExt% %OutDir%
  copy %TargetDir%%TargetName%.pdb %OutDir%
  copy %TargetDir%*.lng %OutDir%
)
exit 0