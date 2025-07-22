namespace PetOwnerWasm.ViewModel;

public record class Subscription(SubscriptionFrequency Frequency, double Amount, bool IsDirectDebit, bool IsUpToDate);

