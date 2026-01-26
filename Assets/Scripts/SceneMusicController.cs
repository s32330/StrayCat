using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic("Lose");
    }
}
