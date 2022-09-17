using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TankGame
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private int killScore;
        [SerializeField] private TextMeshProUGUI score;

        private int totalScore;
        private const string defaultText = "Score: ";

        private void OnEnable()
        {
            EnemyTankView.OnEnemyDeath += UpdateScore;
        }
        void Start()
        {
            totalScore = 0;
            score.text = defaultText + totalScore;
        }

        private void UpdateScore()
        {
            totalScore += killScore;
            score.text = defaultText + totalScore;
        }

        private void OnDisable()
        {
            EnemyTankView.OnEnemyDeath -= UpdateScore;
        }


    }
}
