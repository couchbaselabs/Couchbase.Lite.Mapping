**This is an independent, open source project, and is NOT affiliated with or maintained by Couchbase, Inc.**

# Couchbase.Lite.Mapping

Couchbase.Lite.Mapping gives developers the ability to dynamically automatically convert generic objects to and from Couchbase document objects. This drastically reduces the amount of, often repeated, code needed to be written to store and retrieve information to and from Couchbase.Lite databases.

[![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/couchbaselabs/Couchbase.Lite.Mapping)

## Getting started ##
The Couchbase.Lite.Mapping project has the following dependencies:

- [Couchbase.Lite](https://github.com/couchbase/couchbase-lite-net) (2.x)

### Installation

Couchbase.Lite.Mapping is available via:

* NuGet Official Releases: [![GitHub release](https://img.shields.io/nuget/v/Couchbase.Lite.Mapping.svg?style=plastic)]()

### Documentation

To get started using [Couchbase.Lite](https://github.com/couchbase/couchbase-lite-net) please refer to the [official documentation](https://developer.couchbase.com/documentation/mobile/2.0/guides/couchbase-lite/index.html).


## Quick Start

### Sample

<p>
  <img src="images/login.png" width="200" title="hover text" style="margin-right: 25px;">
  <img src="images/profile.png" width="200" alt="accessibility text">
</p>

### Usage
```csharp
// An object to be converted to a document
public class TestObject
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
}

// Create an instance of the object
var testObject = new TestObject
{
    Value1 = "Couchbase",
    Value2 = "Rocks!"
};

// Convert the object into a Couchbase MutableDocument
var mutableDocument = testObject.ToMutableDocument();

// Convert a Couchbase MutableDocument into an object (of a type speficied via generic)
var newTestObject = mutableDocument.ToObject<TestObject>();
```

## Contributors ##
Couchbase.Lite.Mapping is an open source project and community contributions are welcome whether they be pull-requests, feedback or filing bug tickets or feature requests. We appreciate any contribution no matter how big or small!

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=plastic)](https://opensource.org/licenses/Apache-2.0)
