using System;

namespace Bank;

public class Konto
{
    private string klient;
    private decimal bilans;
    private bool zablokowane = false;

    private Konto()
    {

    }

    public Konto(string klient, decimal bilansNaStart = 0)
    {
        if (string.IsNullOrWhiteSpace(klient))
        {
            throw new ArgumentException("Nazwa klienta nie może być pusta.");
        }

        if (bilansNaStart < 0)
        {
            throw new ArgumentOutOfRangeException("Bilans na start nie może być ujemny.");
        }

        this.klient = klient;
        this.bilans = bilansNaStart;
    }

    public string Nazwa => klient;
    public virtual decimal Bilans => bilans;
    public bool Zablokowane => zablokowane;

    public void BlokujKonto()
    {
        zablokowane = true;
    }

    public void OdblokujKonto()
    {
        zablokowane = false;
    }

    public virtual void Wplata(decimal kwota)
    {
        if (zablokowane)
        {
            throw new InvalidOperationException("Nie można wpłacać, konto jest zablokowane.");
        }

        if (kwota <= 0)
        {
            throw new ArgumentOutOfRangeException("Wpłacana kwota musi być większa od zera.");
        }

        bilans += kwota;
    }

    public virtual void Wyplata(decimal kwota)
    {
        if (zablokowane)
        {
            throw new InvalidOperationException("Nie można wypłacać, konto jest zablokowane.");
        }

        if (kwota <= 0)
        {
            throw new ArgumentOutOfRangeException("Wpłacana kwota musi być większa od zera.");
        }

        if (kwota > bilans)
        {
            throw new InvalidOperationException("Brak wystarczających środków na koncie.");
        }

        bilans -= kwota;
    }
}