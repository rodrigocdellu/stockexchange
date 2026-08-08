import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import * as Path from 'path'

// https://vite.dev/config/
export default defineConfig({
    envDir:  Path.join(__dirname, "src/environments"),
    publicDir: Path.join(__dirname, "public"),
    root: Path.join(__dirname, "src"),
    build: {
        outDir: Path.join(__dirname, "dist"),
        rollupOptions: {
            input: [Path.join(__dirname, "src/index.html")],
        },
        emptyOutDir: true
    },
    plugins: [
        vue()
    ],
    server: {
        port: 5173,
        host: 'localhost'
    }
});
