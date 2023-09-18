**This is an independent, open source couchbaselabs project, and is NOT officially supported by Couchbase, Inc.  Issues are disabled, but if you post a question on the [forums](https://www.couchbase.com/forums/) you might get an answer.**

# Couchbase.Lite.Mapping

`Couchbase.Lite.Mapping` gives developers the ability to dynamically automatically convert generic objects to/from Couchbase `Document` objects and lists of `Result` objects. This drastically reduces the amount of, often repeated, code needed to be written to store and retrieve information to and from Couchbase Lite databases.

# Table of Contents
1. [Getting Started](#gettingstarted)
2. [Building the Project (optional)](#build)
3. [Basic Usage: Object/Document](#basicusage1)
4. [Basic Usage: IResultSet to Object(s)](#basicusage2)
5. [Advanced Usage: IResultSet to Object(s)](#advusage)
6. [Customizing Property Name Serialization](#custom-property-serialization)
    1. [Globally](#custom-property-serialization-global)
    2. [By Document](#custom-property-serialization-document)
    3. [By Property](#custom-property-serialization-property)
7. [Testing](#testing)

### Getting Started <a name="gettingstarted"></a>

**NOTE:**
As of version 1.0.2, in order to use `Couchbase.Lite.Mapping` you must have either the [Couchbase.Lite](https://www.nuget.org/packages/Couchbase.Lite/) or [Couchbase.Lite.Enterprise](https://www.nuget.org/packages/Couchbase.Lite/) package installed.

`Couchbase.Lite.Mapping` does **not** include dependencies so that it can work with both `Couchbase.Lite` and `Couchbase.Lite.Enterprise`. This also provides the flexibility to install any compatible version of Couchbase.lite.

`Couchbase.Lite.Mapping` is available via:
* NuGet Official Releases: [![GitHub release](https://img.shields.io/nuget/v/Couchbase.Lite.Mapping.svg?style=plastic)](https://www.nuget.org/packages/Couchbase.Lite.Mapping)


### Build (optional) <a name="build"></a>
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

### Documentation <a name="documentation"></a>

To get started using [Couchbase.Lite](https://github.com/couchbase/couchbase-lite-net) or [Couchbase.Lite.Enterprise](https://www.nuget.org/packages/Couchbase.Lite.Enterprise/) please refer to the [official documentation](https://developer.couchbase.com/documentation/mobile/2.0/guides/couchbase-lite/index.html).


### Basic Usage: Object/Document <a name="basicusage1"></a>

Create a new document with a new object.
```csharp
// An object to be converted to a document
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

// Create an instance of the object
var person = new Person
{
    FirstName = "Clark",
    LastName = "Kent"
};

// Convert the object into a Couchbase.Lite MutableDocument
var newMutableDocument = person.ToMutableDocument();

// Convert a Couchbase.Lite MutableDocument into an object (of a type specified via generic)
var newPerson = newMutableDocument.ToObject<Person>();
```

Modify an existing document.
```csharp
// You can provide the document ID to the ToMutableDocument extension method
// Where "person" is a previously retrieved (and mapped) document, and "id" is a known document ID.
var existingMutableDocument = person.ToMutableDocument($"person::{id}"); 
```

**Note:** The `ToMutableDocument` extension method also 

### Basic Usage: IResultSet to Object(s) <a name="basicusage2"></a>

#### Default (old) approach
```csharp
var query = QueryBuilder.Select(SelectResult.All()) 
                                        .From(DataSource.Database(database)) 
                                        .Where(whereQueryExpression); 
                                        
var results = query?.Execute()?.AllResults();

if (results?.Count > 0)
{
    personList = new List<Person>();

    foreach (var result in results)
    {
        // Where 'people' is the containing Dictionary key
        var dictionary = result.GetDictionary("people");

        if (dictionary != null)
        {
            var person = new Person
            {
                FirstName = dictionary.GetString("firstName"), 
                LastName = dictionary.GetString("lastName") 
            };

            personList.Add(person);
        }
    }
}
```
#### Couchbase.Lite.Mapping approach
```csharp
var query = QueryBuilder.Select(SelectResult.All()) 
                                        .From(DataSource.Database(database)) 
                                        .Where(whereQueryExpression); 

var results = query?.Execute()?.AllResults();

// Map all of the results to a list of objects
var personList = results?.ToObjects<Person>(); 

// OR map a single result item to an object
var person = results?[0].ToObject<Person>();

```

### Advanced Usage: IResultSet to Object(s)<a name="advusage"></a>

You can also map more complex queriies. 

Returning Meta ID (and any other valid column)
```csharp
public class Person
{ 
    public string Type => "person";
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

var query = QueryBuilder.Select(SelectResult.Expression(Meta.ID), SelectResult.All())
            .From(DataSource.Database(Database))
            .Where(Expression.Property("type").EqualTo(Expression.String("person")));

var results = query?.Execute()?.AllResults();

var people = results?.ToObjects<Person>();
```

Data aggregation
```csharp
public class PersonStats
{ 
    public int Count { get; set; }
    public string Type { get; set; }
}

var query = QueryBuilder.Select(
                        SelectResult.Expression(Function.Count(Expression.All())).As("count"),
                        SelectResult.Property("type"))
                       .From(DataSource.Database(Database))
                       .Where(Expression.Property("type").NotNullOrMissing())
                       .GroupBy(Expression.Property("type"))
                       .OrderBy(Ordering.Property("type").Ascending());

var results = query?.Execute()?.AllResults();

var personStats = results?[0].ToObjects<PersonStats>(); 
```

### Customizing Property Name Serialization <a name="custom-property-serialization"></a>

The default serialization for object property names into Couchbase Lite databases uses **Lower Camel Case** (e.g. lowerCamelCase).

By default the following object
```csharp
var person = new Person
{
    FirstName = "Bruce",
    LastName = "Wayne"
};
```
will look like the following in JSON.

```json
{
    "firstName": "Bruce",
    "lastName": "Wayne"
}
```
*Note the casing of `firstName` and `lastName`.*

#### Globally <a name="custom-property-serialization-global"></a>
You can override the default implementation of `IPropertyNameConverter` by setting `Couchbase.Lite.Mapping.Setting.PropertyNameConverter`.

```csharp
using Couchbase.Lite.Mapping;
...

// Set this value to override the default IPropertyNameConverter
Settings.PropertyNameConverter = new CustomPropertyNameConverter();

// Here's an example of a custom implementation of IPropertyNameConverter
public class CustomPropertyNameConverter : IPropertyNameConverter
{
    public string Convert(string propertyName)
    {
      return propertyName.ToUpper();
    }
}
```

Using `CustomerPropertyNameConverter` will yield the following JSON seralization for `Person`.

```json
{
    "FIRSTNAME": "Bruce",
    "LASTNAME": "Wayne"
}
```

#### By Document <a name="custom-property-serialization-document"></a>

You can override the default implementation of `IPropertyNameConverter` at the document level by passing in an instance of a class that implements `IPropertyNameConverter` into the `ToMutableDocument` extension method.

```csharp
var mutableDocument = testObject.ToMutableDocument(new CustomPropertyNameConverter());
```

#### By Property <a name="custom-property-serialization-property"></a>

You can override the default implementation of `IPropertyNameConverter` at the property level by adding a `MappingPropertyName` attribute above a property.

```csharp
using Couchbase.Lite.Mapping;

public class Person
{
    [MappingPropertyName("fIRStNaME")]
    public string FirstName { get; set; }

    // This property will be converted using the default converter
    public string LastName { get; set; }
}
```

Using `MappingPropertyName` (like above) will yield the following JSON seralization for `Person`.

```json
{
    "fIRStNaME": "Diana",
    "lastName": "Prince"
}
```

## Testing <a name="testing"></a>

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

## Licenses ##
The mapping package is available under
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=plastic)](https://opensource.org/licenses/Apache-2.0)
 
**Note:**: 
Use of `Couchbase.Lite.Mapping` has no implications on the `Couchbase.Lite.Enterprise` or `Couchbase.Lite` licenses. Those packages are governed by the terms of the Couchbase Enterprise Edition and Community Edition licenses respectively
