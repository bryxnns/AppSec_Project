using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public ApplicationUser AppUser { get; set; }

        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ProfileModel(UserManager<ApplicationUser> UserManager)
        {
            this.userManager = UserManager;
        }

        public async Task OnGet()
        {
            AppUser  = await userManager.GetUserAsync(User);
            
        }

        private string Decrypt(string cipherText)
        {
            string encryptionKey = "E)H@McQfThWmZq4t7w!z%C*F-JaNdRgU";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }
    }
}
