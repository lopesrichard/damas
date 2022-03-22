import { v4 as uuid } from 'uuid';
import { makeAutoObservable } from 'mobx';
import { BlackOrWhite, IMatch, IMatchPlayer, IPossibleMoves, ISlot } from './types';
import { Position } from '../services/types';

class MatchStore {
  id: string | null = null;
  playerOne: IMatchPlayer | null = null;
  playerTwo: IMatchPlayer | null = null;
  turnColor: BlackOrWhite | null = null;
  slots: ISlot[] = createSlots();
  possibleMoves: IPossibleMoves = {};

  constructor() {
    makeAutoObservable(this);
  }

  newMatch(match: IMatch) {
    this.id = match.id;
    this.playerOne = match.playerOne;
    this.playerTwo = match.playerTwo;
    this.turnColor = match.turnColor;
    this.slots = match.slots;
  }

  getSlotByPosition(x: number, y: number) {
    return this.slots.find((slot) => slot.position[0] === x && slot.position[1] === y);
  }

  selectPiece(id: string) {
    this.slots.forEach((slot) => {
      if (slot.piece) {
        slot.piece.selected = slot.piece.id === id;
      }
    });
  }

  highlightSlots(positions: Position[]) {
    this.slots.forEach((slot) => {
      slot.hilighted = positions.some(
        (position) => slot.position[0] === position[0] && slot.position[1] === position[1],
      );
    });
  }

  moveSelectedPieceTo(slotId: string) {
    const previous = this.slots.find((slot) => slot.piece?.selected);
    const next = this.slots.find((slot) => slot.id === slotId);
    if (!next || !previous || !previous.piece) return;
    next.piece = previous.piece;
    next.piece.selected = false;
    previous.piece = null;
    this.removeHighlights();
  }

  getSelectedPiece() {
    const slot = this.slots.find((slot) => slot.piece?.selected);
    return slot ? slot.piece : null;
  }

  removeHighlights() {
    this.slots.forEach((slot) => {
      slot.hilighted = false;
    });
  }

  setPossibleMoves(possibleMoves: IPossibleMoves) {
    this.possibleMoves = possibleMoves;
  }

  capturePiece(id: string) {
    const slot = this.slots.find((slot) => slot.piece?.id === id);
    if (slot) {
      slot.piece = null;
    }
  }

  promote(id: string) {
    const slot = this.slots.find((slot) => slot.piece?.id === id);
    if (slot?.piece) {
      slot.piece.isDama = true;
    }
  }
}

const createSlots = (): ISlot[] => {
  return [
    { id: uuid(), position: [0, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 7], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [2, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 7], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [4, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 7], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [6, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 7], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [0, 6], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [1, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 6], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [3, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 6], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [5, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 6], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [7, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [0, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 5], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [2, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 5], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [4, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 5], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [6, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 5], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [0, 4], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [1, 4], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 4], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [3, 4], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 4], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [5, 4], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 4], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [7, 4], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [0, 3], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 3], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [2, 3], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 3], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [4, 3], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 3], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [6, 3], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 3], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [0, 2], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [1, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 2], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [3, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 2], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [5, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 2], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [7, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [0, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 1], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [2, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 1], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [4, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 1], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [6, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 1], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [0, 0], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [1, 0], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 0], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [3, 0], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 0], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [5, 0], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 0], playable: true, hilighted: false, piece: null },
    { id: uuid(), position: [7, 0], playable: false, hilighted: false, piece: null },
  ];
};

export default new MatchStore();
