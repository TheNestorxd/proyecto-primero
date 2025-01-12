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
            const int CASILLA_X_SIZE = 30;
            const int CASILLA_Y_SIZE = 20;
            Random numeroAleatorio = new Random();                                
            Casilla[,] Laberinto = new Casilla[CASILLA_X_SIZE,CASILLA_Y_SIZE];
            //============instanciando========================
            for (int i = 0; i < CASILLA_X_SIZE; i++)
                for (int j = 0; j < CASILLA_Y_SIZE; j++)
                    Laberinto[i,j] = new Casilla(); 

            //============Creacion del Mapa=========================
            
            crearLimites(Laberinto);                                                                                                
            colocarParedes(Laberinto , 100);
            generarJugador(Laberinto);
            Graph(Laberinto);
            juego(Laberinto);
            
            //generarJugador(Laberinto); 
                              
                                    
        } 
    //================Dibujado del mapa=============================
    static void render (Casilla [,] Mapa){
        
        Console.Clear();
        Graph(Mapa); 
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

    
    static void Graph(Casilla[,] Laberinto){
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
                
            }
        AnsiConsole.Write(canvas);  
    }

    static void desbloquearTodo(int[,] Laberinto , int i , int j)
    {                        
        
    }

    static void generarJugador(Casilla[,] Mapa)
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
            NoColocado = false;


        }
    }

    static (int,int) buscarJugador(Casilla[,] Mapa)
    {
        for(int j = 1; j < Mapa.GetLength(1) ; j++)
        for(int i = 1; i < Mapa.GetLength(0) ; i++)
        {
            if(Mapa[i,j].tipoCasilla == TipoCasilla.jugador)
            return ( i, j);
            
        }
        return (0,0);
    }
    

    static void juego(Casilla[,] Mapa)
    {     
        var resultado = buscarJugador(Mapa);
        int posicion_x = resultado.Item1;
        int posicion_y = resultado.Item2;     
        int velocidad = 100;
                        
        while(velocidad > 0)
        {
            ConsoleKeyInfo teclaPresionada = Console.ReadKey(true);
            switch(teclaPresionada.Key)
            {
                //izquierda
                case ConsoleKey.A:
                if(Mapa[posicion_x - 1, posicion_y].tipoCasilla == TipoCasilla.camino)
                {
                   Mapa[posicion_x - 1, posicion_y].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   buscarJugador(Mapa);
                   resultado = buscarJugador(Mapa);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2; 
                   render(Mapa);
                   velocidad --;
                }   
                //derecha                 
                    break;
                case ConsoleKey.D:                
                if(Mapa[posicion_x + 1, posicion_y].tipoCasilla == TipoCasilla.camino)
                {
                   Mapa[posicion_x + 1, posicion_y].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   buscarJugador(Mapa);
                   resultado = buscarJugador(Mapa);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                //arriba  
                    break;
                case ConsoleKey.W:
                if(Mapa[posicion_x, posicion_y - 1].tipoCasilla == TipoCasilla.camino)
                {
                   Mapa[posicion_x, posicion_y - 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   buscarJugador(Mapa);
                   resultado = buscarJugador(Mapa);
                   posicion_x = resultado.Item1;
                   posicion_y = resultado.Item2;
                   render(Mapa);
                   velocidad --;
                }
                //abajo 
                    break;
                case ConsoleKey.S:
                if(Mapa[posicion_x, posicion_y + 1].tipoCasilla == TipoCasilla.camino)
                {
                   Mapa[posicion_x, posicion_y + 1].tipoCasilla = TipoCasilla.jugador;
                   Mapa[posicion_x, posicion_y].tipoCasilla = TipoCasilla.camino;
                   buscarJugador(Mapa);
                   resultado = buscarJugador(Mapa);
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