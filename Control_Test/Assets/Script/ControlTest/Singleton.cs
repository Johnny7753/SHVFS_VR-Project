using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{


    //private field of type T
    private static T instance = null;
    //public property of type T
    //This is what our other components will access
    //This is a pattern you see a lot in C#; private fields with public properties
    public static T Instance
    {
        //This is our public "getter"
        get
        {
            //Our getter checks to see if the private instance is null...
            //If it's not, it returns it -> remeber, we only want 1 instance, ever with a singleton, never more than one
            if (instance != null) return instance;

            //If it is null, it first checks the scene, and try to grab it
            instance = FindObjectOfType<T>();

            //If it's still null after checking the scene, we instantiate a new instance
            if (instance == null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            //DontDestroyOnLoad(instance.gameObject);

            //We return the instance
            return instance;
        }
    }
    public virtual void Awake()
    {
        if (instance != null) Destroy(gameObject);
    }

}
