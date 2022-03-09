using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Game.Graphic
{
    static class Textures
    {
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        public static Texture GetTexture(string name)
        {
            if (textures.ContainsKey(name))
                return textures[name];

            var texture = new Texture("..\\..\\Content\\" + name + ".png");
            textures.Add(name, texture);
            return texture;
        }
    }
}
