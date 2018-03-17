using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectFTP.SceneManagement
{
    public class StackedSceneManager
    {
        private static StackedSceneManager instance;
        private List<StackedScene> stack = new List<StackedScene>();

        private static SceneName[] mainScenes = { SceneName.MainMenu, SceneName.LevelScene, SceneName.WorldScene };

        public AsyncOperation Load(SceneName sceneName)
        {
            return Load(sceneName, new Dictionary<SceneParameter, object>());
        }

        public AsyncOperation Load(SceneName sceneName, IDictionary<SceneParameter, object> parameters)
        {
            if (stack.Exists(delegate(StackedScene entry) { return entry.Name == sceneName; }))
            {
                return null;
            }

            if (mainScenes.Any(delegate (SceneName name) { return name == sceneName; }))
            {
                // Find the current Main Scene
                int mainIndex = stack.FindIndex(delegate (StackedScene entry) { return mainScenes.Any(delegate (SceneName name) { return name == entry.Name; }); });
                UnloadRange(mainIndex);
            }

            StackedScene scene = stack.Find(delegate (StackedScene entry) { return entry.Name == sceneName; });
            if (scene == null) {
                scene = new StackedScene(sceneName, parameters);
            }

            stack.Add(scene);
            return scene.Load();
        }

        public void Unload(SceneName sceneName)
        {
            int sceneIndex = stack.FindIndex(delegate (StackedScene entry) { return entry.Name == sceneName; });

            UnloadRange(sceneIndex);
        }

        public void Reload()
        {
            // Find the current Main Scene
            StackedScene scene = stack.FindLast(delegate (StackedScene entry) { return mainScenes.Any(delegate (SceneName name) { return name == entry.Name; }); });
            Unload(scene.Name);
            Load(scene.Name, scene.Parameters);
        }
        
        private void UnloadRange(int index)
        {
            if (index < 0)
            {
                return;
            }

            // Unload all scenes that are load on top of it
            List<StackedScene> unloadStack = stack.GetRange(index, stack.Count - index);
            unloadStack.Reverse();
            unloadStack.ForEach(delegate (StackedScene entry) { entry.Unload(); });
            // Remove them from the stack
            stack.RemoveRange(index, stack.Count - index);
        }
    
        public static StackedScene Active
        {
            get 
            {
                return Instance.stack.Last();
            }
        }

        public static AsyncOperation LoadScene(SceneName name)
        {
            return Instance.Load(name);
        }

        public static AsyncOperation LoadScene(SceneName name, IDictionary<SceneParameter, object> parameters)
        {
            return Instance.Load(name, parameters);
        }

        public static void UnloadScene(SceneName name)
        {
            Instance.Unload(name);
        }

        public static void ReloadScene()
        {
            Instance.Reload();
        }

        private static StackedSceneManager Instance {
            get {
                if (instance == null)
                {
                    instance = new StackedSceneManager();
                }
                return instance;
            }
        }
    }
}
