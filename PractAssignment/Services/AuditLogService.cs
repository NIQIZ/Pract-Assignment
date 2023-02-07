using PractAssignment.Models;

namespace PractAssignment.Services
{
    public class AuditLogService
    {
        private AuthDbContext _context;

        public AuditLogService(AuthDbContext context)
        {
            _context = context;
        }

    
        public async Task AddLoginLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Login",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
         
        }
    
        public async Task Add2FactorLoginLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "2FA Login",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
         
        }
        public async Task AddInvalid2FactorInputLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Invalid 2FA Input attempt",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
        }
        public async Task AddInvalidLoginAttemptLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Invalid Login Attempt",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
        }
        
        public async Task AddLockoutLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Lockout",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
        }
        
        public async Task AddLogoutLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Logout",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
        }
    
        public async Task AddChangePasswordLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Change Password",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
          
        }
    
        public async Task AddPasswordResetLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Password Reset",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
     
        }

        public async Task AddSetUp2FactorLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Set Up 2FA",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
        }
    
        public async Task AddDisable2FactorLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Disable 2FA",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
         
        }
    
        public async Task AddReset2FactorLog(ApplicationUser user)
        {
            AuditLog newLog = new AuditLog()
            {
                User = user,
                Action = "Reset 2FA",
                ActionTime = DateTime.Now
            };
            _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();          
         
        }
    }
}

