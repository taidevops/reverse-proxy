# ReverseProxy.RuntimeModel namespace

Classes in this folder define the internal representation
of ReverseProxy's runtime state used in perf-critical code paths.

All classes should be immutable, and all members and members of members
MUST be either:

   A) immutable
   B) `AtomicHolder<T>` wrapping an immutable type `T`.
   C) Thread-safe (e.g. `AtomicCounter`)

This ensures we can easily handle hot-swappable configurations
without explicit synchronization overhead across threads,
and each thread can operate safely with up-to-date yet consistent information
(always the latest and consistent snapshot available when processing of a request starts).

## Class naming conventions

* Classes named `*Info` (`RouteInfo`, `ClusterInfo`, `EndpointInfo`)
  represent the 3 primary abstractions in Reverse Proxy (Routes, Clusters and Destinations);
