using System;
using System.Collections.Generic;
using System.Linq;
using Damas.Algorithms;
using Damas.Enums;
using Damas.Models;
using Damas.Serialization;
using Damas.Structs;
using NUnit.Framework;

namespace Damas.Test.Algorithms;

public class MoveSelectorTest
{
    private Match BuildMatch(ICollection<Piece> pieces)
    {
        var playerOne = new Player("Player 1");
        var playerTwo = new Player("Player 2");

        var playerOneColor = Color.WHITE;
        var playerTwoColor = Color.BLACK;

        var board = new Board(BoardSize.SIXTY_FOUR_SQUARES, pieces);

        return new Match(playerOne, playerOneColor, playerTwo, playerTwoColor, board, Color.WHITE);
    }

    // ==============================================================================================
    // ============================= REGULAR PIECE NON CAPTURE TESTS ================================
    // ==============================================================================================

    // -------------------------------------
    // The following scenario is returned
    // -------------------------------------
    // | 7 |   |   |   |   |   |   |   |   |
    // | 6 |   |   |   |   |   |   |   |   |
    // | 5 |   |   |   |   |   |   |   |   |
    // | 4 |   |   |   |   |   |   |   |   |
    // | 3 |   |   |   | B |   | B |   | B |
    // | 2 |   |   |   |   | B |   | B |   |
    // | 1 |   | W |   | W |   | W |   |   |
    // | 0 |   |   |   |   |   |   |   |   |
    // | - | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
    // -------------------------------------
    private Match GetNonCaptureTestCase()
    {
        return BuildMatch(new List<Piece>()
        {
            new Piece(new Position(1, 1), Color.WHITE, false, false),
            new Piece(new Position(3, 1), Color.WHITE, false, false),
            new Piece(new Position(5, 1), Color.WHITE, false, false),
            new Piece(new Position(4, 2), Color.BLACK, false, false),
            new Piece(new Position(6, 2), Color.BLACK, false, false),
            new Piece(new Position(3, 3), Color.BLACK, false, false),
            new Piece(new Position(5, 3), Color.BLACK, false, false),
            new Piece(new Position(7, 3), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldSelectRegularPieceNonCaptureMoves()
    {
        var match = GetNonCaptureTestCase();

        var calculator = new MoveCalculator();

        var moves = calculator.Calculate(match);

        // Before selection
        Assert.That(moves.ElementAt(0).Root.Children, Has.Count.EqualTo(2));
        Assert.That(moves.ElementAt(1).Root.Children, Has.Count.EqualTo(1));
        Assert.That(moves.ElementAt(2).Root.Children, Has.Count.EqualTo(0));

        var selector = new MoveSelector();

        var selected = moves.Select(selector.Select);

        // After selection
        Assert.That(selected.ElementAt(0).Root.Children, Has.Count.EqualTo(2));
        Assert.That(selected.ElementAt(1).Root.Children, Has.Count.EqualTo(1));
        Assert.That(selected.ElementAt(2).Root.Children, Has.Count.EqualTo(0));

        // Check selected nodes
        Assert.AreEqual(new Position(1, 1), selected.ElementAt(0).Root.Value);
        Assert.AreEqual(new Position(0, 2), selected.ElementAt(0).Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 2), selected.ElementAt(0).Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(3, 1), selected.ElementAt(1).Root.Value);
        Assert.AreEqual(new Position(2, 2), selected.ElementAt(1).Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(5, 1), selected.ElementAt(2).Root.Value);
    }

    // ==============================================================================================
    // =============================== REGULAR PIECE CAPTURE TESTS ==================================
    // ==============================================================================================

    // -------------------------------------
    // The following scenario is returned
    // -------------------------------------
    // | 7 |   |   |   |   |   |   |   |   |
    // | 6 |   |   |   |   |   |   |   |   |
    // | 5 |   | B |   | B |   |   |   |   |
    // | 4 |   |   |   |   |   |   |   |   |
    // | 3 |   | B |   | B |   | B |   |   |
    // | 2 | W |   |   |   |   |   |   |   |
    // | 1 |   | B |   | B |   |   |   |   |
    // | 0 |   |   |   |   |   |   |   |   |
    // | - | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
    // -------------------------------------
    private Match GetCaptureTestCase()
    {
        return BuildMatch(new List<Piece>()
        {
            new Piece(new Position(0, 2), Color.WHITE, false, false),
            new Piece(new Position(1, 1), Color.BLACK, false, false),
            new Piece(new Position(1, 3), Color.BLACK, false, false),
            new Piece(new Position(1, 5), Color.BLACK, false, false),
            new Piece(new Position(3, 1), Color.BLACK, false, false),
            new Piece(new Position(3, 3), Color.BLACK, false, false),
            new Piece(new Position(3, 5), Color.BLACK, false, false),
            new Piece(new Position(5, 3), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldSelectRegularPieceCaptureMoves()
    {
        var match = GetCaptureTestCase();

        var calculator = new MoveCalculator();

        var moves = calculator.Calculate(match);

        // Before selection
        Assert.That(moves.ElementAt(0).Root.Children, Has.Count.EqualTo(2));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children, Has.Count.EqualTo(3));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(1).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children, Has.Count.EqualTo(2));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(0).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children, Has.Count.EqualTo(1));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(0).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children, Has.Count.EqualTo(1));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(3));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children, Has.Count.EqualTo(2));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(1).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(0));
        Assert.That(moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(2).Children, Has.Count.EqualTo(0));

        Assert.AreEqual(new Position(0, 2), moves.ElementAt(0).Root.Value);
        Assert.AreEqual(new Position(2, 4), moves.ElementAt(0).Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 6), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(4, 2), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(6, 4), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 0), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(0, 2), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 0), moves.ElementAt(0).Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(4, 2), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 6), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(0, 2), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(6, 4), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(1).Value);

        var selector = new MoveSelector();

        var selected = moves.Select(selector.Select);

        var json = selected.Serialize();

        // After selection
        Assert.That(selected.ElementAt(0).Root.Children, Has.Count.EqualTo(2));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(0).Children, Has.Count.EqualTo(1));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(1));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(1));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(0));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(1).Children, Has.Count.EqualTo(1));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children, Has.Count.EqualTo(1));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(3));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(0));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Children, Has.Count.EqualTo(0));
        Assert.That(selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(2).Children, Has.Count.EqualTo(0));

        Assert.AreEqual(new Position(0, 2), selected.ElementAt(0).Root.Value);
        Assert.AreEqual(new Position(2, 4), selected.ElementAt(0).Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 2), selected.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 0), selected.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 2), selected.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 0), selected.ElementAt(0).Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(4, 2), selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 6), selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(0, 2), selected.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(2).Value);
    }
}