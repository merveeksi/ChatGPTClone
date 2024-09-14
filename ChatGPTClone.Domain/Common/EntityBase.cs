using ChatGPTClone.Domain.Common;

namespace ChatGPTClone.Domain.Common;

public abstract class EntityBase: IEntity,ICreatedByEntity, IModifiedByEntity //kod tekrarını önlemek için oluşturulmuş bir base class
{
    public virtual Guid Id { get; set; } //unic olarak tanımlanmış bir id   //virtual: ezilebilir olmasını sağlar, özelleri ezerek kullanabiliriz

    public virtual DateTimeOffset CreatedOn{ get; set; } //kayıt ne zaman oluşturuldu

    public virtual string CreatedByUserId { get; set; } //kayıt kim tarafından oluşturuldu
    

    public virtual DateTimeOffset? ModifiedOn { get; set; } //kayıt ne zaman değiştirildi

    public virtual string ModifiedByUserId { get; set; } //kayıt kim tarafından değiştirildi
}