///
///　クラス説明 MVPbaseプレゼンター
///

using UnityEngine;

namespace Mock.Core.Common
{
    public abstract class PresenterBase<M, V> : BehaviorPresenterBase
        where M : ModelBase, new()
        where V : ViewBase
    {
        [SerializeField] private V _view;

        public V View => _view;

        private M _model;
        public M Model => _model;

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// フィールド変数を初期化する
        /// </summary>
        protected override void InitializeFields()
        {
            _model = new M();
            Model.Initialize();
            View.Initialize();
        }
    }
}