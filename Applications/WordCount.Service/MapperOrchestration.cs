// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.Logging;
using ReactiveMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Service
{
    public class WordCountMapReduceJob : 
        IStartupOrchestration 
    {
        #region Inputs

        public Uri DocumentUrl;

        public int ReducerCount;

        #endregion

        public async Task<UnitType> Execute(IOrchestrationContext context)
        {
            var wordcounts = await context.PerformActivity(new DownloadAndCountActivity()
            {
                DocumentUrl = DocumentUrl
            });

            foreach (var kvp in wordcounts)
            {
                string word = kvp.Key;
                int hash = Math.Abs(word.GetHashCode());
                int reducerIndex = hash % ReducerCount;

                context.ForkEvent(new WordCountEvent()
                {
                    Word = word,
                    Count = kvp.Value,
                    Reducer = reducerIndex
                });
            }

            await context.Finish(); // do not return until all forked events are processed

            return UnitType.Value;
        }
    }
}
