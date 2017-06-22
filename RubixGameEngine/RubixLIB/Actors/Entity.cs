using Rubix;
using OpenTK;
using RubixLIB.Graphics;

namespace RubixLIB
{
    public abstract class Entity
    {
        private Vector4 velocity;
        private Vector4 position;

        protected ShaderProgram program;

        #region Velocity and Position Properties
        Vector4 Velocity
        {
            get { return velocity; }
        }

        Vector4 Position
        {
            get { return Position;  }
        }
        #endregion

        public Entity(Vector4 vel, Vector4 pos)
        {
            velocity = vel;
            position = pos;
        }

        public void FixedUpdate(float timeElapsed)
        {
            position = Vector4.Add(position, Vector4.Multiply(velocity, timeElapsed));
        }

        public Vector4 TempFixedUpdate(float timeElapsed)
        {
            return Vector4.Add(position, Vector4.Multiply(velocity, timeElapsed)); 
        }

        public void SetShaderProgram(ShaderProgram program)
        {
            this.program = program;
        }

        public abstract void Draw(float timeElapsed);
        public abstract void Load();

    }
}
