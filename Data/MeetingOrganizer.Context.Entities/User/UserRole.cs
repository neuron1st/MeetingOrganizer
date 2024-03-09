using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class UserRole : IdentityRole<Guid>
{
}
