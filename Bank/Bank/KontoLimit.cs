using System;

namespace Bank;

public class KontoLimit
{
    private Konto konto;

    private decimal limit;
    private decimal debet = 0;
    private bool limitWykorzystany;

    public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 100)
    {
        this.konto = new Konto(klient, bilansNaStart);
        Limit = limit;
    }

    public decimal Limit
    {
        get => limit;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Limit nie może być ujemny.");
            }

            limit = value;
        }
    }

    public string Nazwa => konto.Nazwa;

    public bool Zablokowane => konto.Zablokowane;

    public decimal Bilans
    {
        get
        {
            if (Zablokowane)
                return 0;

            if (limitWykorzystany)
                return konto.Bilans;

            return konto.Bilans + limit;
        }
    }

    public void BlokujKonto()
    {
        konto.BlokujKonto();
    }

    public void OdblokujKonto()
    {
        konto.OdblokujKonto();
    }

    public void Wplata(decimal kwota)
    {
        if (kwota <= 0)
        {
            throw new ArgumentOutOfRangeException("Kwota wpłaty musi być większa od zera.");
        }

        bool byloZablokowane = Zablokowane;

        if (byloZablokowane)
        {
            OdblokujKonto();
        }

        if (debet > 0)
        {
            if (kwota >= debet)
            {
                decimal reszta = kwota - debet;
                debet = 0;

                if (reszta > 0)
                {
                    konto.Wplata(reszta);
                }

                if (konto.Bilans > 0)
                {
                    limitWykorzystany = false;
                }
            }
            else
            {
                debet -= kwota;
                BlokujKonto();
            }
        }
        else
        {
            konto.Wplata(kwota);
        }

        if (debet > 0 && byloZablokowane)
        {
            BlokujKonto();
        }
    }

    public void Wyplata(decimal kwota)
    {
        if (Zablokowane)
        {
            throw new InvalidOperationException("Nie można wypłacać, konto jest zablokowane.");
        }

        if (kwota <= 0)
        {
            throw new ArgumentOutOfRangeException("Wypłacana kwota musi być większa od zera.");
        }

        if (kwota <= konto.Bilans)
        {
            konto.Wyplata(kwota);
        }
        else
        {
            if (limitWykorzystany)
            {
                throw new InvalidOperationException("Jednorazowy limit debetowy został już wykorzystany.");
            }

            decimal brakujacaKwota = kwota - konto.Bilans;

            if (brakujacaKwota > limit)
            {
                throw new InvalidOperationException("Kwota wypłaty przekracza Twój jednorazowy limit debetowy.");
            }

            if (konto.Bilans > 0)
            {
                konto.Wyplata(konto.Bilans);
            }

            debet = brakujacaKwota;
            limitWykorzystany = true;
            BlokujKonto();
        }
    }
}