<script lang="ts">
  import { css, cx } from '@emotion/css';
  import colors from 'colors';
  import type { Piece } from 'store';
  import crown from 'assets/icons/crown-solid.svg';
  import store from 'store';
  import Icon from './Icon.svelte';

  export let piece: Piece;

  const select = () => {
    if (!piece.hilighted) return;
    store.update((store) => {
      store.match.pieces.forEach((current) => {
        current.selected = current.id === piece.id;
      });
      return store;
    });
  };

  const styles = {
    piece: css`
      display: flex;
      align-items: center;
      justify-content: center;
      width: 35px;
      height: 35px;
      border-radius: 100%;
      margin: auto;
      transition: box-shadow 300ms;
      background-color: ${colors[piece.color]};
    `,
    hilighted: css`
      box-shadow: 0 0 3px 3px ${colors.orange};
      cursor: pointer;
    `,
    selected: css`
      box-shadow: 0 0 3px 3px ${colors.pink.dark} !important;
      cursor: pointer;
    `,
  };
</script>

<div
  class={cx(styles.piece, { [styles.selected]: piece.selected, [styles.hilighted]: piece.hilighted })}
  on:click={select}
>
  {#if piece.isDama}
    <Icon name="crown" color={piece.color === 'black' ? 'white' : 'black'} />
  {/if}
</div>
