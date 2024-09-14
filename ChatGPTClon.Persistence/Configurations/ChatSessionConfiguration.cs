using ChatGPTClone.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatGPTClonePersistence.Configurations;

using ChatGPTClone.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

public class ChatSessionConfiguration : IEntityTypeConfiguration<ChatSession>
{
    public void Configure(EntityTypeBuilder<ChatSession> builder)
    {
        //ID alanını primary key olarak belirliyoruz
        builder.HasKey(p => p.Id);

        //Title alanını 100 karaktere kadar ve zorunlu olarak belirliyoruz
        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        //Model alanını zorunlu olarak belirliyoruz
        builder.Property(p => p.Model)
            .IsRequired()
            .HasConversion<int>();

        // Threads (JSONB configuration)
        builder.Property(p => p.Threads)
            .HasColumnType("jsonb")
            .IsRequired();

        // Configure JSON serialization options
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy =
                JsonNamingPolicy.CamelCase, // Threads => threads in JSON // ChatMessage => chatMessage
            WriteIndented = true
        };

        // Configure value conversion and comparison for Threads
        builder.Property(p => p.Threads)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<List<ChatThread>>(v, jsonOptions) ?? new List<ChatThread>(),
                new ValueComparer<List<ChatThread>>(
                    (c1, c2) => JsonSerializer.Serialize(c1, jsonOptions) == JsonSerializer.Serialize(c2, jsonOptions),
                    c => c == null ? 0 : JsonSerializer.Serialize(c, jsonOptions).GetHashCode(),
                    c => JsonSerializer.Deserialize<List<ChatThread>>(JsonSerializer.Serialize(c, jsonOptions),
                        jsonOptions)
                )
            );

        // Index on JSONB column for better performance
        builder.HasIndex(p => p.Threads)
            .HasMethod("gin");

        // Configure JSONB operations
        // builder.HasQueryFilter(p => EF.Functions.JsonContains(p.Threads, @"{""id"": ""some-id""}"));

        //// AppUser relationship
        //builder.HasOne(p => p.AppUser)
        //    .WithMany()
        //    .HasForeignKey(p => p.AppUserId)
        //    .OnDelete(DeleteBehavior.Cascade);

        // CreatedOn (zorunlu) ve null olamaz
        builder.Property(p => p.CreatedOn)
            .IsRequired();

        // CreatedByUserId (zorunlu) ve maksimum 150 karakter
        builder.Property(p => p.CreatedByUserId)
            .IsRequired()
            .HasMaxLength(150);

        // ModifiedOn (zorunlu değil) ve null olabilir
        builder.Property(p => p.ModifiedOn)
            .IsRequired(false);

        // ModifiedByUserId (zorunlu değil) ve maksimum 150 karakter
        builder.Property(p => p.ModifiedByUserId)
            .IsRequired(false)
            .HasMaxLength(150);
        //Tablo adını belirliyoruz
        builder.ToTable("ChatSessions");
    }
}