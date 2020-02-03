using Assets.Scripts.Controllers;
using UnityEngine;

public class OpenScript : MonoBehaviour
{
    public Button _button;
    public AstarPath boo;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (_button.signal)
        {
            Destroy(gameObject);
            boo.Scan();
        }
    }
}