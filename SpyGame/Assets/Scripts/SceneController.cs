
namespace SpyGame
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;


    public class SceneController : MonoBehaviour
    {
        public const string LevelPrefix = "MissionLevel";
        private static SceneController sceneController;

        private int currentSceneNumber;
        private string currentSceneName;
        private bool reload;
        private string nextSceneName;
        private AsyncOperation resourceUnloadTask;
        private AsyncOperation sceneLoadTask;
        private enum SceneState
        {
            Reset, Preload, Load, Unload, Postload, Ready, Run, Count
        };
        private SceneState sceneState;
        private delegate void UpdateDelegate();
        private UpdateDelegate[] updateDelegates;

        public static Game Game
        { get; private set; }

        //--------------------------------------------------------------------------
        // public static methods
        //--------------------------------------------------------------------------

        public static void Reload()
        {
            if (sceneController != null)
                sceneController.reload = true;

        }

        public static void SwitchScene(int level)
        {
            if (sceneController != null)
            {
                sceneController.currentSceneNumber = level;
                SwitchScene(LevelPrefix + level.ToString("00"));
            }
        }

        public static void SwitchScene(string nextSceneName)
        {
            if (sceneController != null)
            {
                if (sceneController.currentSceneName != nextSceneName)
                {
                    sceneController.nextSceneName = nextSceneName;
                }
            }
        }

        public static string GetCurrentLevelName()
        {
            if (sceneController != null)
            {
                return sceneController.currentSceneName;
            }
            return "";
        }

        public static int GetCurrentLevelNumber()
        {
            if (sceneController != null)
            {
                return sceneController.currentSceneNumber;
            }
            return 0;
        }
        //--------------------------------------------------------------------------
        // protected mono methods
        //--------------------------------------------------------------------------
        protected void Awake()
        {
            //Let's keep this alive between scene changes
            Object.DontDestroyOnLoad(gameObject);

            //Setup the singleton instance
            sceneController = this;
            Game = new Game();

            //Setup the array of updateDelegates
            updateDelegates = new UpdateDelegate[(int)SceneState.Count];

            //Set each updateDelegate
            updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
            updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
            updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
            updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
            updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
            updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
            updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

            nextSceneName = "MenuScene";
            sceneState = SceneState.Reset;
            //			GetComponent<Camera>().orthographicSize = Screen.height/2;
        }

        protected void OnDestroy()
        {
            //Clean up all the updateDelegates
            if (updateDelegates != null)
            {
                for (int i = 0; i < (int)SceneState.Count; i++)
                {
                    updateDelegates[i] = null;
                }
                updateDelegates = null;
            }

            //Clean up the singleton instance
            if (sceneController != null)
            {
                sceneController = null;
            }
        }

        protected void OnDisable()
        {
        }

        protected void OnEnable()
        {
        }

        protected void Start()
        {
        }

        protected void Update()
        {
            if (updateDelegates[(int)sceneState] != null)
            {
                updateDelegates[(int)sceneState]();
            }
        }

        //--------------------------------------------------------------------------
        // private methods
        //--------------------------------------------------------------------------

        /// <summary>
        /// Attach the new scene controller to start cascade of loading
        /// </summary>
        private void UpdateSceneReset()
        {
            // run a gc pass
            System.GC.Collect();
            sceneState = SceneState.Preload;
        }

        /// <summary>
        /// Handle anything that needs to happen before loading
        /// </summary>
        private void UpdateScenePreload()
        {
            sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
            sceneState = SceneState.Load;
        }

        /// <summary>
        /// Show the loading screen until it's loaded
        /// </summary>
        private void UpdateSceneLoad()
        {
            // done loading?
            if (sceneLoadTask.isDone == true)
            {
                sceneState = SceneState.Unload;
            } else
            {
                // update scene loading progress
            }
        }

        /// <summary>
        /// Clean up unused resources by unloading them
        /// </summary>
        private void UpdateSceneUnload()
        {
            // cleaning up resources yet?
            if (resourceUnloadTask == null)
            {
                resourceUnloadTask = Resources.UnloadUnusedAssets();
            } else
            {
                // done cleaning up?
                if (resourceUnloadTask.isDone == true)
                {
                    resourceUnloadTask = null;
                    sceneState = SceneState.Postload;
                }
            }
        }

        /// <summary>
        /// Handle anything that needs to happen immediately after loading
        /// </summary>
        private void UpdateScenePostload()
        {
            currentSceneName = nextSceneName;
            sceneState = SceneState.Ready;
        }

        /// <summary>
        /// Handle anything that needs to happen immediately before running
        /// </summary>
        private void UpdateSceneReady()
        {
            // run a gc pass
            // if you have assets loaded in the scene that are unused currently but 
            // may be used later DON'T do this here
            // System.GC.Collect();
            sceneState = SceneState.Run;
        }

        /// <summary>
        /// Wait for scene change
        /// </summary>
        private void UpdateSceneRun()
        {
            if (currentSceneName != nextSceneName || reload)
            {
                sceneState = SceneState.Reset;
                reload = false;
            }
        }
    }
}