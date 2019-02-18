// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using ReactiveMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordCount.Service
{
    public interface IReducerAffinity
        : IPartitionedAffinity<IReducerAffinity, int>
    {
        int Reducer { get; }
    }

}
