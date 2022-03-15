import { styled } from '@stitches/react';
import { observer } from 'mobx-react';
import { FunctionComponent } from 'react';
import { SLOT_SIZE } from './constants';
import { Slot } from './slot';
import store from './store';

const Board = styled('div', {
  display: 'grid',
  width: `calc(8 * ${SLOT_SIZE}px)`,
  gridTemplateRows: `repeat(8, ${SLOT_SIZE}px)`,
  gridTemplateColumns: `repeat(8, ${SLOT_SIZE}px)`,
  border: '1px solid gray',
});

export const Damas: FunctionComponent = observer(() => {
  return (
    <Board>
      {store.slots.map((slot) => {
        return <Slot key={slot.id} {...slot} />;
      })}
    </Board>
  );
});
