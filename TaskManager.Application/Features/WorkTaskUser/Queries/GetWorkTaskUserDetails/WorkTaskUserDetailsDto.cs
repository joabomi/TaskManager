using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.WorkTaskUser.Queries.GetWorkTaskUserDetails;

//This class is created containing the same properties as the WorkTaskUserDto class 
//to easily add new detailed properties if needed in future
public class WorkTaskUserDetailsDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
