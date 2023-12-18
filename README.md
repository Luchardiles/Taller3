# TALLER 3 

Este repositorio contiene el código para el backend hecho en .NET y el frontend Mobil hecho en Expo para una aplicación móvil que permite visualizar repositorios en una lista, ademas de todos los commits de estos y sus detalles.

## Requisitos previos

Para que el proyecto funcione de manera correcta, se deben tener los siguientes programas instalados:

- [Node.js (v18.18.2)](https://nodejs.org/download/release/v18.18.2/node-v18.18.2-x64.msi)
- [Dotnet SDK v8.0.1](https://dotnet.microsoft.com/es-es/download/dotnet/thank-you/sdk-8.0.100-windows-x64-installer)
- [Git v2.43.0](https://git-scm.com/downloads)
- [Expo Go](https://expo.dev/client) en tu dispositivo móvil

## Configuración del Backend

Abrir una consola de comando, y ejecutar el siguiente comando:

```bash
 dotnet tool install --global dotnet-ef
```

Clonar el repositorio en una carpeta vacia con el siguiente comando:

```bash
git clone https://github.com/Luchardiles/Taller3.git
```

Luego ejecute estos comandos para ingresar al backend:

```bash
cd TALLER3/Backend/backend
```

Ya dentro , copie el archivo _.env.example_ y renombrarlo a _.env_, con el siguiente comando:

```bash
cp .env.example .env
```

En el archivo .env rellene estos campos con sus propios datos:

```bash
GITHUB_TOKEN= # Token de GitHub
SECRET_KEY= # Llave secreta para JWT
```

Luego ejecute los siguientes comandos: 

-Instalación de las dependencias:

```bash
dotnet restore
```
-Crear de la base de datos :

```bash
dotnet ef database update
```

-Iniciar el backend:

```bash
dotnet run
```

## Configuración del Backend

Abre otra consola de comando en la raiz del proyecto, y ejecute estos comandos para ingresar al frontend:

```bash

cd TALLER3/Frontend/frontend
```

Dentro de la carpeta frontend, copie el archivo _.env.example_ y renombrarlo a _.env_, con el siguiente comando:

```bash
cp .env.example .env
```

Luego, rellene el campo con sus datos:

```bash
IP_V4= # IP V4 de su computador
```

Para obtener la IP V4 del computador, se debe abrir una consola de comando (en Windows) y ejecutar el siguiente comando:

```bash
ipconfig
```

Luego vuelva a su carpeta y ejecute los siguientes comandos:, para la instalación de las dependencias y la ejecución del proyecto:
-Instalación de las dependencias:

```bash
npm install
```

-Ejecutar el proyecto;

```bash
npx expo start
```

## Uso

Para usar la aplicación, debe tener instalada la aplicacion Expo Go en su dispositivo móvil y es escanear el código QR que aparece en la consola de comando donde se ejecutó el comando "npx expo start".