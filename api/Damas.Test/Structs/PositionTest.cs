using Damas.Structs;
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
}