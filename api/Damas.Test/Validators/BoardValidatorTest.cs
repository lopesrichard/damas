using System;
using System.Collections.Generic;
using Damas.Entities;
using Damas.Enums;
using Damas.Exceptions;
using Damas.Structs;
using Damas.Validators;
using NUnit.Framework;

namespace Damas.Test.Validators;

public class BoardValidatorTest
{
    [Test]
    public void ShouldReturnNotEnoughPiecesExceptionWhenDontHaveWhiteOrBlackPieces()
    {
        var validator = new BoardValidator();

        var board = new Board(Guid.NewGuid(), BoardSize.SIXTY_FOUR_SQUARES, new List<Piece>());

        Assert.That(validator.Validate(board), Has.Some.InstanceOf<NotEnoughPiecesException>());
    }

    [Test]
    public void ShouldReturnTooManyPiecesExceptionWhenHaveMoreThanMaximumNumberOfWhiteOrBlackPieces()
    {
        var validator = new BoardValidator();

        var board = new Board(Guid.NewGuid(), BoardSize.SIXTY_FOUR_SQUARES, new List<Piece>()
        {
            new Piece(Guid.NewGuid(), new Position(0, 0), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(0, 2), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(0, 4), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(0, 6), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(2, 0), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(2, 2), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(2, 4), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(2, 6), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(4, 0), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(4, 2), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(4, 4), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(4, 6), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(4, 6), Color.WHITE, false),
        });

        Assert.That(validator.Validate(board), Has.Some.InstanceOf<TooManyPiecesException>());
    }

    [Test]
    public void ShouldReturnPieceInSamePositionExceptionWhenHaveTwoOrMorePiecesInSamePosition()
    {
        var validator = new BoardValidator();

        var board = new Board(Guid.NewGuid(), BoardSize.SIXTY_FOUR_SQUARES, new List<Piece>()
        {
            new Piece(Guid.NewGuid(), new Position(0, 0), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(0, 0), Color.WHITE, false),
        });

        Assert.That(validator.Validate(board), Has.Some.InstanceOf<PieceInSamePositionException>());
    }
}