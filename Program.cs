﻿using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using Spectre.Console; 
using CasillaNS;
using PersonajeNS;
using HabilidadesNS;

namespace Principal
{
class Program
{
    public static void Main(string[] args)
    {
        // Variables----------------------------------------------                                                                                  
        const int CASILLA_X_SIZE = 60;
        const int CASILLA_Y_SIZE = 30;
        TipoPersonaje jugador1Personaje = TipoPersonaje.Halvar;
        asignandoVelocidades(jugador1Personaje , 1);
        TipoPersonaje jugador2Personaje = TipoPersonaje.Zara;
        asignandoVelocidades(jugador2Personaje , 2);
        int jugador = 1;
        TipoPersonaje jugadorPersonaje = jugador1Personaje;
        Random numeroAleatorio = new Random();        
        Casilla[,] Laberinto = new Casilla[Globales.CASILLA_X_SIZE,Globales.CASILLA_Y_SIZE];
        //============instanciando========================
        for (int i = 0; i < CASILLA_X_SIZE; i++)
        for (int j = 0; j < CASILLA_Y_SIZE; j++)
        Laberinto[i,j] = new Casilla();                                   
        //============Creacion del Mapa=========================
        crearMapa(Laberinto);
        
        
                                          
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
    

    static void colocarParedes(Casilla[,] Mapa , int paredesTotales)
    {
        int x = 0;
        int y = 0;
        int ParedesTotales = paredesTotales;  
        
        Random numeroAleatorio = new Random();
        
        
        for(int i = 0; i < ParedesTotales ; i++) 
        {          
            do
            {
                x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 2);
                y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 2);

            }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);

            Mapa[x,y].tipoCasilla = TipoCasilla.pared;                                                                 
        
        }
                                      
    }
        
    

    public static int contarParedes(Casilla[,] Mapa)
    {
      int contadorParedes = 0;
      for(int j = 1; j < Globales.CASILLA_Y_SIZE - 1 ; j++)
      for(int i = 1; i < Globales.CASILLA_X_SIZE - 1 ; i++)
      {
        if(Mapa[i,j].tipoCasilla == TipoCasilla.pared)
        contadorParedes ++;
      }
      return contadorParedes;
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
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.trampavelocidad)
                    canvas.SetPixel(i, j, Color.Pink1); 
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.trampateletransportacion)
                    canvas.SetPixel(i, j, Color.Yellow);
                if(Laberinto[i,j].tipoCasilla == TipoCasilla.trampaenfriamiento)
                    canvas.SetPixel(i, j, Color.Green);  
                      
            }
        AnsiConsole.Write(canvas);  
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

