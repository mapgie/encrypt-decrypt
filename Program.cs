using System;
using System.IO;

/* Written on 19 July 2019; (c) mapgie
 To Do:
 + Under Encryption:
    > Currently the returned text doesn't know which character is last, so the program doesn't specify a different syntax for exit.
    i.e. it would return 5, 29, ) with the last ,
    > If "encrypting" to/from ASCII, then could be case sensitive
 + Let user define delimiter(s)
 + Let user pick whether they want to encrypt or decrypt 
 + Set up encrypt / decrypt as classes
 */

/* This is inspired by HSCTF 3 Practice Problem written by Jacob Edelman
    In order to make text more easily encrypted, it is essential to transform it into some sort of numeric state. A simple way to do this is by taking letters, transforming them into numbers by their place in the alphabet ( a -> 1, b -> 2, c -> 3, and so on), “ ” going to 27, and “_” going to 28. For instance, the string “bad ” would go to the numbers (2,1,4,27). A program to automate this will make things vastly easier for you. Can you decrypt the flag?

(23, 5, 12, 12, 27, 20, 8, 5, 27, 6, 12, 1, 7, 27, 9, 19, 27, 8, 9, 4, 4, 5, 14, 27, 8, 5, 18, 5, 27, 2, 21, 20, 27, 6, 9, 18, 19, 20, 27, 23, 5, 27, 8, 1, 22, 5, 27, 19, 15, 13, 5, 27, 20, 5, 24, 20, 27, 20, 15, 27, 3, 15, 14, 6, 21, 19, 5, 27, 25, 15, 21, 27, 14, 15, 23, 27, 20, 8, 5, 27, 6, 12, 1, 7, 27, 9, 19, 27, 9, 14, 27, 6, 1, 3, 20, 27, 19, 5, 3, 18, 5, 20, 19, 28, 1, 18, 5, 28, 8, 9, 4, 4, 5, 14, 28, 9, 14, 28, 20, 8, 9, 19, 28, 12, 9, 19, 20, 27, 4, 15, 14, 20, 27, 9, 14, 3, 12, 21, 4, 5, 27, 20, 8, 5, 27, 16, 1, 18, 20, 19, 27, 20, 8, 1, 20, 27, 1, 18, 5, 27, 19, 5, 16, 5, 18, 1, 20, 5, 4, 27, 23, 9, 20, 8, 27, 19, 16, 1, 3, 5, 19)
 */

namespace EncryptDecrypt
{
    class Program
    {
        private static void Main()
        {
            #region extendCharInput
            byte[] bytes = new byte[2000];
            Stream inputStream = Console.OpenStandardInput(bytes.Length);
            Console.SetIn(new StreamReader(inputStream));
            #endregion

            #region Decrypt
            Console.WriteLine(@"
::::: Decrypting :::::
This program will take a string of numbers separated by delimiter characters and return a string of text.
Alpha characters will be ignored. The following are recognized as delimiters: () ,.:

If you need an example, use this:

( 20 8 9 19 27 9 19 27 1 14 27 5 24 1 13 16 12 5 27 25 15 21 27 3 1 14 27 21 19 5 29 )

Encrypted Text:
");
            var encryptedPhrase = Console.ReadLine();

            char[] delimiterChars = { '(', ')', ' ', ',', '.', ':', '\t' }; //if changing this, change description above
            if (encryptedPhrase != null)
            {
                string[] splitPhrase = encryptedPhrase.Split(delimiterChars);

                Console.WriteLine("");
                Console.WriteLine("Your message reads:");

                int charLookup;
                foreach (var splitChar in splitPhrase)
                {
                    int.TryParse(splitChar, out charLookup);

                    if (charLookup == 27) { Console.Write(" "); }
                    else if (charLookup == 28) { Console.Write("_"); }
                    else if (charLookup == 29) { Console.Write("."); }
                    else if (charLookup == 0) { Console.Write(""); } //Not happy with this. Think about neater way to approach this.
                    else
                    {
                        charLookup += 64;
                        var myChar = (char)charLookup;
                        Console.Write(myChar);
                    }

                }
            }

            #endregion

            Console.WriteLine("");

            #region Encrypt

            Console.WriteLine(@"
::::: Encrypting :::::
This program will take a string of characters a-z (not case sensitive), including space and _ and return the string 'encrypted' such that a = 1, ... z = 26, space = 27, _ = 28 and . = 29. 

You can type: 'this is an example you can use.' and compare it to your result from above.

Plain Text: 
");
            var plaintextPhrase = Console.ReadLine();
            if (plaintextPhrase != null)
            {
                var plainArray = plaintextPhrase.ToUpper().ToCharArray();

                Console.Write("( ");
                foreach (var each in plainArray)
                {
                    var eachString = each.ToString();
                    var space = " ";
                    var underscore = "_";
                    var period = ".";

                    if (eachString.Contains(space))
                    {
                        Console.Write("27 ");
                    }
                    else if (eachString.Contains(underscore))
                    {
                        Console.Write("28 ");
                    }
                    else if (eachString.Contains(period))
                    {
                        Console.Write("29 ");
                    }
                    else
                    {
                        var asNum = (int)each;
                        asNum -= 64;
                        Console.Write(asNum + " ");
                    }

                }
            }

            Console.Write(")");
            Console.WriteLine("");

            #endregion

            Console.WriteLine("");
            Console.WriteLine("mapgie ©2019");
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}
