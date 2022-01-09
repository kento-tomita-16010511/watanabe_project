///
///  @説明 駒のbaseクラスビュークラス
///

using Mock.Core;
using Mock.Core.Common;
using Mock.Scripts.Enum;
using UnityEngine;

namespace Mock.InGame.Parts.Piece
{
    public class PieceBaseView : ViewBase
    {
        /// <summary>
        /// 画像
        /// </summary>
        [SerializeField] private AtlasImage _atlasImage;

        
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// 描画更新
        /// </summary>
        public virtual void UpdateView(CellState.PieceDataState　pieceData)
        {
            _atlasImage.spriteName = SpriteName(pieceData);
        }

        /// <summary>
        /// スプライト名前
        /// </summary>
        protected virtual string SpriteName(CellState.PieceDataState　pieceData)
        {
            return $"shogi_{(int)pieceData:D2}";
        }

    }
}
