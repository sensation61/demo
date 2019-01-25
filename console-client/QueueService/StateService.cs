using System;
using System.Linq;

public class StateService
{
    public StateService Active()
    {
        var activeKeyList = KeyModel.Model.keyLists.Where(s => s.State == true).ToList();
        foreach (var item in activeKeyList)
        {
            QueueServiceFactory.Service.Sent("keyState", item);
        }

        return this;
    }
}