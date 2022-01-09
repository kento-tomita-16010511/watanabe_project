using System;
using System.Collections.Generic;

namespace UniLib.Common.Singletons
{
    /// <summary>
    /// シングルトン制御クラス
    /// </summary>
    public static class SingletonController
    {
        /// <summary>
        /// インスタンスリストキャッシュ
        /// </summary>
        private static readonly List<ISingleton> _singletonInstanceList = new List<ISingleton>();

        /// <summary>
        /// シングルトンが存在するかどうか
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal static bool ContainsInstanceList(ISingleton instance)
        {
            return _singletonInstanceList.Contains(instance);
        }

        /// <summary>
        /// シングルトンに追加
        /// </summary>
        internal static void AddInstanceList(ISingleton instance)
        {
            _singletonInstanceList.Add(instance);
        }
        
        /// <summary>
        /// シングルトンから除去
        /// </summary>
        internal static void RemoveInstanceList(ISingleton instance)
        {
            _singletonInstanceList.Remove(instance);
        }

        /// <summary>
        /// シングルトンをすべて削除する
        /// </summary>
        /// <param name="ignoreSingletonTypes"></param>
        public static void RemoveAllSingleton(params ISingleton[] ignoreSingletonTypes)
        {
            for (var index = _singletonInstanceList.Count - 1; index >= 0; index--)
            {
                _singletonInstanceList[index].RemoveInstance();
            }
        }
    }
}
