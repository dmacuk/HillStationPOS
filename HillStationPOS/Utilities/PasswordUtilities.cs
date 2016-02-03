using HillStationPOS.Constants;
using Utils.Preference;
using Utils.String;

namespace HillStationPOS.Utilities
{
    internal static class PasswordUtilities
    {
        /// <summary>
        ///     Get the encrypted password from the preferences and decrypt it
        /// </summary>
        /// <returns>A decrypted password</returns>
        public static string GetPassword()
        {
            var encryptedPassword = PreferenceManager.GetPreference(PrefConstants.Password,
                "arif".Encrypt(KeyConstants.Key));
            var decryptedPassword = encryptedPassword.Decrypt(KeyConstants.Key);
            return decryptedPassword;
        }

        /// <summary>
        ///     Encrypt the passed password and set in preferences. Always persist the preference
        /// </summary>
        /// <param name="decryptedPassword">A human readable password</param>
        public static void SetPassword(string decryptedPassword)
        {
            var encryptedPassword = decryptedPassword.Encrypt(KeyConstants.Key);
            PreferenceManager.SetPreference(PrefConstants.Password, encryptedPassword);
            PreferenceManager.SavePreferences();
        }
    }
}