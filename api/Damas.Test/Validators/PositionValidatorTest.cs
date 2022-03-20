using System;
using System.Collections.Generic;
using System.Linq;
using Damas.Entities;
using Damas.Enums;
using Damas.Exceptions;
using Damas.Structs;
using Damas.Validators;
using NUnit.Framework;

namespace Damas.Test.Validators;

public class PositionValidatorTest
{
    [Test]
    public void ShouldReturnNoExceptionsWhenPositionIsValidToSixtyFourSquaresBoardSize()
    {
        var validator = new PositionValidator();

        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(0, 0)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(0, 2)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(0, 4)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(0, 6)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(2, 0)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(2, 2)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(2, 4)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(2, 6)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(4, 0)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(4, 2)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(4, 4)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(4, 6)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(6, 0)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(6, 2)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(6, 4)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(6, 6)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(1, 1)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(1, 3)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(1, 5)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(1, 7)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(3, 1)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(3, 3)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(3, 5)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(3, 7)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(5, 1)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(5, 3)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(5, 5)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(5, 7)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(7, 1)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(7, 3)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(7, 5)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(7, 7)), Is.Empty);
    }

    [Test]
    public void ShouldReturnNoExceptionsWhenPositionIsValidToOneHundredSquaresBoardSize()
    {
        var validator = new PositionValidator();

        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(0, 8)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(2, 8)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(4, 8)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(6, 8)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(8, 8)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(1, 9)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(3, 9)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(5, 9)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(7, 9)), Is.Empty);
        Assert.That(validator.Validate(BoardSize.ONE_HUNDRED_SQUARES, new Position(9, 9)), Is.Empty);
    }

    [Test]
    public void ShouldReturnNonPlayablePositionExceptionWhenPositionIsNotValidToSixtyFourSquaresBoardSize()
    {
        var validator = new PositionValidator();

        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(0, 8)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(2, 8)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(4, 8)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(6, 8)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(8, 8)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(1, 9)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(3, 9)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(5, 9)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(7, 9)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(9, 9)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
    }

    [Test]
    public void ShouldReturnCoordinateOutOfBoundExceptionWhenPositionIsOutOfBounds()
    {
        var validator = new PositionValidator();

        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(100, 100)), Has.Some.InstanceOf<CoordinateOutOfBoundException>());
    }

    [Test]
    public void ShouldReturnNonPlayablePositionExceptionWhenPositionIsNonPlayableSquare()
    {
        var validator = new PositionValidator();

        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(0, 1)), Has.Some.InstanceOf<NonPlayablePositionException>());
        Assert.That(validator.Validate(BoardSize.SIXTY_FOUR_SQUARES, new Position(1, 0)), Has.Some.InstanceOf<NonPlayablePositionException>());
    }
}