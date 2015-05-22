using UnityEngine;
using System.Collections;
using Spine;

public class SpineCreater : MonoBehaviour {
	// 画像ファイル
	public Texture2D texture;
	// atlasテキストファイル
	public TextAsset atlasText;
	// jsonテキストファイル
	public TextAsset jsonText;

	public void CreateSpineObject(){
		
		// 1.Spine/Skeletonのmaterialを生成
		Material material         	= new Material(Shader.Find("Spine/Skeleton"));
		material.mainTexture     	= texture;
		
		// 2.AtlasAssetを生成して、1のmaterialを紐づける
		AtlasAsset atlasAsset       = AtlasAsset.CreateInstance<AtlasAsset> ();
		atlasAsset.atlasFile        = atlasText;
		atlasAsset.materials        = new Material[1];
		atlasAsset.materials [0]    = material;
		
		// 3.SkeletonDataAssetを生成して、初期データを投入する
		SkeletonDataAsset dataAsset = SkeletonDataAsset.CreateInstance<SkeletonDataAsset> ();
		dataAsset.atlasAssets       = new AtlasAsset[1];
		dataAsset.atlasAssets [0]   = atlasAsset;
		dataAsset.skeletonJSON      = jsonText;
		dataAsset.fromAnimation     = new string[0];
		dataAsset.toAnimation       = new string[0];
		dataAsset.duration          = new float[0];
		dataAsset.defaultMix        = 0.2f;
		dataAsset.scale             = 1.0f;
		
		// 4.実際にUnityに配置するGameObjectを生成して、SkeletonAnimationをadd
		GameObject obj              = new GameObject ("SpineObject");
		SkeletonAnimation anim      = obj.AddComponent<SkeletonAnimation> ();
		MeshRenderer meshRenderer 	= obj.GetComponent<MeshRenderer>();

		meshRenderer.sortingLayerName = "spine";
		meshRenderer.sortingOrder = 0;
		// 5.SkeletonAnimationにSkeletonDataAssetを紐付け、skinName、reset(),animationNameを設定する。
		anim.skeletonDataAsset 		= dataAsset;
		
		// Reset()前に呼び出す必要あり。後に呼ぶとskinデータが反映されない
		anim.initialSkinName 		= "goblin";
		
		// Reset()時にDataAssetからAnimationデータを読みだして格納している
		anim.Reset ();

		// ループの設定はAnimation指定前じゃないと反映されない
		anim.loop = true;
		anim.AnimationName = "walk";

		// アニメ終了イベントをセット
		anim.state.Complete += OnCompleteSpineAnim;

		obj.transform.SetParent(this.gameObject.transform);
	}

	// アニメ終了イベント
	private void OnCompleteSpineAnim(Spine.AnimationState state , int trackIndex , int loopCount){
		Debug.Log(string.Format("state : {0} , trackIndex : {1} , loopCount : {2}" , state , trackIndex , loopCount));
	}
}
