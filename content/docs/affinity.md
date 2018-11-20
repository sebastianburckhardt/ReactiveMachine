---
title: Affinities
description: How to control the partitioning of your data
weight: 2
---

## Singletons

```c#
public interface IClientAffinity 
        : ISingletonAffinity<IClientAffinity>
    {
    }
```

## Partitioned

```c#
public interface ICounterAffinity :
         IPartitionedAffinity<ICounterAffinity, uint>
    {
        [RoundRobinPlacement]
        uint CounterId { get; }
    }
```