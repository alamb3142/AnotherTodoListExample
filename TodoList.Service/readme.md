# TodoList Service

[home](../README.md)

## Contents

- [Overview](#overview)
- [Project Structure](#project-structure)
	- [Clean Architecture](#clean-architecture)
	- [Screaming Architecture](#screaming-architecture)
	- [The Application Layer](#the-application-layer)
- [Design Decisions](#design-decisions)
	- [DDD](#ddd)
	- [Results/Avoiding Exceptions](#resultsavoiding-exceptions)

## Overview

TodoList.Service is a .NET 8 application that can be exposed by either HTTP or
gRPC*. It follows DDD tactical patterns Command Query Responsibility Segregation 
(CQRS) using Mediator (TODO link). It uses Entity Framework with the fluent
builder API for code first schema generation, but uses Dapper for queries to
gain performance and get the best of both worlds.

\*The gRPC side isn't being maintained currently but once the UI is built
up a bit more I will be updating this API to do some performance testing.

## Project Structure

### Clean Architecture

The solution roughly follows the ideas of Clean Architecture, with Api,
application, domain and infrastructure layers represented by dotnet 
projects.

```plaintext
| TodoList.Service
|  | Api.Grpc  			gRPC ASP.NET API
|  | Api.Http 			HTTP ASP.NET API
|  | Application 		Application logic following Mediator pattern
|  | Domain 			Domain entities
|  | Infrastructure 	Code to communicate with SQL (using EF Core) & other services
|  | Testing   			NUnit test projects for Application & Domain layers
```

### Screaming Architecture

The project also follows the ideas of Screaming Architecture, where code is
layed out, as much as possible, to match the structure of the domain.

The pattern I will generally always follow is to group each layer by related
aggregate root, with common folders at various levels representing code to be
shared within a certain branch of the folder tree. For example `/Domain/Common/`
contains code relevant to the entire Domain layer, while `Domain/Users/Common/`
represents shared code only relating to users.

### The Application Layer

Following the patterns mentioned above, the solution has an application layer
split up by aggregate root entities. Within each of the aggregate's folders is
a collection of command and query folders. For example `/TodoLists/CreateTodoList`
contains the command definition, it's handler, any relevant DTOs/response types,
and any other code written to serve that command.

This layer makes heavy use of the Mediator library (TODO find the link), which
is an optimised version of `MediatR` library, using the same abstractions but
utilising dotnet's source generators (compile time logic) rather than runtime
reflection.

## Design Decisions

### DDD

There are many, many resources out there on the tactical patterns of Domain
Driven Design (DDD), this solution follows a number of these, including 
Entities, Aggregates and Value Objects.

The application is made up of one domain/bounded context covered in more detail
in the [domain-model](../Documentation/domain-model.md) document.

### Results/Avoiding Exceptions

You will never see me throw an exception in this repository unless absolutely
necessary (& most will be for failed gRPC returns). Throughout the solution the
'Result' pattern is followed using the FluentResults (TODO link) library. 

Anything that can potentially fail should wrap its return in a `Result<T>` type
to indicate relevant callers up the stack that failure cases need to be dealt
with or passed up the stack to be dealt with. This is a great pattern for 
functional programming nerds or anyone who doesn't like non-linearity
introduced by exceptions & the need to have something somewhere in the stack
ready to catch them.

Obviously third party libraries will still throw some exceptions, and these
will slip through in truly exceptional circumstances, but over time known
issues will be converted from exceptions to results (FluentResults has some
great helper methods for this!). Generally the only exceptions will be the
unhandled errors that result in a 500 being returned.
