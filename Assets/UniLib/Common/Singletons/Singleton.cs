using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniLib.Common.Singletons
{
    /// <summary>
    /// シングルトンクラス
    /// </summary>
    /// <typeparam name="TSingleton">シングルトンベース</typeparam>
    public abstract class Singleton<TSingleton> : ISingleton where TSingleton : Singleton<TSingleton>, new()
    {
        /// <summary>
        /// [Option] インスタンスをアプリ内で永続化するか
        /// </summary>
        protected virtual bool IsInAppPersistence => false;
        
        /// <summary>
        /// インスタンスキャッシュ
        /// </summary>
        private static TSingleton _instance;
        
        protected Singleton(){}
        
        /// <summary>
        /// インスタンスアクセス
        /// </summary>
        public static TSingleton Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var instance = CreateInstance();
                instance.RegisterInstance();
                
                Debug.Assert(_instance != null, $"[{typeof(TSingleton).Name}] インスタンスがセットされていません!");

                return _instance;
            }
        }

        /// <summary>
        /// インスタンス生成
        /// </summary>
        private static TSingleton CreateInstance()
        {
            var instance = new TSingleton();
            return instance;
        }

        /// <summary>
        /// シングルトン登録
        /// </summary>
        public void RegisterInstance()
        {
            var instance = this as TSingleton;
            
            //インスタンスキャスト失敗
            if (instance == null)
            {
                Debug.LogError($"[{typeof(TSingleton).Name}] 何らかの原因でキャストに失敗したため登録処理を中断");
                return;
            }
            
            //インスタンスが既に存在している
            if (_instance != null)
            {
                //自分
                if (_instance == instance)
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] 既にこのインスタンスが登録されているため処理を中断");
                }
                //2重生成
                else
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] 別のインスタンスを2重登録しようとしているため処理を中断");
                }
                
                return;
            }
            
            //既にインスタンスに登録済み
            if (SingletonController.ContainsInstanceList(instance))
            {
                Debug.LogError($"[{typeof(TSingleton).Name}] 既にシングルトンリストに存在しているため登録処理を中断します");
                return;
            }

            _instance = instance;
            _instance.OnInitialize();
            SingletonController.AddInstanceList(_instance);
        }
        
        /// <summary>
        /// シングルトン削除
        /// </summary>
        public void RemoveInstance()
        {
            //アプリ内で永続化する場合は削除を受け付けない
            if (IsInAppPersistence)
            {
                return;
            }
            
            SingletonController.RemoveInstanceList(_instance);
            
            _instance.OnFinalize();
            _instance = null;
        }
        
        /// <summary>
        /// 初期化されたら呼ばれる
        /// </summary>
        protected virtual void OnInitialize(){}

        /// <summary>
        /// 破棄する直前に呼ばれる
        /// </summary>
        protected virtual void OnFinalize(){}
    }
}