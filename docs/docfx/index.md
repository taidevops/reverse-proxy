---
uid: root
title: YARP Documentation
---

# YARP: Yet Another Reverse Proxy

Welcome to the documentation for YARP! YARP is a library to help create reverse proxy servers that are high-performance, production-ready, and highly customizable. Right now it's still in preview, but please provide us your feedback by going to [the GitHub repository](https://github.com/microsoft/reverse-proxy).

## Why YARP

We found a bunch of internal teams at Microsoft who were either building a reverse proxy for their service or had been asking about APIs and tech for building one, so we decided to get them all together to work on a common solution, this project. Each of these projects was doing something slightly off the beaten path which meant they were not well served by existing proxies, and customization of those proxies had a high cost and ongoing maintenance considerations.

Many of the existing proxies were built to support HTTP/1.1, but with workloads changing to include gRPC traffic, they require HTTP/2 support which requires a significantly more complex implementation. By using YARP the projects get to customize the routing and handling behavior without having to implement the http protocol.
