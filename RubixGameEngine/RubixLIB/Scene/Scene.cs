using Rubix;
using System.Collections.Generic;

namespace RubixLIB
{
    public abstract class Scene
    {
        List<List<Entity>> spriteBatch;

        bool paused;
        bool hidden;

        public Scene()
        {
            spriteBatch = new List<List<Entity>>();
            paused = false;
            hidden = false;
        }

        public void Load()
        {
            foreach (List<Entity> layer in spriteBatch)
            {
                foreach (Entity entity in layer)
                {
                    entity.Load();
                }
            }
            OnLoad();
            Debug.Log("Scene Loaded!");
        }

        public void Pause(bool paused)
        {
            this.paused = paused;
        }

        public void Hide(bool hidden)
        {
            this.hidden = hidden;
        }

        public void Draw(float timeElapsed)
        {
            if (hidden)
                return;
            foreach (List<Entity> layer in spriteBatch)
            {
                foreach(Entity entity in layer)
                {
                    entity.Draw(timeElapsed);
                }
            }
            OnDraw(timeElapsed);
        }

        public void FixedUpdate(float timeElapsed)
        {
            if (paused)
                return;
            foreach (List<Entity> layer in spriteBatch)
            {
                foreach (Entity entity in layer)
                {
                    entity.FixedUpdate(timeElapsed);
                }
            }
        }

        public void Update()
        {
            if (!paused)
                OnUpdate();
        }

        #region AddLayer
        public void AddLayer()
        {
            spriteBatch.Add(new List<Entity>());
        }
        public void AddLayer(int index)
        {
            spriteBatch.Insert(index, new List<Entity>());
        }
        public void AddLayer(int index, List<Entity> layer)
        {
            spriteBatch.Insert(index, layer);
        }
        #endregion

        public void AddToLayer(int index, Entity entity)
        {
            spriteBatch[index].Add(entity);
        }

        public void Shutdown()
        {
            spriteBatch.Clear();
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw(float timeElapsed);
    }
}
