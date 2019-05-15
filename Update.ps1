try {
Write-Output('stop CRM');
# copy _app_offline and rename app_offline
Copy-item C:\GitLab-Runner\builds\c02fe04c\0\SevenGroupDevelopers\CRM-developer\CRMDeveloper\CRMDeveloper\_app_offline.htm C:\Host\seven-group-crm\
Rename-Item -Path "C:\Host\seven-group-crm\_app_offline.htm" -NewName "app_offline.htm"

Write-Output('publish CRM');
#Publish CRM
cd ./CRMDeveloper/
dotnet publish -c Release -o /Host/seven-group-crm

Write-Output('start CRM');
Rename-Item -Path "C:\Host\seven-group-crm\app_offline.htm" -NewName "_app_offline.htm"
}
catch {
	Write-Host ("Error during the command to do something : {0}" -f $_.Exception.Message); exit 1;
}

