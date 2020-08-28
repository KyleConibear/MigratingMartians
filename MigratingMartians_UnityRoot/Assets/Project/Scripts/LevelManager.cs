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

        [Header("Level Progression")]
        #endregion

        public static Action<string, LevelManager> On_LevelLoaded;

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

    }
}