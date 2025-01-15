using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using Spectre.Console; 
using CasillaNS;
using PersonajeNS;

namespace Principal
{
class Program
{
    public static void Main(string[] args)
    {
        // Variables----------------------------------------------                                                                                  
        const int CASILLA_X_SIZE = 50;
        const int CASILLA_Y_SIZE = 20;
        TipoPersonaje jugador1Personaje = TipoPersonaje.Halvar;
        TipoPersonaje jugador2Personaje = TipoPersonaje.Zara;
        int jugador = 1;
        TipoPersonaje jugadorPersonaje = jugador1Personaje;
        Random numeroAleatorio = new Random();                                
        Casilla[,] Laberinto = new Casilla[CASILLA_X_SIZE,CASILLA_Y_SIZE];
        //============instanciando========================
        for (int i = 0; i < CASILLA_X_SIZE; i++)
        for (int j = 0; j < CASILLA_Y_SIZE; j++)
        Laberinto[i,j] = new Casilla(); 

        //============Creacion del Mapa=========================
            
        crearLimites(Laberinto);                                                                                                
        colocarParedes(Laberinto , 100);
        generarJugadores(Laberinto);
        render(Laberinto);
        
        //============Jugabilidad=========================   
        while(true)
        {

          jugarTurno(Laberinto , jugador , jugadorPersonaje);
          if(jugador == 1)
          {
             jugador = 2;
             jugadorPersonaje = jugador2Personaje;             
          }
          else if(jugador == 2)
          {
            jugador = 1;
            jugadorPersonaje = jugador1Personaje; 
          }
                                                     
        }                                                                                  
    } 
    //================Dibujado del mapa=============================
    static void render (Casilla [,] Mapa){
        
        Console.Clear();
        Dibujar(Mapa); 
    }
    //===============================================================
    static void crearLimites(Casilla[,] Mapa)
    {
        for(int i = 0; i < Mapa.GetLength(0); i++) //cerrando laterales
        {
            Mapa[i,0].tipoCasilla = TipoCasilla.limite;                                                   
            Mapa[i,Mapa.GetLength(1) - 1].tipoCasilla = TipoCasilla.limite;
        }
        for(int i = 0; i < Mapa.GetLength(1) ; i++) //cerrando arriba y abajo
        {
            Mapa[0,i].tipoCasilla = TipoCasilla.limite;
            Mapa[Mapa.GetLength(0) - 1,i].tipoCasilla = TipoCasilla.limite;
        }
    }
    

