using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace Bank.Tests;

[TestClass]
public class KontoTests
{
    [TestMethod]
    public void Konstruktor_Prawidlowe_Dane_Tworzy_Konto()
    {
        var konto = new Konto("Jan Kowalski", 100);

        Assert.AreEqual("Jan Kowalski", konto.Nazwa);
        Assert.AreEqual(100, konto.Bilans);
        Assert.IsFalse(konto.Zablokowane);
    }

    
}
