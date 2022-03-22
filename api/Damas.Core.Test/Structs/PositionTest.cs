using System.Collections.Generic;
using Damas.Core.Structs;
using NUnit.Framework;

namespace Damas.Test.Validators;

public class PositionTest
{
    [Test]
    public void ShouldComparePositionsEquality()
    {
        Assert.AreEqual(new Position(0, 0), new Position(0, 0));
    }

    [Test]
    public void ShouldComparePositionsEqualityUsingExplicitOperator()
    {
        Assert.True(new Position(0, 0) == new Position(0, 0));
    }

    [Test]
    public void ShouldComparePositionsInequality()
    {
        Assert.AreNotEqual(new Position(0, 0), new Position(0, 1));
    }

    [Test]
    public void ShouldComparePositionsInequalityUsingExplicitOperator()
    {
        Assert.True(new Position(0, 0) != new Position(0, 1));
    }

    [Test]
    public void ShouldComparePositionWithObject()
    {
        Assert.AreNotEqual(new Position(0, 0), new object());
    }

    [Test]
    public void ShouldCalculateDistanceBetweenTwoPositions()
    {
        Assert.AreEqual(1, new Position(0, 0).Distance(new Position(1, 1)));
        Assert.AreEqual(2, new Position(0, 0).Distance(new Position(2, 2)));
        Assert.AreEqual(3, new Position(0, 0).Distance(new Position(3, 3)));
        Assert.AreEqual(4, new Position(0, 0).Distance(new Position(4, 4)));
        Assert.AreEqual(5, new Position(0, 0).Distance(new Position(5, 5)));
        Assert.AreEqual(6, new Position(0, 0).Distance(new Position(6, 6)));
        Assert.AreEqual(7, new Position(0, 0).Distance(new Position(7, 7)));
        Assert.AreEqual(7, new Position(7, 7).Distance(new Position(0, 0)));
        Assert.AreEqual(6, new Position(7, 7).Distance(new Position(1, 1)));
        Assert.AreEqual(5, new Position(7, 7).Distance(new Position(2, 2)));
        Assert.AreEqual(4, new Position(7, 7).Distance(new Position(3, 3)));
        Assert.AreEqual(3, new Position(7, 7).Distance(new Position(4, 4)));
        Assert.AreEqual(2, new Position(7, 7).Distance(new Position(5, 5)));
        Assert.AreEqual(1, new Position(7, 7).Distance(new Position(6, 6)));
    }

    [Test]
    public void ShouldCalculatePositionsBetweenTwoPositions()
    {
        Assert.That(new Position(0, 0).Between(new Position(7, 7)), Is.EquivalentTo(
            new List<Position>()
            {
                new Position(1, 1),
                new Position(2, 2),
                new Position(3, 3),
                new Position(4, 4),
                new Position(5, 5),
                new Position(6, 6),
            })
        );

        Assert.That(new Position(7, 7).Between(new Position(0, 0)), Is.EquivalentTo(
            new List<Position>()
            {
                new Position(1, 1),
                new Position(2, 2),
                new Position(3, 3),
                new Position(4, 4),
                new Position(5, 5),
                new Position(6, 6),
            })
        );

        Assert.That(new Position(0, 6).Between(new Position(6, 0)), Is.EquivalentTo(
            new List<Position>()
            {
                new Position(1, 5),
                new Position(2, 4),
                new Position(3, 3),
                new Position(4, 2),
                new Position(5, 1),
            })
        );

        Assert.That(new Position(7, 5).Between(new Position(2, 0)), Is.EquivalentTo(
            new List<Position>()
            {
                new Position(6, 4),
                new Position(5, 3),
                new Position(4, 2),
                new Position(3, 1),
            })
        );
    }
}