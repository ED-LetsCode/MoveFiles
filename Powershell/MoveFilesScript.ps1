# Source path
$sourceFolder ="C:\"

# Destination path
$destinationFolder="C:\"

#                       DD-MM-YYYY
$filterDate = Get-Date "01/01/2018"

Get-ChildItem $sourceFolder | 
Where-Object {! $_.PSIsContainer -and $_.LastWriteTime -lt $filterDate} |
Move-Item -Destination $destinationFolder
