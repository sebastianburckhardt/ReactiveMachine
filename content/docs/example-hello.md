---
title: "Example: Hello World"
description: Writing your first reactive machine application
weight: 11
---

# Hello World

The Hello World example serves to verify that you've got the Reactive Machine built correctly and can get a basic service up and running.  Further examples, like the Echo and Counter examples demonstrate features of the programming model.

## Definining a service

We begin by defining a new service: ```HelloWorld.Service``` using the Reactive Machine's service builder definition.  This builder will scan the current DLL and generate a service based on the definitions included.

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

We define a single orchestration, the ```HelloWorld``` orchestration that logs a message and returns the string "Hello World!".

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