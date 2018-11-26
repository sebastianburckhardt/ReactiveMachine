---
title: "Example: Hello World"
description: Writing your first reactive machine application
weight: 3
---

...

## Definining a service

```c#
namespace HelloWorld.Service
{
    /// <summary>
    /// Provides the building instructions for the hello world service
    /// </summary>
    public class HelloWorldService : ReactiveMachine.IServiceBuildDefinition
    {
        public void Build(IServiceBuilder builder)
        {
            // we build this service by automatically scanning the project for declarations
            builder.ScanThisDLL();
        }
    }
}
```

## Your First Orchestration

```c#
namespace HelloWorld.Service
{
    /// <summary>
    /// Defines an orchestration that runs automatically when the service is started for the first time
    /// </summary>
    public class HelloWorldOrchestration : ReactiveMachine.IOrchestration<string>
    {
        public Task<string> Execute(IOrchestrationContext context)
        {
            context.Logger.LogInformation("Hello World was called");

            return Task.FromResult("Hello World");
        }
    }
}
```