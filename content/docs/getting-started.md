---
title: Getting started
description: Run Reactive Machine in your local environment
weight: 2
---

Here is the getting started documentation for Reactive Machine.

Here is an Erlang code block:

```erlang
connect(Host, User, Password) ->
    {ftp_server, Host} ! {connect,self(),User,Password},
    receive
        {ftp_server, Reply} -> Reply;
        Other -> Other
    after 10000 ->
            timeout
    end.
```
