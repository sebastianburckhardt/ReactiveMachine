---
title: "Example: Echo Service Application"
description: Building an echo service
weight: 3
---

...

# Affinity

We begin by defining an affinity for the client and the server.  We use the singleton affinities to route everything to a single node.

```c#
public interface IClientAffinity 
    : ISingletonAffinity<IClientAffinity>
{
}

public interface IServerAffinity 
    : ISingletonAffinity<IServerAffinity>
{
}
```

## Events

We define two events:

* ```PingEvent```: issued by the client against the server, using the ```IServerAffinity```
* ```PongEvent```: issued by the server against the client, using the ```IClientAffinity```

Here's the definition of the ```PingEvent```:

```c#
[DataContract]
public class PingEvent :
    IEvent,
    IServerAffinity
{
    [DataMember]
    public string Message;
}
```

Here's the definition of the ```PongEvent```:

```c#
[DataContract]
public class PongEvent :
    IEvent,
    IClientAffinity
{
    [DataMember]
    public string Message;
}
```

## Server

Our server is responsible for subscribing to ```PingEvent``` events and when one is received, generates a new ```PongEvent```.  ```ForkEvent``` is used to generate the event and asynchronously schedules the event to be transmitted without blocking.  The local state ```count``` is advanced for each message.

```c#
[DataContract]
public class ServerState :
    ISingletonState<IServerAffinity>,
    ISubscribe<PingEvent, IServerAffinity>
{
    [DataMember]
    int count;

    public void On(ISubscriptionContext context, PingEvent pingEvent)
    {
        count++;

        context.Logger.LogInformation($"Received: {pingEvent.Message}");

        context.ForkEvent(new PongEvent()
        {
            Message = $"Echo {pingEvent.Message}",
        });
    }
}
```

## Client

...

```c#
[DataContract]
public class ClientState :
    ISingletonState<IClientAffinity>,
    ISubscribe<PongEvent, IClientAffinity>
{
    [DataMember]
    int count;

    public void On(ISubscriptionContext context, PongEvent pongEvent)
    {
        count++;

        context.Logger.LogInformation($"Received: {pongEvent.Message}");

        // Send a ping event unless we have reach max count
        if (count < SendFirstPing.NumberOfEvents)
        {
            context.ForkEvent(new PingEvent()
            {
                Message = $"Ping #{count + 1}",
            });
        }
    }
}
```

## Operations

...

```c#
public class SendFirstPing : 
    IStartupOrchestration,
    IClientAffinity
{
    public static int NumberOfEvents = 100;

    public Task<UnitType> Execute(IOrchestrationContext context)
    {
        var startTime = DateTime.Now;

        context.ForkEvent(new PingEvent()
        {
            Message = $"Ping!",
        });

        return UnitType.CompletedTask;
    }
}
```