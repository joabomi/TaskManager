using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastModificationDate { get; set; }
}
