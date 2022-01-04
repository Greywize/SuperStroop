using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    // Static instance we can access from anywhere
    public static InterfaceManager Instance;

    private void Awake()
    {
        // Simple singleton setup
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }
}
