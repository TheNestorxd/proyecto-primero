namespace CasillaNS
{
    public enum TipoCasilla
    {
        camino,
        pared,
        limite,
        meta,
    }
    public class Casilla
    {
        //Vars
        public TipoCasilla tipoCasilla = TipoCasilla.camino;
        public bool alcanzable = false;
        public Trampa trampa= new Trampa();

        //Funct
        public Casilla(){
        }
        
    }
    
    public enum TipoTrampa
    {
        dardos,
        pinchos,

    }

     public class Trampa
    {
        public TipoTrampa tipoTrapa = TipoTrampa.dardos;

    }
}