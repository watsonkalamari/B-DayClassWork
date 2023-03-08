using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalSalmon.C360 {
	[AddComponentMenu("Complete 360 Tour/Input/Mouse Input Raycaster")]
	public class MouseInputRaycaster : UIInputRaycaster {
		[Header("Mouse Raycaster")]
		[SerializeField]
		protected bool clickToSubmit;
		
		protected override Vector2 GetInputPosition() => Input.mousePosition;


		protected void Update() {

			
			if (Input.GetMouseButtonDown(0) && clickToSubmit) {

				Debug.Log("CLICKED: " + CurrentResult.gameObject.name);

				//SUBMIT HOTSPOT
				if (CurrentInteractable != null) {
					CurrentInteractable.Submit();
				}





				//CLOSE X TEXT POPUP
				if (CurrentResult.gameObject != null && (CurrentResult.gameObject.transform.parent.Find("Icon/X Image") != null)) {
					if((CurrentResult.gameObject.transform.parent.Find("Icon/X Image").GetComponent<Image>().color.a == 1)){
						DeactiveLastInteractible();
					}
				}


				//CLOSE X IMAGE POPUP
				if (CurrentInteractable != null)
				{

					if (CurrentResult.gameObject.name == "Image")
					{

						if (CurrentResult.gameObject.transform.parent.parent.parent.Find("Icon/X Image") != null && CurrentResult.gameObject.transform.parent.parent.parent.Find("Icon/X Image").gameObject.GetComponent<Image>().color.a == 1)
						{

							DeactiveLastInteractible();

						}

					}
					else if (CurrentResult.gameObject.name == "Background")
					{

						if (CurrentResult.gameObject.transform.parent.parent.Find("Icon/X Image") != null && CurrentResult.gameObject.transform.parent.parent.Find("Icon/X Image").gameObject.GetComponent<Image>().color.a == 1)
						{

							DeactiveLastInteractible();

						}

					}

				}






				//OPEN X TEXT POPUP
				if (CurrentInteractable != null && CurrentResult.gameObject.transform.parent.Find("Icon/X Image") != null && CurrentResult.gameObject.transform.parent.Find("Icon/X Image").gameObject.GetComponent<Image>().color.a != 1) {
					interactionSequence.Coroutine(InteractionCoroutine());
				}




				//OPEN X IMAGE POPUP
				if(CurrentInteractable != null)
                {
					
					if(CurrentResult.gameObject.name == "Image")
                    {

						if(CurrentResult.gameObject.transform.parent.parent.parent.Find("Icon/X Image") != null && CurrentResult.gameObject.transform.parent.parent.parent.Find("Icon/X Image").gameObject.GetComponent<Image>().color.a != 1)
                        {

							interactionSequence.Coroutine(InteractionCoroutine());

						}

                    }else if (CurrentResult.gameObject.name == "Overlay")
					{

						if (CurrentResult.gameObject.transform.parent.parent.Find("Icon/X Image") != null && CurrentResult.gameObject.transform.parent.parent.Find("Icon/X Image").gameObject.GetComponent<Image>().color.a != 1)
						{

							interactionSequence.Coroutine(InteractionCoroutine());

						}

					}

				}



			}



		}

	}
}