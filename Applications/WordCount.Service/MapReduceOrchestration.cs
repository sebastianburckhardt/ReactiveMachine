// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using ReactiveMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Service
{
    public class MapReduceOrchestration : 
        IStartupOrchestration 
    {
        public int MapperCount;

        public int ReducerCount;


        public Task<UnitType> Execute(IOrchestrationContext context)
        {

            string doc = client.DownloadString(documentUrl);
            string[] words = doc.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                int hash = word.GetHashCode();
                if (hash < 0)
                {
                    hash = -hash;
                }
                int reducerBucketIndex = hash % state.reducerCount;
                //log.Info($"{host.CurrentActorId}: sending '{word}' to reducer{reducerBucketIndex}");
                host.SendMessageBuffered("Reducer", $"reducer{reducerBucketIndex}", "inc", word);
            }


        }
    }
}
