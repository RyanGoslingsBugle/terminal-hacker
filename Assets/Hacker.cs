using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Member vars
    int level;
    enum Screen { Menu, Password, Win };
    Screen currentScreen = Screen.Menu;

	// Use this for initialization
	void Start ()
    {
        string greeting = "Welcome User:[Rich]";
        MainMenu(greeting);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Called when user confirms input - pass input to handlers
    void OnUserInput(string input)
    {
        if (input == "exit" || input == "Exit")
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        else if (input == "menu" || input == "Menu")
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

    // Handle inputs on password guess screen
    void PassHandler(string input)
    {
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
                StartGame();
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
    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine("Thanks for selecting access level " + level);
        Terminal.WriteLine("Type menu to return to level select");
        Terminal.WriteLine("");
        Terminal.WriteLine("Waiting for your input: ");
    }

    // Display main menu screen
    void MainMenu (string greeting = "")
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
}
