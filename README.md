# WWKS2
This is an experimental implementation of the WWKS2 protocol.

Key features are: 

- Support of free role definition of subscribers.
- Strict separation of protocol messages and its infrastructure services (serialization, transfer)
- Two supported serialization formats (XML and JSON). The library is extendible due to more serialization formats.
- Transfer modes can be combined to bridge different transfer technologies e. g. TCP to File System. Or later a REST API to TCP.
- The library is extendible. Basically it provides you with the standard implementation of the WWKS2 protocol. If you wish to introduce new dialogs or extend existing ones, this is easily possible.

# Current status
At the moment the code is an a development phase. I'm about to check, if the design fulfills the intended purpose in practice. So major changes may be ahead. It's just a free time project of mine...

# What's coming up next?
- Increase stability, improve design
- Logging support
- Use case layer, which avoids dealing with message dialogs directly
- Transfer modes: SignalR, HTTP/REST API
- Simulation tools
- Documentation (API/Architecture)

# How to start
	// First you have to create a message channel. In this case a TCP message channel:

	TcpClient tcpClient = ... // Created by your own

	Encoding encoding = Encoding.UTF8;

	IMessageChannel messageChannel = new TcpMessageChannel(	new XmlMessageSerializer( encoding ),
															new XmlTokenReader( stream, encoding ),
															tcpClient.GetStream(),
															tcpClient  );
																	
	// Then connect it to a message endpoint:
																	
	IMessageEndpoint messageEndpoint = new MessageEndpoint( messageChannel, TimeSpan.FromSeconds( 10 ) );

	// Choose a role and a context. In this case the calling code acts as a client to a storage system:

	IClientRole<IStorageSystemProxy> storageSystem = new ClientRole<IStorageSystemProxy>( new StorageSystemProxy( messageEndpoint ) );

	// Subscribe to publishers for what you want a notfication straight from the beginning.

	this.StorageSystem.Proxy.Subscribe(	( KeepAliveRequest request ) =>
										{
											storageSystem.Proxy.SendResponse( new KeepAliveResponse( request ) );
										}  );
								
	// Connect the message endpoint:

	messageEndpoint.Connect();

	// Send a hello request to the storage system:

	Subscriber localSubscriber = new(	new[]
										{
											Capabilities.StockLocationInfo,
											Capabilities.KeepAlive,
										},
										SubscriberId.DefaultIMS,
										SubscriberType.IMS,
										null,
										"ACME",
										"ABC"	);

	HelloResponse helloResponse = storageSystem.SendRequest( new HelloRequest( localSubscriber ) );
