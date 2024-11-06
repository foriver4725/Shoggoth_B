using Cysharp.Threading.Tasks;
using Ex;
using MainGame;
using SO;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    public Light2D Light2D => light2D;

    public HPManager hpManager; // HP管理スクリプト
    [SerializeField] private AudioSource damagedAS;

    private readonly DamageFlag elevatorFlag = new();
    private readonly DamageFlag damagableFlag = new();

    private float SqrDistance2D(Vector3 v1, Vector3 v2) => ((Vector2)v1 - (Vector2)v2).sqrMagnitude;
    private float sqrPlayerDamageRange => SO_Player.Entity.PlayerDamageRange * SO_Player.Entity.PlayerDamageRange;

    public void OnInteractedElevator() => elevatorFlag.CountInvincible();

    private void OnDisable()
    {
        elevatorFlag.Dispose();
        damagableFlag.Dispose();
    }

    private void Awake()
    {
        light2D.pointLightOuterRadius = SO_DifficultySettings.Entity.VisibilityRange;
    }

    private void Update()
    {
        if (damagableFlag.IsCounting) return;
        if (!elevatorFlag.IsDamagable) return;

        if (IsHit())
        {
            damagableFlag.CountInvincible();
            damagedAS.Raise(SO_Sound.Entity.DamageTookSE, SType.SE);
            hpManager.DecreaseHP();
        }
    }

    /// <summary>
    /// 敵とぶつかったか返す
    /// </summary>
    private bool IsHit()
    {
        if (GameManager.Instance.EventState == EventState.End) return false;
        if (GameManager.Instance.CurrentHP <= 0) return false;

        foreach (GameObject e in GameManager.Instance.Enemys)
        {
            if (e == null) continue;
            if (SqrDistance2D(transform.position, e.transform.position) > sqrPlayerDamageRange) continue;
            return true;
        }

        List<GameObject> extraShoggoth = GameManager.Instance.ExtraShoggoth;
        if (extraShoggoth is not null)
        {
            foreach (GameObject e in extraShoggoth)
            {
                if (e == null) continue;
                if (SqrDistance2D(transform.position, e.transform.position) > sqrPlayerDamageRange) continue;
                return true;
            }
        }

        return false;
    }
}

public sealed class DamageFlag : IDisposable
{
    public bool IsDamagable { get; private set; } = true;
    public bool IsCounting { get; private set; } = false;

    private CancellationTokenSource cts;

    public DamageFlag() => cts = new();
    public void Dispose() => ResetToken();

    /// <summary>
    /// フラグをfalseにし、無敵時間のカウント後にtrueに戻す
    /// 呼び出すたびにリセット・再カウント
    /// </summary>
    public void CountInvincible()
    {
        ResetToken();
        cts = new();
        CountInvincibleImpl(cts.Token).Forget();
    }

    private async UniTask CountInvincibleImpl(CancellationToken ct)
    {
        IsCounting = true;
        IsDamagable = false;
        await UniTask.Delay(TimeSpan.FromSeconds(SO_Player.Entity.InvincibleTime), cancellationToken: ct);
        IsDamagable = true;
        IsCounting = false;
    }

    private void ResetToken()
    {
        if (cts == null) return;
        cts.Cancel();
        cts.Dispose();
        cts = null;
    }
}
