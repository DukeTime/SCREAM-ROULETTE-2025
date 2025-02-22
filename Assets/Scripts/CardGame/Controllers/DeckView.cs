using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckView: MonoBehaviour
{
    public List<GameObject> cards;
    public GameObject txt;

    public void View(int count)
    {
        foreach (GameObject j in cards)
        {
            j.SetActive(false);
        }
        for (int i = 0; i < count; i++)
        {
            cards[i].SetActive(true);
        }
    }
    
    void OnMouseEnter()
    {
        txt.SetActive(true);
        Debug.Log("Мышка наведена на " + gameObject.name);
    }
    
    void OnMouseExit()
    {
        txt.SetActive(false);
        Debug.Log("Мышка ушла с " + gameObject.name);
    }
}