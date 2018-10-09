#pragma strict

public var LoadingImage:GameObject;
var async:AsyncOperation;
var loadingImage:GameObject;


function Awake ()
{
    LeaveScene ();
}
 
function LeaveScene ()
{
    yield WaitForSeconds (.0002f);
	async = Application.LoadLevelAsync (1);
	
	if (!async.isDone) 
		{
			loadingImage = Instantiate(LoadingImage, LoadingImage.transform.position, LoadingImage.transform.rotation);	
			loadingImage.transform.SetParent (GameObject.Find("Canvas").transform,false);
		}
}