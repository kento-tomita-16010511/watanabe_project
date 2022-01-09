///
///  @説明 駒のbaseクラスプレゼンタークラス
///

using Mock.Core.Common;
using Mock.Scripts.Enum;
using Mock.Scripts.Utility;
using UnityEngine;

namespace Mock.InGame.Parts.Piece
{
    public class PieceBasePresenter<M, V> : PresenterBase<M, V>
        where M : PieceBaseModel, new()
        where V : PieceBaseView
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

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(CellState.PieceDataState pieceData)
        {
            var moveData = InGameUtility.GetPieceMoveData(pieceData);
            Model.SetData(moveData, pieceData);
            View.UpdateView(pieceData);
        }
        
        /// <summary>
        /// 動かす
        /// </summary>
        public void Move(Transform parent)
        {
            if (!(gameObject.transform is RectTransform { } rect)) return;
            rect.parent = parent;
            rect.localPosition = Vector3.zero;
        }
    }
}