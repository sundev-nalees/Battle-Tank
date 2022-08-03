using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoSingletonGeneric<T> : MonoBehaviour where T:monoSingletonGeneric<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }


    public void Awake()
    {
        if (!instance) 
        {
            instance = (T)this;
        }
        else
        {
            Destroy(this);
        }
    }
}
