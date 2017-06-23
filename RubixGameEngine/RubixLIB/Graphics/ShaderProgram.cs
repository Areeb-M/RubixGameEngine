using OpenTK;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace RubixLIB.Graphics
{
    public class ShaderProgram
    {
        private int programID;

        public Shader[] shaders;

        public ShaderProgram(Shader[] shaders)
        {
            this.programID = GL.CreateProgram();
            this.shaders = shaders;
            foreach (Shader shader in shaders)
            {
                Compile(shader);
            }
            LinkProgram();
        }

        private void Compile(Shader shader)
        {
            int shaderID = GL.CreateShader(shader.type);
            GL.ShaderSource(shaderID, shader.source);
            GL.CompileShader(shaderID);
            GL.AttachShader(programID, shaderID);
        }

        private void LinkProgram()
        {
            GL.LinkProgram(programID);
        }

        public void UseProgram()
        {
            GL.UseProgram(programID);
        }

    }

    #region Shader Type Classes
    public class Shader
    {
        public ShaderType type;
        public string source;
    }

    public class VertexShader : Shader
    {
        public VertexShader(string source)
        {
            this.type = ShaderType.VertexShader;
            this.source = source;
        }

        public VertexShader(string path, int difference = 0)
        {
            this.type = ShaderType.VertexShader;
            this.source = File.ReadAllText(path);
        }
    }

    public class FragmentShader : Shader
    {
        public FragmentShader(string source)
        {
            this.type = ShaderType.FragmentShader;
            this.source = source;
        }

        public FragmentShader(string path, int difference = 0)
        {
            this.type = ShaderType.FragmentShader;
            this.source = File.ReadAllText(path);
        }
    }
    #endregion

}
