// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using ReactiveMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Service
{                
    public class WordCountEvent :
        IEvent,
        IReducerAffinity
    {
        public string Word { get; set; }

        public int Count { get; set; }

        public int Reducer { get; set; }
    }
}
