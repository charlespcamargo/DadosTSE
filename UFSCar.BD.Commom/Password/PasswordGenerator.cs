using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Password
{
    /// <summary>
    /// The PasswordGenerator class is a general utility class for generating password
    /// based on a number of properties. Once one has set these properties, a random
    /// password that compliess with these can be generated.
    /// </summary>
    /// <remarks>
    /// The PasswordGenerator class is originally created by Kevin Stewart as posted on
    /// </remarks>
    /// <example>
    /// <code>
    /// Create new password by using PasswordGenerator class
    /// PasswordGenerator generator = new PasswordGenerator();
    /// generator.ConsecutiveCharacters = true;
    /// generator.Exclusions = "`'~^|{}\\[](),.\";:/"; //
    /// generator.Minimum = 8;
    /// generator.Maximum = 8;
    /// generator.RepeatCharacters = true;
    /// string randomPassword = generator.Generate();
    /// </code>
    /// </example>
    public class PasswordGenerator
    {
        #region Constants

        private const int DefaultMinimum = 6;
        private const int DefaultMaximum = 10;
        private const int UBoundDigit = 61;

        #endregion

        #region Member Variables

        private RNGCryptoServiceProvider rng;
        private int minSize;
        private int maxSize;
        private bool hasRepeating;
        private bool hasConsecutive;
        private bool hasSymbols;
        private string exclusionSet;

        private char[] pwdCharArray =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?".
                ToCharArray();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PasswordGenerator()
        {
            Minimum = DefaultMinimum;
            Maximum = DefaultMaximum;
            ConsecutiveCharacters = false;
            RepeatCharacters = true;
            ExcludeSymbols = false;
            Exclusions = null;

            rng = new RNGCryptoServiceProvider();
        }

        public PasswordGenerator(int qtdMinima, int qtdMaxima, bool excluirSimbolos)
        {
            Minimum = qtdMinima;
            Maximum = qtdMaxima;
            ConsecutiveCharacters = false;
            RepeatCharacters = true;
            ExcludeSymbols = excluirSimbolos;
            Exclusions = null;

            rng = new RNGCryptoServiceProvider();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The class will by default generate passwords with the characters
        /// a-z, A-Z, 0-9 and special characters:
        /// By specifying an Exclusions string, one can exclude some of these
        /// special characters
        /// </summary>
        public string Exclusions
        {
            get { return exclusionSet; }
            set { exclusionSet = value; }
        }

        /// <summary>
        /// Defines the minimum length of the generated password
        /// </summary>
        /// <remarks>
        /// Note that the absolute minimum size is 6. Setting a value smaller than
        /// 6 will have no effect
        /// </remarks>
        public int Minimum
        {
            get { return minSize; }
            set
            {
                minSize = value;
                if (DefaultMinimum > minSize)
                {
                    minSize = DefaultMinimum;
                }
            }
        }

        /// <summary>
        /// Defines the maximum length of the generated password
        /// </summary>
        /// <remarks>
        /// Note that if the maximum size is set to something  smaller than
        /// <see cref="Minimum"/>, the maximum will be set to 10
        /// </remarks>
        public int Maximum
        {
            get { return maxSize; }
            set
            {
                maxSize = value;
                if (minSize >= maxSize)
                {
                    maxSize = DefaultMaximum;
                }
            }
        }

        /// <summary>
        /// Defines whether the generated password may contain any special characters
        /// at all, or just letters and numbers
        /// </summary>
        public bool ExcludeSymbols
        {
            get { return hasSymbols; }
            set { hasSymbols = value; }
        }

        /// <summary>
        /// Defines whether the generated password may contain the same character
        /// more than once
        /// </summary>
        public bool RepeatCharacters
        {
            get { return hasRepeating; }
            set { hasRepeating = value; }
        }

        /// <summary>
        /// Defines whether the generated password may contain the same character
        /// repeated after itself
        /// </summary>
        public bool ConsecutiveCharacters
        {
            get { return hasConsecutive; }
            set { hasConsecutive = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates a random number between the specified bounds
        /// </summary>
        /// <param name="lBound">Lower bound</param>
        /// <param name="uBound">Upper bound</param>
        /// <returns>The generated random number</returns>
        protected int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            // Assumes lBound >= 0 && lBound < uBound
            // returns an int >= lBound and < uBound
            uint urndnum;
            byte[] rndnum = new Byte[4];
            if (lBound == uBound - 1)
            {
                // test for degenerate case where only lBound can be returned  
                return lBound;
            }

            uint xcludeRndBase = 0;

            if (uBound != lBound)
            {
                xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(uBound - lBound)));
            }

            do
            {
                rng.GetBytes(rndnum);
                urndnum = BitConverter.ToUInt32(rndnum, 0);
            } while (urndnum >= xcludeRndBase);

            return (int)(urndnum % (uBound - lBound)) + lBound;
        }

        /// <summary>
        /// Generates a random character from the specified valid range of characters
        /// based on a generated random number between the character arrays bounds
        /// </summary>
        /// <returns>A random character within the allowed range</returns>
        protected char GetRandomCharacter()
        {
            int upperBound = pwdCharArray.GetUpperBound(0);

            if (ExcludeSymbols)
            {
                upperBound = UBoundDigit;
            }

            int randomCharPosition = GetCryptographicRandomNumber(pwdCharArray.GetLowerBound(0), upperBound);

            char randomChar = pwdCharArray[randomCharPosition];

            return randomChar;
        }

        /// <summary>
        /// Generates the random password based on the specified properties
        /// </summary>
        /// <returns>The random password</returns>
        public string Generate()
        {
            // Pick random length between minimum and maximum  
            int pwdLength = GetCryptographicRandomNumber(Minimum, Maximum);

            StringBuilder pwdBuffer = new StringBuilder();
            pwdBuffer.Capacity = Maximum;

            // Generate random characters
            char lastCharacter, nextCharacter;

            // Initial dummy character flag
            lastCharacter = nextCharacter = '\n';

            for (int i = 0; i < pwdLength; i++)
            {
                nextCharacter = GetRandomCharacter();

                if (false == ConsecutiveCharacters)
                {
                    while (lastCharacter == nextCharacter)
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                if (false == RepeatCharacters)
                {
                    string temp = pwdBuffer.ToString();
                    int duplicateIndex = temp.IndexOf(nextCharacter);
                    while (-1 != duplicateIndex)
                    {
                        nextCharacter = GetRandomCharacter();
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }

                if ((null != Exclusions))
                {
                    while (-1 != Exclusions.IndexOf(nextCharacter))
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                pwdBuffer.Append(nextCharacter);
                lastCharacter = nextCharacter;
            }

            if (pwdBuffer != null)
            {
                return pwdBuffer.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Validates a password against the defined properties
        /// </summary>
        /// <param name="password">The password to validate</param>
        /// <returns>True if the password is valid, otherwise false</returns>
        public bool Validate(string password)
        {
            int iCount;

            if (password.Length < minSize || password.Length > maxSize)
                return (false);

            // check for Consecutive characters
            if (!hasConsecutive) // cannot have consecutive characters
            {
                for (iCount = 0; iCount < password.Length - 1; iCount++)
                {
                    if (password[iCount] == password[iCount + 1])
                        return (false);
                }
            }

            if (!hasRepeating) // cannot have repeating characters
            {
                for (iCount = 0; iCount < password.Length; iCount++)
                {
                    int index = password.IndexOf(password[iCount]);
                    while (index != -1)
                    {
                        if (index != iCount)
                            return (false);
                        index = password.IndexOf(password[iCount]);
                    }
                }
            }

            if (Exclusions != null) // cannot have characters from exclusion string
            {
                for (iCount = 0; iCount < password.Length; iCount++)
                {
                    if (Exclusions.IndexOf(password[iCount]) != -1)
                        return (false);
                }
            }

            if (ExcludeSymbols) // cannot contain 'symbols'
            {
                for (iCount = UBoundDigit; iCount < pwdCharArray.GetUpperBound(0); iCount++)
                {
                    if (password.IndexOf(pwdCharArray[iCount]) != -1)
                        return (false);
                }
            }

            return (true);
        }

        #endregion
    }
}
