import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
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
    },
    plugins: [
        react(),
    ],
    resolve: {
        alias: {
            "@": Path.resolve(__dirname, "./src"),
        },
    },
    //plugins: [react()], // 2025/05/14 - The original Vite project started with just this line
});
