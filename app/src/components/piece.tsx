import { styled } from '@stitches/react';
import { observer } from 'mobx-react-lite';
import { FunctionComponent, MouseEventHandler } from 'react';
import { SLOT_SIZE } from './constants';
import store from './store';
import { PieceProps } from './types';

const Circle = styled('div', {
  width: `calc(${SLOT_SIZE}px - 30%)`,
  height: `calc(${SLOT_SIZE}px - 30%)`,
  borderRadius: '100%',
  margin: 'auto',
  variants: {
    color: {
      black: {
        backgroundColor: 'black',
      },
      white: {
        backgroundColor: 'white',
      },
    },
    selected: {
      true: {
        boxShadow: '0 0 5px 5px red',
      },
    },
  },
});

const calculateMovements = (pieceId: string): string[] => {
  const slot = store.slots.find((slot) => slot.piece?.id === pieceId);

  if (!slot?.piece) return [];

  const slotLeftUp = store.getSlotByPosition(slot.position[0] - 1, slot.position[1] + 1);
  const slotRightUp = store.getSlotByPosition(slot.position[0] + 1, slot.position[1] + 1);
  const slotLeftBottom = store.getSlotByPosition(slot.position[0] - 1, slot.position[1] - 1);
  const slotRightBottom = store.getSlotByPosition(slot.position[0] + 1, slot.position[1] - 1);

  const movements = [];

  if (slot.piece.color === 'white' && slotLeftUp && slotLeftUp.piece == null) {
    movements.push(slotLeftUp.id);
  }

  if (slot.piece.color === 'white' && slotRightUp && slotRightUp.piece == null) {
    movements.push(slotRightUp.id);
  }

  if (slot.piece.color === 'black' && slotLeftBottom && slotLeftBottom.piece == null) {
    movements.push(slotLeftBottom.id);
  }

  if (slot.piece.color === 'black' && slotRightBottom && slotRightBottom.piece == null) {
    movements.push(slotRightBottom.id);
  }

  return movements;
};

export const Piece: FunctionComponent<PieceProps> = observer((props) => {
  const handleClick: MouseEventHandler = (evt) => {
    evt.stopPropagation();
    store.selectPiece(props.id);
    store.highlightSlots(calculateMovements(props.id));
  };
  return <Circle {...props} onClick={handleClick} />;
});
