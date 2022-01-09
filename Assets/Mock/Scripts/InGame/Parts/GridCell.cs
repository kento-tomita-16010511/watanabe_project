///
/// クラス説明 盤面
/// 

using UnityEngine;
using UnityEngine.UI;

namespace Mock.Scripts.InGame.Parts
{
    public class GridCell : MonoBehaviour
    {
        /// <summary>
        /// 盤面のイメージ
        /// </summary>
        [SerializeField] private Image _image;

        /// <summary>
        /// 移動範囲
        /// </summary>
        [SerializeField] private Image _movedImage;

        /// <summary>
        /// 盤面のイメージ
        /// </summary>
        [SerializeField] private Button _button;

        public Button Button => _button;

        /// <summary>
        /// データ
        /// </summary>
        private GridCellData _data;

        public int Index => _data.Index;

        public bool OnMoved => _movedImage.IsActive();

        /// <summary>
        /// データセット
        /// </summary>
        public void SetData(GridCellData data)
        {
            _data = data;
            SetMoveActiveCell(false);
        }

        /// <summary>
        /// 動ける範囲を見せる
        /// </summary>
        public void SetMoveActiveCell(bool isActive)
        {
            _movedImage.gameObject.SetActive(isActive);
        }
    }
}