---
title: Affinities
description: How to control the partitioning of your data
weight: 2
---

## Singletons

Something something something.

```c#
public interface ICounterAffinity 
        : ISingletonAffinity<ICounterAffinity>
    {
    }
```

## Partitioned

Something something something.

```c#
public interface ICounterAffinity :
         IPartitionedAffinity<ICounterAffinity, uint>
    {
        [RoundRobinPlacement]
        uint CounterId { get; }
    }
```