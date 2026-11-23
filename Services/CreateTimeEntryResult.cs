namespace TimesheetApi.Services
{
    // De service kan om 3 redenen anders antwoorden bij het aanmaken van een registratie.
    // Met deze enum weet de controller welke statuscode hij moet teruggeven.
    public enum CreateTimeEntryResult
    {
        Success,          // 201 Created
        ProjectNotFound,  // 400 Bad Request: ProjectId verwijst niet naar een bestaand project
        ProjectClosed     // 409 Conflict: geen uren op een afgesloten project
    }
}
