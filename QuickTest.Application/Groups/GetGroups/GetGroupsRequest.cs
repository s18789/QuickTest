using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Groups.GetGroups;

public class GetGroupsRequest : IRequest<IEnumerable<GroupDto>>
{
}
