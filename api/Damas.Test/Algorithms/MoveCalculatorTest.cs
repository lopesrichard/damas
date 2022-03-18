using System;
using System.Collections.Generic;
using System.Linq;
using Damas.Algorithms;
using Damas.Entities;
using Damas.Enums;
using Damas.Structs;
using NUnit.Framework;

namespace Damas.Test.Algorithms;

public class MoveCalculatorTest
{
    private Match BuildMatch(ICollection<Piece> pieces)
    {
        var playerOne = new Player(Guid.NewGuid(), "Player 1", Color.WHITE);
        var playerTwo = new Player(Guid.NewGuid(), "Player 2", Color.BLACK);

        var board = new Board(Guid.NewGuid(), BoardSize.SIXTY_FOUR_SQUARES, pieces);

        var captures = new List<Piece>();

        return new Match(Guid.NewGuid(), playerOne, playerOne.Id, playerTwo, playerTwo.Id, board, board.Id, playerOne, playerOne.Id, captures);
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
            new Piece(Guid.NewGuid(), new Position(0, 2), Color.WHITE, false, false),
            new Piece(Guid.NewGuid(), new Position(1, 1), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(1, 3), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(1, 5), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 1), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 3), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 5), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(5, 3), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldReturnOnePieceCaptureMoves()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);
        Assert.That(moves, Has.Count.EqualTo(1));
    }

    [Test]
    public void ShouldCountTreeNodes()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);
        var tree = moves.First().Value;
        Assert.AreEqual(15, tree.CountNodes());
    }

    [Test]
    public void ShouldCountTreeLeaves()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);
        var tree = moves.First().Value;
        Assert.AreEqual(8, tree.CountLeaves());
    }

    [Test]
    public void ShouldReturnCorrectNodes()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);

        var tree = moves.First().Value;

        Assert.That(tree.Root.Children, Has.Count.EqualTo(2));
        Assert.That(tree.Root.Children[0].Children, Has.Count.EqualTo(3));
        Assert.That(tree.Root.Children[0].Children[0].Children, Is.Empty);
        Assert.That(tree.Root.Children[0].Children[1].Children, Is.Empty);
        Assert.That(tree.Root.Children[0].Children[2].Children, Has.Count.EqualTo(2));
        Assert.That(tree.Root.Children[0].Children[2].Children[0].Children, Is.Empty);
        Assert.That(tree.Root.Children[0].Children[2].Children[1].Children, Has.Count.EqualTo(1));
        Assert.That(tree.Root.Children[0].Children[2].Children[1].Children[0].Children, Is.Empty);
        Assert.That(tree.Root.Children[1].Children, Has.Count.EqualTo(1));
        Assert.That(tree.Root.Children[1].Children[0].Children[0].Children, Has.Count.EqualTo(3));
        Assert.That(tree.Root.Children[1].Children[0].Children[0].Children[1].Children, Is.Empty);
        Assert.That(tree.Root.Children[1].Children[0].Children, Has.Count.EqualTo(2));
        Assert.That(tree.Root.Children[1].Children[0].Children[1].Children, Is.Empty);
        Assert.That(tree.Root.Children[1].Children[0].Children[0].Children[0].Children, Is.Empty);
        Assert.That(tree.Root.Children[1].Children[0].Children[0].Children[2].Children, Is.Empty);

        Assert.AreEqual(new Position(0, 2), tree.Root.Value);
        Assert.AreEqual(new Position(2, 4), tree.Root.Children[0].Value);
        Assert.AreEqual(new Position(0, 6), tree.Root.Children[0].Children[0].Value);
        Assert.AreEqual(new Position(4, 6), tree.Root.Children[0].Children[1].Value);
        Assert.AreEqual(new Position(4, 2), tree.Root.Children[0].Children[2].Value);
        Assert.AreEqual(new Position(6, 4), tree.Root.Children[0].Children[2].Children[0].Value);
        Assert.AreEqual(new Position(2, 0), tree.Root.Children[0].Children[2].Children[1].Value);
        Assert.AreEqual(new Position(0, 2), tree.Root.Children[0].Children[2].Children[1].Children[0].Value);
        Assert.AreEqual(new Position(2, 0), tree.Root.Children[1].Value);
        Assert.AreEqual(new Position(4, 2), tree.Root.Children[1].Children[0].Value);
        Assert.AreEqual(new Position(2, 4), tree.Root.Children[1].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(0, 6), tree.Root.Children[1].Children[0].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(4, 6), tree.Root.Children[1].Children[0].Children[0].Children[1].Value);
        Assert.AreEqual(new Position(0, 2), tree.Root.Children[1].Children[0].Children[0].Children[2].Value);
        Assert.AreEqual(new Position(6, 4), tree.Root.Children[1].Children[0].Children[1].Value);
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
            new Piece(Guid.NewGuid(), new Position(1, 1), Color.WHITE, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 1), Color.WHITE, false, false),
            new Piece(Guid.NewGuid(), new Position(5, 1), Color.WHITE, false, false),
            new Piece(Guid.NewGuid(), new Position(4, 2), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(6, 2), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 3), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(5, 3), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(7, 3), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldReturnCorrectTreeOfNonCaptureMoves()
    {
        var match = GetNonCaptureTestCase();

        var calculator = new MoveCalculator();

        var moves = calculator.Calculate(match);

        Assert.That(moves, Has.Count.EqualTo(3));

        var firstTree = moves.First().Value;
        Assert.AreEqual(3, firstTree.CountNodes());
        Assert.AreEqual(new Position(1, 1), firstTree.Root.Value);
        Assert.That(firstTree.Root.Children, Has.Count.EqualTo(2));

        var secondTree = moves.Skip(1).First().Value;
        Assert.AreEqual(2, secondTree.CountNodes());
        Assert.AreEqual(new Position(3, 1), secondTree.Root.Value);
        Assert.That(secondTree.Root.Children, Has.Count.EqualTo(1));

        var thirdTree = moves.Skip(2).First().Value;
        Assert.AreEqual(1, thirdTree.CountNodes());
        Assert.AreEqual(new Position(5, 1), thirdTree.Root.Value);
        Assert.That(thirdTree.Root.Children, Is.Empty);
    }

    // ==============================================================================================
    // ================================ DAMA PIECE CAPTURE TESTS ====================================
    // ==============================================================================================

    // -------------------------------------
    // The following scenario is returned
    // -------------------------------------
    // | 7 |   |   |   |   |   | Ẅ |   |   |
    // | 6 |   |   |   |   |   |   |   |   |
    // | 5 |   | B |   | B |   |   |   |   |
    // | 4 |   |   |   |   |   |   | B |   |
    // | 3 |   |   |   | B |   |   |   |   |
    // | 2 | Ẅ |   |   |   |   |   | B |   |
    // | 1 |   | B |   |   |   |   |   |   |
    // | 0 |   |   |   |   |   |   |   |   |
    // | - | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
    // -------------------------------------
    private Match GetDamaCaptureTestCase()
    {
        return BuildMatch(new List<Piece>()
        {
            new Piece(Guid.NewGuid(), new Position(0, 2), Color.WHITE, true, false),
            new Piece(Guid.NewGuid(), new Position(5, 7), Color.WHITE, true, false),
            new Piece(Guid.NewGuid(), new Position(1, 1), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(1, 5), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 3), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(3, 5), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(6, 2), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(6, 4), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldReturnCorrectTreeOfDamaCaptureMoves()
    {
        var match = GetDamaCaptureTestCase();

        var calculator = new MoveCalculator();

        var pieces = match.GetCurrentPlayerPieces();

        var moves = calculator.Calculate(match);

        Assert.That(moves, Has.Count.EqualTo(2));

        Assert.AreEqual(new Position(4, 6), moves[pieces.ElementAt(0).Id].Root.Children[0].Value);
        Assert.AreEqual(new Position(7, 3), moves[pieces.ElementAt(0).Id].Root.Children[0].Children[0].Value);
        Assert.AreEqual(new Position(5, 1), moves[pieces.ElementAt(0).Id].Root.Children[0].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(2, 4), moves[pieces.ElementAt(0).Id].Root.Children[0].Children[0].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(0, 6), moves[pieces.ElementAt(0).Id].Root.Children[0].Children[0].Children[0].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(4, 0), moves[pieces.ElementAt(0).Id].Root.Children[0].Children[0].Children[1].Value);
        Assert.AreEqual(new Position(2, 0), moves[pieces.ElementAt(0).Id].Root.Children[1].Value);
        Assert.AreEqual(new Position(7, 5), moves[pieces.ElementAt(0).Id].Root.Children[1].Children[0].Value);
        Assert.AreEqual(new Position(2, 4), moves[pieces.ElementAt(1).Id].Root.Children[0].Value);
        Assert.AreEqual(new Position(0, 6), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[0].Value);
        Assert.AreEqual(new Position(4, 2), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(7, 5), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[0].Children[0].Children[0].Value);
        Assert.AreEqual(new Position(4, 2), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[1].Value);
        Assert.AreEqual(new Position(0, 6), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[1].Children[0].Value);
        Assert.AreEqual(new Position(7, 5), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[1].Children[1].Value);
        Assert.AreEqual(new Position(5, 1), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Value);
        Assert.AreEqual(new Position(0, 6), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Children[0].Value);
        Assert.AreEqual(new Position(7, 3), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Children[1].Value);
        Assert.AreEqual(new Position(5, 5), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Children[1].Children[0].Value);
        Assert.AreEqual(new Position(4, 6), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Children[1].Children[1].Value);
        Assert.AreEqual(new Position(3, 7), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Children[1].Children[2].Value);
        Assert.AreEqual(new Position(0, 4), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[2].Children[1].Children[2].Children[0].Value);
        Assert.AreEqual(new Position(6, 0), moves[pieces.ElementAt(1).Id].Root.Children[0].Children[3].Value);
        Assert.AreEqual(new Position(1, 3), moves[pieces.ElementAt(1).Id].Root.Children[1].Value);
    }

    // ==============================================================================================
    // ============================== DAMA PIECE NON CAPTURE TESTS ==================================
    // ==============================================================================================

    // -------------------------------------
    // The following scenario is returned
    // -------------------------------------
    // | 7 |   |   |   |   |   |   |   |   |
    // | 6 |   |   |   |   |   |   |   |   |
    // | 5 |   | Ẅ |   |   |   |   |   |   |
    // | 4 |   |   |   |   | B |   | B |   |
    // | 3 |   |   |   |   |   | B |   |   |
    // | 2 | Ẅ |   |   |   | Ẅ |   |   |   |
    // | 1 |   | B |   |   |   |   |   |   |
    // | 0 |   |   | B |   |   |   |   |   |
    // | - | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
    // -------------------------------------
    private Match GetDamaNonCaptureTestCase()
    {
        return BuildMatch(new List<Piece>()
        {
            new Piece(Guid.NewGuid(), new Position(1, 5), Color.WHITE, true, false),
            new Piece(Guid.NewGuid(), new Position(0, 2), Color.WHITE, true, false),
            new Piece(Guid.NewGuid(), new Position(4, 2), Color.WHITE, true, false),
            new Piece(Guid.NewGuid(), new Position(1, 1), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(2, 0), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(4, 4), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(5, 3), Color.BLACK, false, false),
            new Piece(Guid.NewGuid(), new Position(6, 4), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldReturnCorrectTreeOfDamaNonCaptureMoves()
    {
        var match = GetDamaNonCaptureTestCase();

        var calculator = new MoveCalculator();

        var moves = calculator.Calculate(match);

        Assert.That(moves, Has.Count.EqualTo(3));

        var firstTree = moves.First().Value;
        Assert.AreEqual(7, firstTree.CountNodes());
        Assert.AreEqual(new Position(1, 5), firstTree.Root.Value);
        Assert.That(firstTree.Root.Children, Has.Count.EqualTo(6));

        Assert.AreEqual(new Position(0, 6), firstTree.Root.Children[0].Value);
        Assert.AreEqual(new Position(2, 6), firstTree.Root.Children[1].Value);
        Assert.AreEqual(new Position(3, 7), firstTree.Root.Children[2].Value);
        Assert.AreEqual(new Position(0, 4), firstTree.Root.Children[3].Value);
        Assert.AreEqual(new Position(2, 4), firstTree.Root.Children[4].Value);
        Assert.AreEqual(new Position(3, 3), firstTree.Root.Children[5].Value);

        Assert.That(firstTree.Root.Children[0].Children, Is.Empty);
        Assert.That(firstTree.Root.Children[1].Children, Is.Empty);
        Assert.That(firstTree.Root.Children[2].Children, Is.Empty);
        Assert.That(firstTree.Root.Children[3].Children, Is.Empty);
        Assert.That(firstTree.Root.Children[4].Children, Is.Empty);
        Assert.That(firstTree.Root.Children[5].Children, Is.Empty);

        var secondTree = moves.Skip(1).First().Value;
        Assert.AreEqual(new Position(0, 2), secondTree.Root.Value);
        Assert.That(secondTree.Root.Children, Has.Count.EqualTo(5));

        Assert.AreEqual(new Position(1, 3), secondTree.Root.Children[0].Value);
        Assert.AreEqual(new Position(2, 4), secondTree.Root.Children[1].Value);
        Assert.AreEqual(new Position(3, 5), secondTree.Root.Children[2].Value);
        Assert.AreEqual(new Position(4, 6), secondTree.Root.Children[3].Value);
        Assert.AreEqual(new Position(5, 7), secondTree.Root.Children[4].Value);

        Assert.That(secondTree.Root.Children[0].Children, Is.Empty);
        Assert.That(secondTree.Root.Children[1].Children, Is.Empty);
        Assert.That(secondTree.Root.Children[2].Children, Is.Empty);
        Assert.That(secondTree.Root.Children[3].Children, Is.Empty);
        Assert.That(secondTree.Root.Children[4].Children, Is.Empty);

        var thirdTree = moves.Skip(2).First().Value;
        Assert.AreEqual(new Position(4, 2), thirdTree.Root.Value);
        Assert.That(secondTree.Root.Children, Has.Count.EqualTo(5));

        Assert.AreEqual(new Position(3, 3), thirdTree.Root.Children[0].Value);
        Assert.AreEqual(new Position(2, 4), thirdTree.Root.Children[1].Value);
        Assert.AreEqual(new Position(3, 1), thirdTree.Root.Children[2].Value);
        Assert.AreEqual(new Position(5, 1), thirdTree.Root.Children[3].Value);
        Assert.AreEqual(new Position(6, 0), thirdTree.Root.Children[4].Value);

        Assert.That(thirdTree.Root.Children[0].Children, Is.Empty);
        Assert.That(thirdTree.Root.Children[1].Children, Is.Empty);
        Assert.That(thirdTree.Root.Children[2].Children, Is.Empty);
        Assert.That(thirdTree.Root.Children[3].Children, Is.Empty);
        Assert.That(thirdTree.Root.Children[4].Children, Is.Empty);
    }
}