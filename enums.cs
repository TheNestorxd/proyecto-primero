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
        trampa,
        sombra,
    }

    public enum TipoTrampa
    {
        Enfriamiento,
        Velocidad,
        Teletransportacion,
        Fuga,
        Generador
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
    public TipoCasilla tipoCasilla = TipoCasilla.camino;
    public TipoTrampa tipoTrampa { get; set; } 
}


    
    
}