using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public TextMeshProUGUI connectText;
    public TextMeshProUGUI statusText;
    public GameObject Loading;

    bool done = false;
    void Start()
    {
        StartCoroutine(Connection());
    }

    IEnumerator Connection()
    {
        while (!NetworkManager._Manager.isConnected) yield return null;
        connectText.text = "Connected Successfully";
        Loading.SetActive(false);
        yield return new WaitForSeconds(3);
        Loading.SetActive(true);
        connectText.text = "Waiting for command";
        statusText.text = "MAKO Says Hi! =)";
        done = true;
    }
}
