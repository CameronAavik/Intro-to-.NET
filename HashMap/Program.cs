// using is the same as Java's import
using System;
using System.Collections.Generic;

// namespaces are a bit like Java packages, except that it doesn't have to match up with the directory
// this means you can use this same namespace anywhere, and even extend someone else's namespace.
namespace HashMap
{
    // This is a Map class with K and V generic types. Its full name is HashMap.Map
    public class Map<K, V>
    {
        // inner private class, not accessible from outside Map
        class Entry {
            // Here we have a private field called _key which we want to be accessible outside the class
            private K _key;
            // In Java, you could use a getter/setter to achieve this. C# has getters and setters built in as features
            public K Key 
            { 
                get 
                {
                    return _key;
                } 
                set
                {
                    _key = value;
                }
            }

            // To use this getter/setter, you simply do entry.Key where entry is an instance of Entry. No need to do entry.getKey() or setKey() like Java!
            
            // Since most properties will just want to get/set by default, we can use the following shorthand
            // which is equivalent to what we did above
            public V Value { get; set; }
        }

        // this is a private field to Map
        private int _numBuckets;

        // this is an array of lists where each list contains Entry objects
        private List<Entry>[] _buckets;

        // Here you can set it such that the Count variable has a public getter and a private setter.
        // We will use this to represent the number of items in the map. It is defined later.
        public int Count { get; private set; }

        // constructor, can optionally take in number of buckets to use
        public Map(int buckets=64) 
        {
            _numBuckets = buckets;
            // create array of buckets
            _buckets = new List<Entry>[buckets];
            // populate each bucket with empty list
            // var is used when we want to infer the type. C# knows 0 is an int, so it infers i is an int
            for (var i = 0; i < buckets; i++)
                _buckets[i] = new List<Entry>();
            // initialise count variable
            Count = 0;
        }
        
        // private method only available to HashMap
        // uses an expression body, int A() => 2; is equivalent to int A() { return 2; }
        private int GetBucketNumber(K key) => Math.Abs(key.GetHashCode()) % _numBuckets;

        public V Get(K key)
        {
            var bucketNumber = GetBucketNumber(key); // inferred int
            var bucket = _buckets[bucketNumber]; // inferred List<Entry>
            // foreach loop over a list
            foreach (var entry in bucket) // entry is inferred as Entry
            {
                if (entry.Key.Equals(key))
                {
                    return entry.Value;
                }
            }
            // Throwing exceptions does not need you to specify the exceptions which can be thrown at the top
            // String interpolation, better than concatenation! Put a $ at the start of the string to use this.
            // nameof(key) is evaluated at compile time to be "key", useful for automated refactoring.
            throw new KeyNotFoundException($"Parameter {nameof(key)} ({key}) is not a valid key for this map");
        }

        public void Put(K key, V value)
        {
            var bucketNumber = GetBucketNumber(key);
            var bucket = _buckets[bucketNumber];
            foreach (var entry in bucket)
            {
                if (entry.Key.Equals(key))
                {
                    entry.Value = value;
                    return;
                }
            }
            // An alternative to a constructor is an object initializer
            var entryToAdd = new Entry
            { 
                Key = key,
                Value = value
            };
            bucket.Add(entryToAdd);
            // Can set this value inside class, but not outside
            Count++;
        }

        public void Remove(K key)
        {
            var bucketNumber = GetBucketNumber(key);
            var bucket = _buckets[bucketNumber];
            Entry entryToRemove = null;
            foreach (var entry in bucket)
            {
                if (entry.Key.Equals(key))
                {
                    entryToRemove = entry;
                    break;
                }
            }
            if (entryToRemove == null)
                throw new KeyNotFoundException($"Parameter {nameof(key)} ({key}) is not a valid key for this map");
            bucket.Remove(entryToRemove);
            Count--;
        }
    }

    // Multiple classes per file!
    class Program
    {
        static void Main(string[] args)
        {
            var doubleNumbers = new Map<string, int>();
            doubleNumbers.Put("1", 2);
            doubleNumbers.Put("2", 4);
            doubleNumbers.Put("3", 6);
            Console.WriteLine($"Double of 1 is {doubleNumbers.Get("1")}"); // 2
            try
            {
                var double4 = doubleNumbers.Get("4");
                Console.WriteLine("Should have thrown an exception!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"There are now {doubleNumbers.Count} items in the map");
            // the below code won't compile because the setter is private!
            // doubleNumbers.Count = 4;
            doubleNumbers.Remove("2");
            Console.WriteLine($"There are now {doubleNumbers.Count} items in the map");
        }
    }
}
