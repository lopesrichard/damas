import { Color, Position } from '../types';

export type BasicPieceModel = {
  id: string;
  position: Position;
  color: Color;
  isDama: boolean;
  isCaptured: boolean;
};
