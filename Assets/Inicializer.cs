using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializer : MonoBehaviour
{
    private void Awake()
    {
        InputManager.SwitchMap(InputManager.Actions.Player);
    }
}
