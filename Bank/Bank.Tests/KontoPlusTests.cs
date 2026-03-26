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
        var konto = new KontoPlus("Jan Kowalski", 0, 100);
        konto.Wplata(50);
        Assert.AreEqual(150, konto.Bilans);
    }

    [TestMethod]
    public void Wplata_Mniejsza_Niz_Debet_Zmniejsza_Debet_Konto_Nadal_Zablokowane()
    {
        var konto = new KontoPlus("Jan Kowalski", 0, 100);
        konto.Wyplata(50);
        konto.Wplata(20);

        Assert.IsTrue(konto.Zablokowane);
        Assert.AreEqual(0, konto.Bilans);
    }

    [TestMethod]
    public void Wplata_Rowna_Debet_Odblokowuje_Ale_Limit_Nadal_Wykorzystany()
    {
        var konto = new KontoPlus("Jan Kowalski", 0, 100);

        konto.Wyplata(50);
        konto.Wplata(50);

        Assert.IsFalse(konto.Zablokowane);
        Assert.AreEqual(0, konto.Bilans);
    }

    [TestMethod]
    public void Wplata_Wieksza_Niz_Debet_Odblokowuje_I_Odnawia_Limit()
    {
        var konto = new KontoPlus("Jan Kowalski", 0, 100);

        konto.Wyplata(50);
        konto.Wplata(60);

        Assert.IsFalse(konto.Zablokowane);
        Assert.AreEqual(110, konto.Bilans);
    }

    [TestMethod]
    public void Wyplata_Kwota_Ujemna_Lub_Zero_Wyrzuca_Wyjatek()
    {
        var konto = new KontoPlus("Jan Kowalski", 100, 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wyplata(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wyplata(-50));
    }

    [TestMethod]
    public void Wyplata_Wejscie_W_Limit_Blokuje_Konto()
    {
        var konto = new KontoPlus("Jan Kowalski", 100, 100);
        konto.Wyplata(150);
        Assert.IsTrue(konto.Zablokowane);
    }

    [TestMethod]
    public void Wyplata_Konto_Zablokowane_Wyrzuca_Wyjatek()
    {
        var konto = new KontoPlus("Jan Kowalski", 100, 100);
        konto.BlokujKonto();
        Assert.Throws<InvalidOperationException>(() => konto.Wyplata(50));
    }

    [TestMethod]
    public void Wyplata_Przekroczenie_Limitu_Wyrzuca_Wyjatek()
    {
        var konto = new KontoPlus("Jan Kowalski", 50, 100);

        Assert.Throws<InvalidOperationException>(() => konto.Wyplata(200));
    }

    [TestMethod]
    public void Wyplata_Gdy_Limit_Juz_Wykorzystany_Wyrzuca_Wyjatek()
    {
        var konto = new KontoPlus("Jan Kowalski", 50, 100);
        konto.Wyplata(100);
        konto.Wplata(50);

        Assert.Throws<InvalidOperationException>(() => konto.Wyplata(10));
    }
}