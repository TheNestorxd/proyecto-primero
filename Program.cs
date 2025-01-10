using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using Spectre.Console; 
class Program
{
    public static void Main(string[] args)
    {
            // Variables----------------------------------------------
            // 0 es camino , 1 es pared , 3 es limite
                     
             int paredesTotales = 60;                    
            Random numeroAleatorio = new Random();
            int aleatorio = 0;        
            int[,] Laberinto = new int[20, 20];           
            //--------------------------------------------------------

            // Creaccion de limites--------------------------------------------- OK
            crearLimites(Laberinto);           
            //----------------------------------------------------------------------
            var canvas = new Canvas(Laberinto.GetLength(0), Laberinto.GetLength(1));
                                                 
            // colocar paredes-----------------------------------OK
            colocarParedes(Laberinto , paredesTotales);                             
            //----------------------------------------------------

            //impedir encerronas------------
            for(int j = 1; j < Laberinto.GetLength(1) - 1; j++)
            for(int i = 1; j < Laberinto.GetLength(0) - 1; i++)
            {
                if(Laberinto[i,j] == 0)
                {
                    desbloquearTodo(Laberinto , i , j);
                }
            }
            
                                     
            // Contador de paredes-----------------------------
                //for(int j = 1; j < Laberinto.GetLength(1) -1; j++) 
                //for(int i = 1; i < Laberinto.GetLength(0) -1; i++)
                //{
                //    if(Laberinto[i,j] == 1)
                //    {paredesTotales = paredesTotales - 1;}
                // }
            //---------------------------------------------------
            //Dibujado y creaccion del canvas para mostrarlo en la consola-------- OK
                for(int j = 0; j < Laberinto.GetLength(1); j++)            
                for(int i = 0; i < Laberinto.GetLength(0); i++)
                {
                if(Laberinto[i,j] == 0)
                {canvas.SetPixel(i, j, Color.Black);}
                if(Laberinto[i,j] == 1)
                {canvas.SetPixel(i, j, Color.White);}
                if(Laberinto[i,j] == 3)
                {canvas.SetPixel(i, j, Color.Red);}
                }
                AnsiConsole.Write(canvas);                
            //-----------------------------------

            //Jugabilidad-------------------------

            //------------------------------------

        } 

        static void crearLimites(int[,] Laberinto)
        {
            for(int i = 0; i < Laberinto.GetLength(0); i++) //cerrando laterales
            {
                Laberinto[i,0] = 3;                                                   
                Laberinto[i,Laberinto.GetLength(1) - 1] = 3;
            }
            for(int i = 0; i < Laberinto.GetLength(1) ; i++) //cerrando arriba y abajo
            {
                Laberinto[0,i] = 3;
                Laberinto[Laberinto.GetLength(0) - 1,i] = 3;
            }
        }

        static void colocarParedes(int[,] Laberinto , int paredesTotales)
        {
            int x = 0;
            int y = 0;            
            Random numeroAleatorio = new Random();
            for(int i = 0; i < paredesTotales ; i++) 
            {                             
                x = numeroAleatorio.Next(1, Laberinto.GetLength(0) - 1);
                y = numeroAleatorio.Next(1, Laberinto.GetLength(1) - 1);
                Laberinto[x,y] = 1;                                                                 
            }
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



             
        