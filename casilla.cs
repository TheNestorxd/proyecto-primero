namespace CasillaNS
{
    public enum TipoCasilla
    {
        camino,
        pared,
        limite,        
        jugador,
        jugador2,
    }
    public class Casilla
    {
        //Vars
        public TipoCasilla tipoCasilla = TipoCasilla.camino;
        
        public Trampa trampa= new Trampa();

        //Funct
        
        
    }
    
    public enum TipoTrampa
    {
        dardos,
        pinchos,

    }

     public class Trampa
    {
        public TipoTrampa tipoTrampa = TipoTrampa.dardos;

    }
}