using UnityEngine;
using System.Collections;

public class SpineColorControl : MonoBehaviour {

	public SkeletonAnimation skeletonAnimation;
	
	private bool colorChange;
	private bool alphaChange;

	public void ChangeColor(){
		if(colorChange){
			skeletonAnimation.skeleton.r = 1.0f;
			skeletonAnimation.skeleton.g = 1.0f;
			skeletonAnimation.skeleton.b = 1.0f;
		}else{
			skeletonAnimation.skeleton.r = 0.7f;
			skeletonAnimation.skeleton.g = 0.5f;
			skeletonAnimation.skeleton.b = 0.3f;
		}
		colorChange = !colorChange;
	}

	public void ChangeAlpha(){
		if(alphaChange){
			skeletonAnimation.skeleton.a = 0.8f;
		}else{
			skeletonAnimation.skeleton.a = 0.2f;
		}
		alphaChange = !alphaChange;
	}
}
