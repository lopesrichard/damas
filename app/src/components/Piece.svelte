<script lang="ts">
  import colors from 'colors';
  import type { Piece } from 'store';
  import store from 'store';
  import { variables } from 'helpers';
  import FaCrown from 'svelte-icons/fa/FaCrown.svelte';

  export let piece: Piece;

  const select = () => {
    if (!piece.highlighted) return;
    store.update((store) => {
      store.match.pieces.forEach((current) => {
        current.selected = current.id === piece.id;
      });
      return store;
    });
  };
</script>

<div
  use:variables={{ color: colors[piece.color], selection: colors.pink.dark, highlight: colors.orange }}
  class:selected={piece.selected}
  class:highlighted={piece.highlighted}
  on:click={select}
>
  {#if piece.isDama}
    <i use:variables={{ color: piece.color === 'black' ? 'white' : 'black' }}>
      <FaCrown />
    </i>
  {/if}
</div>

<style>
  div,
  i {
    display: flex;
    align-items: center;
    justify-content: center;
  }
  div {
    width: 35px;
    height: 35px;
    border-radius: 100%;
    margin: auto;
    transition: box-shadow 300ms;
    background-color: var(--color);
  }
  div.highlighted {
    box-shadow: 0 0 3px 3px var(--highlight);
    cursor: pointer;
  }
  div.selected {
    box-shadow: 0 0 3px 3px var(--selection) !important;
    cursor: pointer;
  }
  i {
    color: var(--color);
    width: 20px;
  }
</style>
