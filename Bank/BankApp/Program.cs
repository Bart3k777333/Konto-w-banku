using Bank;
using System;

Console.WriteLine("=== Test konta standardowego ===");

Konto kontoPodstawowe = new Konto("Jan Kowalski", 200);
Console.WriteLine($"Właściciel: {kontoPodstawowe.Nazwa}, Stan konta: {kontoPodstawowe.Bilans} zł");

kontoPodstawowe.Wyplata(50);
Console.WriteLine($"Po wypłacie 50 zł saldo wynosi: {kontoPodstawowe.Bilans} zł");

Console.WriteLine("Test konta z debetem");

KontoPlus kontoZDodatkiem = new KontoPlus("Anna Nowak", 50, 100);
Console.WriteLine($"Właściciel: {kontoZDodatkiem.Nazwa}, Dostępne środki: {kontoZDodatkiem.Bilans} zł");

Console.WriteLine("Wypłata 100 zł w toku...");
kontoZDodatkiem.Wyplata(100);

Console.WriteLine($"Operacja zakończona. Dostępne środki: {kontoZDodatkiem.Bilans} zł");
Console.WriteLine($"Status blokady konta: {kontoZDodatkiem.Zablokowane}");


Console.WriteLine("Test konta z limitem");

KontoLimit kontoZLimitem = new KontoLimit("Piotr Wiśniewski", 20, 50);
Console.WriteLine($"Właściciel: {kontoZLimitem.Nazwa}, Dostępne środki: {kontoZLimitem.Bilans} zł");

Console.WriteLine("Próba wypłaty 60 zł");
kontoZLimitem.Wyplata(60);

Console.WriteLine($"Operacja zakończona. Dostępne środki: {kontoZLimitem.Bilans} zł");
Console.WriteLine($"Status blokady konta: {kontoZLimitem.Zablokowane}");

Console.WriteLine("Wciśnij Enter, aby zamknąć program.");
Console.ReadLine();