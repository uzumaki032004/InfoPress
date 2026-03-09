using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Fabrici
{
    public class FabricaGalerie : FabricaContinut
    {
        public override IContinut CreeazaContinut()
        {
            return new GalerieFoto();
        }
    }
}