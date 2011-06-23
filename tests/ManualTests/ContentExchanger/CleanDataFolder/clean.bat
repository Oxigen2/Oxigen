@echo off
for /f "tokens=2* delims= " %%A IN ('reg query HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen /v DataSettingsPath') DO SET DataPath=%%B

rd /s /q %DataPath%data\Assets\A
rd /s /q %DataPath%data\Assets\B
rd /s /q %DataPath%data\Assets\C
rd /s /q %DataPath%data\Assets\D
rd /s /q %DataPath%data\Assets\E
rd /s /q %DataPath%data\Assets\F
rd /s /q %DataPath%data\Assets\G
rd /s /q %DataPath%data\Assets\H
rd /s /q %DataPath%data\Assets\I
rd /s /q %DataPath%data\Assets\J
rd /s /q %DataPath%data\Assets\K
rd /s /q %DataPath%data\Assets\L
rd /s /q %DataPath%data\Assets\M
rd /s /q %DataPath%data\Assets\N
rd /s /q %DataPath%data\Assets\O
rd /s /q %DataPath%data\Assets\P
rd /s /q %DataPath%data\Assets\Q
rd /s /q %DataPath%data\Assets\R
rd /s /q %DataPath%data\Assets\S
rd /s /q %DataPath%data\Assets\T
rd /s /q %DataPath%data\Assets\U
rd /s /q %DataPath%data\Assets\V
rd /s /q %DataPath%data\Assets\W
rd /s /q %DataPath%data\Assets\X
rd /s /q %DataPath%data\Assets\Y
rd /s /q %DataPath%data\Assets\Z
del /q %DataPath%data\ChannelData\*.*
del /q %DataPath%data\SettingsData\ss_adcond_data.dat
del /q %DataPath%data\SettingsData\ss_channel_subscription_data.dat
del /q %DataPath%data\SettingsData\ss_demo_data.dat
del /q %DataPath%data\SettingsData\ss_play_list.dat
del /q %DataPath%data\SettingsData\ss_general_data.dat
xcopy .\ss_general_data.dat %DataPath%data\SettingsData\

echo Ignore "The system cannot find the file specified" errors as some of the Slide folders may not exist.
echo Data Folder cleared.
pause