# 🕵️♂️ Cazadores de Sombras 🕵️♀️

¡Un juego de estrategia y suerte donde dos jugadores compiten por capturar sombras en un laberinto lleno de trampas! Desarrollado en C# con la consola como campo de batalla.



---

## 🎮 **¿De qué trata?**
Eres uno de dos cazadores en un laberinto generado aleatoriamente. Tu objetivo es **capturar más sombras que tu oponente** antes de que se agoten los turnos. Pero cuidado: el mapa está plagado de trampas que pueden cambiar el rumbo del juego en segundos.

**¡La suerte y la estrategia serán tus mejores aliados!**

---

## ✨ **Características principales**
- 🔄 **Laberintos aleatorios**: Cada partida es única.
- 🎭 **6 personajes con habilidades únicas**: Rompe paredes, teletranspórtate,  reubica sombras, etc.
- ⚡ **Trampas dinámicas**: Congelación, teletransporte, generadores de sombras, y más.
- 🎨 **Interfaz colorida**: Gráficos en consola con `Spectre.Console`.
- 🎵 **Música ambiental**: Ambiente inmersivo con efectos de audio.

---

# 🛠️ Pasos detallados para instalar y jugar

### 1. **Descargar el repositorio**
   - **Opción A**: Clonar con Git  
     ```bash
     git clone https://github.com/tu-usuario/cazadores-de-sombras.git
     ```  
   - **Opción B**: Descargar manualmente  
     *Ve al repositorio en GitHub → "Code" → "Download ZIP".*  
     *Extrae el archivo ZIP en una carpeta de tu elección.*

---

### 2. **Instalar requisitos previos**
   - **.NET 8.0 SDK**:  
     Descárgalo desde [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/8.0).  
     *Sigue las instrucciones para tu sistema operativo (Windows, macOS, Linux).*

---

### 3. **Restaurar dependencias**
   Abre una terminal en la carpeta del proyecto y ejecuta:  
   ```bash
   dotnet restore

   5. Iniciar el juego
Ejecuta en la terminal:

bash
Copy
dotnet run
¡Listo! El menú principal aparecerá y podrás empezar a jugar 🎮.

🔧 Solucionar problemas comunes

Errores de compilación:
Verifica que tienes .NET 8.0 instalado con dotnet --version.

Pantalla parpadeante:
Es normal: la consola se redibuja constantemente para actualizar el mapa.
