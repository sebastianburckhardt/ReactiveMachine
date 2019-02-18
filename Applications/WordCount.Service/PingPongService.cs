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

    public class PingPongService : IServiceBuildDefinition
    {
        public void Build(IServiceBuilder builder)
        {
            builder
                 .ScanThisDLL()
                 .OverridePlacement(placementBuilder => placementBuilder
                      .PlaceOnProcess<IMapperAffinity>(0)
                      .PlaceOnProcess<IReducerAffinity>(1))
                 ;
        }
    }
}
