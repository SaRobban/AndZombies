using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReloadScene : MonoBehaviour
{
    public void ResetScene(InputAction.CallbackContext context)
    {
        if(context.performed)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
