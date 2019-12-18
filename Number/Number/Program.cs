using System;

namespace Number
{
    class Program
    {
        static void Main(string[] args)
        {
            //Kaikki tehtävät tehty yhteen, tehtävien tarkistus tehdään valikon kautta
            int select;
            string input;
            Console.WriteLine("Would you like to check the validity of an existing number (type ¨1¨) or generate a new one (type ¨2¨)?");
            Console.WriteLine("");
            //Laitetaan käyttäjän valinta string arvoon
            input = Console.ReadLine();
            Console.WriteLine("");
            //Käsitellään edellä annettu arvo.
            //Jos mahdollista muuttaa int arvoksi, tarkistetaan mikä numero.
            //Jos annettu arvo ei ole numero väliltä 1-2, mitään ei tapahdu.
            if (int.TryParse(input, out select) == true)
            {
                if (select == 1)
                {
                    //Pydetään numero tarkistettavaksi
                    Console.WriteLine("Please input number to be checked: ");
                    string number = Console.ReadLine().Trim();

                    //Tarkistetaan pituus heti alkuun
                    do
                    {
                        Console.WriteLine("Invalid input, must be 11 characters long.");
                        Console.WriteLine("Please input number to be checked: ");
                        number = Console.ReadLine();

                    } while (number.Length != 11);

                    //Tarkistetaan numeron oikeellisuus
                    bool result = numberCheck(number);

                    //Tulostetaan vastaus
                    if (result == true)
                    {
                        Console.WriteLine($"Input: {number}");
                        Console.WriteLine($"Valid number");
                    } else if (result == false)
                    {
                        Console.WriteLine($"Input: {number}");
                        Console.WriteLine($"Invalid number");
                    }
                }
                else if (select == 2)
                {
                    
                }
                else
                {
                    Console.WriteLine("Invalid selection, please reboot.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection, please reboot.");
            }

        }

        static bool numberCheck (string input)
        {
            bool result = false;
            string daystring = input.Substring(0, 2);
            string monthstring = input.Substring(2, 2);
            string yearstring = input.Substring(4, 2);
            string centurystring = input.Substring(6, 1);
            string numberstring = input.Substring(7, 3);
            string confirms = input.Substring(10, 1);

            //If rakenteille tarkistetaan että syötteet ovat tarvittaessa numeroita, kasattu regioniin luettavuuden helpottamiseksi
            #region
            if (int.TryParse(daystring, out int day) == true)
            {
                if (int.TryParse(monthstring, out int month) == true)
                {
                    if (int.TryParse(yearstring, out int year) == true)
                    {
                        if (int.TryParse(numberstring, out int number) == true)
                        {
                            #endregion
                            if (dateCheck(day, month, year) == true)
                            {

                            } else
                            {
                                return result;
                            }

                            #region
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        return result;
                    }
                } else
                {
                    return result;
                }
            } else
            {
                return result;
            }
            #endregion

            result = true;
            return result;
        }

        //Tarkistaa onko annettu päivämäärä mahdollinen
        static bool dateCheck(int day, int month, int year)
        {
            bool result = false;

            //Perustarkistukset, yli/ali mahdolliset numerot
            if (day > 31 || month > 12)
            {
                return result;
            } else if (day < 0 || month < 0)
            {
                return result;
            //Lyhyemmät kuukaudet, joissa on vain 30 päivää
            } else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day > 30)
                {
                    return result;
                }
            //Helmikuu, lyhyin kuukausi ja mahdolliset hyppyvuodet
            } else if (month == 2)
            {
                if (year % 4 == 0)
                {
                    if (day > 29)
                    {
                        return result;
                    } else if (day > 28)
                    {
                        return result;
                    }
                }
            } else
            {
                result = true;
            }

            return result;
        }
    }
}
