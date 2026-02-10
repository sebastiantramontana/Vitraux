namespace FromTemplate;

public record class FromTemplateViewModel(DateTime Now = default!, IEnumerable<SuffledNumbersLevel> ShuffledNumbers = default!);

public record class SuffledNumbersLevel(int ShuffledNumber, IEnumerable<SuffledNumbersLevel> Children);