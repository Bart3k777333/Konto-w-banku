using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace Bank.Tests;

[TestClass]
public class KontoLimitTests
{
    [TestMethod]
    public void Limit_Ustawienie_Ujemnego_Wyrzuca_Wyjatek()
    {
        var konto = new KontoLimit("Jan Kowalski", 100, 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Limit = -10);
    }

    [TestMethod]
    public void Bilans_Gdy_Konto_Zablokowane_Zwraca_Zero()
    {
        var konto = new KontoLimit("Jan Kowalski", 100, 100);
        konto.BlokujKonto();
        Assert.AreEqual(0, konto.Bilans);
    }

    [TestMethod]
    public void Wplata_Kwota_Ujemna_Lub_Zero_Wyrzuca_Wyjatek()
    {
        var konto = new KontoLimit("Jan Kowalski", 100, 100);

        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wplata(-50));
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wplata(0));
    }
}