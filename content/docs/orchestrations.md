---
title: "Orchestrations"
description: Write me!
weight: 5
---

Orchestrations describe operations that are composed of one or more other operations. They are written in async/await style, and execute deterministically and reliably. When executing an operation, orchestrations can either perform it (meaning they wait for its completion) or fork it (meaning it executes completely independently of its parent).

...