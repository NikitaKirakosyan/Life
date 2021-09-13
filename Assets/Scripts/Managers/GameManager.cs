/*
 * author : Kirakosyan Nikita
 * e-mail : nikita.kirakosyan.work@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NikitaKirakosyan.Patterns;

namespace NikitaKirakosyan.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public List<Cell> cells { get; set; } = new List<Cell>(0);
        public Cell[,] cellMatrix { get; set; } = new Cell[0, 0];

        [Header("Simulation Settings")]
        public float tickInSeconds = 0.32f;

        #region Unity Methods
        private void Start()
        {
            StartCoroutine(OnSimulate());
        }
        #endregion

        #region IEnumerators
        private IEnumerator OnSimulate()
        {
            yield return new WaitForSeconds(tickInSeconds);

            foreach (var cell in cells)
            {
                cell.Simulate();
            }

            StartCoroutine(OnSimulate());
        }
        #endregion
    }
}