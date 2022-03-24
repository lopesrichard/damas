<script lang="ts">
  import { css } from '@emotion/css';
  import colors from 'colors';
  import Square from './Square.svelte';
  import { range } from 'lodash';
  import { Position } from 'store';

  export let size: number;

  const styles = {
    board: css`
      display: grid;
      width: calc(${size} * 50px);
      border: 5px solid ${colors.gray.dark};
      background-color: white;
      grid-template-rows: repeat(${size}, 50px);
      grid-template-columns: repeat(${size}, 50px);
    `,
  };
</script>

<div class={styles.board}>
  {#each range(size - 1, -1) as y}
    {#each range(0, size) as x}
      <Square position={new Position(x, y)} />
    {/each}
  {/each}
</div>
