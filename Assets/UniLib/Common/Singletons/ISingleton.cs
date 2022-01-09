using System;

namespace UniLib.Common.Singletons
{
    /// <summary>
    /// シングルトンのインターフェース
    /// </summary>
    public interface ISingleton
    {
        /// <summary>
        /// シングルトン登録
        /// </summary>
        void RegisterInstance();
        /// <summary>
        /// シングルトン削除
        /// </summary>
        void RemoveInstance();
    }
}
