using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueTutorial : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueName;
    private string[] sentences;

    private string[] names;
    public Image dialogueImage;

    public Sprite[] nameImage;
    private Sprite[] images;

    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;

    public AudioClip typingSound;

    private AudioSource audioSource;

    private Coroutine typingCoroutine;

    private string currentSentence;

    void Start()
    {
        dialoguePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GlobalVariables.waveCounter == 0)
        {
            sentences = new string[] { "HEEEY! WELCOME TO THE TUTORIAL-",
                "HEY, SAMIRA~",
                "OH. IT'S YOU...",
                "JUST TRY HOLDING *Z* OR *L* TO SEE IF YOUR *SHOOTING* IS WORKING ALREADY",
                 };
            images = new Sprite[]
            {
                Resources.Load<Sprite>("sprites/Samira smug"),
                Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"),
                Resources.Load<Sprite>("sprites/Samira reluctant"),
                Resources.Load<Sprite>("sprites/Samira tsundere"),
            };
            names = new string[]{
                "SAMIRA",
                "MAVERICK",
                "SAMIRA",
                "SAMIRA"
            };
            colors = new Color[] {
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue
                };
            GlobalVariables.waveCounter += 1;
            StartDialogue();
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.waveCounter == 2 ||
            Input.GetKey(KeyCode.L) && GlobalVariables.waveCounter == 2)
        {
            GlobalVariables.waveCounter += 1;
        }
        else if (GlobalVariables.waveCounter == 3)
        {
            sentences = new string[] { "GREAT! YOU KNOW HOW TO DO SOMETHING USEFUL!",
                "YOU'RE SWEET, MY DEAR SAMIRA... I CAN TELL",
                "NOW... YOU GOTTA LEARN ABOUT THE FOCUS MODE",
                "ONCE YOU HOLD *SHIFT* IT ALLOWS YOU TO *SLOW DOWN AND MAKE YOUR BULLETS WIDER*",
                "I COULD HAVE FIGUR-",
                "JUST HOLD THAT DAMN *SHIFT* TO *ACTIVATE THE FOCUS MODE*" };
            images = new Sprite[]
            {
                Resources.Load<Sprite>("sprites/Samira cheeky"),
                Resources.Load<Sprite>("sprites/Maverick worried"),
                Resources.Load<Sprite>("sprites/Samira thinking"),
                Resources.Load<Sprite>("sprites/Samira cheeky (OuO)"),
                Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"),
                Resources.Load<Sprite>("sprites/Samira pissed-off")
            };
            names = new string[]{
                "SAMIRA",
                "MAVERICK",
                "SAMIRA",
                "SAMIRA",
                "MAVERICK",
                "SAMIRA"
            };
            colors = new Color[] {
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue
                };
            GlobalVariables.waveCounter += 1;
            StartDialogue();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && GlobalVariables.waveCounter == 5 ||
            Input.GetKey(KeyCode.RightShift) && GlobalVariables.waveCounter == 5)
        {
            GlobalVariables.waveCounter += 1;
            Debug.Log(GlobalVariables.waveCounter);
        }
        else if (GlobalVariables.waveCounter == 6)
        {
            sentences = new string[] { "I'M ACTUALLY SURPRISED THAT YOU HAVEN'T MESSED EVERYTHING UP YET",
                "AWW~ CAN I GO NOW?",
                "SHUT UP",
                "ABOUT ENEMIES, I KNOW A BIT. . .",
                "YOU SHOULD AT LEAST KNOW THAT *YOU CAN LITERALLY MOVE THE ENEMY PROJECTILES*",
                "SO... DOES THAT MEAN *I CAN ACTUALLY THROW THE ENEMY PROJECTILES OUTSIDE THE SCREEN*?",
                "YES, USE IT TO DODGE ENEMIES ATTACKS",
                "SO GIVE IT A TRY ALREADY! DESTROY THE ENEMY, YOU DORK!",
                "CHILLAX... I'M WORKING ON IT"
            };
            images = new Sprite[]
            {
                Resources.Load<Sprite>("sprites/Samira tsundere"),
                Resources.Load<Sprite>("sprites/Maverick cheeky"),
                Resources.Load<Sprite>("sprites/Samira angry"),
                Resources.Load<Sprite>("sprites/Maverick worried talkin'"),
                Resources.Load<Sprite>("sprites/Samira reluctant"),
                Resources.Load<Sprite>("sprites/Maverick surprised"),
                Resources.Load<Sprite>("sprites/Samira reluctant"),
                Resources.Load<Sprite>("sprites/Samira pissed-off"),
                Resources.Load<Sprite>("sprites/Maverick angry")
            };
            names = new string[]{
                "SAMIRA",
                "MAVERICK",
                "SAMIRA",
                "MAVERICK",
                "SAMIRA",
                "MAVERICK",
                "SAMIRA",
                "SAMIRA",
                "MAVERICK"
            };
            colors = new Color[] {
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue
                };
            GlobalVariables.waveCounter += 1;
            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 10)
        {
            sentences = new string[] { "CONGRATULATIONS~!",
                "YOU JUST MANAGED TO COMPLETE THE TUTORIAL!",
                "I GOTTA SAY, YOU ARE-",
                "JUST GET BACK TO THE MENU ALREADY" };
            images = new Sprite[]
            {
                Resources.Load<Sprite>("sprites/Samira cheeky (OwO)"),
                Resources.Load<Sprite>("sprites/Samira cheeky (OuO)"),
                Resources.Load<Sprite>("sprites/Maverick proud talkin'"),
                Resources.Load<Sprite>("sprites/Samira angry"),
            };
            names = new string[]{
                "SAMIRA",
                "SAMIRA",
                "MAVERICK",
                "SAMIRA"
            };
            colors = new Color[] {
                lightBlue,
                lightBlue,
                lightBlue,
                lightBlue
                };
            GlobalVariables.waveCounter += 1;
            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 12 && Input.GetKey(KeyCode.Return) ||
            GlobalVariables.waveCounter == 12 && Input.GetKey(KeyCode.Z) ||
            GlobalVariables.waveCounter == 12 && Input.GetKey(KeyCode.L)||
            GlobalVariables.waveCounter == 12 && Input.GetKey(KeyCode.C))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (dialoguePanel.activeInHierarchy &&
        (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L)||Input.GetKey(KeyCode.C)))
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
            dialogueText.text = currentSentence;
        }
        else
        {
            index++;
            if (index < sentences.Length)
            {
                currentSentence = sentences[index];
                dialogueImage.sprite = images[index];
                dialogueName.text = names[index];
                dialogueName.color = colors[index];
                typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
            }
            else
            {
                EndDialogue();
            }
        }
    }
    }

    public void StartDialogue()
    {
        index = 0;
        dialoguePanel.SetActive(true);
        currentSentence = sentences[index];
        dialogueName.text = names[index];
        dialogueImage.sprite = images[index];
        dialogueName.color = colors[index];
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            audioSource.Play();
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        GlobalVariables.waveCounter += 1;
    }
}
