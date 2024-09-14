namespace ChatGPTClone.Domain.Enums;

public enum ChatMessageType //mesaj kim tarafından gönderildi
{
    //The promp which is sent to the GPT model
    System = 1,    //bota ait mesajlar
    
    //The messahe which is sent by the user
    User = 2,     //kullanıcıya ait mesajlar
    
    //The message which is sent by the assistant
    Assistant = 3     //asistana ait mesajlar
}