///
/// クラス説明 盤面データ
/// 

namespace Mock.Scripts.InGame.Parts
{
    public class GridCellData
    {
        /// <summary>
        /// 自分の配列位置
        /// </summary>
        public int Index { get; private set; }
        
        public GridCellData(int index)
        {
            Index = index;
        }
    }
}