namespace Mock.Scripts.Enum
{
    /// <summary>
    /// 盤面のenum
    /// </summary>
    public class CellState
    {
        /// <summary>
        /// 盤面の進む方向のステータス
        /// </summary>
        public enum MoveNumState
        {
            None = 0, //none
            Start = 1, //自分の位置
            Step = 2, //一歩進める
            Run  = 3, //可能な範囲どこまでも進める
            AttackStep = 4, //攻撃できるときのみ動ける_pawnなど
            FirstStep = 5,　//初手のみ移動できる
        }
        
        /// <summary>
        /// 将棋かchessかEnum
        /// </summary>
        public enum PieceTypeState
        {
            None = 0, //none
            Shogi = 1,　//将棋
            Chess = 2, //チェス
        }
        
        /// <summary>
        /// 盤面で使うデータ＿
        /// </summary>
        public enum PieceDataState
        {
            //将棋
            Ou =  01,
            Hisya = 02,
            Kaku = 03,
            Kin = 04,
            Gin = 05,
            Kei =  06,
            Kyousya = 07,
            Huhyou = 08,
            Ryuu = 09,
            Uma = 10,
            Gin_ura = 11,
            Kei_ura = 12,
            kyousya_ura = 13,
            huhyou_ura = 14,
            
            //チェス
            Pawn = 'a',
            Knight = 'b',
            Bishop = 'c',
            Rook = 'd',
            Queen = 'e',
            King = 'f',
        }
        
        //将棋
        #region Shogi

        #endregion //将棋
    }

}