///
///  @説明 チェスの駒クラスプレゼンタークラス
///

using UnityEngine;
using System.Collections;
using Mock.Core.Common;

namespace Mock.InGame.Parts.Piece.Shogi
{
    public class ChessPiecePresenter : PieceBasePresenter<ChessPieceModel, ChessPieceView>
    {
        /// <summary>
        /// フィールド変数を初期化する
        /// </summary>
        protected override void InitializeFields()
        {
            base.InitializeFields(); //ベース初期化で使用(消さないでください)
        }

        /// <summary>
        /// 初期化完了後に呼ばれる
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        /// <summary>
        /// ViewとModelの紐付け
        /// </summary>
        protected override void Bind()
        {
        }

        /// <summary>
        /// Viewのイベントの設定を行う
        /// </summary>
        protected override void SetEvents()
        {
        }
    }
}
