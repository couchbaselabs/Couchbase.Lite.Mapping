**This is an independent, open source couchbaselabs project, and is NOT officially supported by Couchbase, Inc.**

# Couchbase.Lite.Mapping

`Couchbase.Lite.Mapping` gives developers the ability to dynamically automatically convert generic objects to and from Couchbase document objects. This drastically reduces the amount of, often repeated, code needed to be written to store and retrieve information to and from Couchbase Lite databases.

[![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/couchbaselabs/Couchbase.Lite.Mapping)

### Getting Started

**NOTE:** As of version 1.0.2, in order to use `Couchbase.Lite.Mapping` you must have either the [Couchbase.Lite](https://www.nuget.org/packages/Couchbase.Lite/) or [Couchbase.Lite.Enterprise](https://www.nuget.org/packages/Couchbase.Lite/) package installed. `Couchbase.Lite.Mapping` does **not** include dependencies so that it can work for both `Couchbase.Lite` and `Couchbase.Lite.Enterprise`.

`Couchbase.Lite.Mapping` is available via:

* NuGet Official Releases: [![GitHub release](https://img.shields.io/nuget/v/Couchbase.Lite.Mapping.svg?style=plastic)](https://www.nuget.org/packages/Couchbase.Lite.Mapping)

[Installing](https://docs.microsoft.com/en-us/nuget/consume-packages/ways-to-install-a-package) the NuGet package into your project will automatically install the Couchbase Lite dependency.

### Build
If you would like to build the package from source instead, follow the steps below

- Clone the repo
```
git clone https://github.com/couchbaselabs/Couchbase.Lite.Mapping
```
- Build the release version of project using Visual Studio
- Build the package
```
cd /path/to/repo/src/Couchbase.Lite.Mapping/packaging/nuget

nuget pack Couchbase.Lite.Mapping.nuspec
```

### Documentation

To get started using [Couchbase.Lite](https://github.com/couchbase/couchbase-lite-net) or [Couchbase.Lite.Enterprise](https://www.nuget.org/packages/Couchbase.Lite.Enterprise/) please refer to the [official documentation](https://developer.couchbase.com/documentation/mobile/2.0/guides/couchbase-lite/index.html).


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

## Testing

### Sample App

A sample Xamarin.Forms solution (supporting iOS and Android) can be found within [Samples](https://github.com/couchbaselabs/Couchbase.Lite.Mapping/tree/master/sample/Couchbase.Lite.Mapping.Sample). Simply clone this repo, open [Couchbase.Lite.Mapping.Sample.sln](https://github.com/couchbaselabs/Couchbase.Lite.Mapping/blob/master/sample/Couchbase.Lite.Mapping.Sample/Couchbase.Lite.Mapping.Sample.sln), and build/run the application!

The sample app allows users to log in with _any_ username and password, and maintains a user profile per username. Profiles consist of basic information and an image that is persisted as a user profile Document in the Couchbase Lite database for a "logged in" user.

<p>
  <img src="images/login.png" width="200" title="hover text" style="margin-right:25px;" border="3px">
  <img src="images/profile.png" width="200" alt="accessibility text" border="5px">
</p>

## Contributors ##
Couchbase.Lite.Mapping is an open source project and community contributions are welcome whether they be pull-requests, feedback or filing bug tickets or feature requests.

**Please include appropriate unit tests with pull-requests.**

We appreciate any contribution no matter how big or small!


[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=plastic)](https://opensource.org/licenses/Apache-2.0)
 
(**Note:** Use of `Couchbase.Lite.Mapping` has no implications on the `Couchbase.Lite.Enterprise` license agreement.)

