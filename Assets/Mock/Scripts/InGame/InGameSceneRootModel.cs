///
///　クラス説明 インゲームシーンルートモデル
/// 

using System.Collections.Generic;
using Mock.Core.Common;
using Mock.Scripts.Const;
using Mock.Scripts.InGame.Parts;

namespace Mock.Scripts.InGame
{
    public class InGameSceneRootModel : ModelBase
    {
        /// <summary>
        /// 盤面のデータ配列
        /// </summary>
        public List<GridCellData> GridCellDataList = new List<GridCellData>();

        /// <summary>
        /// 駒のインデックスリスト
        /// </summary>
        /// <returns></returns>
        public List<uint> PieceIndexList { get; private set; } = new List<uint>();

        /// <summary>
        /// 選択した駒のインデックス
        /// </summary>
        public int SelectPieceIndex { get; private set; }

        public override void Initialize()
        {
            for (int i = 0; i < CellConst.GridNum; i++)
            {
                GridCellDataList.Add(new GridCellData(i));
            }
        }

        /// <summary>
        /// 駒のインデックスリスト_代入
        /// </summary>
        public void SetPieceIndexList(List<uint> list)
        {
            PieceIndexList.AddRange(list);
        }

        /// <summary>
        /// 選択した駒のインデックスをセット
        /// </summary>
        public void SetSelectPieceIndex(int index)
        {
            SelectPieceIndex = index;
        }

        /// <summary>
        /// 駒をを移動させる
        /// </summary>
        public void MovePieceIndex(int index)
        {
            PieceIndexList[index] = PieceIndexList[SelectPieceIndex];
            PieceIndexList[SelectPieceIndex] = 0;
        }
    }
}