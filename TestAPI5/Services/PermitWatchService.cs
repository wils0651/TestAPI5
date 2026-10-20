using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.ExternalTypes;
using TestAPI5.Models;

namespace TestAPI5.Services
{
    public class PermitWatchService : IPermitWatchService
    {
        private readonly IPermitFindingRepository _permitFindingRepository;
        private readonly IPermitWatchRepository _permitWatchRepository;

        public PermitWatchService(IPermitFindingRepository permitFindingRepository, IPermitWatchRepository permitWatchRepository)
        {
            _permitFindingRepository = permitFindingRepository;
            _permitWatchRepository = permitWatchRepository;
        }

        public async Task<List<PermitFindingReturn>> ListRecentFindingsAsync(DateTime? startDate)
        {
            startDate ??= DateTime.UtcNow.AddDays(-14);

            // found_at is `timestamp without time zone` storing genuine UTC instants as
            // Kind=Unspecified (see Models/PermitFinding.cs). ToUniversalTime() would treat this
            // value as local time and shift it by the server's UTC offset; SpecifyKind
            // reinterprets it as-is, matching how AvailabilityScanService wrote it.
            var cutoff = DateTime.SpecifyKind(startDate.Value, DateTimeKind.Unspecified);

            var findings = await _permitFindingRepository.ListRecentAsync(cutoff);

            return findings
                .Select(MapToReturn)
                .ToList();
        }

        public async Task<List<PermitWatchReturn>> ListWatchConfigAsync()
        {
            var permits = await _permitWatchRepository.ListPermitsAsync();
            var windows = await _permitWatchRepository.ListWatchWindowsAsync();
            var exceptions = await _permitWatchRepository.ListWatchDateExceptionsAsync();

            return permits
                .Select(p => MapToReturn(p, windows, exceptions))
                .ToList();
        }

        public async Task<PermitWatchReturn> SaveWatchWindowAsync(SaveWatchWindowRequest request)
        {
            var existing = await _permitWatchRepository.GetWatchWindowAsync(request.PermitId);

            if (existing == null)
            {
                _permitWatchRepository.AddWatchWindow(new WatchWindow
                {
                    PermitId = request.PermitId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    IsActive = request.IsActive,
                });
            }
            else
            {
                existing.StartDate = request.StartDate;
                existing.EndDate = request.EndDate;
                existing.IsActive = request.IsActive;
                _permitWatchRepository.UpdateWatchWindow(existing);
            }

            await _permitWatchRepository.SaveChangesAsync();

            return await GetPermitWatchReturnAsync(request.PermitId);
        }

        public async Task<PermitWatchReturn> SaveWatchDateExceptionAsync(SaveWatchDateExceptionRequest request)
        {
            var existing = await _permitWatchRepository.GetWatchDateExceptionAsync(request.PermitId, request.ExceptionDate);

            if (existing == null)
            {
                _permitWatchRepository.AddWatchDateException(new WatchDateException
                {
                    PermitId = request.PermitId,
                    ExceptionDate = request.ExceptionDate,
                    IsIncluded = request.IsIncluded,
                });
            }
            else
            {
                existing.IsIncluded = request.IsIncluded;
                _permitWatchRepository.UpdateWatchDateException(existing);
            }

            await _permitWatchRepository.SaveChangesAsync();

            return await GetPermitWatchReturnAsync(request.PermitId);
        }

        private async Task<PermitWatchReturn> GetPermitWatchReturnAsync(int permitId)
        {
            var watchConfig = await ListWatchConfigAsync();

            return watchConfig.FirstOrDefault(w => w.PermitId == permitId);
        }

        private static PermitFindingReturn MapToReturn(PermitFinding finding)
        {
            return new PermitFindingReturn
            {
                FindingId = finding.FindingId,
                PermitId = finding.PermitId,
                DisplayName = finding.Permit.DisplayName,
                DivisionId = finding.DivisionId,
                LaunchDate = finding.LaunchDate,
                Remaining = finding.Remaining,
                Total = finding.Total,
                FoundAt = finding.FoundAt,
                NotifiedAt = finding.NotifiedAt,
                // Findings arrive on the daemon's staggered 1-8 min scan loop; a day-old finding
                // no longer counts as "just happened" even if the permit is still open.
                IsRecent = DateTime.UtcNow.Subtract(finding.FoundAt).TotalHours < 24
            };
        }

        private static PermitWatchReturn MapToReturn(Permit permit, List<WatchWindow> windows, List<WatchDateException> exceptions)
        {
            var window = windows.FirstOrDefault(w => w.PermitId == permit.PermitId);

            return new PermitWatchReturn
            {
                PermitId = permit.PermitId,
                DisplayName = permit.DisplayName,
                DivisionId = permit.DivisionId,
                StartDate = window?.StartDate,
                EndDate = window?.EndDate,
                IsActive = window?.IsActive ?? false,
                Exceptions = exceptions
                    .Where(e => e.PermitId == permit.PermitId)
                    .Select(e => new WatchDateExceptionReturn { ExceptionDate = e.ExceptionDate, IsIncluded = e.IsIncluded })
                    .ToList()
            };
        }
    }
}
