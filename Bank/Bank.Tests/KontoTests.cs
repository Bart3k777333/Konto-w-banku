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

    [TestMethod]
    public void Konstruktor_Pusta_Nazwa_Klienta_Wyrzuc_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Konto(""));
        Assert.Throws<ArgumentException>(() => new Konto(null));
    }

    [TestMethod]
    public void Konstruktor_Ujemny_Bilans_Na_Start_Wyrzuca_ArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Konto("Jan", -10));
    }

    [TestMethod]
    public void Blokuj_Konto_Zmienia_Status_Na_Zablokowane()
    {
        var konto = new Konto("Jan", 100);
        konto.BlokujKonto();
        Assert.IsTrue(konto.Zablokowane);
    }

    [TestMethod]
    public void Odblokuj_Konto_Zmienia_Status_Na_Odblokowane()
    {
        var konto = new Konto("Jan", 100);
        konto.BlokujKonto();
        konto.OdblokujKonto();
        Assert.IsFalse(konto.Zablokowane);
    }

    [TestMethod]
    public void Wplata_Prawidlowa_Kwota_Zwieksza_Bilans()
    {
        var konto = new Konto("Jan", 100);
        konto.Wplata(50);
        Assert.AreEqual(150, konto.Bilans);
    }

    [TestMethod]
    public void Wplata_Ujemna_Lub_Zerowa_Kwota_Wyrzuca_ArgumentOutOfRangeException()
    {
        var konto = new Konto("Jan", 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wplata(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => konto.Wplata(-10));
    }

    [TestMethod]
    public void Wplata_Konto_Zablokowana_Wyrzuca_InvalidOperationException()
    {
        var konto = new Konto("Jan", 100);
        konto.BlokujKonto();
        Assert.Throws<InvalidOperationException>(() => konto.Wplata(50));
    }

    [TestMethod]
    public void Wyplata_Prawidlowa_Kwota_Zmniejsza_Bilans()
    {
        var konto = new Konto("Jan", 100);
        konto.Wyplata(50);
        Assert.AreEqual(50, konto.Bilans);
    }
}
