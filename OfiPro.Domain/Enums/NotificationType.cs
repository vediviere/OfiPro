namespace OfiPro.Domain.Enums;

public enum NotificationType
{
    General = 1,

    ProposalReceived = 2,
    ProposalAccepted = 3,
    ProposalRejected = 4,

    ContractCreated = 5,
    ContractStarted = 6,
    ContractFinished = 7,
    ContractCancelled = 8,

    EvidenceUploaded = 9,

    ContractPendingConfirmation = 10
}