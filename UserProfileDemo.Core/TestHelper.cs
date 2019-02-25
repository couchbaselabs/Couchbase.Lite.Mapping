using System;
using System.Collections.Generic;
using Couchbase.Lite.Mapping;

namespace UserProfileDemo.Core
{
    /*
    var person = new Person
    {
        PeopleMap = new HashSet<Person> { new Person { Name = "Map" } },
        Gender = Gender.Male,
        Name = "Rob",
        IsCool = true,
        Spouse = new Person
        {
            Gender = Gender.Female,
            Name = "Tracy",
            IsCool = false
        },
        TwoNumbers = new Tuple<int, int>(1, 2),
        ChildrenList = new List<Person> { new Person { Gender = Gender.Male, Name = "Lil' Demon" } },
        ChildrenArray = new Person[] { new Person { Gender = Gender.Male, Name = "Lil' Demon" } }
    };

    person.ChildrenQueue = new Queue<Person>();
    person.ChildrenQueue.Enqueue(new Person { Name = "Q1" });
    person.ChildrenQueue.Enqueue(new Person { Name = "Q2" });

    person.ChildrenStack = new Stack<Person>();
    person.ChildrenStack.Push(new Person { Name = "Q1" });
    person.ChildrenStack.Push(new Person { Name = "Q2" });

    var doc1 = person.ToMutableDocument();

    var p1 = doc1.ToObject<Person>();
    */   

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Person : DocumentObject
    {
        public Gender Gender { get; set; }
        public string Name { get; set; }
        public bool? IsCool { get; set; }
        public Person Spouse { get; set; }
        public List<Person> ChildrenList { get; set; }
        public Person[] ChildrenArray { get; set; }
        public HashSet<Person> PeopleMap { get; set; }
        public Tuple<int, int> TwoNumbers { get; set; }
        public Queue<Person> ChildrenQueue { get; set; }
        public Stack<Person> ChildrenStack { get; set; }
    }
}
