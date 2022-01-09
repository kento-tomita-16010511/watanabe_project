# UniLib

## 概要
ゲーム制作に便利な機能を1つに集約したライブラリです。  

license: MIT(関連する外部アセットはそちらの規約に従ってください)  

## 開発指標
- Fast - 早く
   - あくまでゲーム制作が主役なので凝り過ぎてライブラリ制作に時間を使うのはNG
- Minimum - 最低限
   - 機能をむやみに増やしたり、汎用的にしすぎると保守も大変
   - 機能が大きくなってきた場合はそれ専用のライブラリへ分離を検討する
- Reusability - 再利用性
   - 他のゲームプロジェクトでもすぐ使えるような柔軟性を持たせる

## マイルストーン
- ゲームマネージャー
- シーンマネージャー
- シーケンサークラス
- Layer管理
  - 通常UI,ポップアップ,オーバレイローディング,システム,エフェクトなど…
- サウンドマネージャー
- オブジェクトプール
- エフェクト再利用機能
- PlayFabラッパークラス(特に不要ならそのままでもよい)
   - マスターマネージャー
   - APIマネージャー
- ローカルセーブ/ロード機能
   - PlayFabかUnityServiceで似たような機能ある気がする
- アセットバンドルラッパークラス
   - UnityのAddresable機能などを活用して不要ならそのままでもよい
- ローカルプッシュ機能
- デバッガー機能
   - クラッシュレポートサービス(GoogleAnalytics or UnityService or SmartBeat)
   - SRDebugger拡張機能
   - 環境切り替え機能(サーバー選択画面的な)
      - 接続先URL
      - プレイヤーID等
      - データ削除
      - バイナリNo
   - 簡易プロファイラー
      - CPU&メモリ使用率&FPS監視
- 運用関連
   - アナリティクス(GoogleAnalytics or UnityService)
- WebViewラッパークラス
   - UniWebView v4を使うつもり
- ダイアログマネージャー
- スクロールビュー(他ライブラリで代用可)
- スライドビュー(他ライブラリで代用可)

## 必須外部アセット
- UniRx
- UniTask
- DOTween
- UniWebView v4(スマホOnly)
- TexturePacker
- SRDebugger(または類似機能)

## 最終目標
現在は未完成なので、Mock制作中に必要になれば機能追加＆改良をしていくつもりです。  

最終的には`UniLib`を分離し、Githubのリポジトリにpushしてsubmoduleとして他のプロジェクトに転用可能にすることを当面の最終目標にします。

**ライブラリ~~めんどくさいので~~一緒に作ってくれる人随時募集中☆ミ**


## 開発Tips
- [開発初期に暇を持て余しているUnityエンジニアができる42のTips前編](https://www.shibuya24.info/entry/sumzap_adventcal20191211)
- [開発初期に暇を持て余しているUnityエンジニアができる42のTips後編](https://www.shibuya24.info/entry/sumzap_adventcal20191224)