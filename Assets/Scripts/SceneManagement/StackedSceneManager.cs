using ProjectFTP.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectFTP.SceneManagement
{
    internal class StackedScene
    {
        private SceneName name;
        private int buildIndex;

        public StackedScene(SceneName name)
        {
            this.name = name;
            buildIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/" + name.ToString());
        }
        
        public SceneName Name
        {
            get { return name; }
        }

        public int BuildIndex
        {
            get { return buildIndex; }
        }

        public Scene Scene
        {
            get { return SceneManager.GetSceneByBuildIndex(buildIndex);  }
        }
    
        public AsyncOperation Load()
        {
            return SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Additive);
        }

        public void Unload()
        {
            try
            {
                GameObject animator = Scene.GetRootGameObjects()[0];
                if (animator != null && animator.GetComponent<SceneAnimator>() != null)
                {
                    animator.GetComponent<SceneAnimator>().ActionHandler += delegate (SceneAnimator.State state) {
                        if (state == SceneAnimator.State.FINISHED)
                        {
                            SceneManager.UnloadSceneAsync(buildIndex);
                        }
                    };
                    animator.GetComponent<SceneAnimator>().Hide();
                }
                else
                {
                    SceneManager.UnloadSceneAsync(buildIndex);
                }
            }
            catch (ArgumentException e) {
                Debug.LogWarning("Unable to unload:" + buildIndex + "\n"+e);
            }
        }
    }


    public class StackedSceneManager
    {
        private static StackedSceneManager instance;
        private List<StackedScene> stack = new List<StackedScene>();

        private static SceneName[] mainScenes = { SceneName.MainMenu, SceneName.StoryModeScene };

        public AsyncOperation Load(SceneName sceneName)
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
                scene = new StackedScene(sceneName);
            }

            stack.Add(scene);
            return scene.Load();
        }

        public void Unload(SceneName sceneName)
        {
            int sceneIndex = stack.FindIndex(delegate (StackedScene entry) { return entry.Name == sceneName; });

            UnloadRange(sceneIndex);
        }

        public void Reload(SceneName sceneName)
        {
            Unload(sceneName);
            Load(sceneName);
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
    
        public static Scene Active
        {
            get
            {
                return Instance.stack.Last().Scene;
            }
        }

        public static AsyncOperation LoadScene(SceneName name)
        {
            return Instance.Load(name);
        }

        public static void UnloadScene(SceneName name)
        {
            Instance.Unload(name);
        }

        public static void ReloadScene(SceneName name)
        {
            Instance.Reload(name);
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
