using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowMenu : MonoBehaviour
{
    private bool isAllowedToInput = true;

    private bool isShowedMenu = false;
    private bool isShowedSettings = false;
    private bool isShowedExitDialog = false;

    public CommentEffect _commentEffect;
    public GameObject OverflowMenuObject;
    public GameObject SettingsWindowObject;
    public GameObject ExitDialogObject;

    private void Start()
    {
        OverflowMenuObject.SetActive(false);
        SettingsWindowObject.SetActive(false);
        ExitDialogObject.SetActive(false);
    }

    private void Update()
    {
        if (isAllowedToInput)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isShowedMenu) OpenMenu();
                else
                {
                    if (isShowedSettings) CloseSettings();
                    else if (isShowedExitDialog) CloseExitDialog();
                    else CloseMenu();
                }
            }
        }
    }

    /* メニューを開く */
    private void OpenMenu()
    {
        isShowedMenu = true;
        OverflowMenuObject.SetActive(true);
    }

    /* メニューを閉じる */
    private void CloseMenu()
    {
        isShowedMenu = false;
        OverflowMenuObject.SetActive(false);
    }

    /* 設定を開く */
    private void OpenSettings()
    {
        isShowedSettings = true;
        SettingsWindowObject.SetActive(true);

        if (isShowedExitDialog) CloseExitDialog();
    }

    /* 設定を閉じる */
    private void CloseSettings()
    {
        isShowedSettings = false;
        SettingsWindowObject.SetActive(false);
    }

    /* 終了確認ダイアログを開く */
    private void OpenExitDialog()
    {
        isShowedExitDialog = true;
        ExitDialogObject.SetActive(true);

        if (isShowedMenu) CloseSettings();
    }

    /* 終了確認ダイアログを閉じる */
    private void CloseExitDialog()
    {
        isShowedExitDialog = false;
        ExitDialogObject.SetActive(false);
    }

    /* アプリケーションを終了する */
    private void ApplicationExit()
    {
        _commentEffect.ApplicationExit();
    }


    /*  クリックイベント */

    /* 設定ボタンを押した */
    public void OnClick_SettingsButton()
    {
        if (!isShowedSettings) OpenSettings();
        else CloseSettings();
    }

    /* 終了ボタンを押した */
    public void OnClick_ExitButton()
    {
        OpenExitDialog();
    }


    /* ExitDialogのYesを押した */
    public void OnClick_ExitDialog_Yes()
    {
        ApplicationExit();
    }

    /* ExitDialogのNoを押した */
    public void OnClick_ExitDialog_No()
    {
        CloseExitDialog();
    }

}
