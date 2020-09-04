using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KyleConibear
{
    using static Logger;
    public sealed class LevelManager : MonoBehaviour
    {
        #region Fields
        [Header("Debugging")]
        public bool isLogging = false;
        public Color debugLineColour = Color.cyan;
        public float debugLineLife = 3.0f;

        [Header("General")]        
        [SerializeField] private LevelUI levelUI = null;
        public LevelUI LevelUI
        {
            get
            {
                if (levelUI != null)
                {
                    return this.levelUI;
                }
                else
                {
                    levelUI = GameObject.FindObjectOfType<LevelUI>();

                    if (levelUI == null)
                    {
                        Logger.Log(Type.Error, $"LevelUI is {levelUI}.\n(Link in inspector.)");
                    }

                    return levelUI;
                }
            }
        }

        [SerializeField] private Player player = null;
        #endregion

        public static Action<string, LevelManager> On_LevelLoaded;

        [SerializeField] private Vector4 martianSpawnArea = Vector4.zero;

        private void Awake()
        {
            if (On_LevelLoaded != null)
            {
                On_LevelLoaded.Invoke(SceneManager.GetActiveScene().name, this);
            }
            else
            {
                Logger.Log(Type.Warning, $"On_LevelLoaded Action is null.");
            }
        }

        private void Start()
        {
            this.martianSpawnArea.DrawBounds(this.debugLineColour, this.debugLineLife);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float x = UnityEngine.Random.Range(martianSpawnArea.x, martianSpawnArea.y);
            float y = UnityEngine.Random.Range(martianSpawnArea.z, martianSpawnArea.w);

            return new Vector2(x, y);
        }
    }
}