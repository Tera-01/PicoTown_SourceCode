using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuide : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
