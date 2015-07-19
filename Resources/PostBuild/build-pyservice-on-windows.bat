set SolutionDir=%1
set TargetDir=%2
set TargetFileName=%3
set OutDir=%SolutionDir%..\Build\Services\

mkdir %OutDir%
copy %TargetDir%%TargetFileName% %OutDir%
@exit 0