---
title: "Affinities"
description: Application-aware partitioning and horizontal scalability
weight: 8
---

Affinities define locality, by specifying keys that can be used to place state, orchestrations, and activities. These keys are also used for synchronization (locking). Events provide reliable, consistent pub-sub. When an event is raised by an orchestration, all the states that subscribe to it are modified. Events appear to be globally ordered and virtually synchronous.

...

## Singletons

...

```c#
public interface ICounterAffinity :
    ISingletonAffinity<ICounterAffinity>
{
}
```

## Partitioned by key

...

```c#
public interface ICounterAffinity :
    IPartitionedAffinity<ICounterAffinity, uint>
{
    [RoundRobinPlacement]
    uint CounterId { get; }
}
```