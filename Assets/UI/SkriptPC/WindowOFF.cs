using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.UI;


public class WindowOFF : MonoBehaviour
{
    public GameObject[] runningProgram;
    public Image[] ProgramOpen;
    public int HowMatchOpen;
    public Sprite Defoult;
    

    public static WindowOFF windowOFF;
    public GameObject[] WindowProgramm;
    public Animator anim;
    public GameObject OffPanel;


    public GameObject GraficSeting;
    public GameObject AudioSeting;
    public GameObject ControllerSeting;

    private void Start()
    {
        if (windowOFF == null)
        {
            windowOFF = this;
        }
        else
        {
            Destroy(this);
        }

        GraficSeting.gameObject.SetActive(true);
        AudioSeting.gameObject.SetActive(false);
        ControllerSeting.gameObject.SetActive(false);

        OffPanel.gameObject.SetActive(false);
    }

    public void OpenOffPanel() 
    {
        OffPanel.gameObject.SetActive(true);
        anim.Play("OffPanel");
        
    }

    public void CloseOffPanel()
    {
        OffPanel.gameObject.SetActive(false);
    }
    public void DefouldSetingWindow(int _idProgramm)
    {
        WindowProgramm[_idProgramm].gameObject.SetActive(true);
        
        Debug.Log("chtonibudy");
    }

    public void GraficS()
    {
        GraficSeting.gameObject.SetActive(true);
        AudioSeting.gameObject.SetActive(false);
        ControllerSeting.gameObject.SetActive(false);
    }
    public void AudioS()
    {
        GraficSeting.gameObject.SetActive(false);
        AudioSeting.gameObject.SetActive(true);
        ControllerSeting.gameObject.SetActive(false);
    }
    public void ControllerS()
    {
        GraficSeting.gameObject.SetActive(false);
        AudioSeting.gameObject.SetActive(false);
        ControllerSeting.gameObject.SetActive(true);
    }
    public void Back()
    {
        UnderPanelClose();
        ButtonSeting.isOpen = false;
    }

    public void UnderPanelOpen(Sprite _sprite,int _idProgramm)
    {
        ProgramOpen[HowMatchOpen].sprite = _sprite;
        runningProgram[HowMatchOpen]= WindowProgramm[_idProgramm];
        HowMatchOpen++;
    }

    public void UnderPanelClose()
    {
        HowMatchOpen--;
        ProgramOpen[HowMatchOpen].sprite = Defoult;
        runningProgram[HowMatchOpen].SetActive(false);
    }
}
