using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject loadingScreen;
    
    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
    public void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }
}
