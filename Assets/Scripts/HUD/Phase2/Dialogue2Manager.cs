using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Dialogue2Manager : MonoBehaviour
{
    public GameObject spawner;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    public TextMeshProUGUI dialogueName;
    private string[] names;
    private string[] sentences;
    private Sprite[] images;
    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private bool dialogueHappened = true;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (GlobalVariables.waveCounter == 0 && sentences == null&&dialogueHappened)
        {
            dialogueHappened=false;
            sentences = new string[] {
            "IT SEEMS THAT YOU'VE FINALLY ARRIVED AT THE 2ND DISTRICT", //1
            "NO CAP", //2
            "THO.... WHAT THE HECK HAPPENED HERE...?", //3
            "MAV, YOU GOTTA LISTEN ME. LIKE, FOR REAL THIS TIME", //4
            "THE ONE BEFORE YOU REPORTED US SOMETHING IMPORTANT", //5
            "THE ENEMIES ARE BUILT DIFFERENT HERE...", //6
            "THEY FOUND WAYS TO *COUNTER YOUR TELEKINESIS*, AND ARE *REALLY VENGEFUL*", //7
            "I MADE YOU A ROCKET TO HELP WITH THIS AND *DESTROY THEIR PROJECTILES*", //8
            "YOU BETTER USE IT!", //9
            "HEH. DON'T WORRY, MY DEAR SAMIRA", //10
            "I'VE GOT THIS", //11
            "BE CAREFUL... PLEASE" //12
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Samira tsundere"), //1
            Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"), //2
            Resources.Load<Sprite>("sprites/Maverick worried talkin'"), //3
            Resources.Load<Sprite>("sprites/Samira thinking"), //4
            Resources.Load<Sprite>("sprites/Samira worried"), //5
            Resources.Load<Sprite>("sprites/Samira angry"), //6
            Resources.Load<Sprite>("sprites/Samira worried"), //7
            Resources.Load<Sprite>("sprites/Samira cheeky (OwO)"), //8
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //9
            Resources.Load<Sprite>("sprites/Maverick smug talkin'"), //10
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"), //11
            Resources.Load<Sprite>("sprites/Samira reluctant") //12
        };        
            names = new string[]{
                "SAMIRA",//1
                "MAVERICK",//2
                "MAVERICK",//3
                "SAMIRA",//4
                "SAMIRA",//5
                "SAMIRA",//6
                "SAMIRA",//7
                "SAMIRA",//8
                "SAMIRA",//9
                "MAVERICK",
                "MAVERICK",
                "SAMIRA"
            };
            colors = new Color[]{
                lightBlue,//1
                lightBlue,//2
                lightBlue,//3
                lightBlue,//4
                lightBlue,//5
                lightBlue,//6
                lightBlue,//7
                lightBlue,//8
                lightBlue,//9
                lightBlue,
                lightBlue,
                lightBlue
            };

            StartDialogue();
        }
        if(GlobalVariables.waveCounter == 16 &&sentences == null)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "MY MY~", //1
            "I REALLY WASN'T EXPECTING A PRETTY FACE LIKE YOURS TO PAY ME A VISIT~", //2
            "OH...", //3
            "MAV...?", //4
            "OH HO WOW...~", //5
            "MAVERICK!", //6
            "WHAT MAY I CALL YOU, HANDSOME~?", //7
            "MAVERICK, MA'AM", //8
            "OH. MY. DAMN. GOD.", //9
            "MAVERICK, I AM SERIOUS! SHE'LL KILL YOU, YOU DORK!", //10
            "OH, DON'T SPOIL THE FUN", //11
            "YOU'RE JUST JEALOUS OF HIM", //12
            "AFTER ALL, IT COULD BE YOU PILOTING THE AIRSHIP", //13
            "...", //14
            "YOU TALK AS IF IT WAS YOU WHO TOOK HER DOWN", //15
            "BUT LET'S BE HONEST, YOU'RE JUST TOO WEAK FOR THAT", //16
            "OH? THEN YOU CAN GO AHEAD AND TRY DEFEATING ME", //17
            "BUT UNLIKE YOUR FRIEND...", //18
            "YOU WON'T STAY ALIVE AFTER YOUR CRASH!", //19
            "AHUHUHUHU~ I'LL TEACH YOU AND YOUR FRIEND NOT TO MESS WITH US AGAIN", //20
            "GO AND CRUSH HER, MAVERICK!" //21
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Garance convinced"), //1
            Resources.Load<Sprite>("sprites/Garance Cheeky"), //2
            Resources.Load<Sprite>("sprites/Maverick surprised"), //3
            Resources.Load<Sprite>("sprites/Samira worried"), //4
            Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"), //5
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //6
            Resources.Load<Sprite>("sprites/Garance Cheeky"), //7
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"), //8
            Resources.Load<Sprite>("sprites/Samira angry"), //9
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //10
            Resources.Load<Sprite>("sprites/Garance emotional..."), //11
            Resources.Load<Sprite>("sprites/Garance cheeky"), //12
            Resources.Load<Sprite>("sprites/Garance convinced"), //13
            Resources.Load<Sprite>("sprites/Samira reluctant"), //14
            Resources.Load<Sprite>("sprites/Maverick worried"), //15
            Resources.Load<Sprite>("sprites/Maverick smug talkin'"), //16
            Resources.Load<Sprite>("sprites/Garance irreverent"), //17
            Resources.Load<Sprite>("sprites/Garance angry"), //18
            Resources.Load<Sprite>("sprites/Garance furious"), //19
            Resources.Load<Sprite>("sprites/Garance psycho"), //20
            Resources.Load<Sprite>("sprites/Samira angry") //21
        };
        names = new string[]{
                "GARANCE",//1
                "GARANCE",//2
                "MAVERICK",//3
                "SAMIRA",//4
                "MAVERICK",//5
                "SAMIRA",//6
                "GARANCE",//7
                "MAVERICK",//8
                "SAMIRA",//9
                "SAMIRA",//10
                "GARANCE",//11
                "GARANCE",//12
                "GARANCE",
                "SAMIRA",
                "MAVERICK",
                "MAVERICK",
                "GARANCE",
                "GARANCE",
                "GARANCE",
                "GARANCE",
                "SAMIRA"
            };
            colors = new Color[]{
                Color.red,//1
                Color.red,//2
                lightBlue,//3
                lightBlue,//4
                lightBlue,//5
                lightBlue,//6
                Color.red,//7
                lightBlue,//8
                lightBlue,//9
                lightBlue,//10
                Color.red,//11
                Color.red,//12
                Color.red,
                lightBlue,
                lightBlue,
                lightBlue,
                Color.red,
                Color.red,
                Color.red,
                Color.red,
                lightBlue
            };

            StartDialogue();
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
        spawner.SetActive(true);
        if(GlobalVariables.waveCounter == 16)
        {
            GlobalVariables.enemiesAlive = 0;
            SceneManager.LoadScene("Boss2");
        }
    }
}
