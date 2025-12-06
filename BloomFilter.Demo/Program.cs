using BloomFilter;
using Murmur;
using System;
using System.Text;
using BloomFilter;

var words = new List<string>
{
    "cat", "shit", "weed", "chronic", "bubonic", "babayaga", "Luigi Mangione", "booba"
};

var words2 = new List<string>
{
    "bitch", "work", "Coriolanus", "boombox", "Stankonia"
};

var filter = new BloomFilter.BloomFilter(1000, 3);

foreach (var word in words) filter.AddWord(word);

var present = true;
foreach (var word in words) present = present & filter.CheckWord(word);

var g = words2.Select(x => filter.CheckWord(x)).ToList();
var a = "a";





