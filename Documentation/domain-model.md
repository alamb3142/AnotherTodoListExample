# Domain Model

[home](../README.md)

## Contents

## About

This document is an example of a [Ubiquitous Language](https://martinfowler.com/bliki/UbiquitousLanguage.html).

Its purpose is to outline the domain model, including its behaviour, in plain
text. Important domain concepts are highlighted with inline code
blocks, e.g. `User` or `Todo`. These typically map directly onto our domain
entities when we convert this into code. 

This will largely drive the design of the code, but this relationship goes in
both directions. The model may change when implementing the model for
non-functional concerns (good class design, security, etc), which should always
be reflected in this document. 

This document should always be a reliable source of truth when it comes to the
behaviour of the application. This prevents the codebase and the domain model
from diverging at implementation time, as a core principle of DDD is keeping
these two aspects in sync.

## Basics

A `User` owns a collection of `TodoLists`. These lists are made up of `Todos`.

A `Todo` represents a work item or task to be completed at some point in
the future.

`Todos` can be moved between `TodoLists` so that users can easily organise
and group their daily tasks.

## TodoLists

A `TodoList` has:
- A title under 50 characters long
- An owner (a `User`)
- A collection of `Todos`

## Todos

A `Todo` has:
- A title under 50 characters long
- A description/body
- A `TodoList` it belongs to
- A completed state/flag

## Users

A `User` has:
- A username
	- must be unique across the application
	- information to verify password (e.g. the hash & salt)

## Future Features

### Todo Sharing

A `User` may want to share some of their tasks with other `Users`, as they 
may relate to work shared within a household, on a project, or some other
collaborative effort.

Therefore `Users` will have the option to share `TodoLists` with other `Users`
of the application. The `User` creating the `TodoList` will be the owner of
that list & have the right to revoke access at any time. `Users` will also have
the option to remove themselves from a `TodoList` so that it doesn't show up
when they are no longer interested in seeing it.

### Scheduling

A `Todo` will be present the option to set a due date/time so that users can
set time based goals with their tasks.

This opens the door for things like reminders and calendar views.
