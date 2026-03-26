using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace Bank.Tests;

[TestClass]
public class KontoPlusTests
{
    [TestMethod]
    public void Limit_Ustawienie_Ujemnego_Wyrzuca_Wyjatek()
    {
        var konto = new KontoPlus("Jan Kowalski", 100, 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Limit = -50);
    }

    [TestMethod]
    public void Bilans_Gdy_Konto_Zablokowane_Zwraca_Zero()
    {
        var konto = new KontoPlus("Jan Kowalski", 100, 100);
        konto.BlokujKonto();
        Assert.AreEqual(0, konto.Bilans);
    }
}