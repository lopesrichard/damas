import { styled } from '@stitches/react';
import { observer } from 'mobx-react';
import { FunctionComponent, MouseEventHandler } from 'react';
import Matches from '../services/matches/service';
import Moves from '../services/moves/service';
import { SLOT_SIZE } from './constants';
import { Piece } from './piece';
import store from './store';
import { IPossibleMoves, ISlot } from './types';

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

export const Slot: FunctionComponent<ISlot> = observer(({ id, playable, hilighted, position, piece }) => {
  const handleClick: MouseEventHandler = async (evt) => {
    evt.stopPropagation();
    const selected = store.getSelectedPiece();

    if (!selected) {
      return alert('No piece selected');
    }

    const response = await Moves.newMove({
      pieceId: selected.id,
      newPosition: position,
    });

    if (!response.isSuccess) {
      return alert(response.messages[0].content);
    }

    const move = response.data;

    store.moveSelectedPieceTo(id);

    if (move.capturedPieceId) {
      store.capturePiece(move.capturedPieceId);
    }

    if (move.isPromotionMove) {
      store.promote(move.pieceId);
    }

    if (!store.id) {
      return alert('Match id not found');
    }

    const moves = (await Matches.listPossibleMoves(store.id)).data;

    const possibleMoves: IPossibleMoves = {};

    moves.forEach((move) => {
      if (possibleMoves[move.pieceId]) {
        possibleMoves[move.pieceId].push(move.newPosition);
      } else {
        possibleMoves[move.pieceId] = [move.newPosition];
      }
    });

    store.setPossibleMoves(possibleMoves);
  };
  return (
    <Square playable={playable} hilighted={hilighted} onClick={hilighted ? handleClick : undefined}>
      {piece && <Piece {...piece} />}
    </Square>
  );
});
