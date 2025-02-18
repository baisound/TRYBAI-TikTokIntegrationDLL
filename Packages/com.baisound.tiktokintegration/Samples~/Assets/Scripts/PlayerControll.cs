using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float speed;
    [SerializeField, Header("弾のオブジェクト")]
    private GameObject _bulletPrefab;
    [SerializeField, Header("弾を発射する時間")]
    private float _shootTime;

    private Vector2 _inputVelocity;
    private Rigidbody2D _rigid;
    private float _shootCount;

    private void Start()
    {
        _inputVelocity = Vector2.zero;
        _rigid = GetComponent<Rigidbody2D>();
        _shootCount = 0;
    }

    void Update()
    {
        _Move();
        _Shooting();
    }

    private void _Move()
    {
        _rigid.linearVelocity = _inputVelocity * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputVelocity = context.ReadValue<Vector2>();
    }

    private void _Shooting()
    {
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;

        GameObject bulletObj = Instantiate(_bulletPrefab);
        bulletObj.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y / 2.0f, 0f);
        _shootCount = 0.0f;
    }

    public void PowerUp(float duration)
    {
        StartCoroutine(PowerUpCoroutine(duration));
    }

    private IEnumerator PowerUpCoroutine(float duration)
    {
        float originalSpeed = speed;
        speed *= 2; // 例えば2倍の速度にする
        yield return new WaitForSeconds(duration);
        speed = originalSpeed; // 元の速度に戻す
    }
}
