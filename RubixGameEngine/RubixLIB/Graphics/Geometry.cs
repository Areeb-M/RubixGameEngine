using OpenTK;
using System.Drawing;
using OpenTK.Graphics;

namespace RubixLIB.Graphics
{
    class Geometry
    {
        public Vertex[] ToTriangles(Rectangle rect)
        {
            Vertex[] result = new Vertex[6];
            result[0] = new Vertex(new Vector4(rect.X, rect.Y, 0, 1));
            result[1] = new Vertex(new Vector4(rect.X, rect.Y + rect.Height, 0, 1));
            result[2] = new Vertex(new Vector4(rect.X + rect.Width, rect.Y + rect.Height, 0, 1));
            result[3] = result[0];
            result[4] = new Vertex(new Vector4(rect.X + rect.Width, rect.Y, 0, 1));
            result[5] = result[2];

            return result;
        }
    }

    struct Vertex
    {
        public const int Size = (4) * 4; // size of struct in bytes

        private readonly Vector4 _position;

        public Vertex(Vector4 position)
        {
            _position = position;
        }
    }
}
