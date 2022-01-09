///
///　クラス説明 インゲームシーンルートビュー
/// 

using System.Collections.Generic;
using Mock.Core.Common;
using Mock.InGame.Parts.Piece.Shogi;
using Mock.Scripts.InGame.Parts;
using UnityEngine;

namespace Mock.Scripts.InGame
{
    public class InGameSceneRootView : ViewBase
    {
        /// <summary>
        /// 盤面親
        /// </summary>
        [SerializeField] private GameObject _gridRoot;

        /// <summary>
        /// 盤面複製元
        /// </summary>
        [SerializeField] private GridCell _gridCell;

        /// <summary>
        /// 盤面のすべて
        /// </summary>
        private List<GridCell> _gridCells = new List<GridCell>();

        public List<GridCell> GridCells => _gridCells;

        /// <summary>
        /// 将棋base
        /// </summary>
        [SerializeField] private ShogiPiecePresenter _shogiPieceBase;

        public ShogiPiecePresenter ShogiPieceBase => _shogiPieceBase;
        
        /// <summary>
        /// 将棋base
        /// </summary>
        [SerializeField] private ChessPiecePresenter _chessPieceBase;

        public ChessPiecePresenter ChessPieceBase => _chessPieceBase;
        
        /// <summary>
        /// 盤面作成
        /// </summary>
        public void CreateGrid(List<GridCellData> dataList)
        {
            foreach (var data in dataList)
            {
                var cell = Instantiate(_gridCell, _gridRoot.transform);
                cell.SetData(data);
                _gridCells.Add(cell);
            }

            _gridCell.gameObject.SetActive(false);
        }

        /// <summary>
        /// 動ける範囲有効か
        /// </summary>
        public void SetMoveActive(bool[] isActives)
        {
            for (int i = 0; i < isActives.Length; i++)
            {
                GridCells[i].SetMoveActiveCell(isActives[i]);
            }
        }
    }
}