using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Fabrici
{
    public class FabricaArticol : FabricaContinut
    {
        public override IContinut CreeazaContinut()
        {
            return new ArticolStire();
        }
    }
}