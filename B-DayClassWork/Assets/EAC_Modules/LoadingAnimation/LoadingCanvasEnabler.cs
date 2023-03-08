using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalSalmon;
using DigitalSalmon.C360;

public class LoadingCanvasEnabler : MonoBehaviour
{
    // Start is called before the first frame update
	public FadeTransition fade;
	public GameObject LoadingCanvas;
    void Start()
    {
        LoadingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(LoadingCanvas != null){
			if (fade.IsTransitioning){
				LoadingCanvas.SetActive(true);
			}
			else{
				LoadingCanvas.SetActive(false);
			}
		}
	}
}
