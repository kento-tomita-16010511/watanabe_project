///
///  @クラス説明 MVPの新しくスクリプト作成
///

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Mock.Editor.ScriptCreator
{
    public class MvpScriptCreator : EditorWindow
    {
        /// <summary>
        /// メニューのパス
        /// </summary>
        private const string MenuPath = "Assets/Create/";

        /// <summary>
        /// テンプレートがあるディレクトリへのパス
        /// </summary>
        private const string TemplateScriptDirectoryPath = "Assets/Mock/Scripts/Editor/ScriptCreator/CSharpTemplate/";

        /// <summary>
        /// テンプレート名とテンプレートメニュー記載項目
        /// </summary>
        private const string TemplateScriptMenuItemMVP = "C# MVP Script";

        /// <summary>
        /// スクリプトMAX
        /// </summary>
        private const int ScriptMax = 3;

        /// <summary>
        /// テンプレート名とテンプレートの拡張子
        /// </summary>
        private const string TemplateScriptExtension = ".txt";

        /// <summary>
        /// テンプレートファイル名
        /// </summary>
        private static readonly string[] TemplateScriptMvp =
        {
            "MVPPresenter",
            "MVPView",
            "MVPModel"
        };

        /// <summary>
        /// 作成する元のテンプレート名
        /// </summary>
        private string _templateScriptName = "";

        /// <summary>
        /// 新しく作成するスクリプト及びクラス名
        /// </summary>
        private string _newScriptName = "";

        /// <summary>
        /// スクリプトの説明文
        /// </summary>
        private string _scriptSummary = "";

        /// <summary>
        /// 出力するスクリプト及びクラス名
        /// </summary>
        private readonly string[] _outNewScriptName = new string[ScriptMax];

        /// <summary>
        /// スクリプトの説明文
        /// </summary>
        private readonly string[] _outScriptSummary = new string[ScriptMax];

        /// <summary>
        /// メニューアイテムMVP
        /// </summary>
        [MenuItem(MenuPath + TemplateScriptMenuItemMVP)]
        private static void CreateMvpScript()
        {
            ShowWindow();
        }

        /// <summary>
        /// ウィンドウ表示
        /// </summary>
        private static void ShowWindow()
        {
            //ウィンドウ作成
            GetWindow<MvpScriptCreator>("MVP Create Script");

            //各項目を初期化
            GetWindow<MvpScriptCreator>()._newScriptName = "NewScript";
            GetWindow<MvpScriptCreator>()._scriptSummary = "";
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
            GetWindow<MvpScriptCreator>()._newScriptName =
                EditorGUILayout.TextField(GetWindow<MvpScriptCreator>()._newScriptName);
            GUILayout.Space(10);

            //スクリプトの説明文
            GUILayout.Label("スクリプト説明");
            GetWindow<MvpScriptCreator>()._scriptSummary =
                EditorGUILayout.TextArea(GetWindow<MvpScriptCreator>()._scriptSummary);
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
            if (string.IsNullOrEmpty(GetWindow<MvpScriptCreator>()._newScriptName))
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
            var scriptNamespace = CSharpScriptCreator.GetScriptNamespace(directoryPath);

            // ファイル名確定
            GetWindow<MvpScriptCreator>()._outNewScriptName[0] =
                GetWindow<MvpScriptCreator>()._newScriptName + "Presenter";
            GetWindow<MvpScriptCreator>()._outNewScriptName[1] = GetWindow<MvpScriptCreator>()._newScriptName + "View";
            GetWindow<MvpScriptCreator>()._outNewScriptName[2] = GetWindow<MvpScriptCreator>()._newScriptName + "Model";

            //説明確定
            GetWindow<MvpScriptCreator>()._outScriptSummary[0] =
                GetWindow<MvpScriptCreator>()._scriptSummary + "プレゼンタークラス";
            GetWindow<MvpScriptCreator>()._outScriptSummary[1] =
                GetWindow<MvpScriptCreator>()._scriptSummary + "ビュークラス";
            GetWindow<MvpScriptCreator>()._outScriptSummary[2] =
                GetWindow<MvpScriptCreator>()._scriptSummary + "モデルクラス";

            for (var i = 0; i < ScriptMax; i++)
            {
                //同名ファイルがあった場合はスクリプト作成失敗にする(上書きしてしまうため)
                var exportPath = directoryPath + "/" + GetWindow<MvpScriptCreator>()._outNewScriptName[i] + ".cs";

                if (File.Exists(exportPath))
                {
                    Debug.Log(exportPath + "が既に存在するため、スクリプトが作成できませんでした");
                    return false;
                }

                //テンプレートへのパスを作成しテンプレート読み込み
                var templatePath = TemplateScriptDirectoryPath + TemplateScriptMvp[i] + TemplateScriptExtension;
                var streamReader = new StreamReader(templatePath, Encoding.UTF8);
                var scriptText = streamReader.ReadToEnd();
                // 閉じる
                streamReader.Close();

                //各項目を置換
                scriptText = scriptText.Replace("#NAMESPACE#", scriptNamespace);
                scriptText = scriptText.Replace("#SUMMARY#",
                    GetWindow<MvpScriptCreator>()._outScriptSummary[i]
                        .Replace("\n", "\n/// ")); //改行するとコメントアウトから外れるので修正     
                scriptText = scriptText.Replace("#SCRIPT_NAME#", GetWindow<MvpScriptCreator>()._outNewScriptName[i]);
                scriptText = scriptText.Replace("#INPUT_SCRIPT_NAME#", GetWindow<MvpScriptCreator>()._newScriptName);

                //スクリプトを書き出し
                File.WriteAllText(exportPath, scriptText, Encoding.UTF8);
            }

            //AssetDatabaseリフレッシュ
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

            return true;
        }
    }
}