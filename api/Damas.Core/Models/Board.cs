using Damas.Core.Collections;
using Damas.Core.Enums;
using Damas.Core.Structs;
using Damas.Core.Validators;

namespace Damas.Core.Models
{
    public class Board
    {
        public BoardSize Size { get; set; }
        public ICollection<Piece> Pieces { get; set; }

        public Board(BoardSize size, ICollection<Piece> pieces)
        {
            Size = size;
            Pieces = pieces;
        }

        public bool IsValidPosition(Position position)
        {
            var validator = new PositionValidator();

            var exceptions = validator.Validate(Size, position);

            return exceptions.None();
        }

        public bool IsPositionAvaialable(Position position)
        {
            return Pieces.None(p => p.Position == position && !p.IsCaptured);
        }

        public bool IsPositionOccupied(Position position)
        {
            return Pieces.Any(p => p.Position == position && !p.IsCaptured);
        }

        public bool IsPositionOccupied(Position position, Color color)
        {
            return Pieces.Any(p => p.Position == position && !p.IsCaptured && p.Color == color);
        }

        public Piece? GetPieceAt(Position position)
        {
            return Pieces.SingleOrDefault(p => p.Position == position && !p.IsCaptured);
        }

        public void AddPiece(Piece piece)
        {
            Pieces.Add(piece);
        }

        public void RemovePiece(Piece piece)
        {
            Pieces.Remove(piece);
        }
    }
}