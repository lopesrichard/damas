using Damas.Collections;
using Damas.Enums;
using Damas.Structs;
using Damas.Validators;

namespace Damas.Entities
{
    public class Board : Entity
    {
        public BoardSize Size { get; set; }
        public ICollection<Piece> Pieces { get; set; }

        public Board(Guid id, BoardSize size, ICollection<Piece> pieces) : base(id)
        {
            Size = size;
            Pieces = pieces;
        }

        public bool IsValidPosition(Position position)
        {
            var validator = new PositionValidator();

            var exceptions = validator.Validate(this, position);

            return exceptions.None();
        }

        public bool IsPositionAvaialable(Position position)
        {
            return Pieces.None(p => p.Position == position);
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
            return Pieces.SingleOrDefault(piece => piece.Position == position);
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