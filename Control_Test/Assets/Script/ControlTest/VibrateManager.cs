using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VibrateManager : MonoBehaviour
{
    public ActionBasedController controller;
    // Start is called before the first frame update
    void Start()
    {
       controller = GetComponent<ActionBasedController>(); 
    }

    // Update is called once per frame
    public void VibrateController(float ampliitude,float duration)
    {
        controller.SendHapticImpulse(ampliitude, duration);
    }
}
