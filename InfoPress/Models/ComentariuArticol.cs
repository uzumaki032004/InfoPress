using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ComentariuArticol : IComentariu
    {
        public void AfiseazaComentariu()
        {
            Console.WriteLine("Comentariu pentru articol");
        }
    }
}