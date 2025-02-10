using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField]
    private TestA _test;
    private void Start(){
        _test = new TestA();
        _test.Debugger();
        _test = new TestB();
        _test.Debugger();
    }

    private void Update(){
        
    }
}
