// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Logging;
using ReactiveMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Service
{
    public class ReducerState :
        IPartitionedState<IReducerAffinity, int>,
        ISubscribe<WordCountEvent, IReducerAffinity, int>
    {
        public Dictionary<string,int> WordCounts;

        public void On(ISubscriptionContext<int> context, WordCountEvent evt)
        {
            int currentCount;
            WordCounts.TryGetValue(evt.Word, out currentCount);
            WordCounts[evt.Word] = currentCount + evt.Count;
        }
    }

    public class ReadReducerState : IRead<ReducerState, Dictionary<string, int>>
    {
        public Dictionary<string, int> Execute(IReadContext<ReducerState> context)
        {
            return context.State.WordCounts;
        }
    }

}
