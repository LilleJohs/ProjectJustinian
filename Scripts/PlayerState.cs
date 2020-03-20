using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool paused = false;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public static void Pause()
    {
        if (paused) {
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            paused = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
