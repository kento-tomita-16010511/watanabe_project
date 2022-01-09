using System.Collections.Generic;
using Mock.Scripts.Enum;

namespace Mock.Scripts.Const
{
    public class CellConst
    {
        public static readonly int GridX = 9;
        public static readonly int GridY = 9;
   
        //盤面
        public static readonly int GridNum = GridX * GridY;

        //初期盤面_将棋
        public static readonly uint[] FirstCellShogiGrid =
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            08, 08, 08, 08, 08, 08, 08, 08, 08,
            0, 03, 0, 0, 0, 0, 0, 02, 0,
            07, 06, 05, 04, 01, 04, 05, 06, 07
        };
        
        //初期盤面_chess
        public static readonly uint[] FirstCellChessGrid =
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a',      
            'd', 'b', 'c', 'f', 0, 'e', 'c', 'b', 'd',
        };

        #region shogi

        //駒種類
        //歩兵
        public static readonly int[] Shogi_fuhyou =
        {
            0, 0, 0, 0, 0,
            0, 0, 2, 0, 0,
            0, 0, 1, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
        };

        //香車
        public static readonly int[] Shogi_kyousya =
        {
            0, 0, 0, 0, 0,
            0, 0, 3, 0, 0,
            0, 0, 1, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
        };

        //桂
        public static readonly int[] Shogi_kei =
        {
            0, 2, 0, 2, 0,
            0, 0, 0, 0, 0,
            0, 0, 1, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
        };

        //銀
        public static readonly int[] Shogi_gin =
        {
            0, 0, 0, 0, 0,
            0, 2, 2, 2, 0,
            0, 0, 1, 0, 0,
            0, 2, 0, 2, 0,
            0, 0, 0, 0, 0,
        };

        //金
        public static readonly int[] Shogi_kin =
        {
            0, 0, 0, 0, 0,
            0, 2, 2, 2, 0,
            0, 2, 1, 2, 0,
            0, 0, 2, 0, 0,
            0, 0, 0, 0, 0,
        };

        //王
        public static readonly int[] Shogi_ou =
        {
            0, 0, 0, 0, 0,
            0, 2, 2, 2, 0,
            0, 2, 1, 2, 0,
            0, 2, 2, 2, 0,
            0, 0, 0, 0, 0,
        };

        //飛車
        public static readonly int[] Shogi_hisya =
        {
            0, 0, 0, 0, 0,
            0, 0, 3, 0, 0,
            0, 3, 1, 3, 0,
            0, 0, 3, 0, 0,
            0, 0, 0, 0, 0,
        };

        //角
        public static readonly int[] Shogi_kaku =
        {
            0, 0, 0, 0, 0,
            0, 3, 0, 3, 0,
            0, 0, 1, 0, 0,
            0, 3, 0, 3, 0,
            0, 0, 0, 0, 0,
        };

        //龍
        public static readonly int[] Shogi_ryuu =
        {
            0, 0, 0, 0, 0,
            0, 2, 3, 2, 0,
            0, 3, 1, 3, 0,
            0, 2, 3, 2, 0,
            0, 0, 0, 0, 0,
        };

        //馬
        public static readonly int[] Shogi_uma =
        {
            0, 0, 0, 0, 0,
            0, 3, 2, 3, 0,
            0, 2, 1, 2, 0,
            0, 3, 2, 3, 0,
            0, 0, 0, 0, 0,
        };

        #endregion //将棋

        #region chess //駒種類

        //pawn
        public static readonly int[] Chess_pawn =
        {
            0, 0, 5, 0, 0,
            0, 4, 2, 4, 0,
            0, 0, 1, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
        };

        //knight
        public static readonly int[] Chess_knight =
        {
            0, 2, 0, 2, 0,
            2, 0, 0, 0, 2,
            0, 0, 1, 0, 0,
            2, 0, 0, 0, 2,
            0, 2, 0, 2, 0,
        };


        //bishop
        public static readonly int[] Chess_bishop =
        {
            0, 0, 0, 0, 0,
            0, 3, 0, 3, 0,
            0, 0, 1, 0, 0,
            0, 3, 0, 3, 0,
            0, 0, 0, 0, 0,
        };

        //rook
        public static readonly int[] Chess_rook =
        {
            0, 0, 0, 0, 0,
            0, 0, 3, 0, 0,
            0, 3, 1, 3, 0,
            0, 0, 3, 0, 0,
            0, 0, 0, 0, 0,
        };

        //queen
        public static readonly int[] Chess_queen =
        {
            0, 0, 0, 0, 0,
            0, 3, 3, 3, 0,
            0, 3, 1, 3, 0,
            0, 3, 3, 3, 0,
            0, 0, 0, 0, 0,
        };

        //king
        public static readonly int[] Chess_king =
        {
            0, 0, 0, 0, 0,
            0, 2, 2, 2, 0,
            0, 2, 1, 2, 0,
            0, 2, 2, 2, 0,
            0, 0, 0, 0, 0,
        };

        #endregion //チェス
    };
}