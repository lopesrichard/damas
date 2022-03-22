import { Position } from '../types';

export type NewMoveModel = {
  pieceId: string;
  newPosition: Position;
};

export type BasicMoveModel = {
  id: string;
  pieceId: string;
  previousPosition: Position;
  newPosition: Position;
  capturedPieceId?: string;
  isPromotionMove: boolean;
  dateTime: Date;
};
