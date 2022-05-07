using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommandManagerX : MonoBehaviour
{
    private static CommandManagerX _instance;
    public static CommandManagerX Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("CommandManagerX is NULL");
            }
            return _instance;
        }
    }

    private List<ICommandX> _commandBuffer = new List<ICommandX>();

    private void Awake()
    {
        _instance = this;
    }

    public void AddCommand(ICommandX command) 
    {

        _commandBuffer.Add(command);

    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    IEnumerator RewindRoutine()
    {
        Debug.Log("Rewinding...");
        foreach(var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Undo();
            yield return new WaitForEndOfFrame();
        }

        _commandBuffer.Clear();
        Debug.Log("Finished...");
    }
}
