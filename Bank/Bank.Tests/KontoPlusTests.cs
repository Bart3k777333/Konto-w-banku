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

    [TestMethod]
    public void Wplata_Kwota_Ujemna_Lub_Zero_Wyrzuca_Wyjatek()
    {
        var konto = new KontoPlus("Jan Kowlaski", 100, 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wplata(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wplata(-50));
    }

    [TestMethod]
    public void Wplata_Brak_Debetu_Zwieksza_Bilans()
    {
        var konto = new KontoPlus("Jan Kowalski", 100, 100);
        konto.Wplata(50);
        Assert.AreEqual(150, konto.Bilans);
    }
}