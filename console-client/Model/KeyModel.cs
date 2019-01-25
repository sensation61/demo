using System;
using System.Collections.Generic;

public class KeyModel
{

    private static KeyModel _model;
    public static KeyModel Model
    {
        get
        {
            if (_model == null) _model = new KeyModel();
            return _model;
        }
    }
    public List<KeyState> keyLists = null;
    KeyModel()
    {
        keyLists = new List<KeyState>();
        keyLists.Add(new KeyState() { Name = "Key-1", State = true });
        keyLists.Add(new KeyState() { Name = "Key-2", State = true });
        keyLists.Add(new KeyState() { Name = "Key-3", State = true });
        keyLists.Add(new KeyState() { Name = "Key-4", State = true });
        keyLists.Add(new KeyState() { Name = "Key-5", State = true });
    }
}