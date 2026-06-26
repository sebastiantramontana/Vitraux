namespace FromTemplate;

public record class FromTemplateViewModel(DateTime Now, IEnumerable<NumbersLevel> Numbers);

public record class NumbersLevel(int Level, int Number,  IEnumerable<NumbersLevel> Children);