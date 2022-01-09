///
///　クラス説明 インゲームシーンプレゼンター
/// 

using System.Collections.Generic;
using Mock.Core.Common;
using Mock.InGame.Parts.Piece.Shogi;
using Mock.Scripts.Const;
using Mock.Scripts.Enum;
using Mock.Scripts.Utility;
using UniRx;

namespace Mock.Scripts.InGame
{
    public class InGameSceneRootPresenter : PresenterBase<InGameSceneRootModel, InGameSceneRootView>
    {
        /// <summary>
        /// フィールド変数を初期化する
        /// </summary>
        protected override void InitializeFields()
        {
            base.InitializeFields();
            View.CreateGrid(Model.GridCellDataList);

            //通信で変えたりNPCで変えたりする　TODO
            CreatePiece(CellState.PieceTypeState.Shogi, CellState.PieceTypeState.Chess);
        }

        protected override void Bind()
        {
        }

        protected override void SetEvents()
        {
            foreach (var cell in View.GridCells)
            {
                //セルを押したとき 動かす
                cell.Button.onClick.AsObservable().Subscribe(it =>
                {
                    var shogi = cell.GetComponentInChildren<ShogiPiecePresenter>();
                    if (shogi != null)
                    {
                        bool[] actives = InGameUtility.GetPieceMoveActive(Model.PieceIndexList, shogi.Model.MoveRange,
                            cell.Index, CellState.PieceTypeState.Shogi);
                        View.SetMoveActive(actives);
                        Model.SetSelectPieceIndex(cell.Index);
                    }


                    var chess = cell.GetComponentInChildren<ChessPiecePresenter>();
                    if (chess != null)
                    {
                        bool[] actives = InGameUtility.GetPieceMoveActive(Model.PieceIndexList, chess.Model.MoveRange,
                            cell.Index, CellState.PieceTypeState.Chess);
                        View.SetMoveActive(actives);
                        Model.SetSelectPieceIndex(cell.Index);
                    }

                    //動かす場合
                    if (cell.OnMoved)
                    {
                        PieceMove(cell.Index);
                    }
                }).AddTo(this);
            }
        }

        /// <summary>
        /// 駒を動かす
        /// </summary>
        public void PieceMove(int moveIndex)
        {
            var moveCell = View.GridCells[Model.SelectPieceIndex];
            var moveShogi = moveCell.GetComponentInChildren<ShogiPiecePresenter>();
            if (moveShogi != null)
            {
                moveShogi.Move(View.GridCells[moveIndex].transform);
            }

            var moveChess = moveCell.GetComponentInChildren<ChessPiecePresenter>();
            if (moveChess != null)
            {
                moveChess.Move(View.GridCells[moveIndex].transform);
            }

            Model.MovePieceIndex(moveIndex);

            //選択範囲初期化
            View.SetMoveActive(new bool[CellConst.GridNum]);
        }

        /// <summary>
        /// 駒作成
        /// </summary>
        private void CreatePiece(CellState.PieceTypeState youState, CellState.PieceTypeState opponentState)
        {
            List<uint> firstCellGrid = new List<uint>();

            uint[] youGrid;

            //自分の盤面代入
            youGrid = youState == CellState.PieceTypeState.Shogi
                ? CellConst.FirstCellShogiGrid
                : CellConst.FirstCellChessGrid;
            firstCellGrid.AddRange(youGrid);
            uint[] opponentGrid;
            //相手の盤面代入
            opponentGrid = opponentState == CellState.PieceTypeState.Shogi
                ? CellConst.FirstCellShogiGrid
                : CellConst.FirstCellChessGrid;

            for (int i = 0; i < opponentGrid.Length; i++)
            {
                if (opponentGrid[i] != 0)
                {
                    firstCellGrid[firstCellGrid.Count - 1 - i] = opponentGrid[i];
                }
            }

            //データ反映
            Model.SetPieceIndexList(firstCellGrid);

            //pieceの配置
            for (int i = 0; i < firstCellGrid.Count; i++)
            {
                //idがある場合作成
                if (firstCellGrid[i] != 0)
                {
                    //pawnより値が小さい場合
                    if (firstCellGrid[i] < (uint) CellState.PieceDataState.Pawn)
                    {
                        var piece = Instantiate(View.ShogiPieceBase, View.GridCells[i].transform, false);
                        piece.Init((CellState.PieceDataState) firstCellGrid[i]);
                    }
                    else
                    {
                        var piece = Instantiate(View.ChessPieceBase, View.GridCells[i].transform, false);
                        piece.Init((CellState.PieceDataState) firstCellGrid[i]);
                    }
                }
            }
        }
    }
}