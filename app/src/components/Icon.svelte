<script lang="ts">
  import { css } from '@emotion/css';

  export let name: string;
  export let color: string;
  export let width: number = 20;
  export let height: number = 20;

  const styles = {
    img: css`
      color: ${color};
      width: ${width}px;
      height: ${height}px;
    `,
  };
</script>

<img src={`/icons/${name}.svg`} alt="icon" class={styles.img} />
