using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using Spectre.Console; 
using CasillaNS;
using PersonajeNS;
class Program
{
    public static void Main(string[] args)
    {
        // Variables----------------------------------------------                                                                                  
        const int CASILLA_X_SIZE = 50;
        const int CASILLA_Y_SIZE = 20;
        int jugador = 0;
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
        Dibujar(Laberinto);
        
        //============Jugabilidad=========================
        jugador = 1;
        while(true)
        {
          jugarTurno(Laberinto , jugador);
          if(jugador == 1)
          {
             jugador = 2;             
          }
          else if(jugador == 2)
             jugador = 1;                                        
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

    static (int,int) buscarJugador(Casilla[,] Mapa , int jugador)
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
    

    static void jugarTurno(Casilla[,] Mapa , int jugador)
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
         velocidad = 4;
      }
      if(jugador == 2)
      {     
         resultado = buscarJugador(Mapa , 2);
         posicion_x = resultado.Item1;
         posicion_y = resultado.Item2;     
         velocidad = 4;         
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

            }

        }

    }

                                                                                                                                                            
}                     