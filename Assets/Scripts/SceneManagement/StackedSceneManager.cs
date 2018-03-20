using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectFTP.SceneManagement
{
    public class StackedSceneManager
    {
        private static SceneName[] mainScenes = { SceneName.MainMenu, SceneName.LevelScene, SceneName.WorldScene };
        private static StackedSceneManager instance;
        
        private List<StackedScene> stack = new List<StackedScene>();

        public AsyncOperation Load(SceneName sceneName)
        {
            return Load(sceneName, new Dictionary<SceneParameter, object>());
        }

        public AsyncOperation Load(SceneName sceneName, IDictionary<SceneParameter, object> parameters)
        {
            // Check if the scene is already loaded.
            if (stack.Exists(delegate(StackedScene entry) { return entry.Name == sceneName; }))
            {
                return null;
            }

            // Check if the scene is a Main scene
            if (mainScenes.Any(delegate (SceneName name) { return name == sceneName; }))
            {
                // Find the current Main Scene
                int mainIndex = stack.FindIndex(delegate (StackedScene entry) { return mainScenes.Any(delegate (SceneName name) { return name == entry.Name; }); });
                // Unload all the scene loaded on top the old main scene.
                UnloadRange(mainIndex);
            }

            // Create the Scene Wrapper.
            StackedScene scene = new StackedScene(sceneName, parameters);

            // Add the scene to the stack.
            stack.Add(scene);

            // Load the scene.
            return scene.Load();
        }

        public void Unload(SceneName sceneName)
        {
            // Find the scene in the stack.
            int sceneIndex = stack.FindIndex(delegate (StackedScene entry) { return entry.Name == sceneName; });

            // Unload all scenes loaded on top.
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
            // Unload from top to bottom.
            unloadStack.Reverse();
            unloadStack.ForEach(delegate (StackedScene entry) { entry.Unload(); });
            // Remove them from the stack
            stack.RemoveRange(index, stack.Count - index);
        }

        #region static methods
        private static StackedSceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StackedSceneManager();
                }
                return instance;
            }
        }

        public static StackedScene Active
        {
            get {
                if (Instance.stack.Count == 0)
                {
                    return null;
                }
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
        #endregion
    }
}
