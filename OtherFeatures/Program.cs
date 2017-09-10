using System;
using System.Collections.Generic;
using System.Linq;

namespace OtherFeatures
{
    class Program
    {
        static void Main() { }

        static void DemonstrateTypes()
        {
            // Signed Integral
            sbyte sbyteVar; // signed byte
            short shortVar;
            int intVar;
            long longVar;
            // Unsigned Integral
            byte byteVar;
            ushort ushortVar; // unsigned short
            uint uintVar; // unsigned int
            ulong ulongVar; // unsigned long
            // Unicode characters
            char charVar;
            // IEEE flaoting point
            float floatVar;
            double doubleVar;
            // High-precision decimal
            decimal decimalVar;
            // Boolean
            bool booVar;
            // Tuples!
            (string firstName, string lastName) name;
            name = ("John", "Smith");
            var firstName = name.firstName;
            // tuple deconstruction!
            (var fst, var snd) = name;
            Console.WriteLine(firstName == fst);
        }
    }

    enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    // not allocated on the heap!
    struct ExampleStruct
    {
        int fieldA;
        string fieldB;
    }

    class MoreCoolStuff
    {
        static void PrintNullableInt(int? nullableInt)
        {
            // nullableInt can either be null or an integer.
            if (nullableInt.HasValue) {
                Console.WriteLine(nullableInt.Value);
            } else {
                Console.WriteLine("null");
            }
        }
    }

    // Interfaces
    public interface IOperation
    {
        int operate(int a, int b);
    }

    // Implementing an interface, can also do multiple inheritance if you want
    public class AdditionOperator : IOperation
    {
        public int operate(int a, int b) => a + b;
    }

    class Node
    {
        string Name { get; set; }
        // node has one child
        Node Child { get; set; }
        // ?. is a null conditional operator
        // equivalent to following in Java: Child == null ? null : Child.Child
        Node Grandchild => Child?.Child;
        // ?? is a null coalescing operator
        // equivalent to following in Java: Name == null ? "No Name!" : Name
        string NameNonNullable => Name ?? "No Name!";
        // Can combine for really clean readable code
        string GrandChildName => Child?.Child?.Name ?? "No Name!";
        // Java equivalent:
        // Child != null && Child.Child != null && Child.Child.Name != null 
        //    ? Child.Child.Name
        //    : "No Name!"
    }

    class ExpressionSamples
    {
        static void SwitchPatternMatching(ValueType value)
        {
            // int and double extend ValueType
            switch(value)
            {
                case int i:
                    Console.WriteLine($"Integer: {i}");
                    break;
                case double d when d > 0:
                    Console.WriteLine($"{d} is positive");
                    break;
                case double d:
                    Console.WriteLine($"{d} is negative");
                    break;
            }
        }
    }

    class LinqExample
    {
        static IEnumerable<int> FilterNegative(IEnumerable<int> numbers)
        {
            // SQL Queries in C#?!!?!
            return from num in numbers
                where num >= 0
                select num;
        }

        static IEnumerable<int> AbsoluteNumbers(IEnumerable<int> numbers)
        {
            return from num in numbers
                select Math.Abs(num);
        }

        static IEnumerable<int> SortList(IEnumerable<int> numbers)
        {
            return from num in numbers
                orderby num ascending
                select num;
        }

        static IEnumerable<int> GetNumLengths(IEnumerable<int> numbers)
        {
            return from num in numbers
                group num by Math.Abs(num).ToString().Length into numDigits
                orderby numDigits.Key ascending
                select numDigits.Key;
        }

        static IEnumerable<int> FilterNegativeMethodSyntax(IEnumerable<int> numbers)
        {
            return numbers.Where(num => num >= 0);
        }

        static IEnumerable<int> AbsoluteNumbersMethodSyntax(IEnumerable<int> numbers)
        {
            return numbers.Select(num => Math.Abs(num));
            // could also do numbers.Select(Math.Abs);
        }

        static IEnumerable<int> SortListMethodSyntax(IEnumerable<int> numbers)
        {
            return numbers.OrderBy(num => num);
        }

        static IEnumerable<int> GetNumLengthsMethodSyntax(IEnumerable<int> numbers)
        {
            return numbers
                .GroupBy(num => Math.Abs(num).ToString().Length)
                .OrderBy(group => group.Key)
                .Select(group => group.Key);
        }
    }
}
