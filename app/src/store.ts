import { writable } from 'svelte/store';

export type PositionProps = {
  x: number;
  y: number;
};

export class Position implements PositionProps {
  public x: number;
  public y: number;

  constructor(x: number, y: number) {
    this.x = x;
    this.y = y;
  }

  public equals(position: Position): boolean {
    return this.x === position.x && this.y === position.y;
  }

  public playable(): boolean {
    return this.x % 2 === this.y % 2;
  }

  public values(): [number, number] {
    return [this.x, this.y];
  }

  public static from({ x, y }: PositionProps): Position {
    return new Position(x, y);
  }
}

export type Color = 'black' | 'white';

export type Player = {
  id: string;
  name: string;
  color: Color;
};

export type Piece = {
  id: string;
  color: Color;
  position: Position;
  isDama: boolean;
  selected: boolean;
  hilighted: boolean;
};

export type Match = {
  id: string;
  playerOne: Player;
  playerTwo: Player;
  whoseTurn: Color;
  pieces: Piece[];
  possibleMoves: Map<string, Position[]>;
};

export type Store = {
  match?: Match;
};

const store = writable<Store>({});

export default store;
