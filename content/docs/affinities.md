---
title: "Affinities"
description: Application-aware partitioning and horizontal scalability
weight: 8
---

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