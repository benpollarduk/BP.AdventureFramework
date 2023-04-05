using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using AdventureFramework.Structure;

namespace AdventureFramework.IO
{
    /// <summary>
    /// A class for saving and loading Game objects from a file
    /// </summary>
    public class GameSave
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the salt value for all encryption
        /// </summary>
        protected static readonly byte[] salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        /// <summary>
        /// Get or set the salt value for all encryption
        /// </summary>
        protected const string password = "07GH&ghdfbnj&^£$45";

        /// <summary>
        /// Get a string array of illegal file handling characters
        /// </summary>
        public static readonly string[] ILLEGAL_FILE_HANDLING_CHARACTERS = { "/", "?", "<", ">", "\\", ":", "*", "|", "[", "]", ".", ";", "=", "{", "}" };

        /// <summary>
        /// Get or set the file for all GameSave's to reside in
        /// </summary>
        public static readonly string DefaultDirectory = AppDomain.CurrentDomain.BaseDirectory + "Saves\\";

        /// <summary>
        /// Occurs when a Game is sucsessfuly loaded
        /// </summary>
        public static event GameIOEventHandler GameLoaded;

        /// <summary>
        /// Occurs when a Game is sucsessfuly saved
        /// </summary>
        public static event GameIOEventHandler GameSaved;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Save the progress of the current game
        /// </summary>
        /// <param name="fullPath">The path to save the file to</param>
        /// <param name="game">The game to save</param>
        /// <param name="message">A message detailing the save</param>
        /// <param name="useEncryption">Specify if encryption is used</param>
        /// <returns>If the save was sucsessful</returns>
        public static bool SaveGameAsXML(string fullPath, Game game, out string message, bool useEncryption)
        {
            try
            {
                // hold XML data
                var XMLData = XMLSerializableObject.SerializeToXML(game);

                // if using encryption 
                if (useEncryption)
                    // encrypt data
                    XMLData = EncryptStringAES(XMLData, password);

                // create writer
                using (TextWriter w = new StreamWriter(fullPath))
                {
                    // write file
                    w.Write(XMLData);
                }

                // if subscribers
                if (GameSaved != null)
                    // dispatch
                    GameSaved(game, new GameIOEventArgs(game));

                // set message
                message = "Save complete";

                // pass
                return true;
            }
            catch (Exception e)
            {
                // hold message
                message = e.Message;

                // failed
                return false;
            }
        }

        /// <summary>
        /// Load a game
        /// </summary>
        /// <param name="fullPath">The path of the game to load</param>
        /// <param name="game">The loaded game</param>
        /// <param name="message">A message detailing the load</param>
        /// <param name="useDecryption">Specify if decryption should be used</param>
        /// <returns>If the load was sucsessfil</returns>
        public static bool LoadGameAsXML(string fullPath, Game game, out string message, bool useDecryption)
        {
            try
            {
                // if file exists
                if (File.Exists(fullPath))
                {
                    // create document
                    var doc = new XmlDocument();

                    // hold text
                    string text;

                    // if no game
                    if (game == null)
                        // create new
                        game = new Game(string.Empty, string.Empty, null, null);

                    // open reader
                    using (TextReader reader = new StreamReader(fullPath))
                    {
                        // read all text in file
                        text = reader.ReadToEnd();
                    }

                    // if using decryption
                    if (useDecryption)
                        // decrypt string
                        text = DecryptStringAES(text, password);

                    // load to XML
                    doc.LoadXml(text);

                    // find root node
                    var root = Game.FindRootNode(doc);

                    // if no root found
                    if (root == null)
                        // throw exception
                        throw new ArgumentException("The root node for the game could not be found");

                    // hold game name - set as something crazy and random
                    var loadedFilesGameName = Environment.TickCount.ToString();

                    // if name exists
                    if (XMLSerializableObject.AttributeExists(root, "Name"))
                        // set name
                        loadedFilesGameName = XMLSerializableObject.GetAttribute(root, "Name").Value;

                    // if names match
                    if (game.Name == loadedFilesGameName)
                    {
                        // read the XML node, and allow deserialisation to commence!
                        game.ReadXmlNode(root);

                        // if subscribers
                        if (GameLoaded != null)
                            // dispatch
                            GameLoaded(game, new GameIOEventArgs(game));

                        // set message
                        message = "Load complete";

                        // pass
                        return true;
                    }

                    // set message
                    message = "The file could not be loaded as it is not a save for this game";

                    // fail
                    return false;
                }

                // fail
                throw new Exception("The file could not be found");
            }
            catch (Exception e)
            {
                // hold message
                message = e.Message;

                // no game
                game = null;

                // failed
                return false;
            }
        }

