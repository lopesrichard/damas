<script lang="ts">
  export let text: string;
</script>

<button on:click>{text}</button>

<style>
  button {
    all: unset;
    padding: 5px 30px;
    margin-top: 10px;
    background-color: #5a5a5a;
    color: white;
    transition: background 300ms;
  }

  button:hover {
    background-color: #939393;
    cursor: pointer;
  }
</style>
