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
                    if (number.Length != 11)
                    {
                        do
                        {
                            Console.WriteLine("Invalid input, must be 11 characters long.");
                            Console.WriteLine("Please input number to be checked: ");
                            number = Console.ReadLine();

                        } while (number.Length != 11);
                    }
                    

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
                    //Pydetään numero tarkistettavaksi
                    Console.WriteLine("Please input number to be completed: ");
                    string number = Console.ReadLine().Trim();

                    //Tarkistetaan pituus heti alkuun
                    if (number.Length != 10)
                    {
                        do
                        {
                            Console.WriteLine("Invalid input, must be 10 characters long.");
                            Console.WriteLine("Please input number to be checked: ");
                            number = Console.ReadLine();

                        } while (number.Length != 10);
                    }

                    //Tarkistetaan onko koko luku oikein generoidun osan kanssa
                    string fullnum = generateCode(number);
                    bool result = numberCheck(fullnum);

                    if (result == true)
                    {
                        Console.WriteLine($"Full code: {fullnum}");
                    } else if (result == false)
                    {
                        Console.WriteLine("Given input was not valid. Please reboot and try again.");
                    }
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
        //Numeron tarkistus. Valtava if-rakenne, tekee paljon viittauksia muuhun koodiin.
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
                            //Tarkistetaan ensimmäisen 6 luvun oikeellisuus
                            if (dateCheck(day, month, year) == true)
                            {
                                //Tarkistetaan vuosisata merkinnän oikeellisuus
                                if (centurystring == "-" || centurystring == "+" || centurystring == "A")
                                {
                                    //Tarkistetaan satunnaisluvun oikeellisuus
                                    if (number >= 2 && number <= 899)
                                    {
                                        //Tarkistetaan tarkistusmerkin oikeellisuus
                                        if (confirmCheck(input, confirms) == true)
                                        {
                                            result = true;
                                        } else
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
        //Tarkistaa, onko tunnuksen tarkistusluku oikein
        static bool confirmCheck(string input, string test)
        {
            //Alustetaan taulukko arvon tarkistamista varten
            char[] alphabet = "ABCDEFHJKLMNPRSTUVWXY".ToCharArray();
            string[] values = new string[31];
            for (int i = 0; i < 31; i++)
            {
                if (i < 10)
                {
                    values[i] = i.ToString();
                } else
                {
                    values[i] = alphabet[i - 10].ToString();
                }
            }
            //Alustetaan arvot tarkistusta varten
            input = input.Remove(6, 1);
            input = input.Remove(9, 1);
            bool result = false;
            int math = int.Parse(input);
            Math.DivRem(math, 31, out int mathresult);

            //Tarkistetaan
            if (values[Convert.ToInt32(mathresult)] == test)
            {
                result = true;
            }

            return result;
        }
        //Generoi tarkistustunnuksen annetulle vajaalle koodille
        static string generateCode(string input)
        {
            //Alustetaan taulukko
            char[] alphabet = "ABCDEFHJKLMNPRSTUVWXY".ToCharArray();
            string[] values = new string[31];
            for (int i = 0; i < 31; i++)
            {
                if (i < 10)
                {
                    values[i] = i.ToString();
                }
                else
                {
                    values[i] = alphabet[i - 10].ToString();
                }
            }
            string process = input.Remove(6, 1);
            int math = int.Parse(process);
            Math.DivRem(math, 31, out int mathresult);

            string output = input + values[mathresult];

            return output;
        }
    }
}
