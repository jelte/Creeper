using ProjectFTP.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectFTP.SceneManagement
{
    public class StackedScene
    {
        private SceneName name;
        private int buildIndex;
        private IDictionary<SceneParameter, object> parameters = new Dictionary<SceneParameter, object>();

        public StackedScene(SceneName name, IDictionary<SceneParameter, object> parameters)
        {
            this.name = name;
            // Determine the buildIndex.
            buildIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/" + name.ToString());
            this.parameters = parameters;
        }

        public SceneName Name
        {
            get { return name; }
        }

        public IDictionary<SceneParameter, object> Parameters
        {
            get { return parameters; }
        }

        public int BuildIndex
        {
            get { return buildIndex; }
        }

        public T Get<T>(SceneParameter key)
        {
            object value = null;
            if (parameters.TryGetValue(key, out value))
            {
                return (T)value;
            }

            return (T)value;
        }

        public Scene Scene
        {
            get { return SceneManager.GetSceneByBuildIndex(buildIndex); }
        }

        public AsyncOperation Load()
        {
            // Load the scene Additively
            return SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Additive);
        }

        public void Unload()
        {
            try
            {
                // Before Unloading the scene check if any animation should happen.
                GameObject animator = Scene.GetRootGameObjects()[0];
                if (animator != null && animator.GetComponent<SceneAnimator>() != null)
                {
                    // add the callback from when the animation is finished
                    animator.GetComponent<SceneAnimator>().ActionHandler += delegate (SceneAnimator.State state) {
                        if (state == SceneAnimator.State.FINISHED)
                        {
                            // Unload the scene.
                            SceneManager.UnloadSceneAsync(buildIndex);
                        }
                    };

                    // trigger the animation.
                    animator.GetComponent<SceneAnimator>().Hide();
                }
                else
                {
                    // No animation, just unload the scene.
                    SceneManager.UnloadSceneAsync(buildIndex);
                }
            }
            catch (ArgumentException e)
            {
                Debug.LogWarning("Unable to unload:" + buildIndex + "\n" + e);
            }
        }
    }

}