    static void colocarParedes(Casilla[,] Laberinto , int paredesTotales)
    {
        int x = 0;
        int y = 0;   
        
        Random numeroAleatorio = new Random();
        for(int i = 0; i < paredesTotales ; i++) 
        {          
            do
            {
                x = numeroAleatorio.Next(1, Laberinto.GetLength(0) - 2);
                y = numeroAleatorio.Next(1, Laberinto.GetLength(1) - 2);

            }while(Laberinto[x,y].tipoCasilla != TipoCasilla.camino);

            Laberinto[x,y].tipoCasilla = TipoCasilla.pared;                                                                 
        
        }
    }

    
    static void Dibujar(Casilla[,] Laberinto){
        var canvas = new Canvas(Laberinto.GetLength(0), Laberinto.GetLength(1));
        for(int j = 0; j < Laberinto.GetLength(1); j++)            
            for(int i = 0; i < Laberinto.GetLength(0); i++)
            {
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.camino)
                    canvas.SetPixel(i, j, Color.Black);
                
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.pared)
                    canvas.SetPixel(i, j, Color.White);
                    
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.limite)
                    canvas.SetPixel(i, j, Color.Grey);
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.jugador)
                    canvas.SetPixel(i, j, Color.Blue);
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.jugador2)
                    canvas.SetPixel(i, j, Color.Red);
            }
        AnsiConsole.Write(canvas);  
    }

    static void desbloquearTodo(int[,] Laberinto , int i , int j)
    {                        
        
    }

    static void generarJugadores(Casilla[,] Mapa)
    {
        bool NoColocado = true;
        int x = 0;
        int y = 0;
        Random numeroAleatorio = new Random();
        while(NoColocado)
        {
            do
            {
                x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 2);
                y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 2);

            }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);
            Mapa[x,y].tipoCasilla = TipoCasilla.jugador;
            do
            {
                x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 2);
                y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 2);

            }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);
            Mapa[x,y].tipoCasilla = TipoCasilla.jugador2;
            NoColocado = false;


        }
    }

    public static (int,int) buscarJugador(Casilla[,] Mapa , int jugador)
    {
        for(int j = 1; j < Mapa.GetLength(1) ; j++)
        for(int i = 1; i < Mapa.GetLength(0) ; i++)
        {
            if(Mapa[i,j].tipoCasilla == TipoCasilla.jugador && jugador == 1)
            return ( i, j);
            if(Mapa[i,j].tipoCasilla == TipoCasilla.jugador2 && jugador == 2)
            return ( i, j);            
        }
        return (0,0);
    }

    public static Casilla[,]  usarHabilidad(TipoPersonaje tipo , Casilla[,] Mapa , int jugador)
    {
      int posicion_x;
      int posicion_y;
      if(tipo == TipoPersonaje.Zara)
      {
        bool habilidadNoUsada = true;
        var resultado = buscarJugador(Mapa , 2);
         posicion_x = resultado.Item1;
         posicion_y = resultado.Item2;
         Console.WriteLine("Zara : Estas paredes estorban(pulsa hacia donde ira su habilidad)");
         

         while(habilidadNoUsada)
         {
         ConsoleKeyInfo teclaPresionada = Console.ReadKey(true);
         switch(teclaPresionada.Key)
         {
          case ConsoleKey.A:
          if(Mapa[posicion_x - 1, posicion_y].tipoCasilla == TipoCasilla.pared)
          {
            Mapa[posicion_x - 1, posicion_y].tipoCasilla = TipoCasilla.camino;
            render(Mapa);
            habilidadNoUsada = false;
            if(jugador == 1)
            Globales.enfriamiento1 = 2;
            if(jugador == 2)
            Globales.enfriamiento2 = 2;            
          }
          else if(Mapa[posicion_x - 1, posicion_y].tipoCasilla == TipoCasilla.limite)
          {
            render(Mapa);
            Console.WriteLine("Zara: Ni siquiera yo puedo derribar estas paredes , mis puños se harian pure");
            return Mapa;
          }
          else if(Mapa[posicion_x - 1, posicion_y].tipoCasilla == TipoCasilla.camino)
          {
            render(Mapa);
            Console.WriteLine("Zara: No hay nada que derribar aqui");
            return Mapa;
          }
          break;
          case ConsoleKey.D:
          if(Mapa[posicion_x + 1, posicion_y].tipoCasilla == TipoCasilla.pared)
          {
            Mapa[posicion_x + 1, posicion_y].tipoCasilla = TipoCasilla.camino;
            render(Mapa);
            habilidadNoUsada = false;
            if(jugador == 1)
            Globales.enfriamiento1 = 2;
            if(jugador == 2)
            Globales.enfriamiento2 = 2;
          }
          else if(Mapa[posicion_x + 1, posicion_y].tipoCasilla == TipoCasilla.limite)
          {
            render(Mapa);
            Console.WriteLine("Zara: Ni siquiera yo puedo derribar estas paredes , mis puños se harian pure");
            return Mapa;
          }
          else if(Mapa[posicion_x + 1, posicion_y].tipoCasilla == TipoCasilla.camino)
          {
            render(Mapa);
            Console.WriteLine("Zara: No hay nada que derribar aqui");
            return Mapa;
          }
          break;
          case ConsoleKey.W:
          if(Mapa[posicion_x, posicion_y - 1].tipoCasilla == TipoCasilla.pared)
          {
            Mapa[posicion_x, posicion_y - 1].tipoCasilla = TipoCasilla.camino;
            render(Mapa);
            habilidadNoUsada = false;
            if(jugador == 1)
            Globales.enfriamiento1 = 2;
            if(jugador == 2)
            Globales.enfriamiento2 = 2;
          }
          else if(Mapa[posicion_x, posicion_y - 1].tipoCasilla == TipoCasilla.limite)
          {
            render(Mapa);
            Console.WriteLine("Zara: Ni siquiera yo puedo derribar estas paredes , mis puños se harian pure");
            return Mapa;
          }
          else if(Mapa[posicion_x, posicion_y - 1].tipoCasilla == TipoCasilla.camino)
          {
            render(Mapa);
            Console.WriteLine("Zara: No hay nada que derribar aqui");
            return Mapa;
          }
          break; 
          case ConsoleKey.S:
          if(Mapa[posicion_x, posicion_y + 1].tipoCasilla == TipoCasilla.pared)
          {
            Mapa[posicion_x, posicion_y + 1].tipoCasilla = TipoCasilla.camino;
            render(Mapa);
            habilidadNoUsada = false;
            if(jugador == 1)
            Globales.enfriamiento1 = 2;
            if(jugador == 2)
            Globales.enfriamiento2 = 2;
          }
          else if(Mapa[posicion_x, posicion_y + 1].tipoCasilla == TipoCasilla.limite)
          {
            render(Mapa);
            Console.WriteLine("Zara: Ni siquiera yo puedo derribar estas paredes , mis puños se harian pure");
            return Mapa;
          }
          else if(Mapa[posicion_x, posicion_y + 1].tipoCasilla == TipoCasilla.camino)
          {
            render(Mapa);
            Console.WriteLine("Zara: No hay nada que derribar aqui");
            return Mapa;
          }
          break;
          
         }
         }
        
         


      }
       return Mapa;
    }
    

    static void jugarTurno(Casilla[,] Mapa , int jugador , TipoPersonaje personaje)
    {
      var resultado = buscarJugador(Mapa , 1);
      int posicion_x = 5;
      int posicion_y = 5;
      int velocidad = 5;
      if(jugador == 1)
      {     
         resultado = buscarJugador(Mapa , 1);
         posicion_x = resultado.Item1;
         posicion_y = resultado.Item2;
         if(personaje == TipoPersonaje.Zara)
         {
           velocidad = 6;
         }
         else if(personaje == TipoPersonaje.Halvar)
         {
           velocidad = 4; 
         }
            else if(personaje == TipoPersonaje.Axton)
         {
           velocidad = 4; 
         }
          else if(personaje == TipoPersonaje.Ralof)
         {
           velocidad = 4; 
         }
          else if(personaje == TipoPersonaje.Sky)
         {
           velocidad = 5; 
         }
         else if(personaje == TipoPersonaje.Lyn)
         {
           velocidad = 7; 
         }
           
      }
      if(jugador == 2)
      {     
         resultado = buscarJugador(Mapa , 2);
         posicion_x = resultado.Item1;
         posicion_y = resultado.Item2;
          if(personaje == TipoPersonaje.Zara)
         {
           velocidad = 6;
         }
         else if(personaje == TipoPersonaje.Halvar)
         {
           velocidad = 4; 
         }
            else if(personaje == TipoPersonaje.Axton)
         {
           velocidad = 4; 
         }
          else if(personaje == TipoPersonaje.Ralof)
         {
           velocidad = 4; 
         }
          else if(personaje == TipoPersonaje.Sky)
         {
           velocidad = 5; 
         }
         else if(personaje == TipoPersonaje.Lyn)
         {
           velocidad = 7; 
         }      
      }
      
                        
        while(velocidad > 0)
        {
            ConsoleKeyInfo teclaPresionada = Console.ReadKey(true);
            switch(teclaPresionada.Key)
            {
                //izquierda
                case ConsoleKey.A:
                if(Mapa[posicion_x - 1, posicion_y].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[posicion_x - 1, posicion_y].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2; 
                   render(Mapa);
                   velocidad --;
                }
                  if(Mapa[posicion_x - 1, posicion_y].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[posicion_x - 1, posicion_y].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2; 
                   render(Mapa);
                   velocidad --;
                }    
                //derecha                 
                    break;
                case ConsoleKey.D:                
                if(Mapa[posicion_x + 1, posicion_y].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[posicion_x + 1, posicion_y].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                if(Mapa[posicion_x + 1, posicion_y].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[posicion_x + 1, posicion_y].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                //arriba  
                    break;
                case ConsoleKey.W:
                if(Mapa[posicion_x, posicion_y - 1].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[posicion_x, posicion_y - 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                if(Mapa[posicion_x, posicion_y - 1].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[posicion_x, posicion_y - 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                //abajo 
                    break;
                case ConsoleKey.S:
                if(Mapa[posicion_x, posicion_y + 1].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[posicion_x, posicion_y + 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                 if(Mapa[posicion_x, posicion_y + 1].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[posicion_x, posicion_y + 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                } 
                    break;
                case ConsoleKey.H:
                if(Globales.enfriamiento1 == 0 && jugador == 1)
                {
                  if(personaje == TipoPersonaje.Zara)
                {
                  usarHabilidad(personaje, Mapa , jugador);
                }
                }
                else if(Globales.enfriamiento2 == 0 && jugador == 2)
                {
                  if(personaje == TipoPersonaje.Zara)
                {
                  usarHabilidad(personaje, Mapa , jugador);
                }
                }
                else if(Globales.enfriamiento1 > 0 || Globales.enfriamiento2 > 0)
                {
                  if(jugador == 1)
                  Console.WriteLine("no puedes usar tu habilidad ahora , estara disponible en: " + Globales.enfriamiento1 + " turnos" );
                  if(jugador == 2)
                  Console.WriteLine("no puedes usar tu habilidad ahora , estara disponible en: " + Globales.enfriamiento2 + " turnos" );
                }
                break;
                }
                

            }
        if(jugador == 1)
        Globales.enfriamiento1 --;
        if(jugador == 2)
        Globales.enfriamiento2 --;

        }
        

    }
public static class Globales
{
  public static int enfriamiento1 = 0;
  public static int enfriamiento2 = 0;
}

                                                                                                                                                            
}


                   