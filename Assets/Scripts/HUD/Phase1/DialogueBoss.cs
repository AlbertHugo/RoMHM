using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DialogueBoss : MonoBehaviour
{
    public GameObject bossBar;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;
    public TextMeshProUGUI dialogueName;

    private string[] names;
    private string[] sentences;
    private Sprite[] images;

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    void Start()
    {
        images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Kleber annoyed"), //1
            Resources.Load<Sprite>("sprites/Kleber annoyed talkin'"), //2
            Resources.Load<Sprite>("sprites/Maverick convinced talkin'"), //3
            Resources.Load<Sprite>("sprites/Kleber cheeky talkin'"), //4
            Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"), //5
            Resources.Load<Sprite>("sprites/Maverick angry"), //6
            Resources.Load<Sprite>("sprites/Kleber default"), //7
            Resources.Load<Sprite>("sprites/Kleber annoyed"), //8
            Resources.Load<Sprite>("sprites/Kleber excited talkin'"), //9
            Resources.Load<Sprite>("sprites/Maverick angry talkin'"), //10
            Resources.Load<Sprite>("sprites/Maverick furious"), //11
            Resources.Load<Sprite>("sprites/Maverick furious talkin'"), //12
            Resources.Load<Sprite>("sprites/Maverick worried"), //13
            Resources.Load<Sprite>("sprites/Maverick worried talkin'"), //14
            Resources.Load<Sprite>("sprites/Maverick determined talkin'") //15
        };
        audioSource = GetComponent<AudioSource>();
        dialoguePanel.SetActive(false);
        names = new string[]{
                "KLEBER",//1
                "KLEBER",//2
                "MAVERICK",//3
                "KLEBER",//4
                "MAVERICK",//5
                "MAVERICK",//6
                "KLEBER",//7
                "KLEBER",//8
                "KLEBER",//9
                "MAVERICK",//10
                "MAVERICK",//11
                "MAVERICK",//12
                "MAVERICK",//13
                "MAVERICK",//14
                "MAVERICK"//15
            };
        colors = new Color[]{
                Color.red,//1
                Color.red,//2
                lightBlue,//3
                Color.red,//4
                lightBlue,//5
                lightBlue,//6
                Color.red,//7
                Color.red,//8
                Color.red,//9
                lightBlue,//10
                lightBlue,//11
                lightBlue,//12
                lightBlue,//13
                lightBlue,//14
                lightBlue//15
            };
    }

    void Update()
    {
        if (GlobalVariables.bossCounter == 50 && sentences == null)
        {
            sentences = new string[] {
            "H-HOW....?", //1
            "I.... LOST...", //2
            "WHERE'S ALL THAT MACHO FROM AN HOUR AGO, HUH~?", //3
            "UGH. . . I'M GONNA-", //4
            "*GONNA* WHAT, BUD~?", //5
            "IT'S OVER FOR YOU.", //6
            ". . .", //7
            ". . . . .", //8
            "YOU WILL NEVER TAKE ME ALIVE, SHRIMP! AHAHAHAHAHAHAHAHAHAHAHAHAH!", //9
            "OH NO. YOU DON-", //10
            ". . .", //11
            "I'VE LOST HIS SIGHT....", //12
            "SIGH. . .", //13
            "WELL... THAT'S A *FUTURE ME* PROBLEM", //14
            "THERE'S STILL WORK TO BE DONE" //15
        };

            StartDialogue();
        }

        if (dialoguePanel.activeInHierarchy &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L) || Input.GetKey(KeyCode.C)))
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
        bossBar.SetActive(false);
        index = 0;
        dialoguePanel.SetActive(true);
        currentSentence = sentences[index];
        dialogueImage.sprite = images[index];
        dialogueName.text = names[index];
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
        sentences = null;
        SceneManager.LoadScene("Victory");
    }
}
