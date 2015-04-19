using System;

public class StorageManager
{
    private static StorageManager instance;

    private int numberOfPlayers = 0;
    private bool seenSplash = false;
    private string player1CharacterName = "";
    private string player2CharacterName = "";

    private StorageManager() { }

    public int NumberOfPlayers
    {
        get {return numberOfPlayers;}
        set {numberOfPlayers = value;}
    }

    public string Player1CharacterName
    {
        get { return player1CharacterName; }
        set { player1CharacterName = value; }
    }

    public string Player2CharacterName
    {
        get { return player2CharacterName; }
        set { player2CharacterName = value; }
    }

    public bool SeenSplash
    {
        get { return seenSplash; }
        set { seenSplash = value; }
    }

    public static StorageManager Instance
    {
        get
        {
            if (instance == null)
                instance = new StorageManager();
            return instance;
        }
    }
}
