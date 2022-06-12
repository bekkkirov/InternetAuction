namespace InternetAuction.DAL.Entities
{
    /// <summary>
    /// Defines all possible values of the lot status.
    /// </summary>
    public enum LotStatus
    {
        Active,
        Ended,
        CanceledByUser,
        CanceledByModerator
    }
}