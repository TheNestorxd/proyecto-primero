namespace CasillaNS
{
    public enum TipoCasilla
    {
        camino,
        pared,
        limite,        
        jugador,
        jugador2,
        caminovisitado,
        trampaenfriamiento,
        trampavelocidad,
        trampateletransportacion,
        sombra,
    }
    public class Casilla
    {
        //Vars
        public TipoCasilla tipoCasilla = TipoCasilla.camino;
               
        
    }
    
    
}