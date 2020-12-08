using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReTurnTitel : MonoBehaviour
{

    public GameObject panel1 = null;
    public GameObject panel2 = null;
    public GameObject panel = null;
    public GameObject mask = null;



    public void Onckick()
    {
        SceneManager.LoadScene("TItel");
    }

    public void setfalse()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void setActive()
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        mask.SetActive(true);
    }
}
