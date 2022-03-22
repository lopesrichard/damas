export type IMatch = {
  id: string;
  playerOne: IMatchPlayer;
  playerTwo: IMatchPlayer;
  turnColor: BlackOrWhite;
  slots: ISlot[];
};

export type IMatchPlayer = {
  id: string;
  name: string;
  color: BlackOrWhite;
};

export type ISlot = {
  id: string;
  position: [number, number];
  playable: boolean;
  hilighted: boolean;
  piece: IPiece | null;
  onClick?: () => void;
};

export type IPiece = {
  id: string;
  color: BlackOrWhite;
  isDama: boolean;
  selected: boolean;
};

export type BlackOrWhite = 'black' | 'white';

export type IPossibleMoves = {
  [pieceId: string]: Position[];
};

export type Position = [number, number];
