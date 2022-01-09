using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Mock.Scripts.Const;
using Mock.Scripts.Enum;
using UnityEngine;

namespace Mock.Scripts.Utility
{
    /// <summary>
    /// インゲームUtility
    /// </summary>
    public class InGameUtility
    {
        /// <summary>
        ///　駒の動き取得
        /// </summary>
        /// <returns></returns>
        public static int[] GetPieceMoveData(CellState.PieceDataState pieceData)
        {
            switch (pieceData)
            {
                //王
                case CellState.PieceDataState.Ou:
                    return CellConst.Shogi_ou;
                //飛車
                case CellState.PieceDataState.Hisya:
                    return CellConst.Shogi_hisya;
                //角
                case CellState.PieceDataState.Kaku:
                    return CellConst.Shogi_kaku;
                //金
                case CellState.PieceDataState.Kin:
                    return CellConst.Shogi_kin;
                //銀
                case CellState.PieceDataState.Gin:
                    return CellConst.Shogi_gin;
                //桂
                case CellState.PieceDataState.Kei:
                    return CellConst.Shogi_kei;
                //香車
                case CellState.PieceDataState.Kyousya:
                    return CellConst.Shogi_kyousya;
                //歩兵
                case CellState.PieceDataState.Huhyou:
                    return CellConst.Shogi_fuhyou;
                //龍
                case CellState.PieceDataState.Ryuu:
                    return CellConst.Shogi_ryuu;
                //馬
                case CellState.PieceDataState.Uma:
                    return CellConst.Shogi_uma;
                //銀_桂_香車_歩_裏
                case CellState.PieceDataState.Gin_ura:
                case CellState.PieceDataState.Kei_ura:
                case CellState.PieceDataState.kyousya_ura:
                case CellState.PieceDataState.huhyou_ura:
                    return CellConst.Shogi_kin;
                //チェス
                case CellState.PieceDataState.Pawn:
                    return CellConst.Chess_pawn;
                case CellState.PieceDataState.Knight:
                    return CellConst.Chess_knight;
                case CellState.PieceDataState.Bishop:
                    return CellConst.Chess_bishop;
                case CellState.PieceDataState.Rook:
                    return CellConst.Chess_rook;
                case CellState.PieceDataState.Queen:
                    return CellConst.Chess_queen;
                case CellState.PieceDataState.King:
                    return CellConst.Chess_king;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pieceData), pieceData, null);
            }
        }

        public struct IndexData
        {
            public int Index { get; private set; }
            public uint Data { get; private set; }

            public IndexData(int index, uint data)
            {
                Index = index;
                Data = data;
            }
        }

        /// <summary>
        /// 移動できる範囲を返す
        /// </summary>
        /// <returns></returns>
        public static bool[] GetPieceMoveActive(List<uint> grid, int[] moveRange, int index,
            CellState.PieceTypeState state)
        {
            bool[] actives = new bool[grid.Count];
            var gridTowArray = ToTowDimensionalPrimitives(grid.ToArray(), 9, 9);

            int inY = index / 9; //indexのX
            int inX = index % 9; //indexのY
            List<Vector2> lineVec = new List<Vector2>();
            //moveRange内に設定されているデータで
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var moveR = ToTowDimensionalPrimitives(moveRange, 5, 5);
                    int moveY = y - (inY + 5 / 2) + (9 - 5);
                    int moveX = x - (inX + 5 / 2) + (9 - 5);
                    if (moveY > -1 && moveY < 5 &&
                        moveX > -1 && moveX < 5)
                    {
                        //仲間check
                        bool isActive = false;
                        switch (state)
                        {
                            case CellState.PieceTypeState.Shogi:
                                isActive = gridTowArray[y, x] == 0 ||
                                           (uint) CellState.PieceDataState.Pawn <= gridTowArray[y, x];
                                break;
                            case CellState.PieceTypeState.Chess:
                                isActive = gridTowArray[y, x] == 0 ||
                                           gridTowArray[y, x] < (uint) CellState.PieceDataState.Pawn;
                                break;
                        }

                        switch ((CellState.MoveNumState) moveR[moveY, moveX])
                        {
                            case CellState.MoveNumState.Start:
                                break;
                            case CellState.MoveNumState.Step:
                                actives[y * 9 + x] = isActive;
                                break;
                            case CellState.MoveNumState.Run:
                                actives[y * 9 + x] = isActive;
                                if (isActive)
                                {
                                    lineVec.Add(new Vector2(moveX - 2, moveY - 2));
                                }

                                break;
                            case CellState.MoveNumState.AttackStep:
                                actives[y * 9 + x] = isActive;
                                break;
                            case CellState.MoveNumState.FirstStep:
                                actives[y * 9 + x] = isActive;
                                break;
                        }
                    }
                }
            }


            //縦リスト
            List<IndexData> YDict = new List<IndexData>();
            for (int i = 0; i < 9; i++)
            {
                int j = index % 9 + i * 9;
                YDict.Add(new IndexData(j, grid[j]));
            }

            //横リスト
            List<IndexData> XDict = new List<IndexData>();
            for (int i = 0; i < 9; i++)
            {
                int x = index / 9 * 9;
                XDict.Add(new IndexData(x + i, grid[x + i]));
            }

            //斜めリスト_左
            List<IndexData> leftDiagDict = new List<IndexData>();
            for (int i = 0; i < 9; i++)
            {
                int y = index % 9 + i * 9;
                int x = index / 9 * 9;
                int diag = index / 9 - y / 9;

                int ix = y - diag;
                if (0 <= ix && ix < 81)
                {
                    leftDiagDict.Add(new IndexData(ix, grid[ix]));
                }
            }

            //斜めリスト_右
            List<IndexData> rightDiagDict = new List<IndexData>();
            for (int i = 0; i < 9; i++)
            {
                int y = index % 9 + i * 9;
                int x = index / 9 * 9;
                int diag = index / 9 - y / 9;

                int ix = y + diag;
                if (0 <= ix && ix < 81)
                {
                    rightDiagDict.Add(new IndexData(ix, grid[ix]));
                }
            }

            //縦横斜めあった場合動けるか決める
            //縦一直線判定_上
            if (lineVec.Any(it => it == Vector2.down))
            {
                var list = YDict.Where(it => it.Index < index).ToList();
                for (int i = list.Count() - 1; i >= 0; i--)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //縦一直線判定_下
            if (lineVec.Any(it => it == Vector2.up))
            {
                var list = YDict.Where(it => it.Index > index).ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //横一直線判定_左
            if (lineVec.Any(it => it == Vector2.left))
            {
                var list = XDict.Where(it => it.Index < index).ToList();
                for (int i = list.Count() - 1; i >= 0; i--)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //横一直線判定_右
            if (lineVec.Any(it => it == Vector2.right))
            {
                var list = XDict.Where(it => it.Index > index).ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //斜め一直線判定 左斜め_上
            if (lineVec.Any(it => it == new Vector2(-1, -1)))
            {
                var list = leftDiagDict.Where(it => it.Index < index).ToList();
                for (int i = list.Count() - 1; i >= 0; i--)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //斜め一直線判定 左斜め_下
            if (lineVec.Any(it => it == new Vector2(1, 1)))
            {
                var list = leftDiagDict.Where(it => it.Index > index).ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //斜め一直線判定 右斜め_上
            if (lineVec.Any(it => it == new Vector2(-1, 1)))
            {
                var list = rightDiagDict.Where(it => it.Index < index).ToList();
                for (int i = list.Count() - 1; i >= 0; i--)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            //斜め一直線判定 右斜め_下
            if (lineVec.Any(it => it == new Vector2(1, -1)))
            {
                var list = rightDiagDict.Where(it => it.Index > index).ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    int nowIndex = list[i].Index;
                    if (list[i].Data == 0)
                    {
                        actives[nowIndex] = true;
                    }
                    else
                    {
                        actives[nowIndex] = state != CellState.PieceTypeState.Shogi
                            ? list[i].Data <= (uint) CellState.PieceDataState.Pawn
                            : list[i].Data > (uint) CellState.PieceDataState.Pawn;
                        //ぶつかったらそれ以降は処理しない
                        break;
                    }
                }
            }

            return actives;
        }

        /// <summary>
        ///  組み込み型のみを対象に1次元配列を2次元配列に変換します。
        /// <para>T[height, width] 範囲を超える分は切り捨て、不足している分は(T)の初期値になります。</para>
        /// </summary>
        public static T[,] ToTowDimensionalPrimitives<T>(T[] src, int width, int height)
        {
            var dest = new T[height, width];
            int len = width * height;
            len = src.Length < len ? src.Length : len;

            var size = Marshal.SizeOf(typeof(T));
            Buffer.BlockCopy(src, 0, dest, 0, len * size);
            return dest;
        }

        /// <summary>
        /// 指定した2次元配列を1次元配列に変換します。
        /// <para>T[height, width] 範囲を超える分は切り捨て、不足している分は(T)の初期値になります。</para>
        /// </summary>
        public static T[,] ToTowDimensional<T>(T[] src, int width, int height)
        {
            var dest = new T[height, width];
            int len = width * height;
            len = src.Length < len ? src.Length : len;
            for (int y = 0, i = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++, i++)
                {
                    if (i >= len)
                    {
                        return dest;
                    }

                    dest[y, x] = src[i];
                }
            }

            return dest;
        }
    }
}