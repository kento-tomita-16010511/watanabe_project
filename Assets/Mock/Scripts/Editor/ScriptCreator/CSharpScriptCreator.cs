///
///  @クラス説明 テンプレートから新しくスクリプト作成
///

using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Mock.Editor.ScriptCreator
{
    public class CSharpScriptCreator : EditorWindow
    {
        //メニューのパス
        private const string MenuPath = "Assets/Create/";

        //テンプレートがあるディレクトリへのパス
        private const string TemplateScriptDirectoryPath = "Assets/Mock/Scripts/Editor/ScriptCreator/CSharpTemplate/";

        //テンプレート名とテンプレートメニュー記載項目
        private const string TemplateScriptMenuItemBasic = "C# Script(Inherit MonoBase)";
        private const string TemplateScriptMenuItemStatic = "C# Script Static";
        private const string TemplateScriptMenuItemSingleton = "C# Script Singleton";

        //テンプレート名とテンプレート項目
        private const string TemplateScriptBasic = "Basic";
        private const string TemplateScriptStatic = "Static";
        private const string TemplateScriptSingleton = "Singleton";

        //テンプレート名とテンプレートの拡張子
        private const string TemplateScriptExtension = ".txt";

        //作成する元のテンプレート名
        private string _templateScriptName = "";

        //新しく作成するスクリプト及びクラス名
        private string _newScriptName = "";

        //スクリプトの説明文
        private string _scriptSummary = "";

        /// <summary>
        /// メニューアイテムBasic
        /// </summary>
        [MenuItem(MenuPath + TemplateScriptMenuItemBasic)]
        private static void CreateBasicScript()
        {
            ShowWindow(TemplateScriptBasic);
        }

        /// <summary>
        /// メニューアイテムStatic
        /// </summary>
        [MenuItem(MenuPath + TemplateScriptMenuItemStatic)]
        private static void CreateStaticScript()
        {
            ShowWindow(TemplateScriptStatic);
        }

        /// <summary>
        /// メニューアイテムSingleton
        /// </summary>
        [MenuItem(MenuPath + TemplateScriptMenuItemSingleton)]
        private static void CreateSingletonScript()
        {
            ShowWindow(TemplateScriptSingleton);
        }

        /// <summary>
        /// ウィンドウ表示
        /// </summary>
        private static void ShowWindow(string templateScriptName)
        {
            //ウィンドウ作成
            GetWindow<CSharpScriptCreator>("Create Script");

            //各項目を初期化
            GetWindow<CSharpScriptCreator>()._templateScriptName = templateScriptName;
            GetWindow<CSharpScriptCreator>()._newScriptName = "NewScript";
            GetWindow<CSharpScriptCreator>()._scriptSummary = "";
        }

        /// <summary>
        /// 表示するGUI処理
        /// </summary>
        private void OnGUI()
        {
            //テンプレート
            EditorGUILayout.LabelField("Template Script Name : " + _templateScriptName);
            GUILayout.Space(10);

            //スクリプト名
            GUILayout.Label("スクリプト名");
            _newScriptName = EditorGUILayout.TextField(_newScriptName);
            GUILayout.Space(10);

            //スクリプトの説明文
            GUILayout.Label("スクリプト説明");
            _scriptSummary = EditorGUILayout.TextArea(_scriptSummary);
            GUILayout.Space(30);

            //作成ボタン
            if (GUILayout.Button("Create"))
            {
                if (!CreateScript())
                {
                    Debug.LogError("作成エラー");
                }

                //ウィンドウを閉じる
                Close();
            }
        }

        /// <summary>
        /// スクリプト作成
        /// </summary>
        private static bool CreateScript()
        {
            //スクリプト名が空欄の場合は作成失敗
            if (string.IsNullOrEmpty(GetWindow<CSharpScriptCreator>()._newScriptName))
            {
                Debug.Log("スクリプト名が入力されていないため、スクリプトが作成できませんでした");
                return false;
            }

            //現在選択しているファイルのパスを取得、選択されていない場合はスクリプト作成失敗
            var directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(directoryPath))
            {
                Debug.Log("作成場所が選択されていないため、スクリプトが作成できませんでした");
                return false;
            }

            //選択されているファイルに拡張子がある場合(ディレクトリでない場合)は一つ上のディレクトリ内に作成する
            if (!string.IsNullOrEmpty(new FileInfo(directoryPath).Extension))
            {
                directoryPath = Directory.GetParent(directoryPath).FullName;
            }

            // ネームスペース確定
            var scriptNamespace = GetScriptNamespace(directoryPath);

            //同名ファイルがあった場合はスクリプト作成失敗にする(上書きしてしまうため)
            var exportPath = directoryPath + "/" + GetWindow<CSharpScriptCreator>()._newScriptName + ".cs";

            if (File.Exists(exportPath))
            {
                Debug.Log(exportPath + "が既に存在するため、スクリプトが作成できませんでした");
                return false;
            }

            //テンプレートへのパスを作成しテンプレート読み込み
            var templatePath = TemplateScriptDirectoryPath + GetWindow<CSharpScriptCreator>()._templateScriptName +
                               TemplateScriptExtension;
            var streamReader = new StreamReader(templatePath, Encoding.UTF8);
            var scriptText = streamReader.ReadToEnd();
            // 閉じる
            streamReader.Close();

            //各項目を置換
            scriptText = scriptText.Replace("#NAMESPACE#", scriptNamespace);
            scriptText = scriptText.Replace("#SUMMARY#",
                GetWindow<CSharpScriptCreator>()._scriptSummary.Replace("\n", "\n/// ")); //改行するとコメントアウトから外れるので修正     
            scriptText = scriptText.Replace("#SCRIPT_NAME#", GetWindow<CSharpScriptCreator>()._newScriptName);

            //スクリプトを書き出し
            File.WriteAllText(exportPath, scriptText, Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

            return true;
        }

        /// <summary>
        /// パスからネームスペースを取得する
        /// </summary>
        public static string GetScriptNamespace(string directoryPath)
        {
            var scriptNamespace = "Mock";
            var tempPath = directoryPath.Replace("\\", "/");
            if (tempPath.IndexOf($"/Scripts/", StringComparison.Ordinal) > 0)
            {
                var dirs = tempPath.Split('/');
                var start = false;
                foreach (var dir in dirs)
                {
                    // OutGameが出たら一旦リセット
                    if (dir == "OutGame")
                    {
                        start = true;
                        scriptNamespace = "Mock";
                    }
                    else if (start)
                    {
                        scriptNamespace += $".{dir}";
                    }
                    else if (dir == "Scripts")
                    {
                        start = true;
                    }
                }
            }

            return scriptNamespace;
        }
    }
}