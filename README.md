This program is designed to run on a Windows platform

1. ENable MSMQ
Enable MSMQ
------------
    a. Open Control Panel.
    b. Search for Turn Windows Features on and off.
    c. Expand Microsoft Message Queue (MSMQ) Server, expand Microsoft Message Queue (MSMQ) Server Core, and then select the check boxes for the following Message Queuing features to install: ...
    Click OK.

View message on MSMQ
------------------------
Open Computer management from start menu 
Services and Applications -> Message Queuing -> private Queues -> OrderStatusQueue -> Queue Messages


2. Open NotificationSystem.sln in Visual Studio 2017

3. Righ click on the solution file in Project Explorer and click on Restore Nuget Packages

4. Build the solution in Release mode and open the project in Windows explorer

5. Run the OrderManagementService by double clicking on the below file and ensure that the messages are added to the message queue
NotifictionSystem\OrderManagementSystem\bin\Release\OrderManagementSystem.exe

6. Run notification system by double clicking on the below file
	NotifictionSystem\NotifictionSystem\bin\Release\NotifictionSystem.exe

This should read messages and simulating sending messages by logging the message content

Ensure the read messages are removed from the queue