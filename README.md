
# Reactive Machine

Reactive Machine is a serverless programming model for writing reliable elastic micro-services.
The documentation is at <https://reactive-machine.org/>.

The idea is to express the application logic at a level of abstraction that hides platform failures (machines or connections going down) and configuration choices (e.g. the number of servers). This high-level representation is then compiled and deployed on a back-end host of choice. Importantly, the host can recover from failures transparently and consistently, because our compiler produces a deterministic state machine that makes it possible to reliably track the application state by logging messages and/or persisting snapshots to durable storage.

We plan to build and experiment with many back-ends: all can provide the same application semantics, and are therefore easily interchangeable. But they can exhibit large differences in non-semantic characteristics, such as cost, scalability, latency, throughput, and recovery speed.

## Programming Model

Reactive machine programs are built on task abstraction we call *operation*. The model offers a number of different operations:

- **Orchestrations** describe operations that are composed of one or more other operations. They are written in async/await style, and execute deterministically and reliably. When executing an operation, orchestrations can either perform it (meaning they wait for its completion) or fork it (meaning it executes completely independently of its parent).
- **Activities** are operations that can be unreliable or nondeterministic, such as calls to external services.
- **States** represent a small piece of information (cf. key-value pair, or a grain, or virtual actor) that can be atomically accessed via a specified set of read and update operations.
- **Affinities** define locality, by specifying keys that can be used to place state, orchestrations, and activities. These keys are also used for synchronization (locking).
- **Events** provide reliable, consistent pub-sub. When an event is raised by an orchestration, all the states that subscribe to it are modified. Events appear to be globally ordered and virtually synchronous. 

## Status

We are currently at 1.0.0-alpha, meaning that this is preview of what we expect to release in the first release (1.0.0).
It includes:

- The C# version of the reactive machine programming model and compiler, on .NET standard 2.0
- A local emulator host
- An Azure Functions host
- Application examples to demonstrate the features of the programming model
- A Hello World sample to demonstrate how to use the 2 hosts

What remains to be done for 1.0.0 is:
- Feature for serving external requests (to the ICompiledApplication abstraction, and to the Azure functions host)
- Fix known bugs in existing tests, and add more tests
- Build and test on Linux
- Set up continuous integration

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
