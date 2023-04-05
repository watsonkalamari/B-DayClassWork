using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayText : MonoBehaviour
{
    /*This 
     is
    a
    mulit-line
    comment
    */

    public string firstString = "Hello, World!";
    public int age = 25;
    public decimal moneyMoneyMoney = 267.99M;

    public string nintyNine = "99";
    public int ninty_Nine = 99;


    public void DisplayText ()
    {
        Debug.Log(age);
    }
}
