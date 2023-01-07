# WWKS2
This is an experimental implementation of the WWKS2 protocol.

Key features are: 

- Support of free role definition of subscribers.
- Strict separation of protocol messages and its infrastructure services (serialization, transfer)
- Two supported serialization formats (XML and JSON). The library is extendible due to more serialization formats.
- Transfer modes can be combined to bridge different transfer technologies e. g. TCP to File System. Or later a REST API to TCP.
- The library is extendible. Basically it provides you with the standard implementation of the WWKS2 protocol. If you wish to introduce new dialogs or extend existing ones, this is easily possible.

# Current status
At the moment the code is an a development phase. So no Beta, RC or even Release status. It should be treated as a technical preview. The next steps are to continue testing and fix the found bugs.

# What's coming up next?
- Increase stability
- Logging support
- Use case layer, which avoids dealing with message dialogs directly
- Transfer modes: SignalR, HTTP/REST API
- Simulation tools
- Documentation (API/Architecture)