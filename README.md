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

### Sample App

A sample Xamarin.Forms solution (supporting iOS and Android) can be found within [Samples](https://github.com/couchbaselabs/Couchbase.Lite.Mapping/tree/master/sample/Couchbase.Lite.Mapping.Sample). Simply clone this repo, open [Couchbase.Lite.Mapping.Sample.sln](https://github.com/couchbaselabs/Couchbase.Lite.Mapping/blob/master/sample/Couchbase.Lite.Mapping.Sample/Couchbase.Lite.Mapping.Sample.sln), and build/run the application!

The sample app allows users to log in with any username and password, and maintains a user profile per username. Profiles consist of basic information and an image that are stored in a Couchbase.Lite database for the "logged in" user.

<p>
  <img src="images/login.png" width="200" title="hover text" style="margin-right:25px;" border="3px">
  <img src="images/profile.png" width="200" alt="accessibility text" border="5px">
</p>

### Basic Usage
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

// Convert the object into a Couchbase.Lite MutableDocument
var mutableDocument = testObject.ToMutableDocument();

// Convert a Couchbase.Lite MutableDocument into an object (of a type specified via generic)
var newTestObject = mutableDocument.ToObject<TestObject>();
```

## Contributors ##
Couchbase.Lite.Mapping is an open source project and community contributions are welcome whether they be pull-requests, feedback or filing bug tickets or feature requests. **Please include appropriate unit tests with pull-requests.**

We appreciate any contribution no matter how big or small!

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=plastic)](https://opensource.org/licenses/Apache-2.0)
