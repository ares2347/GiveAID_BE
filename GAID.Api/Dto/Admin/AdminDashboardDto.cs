using MassTransit.Futures.Contracts;

namespace GAID.Api.Dto.Admin;

public class AdminDashboardDto
{
    public General General { get; set; } = new();
    public Time Time { get; set; } = new();
    public Activity Activity { get; set; } = new();
}

public class Activity
{
    public RegistrationBrief MostRegistered { get; set; } = new();
    public DonationBrief MostDonated { get; set; } = new();
    public PartnerBrief MostPartners { get; set; } = new();
}

public class RegistrationBrief
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Registered { get; set; }
}public class DonationBrief
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Donations { get; set; }
}

public class PartnerBrief
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Programs { get; set; }
}

public class Time
{
    public int NewUsersMonth { get; set; }
    public int NewActivitiesMonth { get; set; }
    public decimal TotalDonationsMonth { get; set; }
}

public class General
{
    public int TotalUser { get; set; }
    public int TotalActivities { get; set; }
    public decimal TotalDonations { get; set; }
}