import { styled } from '@stitches/react';
import { observer } from 'mobx-react';
import { FunctionComponent, useEffect, useState } from 'react';
import Players from '../services/players/service';
import Matches from '../services/matches/service';
import { BasicPlayerModel } from '../services/players/types';
import { SLOT_SIZE } from './constants';
import { Slot } from './slot';
import store from './store';
import { BlackOrWhite, IPossibleMoves } from './types';

const NewGameButton = styled('button', {
  padding: 10,
  marginTop: 10,
  backgroundColor: 'black',
  color: 'white',
});

const Board = styled('div', {
  display: 'grid',
  width: `calc(8 * ${SLOT_SIZE}px)`,
  gridTemplateRows: `repeat(8, ${SLOT_SIZE}px)`,
  gridTemplateColumns: `repeat(8, ${SLOT_SIZE}px)`,
  border: '1px solid gray',
});

export const Damas: FunctionComponent = observer(() => {
  const [players, setPlayers] = useState<BasicPlayerModel[]>([]);

  useEffect(() => {
    (async () => {
      const result = await Players.listPlayers();
      if (result.isSuccess) {
        setPlayers(result.data);
      }
    })();
  }, []);

  const createNewMatch = async () => {
    const match = (
      await Matches.newMatch({
        playerOneId: players[0].id,
        playerOneColor: 'WHITE',
        playerTwoId: players[1].id,
        playerTwoColor: 'BLACK',
        boardSize: 64,
      })
    ).data;

    const playerOne = (await Players.getPlayer(match.playerOneId)).data;
    const playerTwo = (await Players.getPlayer(match.playerTwoId)).data;

    store.newMatch({
      id: match.id,
      playerOne: {
        id: playerOne.id,
        name: playerOne.name,
        color: match.playerOneColor.toLowerCase() as BlackOrWhite,
      },
      playerTwo: {
        id: playerTwo.id,
        name: playerTwo.name,
        color: match.playerTwoColor.toLowerCase() as BlackOrWhite,
      },
      turnColor: match.turnColor.toLowerCase() as BlackOrWhite,
      slots: store.slots.map((slot) => {
        const piece = match.pieces.find(
          (piece) => piece.position[0] === slot.position[0] && piece.position[1] === slot.position[1],
        );

        if (piece) {
          slot.piece = {
            id: piece.id,
            color: piece.color.toLowerCase() as BlackOrWhite,
            isDama: piece.isDama,
            selected: false,
          };
        }

        return slot;
      }),
    });

    const moves = (await Matches.listPossibleMoves(match.id)).data;

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
    <>
      <Board>
        {store.slots.map((slot) => {
          return <Slot key={slot.id} {...slot} />;
        })}
      </Board>
      <NewGameButton onClick={createNewMatch}>New match</NewGameButton>
    </>
  );
});
