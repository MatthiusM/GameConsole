using System;
using System.Collections;
using UnityEngine;

public class SpawnMoney : MonoBehaviour
{
    [SerializeField] private GameObject cashPrefab;
    [SerializeField] private Transform spawnPoint;
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
            if (characterController.velocity.magnitude > 0.1f) 
            {
                DecreaseMoney(decreaseAmount);
            }
        }
    }

    private void DecreaseMoney(float amount)
    {
        totalMoney -= amount;
        OnDecreaseMoney?.Invoke(amount);
        SpawnCash();
    }

    private void SpawnCash()
    {
        if (cashPrefab != null && spawnPoint != null)
        {
            Instantiate(cashPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
