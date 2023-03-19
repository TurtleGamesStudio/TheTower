using UnityEngine;
using System;

public class GameSpeed : MonoBehaviour
{
    public event Action<float> Changed;

    public float Speed { get; private set; } = 1;

    public void SetSpeed(float speed)
    {
        Speed = speed;
        Time.timeScale = speed;
        Changed?.Invoke(speed);
    }
}
