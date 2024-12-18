using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasLoadingScene : UICanvas
{
    [SerializeField] Image loadingImage;
    int randomNum;
    public override void Setup()
    {
        base.Setup();
        ChangeAnim(Constant.ANIM_LOADING);
        randomNum = Random.Range(2, 6);
    }
    public override void Open()
    {
        base.Open();
        Invoke(nameof(ResetAll), 2f);
    }
    private void ResetAll()
    {
        LevelManager.Ins.OnDespawnAll();
        LevelManager.Ins.ReloadLevel();
        LevelManager.Ins.OnInit();
    }    
    private void Update()
    {
        loadingImage.transform.Rotate(0, 0, -10, Space.World);
        if(this.gameObject.activeSelf)
        {
            Invoke(nameof(EndLoading), randomNum);
        }
    }
    private void EndLoading()
    {
        ChangeAnim(Constant.ANIM_FADE_ENDLOADING);
        Invoke(nameof(CloseUI), 2f);
    }
    private void CloseUI()
    {
        Close(0);
    }
}
