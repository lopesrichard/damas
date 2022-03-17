using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Damas.Algorithms;
using Damas.Entities;
using Damas.Enums;
using Damas.Structs;
using NUnit.Framework;

namespace Damas.Test.Algorithms;

public class MovesCalculatorTest
{
    [Test]
    public void ShouldReturnTreeStructureOfCaptureMoves()
    {
        var calculator = new MoveCalculator();

        var playerOne = new Player(Guid.NewGuid(), "Player 1", Color.WHITE);
        var playerTwo = new Player(Guid.NewGuid(), "Player 2", Color.BLACK);

        var pieces = new List<Piece>()
        {
            new Piece(Guid.NewGuid(), new Position(0, 2), Color.WHITE, false),
            new Piece(Guid.NewGuid(), new Position(1, 1), Color.BLACK, false),
            new Piece(Guid.NewGuid(), new Position(1, 3), Color.BLACK, false),
            new Piece(Guid.NewGuid(), new Position(1, 5), Color.BLACK, false),
            new Piece(Guid.NewGuid(), new Position(3, 1), Color.BLACK, false),
            new Piece(Guid.NewGuid(), new Position(3, 3), Color.BLACK, false),
            new Piece(Guid.NewGuid(), new Position(3, 5), Color.BLACK, false),
            new Piece(Guid.NewGuid(), new Position(5, 3), Color.BLACK, false),
        };

        var board = new Board(Guid.NewGuid(), BoardSize.SIXTY_FOUR_SQUARES, pieces);

        var captures = new List<Piece>();

        var match = new Match(Guid.NewGuid(), playerOne, playerOne.Id, playerTwo, playerTwo.Id, board, board.Id, playerOne, playerOne.Id, captures);

        var moves = calculator.CalculateAvailableMoves(match);
    }
}