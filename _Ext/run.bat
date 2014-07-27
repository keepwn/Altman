@ECHO OFF

SET SolutionDir=%1
SET OutDir=%2
IF [%1]==[] GOTO USAGE1
IF [%2]==[] GOTO USAGE2

rem SET SolutionDir=E:\Codes\_GitHub\Altman\
rem SET OutDir=E:\Codes\_GitHub\Altman\_Build\


IF NOT EXIST %OutDir%Bin mkdir %OutDir%Bin
move %OutDir%*.dll %OutDir%Bin
move %OutDir%*.pdb %OutDir%Bin
copy %SolutionDir%_Ext\dll\*.* %OutDir%Bin
copy %SolutionDir%_Ext\*.db3 %OutDir%
copy %SolutionDir%_Ext\*.xml %OutDir%
@echo copy altman musted files ok!


IF NOT EXIST %OutDir%Docs mkdir %OutDir%Docs
copy %SolutionDir%_Docs\*.* %OutDir%Docs
copy %SolutionDir%README.md %OutDir%
@echo copy altman docs ok!

IF NOT EXIST %OutDir%CustomType mkdir %OutDir%CustomType
copy %SolutionDir%_Ext\CustomType\*.* %OutDir%CustomType
IF NOT EXIST %OutDir%Plugins mkdir %OutDir%Plugins
@echo copy altman other files ok!
GOTO END

:USAGE1
@echo need param1 SolutionDir
GOTO END

:USAGE2
@echo need param2 OutDir
:END


