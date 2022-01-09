///
///  @説明 駒のbaseクラスモデルクラス
///

using Mock.Core.Common;
using Mock.Scripts.Enum;

namespace Mock.InGame.Parts.Piece
{
    public class PieceBaseModel : ModelBase
    {
        /// <summary>
        /// 移動できる範囲
        /// </summary>
        public int[] MoveRange { get; private set; } = new int[5 * 5];

        /// <summary>
        /// 駒種類
        /// </summary>
        public CellState.PieceDataState pieceState;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PieceBaseModel()
        {
        }

        /// <summary>
        /// データセット
        /// </summary>
        public virtual void SetData(int[] moveData, CellState.PieceDataState pieceData)
        {
            MoveRange = moveData;
            pieceState = pieceData;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}