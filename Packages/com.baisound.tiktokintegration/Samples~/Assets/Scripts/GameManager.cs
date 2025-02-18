using UnityEngine;
using Com.Baisound.TikTokIntegration;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Collections;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public GameObject spherePrefab;
    [Header("References")]
    private TikTokIntegration tiktokIntegration;
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private IEnumerator WaitForTikTokIntegration()
    {
        while (tiktokIntegration == null)
        {
            tiktokIntegration = FindFirstObjectByType<TikTokIntegration>();

            if (tiktokIntegration != null)
            {
                UnityEngine.Debug.Log("✅ TikTokIntegration が見つかりました！");
                tiktokIntegration.LoadConfig();
                tiktokIntegration.OnGiftReceived += OnGiftReceived;
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }


    private void Start()
    {
        StartCoroutine(WaitForTikTokIntegration());
    }


    // 設定ファイルを再読み込みするメソッドを追加
    public void ReloadConfig()
    {
        UnityEngine.Debug.Log("TikTok Config Reloaded");
        tiktokIntegration.ReloadConfig();
    }


    // TikTokIntegration からギフト情報を受け取った際に呼ばれるハンドラ
    private void OnGiftReceived(TikTokIntegration.GiftAction giftAction)
    {
        // TikTokIntegration から渡されたギフトデータを処理
        UnityEngine.Debug.Log($"ギフト受信: ID: {giftAction.GiftId} / {giftAction.GiftName} を {giftAction.Count} 個、送信者: {giftAction.Sender}");


        // ここで giftAction の内容に応じてゲーム内の処理を実行します（スコア加算やエフェクト発生など）
        // 例:
        // if (giftAction.GiftName == "Rose") {
        //     // バラのギフトに対する処理
        // }
        //if (action == null) return; // 設定にないギフト

        switch (giftAction.Command)
        {
            case "AddSpheres":
                AddSpheres(giftAction.Parameters);
                break;
            case "PowerUpPlayer":
                PowerUpPlayer(giftAction.Parameters);
                break;
            case "Unknown":
                PowerUpPlayer2();
                break;
            default:
                UnityEngine.Debug.Log("[GameManager] Unknown command: " +
                giftAction.Command);
                break;
        }

    }

    private void OnDestroy()
    {
        if (tiktokIntegration != null)
        {
            tiktokIntegration.OnGiftReceived -= OnGiftReceived;
        }
    }

    void SpawnSphere()
    {
        if (isGameOver) return;
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-2f, 2f), 6f, 0f);
        Instantiate(spherePrefab, spawnPos, Quaternion.identity);
    }

    public void OnGameOver()
    {
        isGameOver = true;
        UnityEngine.Debug.Log("[GameManager] Game Over!");
        // ここでUI表示やリトライボタンなどを出す処理
    }

    void AddSpheres(System.Collections.Generic.Dictionary<string, object> parameters)
    {
        if (parameters.ContainsKey("amount"))
        {
            int amount = int.Parse(parameters["amount"].ToString());
            for (int i = 0; i < amount; i++)
            {
                UnityEngine.Debug.Log("[GameManager] SpawnSphere!");
                SpawnSphere();
            }
        }
    }

    void PowerUpPlayer(System.Collections.Generic.Dictionary<string, object> parameters)
    {
        if (parameters.ContainsKey("duration"))
        {
            float duration =
            float.Parse(parameters["duration"].ToString());
            PlayerControll player = FindFirstObjectByType<PlayerControll>();
            if (player != null)
            {
                UnityEngine.Debug.Log("[GameManager] PowerUp!");
                player.PowerUp(duration);
            }
        }
    }

    void PowerUpPlayer2()
    {
        PlayerControll player = FindFirstObjectByType<PlayerControll>();
        if (player != null)
        {
            UnityEngine.Debug.Log("[GameManager] PowerUp!");
            player.PowerUp(0.01f);
        }
    }
}