using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controllers;
using UnityEngine;

public class OpenScript : MonoBehaviour
{
    public Button _button;
    public AstarPath boo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_button.signal)
        {
            Destroy(gameObject);
            boo.Scan();
        }
    }


}
