using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICanAskYesNoQuestion
{
    public void ResponseReceived(bool response);
}
