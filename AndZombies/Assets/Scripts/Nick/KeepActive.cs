using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepActive : MonoBehaviour
{
    public static KeepActive Instance { get { return instance; } }
    private static KeepActive instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);        
    }
}
