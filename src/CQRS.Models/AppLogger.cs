using System;
using Microsoft.Extensions.Logging;

namespace CQRS.Models
{
    public class AppLogger
    {
        private readonly ILogger<AppLogger> _logger;

        public AppLogger(ILogger<AppLogger> logger)
        {
            _logger = logger;
        }

        public int Warning(string message)
        {
            var id = GenerateId();
            _logger.LogWarning(id, message);
            return id;
        }

        public int Error(Exception ex)
        {
            return Error(ex.Message, ex);
        }

        public int Error(string message, Exception ex = null)
        {
            var id = GenerateId();
            _logger.LogError(id, ex, message);
            return id;
        }

        public void Information(string message)
        {
            _logger.LogInformation(message);
        }

        private static int GenerateId()
        {
            var ticks = DateTime.UtcNow.Ticks;
            var nextId = (int)ticks;
            return Math.Abs(nextId);
        }
    }
}
