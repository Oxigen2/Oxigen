Test 1
Precondition: A system with a clear WinINET cache. Fiddler can clear the WinInet cache.
Test: Scheduled execution of the Content Exchanger.
Post condition: HTTP 200 OK across all items.

Test 2
Precondition: A system with a clear WinINET cache. Fiddler can clear the WinInet cache.
Test: Run the Content Exchanger from the system tray.
Postcondition: HTTP 200 OK across all items.

Test 3:
Precondition: A system where Content Exchanger had ran sooner than its frequency period.
Test: Scheduled execution of the Content Exchanger.
Postcondition: No HTTP calls at all.

Test 4:
Precondition: A system where Content Exchanger had ran sooner than its frequency period.
Test: Run the Content Exchanger from the system tray.
Postcondition: 304 Not-Modified across all items. 

Test 5:
Precondition: A system where Content Exchanger ran once or more between half an hour or more ago.
Test: Scheduled execution of the Content Exchanger.
Postcondition: 200 OK status on channels in which if content has been added/deleted and 304 Not-Modified on channels in which content has not been added/deleted.

Scheduled execution of the content exchanger can be simulated by double-clicking the Content Exchanger binary.