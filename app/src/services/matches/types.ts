import { BasicPieceModel } from '../pieces/types';
import { BoardSize, Color } from '../types';

export type NewMatchModel = {
  playerOneId: string;
  playerOneColor: Color;
  playerTwoId: string;
  playerTwoColor: Color;
  boardSize: BoardSize;
};

export interface BasicMatchModel {
  id: string;
  playerOneId: string;
  playerOneColor: Color;
  playerTwoId: string;
  playerTwoColor: Color;
  turnColor: Color;
  boardSize: BoardSize;
  winnerId?: string;
  isDraw: boolean;
  startedAt: Date;
  finishedAt?: Date;
  moves: number;
  pieces: BasicPieceModel[];
}
