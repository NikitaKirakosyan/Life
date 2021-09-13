/*
 * author : Kirakosyan Nikita
 * e-mail : nikita.kirakosyan.work@gmail.com
 */
using System.Collections.Generic;
using UnityEngine;
using NikitaKirakosyan.Managers;

namespace NikitaKirakosyan.Generators
{
    public sealed class CellGenerator : MonoBehaviour
    {
        public Transform root = null;
        [SerializeField] private Cell _cellPrefab = null;

        [Header("Generation Settings")]
        [SerializeField] private int _cellAmount = 64;
        [SerializeField] private int _cellAliveAmount = 32;

        public List<Cell> cells { get; private set; } = new List<Cell>(0);

        #region Unity Methods
        private void Awake()
        {
            GenerateCells();
            GameManager.Instance.cells = cells;
            MarkAsAliveRandomCells();

            Destroy(gameObject);
        }
        #endregion

        #region Public Methods
        public void GenerateCells()
        {
            if (root == null)
            {
                root = new GameObject("Root").transform;
            }

            GameManager.Instance.cellMatrix = new Cell[_cellAmount / 2, _cellAmount / 2];
            int xM = 0;
            int yM = 0;

            int counter = 1;
            int cellAmount = (int)Mathf.Sqrt(_cellAmount);
            for (int y = cellAmount / 2; y > -cellAmount / 2; y--)
            {
                for (int x = -cellAmount / 2; x < cellAmount / 2; x++)
                {
                    var newCell = Instantiate(_cellPrefab, new Vector2(x, y), Quaternion.identity);
                    newCell.transform.SetParent(root);
                    newCell.name = _cellPrefab.name + $" ({counter})";

                    cells.Add(newCell);
                    GameManager.Instance.cellMatrix[xM, yM] = newCell;
                    newCell.xArrayPosition = xM;
                    newCell.yArrayPosition = yM;

                    counter++;
                    xM++;
                }

                xM = 0;
                yM++;
            }
        }
        #endregion

        #region Private Methods
        private void MarkAsAliveRandomCells()
        {
            var cells = new List<Cell>(this.cells);

            for (int i = 0; i < _cellAliveAmount; i++)
            {
                var index = Random.Range(0, cells.Count);
                cells[index].MarkAsAlive();
                cells.RemoveAt(index);
            }
        }
        #endregion
    }
}