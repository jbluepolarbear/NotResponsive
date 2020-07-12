using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public AudioSource AudioSourceDialogue;
    public AudioSource AudioSourceSoundEffectsAlerts;
    public AudioSource AudioSourceSoundEffectsUI;
    public AudioSource AudioSourceSoundEffectsJetRight;
    public AudioSource AudioSourceSoundEffectsJetLeft;
    public AudioClip AirJet;
    public AudioClip BreathSlow;
    public AudioClip Alert;
    public Image glassImage;

    public AudioClip[] JonesClips;
    public AudioClip[] AndreaClips;
    public AlertController AlertController;
    public AstroChair AstroChair;

    public HUD HUD;
    public Canvas HelmetUI;
    public Image FadeImage;
    
    public float BreathTime = 3.75f;
    // Start is called before the first frame update
    void Start()
    {
        PlayAlert();
        StartCoroutine(GameFlow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameFlow()
    {
        //Start
        yield return StartGame();
        //Intro
        yield return Intro();
        // Danger Alert
        // No Hud Input
        // Audio Sequences
        //Reset System
        yield return ResetSystemFirst();
        //First Slow Down
        yield return FirstSlowDown();
        //Second Slow Down Fail
        yield return ResetSystemSecond();
        //Second Slow Down
        yield return SecondSlowdown();
        //Finish
        yield return FinalSlow();
        //End
        yield return EndGame();
    }

    IEnumerator StartGame()
    {
        float startTime = Time.time;
        float timeToTake = 1.0f;
        while (Time.time - startTime < timeToTake)
        {
            float dt = (Time.time - startTime) / timeToTake;
            Color lcolor = FadeImage.color;
            lcolor.a = Mathf.Lerp(1.0f, 0.0f, dt);
            FadeImage.color = lcolor;
            if (dt >= 0.35f)
            {
                HelmetUI.gameObject.SetActive(true);
            }
            yield return null;
        }
        Color color = FadeImage.color;
        color.a = 0.0f;
        FadeImage.color = color;
    }

    IEnumerator Intro()
    {
        HUD.SetAllowInput(false);
        yield return SlowBreathCoroutine();
        yield return WaitForDialogue();
        PlayDialogue(JonesClips[0]);
        yield return WaitForDialogue();
        yield return SlowBreathCoroutine();
        yield return WaitForDialogue();
        PlayDialogue(AndreaClips[0]);
        yield return WaitForDialogue();
        PlayDialogue(JonesClips[1]);
        yield return WaitForDialogue();
        PlayDialogue(AndreaClips[1]);
        yield return WaitForDialogue();
        PlayDialogue(JonesClips[2]);
        yield return WaitForDialogue();
        PlayDialogue(AndreaClips[2]);
        yield return WaitForDialogue();
        PlayDialogue(JonesClips[3]);
        yield return WaitForDialogue();
    }

    IEnumerator ResetSystemFirst()
    {
        HUD.SetAllowInput(false);
        HUD.SetOverrideControls(true);
        PlayDialogue(AndreaClips[3]);
        yield return WaitForDialogue();
        
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HUD.SetLeftMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HUD.SetLeftMouseDown(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                HUD.SetRightMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                HUD.SetRightMouseDown(false);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                HUD.SetWButtonDown(true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                HUD.SetWButtonDown(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                HUD.SetQButtonDown(true);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                HUD.SetQButtonDown(false);
            }

            if (HUD.AllDown())
            {
                break;
            }
            yield return null;
        }
        PlayDialogue(JonesClips[4]);
        yield return WaitForDialogue();
        yield return new WaitForSeconds(0.25f);
        Breathe();
        for (int i = 0; i < 4; ++i)
        {
            HUD.SetLeftMouseDown(false);
            HUD.SetRightMouseDown(false);
            HUD.SetWButtonDown(false);
            HUD.SetQButtonDown(false);
            yield return new WaitForSeconds(0.5f);
            HUD.SetLeftMouseDown(true);
            HUD.SetRightMouseDown(true);
            HUD.SetWButtonDown(true);
            HUD.SetQButtonDown(true);
            yield return new WaitForSeconds(0.5f);
        }
        HUD.SetAllowInput(true);
        AlertController.AlertStatus = AlertController.AlertEnum.Warning;
        for (int i = 0; i < 4; ++i)
        {
            HUD.SetLeftMouseDown(false);
            HUD.SetRightMouseDown(false);
            HUD.SetWButtonDown(false);
            HUD.SetQButtonDown(false);
            yield return new WaitForSeconds(0.25f);
            HUD.SetLeftMouseDown(true);
            HUD.SetRightMouseDown(true);
            HUD.SetWButtonDown(true);
            HUD.SetQButtonDown(true);
            yield return new WaitForSeconds(0.25f);
        }
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
        yield return WaitForDialogue();
    }

    IEnumerator FirstSlowDown()
    {
        AlertController.AlertStatus = AlertController.AlertEnum.Warning;
        HUD.SetAllowInput(true);
        HUD.SetOverrideControls(true);
        PlayDialogue(AndreaClips[4]);
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                HUD.SetRightMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                HUD.SetRightMouseDown(false);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                HUD.SetWButtonDown(true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                HUD.SetWButtonDown(false);
            }

            if (HUD.WButtonDown && HUD.RightMouseDown)
            {
                break;
            }
            yield return null;
        }
        yield return WaitForDialogue();
        //
        // PlayAirJet Right
        //
        PlayRightAirJet();
        AstroChair.SlowDown1();
        yield return new WaitForSeconds(0.5f);
        PlayDialogue(JonesClips[5]);
        yield return WaitForDialogue();
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
        StopAirJets();
        PlayDialogue(AndreaClips[5]);
        yield return WaitForDialogue();
        //
        // Stop PlayAirJet Right
        //
        yield return AstroChair.WaitForMovement();
        Breathe();
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HUD.SetLeftMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HUD.SetLeftMouseDown(false);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                HUD.SetQButtonDown(true);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                HUD.SetQButtonDown(false);
            }

            if (HUD.QButtonDown && HUD.LeftMouseDown)
            {
                break;
            }
            yield return null;
        }
        PlayLeftAirJet();
        AstroChair.SpeedUp1();
        AlertController.AlertStatus = AlertController.AlertEnum.Danger;
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
        HUD.SetOverrideControls(false);
        HUD.SetAllowInput(false);
        yield return AstroChair.WaitForMovement();
        yield return WaitForDialogue();
        //
        // PlayAirJet Left
        //
        yield return new WaitForSeconds(0.5f);
        PlayDialogue(AndreaClips[6]);
        yield return WaitForDialogue();
        PlayDialogue(JonesClips[6]);
        yield return WaitForDialogue();
    }
    
    IEnumerator ResetSystemSecond()
    {
        AlertController.AlertStatus = AlertController.AlertEnum.Danger;
        HUD.SetAllowInput(false);
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
        HUD.SetOverrideControls(true);
        PlayDialogue(AndreaClips[7]);
        yield return WaitForDialogue();
        PlayDialogue(JonesClips[7]);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HUD.SetLeftMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HUD.SetLeftMouseDown(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                HUD.SetRightMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                HUD.SetRightMouseDown(false);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                HUD.SetWButtonDown(true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                HUD.SetWButtonDown(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                HUD.SetQButtonDown(true);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                HUD.SetQButtonDown(false);
            }

            if (HUD.AllDown())
            {
                break;
            }
            yield return null;
        }
        yield return WaitForDialogue();
        
        Breathe();
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 4; ++i)
        {
            HUD.SetLeftMouseDown(false);
            HUD.SetRightMouseDown(false);
            HUD.SetWButtonDown(false);
            HUD.SetQButtonDown(false);
            yield return new WaitForSeconds(0.5f);
            HUD.SetLeftMouseDown(true);
            HUD.SetRightMouseDown(true);
            HUD.SetWButtonDown(true);
            HUD.SetQButtonDown(true);
            yield return new WaitForSeconds(0.5f);
        }
        HUD.SetAllowInput(true);
        AlertController.AlertStatus = AlertController.AlertEnum.Warning;
        for (int i = 0; i < 4; ++i)
        {
            HUD.SetLeftMouseDown(false);
            HUD.SetRightMouseDown(false);
            HUD.SetWButtonDown(false);
            HUD.SetQButtonDown(false);
            yield return new WaitForSeconds(0.25f);
            HUD.SetLeftMouseDown(true);
            HUD.SetRightMouseDown(true);
            HUD.SetWButtonDown(true);
            HUD.SetQButtonDown(true);
            yield return new WaitForSeconds(0.25f);
        }
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
        yield return WaitForDialogue();
        StopAirJets();
    }

    IEnumerator SecondSlowdown()
    {
        AlertController.AlertStatus = AlertController.AlertEnum.Warning;
        HUD.SetOverrideControls(true);
        HUD.SetAllowInput(true);
        PlayDialogue(AndreaClips[8]);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HUD.SetLeftMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HUD.SetLeftMouseDown(false);
            }

            if (HUD.LeftMouseDown)
            {
                HUD.SetLeftMouseDown(true);
                break;
            }
            yield return null;
        }
        //
        // PlayAirJet Left
        //
        PlayLeftAirJet();
        AstroChair.SlowDown2();
        yield return WaitForDialogue();
        yield return new WaitForSeconds(2.0f);
        HUD.SetLeftMouseDown(false);
        StopAirJets();
        yield return AstroChair.WaitForMovement();
        PlayDialogue(AndreaClips[9]);
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                HUD.SetRightMouseDown(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                HUD.SetRightMouseDown(false);
            }

            if (HUD.RightMouseDown)
            {
                HUD.SetRightMouseDown(true);
                break;
            }
            yield return null;
        }
        //
        // PlayAirJet Right
        //
        PlayRightAirJet();
        AstroChair.SlowDown3();
        yield return WaitForDialogue();
        yield return new WaitForSeconds(2.0f);
        yield return AstroChair.WaitForMovement();
        HUD.SetLeftMouseDown(false);
        PlayDialogue(JonesClips[8]);
        yield return WaitForDialogue();
        StopAirJets();
        yield return new WaitForSeconds(2.0f);
        PlayDialogue(AndreaClips[10]);
        yield return WaitForDialogue();
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
    }

    IEnumerator FinalSlow()
    {
        HUD.SetAllowInput(true);
        HUD.SetLeftMouseDown(false);
        HUD.SetRightMouseDown(false);
        HUD.SetWButtonDown(false);
        HUD.SetQButtonDown(false);
        HUD.SetOverrideControls(false);
        yield return new WaitForSeconds(1.0f);
        AlertController.AlertStatus = AlertController.AlertEnum.Info;
        StopAlert();
        PlayDialogue(JonesClips[9]);
        yield return WaitForDialogue();
        yield return new WaitForSeconds(1.0f);
        PlayDialogue(AndreaClips[11]);
        yield return WaitForDialogue();
    }

    IEnumerator EndGame()
    {
        float startTime = Time.time;
        float timeToTake = 1.0f;
        while (Time.time - startTime < timeToTake)
        {
            float dt = (Time.time - startTime) / timeToTake;
            Color lcolor = FadeImage.color;
            lcolor.a = Mathf.Lerp(0.0f, 1.0f, dt);
            FadeImage.color = lcolor;
            if (dt >= 0.35f)
            {
                HelmetUI.gameObject.SetActive(false);
            }
            yield return null;
        }
        Color color = FadeImage.color;
        color.a = 1.0f;
        FadeImage.color = color;
        SceneManager.LoadScene("Menu");
    }

    IEnumerator WaitForDialogue()
    {
        while (AudioSourceDialogue.isPlaying)
        {
            yield return null;
        }
    }

    void PlayDialogue(AudioClip clip)
    {
        AudioSourceDialogue.clip = clip;
        AudioSourceDialogue.loop = false;
        AudioSourceDialogue.Play();
    }

    void StopDialogue()
    {
        AudioSourceDialogue.Stop();
        AudioSourceDialogue.clip = null;
    }

    void PlayLeftAirJet()
    {
        AudioSourceSoundEffectsJetLeft.clip = AirJet;
        AudioSourceSoundEffectsJetLeft.volume = 0.25f;
        AudioSourceSoundEffectsJetLeft.loop = true;
        AudioSourceSoundEffectsJetLeft.Play();
    }

    void PlayRightAirJet()
    {
        AudioSourceSoundEffectsJetRight.clip = AirJet;
        AudioSourceSoundEffectsJetRight.volume = 0.25f;
        AudioSourceSoundEffectsJetRight.loop = true;
        AudioSourceSoundEffectsJetRight.Play();
    }

    void StopAirJets()
    {
        AudioSourceSoundEffectsJetLeft.Stop();
        AudioSourceSoundEffectsJetLeft.clip = null;
        AudioSourceSoundEffectsJetRight.Stop();
        AudioSourceSoundEffectsJetRight.clip = null;
    }

    void PlayAlert()
    {
        AudioSourceSoundEffectsAlerts.clip = Alert;
        AudioSourceSoundEffectsAlerts.volume = 0.05f;
        AudioSourceSoundEffectsAlerts.loop = true;
        AudioSourceSoundEffectsAlerts.Play();
    }

    void StopAlert()
    {
        AudioSourceSoundEffectsAlerts.Stop();
        AudioSourceSoundEffectsAlerts.clip = null;
    }

    void Breathe()
    {
        StartCoroutine(SlowBreathCoroutine());
    }

    IEnumerator SlowBreathCoroutine()
    {
        PlayDialogue(BreathSlow);

        for (int i = 0; i < 4; ++i)
        {
            yield return new WaitForSeconds(0.85f);
            yield return BreatheCoroutine(1.15f);
        }
    }
    
    IEnumerator BreatheCoroutine(float breathTime)
    {
        float startTime = Time.time;
        float breathe = 0.0f;
        float halfBreathTime = breathTime / 2.0f;
        do
        {
            float time = Time.time - startTime;
            if (time < halfBreathTime)
            {
                breathe = Mathf.Lerp(0.15f, 0.45f, time / halfBreathTime);
            }
            else
            {
                time -= halfBreathTime;
                breathe = Mathf.Lerp(0.45f, 0.15f, time / halfBreathTime);
            }
        
            glassImage.material.SetFloat("Breathe", breathe);
            yield return null;
        }
        while (Time.time - startTime < breathTime);
        
        glassImage.material.SetFloat("Breathe", 0.15f);
    }
}
