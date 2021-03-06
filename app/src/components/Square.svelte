<script lang="ts">
  import api, { BasicMoveModel, ListResult, NewMoveModel, Result } from 'api';
  import colors from 'colors';
  import PieceComponent from 'components/Piece.svelte';
  import { chain } from 'lodash';
  import { variables } from 'helpers';
  import store, { Match, Piece, Position } from 'store';

  export let position: Position;

  let match: Match;
  let piece: Piece;
  let selected: Piece;
  let moves: Position[] = [];

  if (position.playable()) {
    store.subscribe((store) => {
      if (!store.match) return;
      match = store.match;
      piece = store.match.pieces.find((piece) => piece.position.equals(position));
    });

    store.subscribe((store) => {
      moves = [];

      if (!store.match) return;

      selected = store.match.pieces.find((piece) => piece.selected);

      if (!selected) return;

      moves = store.match.possibleMoves.has(selected.id) ? match.possibleMoves.get(selected.id) : [];
    });
  }

  const continueToGetPossibleMoves = async (previous: Result<BasicMoveModel>) => {
    if (!previous.isSuccess) {
      throw previous.messages;
    }

    const move = previous.data;

    if (move.capturedPieceId) {
      store.update((store) => {
        const index = store.match.pieces.findIndex((piece) => piece.id === move.capturedPieceId);
        store.match.pieces.splice(index, 1);
        return store;
      });
    }

    if (move.isPromotionMove) {
      store.update((store) => {
        const piece = store.match.pieces.find((piece) => piece.id === move.pieceId);
        piece.isDama = true;
        return store;
      });
    }

    return await api.listPossibleMoves(match.id);
  };

  const continueToUpdateStore = (previous: ListResult<NewMoveModel>) => {
    if (!previous.isSuccess) {
      throw previous.messages;
    }

    const moves = previous.data;

    store.update((store) => {
      store.match.pieces = store.match.pieces.map((piece) => ({
        ...piece,
        highlighted: moves.some((move) => move.pieceId == piece.id),
      }));

      store.match.possibleMoves = new Map<string, Position[]>(
        chain(moves)
          .groupBy((move) => move.pieceId)
          .mapValues((moves) => moves.map(({ newPosition }) => new Position(newPosition.x, newPosition.y)))
          .entries()
          .value(),
      );

      const piece = store.match.pieces.find((piece) => piece.id === selected.id);

      piece.position = position;
      piece.selected = false;

      return store;
    });
  };

  const move = async () => {
    api
      .move({
        pieceId: selected.id,
        newPosition: moves.find((move) => move.equals(position)),
      })
      .then(continueToGetPossibleMoves)
      .then(continueToUpdateStore)
      .catch(console.error);
  };

  const color = position.playable() ? colors.gray.light : colors.white;
</script>

{#if piece}
  <div use:variables={{ color }}>
    <PieceComponent {piece} />
  </div>
{:else}
  <div
    use:variables={{ color, highlight: colors.pink.light }}
    class:highlighted={moves.some((move) => move.equals(position))}
    on:click={move}
  />
{/if}

<style>
  div {
    background-color: var(--color);
    display: flex;
    align-items: center;
    justify-content: center;
    transition: background 300ms;
  }

  div.highlighted {
    background-color: var(--highlight);
    cursor: pointer;
  }
</style>
