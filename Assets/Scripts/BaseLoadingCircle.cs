using UnityEngine;
using UnityEngine.UI;

public class BaseLoadingCircle : MonoBehaviour
{
    protected static void GetCircle(Canvas canvas, Image loadingImage, bool active, float value)
    {
        if (!active)
        {
            canvas.enabled = false;
            loadingImage.fillAmount = 0;
            return;
        }
        canvas.enabled = true;
        loadingImage.fillAmount += 1f / value * Time.deltaTime;
    }
}
