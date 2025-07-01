using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandFactory
{
    public static ICommandFactory Instance;
    Command Create();

}


