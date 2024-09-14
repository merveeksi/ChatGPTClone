using ChatGPTClone.Domain.Common;
using ChatGPTClone.Domain.Enums;
using ChatGPTClone.Domain.ValueObjects;

namespace ChatGPTClone.Domain.Entities;

public sealed class ChatSession:EntityBase //seald class ile kalıtım alınmasını engelledik //miras vermeyecek bir class
{
    public string Title { get; set; }

    public GptModelType Model { get; set; } 
    
    //If the type is list in the entity then it is a ValueObject
    public List<ChatThread> Threads { get; set; } = []; //new list anlamına geliyor
    public Guid AppUserId { get; set; }
    
    //If the type of the list is ICollection then it is an Entity
    //publi ICollection<ChatMessage> ChatMessages { get; set; }
   
}