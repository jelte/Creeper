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
            catch (ArgumentException e)
            {
                Debug.LogWarning("Unable to unload:" + buildIndex + "\n" + e);
            }
        }
    }

}
