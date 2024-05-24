/**
 * Name: vite.config.ts
 * Description: Vite configuration file
 * 
 * Original from https://khalidabuhakmeh.com/running-vite-with-aspnet-core-web-
 * .. with many modifications
 */

import {UserConfig, defineConfig, ProxyOptions} from 'vite';
import {spawn} from "child_process";
import {fileURLToPath, URL} from 'node:url'
import vue from '@vitejs/plugin-vue'
import fs from "fs";
import path from "path";

import appsettingsDev from "../appsettings.Development.json";
import * as process from "process";

export default defineConfig(async ({ command }) => {
  const isDevelopment = command !== "build";

  // Prepare proxy options
  const proxyOptions : Record<string, ProxyOptions> = {};
  if(isDevelopment){
    appsettingsDev.ViteDevelopmentServer.RoutesForAspNet.forEach(actRoute =>{
      proxyOptions[actRoute] = {
        target: appsettingsDev.Kestrel.Endpoints.Http.Url,
        secure: false
      }
    })
  }
  console.log("## Proxy configuration for development server")
  console.log(proxyOptions);
  console.log("");

  // Handle development certificate
  let certFilePath = "";
  let keyFilePath = "";
  if(isDevelopment){
    const baseFolder =
        process.env.APPDATA !== undefined && process.env.APPDATA !== ''
            ? `${process.env.APPDATA}/ASP.NET/https`
            : `${process.env.HOME}/.aspnet/https`;
    const certificateName = process.env.npm_package_name;
    certFilePath = path.join(baseFolder, `${certificateName}.pem`);
    keyFilePath = path.join(baseFolder, `${certificateName}.key`);

    console.log("## Development certification file path")
    console.log("certFilePath: " + certFilePath);
    console.log("keyFilePath: " + keyFilePath);
    console.log("");

    // Ensure the certificate and key exist
    if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
      await new Promise<void>((resolve) => {
        spawn('dotnet', [
          'dev-certs',
          'https',
          '--export-path',
          certFilePath,
          '--format',
          'Pem',
          '--no-password',
        ], { stdio: 'inherit', })
            .on('exit', (code: any) => {
              resolve();
              if (code) {
                process.exit(code);
              }
            });
      });
    }
  }
  
  // Vite configuration
  const config: UserConfig = {
    plugins: [
      vue(),
    ],
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url))
      }
    },
    build: {
      emptyOutDir: true,
      outDir: 'dist',
      sourcemap: isDevelopment
    },
    server: isDevelopment ? {
      proxy: proxyOptions,
      port: appsettingsDev.ViteDevelopmentServer.Port,
      strictPort: true,
      https: isDevelopment ? {
        cert: certFilePath,
        key: keyFilePath
      } : undefined,
      hmr: {
        host: "localhost",
        clientPort: appsettingsDev.ViteDevelopmentServer.Port
      }
    } : undefined
  }

  console.log("## Full vite configuration")
  console.log(config);
  console.log("");
  
  return config;
});