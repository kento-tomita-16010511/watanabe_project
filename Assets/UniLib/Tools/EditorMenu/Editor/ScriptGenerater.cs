#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniLib.Tools.EditorMenu
{
    /// <summary>
    /// スクリプト作成
    /// </summary>
    public class ScriptGenerater
    {
        public static readonly string CODE_TEMPLATE_MONOBEHAVIOUR = 
@"using System;
using UnityEngine;

namespace #NAME_SPACE#
{
    public class #CLASS_NAME# : MonoBehaviour
    {
        void Start()
        {
        }
    }
}
";
        public static readonly string CODE_TEMPLATE = 
@"using System;

namespace #NAME_SPACE#
{
    public class #CLASS_NAME#
    {
    }
}
";

        [MenuItem("Assets/Create/C# MonoBehaviour Scripts", false, 0)]
        private static void GenerateMonoBehaviourScript()
        {
            var selectionDirPath = GetSelectionDirectoryPath();
            InputScriptNameWindow.Open(className =>
            {
                GenerateScripts(selectionDirPath, className, "cs", CODE_TEMPLATE_MONOBEHAVIOUR);
            });
        }
        [MenuItem("Assets/Create/C# Class Scripts", false, 1)]
        private static void GenerateClassScript()
        {
            var selectionDirPath = GetSelectionDirectoryPath();
            InputScriptNameWindow.Open(className =>
            {
                GenerateScripts(selectionDirPath, className, "cs", CODE_TEMPLATE);
            });
        }

        public static void GenerateScripts(string rootDirPath, string className, string fileExtension, string codeText)
        {
            var namespacePath = "";
            var fileName = className + (string.IsNullOrEmpty(fileExtension) ? "" : $".{fileExtension}");

            if (string.IsNullOrEmpty(rootDirPath))
            {
                Debug.LogError("ScriptGenerater Error: ディレクトリが選択されていないためスクリプトを生成できません (※最低でも\"Assets\"を指定すること)");
                return;
            }

            if (string.IsNullOrEmpty(className))
            {
                Debug.LogError("ScriptGenerater Error: クラス名を指定してください");
                return;
            }
            
    //        // フォルダを作成
    //        var folderPath = Path.GetDirectoryName(filePath);
    //        CreateFolder(folderPath);

            var dirPathSplits = rootDirPath.Split('/', '\\');
            for (var index = 0; index < dirPathSplits.Length; index++)
            {
                var dirSplit = dirPathSplits[index];

                switch (dirSplit)
                {
                    //除外する文字列
                    case "Assets":
                    case "Scenes":
                    case "Scripts":
                        continue;
                    
                    //それ以外は含める
                    default:
                        namespacePath += dirSplit + (index < dirPathSplits.Length - 1 ? "." : "");
                        break;
                }
            }

            // アセットのパスを作成
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(rootDirPath, fileName));
            
            // コードテンプレートを置換
            var fixCode = codeText
                .Replace(@"#NAME_SPACE#", namespacePath)
                .Replace(@"#CLASS_NAME#", className);

            //ファイルを作成して更新
            File.WriteAllText(assetPath, fixCode);
            AssetDatabase.Refresh();
        }

        private static string GetSelectionDirectoryPath()
        {
            var selectInstanceId = Selection.activeInstanceID;
    //        Debug.LogError("Selection.activeInstanceID: " + selectInstanceId);

            //選択されていない場合はNull
            if (selectInstanceId == 0)
            {
                return null;
            }

            string selectionRawPath = AssetDatabase.GetAssetPath(selectInstanceId);
    //        Debug.LogError("AssetDatabase.GetAssetPath(): " + selectionRawPath);

            //ファイルを選択していない場合はNull
            if (string.IsNullOrEmpty(selectionRawPath))
            {
                return null;
            }

    //        Debug.LogError("GetDirectoryName: " + Path.GetDirectoryName(selectionRawPath));
    //        Debug.LogError("GetExtension: " + Path.GetExtension(selectionRawPath));
    //        Debug.LogError("File.Exists: " + File.Exists(selectionRawPath));
    //        Debug.LogError("Directory.Exists: " + Directory.Exists(selectionRawPath));

            //選択対象がファイルだったら、ルートディレクトリを渡す
            if (File.Exists(selectionRawPath))
            {
                return Path.GetDirectoryName(selectionRawPath);
            }
            
            //ディレクトリだったらそのまま返す
            return selectionRawPath;
        }

    //    /// <summary>
    //    /// 指定されたパスのフォルダを生成する
    //    /// </summary>
    //    /// <param name="path">フォルダパス（例: Assets/Sample/FolderName）</param>
    //    private static void CreateFolder(string path)
    //    {
    //        var target = "";
    //        var splitChars = new char[]{ Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
    //        foreach (var dir in path.Split(splitChars)) {
    //            var parent = target;
    //            target = Path.Combine(target, dir);
    //            if (!AssetDatabase.IsValidFolder(target)) {
    //                AssetDatabase.CreateFolder(parent, dir);
    //            }
    //        }
    //    }
    }
    // テキスト入力Window
    public class InputScriptNameWindow :EditorWindow
    {
        /// <summary>
        /// 開く
        /// </summary>
        /// <param name="callback"></param>
        public static void Open(Action<string> callback)
        {
            var window = EditorWindow.GetWindow<InputScriptNameWindow>(true);
            window.Setup(callback);
            window.position = new Rect(150f, 150f, 325f, 150f);
        }
        
        private System.Action<string> _callback;
        private void Setup(System.Action<string> callback)
        {
            _callback = callback;
        }

        string _input = "";
        bool _isInit = false;
        void OnGUI ()
        {
            GUILayout.Label ("スクリプト名を入力してください");
            GUILayout.Space (10f);
            GUI.SetNextControlName ("ForcusField");
            _input = GUILayout.TextField (_input);
            GUILayout.Space (10f);

            // 何かしら入力しないとOKボタンを押せないようにするDisableGroup
            EditorGUI.BeginDisabledGroup (string.IsNullOrEmpty (_input));
            GUILayout.BeginHorizontal ();
            if (GUILayout.Button ("OK", GUILayout.Height (30f)))
            {
                _callback (_input);
                Close ();
            }
            EditorGUI.EndDisabledGroup ();
    //        if (GUILayout.Button ("CANCEL", GUILayout.Height (30f)))
    //        {
    //            _callback (null);
    //            Close ();
    //        }
            GUILayout.EndHorizontal ();

            if (_isInit == false)
            {
                // テキストフィールドにフォーカスをあてる
                EditorGUI.FocusTextInControl ("ForcusField");
            }
            _isInit = true;
        }
    }
}
#endif