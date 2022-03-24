<script lang="ts">
  import { css } from '@emotion/css';

  export let text: string;

  const styles = {
    button: css`
      all: unset;
      padding: 5px 30px;
      margin-top: 10px;
      background-color: #5a5a5a;
      color: white;
      transition: background 300ms;
      &:hover {
        background-color: #939393;
        cursor: pointer;
      }
    `,
  };
</script>

<button class={styles.button} on:click>{text}</button>
