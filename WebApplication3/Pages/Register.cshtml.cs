using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Cryptography;
using System.Text;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private IWebHostEnvironment _environment;
        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        [BindProperty]
        public Register RModel { get; set; }


        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment environment, 
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _environment = environment;
        }



        public void OnGet()
        {
        }

        private string Encrypt(string clearText)
        {
            string encryptionKey = "E)H@McQfThWmZq4t7w!z%C*F-JaNdRgU";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload",
                        "File size cannot exceed 2MB.");
                        return Page();
                    }
                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    RModel.Image = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }



                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,

                    Full_Name = RModel.Full_Name,
                    Credit_Card = Encrypt(RModel.Credit_Card),
                    Gender = RModel.Gender,
                    Mobile_No = RModel.Mobile_No,
                    Delivery_Address = RModel.Delivery_Address,
                    AboutMe = RModel.AboutMe,
                    Image = RModel.Image
                };

                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Error message: ", error.Description);
                }
            };
             return Page();   
        }        
    }
}

