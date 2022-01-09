using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniLib.Common.Singletons
{
    /// <summary>
    /// Componentシングルトン基底
    /// </summary>
    public abstract class ComponentSingletonBase : MonoBehaviour, ISingleton
    {
        /// <summary>
        /// シングルトンのリソース情報
        /// </summary>
        public abstract class ResourceInfoBase
        {
            /// <summary>
            /// シングルトンのPrefabのパス
            /// </summary>
            public abstract string PrefabPath { get; }
        }

        /// <summary>
        /// インスタンス登録
        /// </summary>
        public abstract void RegisterInstance();

        /// <summary>
        /// インスタンス削除
        /// </summary>
        public abstract void RemoveInstance();
    }

    /// <summary>
    /// Componentシングルトン
    /// </summary>
    /// <typeparam name="TSingleton">シングルトンベース</typeparam>
    /// <typeparam name="TResourceInfo">コンポーネントリソース情報</typeparam>
    public abstract class ComponentSingleton<TSingleton, TResourceInfo> : ComponentSingletonBase 
        where TSingleton : ComponentSingleton<TSingleton, TResourceInfo> 
        where TResourceInfo : ComponentSingletonBase.ResourceInfoBase, new()
    {
        /// <summary>
        /// [Option] インスタンス生存期間
        /// </summary>
        protected virtual CustomExistenceDuration? CustomExistenceDuration => null;
        
        /// <summary>
        /// エディタの終了時フラグ(インスタンス生成防止)
        /// </summary>
        private static bool IsEditorQuited = false;

        /// <summary>
        /// インスタンス重複している
        /// </summary>
        private bool _isOverlappingInstance = false;
        
        /// <summary>
        /// インスタンスキャッシュ
        /// </summary>
        private static TSingleton _instance;
        
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

                if (IsEditorQuited)
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] エディタ停止直後にインスタンスにアクセスしようとしました");
                    return null;
                }

                CreateInstance();

                //NOTE: CreateInstance()した直後にAwakeが呼ばれ、すでにインスタンスが代入されているはず
                
                Debug.Assert(_instance != null, $"[{typeof(TSingleton).Name}] インスタンスがセットされていません!");

                return _instance;
            }
        }

        /// <summary>
        /// インスタンス存在するか
        /// </summary>
        public static bool Exists => _instance != null;

        /// <summary>
        /// インスタンス生成時によばれる
        /// </summary>
        private static void CreateInstance()
        {
            var prefabPath = new TResourceInfo().PrefabPath;
            
            //新規作成
            if (string.IsNullOrEmpty(prefabPath))
            {
                var newGo = new GameObject(typeof(TSingleton).Name);
                newGo.AddComponent<TSingleton>();
            }
            //Prefabから生成
            else
            {
                var loadResource = Resources.Load(prefabPath);
                if (loadResource == null)
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] リソースをロードできません。パスが正しいか確認してください。 (prefabResourcePath: {prefabPath})");
                    return;
                }

                if (!(loadResource is GameObject loadGameObject))
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] リソースはGameObjectではありません。型を見直してください。 (Object: {loadResource.ToString()})", loadResource);
                    return;
                }

                if (!loadGameObject.activeSelf)
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] ロードしたPrefabが非アクティブ状態です。 GameObjectをアクティブで保存し直してください！", loadGameObject);
                    return;
                }

                var loadSingleton = loadGameObject.GetComponent<TSingleton>();
                if (loadSingleton == null)
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] ロードしたPrefabにシングルトンのコンポーネントが含まれません。 \"{typeof(TSingleton).Name}\"をAddComponentしてください。 (Components: {string.Join(",", loadGameObject.GetComponents<Component>().Select(it => it.ToString()))})", loadGameObject);
                    return;
                }

                //インスタンス生成
                GameObject.Instantiate<TSingleton>(loadSingleton);
            }
        }

        /// <summary>
        /// 外部から安全にインスタンス破棄
        /// </summary>
        public static void SafeRemoveInstance()
        {
            if (!Exists)
            {
                return;
            }
            
            _instance.RemoveInstance();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected virtual void Awake()
        {
            RegisterInstance();
        }

        /// <summary>
        /// インスタンス登録
        /// </summary>
        public override void RegisterInstance()
        {
            //この時点でインスタンス化
            var instance = this as TSingleton;

            //インスタンスキャスト失敗
            if (instance == null)
            {
                Debug.LogError($"[{typeof(TSingleton).Name}] 何らかの原因でキャストに失敗したため登録処理を中断。こちらのインスタンスは削除 (gameObject: {gameObject.name})");
                //強制削除
                ForceDestroy();
                return;
            }

            //インスタンスが既に存在している
            if (_instance != null)
            {
                //自分
                if (_instance == instance)
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] 既にこのインスタンスが登録されているため処理を中断 (gameObject: {gameObject.name})", this);
                }
                //2重生成
                else
                {
                    Debug.LogError($"[{typeof(TSingleton).Name}] 別のインスタンスを2重登録しようとしているため、処理を中断して削除 (登録済みGameObject: {_instance.gameObject.name}, 2重登録GameObject: {this.gameObject.name})");
                    ForceDestroy();
                }
                
                return;
            }

            //既にインスタンスに登録済み
            if (SingletonController.ContainsInstanceList(instance))
            {
                Debug.LogError($"[{typeof(TSingleton).Name}] 既にシングルトンリストに存在しているため登録処理を中断します (gameObject: {gameObject.name})");
                return;
            }

            //シーン内以外
            if (CustomExistenceDuration != Singletons.CustomExistenceDuration.InScene)
            {
                //インスタンス永続化
                DontDestroyOnLoad(this.gameObject);
            }
            
            //インスタンス初期化
            _instance = instance;
            _instance.OnInitialize();
            SingletonController.AddInstanceList(_instance);
        }
        
        /// <summary>
        /// 強制削除
        /// </summary>
        private void ForceDestroy()
        {
            _isOverlappingInstance = true;
            DestroyImmediate(this.gameObject);
        }

        /// <summary>
        /// 削除されたとき
        /// </summary>
        protected virtual void OnDestroy()
        {
            //多重インスタンスの場合は削除の後処理をスキップ
            if (_isOverlappingInstance)
            {
                Debug.LogError($"[{typeof(TSingleton).Name}] 多重生成された方のインスタンスは、削除の後処理はスキップします");
                return;
            }

            DeregisterInstance();
        }

        /// <summary>
        /// インスタンス登録解除
        /// </summary>
        private void DeregisterInstance()
        {
            SingletonController.RemoveInstanceList(_instance);
            
            _instance.OnFinalize();
            _instance = null;
        }
        
        
        /// <summary>
        /// アプリケーション終了時
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            IsEditorQuited = true;
        }

        /// <summary>
        /// インスタンス削除
        /// </summary>
        public sealed override void RemoveInstance()
        {
            //アプリ内で永続化でなければ削除を実行する
            if (CustomExistenceDuration != Singletons.CustomExistenceDuration.InAppPersistence)
            {
                DestroyImmediate(gameObject);
            }
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

    
    /// <summary>
    /// リソース無しver Componentシングルトン
    /// </summary>
    /// <typeparam name="TSingleton"></typeparam>
    public abstract class ComponentSingleton<TSingleton> : ComponentSingleton<TSingleton, ComponentSingleton<TSingleton>.NonResourceInfo>
        where TSingleton : ComponentSingleton<TSingleton>
    {
        /// <summary>
        /// リソース無しversion
        /// </summary>
        public class NonResourceInfo : ResourceInfoBase
        {
            public override string PrefabPath => null;
        }
    }
}