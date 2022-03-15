import { styled } from '@stitches/react';
import { useState } from 'react';

const BOARD_SIZE = 8;

const Board = styled('table', {
  border: '1px solid gray',
  borderSpacing: 0,
});

const Row = styled('tr', {});

const Slot = styled('td', {
  width: 50,
  height: 50,
  userSelect: 'none',
  variants: {
    playable: {
      true: {
        backgroundColor: 'gray'
      },
    },
    hilighted: {
      true: {
        backgroundColor: 'green'
      }
    }
  }
});

type BlackOrWhite = 'black' | 'white';

type Piece = {
  position: [number, number];
  color: BlackOrWhite;
  isDama: boolean;
  selected: boolean;
  hilighted: boolean;
}

const Piece = styled('div', {
  width: 40,
  height: 40,
  borderRadius: '100%',
  margin: 'auto',
  variants: {
    color: {
      black: {
        backgroundColor: 'black'
      },
      white: {
        backgroundColor: 'white'
      }
    },
    selected: {
      true: {
        boxShadow: '0 0 5px 5px red'
      }
    },
  }
});

const createPieces = (): Piece[] => {
  return [
    { position: [1, 0], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [3, 0], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [5, 0], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [7, 0], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [0, 1], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [2, 1], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [4, 1], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [6, 1], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [1, 2], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [3, 2], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [5, 2], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [7, 2], color: 'black', isDama: false, selected: false, hilighted: false },
    { position: [0, 5], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [2, 5], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [4, 5], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [6, 5], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [1, 6], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [3, 6], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [5, 6], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [7, 6], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [0, 7], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [2, 7], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [4, 7], color: 'white', isDama: false, selected: false, hilighted: false },
    { position: [6, 7], color: 'white', isDama: false, selected: false, hilighted: false },
  ];
}

export const Damas = () => {
  const eight = Array(BOARD_SIZE).fill(0);
  const [pieces, setPieces] = useState(createPieces());
  const [turn, setTurn] = useState<BlackOrWhite>('white');

  const getPiece = (x: number, y: number) => {
    return pieces.find(piece => {
      return piece.position[0] == x && piece.position[1] == y
    });
  }

  return (
    <Board onClick={() => {
      setPieces(pieces => pieces.map(piece => {
        piece.selected = false;
        return piece;
      }));
    }}>
      {eight.map((_, y) => {
        return <Row>
          {eight.map((_, x) => {
            const piece = getPiece(x, y);

            const nearestPiece1 = turn === 'white' ? getPiece(x + 1, y + 1) : getPiece(x + 1, y - 1);
            const nearestPiece2 = turn === 'white' ? getPiece(x - 1, y + 1) : getPiece(x - 1, y - 1);

            const isNextSelectedPiece =
              nearestPiece1?.selected ||
              nearestPiece2?.selected;

            const playable = (x + y) % 2 == 1;
            const available = typeof piece === 'undefined';

            if (available) {
              const hilighted = playable && isNextSelectedPiece;
              return <Slot playable={playable} hilighted={hilighted} onClick={() => {
                if (hilighted) {
                  setPieces(pieces => pieces.map(current => {
                    if (current.selected) {
                      current.position = [x, y];
                      setTurn(turn => turn === 'white' ? 'black' : 'white')
                    }
                    return current;
                  }))
                }
              }} />
            }

            return <Slot playable={playable}>
              <Piece {...piece} onClick={(evt) => {
                evt.stopPropagation();
                if (turn !== piece.color) return;
                setPieces(pieces => pieces.map(current => {
                  current.selected = current === piece;
                  return current;
                }));
              }} />
            </Slot>
          })}
        </Row>
      })}
    </Board>
  );
}
