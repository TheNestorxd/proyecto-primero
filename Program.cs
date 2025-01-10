using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using Spectre.Console; 
using CasillaNS;
class Program
{
    public static void Main(string[] args)
    {
            // Variables----------------------------------------------
            // 0 es camino , 1 es pared , 3 es limite
                     
                 
            const int CASILLA_X_SIZE = 20;
            const int CASILLA_Y_SIZE = 20;
            Random numeroAleatorio = new Random();
            int aleatorio = 0;                    
            Casilla[,] Laberinto = new Casilla[CASILLA_X_SIZE,CASILLA_Y_SIZE];
            for (int i = 0; i < CASILLA_X_SIZE; i++)
                for (int j = 0; j < CASILLA_Y_SIZE; j++)
                    Laberinto[i,j] = new Casilla();

            //Casilla casilla = new Casilla();

            //--------------------------------------------------------

            // Creaccion de limites--------------------------------------------- OK
            crearLimites(Laberinto);           
            //----------------------------------------------------------------------
            
                                                 
            // colocar paredes-----------------------------------OK
            colocarParedes(Laberinto , 60);                             
            //----------------------------------------------------

            //impedir encerronas------------
            // for(int j = 1; j < Laberinto.GetLength(1) - 1; j++)
            // for(int i = 1; j < Laberinto.GetLength(0) - 1; i++)
            // {
            //     if(Laberinto[i,j] == 0)
            //     {
            //         desbloquearTodo(Laberinto , i , j);
            //     }
            // }
            
                                     
            // Contador de paredes-----------------------------
                //for(int j = 1; j < Laberinto.GetLength(1) -1; j++) 
                //for(int i = 1; i < Laberinto.GetLength(0) -1; i++)
                //{
                //    if(Laberinto[i,j] == 1)
                //    {paredesTotales = paredesTotales - 1;}
                // }
            //---------------------------------------------------
            //Dibujado y creaccion del canvas para mostrarlo en la consola-------- OK
            Graph(Laberinto);            
            //-----------------------------------

            //Jugabilidad-------------------------

            //------------------------------------

        } 

    static void crearLimites(Casilla[,] casilla)
    {
        for(int i = 0; i < casilla.GetLength(0); i++) //cerrando laterales
        {
            casilla[i,0].tipoCasilla = TipoCasilla.limite;                                                   
            casilla[i,casilla.GetLength(1) - 1].tipoCasilla = TipoCasilla.limite;
        }
        for(int i = 0; i < casilla.GetLength(1) ; i++) //cerrando arriba y abajo
        {
            casilla[0,i].tipoCasilla = TipoCasilla.limite;
            casilla[casilla.GetLength(0) - 1,i].tipoCasilla = TipoCasilla.limite;
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
                    canvas.SetPixel(i, j, Color.Red);
            }
        AnsiConsole.Write(canvas);  
    }

    static void desbloquearTodo(int[,] Laberinto , int i , int j)
    {                        
        List<int> coordenadaI = new List<int>();
        List<int> coordenadaJ = new List<int>();
        int x = i;
        int y = j;   
        //Creacion de un mapa aparte donde se marcaran las casillas visitadas--------
            int[,] Tablero = new int[20,20];
            for(int b = 0; b < Tablero.GetLength(1); b++)
            for(int a = 0; a < Tablero.GetLength(0); a++)
            {
                Tablero[a,b] = Laberinto[a,b];
            }
            //---------------------------------------------------------------------------
            while(coordenadaI.Count > 0 && coordenadaJ.Count > 0)
            {
                x = coordenadaI[0];
                y = coordenadaJ[0];
                //arriba
                if(Tablero[x - 1,y] != 2 && Laberinto[x - 1, y] == 0)
                {
                    coordenadaI.Add(x - 1);
                    coordenadaJ.Add(y);
                }
                //abajo
                if(Tablero[x + 1,y] != 2 && Laberinto[x + 1, y] == 0)
                {
                    coordenadaI.Add(x + 1);
                    coordenadaJ.Add(y);
                }
                //izquierda
                if(Tablero[x,y - 1] != 2 && Laberinto[x, y - 1] == 0)
                {
                    coordenadaI.Add(x);
                    coordenadaJ.Add(y - 1);
                }
                //derecha
                if(Tablero[x,y + 1] != 2 && Laberinto[x, y + 1] == 0)
                {
                    coordenadaI.Add(x);
                    coordenadaJ.Add(y + 1);
                }
                //marcar como visitada y borrarla de la lista
                Tablero[x,y] = 2;
                coordenadaI.RemoveAt(0);
                coordenadaJ.RemoveAt(0);
            }
            //Recorrido en busca de caminos no visitados
            for(int b = 0; b < Tablero.GetLength(1); b++)
            for(int a = 0; a < Tablero.GetLength(0); a++)
            {
                if(Tablero[a,b] == 0)
                {
                    for(int c = 1; c < Laberinto.GetLength(0) - 1; c++)
                    {
                        Laberinto[c,b] = 0;
                    }
                    for(int c = 1; c < Laberinto.GetLength(1) - 1; c++)
                    {
                        Laberinto[a,c] = 0;
                    }
                    
                }
            }
        }


    static void juego()
    {

    }

    public enum personajes
    {

    }
            
    
    
    



                
                
                                               
        
                                                                                
}

class Personaje
{
    
    
    public Personaje()
    {
       int vida;
       int velocidad;

    }

}



             
        