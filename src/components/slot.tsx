import { styled } from '@stitches/react';
import { observer } from 'mobx-react';
import { FunctionComponent, MouseEventHandler } from 'react';
import { SLOT_SIZE } from './constants';
import { Piece } from './piece';
import store from './store';
import { SlotProps } from './types';

const Square = styled('div', {
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  width: SLOT_SIZE,
  height: SLOT_SIZE,
  userSelect: 'none',
  variants: {
    playable: {
      true: {
        backgroundColor: 'gray',
      },
    },
    hilighted: {
      true: {
        backgroundColor: 'green',
      },
    },
  },
});

export const Slot: FunctionComponent<SlotProps> = observer(({ id, playable, hilighted, position, piece }) => {
  const handleClick: MouseEventHandler = (evt) => {
    evt.stopPropagation();
    if (hilighted) store.moveSelectedPieceTo(id);
  };
  return (
    <Square playable={playable} hilighted={hilighted} onClick={handleClick}>
      {piece && <Piece {...piece} />}
    </Square>
  );
});
