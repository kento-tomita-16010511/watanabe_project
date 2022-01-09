///
/// クラス説明  MVPのBehavior継承プレゼンター
/// 

using UnityEngine;

namespace Mock.Core.Common
{
    public abstract class BehaviorPresenterBase : MonoBehaviour, IInitializable
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            InitializeFields();

            Bind();
            SetEvents();

            OnInitialized();
        }

        /// <summary>
        /// フィールド変数を初期化する
        /// </summary>
        protected abstract void InitializeFields();

        /// <summary>
        /// 初期化完了後に呼ばれる
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        /// <summary>
        /// Modelの値の変更を監視する
        /// </summary>
        protected abstract void Bind();

        /// <summary>
        /// Viewのイベントの設定を行う
        /// </summary>
        protected abstract void SetEvents();
    }
}