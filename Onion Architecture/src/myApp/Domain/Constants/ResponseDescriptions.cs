﻿namespace Domain.Constants;

public static class ResponseDescriptions
{
    public const string EXCEPTION_DETAIL = "Genel hata detaylarını gösterir. Error Codes : INVALID_USER ve USER_DELETED hata kodları gelebilir.";


    #region Auth Descriptions

    public const string AUTH_LOGIN = "Login işleminde kullanıcı bulunmazsa 401 fırlatır. Error Codes : INVALID_USER ve USER_DELETED hata kodları gelebilir.";
    public const string AUTH_REFRESHTOKEN = "Refresh token işleminde token ve kullanıcı bulunmazsa 400 fırlatır. Error Codes : INVALID_TOKEN, INVALID_USER ve USER_DELETED hata kodları gelebilir.";
    public const string AUTH_REGISTER =
        "Register işleminde Password alanı minumum 8 haneli olmalıdır.<br>" +
        "Password alanı minimum 6 karakter olmalıdır.<br>" +
        "Password alanı en az birer adet büyük harf, küçük harf, rakam, sembol içermelidir" +
        "PhoneNumber alanı minumum 8 haneli olmalıdır.<br>" +
        "FirstName ve LastName alanları minimum 2 karakter olmalıdır." +
        "Email kayıtlıysa 400 hatası fırlatır. Error Codes : EMAIL_EXIST hata kodları gelebilir.";

    public const string AUTH_FORGOTPASSWORD = "Şifre yenileme işleminde email bulunmazsa 400 fırlatır. 1 dakika geçerli bir token olan mail gönderilir. Doğrulama işlemi için validate-password-reset servisini kullanabilirsiniz. Error Codes : INVALID_EMAIL, EMAIL_SEND_ERROR hata kodları gelebilir.";
    public const string AUTH_FORGOTPASSWORD_TOKEN_VALIDATE = "Şifre yenilemede oluşan tokenı dogrulama için kullanılır . Error Codes : INVALID_TOKEN, TOKEN_EXPIRED, INVALID_USER hata kodları gelebilir.";
    public const string AUTH_FORGOTPASSWORD_VALIDATE = "Şifre yenileme işlemini dogrulama için kullanılır . Error Codes : INVALID_TOKEN, TOKEN_EXPIRED, INVALID_USER hata kodları gelebilir.";
    public const string AUTH_CHANGEPASSWORD = "Şifre yenileme işlemi. . Error Codes : USER_DELETED";
    public const string AUTH_LOGOUT = "Logout servisi";

    #endregion


    #region User Descriptions

    public const string USER_GET_PROFILE = "Giriş yapmış kullanıcının bilgilerini döner. Error Codes : INVALID_USER hata kodları gelebilir.";
    public const string USER_UPDATE_PROFILE = "Giriş yapmış kullanıcının bilgilerini güncellemenizi sağlar. Şifre 6 haneli olmalıdır! Error Codes : EMAIL_EXIST, INVALID_USER hata kodları gelebilir.";
    public const string USER_UPDATE_PHOTO = "Giriş yapmış kullanıcının profil resmini günceller. Error Codes : FILE_UPLOAD_ERROR, INVALID_USER hata kodları gelebilir.";
    public const string USER_DEACTIVATE_ACCOUNT = "Giriş yapmış kullanıcının hesabını devre dışı bırakır. Error Codes : INVALID_USER hata kodları gelebilir.";
    public const string USER_ADD_PROFILE = "Sisteme eklenmiş kullanıcının bilgilerini döner. Error Codes : INVALID_USER hata kodları gelebilir.";

    #endregion


}

