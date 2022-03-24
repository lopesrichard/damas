import { defineConfig } from 'vite';
import { svelte } from '@sveltejs/vite-plugin-svelte';
import alias from '@rollup/plugin-alias';
import { resolve } from 'path';

const base = resolve(__dirname, 'src');

export default defineConfig({
  plugins: [
    svelte(),
    alias({
      entries: [{ find: /(.*)/, replacement: resolve(base, '$1') }],
    }),
  ],
});
