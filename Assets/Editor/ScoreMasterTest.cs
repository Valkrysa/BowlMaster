using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

[TestFixture]
public class ScoreMasterTest {
    
    [Test]
    public void FailingTest () {
        Assert.AreEqual(1, 1);
    }
}
