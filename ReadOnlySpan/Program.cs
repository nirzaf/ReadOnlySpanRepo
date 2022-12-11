// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Console.WriteLine("Testing memory allocation using Span");

BenchmarkRunner.Run<Benchy>();

Benchy b1 = new();
var db1 = b1.DateWithStringAndSpan();
var db2 = b1.DateWithStringAndSpan();

Console.WriteLine($"db1: {db1.month}/{db1.day}/{db1.year}");
Console.WriteLine($"db2: {db2.month}/{db2.day}/{db2.year}");

Console.ReadLine();


[MemoryDiagnoser]
public class Benchy
{
    private readonly string  dateAsText = "08 07 2022";

    [Benchmark]
    public (int day, int month, int year) DateWithStringAndSubstring()
    {
        var daysAsText = dateAsText.Substring(0, 2);
        var monthsAsText = dateAsText.Substring(3, 2);
        var yearsAsText = dateAsText.Substring(6, 4);
        var day = int.Parse(daysAsText);
        var month = int.Parse(monthsAsText);
        var year = int.Parse(yearsAsText);
        return (day, month, year);
    }
    
    [Benchmark]
    public (int day, int month, int year) DateWithStringAndSpan()
    {
        var daysAsText = dateAsText.AsSpan(0, 2);
        var monthsAsText = dateAsText.AsSpan(3, 2);
        var yearsAsText = dateAsText.AsSpan(6, 4);
        var day = int.Parse(daysAsText);
        var month = int.Parse(monthsAsText);
        var year = int.Parse(yearsAsText);
        return (day, month, year);
    }
}