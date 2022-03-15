import { v4 as uuid } from 'uuid';
import { makeAutoObservable } from 'mobx';
import { BlackOrWhite, PieceProps, SlotProps } from './types';

class BoardStore {
  slots: SlotProps[] = createSlots();

  constructor() {
    makeAutoObservable(this);
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

  highlightSlots(ids: string[]) {
    this.slots.forEach((slot) => {
      slot.hilighted = ids.includes(slot.id);
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

  removeHighlights() {
    this.slots.forEach((slot) => {
      slot.hilighted = false;
    });
  }
}

const createPiece = (color: BlackOrWhite): PieceProps => {
  return { id: uuid(), color: color, isDama: false, selected: false, hilighted: false };
};

const createSlots = (): SlotProps[] => {
  return [
    { id: uuid(), position: [0, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 7], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [2, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 7], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [4, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 7], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [6, 7], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 7], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [0, 6], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [1, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 6], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [3, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 6], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [5, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 6], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [7, 6], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [0, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 5], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [2, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 5], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [4, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 5], playable: true, hilighted: false, piece: createPiece('black') },
    { id: uuid(), position: [6, 5], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 5], playable: true, hilighted: false, piece: createPiece('black') },
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
    { id: uuid(), position: [0, 2], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [1, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 2], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [3, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 2], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [5, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 2], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [7, 2], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [0, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [1, 1], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [2, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [3, 1], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [4, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [5, 1], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [6, 1], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [7, 1], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [0, 0], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [1, 0], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [2, 0], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [3, 0], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [4, 0], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [5, 0], playable: false, hilighted: false, piece: null },
    { id: uuid(), position: [6, 0], playable: true, hilighted: false, piece: createPiece('white') },
    { id: uuid(), position: [7, 0], playable: false, hilighted: false, piece: null },
  ];
};

export default new BoardStore();
