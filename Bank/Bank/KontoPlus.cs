using System;

namespace Bank;

public class KontoPlus : Konto
{
    private decimal limit;
    private decimal debet = 0;
    private bool limitWykorzystany = false;

    public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 100)
        : base(klient, bilansNaStart)
    {
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

    public override decimal Bilans
    {
        get
        {
            if (Zablokowane)
            {
                return 0;
            }

            if (limitWykorzystany)
            {
                return base.Bilans;
            }

            return base.Bilans + limit; 
        }
    }

    public override void Wplata(decimal kwota)
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
                    base.Wplata(reszta);
                }

                if (base.Bilans > 0)
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
            base.Wplata(kwota);
        }

        if (debet > 0 && byloZablokowane)
        {
            BlokujKonto();
        }
    }

    public override void Wyplata(decimal kwota)
    {
        if (Zablokowane)
        {
            throw new InvalidOperationException("Nie można wypłacać, konto jest zablokowane.");
        }

        if (kwota <= 0)
        {
            throw new ArgumentOutOfRangeException("Kwota wpłaty musi być większa od zera.");
        }

        if (kwota > base.Bilans)
        {
            throw new InvalidOperationException("Brak wystarczających środków na koncie.");
        }

        if (kwota <= base.Bilans)
        {
            base.Wyplata(kwota);
        }
        else
        {
            if (limitWykorzystany)
            {
                throw new InvalidOperationException("Jednorazowy limit debetowy został już wykorzystany.");
            }

            decimal brakujacaKwota = kwota - base.Bilans;

            if (brakujacaKwota > limit)
            {
                throw new InvalidOperationException("Kwota wypłaty przekracza Twój jednorazowy limit debetowy.");
            }

            if (base.Bilans > 0)
            {
                base.Wyplata(base.Bilans);
            }

            debet = brakujacaKwota;
            limitWykorzystany = true;

            BlokujKonto();
        }
    }
}