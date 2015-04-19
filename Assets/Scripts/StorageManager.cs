using System;

public class StorageManager
{
    private static StorageManager instance;

    private int numberOfPlayers = 0;

    private StorageManager() { }

    public int NumberOfPlayers
    {
        get
        {
            return numberOfPlayers;
        }
        set
        {
            numberOfPlayers = value;
        }
    }

    public static StorageManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StorageManager();
            }
            return instance;
        }
    }
}
