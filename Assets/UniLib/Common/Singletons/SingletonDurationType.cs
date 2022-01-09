using System;

namespace UniLib.Common.Singletons
{
    //Attibute
    // はじめから生成
    // 参照したときに生成
    
    // In Property
    // シーン遷移時に破棄 ×
    // 破棄命令したときに破棄
    // 永続化
    
    /// <summary>
    /// シングルトンの生存期間
    /// </summary>
    public enum CustomExistenceDuration
    {
        /// <summary>
        /// シーン内限定
        /// </summary>
        InScene,
        /// <summary>
        /// アプリ内で永続化(削除不可能)
        /// </summary>
        InAppPersistence,
    }
}
