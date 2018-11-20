---
title: "Example: Counter Application"
description: Building a partitioned, distributed incrementable counter
weight: 2
---

Applications are modeled using events.  Here, we model an increment eveent for a counter.  The counter is automatically partitioned using the ```ICounterAffinity``` affinity which requires that the event supply a ```CounterId```.

```c#
[DataContract]
public class IncrementEvent :
    IEvent,
    ICounterAffinity
{
    [DataMember]
    public uint CounterId { get; set; }
}
```

To specify how our events should be partitioned on the nodes running the application, we describe the partitioning with the ```ICounterAffinity```.  The ```RoundRobinPlacement``` annotation specifies how the counter should be partitioned on the nodes according to the ```CounterId```.

```c#
public interface ICounterAffinity :
    IPartitionedAffinity<ICounterAffinity, uint>
{
    [RoundRobinPlacement]
    uint CounterId { get; }
}
```

We define application state as a view over the events in the system.  

Here, we define ```Counter1```.  ```Counter1``` is a counter whose state is derived from summing the increment operations.  There will automatically be a single ```Counter1``` object for each different ```CounterId``` in the system, and that state will be automatically partitioned based on the affinity.

```c#
[DataContract]
public class Counter1 :
    IPartitionedState<ICounterAffinity, uint>,
    ISubscribe<IncrementEvent, ICounterAffinity, uint>
{
    [DataMember]
    public int Count;

    public void On(ISubscriptionContext<uint> context, IncrementEvent evt)
    {
        Count++;
    }
}
```

The ```ISubscribe``` specifies how you subscribe to events.  In this case, ```Counter1``` will subscribe to all ```IncrementEvents``` and ensure that an instance of the ```On``` method is specified for handling how each event modifies the local state, ```Count```.

