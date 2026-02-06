using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    [SerializeField] TMP_Text Ans;
    NormalDoorBehavior normalDoorBehavior;
    public bool openTheDoor = false;
     public string  answer;
    
    void Start()
    {
        openTheDoor = false;
    }

    void Update()
    {
        
    }

    public void Number(int number)
    {
        Ans.text += number.ToString();
    }
    public void Execute()
    {
        if (Ans.text == answer)
        {
            openTheDoor = true;
        }
        else
        {
            Ans.text = "NOPE";
        }
    }
}
