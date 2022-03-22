import { styled } from '@stitches/react';
import { observer } from 'mobx-react-lite';
import { FunctionComponent, MouseEventHandler } from 'react';
import { SLOT_SIZE } from './constants';
import store from './store';
import { IPiece } from './types';
import { FaCrown } from 'react-icons/fa';

const Circle = styled('div', {
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
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
    hilighted: {
      true: {
        boxShadow: '0 0 5px 5px yellow',
      },
    },
  },
});

export const Piece: FunctionComponent<IPiece> = observer((props) => {
  const { id, color, selected, isDama } = props;

  const positions = store.possibleMoves[id];

  const handleClick: MouseEventHandler = (evt) => {
    evt.stopPropagation();
    store.selectPiece(props.id);
    store.highlightSlots(positions);
  };
  return (
    <Circle color={color} selected={selected} hilighted={!!positions} onClick={positions ? handleClick : undefined}>
      {isDama && <FaCrown color={color == 'black' ? 'white' : 'black'} />}
    </Circle>
  );
});
