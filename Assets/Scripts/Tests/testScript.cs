using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

public class testScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void testScriptSimplePasses()
    {
        // Use the Assert class to test conditions
        throw new Exception("lala ############## FAIL ");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator testScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