        /// <summary>
        /// Encrypt the a string using AES
        /// </summary>
        /// <param name="plainText">The text to encrypt</param>
        /// <param name="password">A password used to generate a key for encryption</param>
        protected static string EncryptStringAES(string plainText, string password)
        {
            // based on http://stackoverflow.com/questions/202011/encrypt-decrypt-string-in-net

            // if text if null or empty
            if (string.IsNullOrEmpty(plainText))
                // throw exception
                throw new ArgumentNullException("Parameter plainText is either null or empty. You must provide a valid string");

            // if text if null or empty
            if (string.IsNullOrEmpty(password))
                // throw exception
                throw new ArgumentNullException("Parameter password is either null or empty. You must provide a valid string");

            // the string to return as encrypted
            string encryptedInput = null;

            // RijndaelManaged object used to encrypt the data
            RijndaelManaged aesAlgorithm = null;

            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(password, salt);

                // create new a RijndaelManaged object
                aesAlgorithm = new RijndaelManaged();

                // set algorithm key
                aesAlgorithm.Key = key.GetBytes(aesAlgorithm.KeySize / 8);

                // set algorithm vector
                aesAlgorithm.IV = key.GetBytes(aesAlgorithm.BlockSize / 8);

                // create a encrytor to perform the stream transform
                var encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                // create the memory stream for encryption
                using (var msEncrypt = new MemoryStream())
                {
                    // create crypto stream
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        // create writer
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // write all data to the stream
                            swEncrypt.Write(plainText);
                        }
                    }

                    // now convert to base64 from the encrypted stream
                    encryptedInput = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // if an algorithm object
                if (aesAlgorithm != null)
                {
                    // clear
                    aesAlgorithm.Clear();

                    // relase
                    aesAlgorithm = null;
                }
            }

            // return the encrypted input
            return encryptedInput;
        }

        /// <summary>
        /// Decrypt a string that has been encrypted with the GameSave.EncryptStringAES() method
        /// </summary>
        /// <param name="cipherText">The text to decrypt</param>
        /// <param name="password">A password used to generate a key for decryption. This must be the same as the password used when encrypting</param>
        protected static string DecryptStringAES(string cipherText, string password)
        {
            // if null or empty input
            if (string.IsNullOrEmpty(cipherText))
                // throw exception
                throw new ArgumentNullException("Parameter cipherText is either null or empty. You must provide a valid string");

            // if null or empty input
            if (string.IsNullOrEmpty(password))
                // throw exception
                throw new ArgumentNullException("Parameter password is either null or empty. You must provide a valid string");

            // create algorithm
            RijndaelManaged aesAlgorithm = null;

            // holds the decrypted text
            string plainText = null;

            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(password, salt);

                // create the algorithm
                aesAlgorithm = new RijndaelManaged();

                // set the key
                aesAlgorithm.Key = key.GetBytes(aesAlgorithm.KeySize / 8);

                // set the vector
                aesAlgorithm.IV = key.GetBytes(aesAlgorithm.BlockSize / 8);

                // create a decrytor to perform the stream transform
                var decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                // create byte array to hold bytes from the base 64 string    
                var bytes = Convert.FromBase64String(cipherText);

                // create stream for the decryption
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // create a new crypto stream and read memory stream
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        // now read the text from the stream
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // set the plain text
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                // if an algorithm object
                if (aesAlgorithm != null)
                {
                    // clear it
                    aesAlgorithm.Clear();

                    // release
                    aesAlgorithm = null;
                }
            }

            // return the plain text
            return plainText;
        }

        #endregion
    }
}