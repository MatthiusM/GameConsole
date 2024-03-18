using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private MoneyObjectPool moneyObjectPool;
    [field: SerializeField] public Transform SpawnPoint {get; private set;}

    [SerializeField] private float totalMoney = 100000f;
    [SerializeField] private float decreaseInterval = 5f;
    [SerializeField] private float decreaseAmount = 5000f;
    [SerializeField] private CharacterController characterController;

    public event Action<float> OnDecreaseMoney;


    private void Start()
    {
        StartCoroutine(DecreaseMoneyOverTime());
    }

    private IEnumerator DecreaseMoneyOverTime()
    {
        while (totalMoney > 0)
        {
            yield return new WaitForSeconds(decreaseInterval);
            if (characterController.velocity.magnitude > 0.1f && characterController.isGrounded)
            {
                DecreaseMoney(decreaseAmount);
                moneyObjectPool.Pool.Get();
            }
        }
    }

    private void DecreaseMoney(float amount)
    {
        totalMoney -= amount;
        OnDecreaseMoney?.Invoke(amount);
    }
}
