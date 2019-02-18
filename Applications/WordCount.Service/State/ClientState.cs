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
    [DataContract]
    public class ClientState :
        ISingletonState<IMapperAffinity>,
        ISubscribe<PongEvent, IMapperAffinity>
    {
        [DataMember]
        int count;

        public void On(ISubscriptionContext context, PongEvent pongEvent)
        {
            count++;
   
            context.Logger.LogInformation($"Received: {pongEvent.Message}");

            // Send a ping event unless we have reach max count
            if (count < MapReduceOrchestration.NumberOfEvents)
            {
                context.ForkEvent(new WordCountEvent()
                {
                    Message = $"Ping #{count + 1}",
                });
            }
        }
    }

}
