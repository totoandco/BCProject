# BioConnect .NET PROJECT

## Description of the application

The application is an asynchronous server which listens on the TCP port 55555. It accepts client connections.

An interaction with this server can be made through a TELNET client. 

This server has the following functions :
* Returns the number of successful Handshakes happened on this server,
* Returns the number of the current connections to the server,
* Returns a randomly generated prime number.

## Available commands

The following commands are available :

* HELO : initiate the handshake. Before doing this handshake, the server won't answer even if the command entered is available. Please note that the handshake is completed after a timeout of 5 second.
* COUNT:request the number of successful handshakes happened on this server.
* CONNECTIONS: request the number of current connections to the server.
* PRIME: request a randomly generated prime number.
* TERMINATE:Initiate the disconnection from the server. 

## Content of the release

3 projects are included in this release :

* BCProject : This project contains the asynchronous server.
* BCProjectUnitTest: This project contains unit tests designed to control the main functions.
* BCProjectIntegrationTest: This project contains an asynchronous TCP Client which connects to the server contained in the BCProject an sends the commands.

# Documentation

## Installation of the application

This application is contained in an .exe file. You need to install this file on the plaform where you want it to run.

## Start the server

In order to start the server, double-click on the .exe file.

## Stop the server

Close manually the application.

## Example of one interaction client<->server

Here is an example of one interaction client/server:

After the client makes a connection to the server :

### The Handshake :

Client: HELO

Timeout 5 seconds

Server: HI
 
After the server replies "HI", the handshakes is considered as successful.
After a successful handshake the server accepts the rest of the available commands:

### Command COUNT:

Client: COUNT

Server Response: 22 

### Command CONNECTIONS:

Client: CONNECTIONS

Server Response: 4

### Command PRIME:

Client: PRIME

Server Response: 142357

### Command TERMINATE (initiating the disconnection):

Client: TERMINATE

Server Response: BYE

Disconnection

## Technologies used

This project is using the following technologies : 

* C#/.NET
* MS unit test
* Ghost Doc

# Technical choices

Here is some explanations about the technical choices made during this project :

## The use of the design pattern strategy

I used the design pattern strategy to implement the answers of the server because it allows:
* to not have the methods which don't concern the server itself (the networking part) in the class AsyncServer,
* to not have a strong coupling between the class AsyncServer and the setOfAnswer therefore, we can :
	* change the implementation of the answers easily, 
	* add another set of answers for the same commands,
	* if we have another set of answers, change during the runtime the set used by the server (with minor modifications).
* having a better maintainable code.

## The use of a list for the client's sockets

I used a list to store the client sockets. this wasn't specified in the specifications, however, because the Socket class is an unmanaged class, I wanted to be sure to be able to dispose them if needed.

This is also why during a connection and after a "CONNECTIONS" command I'm checking this list in order to close the Sockets not connected. Plus , I'm keeping up to date the number of current connexions on the server (as requested).

## Extraction of the strings in static classes

The classes "AvailableAnswers" and "AvailableCommands" have been created in order to store the strings used in the code because :
 
* this allows better clarity in the code, 
* they are easy to locate,
* we can change the values without reviewing all the code,
* we can add other commands.

 I preferred to use these classes instead of a resource file (which can be created in the project's properties) because :
* these strings were not long,
* it adds more clarity in the code (because their names are self-explanatory).

## Use of a global static class

I used a global static class to store the hard-coded values (like the IP address, port number , buffer size etc...)for the same reason than the one used for the strings:
* easy to locate without reviewing the code,
* easy to change if needed.

## Use of a ClientObject class

This class has been created in order to pack different values during the communication , like the flags saying if the client did a handshake or not and if the client has requested a disconnection or not.

## The choice to have unit tests and a client TCP

As the emission/reception of TCP Packets can be hard to mock and unit tests, I've decided to separate my tests in two parts:
* unit tests for the functions not requiring a message emission/reception,
* do a client in order to test the emission/reception part + the loading tests.

# Trade off , or what I might do differently if I were to spend additional time on the project

If I were to spend additional time on the project , I would have considered to use a solution based on the SocketAsyncEventArgs class in order to see the differences with my current solution.

Another thing I would implement is a logging system (log files) as in production environment it's a nice feature to have because:
* it allows the level 1/2 support to quickly investigate what's happening on the server without reviewing the code,
* the development team knows exactly where the code breaks.

All I did in this version (because a logging system was not required) is to print the errors on the server's console.