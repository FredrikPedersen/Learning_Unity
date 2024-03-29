﻿using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game configuration data
    string[] level1Passwords = { "hei", "hade", "kake", "bok", "dør" };
    string[] level2Passwords = { "panteflasker", "kebab", "orakel", "ostepop", "mordi" };
    string[] level3Passwords = { "skosnipp", "eplegnuffel", "onomatopoetikon", "kebabsaus", "panteflasker" };

    //Game State
    int level;
    string password;
    enum Screen {Intro, MainMenu, Password, Win};
    Screen currentScreen;
    int tries;

    // Start is called before the first frame update
    void Start() {
        ShowIntro();
    }

    void ShowIntro() {
        currentScreen = Screen.Intro;
        Terminal.ClearScreen();

        Terminal.WriteLine("Velkommen til Datatorget \nHacker Tool 2.0!");
        Terminal.WriteLine("Skriv \"meny\" når som helst for \nå gå tilbake til hovedmenyen! \n\nGjør det nå for å starte programmet!");
    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        tries = 0;

        Terminal.WriteLine("Hva ønsker du å hacke? \n 1. Biblioteket i P35 \n 2. Kontoret til Sigrun \n 3. Oraklenes topp-hemmelige data ");
        Terminal.WriteLine("\nSkriv inn ditt valg (1, 2 eller 3): ");
    }

    void OnUserInput(string input) {
        if (input.ToLower() == "meny") { //We can always go directly to MainMenu
            ShowMainMenu();
        } else if (currentScreen == Screen.MainMenu) {
            RunMainMenu(input);
        } else if (currentScreen == Screen.Password) {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input) {

        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber) {
            level = int.Parse(input);
            AskForPassword();
        } else if (input.ToLower() == "hello there") {
            Terminal.WriteLine("GENERAL KENOBI");
        } else {
            Terminal.WriteLine("Vennligst oppgi et gyldig hackermål");
        }
    }

    void CheckPassword(string input) {
        if (input == password) {
            DisplayWinScreen();
        } else {
            tries--;
            AskForPassword();
        }
    }

    void AskForPassword() {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        if (tries == 0) {
            SetRandomPassword();
            tries = 3;
        }
        Terminal.WriteLine("Oppgi Passord, hint: " + password.Anagram());
        Terminal.WriteLine("Gjenværende forsøk: " + tries);
    }

    void SetRandomPassword() {
        switch (level) {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number!");
                break;
        }
    }

    void DisplayWinScreen() {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward() {
        switch (level) {
            case 1:
                Terminal.WriteLine("Du hacker biblioteket og har nå evig \nprint credits!");
                break;
            case 2:
                Terminal.WriteLine("Du dobler lønna i \narbeidskontrakten din!");
                break;
            case 3:
                Terminal.WriteLine("Du innser at Oraklenes store \nhemmelighet er Google og bortgjemte \nsoveposer på datatorget.");
                Terminal.WriteLine("Ingen snarveier til kodehimmelen :(");
                break;
            default:
                break;
        }
    }
}
