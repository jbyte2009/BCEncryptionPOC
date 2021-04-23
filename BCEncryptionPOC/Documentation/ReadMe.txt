
This may help you with Encryption using either AES or TwoFish algorithm. My recommendation is TwoFish.  TwoFish is created by Bruce Schneier.  One of the best Subject Matter Experts in the field of Cryptography.

The code is written in C# logic.  It references the following NuGet Package called BouncyCastle.

You will have to create 2 token keys in order to encrypt.  Personally, I created 2 seperate files and placed them in a Network location that has strict network access to them and through code I give the location and consume the file in memory.

In order to get 2 token keys you will need to use the static method TokenKeyGenerator.GetKey().  This will create a byte key and convert it into a base 64 string that you can store in the 2 files:  EncryptKey1.txt and EncryptKey2.txt.

The EncryptionUtility's 2 main methods are Encrypt and Decrypt.  They take the EncryptDTO and DecryptDTO and a UNC path location to the 2 files where the key is stored.

EncryptionBase.cs is where you get the full path of the file location.  There are 2 methods GetKey1 and GetKey2.  The inLocation is the Network UNC Path.  It basically concatenates the location with the filename.  The methods get the text and converts the key into a byte array and passes it to the Encryptor engine.

inLocation:  "\\[sub-domain].[domain].com\share\Departments\IT\Application Development\BCKey"
sInit.EncryptKeyFileName1 = "{0}\EncryptKey1.txt"
sInit.EncryptKeyFileName2 = "{0}\EncryptKey2.txt"

Results of path location:

\\[sub-domain].[domain].com\share\Departments\IT\Application Development\BCKey\EncryptKey1.txt
\\[sub-domain].[domain].com\share\Departments\IT\Application Development\BCKey\EncryptKey2.txt


In my case the inLocation is part of AWS Network location.  Yours can be one of your choice.

Please note:  I strongly discourage putting your Token keys as an embedded resource.  It is better to use a network location.,
For sakes of running this program, I have used an Embedded Resource.

Additional resources:

Nuget Package:  BouncyCastle 1.8.1

Twofish:  https://en.wikipedia.org/wiki/Twofish

Bruce Schneier:  https://www.schneier.com/

Applied Crytography - Bruce Schneier:  https://www.amazon.com/Applied-Cryptography-Protocols-Algorithms-Source/dp/0471117099

Applied Cryptography is a great book and is a quick read.  Bruce does a great job explaining Cryptography concepts.



