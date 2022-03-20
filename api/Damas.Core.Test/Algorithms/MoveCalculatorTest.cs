using System.Collections.Generic;
using System.Linq;
using Damas.Core.Algorithms;
using Damas.Core.Enums;
using Damas.Core.Models;
using Damas.Core.Structs;
using NUnit.Framework;

namespace Damas.Test.Algorithms;

public class MoveCalculatorTest
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
    public void ShouldReturnOnePieceCaptureMoves()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);
        Assert.That(moves.ToList(), Has.Count.EqualTo(1));
    }

    [Test]
    public void ShouldCountTreeNodes()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);
        var tree = moves.First();
        Assert.AreEqual(15, tree.Nodes.Count());
    }

    [Test]
    public void ShouldCountTreeLeaves()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);
        var tree = moves.First();
        Assert.AreEqual(8, tree.Leaves.Count());
    }

    [Test]
    public void ShouldReturnCorrectNodes()
    {
        var calculator = new MoveCalculator();
        var match = GetCaptureTestCase();
        var moves = calculator.Calculate(match);

        var tree = moves.First();

        Assert.That(tree.Root.Children, Has.Count.EqualTo(2));
        Assert.That(tree.Root.Children.ElementAt(0).Children, Has.Count.EqualTo(3));
        Assert.That(tree.Root.Children.ElementAt(0).Children.ElementAt(0).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(0).Children.ElementAt(1).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children, Has.Count.EqualTo(2));
        Assert.That(tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(0).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children, Has.Count.EqualTo(1));
        Assert.That(tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(0).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(1).Children, Has.Count.EqualTo(1));
        Assert.That(tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children, Has.Count.EqualTo(3));
        Assert.That(tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children, Has.Count.EqualTo(2));
        Assert.That(tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(1).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children, Is.Empty);
        Assert.That(tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(2).Children, Is.Empty);

        Assert.AreEqual(new Position(0, 2), tree.Root.Value);
        Assert.AreEqual(new Position(2, 4), tree.Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), tree.Root.Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 6), tree.Root.Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(4, 2), tree.Root.Children.ElementAt(0).Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(6, 4), tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 0), tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(0, 2), tree.Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 0), tree.Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(4, 2), tree.Root.Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 6), tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(0, 2), tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(6, 4), tree.Root.Children.ElementAt(1).Children.ElementAt(0).Children.ElementAt(1).Value);
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
    public void ShouldReturnCorrectTreeOfNonCaptureMoves()
    {
        var match = GetNonCaptureTestCase();

        var calculator = new MoveCalculator();

        var moves = calculator.Calculate(match);

        Assert.That(moves.ToList(), Has.Count.EqualTo(3));

        var firstTree = moves.First();
        Assert.AreEqual(3, firstTree.Nodes.Count());
        Assert.AreEqual(new Position(1, 1), firstTree.Root.Value);
        Assert.That(firstTree.Root.Children, Has.Count.EqualTo(2));

        var secondTree = moves.Skip(1).First();
        Assert.AreEqual(2, secondTree.Nodes.Count());
        Assert.AreEqual(new Position(3, 1), secondTree.Root.Value);
        Assert.That(secondTree.Root.Children, Has.Count.EqualTo(1));

        var thirdTree = moves.Skip(2).First();
        Assert.AreEqual(1, thirdTree.Nodes.Count());
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
            new Piece(new Position(0, 2), Color.WHITE, true, false),
            new Piece(new Position(5, 7), Color.WHITE, true, false),
            new Piece(new Position(1, 1), Color.BLACK, false, false),
            new Piece(new Position(1, 5), Color.BLACK, false, false),
            new Piece(new Position(3, 3), Color.BLACK, false, false),
            new Piece(new Position(3, 5), Color.BLACK, false, false),
            new Piece(new Position(6, 2), Color.BLACK, false, false),
            new Piece(new Position(6, 4), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldReturnCorrectTreeOfDamaCaptureMoves()
    {
        var match = GetDamaCaptureTestCase();

        var calculator = new MoveCalculator();

        var pieces = match.GetPlayerPieces();

        var moves = calculator.Calculate(match);

        Assert.That(moves.ToList(), Has.Count.EqualTo(2));

        Assert.AreEqual(new Position(4, 6), moves.ElementAt(0).Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(7, 3), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(5, 1), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 0), moves.ElementAt(0).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(2, 0), moves.ElementAt(0).Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(7, 5), moves.ElementAt(0).Root.Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), moves.ElementAt(1).Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(0, 6), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 2), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(7, 5), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 2), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(0, 6), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(7, 5), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(1).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(5, 1), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(0, 6), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(7, 3), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(5, 5), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(4, 6), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(3, 7), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(0, 4), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(2).Children.ElementAt(1).Children.ElementAt(2).Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(6, 0), moves.ElementAt(1).Root.Children.ElementAt(0).Children.ElementAt(3).Value);
        Assert.AreEqual(new Position(1, 3), moves.ElementAt(1).Root.Children.ElementAt(1).Value);
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
            new Piece(new Position(1, 5), Color.WHITE, true, false),
            new Piece(new Position(0, 2), Color.WHITE, true, false),
            new Piece(new Position(4, 2), Color.WHITE, true, false),
            new Piece(new Position(1, 1), Color.BLACK, false, false),
            new Piece(new Position(2, 0), Color.BLACK, false, false),
            new Piece(new Position(4, 4), Color.BLACK, false, false),
            new Piece(new Position(5, 3), Color.BLACK, false, false),
            new Piece(new Position(6, 4), Color.BLACK, false, false),
        });
    }

    [Test]
    public void ShouldReturnCorrectTreeOfDamaNonCaptureMoves()
    {
        var match = GetDamaNonCaptureTestCase();

        var calculator = new MoveCalculator();

        var moves = calculator.Calculate(match);

        Assert.That(moves.ToList(), Has.Count.EqualTo(3));

        var firstTree = moves.First();
        Assert.AreEqual(7, firstTree.Nodes.Count());
        Assert.AreEqual(new Position(1, 5), firstTree.Root.Value);
        Assert.That(firstTree.Root.Children, Has.Count.EqualTo(6));

        Assert.AreEqual(new Position(0, 6), firstTree.Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 6), firstTree.Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(3, 7), firstTree.Root.Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(0, 4), firstTree.Root.Children.ElementAt(3).Value);
        Assert.AreEqual(new Position(2, 4), firstTree.Root.Children.ElementAt(4).Value);
        Assert.AreEqual(new Position(3, 3), firstTree.Root.Children.ElementAt(5).Value);

        Assert.That(firstTree.Root.Children.ElementAt(0).Children, Is.Empty);
        Assert.That(firstTree.Root.Children.ElementAt(1).Children, Is.Empty);
        Assert.That(firstTree.Root.Children.ElementAt(2).Children, Is.Empty);
        Assert.That(firstTree.Root.Children.ElementAt(3).Children, Is.Empty);
        Assert.That(firstTree.Root.Children.ElementAt(4).Children, Is.Empty);
        Assert.That(firstTree.Root.Children.ElementAt(5).Children, Is.Empty);

        var secondTree = moves.Skip(1).First();
        Assert.AreEqual(new Position(0, 2), secondTree.Root.Value);
        Assert.That(secondTree.Root.Children, Has.Count.EqualTo(5));

        Assert.AreEqual(new Position(1, 3), secondTree.Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), secondTree.Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(3, 5), secondTree.Root.Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(4, 6), secondTree.Root.Children.ElementAt(3).Value);
        Assert.AreEqual(new Position(5, 7), secondTree.Root.Children.ElementAt(4).Value);

        Assert.That(secondTree.Root.Children.ElementAt(0).Children, Is.Empty);
        Assert.That(secondTree.Root.Children.ElementAt(1).Children, Is.Empty);
        Assert.That(secondTree.Root.Children.ElementAt(2).Children, Is.Empty);
        Assert.That(secondTree.Root.Children.ElementAt(3).Children, Is.Empty);
        Assert.That(secondTree.Root.Children.ElementAt(4).Children, Is.Empty);

        var thirdTree = moves.Skip(2).First();
        Assert.AreEqual(new Position(4, 2), thirdTree.Root.Value);
        Assert.That(secondTree.Root.Children, Has.Count.EqualTo(5));

        Assert.AreEqual(new Position(3, 3), thirdTree.Root.Children.ElementAt(0).Value);
        Assert.AreEqual(new Position(2, 4), thirdTree.Root.Children.ElementAt(1).Value);
        Assert.AreEqual(new Position(3, 1), thirdTree.Root.Children.ElementAt(2).Value);
        Assert.AreEqual(new Position(5, 1), thirdTree.Root.Children.ElementAt(3).Value);
        Assert.AreEqual(new Position(6, 0), thirdTree.Root.Children.ElementAt(4).Value);

        Assert.That(thirdTree.Root.Children.ElementAt(0).Children, Is.Empty);
        Assert.That(thirdTree.Root.Children.ElementAt(1).Children, Is.Empty);
        Assert.That(thirdTree.Root.Children.ElementAt(2).Children, Is.Empty);
        Assert.That(thirdTree.Root.Children.ElementAt(3).Children, Is.Empty);
        Assert.That(thirdTree.Root.Children.ElementAt(4).Children, Is.Empty);
    }
}