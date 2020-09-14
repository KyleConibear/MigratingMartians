using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KyleConibear
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText = null;
        
        [SerializeField] private Image playerHealthImage = null;

        private void OnEnable()
        {
            this.UpdateScoreText();
        }

        public void UpdateScoreText()
        {
            this.scoreText.text = SaveData.score.ToString();
        }

        /// <summary>
        /// Update the player health bar image by reducing its localScale on the 'x' axis.
        /// </summary>
        /// <param name="percentRemaining">The percentage of player health remaining.</param>
        public void UpdatePlayerHealth(float percentRemaining)
        {
            this.playerHealthImage.rectTransform.localScale = new Vector3(percentRemaining, 1, 1);
        }
    }
}