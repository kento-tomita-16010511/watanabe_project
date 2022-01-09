///
///  @説明 チェスの駒クラスビュークラス
///

using UnityEngine;
using System.Collections;
using Mock.Core.Common;
using Mock.Scripts.Enum;

namespace Mock.InGame.Parts.Piece.Shogi
{
    public class ChessPieceView : PieceBaseView
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        
        /// <summary>
        /// スプライト名前
        /// </summary>
        protected override string SpriteName(CellState.PieceDataState　pieceData)
        {
            string str = $"chess_piece_{(char)pieceData}";
            return str;
        }
    }
}
