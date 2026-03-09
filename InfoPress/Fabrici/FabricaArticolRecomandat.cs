using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Fabrici
{
    public class FabricaArticolRecomandat : FabricaContinut
    {
        public override IContinut CreeazaContinut()
        {
            return new ArticolRecomandat();
        }
    }
}