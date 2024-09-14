namespace ChatGPTClone.Application.Common.Localization;

public static class CommonLocalizationKeys
{
    //Genel doğrulama hatası durumunda fırlatılacak istisna
    public static string GeneralValidationException => nameof(GeneralValidationException);
    
    //Genel bir nesne bulunamadığında fırlatılacak istisna
    public static string GeneralNotFoundException => nameof(GeneralNotFoundException);
    
    //Genel bir sunucu hatası durumunda fırlatılacak istisna
    public static string GeneralInternalServerException => nameof(GeneralInternalServerException);
    
    //Zorunlu alan doğrulama hatası
    public static string ValidationIsRequired => nameof(ValidationIsRequired);
    
    //Geçersiz alan doğrulama hatası
    public static string ValidationIsInvalid => nameof(ValidationIsInvalid);
    
    //Belirli bir aralıkta olması gereken alan doğrulama hatası
    public static string ValidationMustBeBetween => nameof(ValidationMustBeBetween);
}