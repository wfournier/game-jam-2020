using UnityEngine;

namespace Assets.Scripts.UI
{
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
}
