/*
 * author : Kirakosyan Nikita
 * e-mail : nikita.kirakosyan.work@gmail.com
 */
using System.Collections.Generic;
using UnityEngine;
using NikitaKirakosyan.Managers;

namespace NikitaKirakosyan
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cell : MonoBehaviour
    {
        public bool willLive { get; private set; } = false;
        public bool willDie { get; private set; } = false;

        public bool isAlive { get; private set; } = false;
        public int xArrayPosition { get; set; } = 0;
        public int yArrayPosition { get; set; } = 0;

        [SerializeField] private Color _aliveColor = Color.green;
        [SerializeField] private Color _deathColor = Color.white;

        public SpriteRenderer spriteRenderer { get; private set; } = null;

        #region Unity Methods
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Public Methods
        public void MarkAsAlive()
        {
            isAlive = true;
            UpdateSpriteRendererColor();
        }

        public void MarkAsDeath()
        {
            isAlive = false;
            UpdateSpriteRendererColor();
        }

        public void Simulate()
        {
            if (willLive == true)
            {
                MarkAsAlive();
                willLive = false;
            }
            else if (willDie == true)
            {
                MarkAsDeath();
                willDie = false;
            }

            var aliveNeighbourAmount = GetAliveNeighbourAmount();

            if (aliveNeighbourAmount == 3)
            {
                willLive = true;
            }
            else if (aliveNeighbourAmount < 2 || aliveNeighbourAmount > 3)
            {
                willDie = true;
            }
        }
        #endregion

        #region Private Methods
        private void UpdateSpriteRendererColor()
        {
            spriteRenderer.color = isAlive ? _aliveColor : _deathColor;
        }

        private int GetAliveNeighbourAmount()
        {
            int aliveNeighbourAmount = 0;

            var cellMatrix = GameManager.Instance.cellMatrix;

            #region Neightbours
            var neighbours = new List<Cell>();

            if (xArrayPosition - 1 >= 0 && yArrayPosition + 1 < cellMatrix.GetLength(1))
            {
                var leftBottomCell = cellMatrix[xArrayPosition - 1, yArrayPosition + 1];
                neighbours.Add(leftBottomCell);
            }

            if (xArrayPosition - 1 >= 0)
            {
                var leftCell = cellMatrix[xArrayPosition - 1, yArrayPosition];
                neighbours.Add(leftCell);
            }

            if (xArrayPosition - 1 >= 0 && yArrayPosition - 1 >= 0)
            {
                var leftTopCell = cellMatrix[xArrayPosition - 1, yArrayPosition - 1];
                neighbours.Add(leftTopCell);
            }

            if (yArrayPosition - 1 >= 0)
            {
                var topCell = cellMatrix[xArrayPosition, yArrayPosition - 1];
                neighbours.Add(topCell);
            }

            if (xArrayPosition + 1 < cellMatrix.GetLength(0) && yArrayPosition - 1 >= 0)
            {
                var rightTopCell = cellMatrix[xArrayPosition + 1, yArrayPosition - 1];
                neighbours.Add(rightTopCell);
            }

            if (xArrayPosition + 1 < cellMatrix.GetLength(0))
            {
                var rightCell = cellMatrix[xArrayPosition + 1, yArrayPosition];
                neighbours.Add(rightCell);
            }

            if (xArrayPosition + 1 < cellMatrix.GetLength(0) && yArrayPosition + 1 < cellMatrix.GetLength(1))
            {
                var rightBottomCell = cellMatrix[xArrayPosition + 1, yArrayPosition + 1];
                neighbours.Add(rightBottomCell);
            }

            if (yArrayPosition + 1 < cellMatrix.GetLength(1))
            {
                var bottomCell = cellMatrix[xArrayPosition, yArrayPosition + 1];
                neighbours.Add(bottomCell);
            }
            #endregion

            foreach (var neighbour in neighbours)
            {
                if (neighbour != null && neighbour.isAlive)
                {
                    aliveNeighbourAmount++;
                }
            }

            return aliveNeighbourAmount;
        }
        #endregion
    }
}