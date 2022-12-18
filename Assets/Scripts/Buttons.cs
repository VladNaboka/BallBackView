using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public WebViewObject webViewObject;
    public void ButtonBack()
    {
        webViewObject.GoBack();
    }
    public void ButtonForward()
    {
        webViewObject.GoForward();
    }
}
