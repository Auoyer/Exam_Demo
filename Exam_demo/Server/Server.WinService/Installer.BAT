set exeName=Server.WinService.exe
set SvrName=GTA_TP

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe %~dp0%exeName%

Net Start SvrName

sc config SvrName start= auto