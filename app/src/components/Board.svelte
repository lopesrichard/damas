<script lang="ts">
  import Square from 'components/Square.svelte';
  import { range } from 'lodash';
  import { Position } from 'store';
  import colors from 'colors';
  import { variables } from 'helpers';

  export let size: number;
</script>

<div use:variables={{ size, color: colors.gray.dark }}>
  {#each range(size - 1, -1) as y}
    {#each range(0, size) as x}
      <Square position={new Position(x, y)} />
    {/each}
  {/each}
</div>

<style>
  div {
    display: grid;
    width: calc(var(--size) * 50px);
    border: 5px solid var(--color);
    background-color: white;
    grid-template-rows: repeat(var(--size), 50px);
    grid-template-columns: repeat(var(--size), 50px);
  }
</style>
