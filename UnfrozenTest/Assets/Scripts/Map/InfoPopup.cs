using System.Collections;
using UnityEngine;

//Небольшой скриптик для окна, который сообщает, что герой не выбран.
public class InfoPopup : MonoBehaviour
{
    private const float AlphaDelimetr = 0.05f;
    private const float Seconds = 0.03f;
    private const float MaxAlpha = 1f;

    [SerializeField] private CanvasGroup _curtain;

    public void Show()
    {
        gameObject.SetActive(true);
        _curtain.alpha = MaxAlpha;
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        while(_curtain.alpha > 0)
        {
            _curtain.alpha -= AlphaDelimetr;
            yield return new WaitForSeconds(Seconds);
        }
        
        gameObject.SetActive(false);
    }
}
