Test 1
Precondition: A system with Oxigen installed. Working internet connection.
Test: Cut off internet connection and perform a scheduled execution of the Content Exchanger.
Post condition: File fca.dat on SettingsData folder and in it there is number 1, as in one failed attempt.

Test 2
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat
Test: Perform a scheduled execution of the Content Exchanger.
Postcondition: Number inside fca.dat has incremented by one.

Test 3:
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat
Test: Restore internet connection. Perform scheduled execution of the Content Exchanger.
Postcondition: fca.dat is deleted.

Test 4:
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat and has the number 24.
Test: Perform a scheduled execution of the Content Exchanger.
Postcondition: Pops up message saying that Oxigen has trouble communicating with the servers. Dismissing the window deletes fca.dat.

Test 5:
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat and has the number 24.
Test: Restore internet connection. Perform scheduled execution of the Content Exchanger.
Postcondition: fca.dat is deleted. No Pop up message saying that Oxigen has trouble communicating with the servers.

Test 6
Precondition: A system with Oxigen installed. Working internet connection.
Test: Cut off internet connection and perform a tray execution of the Content Exchanger.
Post condition: File fca.dat on SettingsData folder and in it there is number 1, as in one failed attempt.

Test 7
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat
Test: Perform a tray execution of the Content Exchanger.
Postcondition: Number inside fca.dat has incremented by one.

Test 8
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat
Test: Restore internet connection. Perform tray execution of the Content Exchanger.
Postcondition: fca.dat is deleted.

Test 9
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat and has the number 24.
Test: Run the Content Exchanger from the system tray.
Postcondition: Does not pop up a "Cannot connect" message and number in fca.dat increments by one.

Test 10
Precondition: A system with Oxigen installed. Internet connection is cut off. File fca.dat exists in SettingsData.dat and has the number 24.
Test: Restore internet connection. Perform tray execution of the Content Exchanger.
Postcondition: fca.dat is deleted. No Pop up message saying that Oxigen has trouble communicating with the servers.

Scheduled execution of the content exchanger can be simulated by double-clicking the Content Exchanger binary.