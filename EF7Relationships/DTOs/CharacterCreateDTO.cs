namespace EF7Relationships.DTOs
{
    public record struct CharacterCreateDTO(
        string Name,
        BackpackCreateDTO Backpack,
        List<WeaponCreateDTO> Weapons,
        List<FactionCreateDTO> Factions);
}
