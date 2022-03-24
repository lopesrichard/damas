<script lang="ts">
  import Board from 'components/Board.svelte';
  import Button from 'components/Button.svelte';
  import api, { BasicMatchModel, BasicPlayerModel, NewMoveModel, ListResult, Result } from 'api';
  import store, { Color, Position } from 'store';
  import { chain } from 'lodash';
  import { css } from '@emotion/css';

  const continueToNewMatch = async (previous: ListResult<BasicPlayerModel>) => {
    if (!previous.isSuccess) {
      throw previous.messages;
    }

    const players = previous.data;

    const response = await api.newMatch({
      playerOneId: players[0].id,
      playerOneColor: 'WHITE',
      playerTwoId: players[1].id,
      playerTwoColor: 'BLACK',
      boardSize: 8,
    });

    return [players, response];
  };

  const continueToGetPossibleMoves = async ([players, previous]: [BasicPlayerModel[], Result<BasicMatchModel>]) => {
    if (!previous.isSuccess) {
      throw previous.messages;
    }

    const match = previous.data;

    const response = await api.listPossibleMoves(match.id);

    return [players, match, response];
  };

  const continueToUpdateStore = ([players, match, previous]: [
    BasicPlayerModel[],
    BasicMatchModel,
    ListResult<NewMoveModel>,
  ]) => {
    if (!previous.isSuccess) {
      throw previous.messages;
    }

    const moves = previous.data;

    store.set({
      match: {
        id: match.id,
        playerOne: {
          id: players[0].id,
          name: players[0].name,
          color: match.playerOneColor.toLowerCase() as Color,
        },
        playerTwo: {
          id: players[1].id,
          name: players[1].name,
          color: match.playerTwoColor.toLowerCase() as Color,
        },
        pieces: match.pieces.map((piece) => ({
          id: piece.id,
          color: piece.color.toLowerCase() as Color,
          position: Position.from(piece.position),
          isDama: piece.isDama,
          selected: false,
          hilighted: moves.some((move) => move.pieceId == piece.id),
        })),
        whoseTurn: match.whoseTurn.toLocaleLowerCase() as Color,
        possibleMoves: new Map<string, Position[]>(
          chain(moves)
            .groupBy((move) => move.pieceId)
            .mapValues((moves) => moves.map((move) => Position.from(move.newPosition)))
            .entries()
            .value(),
        ),
      },
    });
  };

  const start = async () => {
    api
      .listPlayers()
      .then(continueToNewMatch)
      .then(continueToGetPossibleMoves)
      .then(continueToUpdateStore)
      .catch(console.error);
  };

  const styles = {
    container: css`
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      height: 100%;
    `,
    title: css`
      font-family: Hurricane, cursive;
      font-size: 3rem;
      margin: 0;
    `,
  };
</script>

<div class={styles.container}>
  <h1 class={styles.title}>Damas Online</h1>
  <Board size={8} />
  <Button text="Novo jogo" on:click={start} />
</div>
