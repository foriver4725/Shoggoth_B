using Cysharp.Threading.Tasks;
using Ex;
using MainGame;
using SO;
using System;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager; // HP�Ǘ��X�N���v�g
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
    /// �G�ƂԂ��������Ԃ�
    /// </summary>
    private bool IsHit()
    {
        foreach (GameObject e in GameManager.Instance.Enemys)
        {
            if (GameManager.Instance.IsClear || GameManager.Instance.IsOver) continue;
            if (GameManager.Instance.CurrentHP <= 0) continue;
            if (SqrDistance2D(transform.position, e.transform.position) > sqrPlayerDamageRange) continue;
            return true;
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
    /// �t���O��false�ɂ��A���G���Ԃ̃J�E���g���true�ɖ߂�
    /// �Ăяo�����тɃ��Z�b�g�E�ăJ�E���g
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
