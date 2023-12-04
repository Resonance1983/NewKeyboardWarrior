using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace KeyboardWarrior
{
    public class TitleScreenManager : MonoBehaviour
    {
        public GameObject clickToStartObj;
        public Image logoImage;

        public float whiteScreenTime = 3f;
        public float fadeInTime = 3f;
        public float canBeginTime = 1f;

        void Start ()
        {
            clickToStartObj.SetActive(false);
            StartCoroutine(TitleScreenSequence());
        }

        IEnumerator TitleScreenSequence()
        {
            logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, 0);
            yield return whiteScreenTime;
            for (float i = 0; i <= 1; i += Time.deltaTime/fadeInTime)
            {
                logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, i);
                yield return null;
            }
            yield return new WaitForSeconds(canBeginTime);
            clickToStartObj.SetActive(true);
        }
    }
}
