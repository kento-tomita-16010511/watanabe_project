using UnityEngine;

namespace Mock.Core
{
    /// <summary>
    /// プロジェクト全般の初期化を行うクラス
    /// </summary>
    public static class ProjectInitializer
    {
        /// <summary>
        /// システム系の初期化(起動直後によばれる)
        /// NOTE:
        ///  初回シーン、マネージャークラス、サウンド、リソース、ローカライゼーション…etc
        ///  など、ゲームに必要な要素を起動直後に呼ぶ
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
        }

        /// <summary>
        /// 再起動する
        /// </summary>
        public static void Reboot()
        {
            //もういちどシステム系の初期化を呼んでおく
            Initialize();
        }
    }
}