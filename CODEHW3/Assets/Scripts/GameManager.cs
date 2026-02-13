using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Manages score, level progression and persists across scenes.
/// Attach to a GameObject and assign `scoreText` in the Inspector.
/// </summary>
public class GameManager : MonoBehaviour
{
    //const variables can never be changed once they are set
    //static variables can change, but they are the same across instance of a class and the class itself
    
    const string DIR_DATA = "/Data/"; //folder we will put our data file into
    const string GREETING_NUMBER = DIR_DATA + "greeting.txt"; //file we will put data into
    
    
    //Keys for saving data to PlayerPrefs
    const string KeyGreeting = "Greeting";
    const string KeyForTheHighScorePlayerPref = "Current Greeting";
    
    // private int haw;
    // public int Haw { set; get; }

    // Current player score (can be modified by other scripts)
    private int greeting;

    public int Greeting //C# Property that is a wrapper around "score",
                     //must be a capitalized version of that  
    {
        set //gets called whenever Score is set
        {
            Debug.Log("Set Greeting: " + value);
            
            greeting = value; //sets the var "score" to the value of Score
            
            //Replaced PlayerPrefs with File IO
            PlayerPrefs.SetInt(KeyGreeting, greeting); //Saving the score to player prefs so it can be retrieved later (even after closing the game)
            
            // Debug.Log("Where we save: " + fullFilePath);
            
            //play a sound?
            //add juice?
            //spawn enemy?
            //you can do whatever you want in this function
            //and it will happen whenever you set the value
            //of the Score property
            
          /*  if (greeting > GreetingNumber) //int var score > the property HighScore
            {
                GreetingNumber = greeting;
            }*/
        }
        get
        {
            greeting = PlayerPrefs.GetInt(KeyGreeting, 0); //Retrieving the score from player prefs
            
            Debug.Log("Got Greeting: " + greeting);
            return greeting;  //return the value of the "score" var
        }
    }


    private int greetingNumber;

    public int GreetingNumber  //a property that wraps the var highScore that calls
    {
        get
        {
            //highScore = PlayerPrefs.GetInt(KeyForTheHighScorePlayerPref, 5);
            
            //getting the path to the highScore.txt file
            string fullFilePath = Application.dataPath + GREETING_NUMBER;


            //if there is no file
            if (!File.Exists(fullFilePath))
            {
                greetingNumber = 0; //default high score is 1
            }
            else //otherwise
            {
                //get the contents out of the highScore file
                string fileContents = File.ReadAllText(fullFilePath);
                
                //turn the string version of those contents into an int
                greetingNumber = int.Parse(fileContents);
            }

            return greetingNumber;
        
        }

        set
        {
            Debug.Log("Got New Greeting: " + value);
            greetingNumber = value;
            //PlayerPrefs.SetInt(KeyForTheHighScorePlayerPref, highScore);

            string fileContents = greetingNumber + ""; //turn the score into a string we can put in a file
            
            //get the full data path with Unity's help
            //NOTE: If you were releasing this, you should use
            //Application.persistentDataPath instead of just
            //Application.dataPath, which is better for debuggin
            string fullFilePath = Application.dataPath + GREETING_NUMBER;
            
            Debug.Log(fullFilePath);

            if (!File.Exists(fullFilePath))  //if we haven't saved already
            {
                //create the folder to save
                Directory.CreateDirectory(Application.dataPath + DIR_DATA);
            }
            
            //Save the fileContents (highScore string) to the file "highScore.txt"
            File.WriteAllText(fullFilePath, fileContents);
        }
    }

    // Current level / scene index
    public int currentLevel = 0;

    // Singleton instance so the GameManager persists and is globally accessible
    public static GameManager instance;

    // Reference to a TextMeshPro UI element that displays score and target
    // Must be assigned in the Inspector to avoid a null reference at runtime
    public TMP_Text greetingText;

    // Template string used to format the displayed score and target
    string defaultGreetingText = "Keep going I guess. <greetingNumber> times a charm!";

    // Start is called once before the first frame update
    void Start()
    {
        //If you want to delete all the keys in a playerPref, use this
        //PlayerPrefs.DeleteAll();
       

        // Establish singleton: keep the first instance and destroy duplicates
        if (instance == null)
        {
            instance = this;
            // Prevent this GameObject from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate GameManager instances to enforce single instance
            Destroy(gameObject);
        }
        GameManager.instance.Greeting++;

        if (greetingNumber <= 1)

        {
            string updatedGreetingText = defaultGreetingText;
            // Replace placeholders with current values
            updatedGreetingText = "Hi there. This isn't much of a game. Try restarting."; //always use the property to make sure the value is properly loaded
                                                                                          //  updatedGreetingText = updatedGreetingText.Replace("<high>", HighScore + "");
            if (greetingText != null)
            {
                greetingText.text = updatedGreetingText;
            }

        }

        if (greetingNumber < 3)

        {
            if (greetingText != null)
            {
                greetingText.text = "Oh you believed me. Try again?";
            }

        }

        if (greetingNumber < 4)

        {
            
            if (greetingText != null)
            {
                greetingText.text = "Still here? Three times a charm I guess.";
            }

        }

        if (Greeting > 10)

        {
            string updatedGreetingText = defaultGreetingText;
            // Replace placeholders with current values
            updatedGreetingText = updatedGreetingText.Replace("<greetingNumber>", Greeting + ""); //always use the property to make sure the value is properly loaded
                                                                                                  //  updatedGreetingText = updatedGreetingText.Replace("<high>", HighScore + "");
            if (greetingText != null)
            {
                greetingText.text = updatedGreetingText;
            }
            // Update the UI text if assigned (avoid null reference)

        }
    }

    
}