using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMobileUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
