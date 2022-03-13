using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Test.Test.started += _ => Lol(true);
        controls.Test.Test.canceled += _ => Lol(false);
    }

    private void Lol(bool a)
    {
        Debug.Log("lolol" + a);
    }

    private void OnEnable()
    {
        controls.Camera.Enable();
        controls.Test.Enable();
    }
    private void OnDisable()
    {
        controls.Test.Disable();
    }
}
