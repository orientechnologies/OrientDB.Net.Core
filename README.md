## OrientDB.Net.Core ##
---
OrientDB.Net.Core represents a re-imaging of the .NET OrientDB Driver for .NET Framework. Written using the new .NET Core SDK, it supports .NET Framework 4.5.1 and .NET Core 1.0 and above. The purpose in re-imaging the .NET OrientDB driver, is to design an extensible base library that can support any number of implementations in the same vein as the Serilog project.

The current state of the driver and its derived components is pre-alpha. As such, the library itself can and will change substantially over the coming months.

OrientDB.Net.Core is the core library that provides the
base classes and abstractions that represent the "core" of the API. It provides abstractions for communication with OrientDB and Serialization/De-Serialization of the output. The intent is to be able to flip between binary and http in the beginning and add any additional support that may arise.