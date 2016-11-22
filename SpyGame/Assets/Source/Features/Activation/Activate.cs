using UnityEngine;
using System.Collections;

namespace SpyGame.Components
{
    public class Activate : MonoBehaviour
    {
        public void SetActive(bool activate)
        {
            gameObject.SetActive(activate);
        }

    }
}
