# 🧩 **Informe Técnico: Cazadores de Sombras**  
*Un viaje detrás de cámaras del desarrollo del juego*  

---

## 🛠️ **Arquitectura del Proyecto**  
### 🔌 Core Tecnológico  
```csharp
// Estructura básica de una Casilla
public class Casilla {
    public TipoCasilla tipoCasilla = TipoCasilla.camino;
    public TipoTrampa tipoTrampa { get; set; } 
}
Lenguaje: C# puro (Console App)

Dependencias:

🎨 Spectre.Console para gráficos en consola

🎵 NAudio para reproducción de música

Paradigma: Métodos estáticos + variables globales (por practicidad en etapas tempranas)



🗺️ Generación del Mapa
Base: Array bidimensional de objetos Casilla (inicialmente todos como caminos)

Capas:

🔒 Límites indestructibles

🧱 Paredes aleatorias (301 unidades)

✅ Verificación DFS de conectividad

Elementos dinámicos:

🕳️ 80 trampas aleatorias

👥 2 jugadores (casillas especiales)

👻 30 sombras (objetivo de captura)

🎮 Flujo del Juego
- Al iniciarse el juego se ejecuta la obvia bienvenida , en ella aparece el menu de inicio que te deja elejir entre 3 opciones , una es para salir de la app , la otra para ver la info del juego y la otra para empezar a jugar.
- Al presionar Jugar nos mostrara el primer menu de seleccion que es para escojer el personaje del primer jugador , luego de elejirlo este es quitado de la lista de seleccion para que el jugador 2 no pueda escojerlo.
- Al haber seleccionado los dos jugadores se procede a crear el mapa y mosgtrarlo , iniciando asi el juego , el juego funciona por un sistema por turnos que alterna entre el jugador 1 y el dos hasta que se cumpla la condicion de victoria(detalles sobre esto en el Readme o el juego).
- Al haber alcanzado la condicion de victoria se quita el mapa y se muestra las concluciones del programa que duran unos segundos hasta cerrarse.

🛠️ Problemas o trabas que tuve o tengo en el desarrollo :
- Lo mas problematico fue el que el mapa quedara con los caminos conectados , tuve que pedir fuerte ayuda de IA para eso y editarlo despues como se podra ver en un metodo llamado solucionarError que me arregla un problema que me creaba el DFS.
- Eleji crear variables globales ya que pasaba trabajo a la hora de querer usar diversas variables en metodos.
- Hasta ahora no he podido hacer que la musica se repita al terminar de reproducirse.
- Tengo clarisimo que profesionalmente los metodos y demas se separan en clases , peeero como me pase mucho tiempo sin saber usarlas decidi priorizar mecanicas y demas en vez de optimizar eso , en futuras optimizaciones lo llevare a cabo.
