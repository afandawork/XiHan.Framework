﻿using JetBrains.Annotations;
using XiHan.Framework.Utils.Collections;
using XiHan.Framework.Utils.DataFilter.Pages.Dtos;
using XiHan.Framework.Utils.DataFilter.Pages.Enums;

namespace XiHan.Framework.Utils.Test.Collections;

[TestSubject(typeof(EnumerableExtensions))]
public class EnumerableExtensionsTest
{
    [Fact]
    public void JoinAsString_JoinsStringsWithSeparator()
    {
        var source = new List<string>
        {
            "a", "b", "c"
        };
        var result = source.JoinAsString(", ");
        Assert.Equal("a, b, c", result);
    }

    [Fact]
    public void JoinAsString_ReturnsEmptyString_WhenSourceIsEmpty()
    {
        var source = new List<string>();
        var result = source.JoinAsString(", ");
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void JoinAsStringWithGeneric_JoinsObjectsWithSeparator()
    {
        var source = new List<int>
        {
            1, 2, 3
        };
        var result = source.JoinAsString(", ");
        Assert.Equal("1, 2, 3", result);
    }

    [Fact]
    public void Where_ReturnsFilteredResults_WhenConditionIsMet()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 1, Name = "Alice"
            },
            new()
            {
                Id = 2, Name = "Bob"
            }
        };

        var result = data.Where("Name", "Alice");

        _ = Assert.Single(result);
        Assert.Equal("Alice", result.First().Name);
    }

    [Fact]
    public void Where_ReturnsEmptyResults_WhenConditionIsNotMet()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 1, Name = "Alice"
            },
            new()
            {
                Id = 2, Name = "Bob"
            }
        };

        var result = data.Where("Name", "Charlie");

        Assert.Empty(result);
    }

    [Fact]
    public void WhereMultiple_ReturnsFilteredResults_WhenConditionsAreMet()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 1, Name = "Alice"
            },
            new()
            {
                Id = 2, Name = "Bob"
            },
            new()
            {
                Id = 3, Name = "Alice"
            }
        };

        var conditions = new List<SelectConditionDto>
        {
            new("Name", "Alice", SelectCompareEnum.Equal)
        };

        var result = data.WhereMultiple(conditions);

        Assert.Equal(2, result.Count());
        Assert.All(result, item => Assert.Equal("Alice", item.Name));
    }

    [Fact]
    public void WhereIf_ReturnsFilteredResults_WhenConditionIsTrue()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 1, Name = "Alice"
            },
            new()
            {
                Id = 2, Name = "Bob"
            }
        };

        var result = data.WhereIf(true, x => x.Name == "Alice");

        _ = Assert.Single(result);
        Assert.Equal("Alice", result.First().Name);
    }

    [Fact]
    public void WhereIf_ReturnsOriginalResults_WhenConditionIsFalse()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 1, Name = "Alice"
            },
            new()
            {
                Id = 2, Name = "Bob"
            }
        };

        var result = data.WhereIf(false, x => x.Name == "Alice");

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void OrderBy_SortsResultsInAscendingOrder()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 2, Name = "Bob"
            },
            new()
            {
                Id = 1, Name = "Alice"
            }
        };

        var result = data.OrderBy("Name", SortDirectionEnum.Asc);

        Assert.Equal("Alice", result.First().Name);
        Assert.Equal("Bob", result.Last().Name);
    }

    [Fact]
    public void OrderBy_SortsResultsInDescendingOrder()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 1, Name = "Alice"
            },
            new()
            {
                Id = 2, Name = "Bob"
            }
        };

        var result = data.OrderBy("Name", SortDirectionEnum.Desc);

        Assert.Equal("Bob", result.First().Name);
        Assert.Equal("Alice", result.Last().Name);
    }

    [Fact]
    public void ThenBy_SortsResultsInSecondaryOrder()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 2, Name = "Alice"
            },
            new()
            {
                Id = 5, Name = "Bob"
            },
            new()
            {
                Id = 8, Name = "Alice"
            },
            new()
            {
                Id = 1, Name = "Bob"
            },
            new()
            {
                Id = 3, Name = "Alice"
            }
        };

        var result = data.OrderBy("Name", SortDirectionEnum.Asc).ThenBy("Id", SortDirectionEnum.Desc);

        Assert.Equal(8, result.ElementAt(0).Id);
        Assert.Equal(3, result.ElementAt(1).Id);
    }

    [Fact]
    public void OrderByMultiple_SortsResultsByMultipleConditions()
    {
        var data = new List<TestEntity>
        {
            new()
            {
                Id = 2, Name = "Alice"
            },
            new()
            {
                Id = 5, Name = "Bob"
            },
            new()
            {
                Id = 8, Name = "Alice"
            },
            new()
            {
                Id = 1, Name = "Bob"
            },
            new()
            {
                Id = 3, Name = "Alice"
            }
        };

        var conditions = new List<SortConditionDto>
        {
            new("Name", SortDirectionEnum.Asc), new("Id", SortDirectionEnum.Desc)
        };

        var result = data.OrderByMultiple(conditions);

        Assert.Equal(8, result.ElementAt(0).Id);
        Assert.Equal(3, result.ElementAt(1).Id);
    }
}
