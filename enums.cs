namespace EnumsNS
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

    public enum TipoPersonaje
    {
        Zara,
        Halvar,
        Axton,
        Yuri,
        Lyn,
        Mercer,
    }

    public class Casilla
    {
        //Vars
        public TipoCasilla tipoCasilla = TipoCasilla.camino;
               
        
    }


    
    
}