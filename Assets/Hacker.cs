using UnityEngine;

public class Hacker : MonoBehaviour
{

    // Config vars
    const string menuFlag = "Type menu to return to level select";
    enum Screen { Menu, Password, Win };
    string[,] passwords = new string[3, 5]{ {"chum", "pint", "football", "rain", "ascot" },
        { "vodka", "kremlin", "makarov", "caviar", "borscht"  },
        { "benghazi", "phosphorus", "predator", "washington", "obamacare" } };

    // State vars
    int level;
    Screen currentScreen = Screen.Menu;
    string currentPassword;

    // Use this for initialization
    void Start()
    {
        string greeting = "Welcome User:[Rich]";
        MainMenu(greeting);
    }

    // Called when user confirms input - pass input to handlers
    void OnUserInput(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            if (input.ToLower() == "exit")
            {
                Application.Quit();
            }
            else if (input.ToLower() == "menu")
            {
                MainMenu();
            }
            else if (currentScreen == Screen.Menu)
            {
                MenuHandler(input);
            }
            else if (currentScreen == Screen.Password)
            {
                PassHandler(input);
            }
        }
    }

    // Display main menu screen
    void MainMenu(string greeting = "")
    {
        currentScreen = Screen.Menu;
        Terminal.ClearScreen();
        if (greeting.Length > 0)
        {
            Terminal.WriteLine(greeting);
        }
        Terminal.WriteLine("Do you want to play a game?");
        Terminal.WriteLine("Not global thermonuclear war, promise.");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter 1 to snoop on MI5");
        Terminal.WriteLine("Enter 2 to gather data on the FSB");
        Terminal.WriteLine("Enter 3 to penetrate the NSA network");
        Terminal.WriteLine("");
        Terminal.WriteLine("Your choice: ");
    }

    // Handle inputs on main menu
    void MenuHandler(string input)
    {
        switch (input)
        {
            case "1":
            case "2":
            case "3":
                level = int.Parse(input);
                GameScreen();
                break;

            case "007":
                Terminal.WriteLine("Please select a level... Mr. Bond.");
                break;

            default:
                Terminal.WriteLine("Please select a valid level.");
                break;
        }
    }

    // Called when level is selected on main menu
    void GameScreen()
    {
        currentScreen = Screen.Password;
        PasswordGen();
        Terminal.ClearScreen();
        Terminal.WriteLine(menuFlag);
        DrawLevelArt();
        Terminal.WriteLine("Enter password, hint " + currentPassword.Anagram() + ": ");
    }

    // Generate new random password
    void PasswordGen()
    {
        currentPassword = passwords[level - 1, Random.Range(0, passwords.GetLength(1))];
    }

    // Draw ascii for title
    void DrawLevelArt()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"
         __  __ ___ ___ 
        |  \/  |_ _| __|
        | |\/| || ||__ \
        |_|  |_|___|___/
                ");
                break;
            case 2:
                Terminal.WriteLine(@"
        ______ ______  ______    
       /\  ___/\  ___\/\  == \   
       \ \  __\ \___  \ \  __<   
        \ \_\  \/\_____\ \_____\ 
         \/_/   \/_____/\/_____/        
                ");
                break;
            case 3:
                Terminal.WriteLine(@"
            _   _______ ___ 
           / | / / ___//   |
          /  |/ /\__ \/ /| |
         / /|  /___/ / ___ |
        /_/ |_//____/_/  |_|
                ");
                break;
            default:
                Debug.LogError("Error in game screen draw call, no level.");
                break;
        }
    }

    // Handle inputs on password guess screen
    void PassHandler(string input)
    {
        if (input.ToLower() == currentPassword.ToLower())
        {
            WinScreen();
        }
        else
        {
            GameScreen();
        }
    }

    // Called when correct password entered
    void WinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        WinHandler();
        Terminal.WriteLine(menuFlag);
    }

    // Handle logic for win screen
    void WinHandler()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Excellent!");
                Terminal.WriteLine("Those tea-drinkers will rue the day.");
                Terminal.WriteLine(@"
           ( (
            ) )
          ........
          |      |]
          \      /
           `----'
                ");
                break;
            case 2:
                Terminal.WriteLine("Khorosho!");
                Terminal.WriteLine("Cold War? More like old snore.");
                Terminal.WriteLine(@"
                -..
            //    ))
           //\  . _.'
          '/ \\//~
              \\
             //\\
                ");
                break;
            case 3:
                Terminal.WriteLine("Tubular!");
                Terminal.WriteLine("Turns out aliens are totally real.");
                Terminal.WriteLine(@"
                           .-.
            .-''`''-.    |(@ @)
         _/`oOoOoOoOo`\_ \ \-/
        '.-=-=-=-=-=-=-.' \/ \
          `-=.=-.-=.=-'    \ /\
             ^  ^  ^       _H_ \
                ");
                break;
            default:
                Debug.LogError("Error in WinHandler, no level value");
                break;
        }
    }
}
