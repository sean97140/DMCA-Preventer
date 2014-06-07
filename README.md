DMCA-Preventer
==============

This program is designed to detect when you are on an internet IP range, such as college campuses,
that is specified and will inturn kill a specified program. The main use case for this program is to
prevent DMCA violations while on a specified ip range. Many users run torrent programs at home
and then bring their computer to school, forgetting to close the torrent program. As a result, file
sharing will continue on the school's internet connection which is heavily monitored by copyright 
protection orginazations. Most of the time you will just receive a warning for doing this, but 
subsuquent warnings may result in academic reprocussions or legal issues (they sue you).

To use this program:

run in visual studio, 
set ip range of your campus 131.252 for Portland State
run torrent program
find service name, ex: utorrent
enter it in box.
connect to campus wifi/internet (make sure you are not sharing for test)
click test.

if you recieve warning and torrent/filesharing program was killed, save settings.
if you like how the program works, click install.

program will be copied to %appdata%\DMCA Preventer\dmca.exe and
registry key will be added to "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run"
so that the program will start automatically. 

TODO: make program start hidden.

note: this program checks if your ip address falls within a specified range. Some campuses may have 
multiple IP address ranges rendering this program ineffective, later commits will resolve this issue.

Do not assume this program will always work, I obtain the ip address from icanhazip.com and this may go
down and the program is designed to fail silenty if it can not resolve the page (for cases when not connected to internet).
It is advisable to run the test on ocassion to ensure that it is correctly functioning and that your campus IP range has
not changed.

By using this program you understand that this program is experimental and may contain bugs that prevent it from working.
If you recieve a DMCA notice while using this program, or are sued as a result, you agree to not hold me liable in anyway.