//==================Metodos de Habilidades==============================================================
    public static Casilla[,]  usarHabilidadZara(TipoPersonaje tipo , Casilla[,] Mapa , int jugador)
    {
      int posicion_x;
      int posicion_y;
      //==================Habilidad de Zara=========================
      if(tipo == TipoPersonaje.Zara)
      {
        bool habilidadNoUsada = true;
        var resultado = buscarJugador(Mapa , jugador);
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
      //==================================================================================
       return Mapa;
    }

    public static void usarHabilidadHalvar(TipoPersonaje tipo , Casilla[,] Mapa , int jugador)
    {
       var resultado = buscarJugador(Mapa , 1);
      int posicion_x1 = resultado.Item1;
      int posicion_y1 = resultado.Item2;
       var resultado2 = buscarJugador(Mapa , 2);
      int posicion_x2 = resultado2.Item1;
      int posicion_y2 = resultado2.Item2;
      Mapa[posicion_x1,posicion_y1].tipoCasilla = TipoCasilla.jugador2;
      Mapa[posicion_x2,posicion_y2].tipoCasilla = TipoCasilla.jugador;      
      render(Mapa);
      if(jugador == 1)
      {
      Globales.enfriamiento1 = Globales.enfriamiento1 + 5;
      Globales.posicion_x_actual = posicion_x2;
      Globales.posicion_y_actual = posicion_y2;
      }
      else if(jugador == 2)
      {
      Globales.enfriamiento2 = Globales.enfriamiento2 + 5;
      Globales.posicion_x_actual = posicion_x1;
      Globales.posicion_y_actual = posicion_y1;
      }
    }  
//=======================================================================================================
    static void jugarTurno(Casilla[,] Mapa , int jugador , TipoPersonaje personaje)
    {      
      var resultado = buscarJugador(Mapa , 1);
      Globales.posicion_x_actual = 5;
      Globales.posicion_y_actual = 5;
      if(jugador == 1)
      {     
         resultado = buscarJugador(Mapa , 1);
         Globales.posicion_x_actual = resultado.Item1;
         Globales.posicion_y_actual = resultado.Item2;
         Globales.velocidad = Globales.velocidadMax1;
         
           
      }
      if(jugador == 2)
      {     
         resultado = buscarJugador(Mapa , 2);
         Globales.posicion_x_actual = resultado.Item1;
         Globales.posicion_y_actual = resultado.Item2;
          Globales.velocidad = Globales.velocidadMax2;
             
      }
      
                        
        while(Globales.velocidad > 0)
        {         
            ConsoleKeyInfo teclaPresionada = Console.ReadKey(true);
            switch(teclaPresionada.Key)
            {
                //======================izquierda Jugador 1================================
                case ConsoleKey.A:
                if(Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 1);
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 1);
                   resultado = buscarJugador(Mapa , 1);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 1);
                   Mapa[Globales.posicion_x_actual - 1,Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                //=================================Izquierda Jugador 2=======================================
                  if(Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.camino && jugador == 2 )
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                } 
                else if( Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 2);
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 2);
                   resultado = buscarJugador(Mapa , 2);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 2);
                  Mapa[Globales.posicion_x_actual - 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                //=========================================================================================== 

                //======================================Derecha  jugador 1========================================               
                    break;
                case ConsoleKey.D:                
                if(Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.camino && jugador == 1 )
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2;
                   render(Mapa);
                   Globales.velocidad --;
                }
                 else if( Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 1);
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 1);
                   resultado = buscarJugador(Mapa , 1);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 1);
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                //==============================================================================================

                //==================================derecha jugador 2======================================
                if(Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2;
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 2);
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 2);
                   resultado = buscarJugador(Mapa , 2);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 2);
                   Mapa[Globales.posicion_x_actual + 1, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                //===========================================================================================

                //========================================arriba  jugador 1=================================================
                    break;
                case ConsoleKey.W:
                if(Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2;
                   render(Mapa);
                   Globales.velocidad --;
                }
                 else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 1);
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 1);
                   resultado = buscarJugador(Mapa , 1);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 1);
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                //==============================================================================================

                //===================================arriba jugador 2==========================================
                if(Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2;
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 2);
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 2);
                   resultado = buscarJugador(Mapa , 2);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 2);
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual - 1].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                //==================================================================================

                //===================================abajo jugador 1=================================
                    break;
                case ConsoleKey.S:
                if(Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.camino && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2;
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 1);
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 1);
                   resultado = buscarJugador(Mapa , 1);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 1)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 1);
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 1);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                
                //======================================================================================

                //============================abajo jugador 2================================================
                 if(Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.camino && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2;
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.trampaenfriamiento && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampaenfriamiento , 2);
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.trampavelocidad && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampavelocidad , 2);
                   resultado = buscarJugador(Mapa , 2);                   
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);                   
                }
                else if( Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla == TipoCasilla.trampateletransportacion && jugador == 2)
                {
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.jugador2;
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual].tipoCasilla = TipoCasilla.camino;
                   tomaTrampa(Mapa , TipoCasilla.trampateletransportacion , 2);
                   Mapa[Globales.posicion_x_actual, Globales.posicion_y_actual + 1].tipoCasilla = TipoCasilla.camino;
                   resultado = buscarJugador(Mapa , 2);
                   Globales.posicion_x_actual = resultado.Item1;
                   Globales.posicion_y_actual = resultado.Item2; 
                   render(Mapa);
                   Globales.velocidad --;
                } 
                //===========================Habilidad=================================================
                    break;
                case ConsoleKey.H:
                if(Globales.enfriamiento1 == 0 && jugador == 1)
                {
                  if(personaje == TipoPersonaje.Zara)
                {
                  usarHabilidadZara(personaje, Mapa , jugador);
                }
                else if(personaje == TipoPersonaje.Halvar)
                {
                  usarHabilidadHalvar(personaje, Mapa , jugador);
                }
                }
                else if(Globales.enfriamiento2 == 0 && jugador == 2)
                {
                  if(personaje == TipoPersonaje.Zara)
                {
                  usarHabilidadZara(personaje, Mapa , jugador);
                }
                else if(personaje == TipoPersonaje.Halvar)
                {
                  usarHabilidadHalvar(personaje, Mapa , jugador);
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
        if(jugador == 1 && Globales.enfriamiento1 > 0)
        Globales.enfriamiento1 --;
        if(jugador == 2 && Globales.enfriamiento2 > 0)
        Globales.enfriamiento2 --;

        }

    static void crearMapa(Casilla[,] Mapa)
    {    
      bool Completo = false;      
      do{
        for(int j = 0; j < Mapa.GetLength(1); j++)
        for(int i = 0; i < Mapa.GetLength(0); i++)
        {
          Mapa[i,j].tipoCasilla = TipoCasilla.camino;
        }
        crearLimites(Mapa);
        colocarParedes(Mapa , 401);
        Completo = EsConectado(Mapa);
      }while(Completo == false);
      solucionarError(Mapa);
      agregarTrampas(Mapa , 100);
      generarJugadores(Mapa);
      render(Mapa);
    } 

     static bool EsConectado(Casilla[,] Mapa)
    {
        int filas = Mapa.GetLength(0);
        int columnas = Mapa.GetLength(1);

        Casilla[,] MapaCopia = (Casilla[,])Mapa.Clone();

        // Encuentra la primera celda de camino
        (int fila, int columna)? inicio = null;
        for (int i = 1; i < filas - 1; i++)
        {
            for (int j = 1; j < columnas - 1; j++)
            {
                if (Mapa[i, j].tipoCasilla == TipoCasilla.camino)
                {
                    inicio = (i, j);
                    break;
                }
            }
            if (inicio.HasValue) break;
        }

        if (!inicio.HasValue) return true; // No hay caminos, por lo tanto están "conectados"

        // Función DFS para explorar el mapa
        void DFS(int x, int y)
        {
            if (x < 0 || x >= filas || y < 0 || y >= columnas || MapaCopia[x, y].tipoCasilla != TipoCasilla.camino)
                return;

            MapaCopia[x, y].tipoCasilla = TipoCasilla.caminovisitado; // Marcar como visitado

            // Llamadas recursivas a las celdas adyacentes
            DFS(x + 1, y); // Abajo
            DFS(x - 1, y); // Arriba
            DFS(x, y + 1); // Derecha
            DFS(x, y - 1); // Izquierda
        }

        // Ejecutar DFS desde la celda inicial
        DFS(inicio.Value.fila, inicio.Value.columna);

        // Verificar si hay caminos no visitados
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (MapaCopia[i, j].tipoCasilla == TipoCasilla.camino) // Encontrar un camino no visitado
                    return false;
            }
        }
        return true;
    }

     static void solucionarError(Casilla[,] Mapa) 
     {
      for(int j = 1; j < Mapa.GetLength(1) - 1; j++)
      for(int i = 1; i < Mapa.GetLength(0) - 1; i++)
      {
        if(Mapa[i, j].tipoCasilla == TipoCasilla.caminovisitado)
        Mapa[i,j].tipoCasilla = TipoCasilla.camino;
      }

     }

     static void agregarTrampas(Casilla[,] Mapa , int cantTrampas)
     {
      int x = 0;
      int y = 0;
      int z = 0;
      int cantColocada = 0;
       Random numeroAleatorio = new Random();      
      for(int i = 0; i < cantTrampas; i++)
      {
        if(cantColocada < cantTrampas)
        {
          z = numeroAleatorio.Next(1,4);
          if(z == 1)
          {
            do{
            x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 1);
            y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 1);
            }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);
            Mapa[x,y].tipoCasilla = TipoCasilla.trampaenfriamiento;
            cantColocada++;
          }
          else if(z == 2)
          {
            do{
            x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 1);
            y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 1);
            }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);
            Mapa[x,y].tipoCasilla = TipoCasilla.trampavelocidad;
            cantColocada++;
          }
          else if(z == 3)
          {
            do{
            x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 1);
            y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 1);
            }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);
            Mapa[x,y].tipoCasilla = TipoCasilla.trampateletransportacion;
            cantColocada++;
          }

        }

      }



     }

     static void tomaTrampa(Casilla[,] Mapa , TipoCasilla tipoTrampa , int jugador)
     {
      Random numeroAleatorio = new Random();
      int x = 0;
      int y = 0;
      //========trampa de enfriamiento====================================
       if(tipoTrampa == TipoCasilla.trampaenfriamiento && jugador == 1)
       Globales.enfriamiento1 = Globales.enfriamiento1 + 5;
       else if(tipoTrampa == TipoCasilla.trampaenfriamiento && jugador == 2)
       Globales.enfriamiento2 = Globales.enfriamiento2 + 5;
       //=================================================================

       //======================trampa de velocidad==================
       else if(tipoTrampa == TipoCasilla.trampavelocidad)
       Globales.velocidad = 0;
       //============================================================
       else if(tipoTrampa == TipoCasilla.trampateletransportacion)
       {
       do
       {
         x = numeroAleatorio.Next(1, Mapa.GetLength(0) - 2);
         y = numeroAleatorio.Next(1, Mapa.GetLength(1) - 2);

       }while(Mapa[x,y].tipoCasilla != TipoCasilla.camino);

       if(jugador == 1)
       {
        Mapa[x,y].tipoCasilla = TipoCasilla.jugador;
       }
       else if(jugador == 2)
       {
        Mapa[x,y].tipoCasilla = TipoCasilla.jugador2;
       }
       }

       

     }

     static void asignandoVelocidades(TipoPersonaje tipoPersonaje , int jugador)
     {
      if(jugador == 1)
      {
       if(tipoPersonaje == TipoPersonaje.Zara)
         {
           Globales.velocidadMax1 = 4;
         }
         else if(tipoPersonaje == TipoPersonaje.Halvar)
         {
           Globales.velocidadMax1 = 4; 
         }
            else if(tipoPersonaje == TipoPersonaje.Axton)
         {
           Globales.velocidadMax1 = 4; 
         }
          else if(tipoPersonaje == TipoPersonaje.Ralof)
         {
           Globales.velocidadMax1 = 4; 
         }
          else if(tipoPersonaje == TipoPersonaje.Sky)
         {
           Globales.velocidadMax1 = 4; 
         }
         else if(tipoPersonaje == TipoPersonaje.Lyn)
         {
           Globales.velocidadMax1 = 4;
         }
        }
      if(jugador == 2)
      {
       if(tipoPersonaje == TipoPersonaje.Zara)
         {
           Globales.velocidadMax2 = 4;
         }
         else if(tipoPersonaje == TipoPersonaje.Halvar)
         {
           Globales.velocidadMax2 = 4; 
         }
            else if(tipoPersonaje == TipoPersonaje.Axton)
         {
           Globales.velocidadMax2 = 4; 
         }
          else if(tipoPersonaje == TipoPersonaje.Ralof)
         {
           Globales.velocidadMax2 = 4; 
         }
          else if(tipoPersonaje == TipoPersonaje.Sky)
         {
           Globales.velocidadMax2 = 4; 
         }
         else if(tipoPersonaje == TipoPersonaje.Lyn)
         {
           Globales.velocidadMax2 = 4;
         }
     }
  }
}
public static class Globales
{
  public static int enfriamiento1 = 0;
  public static int enfriamiento2 = 0;
  public static int CASILLA_X_SIZE = 60;
  public static int CASILLA_Y_SIZE = 30;
  public static int velocidad = 0;
  public static int velocidadMax1 = 0;
  public static int velocidadMax2 = 0;
  public static int posicion_x_actual;
  public static int posicion_y_actual;

}


                                                                                                                                                            
}


                   