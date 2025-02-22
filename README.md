# TikTokIntegration
## v1.0.0

main.ymlを更新しました。 

## v1.0.1

#3のプルリクエストがマージされ、マスターブランチの内容がメインブランチに反映されました。 

## v1.0.2

新機能：ギフト情報リクエストの REST API リクエストメソッドを追加しました。ただし、ライセンス認証とギフト登録リクエストを組み込む必要があります。 

## v1.0.3

main.ymlを更新しました 

## v1.0.4

・漏れていたコミットを追加しました。 

## v1.0.6

- 「GIT ACTION commit_message.txt」のパスを修正しました。 - 「commit_message.txt」を正しく「GITHUB_WORKSPACE」から取得します。 - ファイルが存在しない場合、デフォルト値を「Update README and DLLs for vX.Y.Z」に設定しました。 - 修正後のワークフローを「GitHub Actions」に適用し、動作確認を行いました。 

## v1.0.7

- ギフト登録リクエスト REST APIへの正しいフォーマットでギフト情報を投げるように修正（配信者情報がライブラリから取れなかったので配信ルーム情報から配信者情報を取得するように）
-  開発時のデバッグログをリリースで出力しないように
- ライセンス認証とギフト情報送信APIの共通処理部分を共通化
- ライセンス認証の追加と認証NGの場合はGiftActionを実行させないように制御する実装
